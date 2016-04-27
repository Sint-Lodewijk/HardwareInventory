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
            else if (iframeDownload.Src != "")
            {
                iframeDownload.Src = "";

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
            modalTitleP.InnerText = "Assign history of " + strInternalNr;
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
            udpDetailsP.Update();
            ScriptManager.RegisterStartupScript(udpDetailsP, udpDetailsP.GetType(), "show", "$(function () { $('#" + modalHardwarePeople.ClientID + "').modal('show'); });", true);

        }

        protected void grvHardware_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string strInternalNr = grvHardware.DataKeys[e.RowIndex].Value.ToString();
            var ShowDetail = new JSUtility(modalHardware.ClientID);
            ShowDetail.DetailsPopUp(strInternalNr, grvDetail, imgHardware, udpDetailsP, modalTitle);

        }
       
        protected void DownloadFile(object sender, EventArgs e)
        {

            Session["FilePath"] = "UserUploads/Attachments/";

            Session["FileName"] = (sender as LinkButton).CommandArgument;
            var ShowDownload = new HardwareDetails();
            ShowDownload.IframeDownload(lnkDownloadB, (sender as LinkButton).CommandArgument, iframeDownload, this);

        }
        protected void lnkDownloadB_Click(object sender, EventArgs e)
        {
            string filePath = (sender as LinkButton).CommandArgument;

            Response.ContentType = ContentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
            Response.WriteFile("../UserUploads/Attachments/" + Path.GetFileName(filePath));
            Response.End();
        }
    }

}