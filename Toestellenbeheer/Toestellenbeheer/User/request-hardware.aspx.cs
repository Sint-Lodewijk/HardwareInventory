﻿using Microsoft.AspNet.Identity;
using MySql.Data.MySqlClient;
using System;
using System.Configuration;
using System.Data;
using System.Net.Mail;
using System.Web.UI;
using System.Web.UI.WebControls;
using Toestellenbeheer.Models;
namespace Toestellenbeheer.Users
{
    public partial class request_hardware : System.Web.UI.Page
    {
        MySqlConnection mysqlConnectie = new MySqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            

            if (!IsPostBack)
            {
                drpTypeList.DataBind();
                if (drpTypeList.Items.Count != 0)
                {
                    drpTypeList.Items[0].Selected = true;
                    getTypeAssociatedHardware(drpTypeList.SelectedValue.ToString());
                }
                else
                {
                    Server.Transfer("~/Default.aspx");
                }
            }
        }

        protected void typeList_SelectedIndexChanged(object sender, EventArgs e)
        {
            int intTypeIndex = drpTypeList.SelectedIndex;
            drpTypeList.DataBind();
            drpTypeList.Items[intTypeIndex].Selected = true;
            getTypeAssociatedHardware(drpTypeList.SelectedValue.ToString());
        }
        private void getTypeAssociatedHardware(String strType)
        {
            try
            {
                var type = new TypeName(strType);
                DataTable dt = type.AssociatedDatatableHardware();
                grvAvailableHardwareType.DataSource = dt;
                grvAvailableHardwareType.DataBind();
                int intTotalAssociatedCount = grvAvailableHardwareType.Rows.Count;
                if (intTotalAssociatedCount != 0)
                {
                    lblProblem.Text = intTotalAssociatedCount + " queries listed out.";
                }
            }
            catch (MySqlException ex)
            {
                lblProblem.Text = "MySQL exception occurred: " + ex.ToString();
            }
            catch (Exception ex)
            {
                lblProblem.Text = ex.ToString();
            }
            finally
            {
                mysqlConnectie.Close();
            }
        }
        protected void grvAvailibleHardwareType_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(grvAvailableHardwareType, "Select$" + e.Row.RowIndex);
                e.Row.ToolTip = "Click to select this row.";
            }
        }
        private void sendEmailNotification(String internalNr)
        {
            try
            {
                SmtpClient smtpClient = new SmtpClient(SetupFile.Email.MailServer, SetupFile.Email.SMTPPort);
                smtpClient.Credentials = new System.Net.NetworkCredential(SetupFile.Email.EmailFrom, SetupFile.Email.EmailPassword);
                //smtpClient.UseDefaultCredentials = true;
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.EnableSsl = true;
                MailMessage mail = new MailMessage();
                mail.Subject = "A request has been made for " + internalNr;
                mail.Body = "<div style=\"font-family: Segoe UI,Frutiger,Frutiger Linotype,Dejavu Sans,Helvetica Neue,Arial,sans-serif; \">" +
                    "Dear hardware admin <br /> <br />A request has been made for hardware with the internal number: " + internalNr +
                        "<br />Please <a href=\"" + SetupFile.Web.WebLocation + "\"> log in </a> to the application and review those requests." +
                        "<br />The message included is: " + txtMessage.Text +
                        "<br /><br />" + "Yours sincerely, " + Context.User.Identity.Name + "</div>";
                mail.IsBodyHtml = true;
                mail.From = new MailAddress(SetupFile.Email.EmailFrom, "Hardware Request");
                mail.To.Add(new MailAddress(SetupFile.Email.EmailTo));
                smtpClient.Send(mail);
            }
            catch (SmtpException ex)
            {
                lblProblem.Text = ex.ToString();
            }
        }
        protected void grvAvailibleHardwareType_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnNextStep.Visible = true;
        }
        protected void btnRequest_Click(object sender, EventArgs e)
        {
            String strInternalNr = grvAvailableHardwareType.SelectedDataKey["internalNr"].ToString();
            String strSerialNr = grvAvailableHardwareType.SelectedDataKey["serialNr"].ToString();
            String requestDate = DateTime.Now.ToString("yyyy-MM-dd");
            var userID = new Models.User(base.Context.User.Identity.Name);
            int intEventID = userID.ReturnEventID();
            var hardwareRequest = new Request(strInternalNr, strSerialNr, intEventID, requestDate);
            hardwareRequest.RequestHardware();
            sendEmailNotification(strInternalNr);
            var ShowSuccessAlert = new JSUtility();
            ShowSuccessAlert.ShowAlert(this, "<strong>Congratulations!</strong> The hardware request is successfull.", "alert-warning");

        }
        protected void grvAvailableHardwareType_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string strInternalNr = grvAvailableHardwareType.DataKeys[e.RowIndex]["internalNr"].ToString();
            var picLoc = new Models.Hardware(strInternalNr);
            picDetail.ImageUrl = "../UserUploads/Images/" + picLoc.PicLocation();
            var picModal = new JSUtility("hardwareImageModal");
            picModal.ModalShowUpdate(udpDetails);
        }

        protected void grvAvailableHardwareType_PreRender(object sender, EventArgs e)
        {
            var gridview = sender as GridView;
            var Sort = new GridViewPreRender(gridview);
            Sort.SetHeader();
        }
    }
}