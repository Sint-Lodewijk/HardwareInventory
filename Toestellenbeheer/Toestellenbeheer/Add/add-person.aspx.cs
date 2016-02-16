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
        protected void CreateAccount_Click(object sender, EventArgs e)
        {
            String strUserName = UserName.Text.Trim();
            String strPassword = Password.Text.Trim();
            String strGivenName = txtGivenName.Text.Trim();
            String strLastName = txtGivenName.Text.Trim();
            String strMemberOf = drpRoleSelect.SelectedValue.ToString();
            CreateUserAccount(strUserName, strPassword, strGivenName, strLastName, strMemberOf);
        }
        public void CreateUserAccount(String userName, String userPassword, String givenName, String lastName, String memberOf)
        {
            try
            {
                string oGUID = string.Empty;
                string connectionPrefix = "LDAP://dc.6ib.eu/OU=Employees,DC=6ib,DC=eu";
                int ADS_UF_ACCOUNTDISABLE = 2;

                DirectoryEntry dirEntry = new DirectoryEntry(connectionPrefix, "jli@6ib.eu", "1234QWEr");
                DirectoryEntry newUser = dirEntry.Children.Add("CN=" + userName, "user");
                newUser.Properties["userPrincipalName"].Value = userName + "@6ib.eu";
                newUser.Properties["samAccountName"].Value = userName;
                newUser.Properties["givenName"].Value = givenName;
                newUser.Properties["sn"].Value = lastName;
                //newUser.Properties["memberOf"].Value = memberOf;

                newUser.CommitChanges();
                oGUID = newUser.Guid.ToString();

                newUser.Invoke("SetPassword", new object[] { userPassword });

                newUser.CommitChanges();
                newUser.Close();
                int old_UAC = (int)newUser.Properties["userAccountControl"][0];

                newUser.Properties["userAccountControl"][0] = (old_UAC & ~ADS_UF_ACCOUNTDISABLE);

                dirEntry.Properties[memberOf].Add(userName);

                dirEntry.Close();

                errorLabel.Text = "The account is created.";
            }
            catch (System.DirectoryServices.DirectoryServicesCOMException E)
            {
                errorLabel.Text = "We cannot create a AD account, because: " + E.Message.ToString();

            }


        }


    }
}