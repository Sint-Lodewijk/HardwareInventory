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
                bindTypeToGrid();
                drpTypeList.Items[0].Selected = true;
                getTypeAssociatedHardware(drpTypeList.SelectedValue.ToString());

            }
        }
        protected void bindTypeToGrid()
        {
            try
            {
                mysqlConnectie.Open();

                MySqlCommand bindToGrid = new MySqlCommand("SELECT type FROM type", mysqlConnectie);
                MySqlDataReader rdrGetType = bindToGrid.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(rdrGetType);
                drpTypeList.DataSource = dt;
                drpTypeList.DataBind();
                mysqlConnectie.Close();

            }
            catch (MySqlException ex)
            {
                lblProblem.Text = ex.ToString();
            }
        }
        protected void typeList_SelectedIndexChanged(object sender, EventArgs e)
        {
            getTypeAssociatedHardware(drpTypeList.SelectedValue.ToString());
        }
        private void getTypeAssociatedHardware(String strType)
        {
            try
            {
                mysqlConnectie.Open();
                MySqlCommand getAssociatedHardwareFromType = new MySqlCommand("SELECT manufacturerName, serialNr, internalNr,  pictureLocation, modelNr FROM hardware JOIN type ON type.typeNr = hardware.typeNr WHERE type = '" + strType + "'", mysqlConnectie);
                MySqlDataAdapter adpa = new MySqlDataAdapter(getAssociatedHardwareFromType);
                getAssociatedHardwareFromType.ExecuteNonQuery();
                getAssociatedHardwareFromType.Dispose();
                DataSet ds = new DataSet();
                adpa.Fill(ds);
                grvAvailibleHardwareType.DataSource = ds;
                grvAvailibleHardwareType.DataBind();
                int intTotalAssociatedCount = grvAvailibleHardwareType.Rows.Count;
                if (intTotalAssociatedCount == 0)
                {
                    lblProblem.Text = "No availible hardware of this type of hardware";
                }
                else
                {
                    lblProblem.Text = intTotalAssociatedCount + " queries listed out.";
                }
            }
            catch (MySqlException ex)
            {
                lblProblem.Text = "MySQL exception occured: " + ex.InnerException.ToString();
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
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(grvAvailibleHardwareType, "Select$" + e.Row.RowIndex);
                e.Row.ToolTip = "Click to select this row.";
            }

        }
        private void requestSelectedHardware(String internalNr, String serialNr)
        {
            try
            {
                String requestDate = DateTime.Now.ToString("yyyy-MM-dd");
                mysqlConnectie.Open();
                MySqlCommand sendRequest = new MySqlCommand("INSERT INTO request (serialNr, internalNr,eventID, requestDate) Values (@serialNr, @internalNr, (Select DISTINCT eventID from people Where nameAD = @nameAD),@requestDate)", mysqlConnectie);
                sendRequest.Parameters.AddWithValue("@serialNr", serialNr);
                sendRequest.Parameters.AddWithValue("@internalNr", internalNr);
                sendRequest.Parameters.AddWithValue("@nameAD", Context.User.Identity.GetUserName());
                sendRequest.Parameters.AddWithValue("@requestDate", requestDate);
                sendRequest.ExecuteNonQuery();
                sendRequest.Dispose();
            }
            catch (MySqlException ex)
            {
                lblProblem.Text = ex.ToString();
            }
            finally
            {
                mysqlConnectie.Close();
            }
        }

        private void sendEmailNotification(String internalNr)
        {
            SmtpClient smtpClient = new SmtpClient(SetupFile.Email.MailServer, SetupFile.Email.SMTPPort);

            smtpClient.Credentials = new System.Net.NetworkCredential(SetupFile.Email.EmailFrom, SetupFile.Email.EmailPassword);
            //smtpClient.UseDefaultCredentials = true;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.EnableSsl = true;
            MailMessage mail = new MailMessage();
            mail.Subject = "A request has been made for " + internalNr;
            mail.Body = "A request has been made for " + internalNr ;
            //Setting From - To 
            mail.From = new MailAddress(SetupFile.Email.EmailFrom, "Hardware Request");
            mail.To.Add(new MailAddress(SetupFile.Email.EmailTo));

           smtpClient.Send(mail);
        }
        protected void grvAvailibleHardwareType_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnRequest.Visible = true;

        }

        protected void btnRequest_Click(object sender, EventArgs e)
        {
            String strInternalNr = grvAvailibleHardwareType.SelectedDataKey["internalNr"].ToString();
            String strSerialNr = grvAvailibleHardwareType.SelectedDataKey["serialNr"].ToString();

            requestSelectedHardware(strInternalNr, strSerialNr);
            sendEmailNotification(strInternalNr);
        }
    }
}