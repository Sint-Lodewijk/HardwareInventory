using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

namespace Toestellenbeheer.Models
{
    public class Hardware
    {
        public void archiveAssignedHardware(String strSerialNr, String strInternalNr, int intEventID)
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
        public void createXML(String statusNode,String serialNr, String internalNr, String manufacturer, String nameAD, String userName, String modelNr)
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
        //Archive the assigned hardware into the database

    }
    //WebRequest request = WebRequest.Create(SetupFile.GlobalVar.ScripturaPath);
}
}