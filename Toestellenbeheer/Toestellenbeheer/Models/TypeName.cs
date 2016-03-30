using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace Toestellenbeheer.Models
{
    public class TypeName
    {
        MySqlConnection mysqlConnectie = new MySqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);

        public TypeName()
        {

        }
        public TypeName(string strTypeName)
        {
            this.typeName = strTypeName;
        }

        public string typeName { get; set; }
        /// <summary>
        /// Returns the type in the database into a datatable.
        /// </summary>
        /// <returns>DataTable of type from database.</returns>
        public DataTable ReturnDatatableType()
        {
            mysqlConnectie.Open();
            MySqlCommand getType = new MySqlCommand("SELECT * FROM type", mysqlConnectie);
            var typeReader = getType.ExecuteReader();
            var dt = new DataTable();
            dt.Load(typeReader);
            return dt;
        }
        public void AddTypeToDatabase()
        {
            mysqlConnectie.Open();
            MySqlCommand addType = new MySqlCommand("Insert into type (type) values (@Type)", mysqlConnectie);

            addType.Parameters.AddWithValue("@Type", typeName);
            
            addType.ExecuteNonQuery();
            addType.Dispose();
            mysqlConnectie.Close();
        }
        public DataTable AssociatedDatatableHardware()
        {
            mysqlConnectie.Open();
            MySqlCommand getAssociatedHardwareFromType = new MySqlCommand("SELECT  manufacturerName, hardware.serialNr, hardware.internalNr, pictureLocation, modelNr, type  FROM hardware WHERE type = '" + typeName + "'", mysqlConnectie);
            var hardwareReader = getAssociatedHardwareFromType.ExecuteReader();
            var dt = new DataTable();
            dt.Load(hardwareReader);
            mysqlConnectie.Close();

            return dt;
        }
        public bool IsRemoved()
        {
            mysqlConnectie.Open();
            MySqlCommand removeType = new MySqlCommand("DELETE FROM type WHERE type = '" + typeName + "'", mysqlConnectie);
            removeType.ExecuteNonQuery();
            mysqlConnectie.Close();

            return true;
        }
        public bool IsUpdated(string strUpdateText)
        {
            mysqlConnectie.Open();
            MySqlCommand removeType = new MySqlCommand("UPDATE type SET type = '"+ strUpdateText +"' WHERE type = '" + typeName + "'", mysqlConnectie);
            removeType.ExecuteNonQuery();
            mysqlConnectie.Close();
            return true;
        }
    }
}