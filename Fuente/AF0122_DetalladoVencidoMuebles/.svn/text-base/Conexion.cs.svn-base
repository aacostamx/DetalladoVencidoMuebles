using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace AF0122_DetalladoVencidoMuebles
{
    class Conexion
    {
        private string BD;          //DATA BASE
        private string IP;          //IP
        private string USER;        //USER
        private string PASS;        //PASS
        private SqlConnection con = new SqlConnection();
        private string cConexion;   //CADENA DE CONEXION
        private SqlCommand comm = new SqlCommand();
        private SqlDataAdapter da = new SqlDataAdapter();

        public SqlDataAdapter DataAdapter
        {
            get { return da; }
            set { da = value; }
        }

        public SqlCommand Command
        {
            get { return comm; }
            set { comm = value; }
        }

        public string cadenaConexion
        {
            get { return cConexion; }
            set { cConexion = value; }
        }

        public SqlConnection sqlConexion
        {
            get { return con; }
            set { con = value; }
        }

        public string cadenaPassword
        {
            get { return PASS; }
            set { PASS = value; }
        }

        public string cadenaUser
        {
            get { return USER; }
            set { USER = value; }
        }

        public string cadenaIP
        {
            get { return IP; }
            set { IP = value; }
        }

        public string cadenaBaseDatos
        {

            get { return BD; }
            set { BD = value; }
        }

        public Conexion() { }

        public Conexion(string InitialCatalog, string DataSource, string User, string Password)
        {
            cadenaBaseDatos = InitialCatalog;
            cadenaIP = DataSource;
            cadenaUser = User;
            cadenaPassword = Password;
            cadenaConexion = "Initial Catalog=" + cadenaBaseDatos + ";Data Source=" + cadenaIP + ";UID=" + cadenaUser + ";Pwd=" + cadenaPassword;
            Conectar();
        }

        public Conexion(string connection)
        {
            cadenaConexion = connection;
            Conectar();
        }

        private void Conectar()
        {
            con.ConnectionString = cadenaConexion;
        }

        private void ConexionAbrir()
        {
            if (con.State == ConnectionState.Closed)
                con.Open();
        }

        private void ConexionCerrar()
        {
            if (con.State == ConnectionState.Open)
                con.Close();
        }

        public DataTable LlenarDataTable(ref DataTable dt, string Sentencia)
        {
            try
            {
                ConexionAbrir();
                comm.CommandType = CommandType.Text;
                comm.CommandText = Sentencia;
                comm.CommandTimeout = 10000;
                da.SelectCommand = comm;
                da.SelectCommand.Connection = con;
                da.SelectCommand.CommandTimeout = 10000;
                da.Fill(dt);
                return dt;
            }
            catch (Exception ex) { throw ex; }
            finally { ConexionCerrar(); }
        }

        public void EjecutarSentencia(string Sentencia)
        {
            try
            {
                ConexionAbrir();
                comm.CommandType = CommandType.Text;
                comm.CommandText = Sentencia;
                comm.Connection = con;
                comm.CommandTimeout = 10000;
                comm.ExecuteNonQuery();
            }
            catch (Exception ex) { throw ex; }
            finally { ConexionCerrar(); }
        }

        public string MayusculaPrimera(string s)
        {
            if (string.IsNullOrEmpty(s))
                return string.Empty;
            return char.ToUpper(s[0]) + s.Substring(1);
        }


        //Es muy importante que la tabla destino exista antes de hacer el BulkCopy
        public void BulkCopy(DataTable dt, string Tabla)
        {
            try
            {
                if (dt.Rows.Count > 0)
                {
                    ConexionAbrir();
                    SqlTransaction externalTransaction = con.BeginTransaction();
                    SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(this.con, SqlBulkCopyOptions.Default, externalTransaction);
                    sqlBulkCopy.BatchSize = 20000;
                    sqlBulkCopy.DestinationTableName = "dbo." + Tabla;
                    sqlBulkCopy.WriteToServer(dt);
                    externalTransaction.Commit();
                }
            }
            catch (Exception ex) { throw ex; }
            finally { ConexionCerrar(); }
        }

        public long checaEmpleado()
        {
            Process objProceso = new Process();
            long lNumEmpleado = 0;
            string sArgumentos = string.Empty;

            if (File.Exists("C:\\SYS\\PROGS\\HE.Exe"))
            {
                objProceso.StartInfo = new ProcessStartInfo("C:\\SYS\\PROGS\\HE.Exe");
                objProceso.Start();
                objProceso.WaitForExit();
                lNumEmpleado = objProceso.ExitCode;
            }
            else
            {
                MessageBox.Show("ERROR");
            }
            return lNumEmpleado;
        }

        public string RegresaMes(string mes)
        {
            string str = "";
            switch (mes)
            {
                case "1":
                    str = "ENE";
                    break;
                case "2":
                    str = "FEB";
                    break;
                case "3":
                    str = "MAR";
                    break;
                case "4":
                    str = "ABR";
                    break;
                case "5":
                    str = "MAY";
                    break;
                case "6":
                    str = "JUN";
                    break;
                case "7":
                    str = "JUL";
                    break;
                case "8":
                    str = "AGO";
                    break;
                case "9":
                    str = "SEP";
                    break;
                case "10":
                    str = "OCT";
                    break;
                case "11":
                    str = "NOV";
                    break;
                case "12":
                    str = "DIC";
                    break;
            }
            return str;
        }

        public string RegresaMes(int mes)
        {
            string str = "";
            switch (mes)
            {
                case 1:
                    str = "ENE";
                    break;
                case 2:
                    str = "FEB";
                    break;
                case 3:
                    str = "MAR";
                    break;
                case 4:
                    str = "ABR";
                    break;
                case 5:
                    str = "MAY";
                    break;
                case 6:
                    str = "JUN";
                    break;
                case 7:
                    str = "JUL";
                    break;
                case 8:
                    str = "AGO";
                    break;
                case 9:
                    str = "SEP";
                    break;
                case 10:
                    str = "OCT";
                    break;
                case 11:
                    str = "NOV";
                    break;
                case 12:
                    str = "DIC";
                    break;
            }
            return str;
        }

        public void crearDirectorio(string directorio)
        {
            //recibe como parametro la ruta y el nombre del directorio a crear
            DirectoryInfo DIR = new DirectoryInfo(directorio.Trim());

            if (!DIR.Exists)
            {
                DIR.Create();
            }
        }

        public bool isInterger(string expresion)
        {
            Int64 var;
            return Int64.TryParse(expresion.Trim(), out var);
        }

        public string OpenDialogo()
        {
            //First, declare a variable to hold the user’s file selection.

            string resp = "-1";
            String input = string.Empty;

            //Create a new instance of the OpenFileDialog because it's an object.
            System.Windows.Forms.OpenFileDialog dialog = new System.Windows.Forms.OpenFileDialog();
            dialog.Filter = "Excel |*.xls";

            //Set the starting directory and the title.

            dialog.InitialDirectory = "C:";
            dialog.Title = "Selecciona un archivo";


            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                resp = dialog.FileName;

            if (resp == String.Empty)
                return "-1";  //usuario no selecciono archivo

            return resp;

        }

    }

}
