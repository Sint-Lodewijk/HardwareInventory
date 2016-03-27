using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data;
using System.DirectoryServices;
using System.Xml;
using Toestellenbeheer.Models;

namespace Toestellenbeheer.Manage
{
    public partial class manage_hardware : System.Web.UI.Page
    {
        MySqlConnection mysqlConnectie = new MySqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                getUnassignedHardware();
                getUserFromAD();
            }
        }

        protected void getUnassignedHardware()
        {
            try
            {
                mysqlConnectie.Open();
                MySqlCommand bindToGrid = new MySqlCommand("SELECT DATE_FORMAT(purchaseDate, '%Y-%m-%d') 'purchaseDate', typeNr, manufacturerName, serialNr, internalNr, pictureLocation FROM hardware WHERE eventID IS NULL or eventID=''", mysqlConnectie);
                MySqlDataAdapter adpa = new MySqlDataAdapter(bindToGrid);
                bindToGrid.ExecuteNonQuery();
                bindToGrid.Dispose();
                DataSet ds = new DataSet();
                adpa.Fill(ds);
                grvHardwarePoolUnassigned.DataSource = ds;
                grvHardwarePoolUnassigned.DataBind();
                mysqlConnectie.Close();
            }
            catch (MySqlException ex)
            {
                lblResult.Text = ex.ToString();
            }
        }
        protected void grvHardwarePoolUnassigned_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(grvHardwarePoolUnassigned, "Select$" + e.Row.RowIndex);
                e.Row.ToolTip = "Click to select this row.";
            }
        }

        protected void grvPeopleAD_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(grvPeopleAD, "Select$" + e.Row.RowIndex);
                e.Row.ToolTip = "Click to select this row.";
            }
        }
        protected void getUserFromAD()
        {
            GetADUser getAD = new GetADUser();
            DataTable dt = getAD.returnDataTable();
            grvPeopleAD.DataSource = dt ;

            grvPeopleAD.DataBind();

        }
        protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvPeopleAD.PageIndex = e.NewPageIndex;
            grvPeopleAD.DataBind();
        }

        protected void assignHardwarePeople_Click(object sender, EventArgs e)
        {
            if (grvHardwarePoolUnassigned.SelectedRow != null && grvPeopleAD.SelectedRow != null)
            {
                String strSerialNr = grvHardwarePoolUnassigned.SelectedRow.Cells[1].Text.ToString();
                String strInternalNr = grvHardwarePoolUnassigned.SelectedRow.Cells[2].Text.ToString();
                String strNameAD = grvPeopleAD.SelectedRow.Cells[2].Text.ToString();


                mysqlConnectie.Open();
                MySqlCommand checkPeopleAlreadyExist = new MySqlCommand("SELECT eventID FROM people Where nameAD = '" + strNameAD + "'", mysqlConnectie);
                object checkObj = checkPeopleAlreadyExist.ExecuteScalar();

                if (checkObj == null)
                {
                    MySqlCommand addPeople = new MySqlCommand("INSERT INTO people (nameAD) values (@nameAD)", mysqlConnectie);
                    addPeople.Parameters.AddWithValue("@nameAd", strNameAD);
                    addPeople.ExecuteNonQuery();
                    addPeople.Dispose();

                    MySqlCommand getMaxIndex = new MySqlCommand("SELECT eventID FROM people WHERE eventID = (SELECT MAX(eventID) FROM people)", mysqlConnectie);

                    int maxIndex = Convert.ToInt16(getMaxIndex.ExecuteScalar().ToString());

                    assignHardware(maxIndex, strInternalNr);
                    mysqlConnectie.Close();
                    getUnassignedHardware();
                    String strManufacturer = "";
                    String strModelNr = "";
                    archiveAssignedHardware(strSerialNr, strInternalNr, maxIndex); //Archive the assigned hardware

                    createXML(strSerialNr, strInternalNr, strManufacturer, strNameAD, strNameAD, strModelNr); //Temporary
                }
                else
                {
                    int userID = Convert.ToInt16(checkPeopleAlreadyExist.ExecuteScalar());
                    assignHardware(userID, strInternalNr);
                    mysqlConnectie.Close();
                    getUnassignedHardware();
                    String strManufacturer = "";
                    String strModelNr = "";
                    archiveAssignedHardware(strSerialNr, strInternalNr, userID); //Archive the assigned hardware

                    createXML(strSerialNr, strInternalNr, strManufacturer, strNameAD, strNameAD, strModelNr); //Temporary

                }

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Please select a hardware or people to continue!');", true);
            }
        }
        private void assignHardware(int index, String internalNr)
        {
            MySqlCommand bindEventIDWithHardware = new MySqlCommand("UPDATE hardware SET eventID = '" + index + "' WHERE internalNr LIKE '" +
                        internalNr + "'", mysqlConnectie);
            bindEventIDWithHardware.ExecuteNonQuery();
            bindEventIDWithHardware.Dispose();
        }
        private void createXML(String serialNr, String internalNr, String manufacturer, String nameAD, String userName, String modelNr)
        {
            XmlDocument doc = new XmlDocument();
            XmlNode docNode = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            doc.AppendChild(docNode);

            XmlNode AssignedNodes = doc.CreateElement("AssignedHardware");
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
        private void archiveAssignedHardware(String strSerialNr, String strInternalNr, int intEventID)
        {
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
    }
    //WebRequest request = WebRequest.Create(SetupFile.GlobalVar.ScripturaPath);
}