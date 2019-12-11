using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace Commons
{
    /// <summary>
    /// Solo da el "esqueleto", porque Dapper usa especificamente las clases, y no podemos tener todas las clases de todos los programas aqui
    /// </summary>
    class Dapper_DB
    {
        private SqlConnection dbCon { get; set; }

        public Dapper_DB(string connString)
        {
            dbCon = new SqlConnection(connString);
        }

        public void dbConOpen()
        {
            dbCon.Open();
        }

        public void dbConClose()
        {
            dbCon.Close();
        }

        //para ejecutar un SELECT
        //public List<Employee> GetCsv(bool cerrarCon = false)
        //{
        //    List<Employee> employees = new List<Employee>();
        //    try
        //    {
        //        if (dbCon.State != System.Data.ConnectionState.Open)
        //            dbCon.Open();
        //        string sql = "SELECT ... FROM {table} WHERE ...";
        //        using (dbCon)
        //        {
        //            employees = dbCon.Query<Employee>(sql).ToList();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        FileLogger.WriteToFile($"Error en la DB : {ex.Message}", FileLogger.LogTipos.ERROR, FileLogger.LogLugares.Log4Net, false);
        //        if (cerrarCon)
        //        {
        //            dbCon.Close();
        //        }
        //        return employees;
        //    }
        //    finally
        //    {
        //        if (dbCon.State == System.Data.ConnectionState.Open && cerrarCon)
        //            dbCon.Close();
        //    }
        //    return employees;
        //}
    }
}