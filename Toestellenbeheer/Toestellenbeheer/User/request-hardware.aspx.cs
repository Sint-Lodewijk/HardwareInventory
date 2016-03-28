using Microsoft.AspNet.Identity;
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
            var type = new TypeName();
            DataTable dt = type.ReturnDatatableType();
            drpTypeList.DataSource = dt;
            drpTypeList.DataBind();

        }
        protected void typeList_SelectedIndexChanged(object sender, EventArgs e)
        {
            getTypeAssociatedHardware(drpTypeList.SelectedValue.ToString());
        }
        private void getTypeAssociatedHardware(String strType)
        {
            try
            {
                var type = new TypeName(strType);
                DataTable dt = type.ReturnDatatableType();
                grvAvailibleHardwareType.DataSource = dt;
                grvAvailibleHardwareType.DataBind();
                int intTotalAssociatedCount = grvAvailibleHardwareType.Rows.Count;
                if (intTotalAssociatedCount == 0)
                {
                    lblProblem.Text = "No available hardware of this type of hardware";
                }
                else
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
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(grvAvailibleHardwareType, "Select$" + e.Row.RowIndex);
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
                        "<br /><br /><br />" + "Yours sincerely, " + Context.User.Identity.Name + "</div>";
                mail.IsBodyHtml = true;
                //Setting From - To 
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
            btnRequest.Visible = true;

        }

        protected void btnRequest_Click(object sender, EventArgs e)
        {
            String strInternalNr = grvAvailibleHardwareType.SelectedDataKey["internalNr"].ToString();
            String strSerialNr = grvAvailibleHardwareType.SelectedDataKey["serialNr"].ToString();
            String requestDate = DateTime.Now.ToString("yyyy-MM-dd");
            var userID = new User(Context.User.Identity.Name);
            int intEventID = userID.ReturnEventID();
            var hardwareRequest = new Request(strInternalNr, strSerialNr, intEventID, requestDate);
            hardwareRequest.RequestHardware();

            sendEmailNotification(strInternalNr);


            Session["SuccessInfo"] = "Congratulations, you have successfully required hardware with internal number: " + strInternalNr;
            Server.Transfer("~/Success.aspx");
        }
    }
}