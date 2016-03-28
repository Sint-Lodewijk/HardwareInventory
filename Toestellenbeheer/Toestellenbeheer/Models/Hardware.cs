﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Xml;
using System.Data;
namespace Toestellenbeheer.Models
{

    public class Hardware
    {
        public Hardware()
        {

        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Hardware"/> class with 11 parameters.
        /// </summary>
        /// <param name="strAddedDate">The string added date.</param>
        /// <param name="strAttachmentLocation">The string attachment location.</param>
        /// <param name="strExtraInfo">The string extra information.</param>
        /// <param name="strInternalNr">The string internal nr.</param>
        /// <param name="strManufacturerName">Name of the string manufacturer.</param>
        /// <param name="strModelName">Name of the string model.</param>
        /// <param name="strPictureLocation">The string picture location.</param>
        /// <param name="strPurchaseDate">The string purchase date.</param>
        /// <param name="strSerialNr">The string serial nr.</param>
        /// <param name="strWarrantyInfo">The string warranty information.</param>
        /// <param name="strTypeName">Name of the string type.</param>
        public Hardware(string strAddedDate, string strAttachmentLocation, string strExtraInfo,
            string strInternalNr, string strManufacturerName, string strModelName, string strPictureLocation,
            string strPurchaseDate, string strSerialNr, string strWarrantyInfo, string strTypeName)
        {
            AddedDate = strAddedDate;
            AttachmentLocation = strAttachmentLocation;
            ExtraInfo = strExtraInfo;
            InternalNr = strInternalNr;
            ManufacturerName = strManufacturerName;
            ModelName = strModelName;
            PictureLocation = strPictureLocation;
            PurchaseDate = strPurchaseDate;
            SerialNr = strSerialNr;
            WarrantyInfo = strWarrantyInfo;
            TypeName = strTypeName;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Hardware"/> class with 1 parameter.
        /// </summary>
        /// <param name="strInternalNr">The string internal nr.</param>
        public Hardware(string strInternalNr)
        {
            InternalNr = strInternalNr;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Hardware"/> class with 2 parameters.
        /// </summary>
        /// <param name="intEventID">The int user event ID.</param>
        /// <param name="strInternalNr">The string internal nr.</param>
        public Hardware(int intEventID, string strInternalNr)
        {
            this.EventID = intEventID;
            this.InternalNr = strInternalNr;
        }
        public string AddedDate { get; set; }
        public string AttachmentLocation { get; set; }
        public string ExtraInfo { get; set; }
        public int EventID { get; set; }
        public string InternalNr { get; set; }
        public string ManufacturerName { get; set; }
        public string ModelName { get; set; }
        public string PictureLocation { get; set; }
        public string PurchaseDate { get; set; }
        public string SerialNr { get; set; }
        public string WarrantyInfo { get; set; }
        public string TypeName { get; set; }
        /// <summary>
        /// Adds the hardware into database.
        /// </summary>
        public void AddHardwareIntoDatabase()
        {
            MySqlConnection mysqlConnectie = new MySqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);

            MySqlCommand cmdAddHardware = new MySqlCommand("Insert into hardware (purchaseDate, serialNr, internalNr,  warranty, extraInfo, manufacturerName, addedDate, pictureLocation, type, attachmentLocation, modelNr) values (@purchaseDate, @serialNr, @internalNr,  @warranty, @extraInfo, @manufacturerName, @addedDate, @pictureLocation, @type, @attachmentLocation, @modelNr)", mysqlConnectie);
            mysqlConnectie.Open();

            //add parameters (assaign the values to the column.)
            cmdAddHardware.Parameters.AddWithValue("@purchaseDate", PurchaseDate);
            cmdAddHardware.Parameters.AddWithValue("@serialNr", SerialNr);
            cmdAddHardware.Parameters.AddWithValue("@internalNr", InternalNr);
            cmdAddHardware.Parameters.AddWithValue("@warranty", WarrantyInfo);
            cmdAddHardware.Parameters.AddWithValue("@extraInfo", ExtraInfo);
            cmdAddHardware.Parameters.AddWithValue("@manufacturerName", ManufacturerName);
            cmdAddHardware.Parameters.AddWithValue("@addedDate", AddedDate);
            cmdAddHardware.Parameters.AddWithValue("@pictureLocation", PictureLocation);
            cmdAddHardware.Parameters.AddWithValue("@type", TypeName);
            cmdAddHardware.Parameters.AddWithValue("@attachmentLocation", AttachmentLocation);
            cmdAddHardware.Parameters.AddWithValue("@modelNr", ModelName);
            cmdAddHardware.ExecuteNonQuery();
            cmdAddHardware.Dispose();
            mysqlConnectie.Close();

        }
        /// <summary>
        /// Returns the datatable hardware from internalnumber.
        /// </summary>
        /// <returns>DataTable.</returns>
        public DataTable ReturnDatatableHardwareFromInternal()
        {
            MySqlConnection mysqlConnectie = new MySqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            mysqlConnectie.Open();
            MySqlCommand getCorrespondingPeople = new MySqlCommand("SELECT pictureLocation, DATE_FORMAT(purchaseDate, '%Y-%m-%d') 'Purchase date', type , manufacturerName 'Manufacturer', serialNr 'Serial Nr', internalNr 'Internal Nr', warranty 'Warranty', extraInfo 'Extra info', DATE_FORMAT(addedDate, '%Y-%m-%d') 'Added date', attachmentLocation FROM hardware WHERE internalNr = '" + InternalNr + "'", mysqlConnectie);
            var dataReader = getCorrespondingPeople.ExecuteReader();
            var dt = new DataTable();
            dt.Load(dataReader);
            mysqlConnectie.Close();

            return dt;
        }
        public void ArchiveAssignedHardware(String strSerialNr, String strInternalNr, int intEventID)
        {
            MySqlConnection mysqlConnectie = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);

            String dteAssignedDate = DateTime.Today.ToString("yyyy-MM-dd");
            mysqlConnectie.Open();
            MySqlCommand archiveAssigned = new MySqlCommand("Insert into archive ( assignedDate, serialNr, internalNr, eventID ) values (@assignedDate, @serialNr, @internalNr, @eventID)", mysqlConnectie);
            archiveAssigned.Parameters.AddWithValue("@assignedDate", dteAssignedDate);
            archiveAssigned.Parameters.AddWithValue("@serialNr", strSerialNr);
            archiveAssigned.Parameters.AddWithValue("@internalNr", strInternalNr);
            archiveAssigned.Parameters.AddWithValue("@eventID", intEventID);

            archiveAssigned.ExecuteNonQuery();
            archiveAssigned.Dispose();
            mysqlConnectie.Close();
        }
        public void CreateXML(String statusNode, String serialNr, String internalNr, String manufacturer, String nameAD, String userName, String modelNr)
        {
            XmlDocument doc = new XmlDocument();
            XmlNode docNode = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            doc.AppendChild(docNode);

            XmlNode AssignedNodes = doc.CreateElement(statusNode);
            doc.AppendChild(AssignedNodes);

            XmlNode xmlHardwareNode = doc.CreateElement("Hardware");
            AssignedNodes.AppendChild(xmlHardwareNode);

            XmlNode xmlPersonNode = doc.CreateElement("person");
            AssignedNodes.AppendChild(xmlPersonNode);

            XmlNode xmlInternalNrNode = doc.CreateElement("InternalNr");
            xmlInternalNrNode.AppendChild(doc.CreateTextNode(internalNr));
            xmlHardwareNode.AppendChild(xmlInternalNrNode);

            XmlNode xmlSerialNr = doc.CreateElement("SerialNr");
            xmlSerialNr.AppendChild(doc.CreateTextNode(serialNr));
            xmlHardwareNode.AppendChild(xmlSerialNr);

            XmlNode xmlManufacturer = doc.CreateElement("Manufacturer");
            xmlManufacturer.AppendChild(doc.CreateTextNode(manufacturer));
            xmlHardwareNode.AppendChild(xmlManufacturer);


            XmlNode xmlModelNr = doc.CreateElement("ModelNr");
            xmlModelNr.AppendChild(doc.CreateTextNode(modelNr));
            xmlHardwareNode.AppendChild(xmlModelNr);

            XmlNode xmlUserId = doc.CreateElement("UserID");
            xmlUserId.AppendChild(doc.CreateTextNode(nameAD));
            xmlPersonNode.AppendChild(xmlUserId);

            XmlNode xmlUserName = doc.CreateElement("UserName");
            xmlUserName.AppendChild(doc.CreateTextNode(userName));
            xmlPersonNode.AppendChild(xmlUserName);

            doc.Save("./UserUploads/Attachments/GeneratedAssignedHardware.xml");
        }
        /// <summary>
        /// Binds the event ID with hardware.
        /// </summary>
        public void BindEventID()
        {
            MySqlConnection mysqlConnectie = new MySqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);

            mysqlConnectie.Open();

            MySqlCommand bindEventIDWithHardware = new MySqlCommand("UPDATE hardware SET eventID = '" + EventID + "' WHERE internalNr LIKE '" +
                InternalNr + "'", mysqlConnectie);
            bindEventIDWithHardware.ExecuteNonQuery();
            bindEventIDWithHardware.Dispose();
            mysqlConnectie.Close();
        }
    }
    //WebRequest request = WebRequest.Create(SetupFile.GlobalVar.ScripturaPath);
}