using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using Toestellenbeheer.Models;
namespace Toestellenbeheer.Models
{
    public class LicenseHandler : Hardware
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LicenseHandler"/> class.
        /// </summary>
        public LicenseHandler()
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="LicenseHandler"/> class with 2 parameters.
        /// </summary>
        /// <param name="intEventID">The int event identifier.</param>
        /// <param name="intLicenseID">The int license identifier.</param>
        public LicenseHandler(int intEventID, int intLicenseID)
        {
            UserID = intEventID;
            LicenseID = intLicenseID;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="LicenseHandler"/> class with 3 parameters.
        /// </summary>
        /// <remarks
        /// <param name="strInternalNr">The string internal nr.</param>
        /// <param name="strSerialNr">The string serial nr.</param>
        /// <param name="intLicenseID">The int license identifier, it refers to the corresponding license.</param>
        public LicenseHandler(string strInternalNr, string strSerialNr, int intLicenseID)
        {
            LicenseID = intLicenseID;
            InternalNr = strInternalNr;
            SerialNr = strSerialNr;
        }
        public int LicenseID { get; set; }
        public int UserID { get; set; }
        public void AssignLicenseToHardware()
        {
            MySqlConnection mysqlConnectie = new MySqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            mysqlConnectie.Open();
            MySqlCommand addLicenseCommand = new MySqlCommand("INSERT INTO licenseHandler (internalNr, serialNr, licenseID) values (@internalNr, @serialNr, @licenseID)", mysqlConnectie);
            addLicenseCommand.Parameters.AddWithValue("@internalNr", InternalNr);
            addLicenseCommand.Parameters.AddWithValue("@serialNr", SerialNr);
            addLicenseCommand.Parameters.AddWithValue("@licenseID", LicenseID);
            addLicenseCommand.ExecuteNonQuery();
            addLicenseCommand.Dispose();
            mysqlConnectie.Close();
        }
        public void AssignLicenseToPeople()
        {
            MySqlConnection mysqlConnectie = new MySqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            mysqlConnectie.Open();
            MySqlCommand assignLicenseToPeople = new MySqlCommand("INSERT INTO licenseHandler (eventID, licenseID) values (@eventID, @licenseID)", mysqlConnectie);
            assignLicenseToPeople.Parameters.AddWithValue("@licenseID", LicenseID);
            assignLicenseToPeople.Parameters.AddWithValue("@eventID", UserID);
            assignLicenseToPeople.ExecuteNonQuery();
            assignLicenseToPeople.Dispose();
            mysqlConnectie.Close();
        }
    }
}