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
            var request = new Request(intRequestID);
            request.AcceptRequest();
            assignHardwareToPeople();
            var ShowSuccessAlert = new JSUtility();
            grvRequests.DataBind();
            ShowSuccessAlert.ShowAlert(this, "<strong>Success!</strong> The request is successfully accepted!", "alert-success");
        }
        private void assignHardwareToPeople()
        {
            String strNameAD = grvRequests.SelectedDataKey["nameAD"].ToString();
            String strInternalNr = grvRequests.SelectedDataKey["internalNr"].ToString();
            String strSerialNr = grvRequests.SelectedDataKey["serialNr"].ToString();
            Models.User getIndex = new Models.User(strNameAD);
            int userIndex = getIndex.ReturnEventID();
            var request = new Hardware(userIndex, strInternalNr);
            request.BindEventID();
            Hardware requestedHardware = new Hardware();
            requestedHardware.ArchiveAssignedHardware(strSerialNr, strInternalNr, userIndex); //Archive the assigned hardware
            string strModelNr = grvRequests.SelectedDataKey["modelNr"].ToString();
            string strManufacturer = grvRequests.SelectedDataKey["manufacturerName"].ToString(); 
            string strType = grvRequests.SelectedDataKey["type"].ToString(); 
            //requestedHardware.CreateXML("AssignedHardware", strSerialNr, strInternalNr, strManufacturer, strNameAD, strNameAD, strModelNr); //Temporary
            var createPDF = new PDFHandler(strInternalNr, strSerialNr, strManufacturer, strType, strModelNr);
            createPDF.CreatePDF("AssignOverview", "Overview of assigned hardware", "Assign", Server.MapPath("../PDF/"),strNameAD);
            ShowPdf(Server.MapPath("../PDF/") + "AssignOverview.pdf");



        }
        public void ShowPdf(string filename)
        {
            //Clears all content output from Buffer Stream
            Response.ClearContent();
            //Clears all headers from Buffer Stream
            Response.ClearHeaders();
            //Adds an HTTP header to the output stream
            Response.AddHeader("Content-Disposition", "inline;filename=" + filename);
            //Gets or Sets the HTTP MIME type of the output stream
            Response.ContentType = "application/pdf";
            //Writes the content of the specified file directory to an HTTP response output stream as a file block
            Response.WriteFile(filename);
            //sends all currently buffered output to the client
            Response.Flush();
            //Clears all content output from Buffer Stream
            Response.Clear();
        }
        protected void btnDenyRequest_Click(object sender, EventArgs e)
        {
            try
            {
                int intRequestID = Convert.ToInt32(grvRequests.SelectedDataKey["requestID"].ToString());
                Request deniedRequest = new Request(intRequestID);
                deniedRequest.DenyRequest();
                var ShowSuccessAlert = new JSUtility();
                grvRequests.DataBind();
                ShowSuccessAlert.ShowAlert(this, "<strong>Success!</strong> The request is successfully deleted!", "alert-success");
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
        protected void grvRequests_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string strInternalNr = grvRequests.DataKeys[e.RowIndex]["internalNr"].ToString();
            var detailHardware = new Hardware(strInternalNr);
            DataTable dt = detailHardware.ReturnDatatableHardwareFromInternal();
            grvDetail.DataSource = dt;
            grvDetail.DataBind();
            imgHardware.ImageUrl = "../UserUploads/Images/" + detailHardware.PicLocation();
            var detailModalShow = new JSUtility(modalHardware.ClientID);
            detailModalShow.ModalShowUpdate(udpDetails);
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
                lblExeption.Text = "Problem with downloading, please check if you added a attachment to the hardware." + ex.ToString();
            }
        }

        protected void grvPreRender(object sender, EventArgs e)
        {
            var gridview = sender as GridView;
            var Sort = new GridViewPreRender(gridview);
            Sort.SetHeader();
        }
    }
}