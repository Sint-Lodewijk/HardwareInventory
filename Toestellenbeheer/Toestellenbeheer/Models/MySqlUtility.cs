using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
namespace Toestellenbeheer.Models
{
    public class MySqlUtility
    {
        public MySqlUtility()
        {
        }
        public DataTable DtTable()
        {
            MySqlConnection mysqlConnectie = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            mysqlConnectie.Open();
            var TableCMD = new MySqlCommand("SHOW TABLES", mysqlConnectie);
            MySqlDataReader dr = TableCMD.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            mysqlConnectie.Close();
            return dt;
    }
}
}