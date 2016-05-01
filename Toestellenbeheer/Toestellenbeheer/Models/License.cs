using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Data;
namespace Toestellenbeheer.Models
{
    public class License
    {
        MySqlConnection mysqlConnectie = new MySqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
        public License()
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="License"/> class.
        /// </summary>
        /// <param name="strLicenseCode">The string license code.</param>
        public License(string strLicenseCode)
        {
            LicenseCode = strLicenseCode;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="License"/> class with 5 parameters.
        /// </summary>
        /// <param name="strLicenseCode">The string license code.</param>
        /// <param name="strLicenseName">Name of the string license.</param>
        /// <param name="strExpireDate">The string expire date.</param>
        /// <param name="strLicenseFile">The string license file.</param>
        /// <param name="strExtraInfo">The string extra information.</param>
        public License(string strLicenseCode, string strLicenseName, string strExpireDate, string strLicenseFile, string strExtraInfo)
        {
            LicenseCode = strLicenseCode;
            LicenseName = strLicenseName;
            ExpireDate = strExpireDate;
            ExtraInfo = strExtraInfo;
            LicenseFile = strLicenseFile;
        }
        public string LicenseCode { get; set; }
        public string LicenseName { get; set; }
        public string ExpireDate { get; set; }
        public string LicenseFile { get; set; }
        public string ExtraInfo { get; set; }
        public int LicenseID { get; set; }
        public string LicenseType { get; set; }
        /// <summary>
        /// Adds the license in to the database;
        /// </summary>
        /// <exception cref="Exception"></exception>
        public void AddLicense()
        {
            try
            {
                mysqlConnectie.Open();
                MySqlCommand addLicense = new MySqlCommand("INSERT INTO license (licenseName, licenseCode, expireDate, extraInfo, licenseFileLocation) values (@licenseName, @licenseCode, @expireDate, @extraInfo, @licenseFileLocation)", mysqlConnectie);
                addLicense.Parameters.AddWithValue("@licenseName", LicenseName);
                addLicense.Parameters.AddWithValue("@licenseCode", LicenseCode);
                addLicense.Parameters.AddWithValue("@expireDate", ExpireDate);
                addLicense.Parameters.AddWithValue("@extraInfo", ExtraInfo);
                addLicense.Parameters.AddWithValue("@licenseFileLocation", LicenseFile);
                addLicense.ExecuteNonQuery();
                addLicense.Dispose();
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                mysqlConnectie.Close();
            }
        }
        public int ReturnMaxLicenseID()
        {
            MySqlConnection mysqlConnectie = new MySqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            mysqlConnectie.Open();
            MySqlCommand getMaxIndexLicenseID = new MySqlCommand("SELECT MAX(licenseID) FROM license", mysqlConnectie);
            int intLicenseID = (int)getMaxIndexLicenseID.ExecuteScalar();
            mysqlConnectie.Close();
            return intLicenseID;
        }
        public void AssignLicenseToHardware(string strSerialNr, string strInternalNr)
        {
        }
        public DataTable ReturnLicenseCHardware()
        {
            DataTable dt = new DataTable();
            return dt;
        }
        public bool IsRemoved()
        {
            try
            {
                mysqlConnectie.Open();
                var RemoveLicense = new MySqlCommand("DELETE FROM license WHERE licenseID= " + LicenseID, mysqlConnectie);
                mysqlConnectie.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                var exep = new MySqlExceptionHandler(ex, "License");
                throw new Exception(exep.ReturnMessage());
            }
        }
        /// <summary>
        /// Gets the license identifier of the corresponding license code or file.
        /// </summary>
        /// <returns>System.Int32.</returns>
        public int GetLicenseID(string Type)
        {
            mysqlConnectie.Open();
            var getLicenseID = new MySqlCommand("SELECT licenseID FROM license WHERE " + Type + "  = '" + LicenseCode + "'", mysqlConnectie);
            mysqlConnectie.Close();
            LicenseID = (int)getLicenseID.ExecuteScalar();
            return LicenseID;
        }
        public DataTable ReturnLicensePeople(string license, bool IsFile)
        {
            if (IsFile == true)
            {
                return DTLicensePeople(license, "licenseFile");
            }
            else
            {
                return DTLicensePeople(license, "licenseCode");
            }
        }
        private DataTable DTLicensePeople(string license, string licenseType)
        {
            mysqlConnectie.Open();
            MySqlCommand getCorrespondingPeople = new MySqlCommand("SELECT licenseEventID, licenseHandler.licenseID, nameAD from licenseHandler join people on licenseHandler.eventID = people.eventID WHERE " + licenseType + " = '" + license + "'", mysqlConnectie);
            var peopleReader = getCorrespondingPeople.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(peopleReader);
            mysqlConnectie.Close();
            return dt;
        }
        public DataTable ReturnLicenseHardware(string license, bool IsFile)
        {
            if (IsFile == true)
            {
                return DTLicenseHardware(license, "licenseFile");
            }
            else
            {
                return DTLicenseHardware(license, "licenseCode");
            }
        }
        private DataTable DTLicenseHardware(string license, string licenseType)
        {
            mysqlConnectie.Open();
            MySqlCommand getCorrespondingHardware = new MySqlCommand("SELECT licenseEventID, licenseHandler.licenseID, nameAD from licenseHandler join people on licenseHandler.eventID = people.eventID WHERE " + licenseType + " = '" + license + "'", mysqlConnectie);
            var HardwareReader = getCorrespondingHardware.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(HardwareReader);
            mysqlConnectie.Close();
            return dt;
        }
    }
}