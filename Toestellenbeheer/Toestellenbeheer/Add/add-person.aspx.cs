using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace Toestellenbeheer.Add
{
    public partial class add_person : System.Web.UI.Page
    {
        string connectionPrefix = "LDAP://dc.6ib.eu/OU=Employees,DC=6ib,DC=eu";

        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void CreateAccount_Click(object sender, EventArgs e)
        {
            String strUserName = UserName.Text.Trim();
            String strPassword = Password.Text.Trim();
            String strGivenName = txtGivenName.Text.Trim();
            String strLastName = txtLastName.Text.Trim();
            String strMemberOf = drpRoleSelect.SelectedValue.ToString();
            CreateADUser(strUserName, strPassword, strGivenName, strLastName, strMemberOf);
        }

        private void CreateADUser(String userName, String userPassword, String givenName, String lastName, String memberOf)
        {

            using (PrincipalContext pc = new PrincipalContext(ContextType.Domain, "dc", "OU=Employees,DC=6ib,DC=eu", "jli@6ib.eu", "1234QWEr"))

            using (UserPrincipal up = new UserPrincipal(pc))
            {
                try
                {
                    up.GivenName = givenName;
                    up.Surname = lastName;
                    up.SamAccountName = userName;
                    up.UserPrincipalName = userName + "@6ib.eu";

                    up.SetPassword(userPassword);
                    up.Enabled = true;
                    up.Save();
                    if (memberOf != "noneSelect")
                    {
                        AddUserToGroup(up.UserPrincipalName, memberOf);
                        errorLabel.Text = "The account is created.";
                    }
                    else
                    {
                        errorLabel.Text = "The account is created, but without assigning group.";
                    }
                }
                catch (TargetInvocationException ex)
                {
                    errorLabel.Text = ex.InnerException.ToString();
                }
                catch (System.DirectoryServices.DirectoryServicesCOMException E)
                {
                    errorLabel.Text = "We cannot create a AD account, because: " + E.Message.ToString();

                }
                catch (Exception ex)
                {
                    errorLabel.Text = "An exeption occured " + ex.InnerException.ToString();
                }
            }
        }


        public void AddUserToGroup(string ADUserPrincipalName, string groupName)
        {
            try
            {
                using (PrincipalContext pc = new PrincipalContext(ContextType.Domain, "dc", "OU=Employees,DC=6ib,DC=eu", "jli@6ib.eu", "1234QWEr"))
                {
                    GroupPrincipal group = GroupPrincipal.FindByIdentity(pc, groupName);
                    group.Members.Add(pc, IdentityType.UserPrincipalName, ADUserPrincipalName);
                    group.Save();
                }
            }
            catch (System.DirectoryServices.DirectoryServicesCOMException E)
            {
                errorLabel.Text = E.InnerException.ToString();
            }
        }
    }
}