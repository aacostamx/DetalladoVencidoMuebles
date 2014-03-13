use Carteras
go

if exists(select * from sysobjects where name = 'tmpFecha') drop table tmpFecha
select top 1 fechaventa
into tmpFecha
from opendatasource('sqloledb','data source=odscarteras.coppel.com;user id=consultas;password = carteras').sep2013.dbo.maecarteramuebles

if exists(select * from sysobjects where name = 'tmpFecha2') drop table tmpFecha2
select FechaV = (select cast(year(FechaVenta) as char(4)) + '-' + cast(month(FechaVenta) as varchar(2)) + '-01' from tmpFecha) into tmpFecha2

if exists(select * from sysobjects where name = 'tmpMueb') drop table tmpMueb
select numerocliente,fechaventa,ImporteVenta,PlazoVenta,InteresSobreCompra,abonomensual,Enganche,saldoalafecha,
vencido = substring(dbo.fnGeVencidoMuebles((select * from tmpFecha),fechaventa,ImporteVenta,InteresSobreCompra,PlazoVenta,abonomensual,saldoalafecha,Enganche),34,11),
FechaCorte = (select fechaventa from tmpFecha)
into dbo.tmpMueb
from opendatasource('sqloledb','data source=odscarteras.coppel.com;user id=consultas;password = carteras').sep2013.dbo.maecarteramuebles
where /*saldoalafecha > 0 and */PlazoVenta >= 18 and fechaventa >= (select dateadd(month,-5,FechaV) from tmpFecha2)
order by fechaventa

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