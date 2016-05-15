using System;
using System.Web;
using System.Web.UI;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Owin;
using System.Web.Security;
using FormsAuth;
using Toestellenbeheer.Models;
namespace Toestellenbeheer.Account
{
    public partial class Login : Page
    {
        public bool IsADAuth;
        protected void Page_Load(object sender, EventArgs e)
        {
        }
        protected void Login_Click(Object sender, EventArgs e)
        {
            if (lnkAuthType.Text == "DB Authentication")
            {
                String adPathtemp = SetupFile.AD.ADRootPath; //Not necessary
                LdapAuthentication adAuthtemp = new LdapAuthentication(adPathtemp);
                String adPath = adAuthtemp.LDAPPath(); //get AD path from class
                LdapAuthentication adAuth = new LdapAuthentication(adPath);
                try
                {
                    if (true == adAuth.IsAuthenticated(UserName.Text, Password.Text))
                    {
                        String groups = adAuth.GetGroups();
                        //Create the ticket, and add the groups.
                        bool isCookiePersistent = RememberMe.Checked;
                        FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(1, UserName.Text,
                      DateTime.Now, DateTime.Now.AddMinutes(60), isCookiePersistent, groups);
                        // Session["group"] = groups;
                        //Encrypt the ticket.
                        String encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                        //Create a cookie, and then add the encrypted ticket to the cookie as data.
                        HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                        if (true == isCookiePersistent)
                            authCookie.Expires = authTicket.Expiration;
                        //Add the cookie to the outgoing cookies collection.
                        Response.Cookies.Add(authCookie);
                        //You can redirect now.
                        Response.Redirect(FormsAuthentication.GetRedirectUrl(UserName.Text, false));
                    }
                    else
                    {
                        var ShowFailedAlert = new JSUtility();
                        ShowFailedAlert.ShowAlert(this, "<strong>Warning!</strong> Authentication did not succeed. Check user name and password.", "alert-danger");
                        errorLabel.Text = "Authentication did not succeed. Check user name and password.";
                    }
                }
                catch (Exception ex)
                {
                    var ShowFailedAlert = new JSUtility();
                    ShowFailedAlert.ShowAlert(this, "<strong>Error authenticating.</strong> " + ex.Message, "alert-danger");
                    errorLabel.Text = "Error authenticating. " + ex.Message;
                }
            }
            else
            {
                try
                {
                    var userAuth = new MySqlAuthentication(UserName.Text, Password.Text);
                    string strUserGroup = userAuth.GetGroup();
                    if (strUserGroup != "")
                    {
                        String groups = strUserGroup;
                        //Create the ticket, and add the groups.
                        bool isCookiePersistent = RememberMe.Checked;
                        FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(1, UserName.Text,
                      DateTime.Now, DateTime.Now.AddMinutes(60), isCookiePersistent, groups);
                        // Session["group"] = groups;
                        //Encrypt the ticket.
                        String encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                        //Create a cookie, and then add the encrypted ticket to the cookie as data.
                        HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                        if (true == isCookiePersistent)
                            authCookie.Expires = authTicket.Expiration;
                        //Add the cookie to the outgoing cookies collection.
                        Response.Cookies.Add(authCookie);
                        //You can redirect now.

                        Response.Redirect(FormsAuthentication.GetRedirectUrl(UserName.Text, false));
                    }
                    else
                    {
                        errorLabel.Text = "Authentication did not succeed. Check user name and password.";
                        var ShowFailedAlert = new JSUtility();
                        ShowFailedAlert.ShowAlert(this, "<strong>Warning!</strong> Authentication did not succeed. Check user name and password.", "alert-danger");
                    }
                }
                catch (Exception ex)
                {
                    errorLabel.Text = "Error authenticating. " + ex.Message;
                    var ShowFailedAlert = new JSUtility();
                    ShowFailedAlert.ShowAlert(this, "<strong>Error authenticating.</strong> " + ex.Message, "alert-danger");
                }
            }
        }



        protected void DBAUTH(object sender, EventArgs e)
        {
            if (lnkAuthType.Text == "AD Authentication")
            {
                IsADAuth = false;
                TitleType.InnerText = "Please use your Active Directory Account to log into this application.";
                btnLogin.Text = "Login using the AD credentials";
                lnkAuthType.Text = "DB Authentication";
            }
            else
            {
                IsADAuth = true;
                TitleType.InnerText = "Please use your DB Account to log into this application.";
                btnLogin.Text = "Login using the DB credentials";
                lnkAuthType.Text = "AD Authentication";
            }
        }
    }
}