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
            User getAD = new User();
            DataTable dt = getAD.ReturnDataTable();
            grvPeopleAD.DataSource = dt;

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

                User getUserID = new User(strNameAD);
                int maxIndex = getUserID.ReturnEventID();
                mysqlConnectie.Open();
                assignHardware(maxIndex, strInternalNr);

                mysqlConnectie.Close();
                getUnassignedHardware();
                String strManufacturer = "";
                String strModelNr = "";
                Hardware assignedHardware = new Hardware();
                assignedHardware.ArchiveAssignedHardware(strSerialNr, strInternalNr, maxIndex); //Archive the assigned hardware

                assignedHardware.CreateXML("AssignedHardware", strSerialNr, strInternalNr, strManufacturer, strNameAD, strNameAD, strModelNr); //Temporary
            }



            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Please select a hardware or people to continue!');", true);
            }
        }
        private void assignHardware(int index, String internalNr)
        {
            var assignedHardware = new Hardware(index, internalNr);
            assignedHardware.BindEventID();
        }
    }
}