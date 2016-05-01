using MySql.Data.MySqlClient;
using System;
using System.Configuration;
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
        /// Initializes a new instance of the <see cref="Hardware"/> class with 7 parameters. 
        /// </summary>
        /// <remarks>
        /// Main purpose: Modify hardware</remarks>
        /// <param name="strExtraInfo">The string extra information.</param>
        /// <param name="strInternalNr">The string internal nr.</param>
        /// <param name="strManufacturerName">Name of the string manufacturer.</param>
        /// <param name="strModelName">Name of the string model.</param>
        /// <param name="strPurchaseDate">The string purchase date.</param>
        /// <param name="strWarrantyInfo">The string warranty information.</param>
        /// <param name="strTypeName">Name of the string type.</param>
        public Hardware(string strExtraInfo, string strInternalNr, string strManufacturerName,
            string strModelName,
            string strPurchaseDate, string strWarrantyInfo, string strTypeName)
        {
            ExtraInfo = strExtraInfo;
            InternalNr = strInternalNr;
            ManufacturerName = strManufacturerName;
            ModelName = strModelName;
            PurchaseDate = strPurchaseDate;
            WarrantyInfo = strWarrantyInfo;
            TypeName = strTypeName;
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
        /// <param name="strInternalNr">The string internal nr.</param>
        /// <param name="strSerialNr">The string serial nr.</param>
        public Hardware(string strInternalNr, string strSerialNr)
        {
            InternalNr = strInternalNr;
            SerialNr = strSerialNr;
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
        /// Returns the datatable hardware from the internal number.
        /// </summary>
        /// <returns>DataTable internal nr corresponsing hardware.</returns>
        public DataTable ReturnDatatableHardwareFromInternal()
        {
            try
            {
                MySqlConnection mysqlConnectie = new MySqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                mysqlConnectie.Open();
                MySqlCommand getCorrespondingPeople = new MySqlCommand("SELECT pictureLocation, DATE_FORMAT(purchaseDate, '%Y-%m-%d') 'purchaseDate', type , manufacturerName, serialNr , internalNr, warranty, extraInfo , DATE_FORMAT(addedDate, '%Y-%m-%d') 'addedDate', attachmentLocation, modelNr FROM hardware WHERE internalNr = '" + InternalNr + "'", mysqlConnectie);
                var dataReader = getCorrespondingPeople.ExecuteReader();
                var dt = new DataTable();
                dt.Load(dataReader);
                mysqlConnectie.Close();
                return dt;
            }
            catch (MySqlException ex)
            {
                var handler = new MySqlExceptionHandler(ex, "Hardware");
                throw new Exception(handler.ExceptionType);
            }
        }
        /// <summary>
        /// Returns the datatable of all hardware in the database.
        /// </summary>
        /// <returns>DataTable.</returns>
        public DataTable ReturnDatatableAllHardware()
        {
            MySqlConnection mysqlConnectie = new MySqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            mysqlConnectie.Open();
            MySqlCommand getHardware = new MySqlCommand("SELECT pictureLocation, DATE_FORMAT(purchaseDate, '%Y-%m-%d') 'purchaseDate', type , manufacturerName, serialNr, internalNr, warranty, extraInfo, DATE_FORMAT(addedDate, '%Y-%m-%d') 'addedDate', attachmentLocation, eventID, modelNr FROM hardware ", mysqlConnectie);
            var dataReader = getHardware.ExecuteReader();
            var dt = new DataTable();
            dt.Load(dataReader);
            mysqlConnectie.Close();
            return dt;
        }
        /// <summary>
        /// Archives the assigned hardware.
        /// </summary>
        /// <param name="strSerialNr">The string serial nr.</param>
        /// <param name="strInternalNr">The string internal nr.</param>
        /// <param name="intEventID">The int event identifier.</param>
        public void ArchiveAssignedHardware(String strSerialNr, String strInternalNr, int intEventID)
        {
            try
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
            catch (MySqlException ex)
            {
                var handler = new MySqlExceptionHandler(ex, "Hardware");
                throw new Exception(handler.ExceptionType);
            }
        }
        /// <summary>
        /// Return the picture location.
        /// </summary>
        /// <returns>String.</returns>
        public String PicLocation()
        {
            MySqlConnection mysqlConnectie = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            mysqlConnectie.Open();
            MySqlCommand getMyHardware = new MySqlCommand("SELECT hardware.pictureLocation FROM hardware WHERE hardware.internalNr = '" + InternalNr + "'", mysqlConnectie);
            object checkObj = new object();
            checkObj = getMyHardware.ExecuteScalar();
            if (checkObj == null)
            {
                mysqlConnectie.Close();
                return "";
            }
            else
            {
                string strPicLoc = getMyHardware.ExecuteScalar().ToString();
                mysqlConnectie.Close();
                return strPicLoc;
            }
        }
        /// <summary>
        /// Returns the current user hardware.
        /// </summary>
        /// <param name="UserName">Name of the user.</param>
        /// <returns>DataTable. Hardware</returns>
        public DataTable ReturnUserHardware(string UserName)
        {
            MySqlConnection mysqlConnectie = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            mysqlConnectie.Open();
            MySqlCommand getMyHardware = new MySqlCommand("SELECT hardware.pictureLocation, hardware.serialNr, hardware.internalNr, hardware.manufacturerName, type FROM hardware JOIN archive ON hardware.internalNr = archive.internalNr  JOIN people ON people.eventID = archive.eventID  WHERE people.nameAD = '" + UserName + "'", mysqlConnectie);
            getMyHardware.Parameters.AddWithValue("@nameAD", UserName);
            MySqlDataReader rdrGetMyHardware = getMyHardware.ExecuteReader();
            DataTable dt = new DataTable();
            // MySqlDataAdapter adpa = new MySqlDataAdapter(rdrGetMyHardware);
            dt.Load(rdrGetMyHardware);
            return dt;
        }
        /// <summary>
        /// Returns the user full hardware.
        /// </summary>
        /// <param name="UserName">Name of the user.</param>
        /// <returns>DataTable.</returns>
        public DataTable ReturnUserFullHardware(string UserName)
        {
            MySqlConnection mysqlConnectie = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            mysqlConnectie.Open();
            MySqlCommand getMyHardware = new MySqlCommand("SELECT serialNr, internalNr, DATE_FORMAT(assignedDate, '%Y-%m-%d') 'assignedDate',DATE_FORMAT(returnedDate, '%Y-%m-%d') 'returnedDate' FROM archive JOIN people on archive.eventID = people.eventID WHERE nameAD = '" + UserName + "'", mysqlConnectie);
            getMyHardware.Parameters.AddWithValue("@nameAD", UserName);
            MySqlDataReader rdrGetMyHardware = getMyHardware.ExecuteReader();
            DataTable dt = new DataTable();
            // MySqlDataAdapter adpa = new MySqlDataAdapter(rdrGetMyHardware);
            dt.Load(rdrGetMyHardware);
            return dt;
        }
        /// <summary>
        /// Creates the XML file.
        /// </summary>
        /// <param name="statusNode">The status node.</param>
        /// <param name="serialNr">The serial nr.</param>
        /// <param name="internalNr">The internal nr.</param>
        /// <param name="manufacturer">The manufacturer.</param>
        /// <param name="nameAD">The name ad.</param>
        /// <param name="userName">Name of the user.</param>
        /// <param name="modelNr">The model nr.</param>
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
            try
            {
                MySqlConnection mysqlConnectie = new MySqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                mysqlConnectie.Open();
                MySqlCommand bindEventIDWithHardware = new MySqlCommand("UPDATE hardware SET eventID = '" + EventID + "' WHERE internalNr LIKE '" +
                    InternalNr + "'", mysqlConnectie);
                bindEventIDWithHardware.ExecuteNonQuery();
                bindEventIDWithHardware.Dispose();
                mysqlConnectie.Close();
            }
            catch (MySqlException ex)
            {
                var handler = new MySqlExceptionHandler(ex, "Hardware");
                throw new Exception(handler.ExceptionType);
            }
        }
        /// <summary>
        /// Returns the datatable of searched hardware .
        /// </summary>
        /// <param name="searchType">Type of the search.</param>
        /// <param name="searchValue">The search value.</param>
        /// <returns>System.Data.DataTable. searched hardware</returns>
        public DataTable ReturnSearchDatatable(string searchType, string searchValue)
        {
            try
            {
                MySqlConnection mysqlConnectie = new MySqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                mysqlConnectie.Open();
                MySqlCommand searchItem = new MySqlCommand("SELECT pictureLocation, DATE_FORMAT(purchaseDate, '%Y-%m-%d') 'purchaseDate', type, manufacturerName , serialNr, internalNr , warranty , extraInfo , DATE_FORMAT(addedDate, '%Y-%m-%d') 'addedDate', attachmentLocation, modelNr FROM hardware WHERE " + searchType + " COLLATE UTF8_GENERAL_CI LIKE '%" + searchValue + "%';", mysqlConnectie);
                var searchReader = searchItem.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(searchReader);
                mysqlConnectie.Close();
                return dt;
            }
            catch (MySqlException ex)
            {
                var handler = new MySqlExceptionHandler(ex, "Hardware");
                throw new Exception(handler.ExceptionType);
            }
        }
        /// <summary>
        /// Returns the datatable of unassigned hardware.
        /// </summary>
        /// <returns>DataTable.</returns>
        public DataTable ReturnUnassignedHardware()
        {
            MySqlConnection mysqlConnectie = new MySqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            mysqlConnectie.Open();
            MySqlCommand ReturnUnassignedHardware = new MySqlCommand("SELECT DATE_FORMAT(purchaseDate, '%Y-%m-%d') 'purchaseDate', type, manufacturerName, serialNr, internalNr, pictureLocation, modelNr FROM hardware WHERE eventID IS NULL or eventID=''", mysqlConnectie);
            var unassignsReader = ReturnUnassignedHardware.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(unassignsReader);
            mysqlConnectie.Close();
            return dt;
        }
        /// <summary>
        /// Updates the hardware information in the database.
        /// </summary>
        public void UpdateHardware()
        {
            MySqlConnection mysqlConnectie = new MySqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            mysqlConnectie.Open();
            var update = new MySqlCommand("UPDATE hardware SET purchaseDate = @purchaseDate, type = @type, manufacturerName = @manufacturer, warranty = @warranty, extraInfo = @extraInfo", mysqlConnectie);
            update.Parameters.AddWithValue("@purchaseDate", PurchaseDate);
            update.Parameters.AddWithValue("@type", TypeName);
            update.Parameters.AddWithValue("@manufacturer", ManufacturerName);
            update.Parameters.AddWithValue("@warranty", WarrantyInfo);
            update.Parameters.AddWithValue("@extraInfo", ExtraInfo);
            update.ExecuteNonQuery();
            mysqlConnectie.Close();
        }
        /// <summary>
        /// Return the datatable of assigned hardware.
        /// </summary>
        /// <returns>DataTable assigned hardware.</returns>
        public DataTable ReturnAssignedHardware()
        {
            MySqlConnection mysqlConnectie = new MySqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            mysqlConnectie.Open();
            DataTable dt = new DataTable();
            MySqlCommand cmdAssignedHardware = new MySqlCommand("SELECT  DATE_FORMAT(purchaseDate, '%Y-%m-%d') 'purchaseDate', type, manufacturerName, serialNr, internalNr, pictureLocation, nameAD, modelNr FROM hardware JOIN people on hardware.eventID = people.eventID ", mysqlConnectie);
            var AssignReader = cmdAssignedHardware.ExecuteReader();
            dt.Load(AssignReader);
            mysqlConnectie.Close();
            return dt;
        }
        public void DeleteHardware()
        {
            MySqlConnection mysqlConnectie = new MySqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            mysqlConnectie.Open();
            MySqlCommand cmdRemoveHardware = new MySqlCommand("DELETE FROM hardware WHERE serialNr= '" + SerialNr + "' and internalNr= '" + InternalNr + "'", mysqlConnectie);
            cmdRemoveHardware.ExecuteNonQuery();
            mysqlConnectie.Close();
        }
        /// <summary>
        /// Hardwares the history.
        /// </summary>
        /// <returns>DataTable.</returns>
        public DataTable HardwareHistory()
        {
            MySqlConnection mysqlConnectie = new MySqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            mysqlConnectie.Open();
            MySqlCommand getPeopleLinked = new MySqlCommand("SELECT nameAD, serialNr, internalNr, DATE_FORMAT(assignedDate, '%Y-%m-%d') 'assignedDate',DATE_FORMAT(returnedDate, '%Y-%m-%d') 'returnedDate' FROM archive JOIN people on archive.eventID = people.eventID where internalNr = '" + InternalNr + "'", mysqlConnectie);
            var AssignReader = getPeopleLinked.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(AssignReader);
            return dt;
        }
    }

}
//WebRequest request = WebRequest.Create(SetupFile.GlobalVar.ScripturaPath);
