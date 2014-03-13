-- =============================================
--		  AF0122_DetalladoVencidoMuebles
-- =============================================
USE Carteras
GO

IF OBJECT_ID ('fnCalculaNombreMes','FN') IS NOT NULL DROP 
FUNCTION  fnCalculaNombreMes
GO
-- =============================================
-- Autor: Antonio Acosta Murillo
-- Fecha: 17 Enero 2014
-- Descripción General: Recibe la fecha en formato smalldatetime y la regresa en una cadena con el nombre y el año ejemplo: Ene2015
-- =============================================  
CREATE FUNCTION fnCalculaNombreMes(@FechaActual AS SMALLDATETIME)    
RETURNS CHAR(12)  
AS    
BEGIN  
 DECLARE @Mes AS CHAR(12)  
 SELECT @Mes =   
    CASE MONTH(@FechaActual)  
    WHEN 1  THEN 'Ene'+CAST(YEAR(@FechaActual) AS CHAR(4))  
    WHEN 2  THEN 'Feb'+CAST(YEAR(@FechaActual) AS CHAR(4))  
    WHEN 3  THEN 'Mar'+CAST(YEAR(@FechaActual) AS CHAR(4))  
    WHEN 4  THEN 'Abr'+CAST(YEAR(@FechaActual) AS CHAR(4))  
    WHEN 5  THEN 'May'+CAST(YEAR(@FechaActual) AS CHAR(4))  
    WHEN 6  THEN 'Jun'+CAST(YEAR(@FechaActual) AS CHAR(4))  
    WHEN 7  THEN 'Jul'+CAST(YEAR(@FechaActual) AS CHAR(4))  
    WHEN 8  THEN 'Ago'+CAST(YEAR(@FechaActual) AS CHAR(4))  
    WHEN 9  THEN 'Sep'+CAST(YEAR(@FechaActual) AS CHAR(4))  
    WHEN 10 THEN 'Oct'+CAST(YEAR(@FechaActual) AS CHAR(4))  
    WHEN 11 THEN 'Nov'+CAST(YEAR(@FechaActual) AS CHAR(4))  
    WHEN 12 THEN 'Dic'+CAST(YEAR(@FechaActual) AS CHAR(4))  
    END   
 RETURN(@Mes)  
END

GO

USE Carteras
GO
IF OBJECT_ID ('AF0122_DetalladoVencidoMuebles ','P') IS NOT NULL 
DROP PROCEDURE  AF0122_DetalladoVencidoMuebles
GO
CREATE PROCEDURE AF0122_DetalladoVencidoMuebles (@Fecha SMALLDATETIME)
AS
-- =============================================
-- Autor: Antonio Acosta Murillo
-- Fecha: 21 Febrero 2014
-- Descripción General: Genera Reporte Detallado Vencido Muebles
-- =============================================
BEGIN
DECLARE @CONSULTA VARCHAR(8000)
DECLARE @MesODS CHAR(12)
SET @MesODS=dbo.fnCalculaNombreMes(@Fecha)
 
--Selecciona la fecha de venta 
iF EXISTS(SELECT * FROM SYSOBJECTS WHERE NAME = 'tmpFecha') DROP TABLE tmpFecha
SET @CONSULTA =
'SELECT TOP 1 fechaventa
INTO tmpFecha
FROM OPENDATASOURCE(''sqloledb'',''data source=odscarteras.coppel.com;user id=consultas;password = carteras'').'+@MesODS+'.dbo.maecarteramuebles ORDER BY fechaventa DESC'
EXEC @CONSULTA

--Se selecciona la FechaV
IF EXISTS(SELECT * FROM SYSOBJECTS WHERE NAME = 'TMPFECHA2') DROP TABLE TMPFECHA2
SELECT FechaV = (SELECT CAST(YEAR(FechaVenta) AS CHAR(4)) + '-' + CAST(MONTH(FechaVenta) AS VARCHAR(2)) + '-01' FROM tmpFecha) INTO tmpFecha2

IF EXISTS(SELECT * FROM SYSOBJECTS WHERE NAME = 'tmpMueb') DROP TABLE tmpMueb
SET @Consulta=
'SELECT numerocliente,fechaventa,ImporteVenta,PlazoVenta,InteresSobreCompra,abonomensual,Enganche,saldoalafecha,
vencido = SUBSTRING(dbo.fnGeVencidoMuebles((SELECT * FROM tmpFecha),fechaventa,ImporteVenta,InteresSobreCompra,PlazoVenta,abonomensual,saldoalafecha,Enganche),34,11),
FechaCorte = (SELECT fechaventa FROM tmpFecha)
INTO dbo.tmpMueb
FROM OPENDATASOURCE(''sqloledb'',''data source=odscarteras.coppel.com;user id=consultas;password = carteras'').'+@MesODS+'.dbo.maecarteramuebles
WHERE /*saldoalafecha > 0 and */PlazoVenta >= 18 and fechaventa >= (SELECT DATEADD(MONTH,-5,FechaV) FROM tmpFecha2)
ORDER BY fechaventa'
EXEC @CONSULTA

if exists(select * from sysobjects where name = 'tmpMueb2') drop table tmpMueb2
select *,MesesTNoCob = (datediff(month,FechaVenta,Fechacorte)+1)
into dbo.tmpMueb2
from tmpMueb

update tmpMueb2 set MesesTNoCob =case when (MesesTNoCob) = 1 then 6
													when (MesesTNoCob) = 2 then 5
													when (MesesTNoCob) = 3 then 4
													when (MesesTNoCob) = 4 then 3
													when (MesesTNoCob) = 5 then 2
													when (MesesTNoCob) = 6 then 1 end
													
if exists(select * from sysobjects where name = 'tmpMueb3') drop table tmpMueb3
select *,LargoPlazo = (abonomensual*MesesTNoCob)
into dbo.tmpMueb3
from tmpMueb2
order by fechaventa

update tmpMueb3 set LargoPlazo=SaldoALaFecha
      where LargoPlazo>SaldoALaFecha

select * from tmpMueb3 order by fechaventa

END
GO
