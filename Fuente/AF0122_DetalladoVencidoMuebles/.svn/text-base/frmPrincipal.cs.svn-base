using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Globalization;
using System.Threading;

namespace AF0122_DetalladoVencidoMuebles
{
    public partial class frmPrincipal : Form
    {
        #region Variables Globales
        private ConexionSQL con = new ConexionSQL();
        private BackgroundWorker Worker = new BackgroundWorker();
        private BackgroundWorker Worker2 = new BackgroundWorker();
        public delegate void Fhandler();
        #endregion

        #region Constructor
        public frmPrincipal()
        {
            InitializeComponent();
            //Inicias el backgoundWorker y asignas verdadero el soporte de cancelacion
            InitializeBackgoundWorker();
            Worker.WorkerSupportsCancellation = true;
            Worker2.WorkerSupportsCancellation = true;
            //Se cancela los llamados por cruce de hilos
            CheckForIllegalCrossThreadCalls = false;
        }   
        #endregion

        #region Eventos
        private void frmPrincipal_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    this.Dispose();
                    this.Close();
                    break;
            }
        }

        private void bIniciar_Click(object sender, EventArgs e)
        {
            //Se desactivan los componentes y limpia las label
            bIniciar.Enabled = false;
            lbHoraInicialUsuario.Text = "--";
            lbHoraFinalUsuario.Text = "--";
            tsslbEstatus.Text = "--";
            Cursor = Cursors.WaitCursor;
            tsslbEstatus.Text = "Proceso en ejecución";
            Refresh();

            //aquí debes de asignar la hora a la hora de inicio 
            lbHoraInicialUsuario.Text = DateTime.Now.ToLongTimeString();
            Refresh();

            //corres los workers
            Worker2.RunWorkerAsync();
            Worker.RunWorkerAsync();
        }

        #endregion

        #region Logica

        private void Logica()
        {

            //es importante que el worker este dentro de un try catch
            try
            {
                //revisa que el archivo de conexion exista
                if (!File.Exists("C:\\Sys\\Exe\\Conexion\\Conexion58Carteras.txt"))
                {
                    MessageBox.Show("El archivo de texto de la conexión no existe", "Advertencia");
                    bIniciar.Enabled = true;
                    Cursor = Cursors.Default;
                    MessageBox.Show("Por favor revisa que el archivo de conexio Conexion58Carteras exista", "Aviso");
                    Application.Exit();
                }

                //Se declaran las variables a utilizar
                String sConexion, sSentencia, sFechaODS, sFechaODSAnterior, sFechaSistema;
                DataTable dtFecha = new DataTable();
                DataTable dtDetalladoMuebles = new DataTable();
                DataTable dtDetalladoMueblesAnterior = new DataTable();
                DataTable dtTotal = new DataTable();
                DateTime dtiFecha = new DateTime();

                //Se selecciona la fecha del cat fechas
                sSentencia = "SELECT Fecha FROM CatFechas";
                sConexion = con.LeeArchivo("C:\\Sys\\Exe\\Conexion\\Conexion58Carteras.txt");

                //Instancia un objecto de la clase Conexion y ejecuta la sentencia
                Conexion objConexion = new Conexion(sConexion);
                objConexion.LlenarDataTable(ref dtFecha, sSentencia);

                //Calcula la fecha del ODS actual y la fecha del ODS de un año atras
                dtiFecha = Convert.ToDateTime(dtFecha.Rows[0][0].ToString());
                sFechaSistema = dtiFecha.ToString("MMMM", (IFormatProvider)CultureInfo.CreateSpecificCulture("es-ES")).ToUpper() + dtiFecha.Year.ToString();
                sFechaODS = objConexion.RegresaMes(dtiFecha.Month) + dtiFecha.Year.ToString();
                dtiFecha = dtiFecha.AddYears(-1);
                sFechaODSAnterior = objConexion.RegresaMes(dtiFecha.Month) + dtiFecha.Year.ToString();

                //Metodo que ejecuta la logica
                DetalladoMuebles(sSentencia, sFechaODS, objConexion);

                //Se agrega a una tabla de datos el detallado de Muebles de la fecha Actual
                sSentencia = @"
                        select * 
                        from tmpMueb3 
                        order by fechaventa";
                objConexion.LlenarDataTable(ref dtDetalladoMuebles, sSentencia);

                //Calcula el total del detallado muebles del mes actual
               sSentencia = @"SELECT '','',SUM(ImporteVenta)ImporteVenta,'',SUM(InteresSobreCompra)InteresSobreCompra,SUM(abonomensual)abonomensual,SUM(Enganche)Enganche,SUM(saldoalafecha)saldoalafecha,'','','',SUM(LargoPlazo)LargoPlazo
                                FROM tmpMueb3";
               objConexion.LlenarDataTable(ref dtTotal, sSentencia);

                //busca todos los renglones del dtTotal e importa los renglones al dtDetalladoMuebles
                foreach (DataRow drTotal in dtTotal.Rows)
                    dtDetalladoMuebles.ImportRow(drTotal);

                //Invoca al metodo que transforma la tabla de datos en un archivo CSV
                DataTableToCsv(dtDetalladoMuebles, "DetalladoVencidoMuebles_" + sFechaSistema + ".csv");

                //Selecciona la fecha anterior del sistema para agregarla al nuevo archivo excel
                sFechaSistema = dtiFecha.ToString("MMMM", (IFormatProvider)CultureInfo.CreateSpecificCulture("es-ES")).ToUpper() + dtiFecha.Year.ToString();
                //Se borra la tabla de datos Total
                dtTotal.Clear();

                //Se ejecuta la logica otra vez pero esta vez con la fecha del año anterior
                DetalladoMuebles(sSentencia, sFechaODSAnterior, objConexion);


                //Se agrega a una tabla de datos el detallado de Muebles de la fecha menos un año
                sSentencia = @"
                        select * 
                        from tmpMueb3 
                        order by fechaventa";
                objConexion.LlenarDataTable(ref dtDetalladoMueblesAnterior, sSentencia);

                //Calcula el total del detallado muebles del mes anterior
                sSentencia = @"SELECT '','',SUM(ImporteVenta)ImporteVenta,'',SUM(InteresSobreCompra)InteresSobreCompra,SUM(abonomensual)abonomensual,SUM(Enganche)Enganche,SUM(saldoalafecha)saldoalafecha,'','','',SUM(LargoPlazo)LargoPlazo
                                FROM tmpMueb3";
                objConexion.LlenarDataTable(ref dtTotal, sSentencia);

                //busca todos los renglones del dtTotal e importa los renglones al dtDetalladoMueblesAnterior
                foreach (DataRow drTotal in dtTotal.Rows)
                    dtDetalladoMueblesAnterior.ImportRow(drTotal);

                //Invoca al metodo que transforma la tabla de datos en un archivo CSV
                DataTableToCsv(dtDetalladoMueblesAnterior, "DetalladoVencidoMuebles_" + sFechaSistema + ".csv");

                //se resetean los elementos del formulario
                dtDetalladoMuebles.Reset();
                dtDetalladoMueblesAnterior.Reset();
                dtFecha.Reset();
                dtTotal.Reset();

                //para finalizar se utiliza el delegado
                BeginInvoke((Delegate)new frmPrincipal.Fhandler(Finaliza));
                //para cancelar los workers
                Worker.CancelAsync();
                Worker2.CancelAsync();
            }
            catch (Exception ex)
            {
                //finaliza el worker
                BeginInvoke((Delegate)new frmPrincipal.Fhandler(this.Finaliza));
                //para cancelar el worker
                Worker.CancelAsync();
                Worker2.CancelAsync();
                int num = (int)MessageBox.Show("Error: " + ((object)ex.Message).ToString() + "\nSource: " + ((object)ex.Source).ToString() + "\nMetodo: " + ex.TargetSite.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private static void DetalladoMuebles(String sSentencia, String sFechaODS, Conexion objConexion)
        {
            sSentencia = @"
                if exists(select * from sysobjects where name = 'tmpFecha') drop table tmpFecha
                select top 1 fechaventa
                into tmpFecha
                from opendatasource('sqloledb','data source=odscarteras.coppel.com;user id=consultas;password = carteras')." + sFechaODS + @".dbo.maecarteramuebles order by fechaventa desc

                if exists(select * from sysobjects where name = 'tmpFecha2') drop table tmpFecha2
                select FechaV = (select cast(year(FechaVenta) as char(4)) + '-' + cast(month(FechaVenta) as varchar(2)) + '-01' from tmpFecha) into tmpFecha2

                if exists(select * from sysobjects where name = 'tmpMueb') drop table tmpMueb
                select numerocliente,fechaventa,ImporteVenta,PlazoVenta,InteresSobreCompra,abonomensual,Enganche,saldoalafecha,
                vencido = substring(dbo.fnGeVencidoMuebles((select * from tmpFecha),fechaventa,ImporteVenta,InteresSobreCompra,PlazoVenta,abonomensual,saldoalafecha,Enganche),34,11),
                FechaCorte = (select fechaventa from tmpFecha)
                into dbo.tmpMueb
                from opendatasource('sqloledb','data source=odscarteras.coppel.com;user id=consultas;password = carteras')." + sFechaODS + @".dbo.maecarteramuebles
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
                      where LargoPlazo>SaldoALaFecha";

            objConexion.EjecutarSentencia(sSentencia);
        }

        public static void DataTableToCsv(DataTable dt, string csvFile)
        {
            StringBuilder sb = new StringBuilder();

            var columnNames = dt.Columns.Cast<DataColumn>().Select(column => "\"" + column.ColumnName.Replace("\"", "\"\"") + "\"").ToArray();
            sb.AppendLine(string.Join(",", columnNames));

            foreach (DataRow row in dt.Rows)
            {
                var fields = row.ItemArray.Select(field => "\"" + field.ToString().Replace("\"", "\"\"") + "\"").ToArray();
                sb.AppendLine(string.Join(",", fields));
            }

            File.WriteAllText(csvFile, sb.ToString(), Encoding.Default);
        }
        #endregion

        #region Metodos
        //metodo para inicializar el backgroundworker 
        private void InitializeBackgoundWorker()
        {
            Worker.DoWork += new DoWorkEventHandler(backgroundWorker1_DoWork);
            Worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundWorker1_RunWorkerCompleted);
            Worker2.DoWork += new DoWorkEventHandler(backgroundWorker2_DoWork);
            Worker2.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundWorker2_RunWorkerCompleted);
        }

        //metodo de la interfaz del worker 1
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            Transcurrido();
        }
        //metodo que llama el procedimiento del worker 2
        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            Logica();
        }

        //si el worker 1 termina revisa que no tenga errores
        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error == null)
                return;
            int num = (int)MessageBox.Show(e.Error.Message);
        }

        //si el worker dos termina revisa que no tenga erroes
        private void backgroundWorker2_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error == null)
                return;
            int num = (int)MessageBox.Show(e.Error.Message);
        }

        //Metodo que invoca el metodo tiempo transcurrido
        public void Transcurrido()
        {
            try
            {
                MethodInvoker methodInvoker = new MethodInvoker(TiempoTranscurrido);
                while (!Worker.CancellationPending)
                {
                    BeginInvoke((Delegate)methodInvoker);
                    Thread.Sleep(500);
                }
            }
            catch (ThreadInterruptedException ex)
            {
                MessageBox.Show(ex.ToString(), "Aviso");
            }
        }

        //Calcula el tiempo transcurrido 
        private void TiempoTranscurrido()
        {
            //lbTime.Text = ("Transcurrido " + Convert.ToString((object)(DateTime.Now - Convert.ToDateTime(lbHoraInicio.Text)))).Substring(0, 21);
            //lbTimeTranscurrido.Text = Convert.ToString(((object)(DateTime.Now - Convert.ToDateTime(lbHoraInicialUsuario.Text)))).Substring(0, 21);
            lbTimeTranscurridoUsuario.Text = Convert.ToString(DateTime.Now - Convert.ToDateTime(lbHoraInicialUsuario.Text)).Substring(0, 8);
            //lbTimeTranscurrido.Text = DateTime.Now.Second.ToString();
            Refresh();
        }

        //si se cierra la forma principal se cancelan los workers
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Worker.CancelAsync();
            Worker2.CancelAsync();
        }

        public void Finaliza()
        {
            Cursor = Cursors.Default;
            lbHoraFinalUsuario.Text = DateTime.Now.ToLongTimeString();
            tsslbEstatus.Text = "Proceso Finalizado";
            bIniciar.Enabled = true;

            Refresh();
            Worker2.CancelAsync();
        }
        #endregion
    }
}
