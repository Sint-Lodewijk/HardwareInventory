using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace Toestellenbeheer.Add
{
    public partial class add_person : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public void CreateUserAccount(string userName, string userPassword)
        {
            try
            {
                string oGUID = string.Empty;
                string connectionPrefix = "LDAP://dc.6ib.eu/OU=Employees,DC=6ib,DC=eu";
                DirectoryEntry dirEntry = new DirectoryEntry(connectionPrefix);
                DirectoryEntry newUser = dirEntry.Children.Add
                    ("CN=" + userName, "user");
                newUser.Properties["samAccountName"].Value = userName;
                newUser.CommitChanges();
                oGUID = newUser.Guid.ToString();

                newUser.Invoke("SetPassword", new object[] { userPassword });
                newUser.CommitChanges();
                dirEntry.Close();
                newUser.Close();
            }
            catch (System.DirectoryServices.DirectoryServicesCOMException E)
            {
                //DoSomethingwith --> E.Message.ToString();

            }

        }
    }
}