﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Toestellenbeheer.Models;

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
            GetADUser getIndex = new GetADUser();

            int userIndex = getIndex.returnEventID(strNameAD);
            mysqlConnectie.Open();

            MySqlCommand bindEventIDWithHardware = new MySqlCommand("UPDATE hardware SET eventID = '" + userIndex + "' WHERE internalNr LIKE '" +
                strInternalNr + "'", mysqlConnectie);
            bindEventIDWithHardware.ExecuteNonQuery();
            bindEventIDWithHardware.Dispose();
            mysqlConnectie.Close();

            Hardware requestedHardware = new Hardware();
            requestedHardware.archiveAssignedHardware(strSerialNr, strInternalNr, maxIndex); //Archive the assigned hardware
            String strModelNr = "";
            String strManufacturer = "";
            requestedHardware.createXML(strSerialNr, strInternalNr, strManufacturer, strNameAD, strNameAD, strModelNr); //Temporary

        }

        protected void setRequestAccept(int requestID)
        {
            try
            {
                mysqlConnectie.Open();
                MySqlCommand setRequestAccepted = new MySqlCommand("UPDATE request SET requestAccepted = true WHERE requestID = " + requestID, mysqlConnectie);
                setRequestAccepted.ExecuteNonQuery();
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