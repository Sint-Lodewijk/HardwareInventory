using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Configuration;
namespace Toestellenbeheer.Models
{
    public class Manufacturer
    {
        MySqlConnection mysqlConnectie = new MySqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
        public Manufacturer()
        {
        }
        public Manufacturer(string strManufacturer)
        {
            this.ManufacturerName = strManufacturer;
        }
        public string ManufacturerName { get; set; }
        /// <summary>
        /// Returns the datatable manufacturer.
        /// </summary>
        /// <returns>DataTable.</returns>
        public DataTable ReturnDatatableManufacturer()
        {
            mysqlConnectie.Open();
            MySqlCommand getManufacturer = new MySqlCommand("SELECT * FROM Manufacturer", mysqlConnectie);
            var manufacturerReader = getManufacturer.ExecuteReader();
            var dt = new DataTable();
            dt.Load(manufacturerReader);
            return dt;
        }
        public void AddManufacturerToDatabase()
        {
            mysqlConnectie.Open();
            MySqlCommand addManufacturer = new MySqlCommand("Insert into Manufacturer (manufacturerName) values (@ManufacturerName)", mysqlConnectie);
            addManufacturer.Parameters.AddWithValue("@ManufacturerName", ManufacturerName);
            addManufacturer.ExecuteNonQuery();
            addManufacturer.Dispose();
            mysqlConnectie.Close();
        }
        public DataTable AssociatedDatatableHardware()
        {
            mysqlConnectie.Open();
            MySqlCommand getAssociatedHardwareFromType = new MySqlCommand("SELECT  hardware.serialNr, hardware.internalNr, pictureLocation, modelNr, type  FROM hardware WHERE manufacturerName = '" +
                ManufacturerName + "'", mysqlConnectie);
            var hardwareReader = getAssociatedHardwareFromType.ExecuteReader();
            var dt = new DataTable();
            dt.Load(hardwareReader);
            mysqlConnectie.Close();
            return dt;
        }
        public bool IsRemoved()
        {
            mysqlConnectie.Open();
            MySqlCommand removeManufacturer = new MySqlCommand("DELETE FROM Manufacturer WHERE manufacturerName = '" +
                ManufacturerName + "'", mysqlConnectie);
            removeManufacturer.ExecuteNonQuery();
            mysqlConnectie.Close();
            return true;
        }
        public bool IsUpdated(string strManufacturerValue)
        {
            mysqlConnectie.Open();
            MySqlCommand updateManufacturer = new MySqlCommand("UPDATE Manufacturer SET manufacturerName = '" +
                strManufacturerValue + "' WHERE manufacturerName = '" + ManufacturerName + "'", mysqlConnectie);
            updateManufacturer.ExecuteNonQuery();
            mysqlConnectie.Close();
            return true;
        }
        /// <summary>
        /// Counts the total manufacturers in database.
        /// </summary>
        /// <returns>System.Int32. total manufacturers in database.</returns>
        public int CountManufacturer()
        {
            mysqlConnectie.Open();
            var TotalManufacturer = new MySqlCommand("SELECT COUNT(*) FROM Manufacturer", mysqlConnectie);
            int intTotal = Convert.ToInt16(TotalManufacturer.ExecuteScalar());
            mysqlConnectie.Close();
            return intTotal;
        }
    }
}
