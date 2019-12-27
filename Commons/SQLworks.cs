using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commons
{
    public class SQLworks
    {
        public string connString { get; set; }
        private SqlConnection connection { get; set; }
        private SqlCommand command { get; set; }

        public SQLworks(string conn)
        {
            connString = conn;
        }

        public int CrearGrupo(string id_planilla, string configuracion)
        {
            try
            {
                int res = 0;
                SetConn();
                command.CommandText = "usp_CrearEnvioEncriptado";
                command.Parameters.Clear();
                command.Parameters.Add("@id_planilla", SqlDbType.NVarChar).Value = id_planilla.Length > 0 ? id_planilla.Substring(0, id_planilla.Length > 4 ? 4 : id_planilla.Length) : null;
                command.Parameters.Add("@configuracion", SqlDbType.NVarChar).Value = configuracion.Length > 0 ? configuracion.Substring(0, configuracion.Length > 30 ? 30 : configuracion.Length) : null;

                connection.Open();
                res = int.Parse(command.ExecuteScalar().ToString());
                return res;
            }
            catch (Exception cg_ex)
            {
                Console.WriteLine(cg_ex.ToString());
                FileLogger.WriteToFile($"Para {id_planilla} con configuración {configuracion} hubo un error: {cg_ex.Message}", FileLogger.LogTipos.ERROR, false, FileLogger.LogLugares.Log4Net);
                return -1;
                throw cg_ex;
            }
        }

        public bool GrabarPasswordPdf(string filename, string password, int idGrupo, string empleado)
        {
            try
            {
                bool res = false;
                SetConn();
                command.CommandText = "usp_GrabarPasswordPdf";
                command.Parameters.Clear();
                command.Parameters.Add("@id_grupo", SqlDbType.Int).Value = idGrupo;
                command.Parameters.Add("@ruta", SqlDbType.NVarChar).Value = filename.Length > 0 ? filename.Substring(0, filename.Length > 500 ? 500 : filename.Length) : null;
                command.Parameters.Add("@clave", SqlDbType.NVarChar).Value = password.Length > 0 ? password.Substring(0, password.Length > 20 ? 20 : password.Length) : null;
                command.Parameters.Add("@num_emp", SqlDbType.NVarChar).Value = empleado.Length > 0 ? empleado.Substring(0, empleado.Length > 10 ? 10 : empleado.Length) : null;

                connection.Open();
                res = int.Parse(command.ExecuteScalar().ToString()) > 0;
                return res;
            }
            catch (Exception cg_ex)
            {
                Console.WriteLine(cg_ex.ToString());
                FileLogger.WriteToFile($"Para el archivo {filename} del colaborador {empleado} hubo un error: {cg_ex.Message}", FileLogger.LogTipos.ERROR, false, FileLogger.LogLugares.Log4Net);
                return false;
                throw cg_ex;
            }
        }

        private void SetConn()
        {
            connection = new SqlConnection(connString);
            command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
        }
    }
}
