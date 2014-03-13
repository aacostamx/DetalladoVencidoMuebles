using System;
using System.Data;
using System.Data.Odbc;
using System.IO;
using System.Windows.Forms;


namespace AF0122_DetalladoVencidoMuebles
{
    public class ConexionPG
    {
        public string[] sDatosConexion_array = new string[4];
        private OdbcConnection con = new OdbcConnection();
        private OdbcCommand comm = new OdbcCommand();
        private OdbcDataAdapter da = new OdbcDataAdapter();
        public string CadenaConexion;

        public ConexionPG()
        {
            //Ingresa el nombre del archivo de texto [nombretxt]
            this.AbrirConexion("C:\\sys\\exe\\conexion\\nombretxt.txt");
            this.CadenaConexion = "Driver={PostgreSql};Server=" + this.sDatosConexion_array[0] + ";Port=5432;Database=" + this.sDatosConexion_array[1] + ";Uid=" + this.sDatosConexion_array[2] + ";Pwd=" + this.sDatosConexion_array[3] + ";SSL=false";
            this.conectar();
        }

        public string[] AbrirConexion(string sRutaArchivo)
        {
            try
            {
                using (StreamReader streamReader = new StreamReader(sRutaArchivo))
                {
                    int num = 0;
                    string str;
                    while ((str = streamReader.ReadLine()) != null)
                    {
                        if (num < 8)
                        {
                            if (num > 3)
                                this.sDatosConexion_array[num - 4] = str;
                            ++num;
                        }
                        else
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                int num = (int)MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            return this.sDatosConexion_array;
        }

        public void conectar()
        {
            this.con.ConnectionString = this.CadenaConexion;
            this.con.ConnectionTimeout = 0;
        }

        public void ConexionAbrir()
        {
            if (this.con.State != ConnectionState.Closed)
                return;
            this.con.Open();
        }

        public void ConexionCerrar()
        {
            if (this.con.State != ConnectionState.Open)
                return;
            this.con.Close();
        }

        public bool Execute(ref DataTable dtDatos, string Sentencia)
        {
            try
            {
                this.ConexionAbrir();
                this.comm.CommandType = CommandType.Text;
                this.comm.CommandText = Sentencia;
                this.comm.CommandTimeout = 0;
                this.da.SelectCommand = this.comm;
                this.da.SelectCommand.Connection = this.con;
                this.da.SelectCommand.CommandTimeout = 0;
                this.da.Fill(dtDatos);
                return true;
            }
            catch (OdbcException ex)
            {
                int num = (int)MessageBox.Show(((object)ex.Message).ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return false;
            }
        }

        public bool Execute(string sentencia)
        {
            try
            {
                this.ConexionAbrir();
                this.comm.Connection = this.con;
                this.comm.CommandType = CommandType.Text;
                this.comm.CommandText = sentencia;
                this.comm.CommandTimeout = 10000;
                this.comm.ExecuteNonQuery();
                return true;
            }
            catch (OdbcException ex)
            {
                int num = (int)MessageBox.Show(((object)ex.Message).ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return false;
            }
        }
    }

}
