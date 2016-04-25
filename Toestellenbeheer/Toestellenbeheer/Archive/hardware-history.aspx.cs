using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Toestellenbeheer.Models;

namespace Toestellenbeheer.Archive
{
    public partial class hardware_history : System.Web.UI.Page
    {
        MySqlConnection mysqlConnectie = new MySqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                getHardware();

            }
        }
        protected void grvHardware_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(grvHardware, "Select$" + e.Row.RowIndex);
                e.Row.ToolTip = "Click to select this row.";
            }
        }
        protected void grvHardware_SelectedIndexChanged(object sender, EventArgs e)
        {
            String strInternalNr = grvHardware.SelectedDataKey.Value.ToString();
            mysqlConnectie.Open();
            MySqlCommand getPeopleLinked = new MySqlCommand("SELECT nameAD, serialNr, internalNr, DATE_FORMAT(assignedDate, '%Y-%m-%d') 'assignedDate',DATE_FORMAT(returnedDate, '%Y-%m-%d') 'returnedDate' FROM archive JOIN people on archive.eventID = people.eventID where internalNr = '" + strInternalNr + "'", mysqlConnectie);
            MySqlDataAdapter adpa = new MySqlDataAdapter(getPeopleLinked);
            getPeopleLinked.ExecuteNonQuery();
            getPeopleLinked.Dispose();
            DataSet ds = new DataSet();
            adpa.Fill(ds);
            grvPeopleLinked.DataSource = ds;
            grvPeopleLinked.DataBind();
            int intTotalResult = grvPeopleLinked.Rows.Count;
            modalTitle.InnerText = "Assign history of " + strInternalNr;
            if (intTotalResult == 0)
            {
                lblResult.Text = "The hardware with internal Nr: " + strInternalNr + " has never been assigned to a person before!";
            }
            else
            {
                lblResult.Text = "The hardware with internal Nr: " + strInternalNr + " has been assigned " + intTotalResult + " times";
            }
            modalShow();
        }
        protected void getHardware()
        {
            var hardware = new Hardware();
            DataTable dt = hardware.ReturnDatatableAllHardware();
            grvHardware.DataSource = dt;
            grvHardware.DataBind();
        }
        public void modalShow()
        {
            udpDetails.Update();
            ScriptManager.RegisterStartupScript(udpDetails, udpDetails.GetType(), "show", "$(function () { $('#" + modalHardware.ClientID + "').modal('show'); });", true);

        }

        protected void grvHardware_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string strInternalNr = grvHardware.DataKeys[e.RowIndex].Value.ToString();
            var ShowDetail = new JSUtility(hardwareDetailsPanel.ClientID);
            ShowDetail.DetailsPopUp(strInternalNr, grvDetail, imgHardware, udpHardwareDetails);

        }
        protected void DownloadFile(object sender, EventArgs e)
        {
            try
            {
                string path = "../UserUploads/Attachments/";

                string filePath = (sender as LinkButton).CommandArgument;
                Response.ContentType = ContentType;
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
                Response.WriteFile(path + Path.GetFileName(filePath));
                Response.End();
            }
            catch (Exception ex)
            {
                lblResult.Text = "Problem with downloading, please check if you added a attachment to the hardware." + ex.ToString();
            }
        }

    }

}