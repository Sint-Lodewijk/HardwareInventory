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
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Login_Click(Object sender, EventArgs e)
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
                    errorLabel.Text = "Authentication did not succeed. Check user name and password.";
                }
            }
            catch (Exception ex)
            {
                errorLabel.Text = "Error authenticating. " + ex.Message;
            }
        }
    }
}