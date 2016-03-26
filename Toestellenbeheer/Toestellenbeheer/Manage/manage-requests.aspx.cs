using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Toestellenbeheer.Manage
{
    public partial class manage_requests : System.Web.UI.Page
    {
        MySqlConnection mysqlConnectie = new MySqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void grvRequests_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(grvRequests, "Select$" + e.Row.RowIndex);
                e.Row.ToolTip = "Click to select this row.";
            }
        }

        protected void grvRequests_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnAcceptRequest.Visible = true;
            btnDenyRequest.Visible = true;
        }

        protected void btnAcceptRequest_Click(object sender, EventArgs e)
        {
            int intRequestID = Convert.ToInt32(grvRequests.SelectedDataKey["requestID"].ToString());
            setRequestAccept(intRequestID);
            assignHardwareToPeople();
            Session["SuccessInfo"] = "Successfully accepted request";
            Server.Transfer("~/Success.aspx");

        }
        private void assignHardwareToPeople()
        {
            String strNameAD = grvRequests.SelectedDataKey["nameAD"].ToString();
            String strInternalNr = grvRequests.SelectedDataKey["internalNr"].ToString();
            String strSerialNr = grvRequests.SelectedDataKey["serialNr"].ToString();


            mysqlConnectie.Open();
            MySqlCommand addPeople = new MySqlCommand("INSERT INTO people (nameAd) values (@nameAD)", mysqlConnectie);
            addPeople.Parameters.AddWithValue("@nameAd", strNameAD);
            addPeople.ExecuteNonQuery();
            addPeople.Dispose();

            MySqlCommand getMaxIndex = new MySqlCommand("SELECT MAX(eventID) FROM people", mysqlConnectie);

            int maxIndex = Convert.ToInt16(getMaxIndex.ExecuteScalar().ToString());

            MySqlCommand bindEventIDWithHardware = new MySqlCommand("UPDATE hardware SET eventID = '" + maxIndex + "' WHERE internalNr LIKE '" +
                strInternalNr + "'", mysqlConnectie);
            bindEventIDWithHardware.ExecuteNonQuery();
            bindEventIDWithHardware.Dispose();
            mysqlConnectie.Close();
           
            archiveAssignedHardware(strSerialNr, strInternalNr, maxIndex); //Archive the assigned hardware

        }
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
    
    protected void setRequestAccept(int requestID)
        {
            try
            {
                mysqlConnectie.Open();
                MySqlCommand setRequestAccepted = new MySqlCommand("UPDATE request SET requestAccepted = true WHERE requestID = " + requestID, mysqlConnectie);
                setRequestAccepted.ExecuteNonQuery();
                    }
            catch(MySqlException ex)
            {
                lblExeption.Text = ex.ToString();
            }
            finally
            {
                mysqlConnectie.Close();
            }
        }
        protected void btnDenyRequest_Click(object sender, EventArgs e)
        {
            try
            {
                int intRequestID = Convert.ToInt32(grvRequests.SelectedDataKey["requestID"].ToString());
                mysqlConnectie.Open();
                MySqlCommand deleteSelectedRequest = new MySqlCommand("DELETE FROM request WHERE requestID = " + intRequestID, mysqlConnectie);
                deleteSelectedRequest.ExecuteNonQuery();
                Session["SuccessInfo"] = "Succesfully deleted request";
                Server.Transfer("~/Success.aspx");
            }
            catch (MySqlException ex)
            {
                lblExeption.Text = ex.ToString();
            }
            finally
            {
                mysqlConnectie.Close();
            }
        }
    }
}