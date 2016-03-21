using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Toestellenbeheer.Models;

namespace Toestellenbeheer.Add
{
    public partial class add_person : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// Clicking button - Create a user into ActiveDirectory
        /// </summary>

        protected void CreateAccount_Click(object sender, EventArgs e)
        {
            String strUserName = UserName.Text.Trim();
            String strPassword = Password.Text.Trim();
            String strGivenName = txtGivenName.Text.Trim();
            String strLastName = txtLastName.Text.Trim();
            String strMemberOf = drpRoleSelect.SelectedValue.ToString();
            CreateADUser(strUserName, strPassword, strGivenName, strLastName, strMemberOf);
        }
        /// <summary>
        /// Creates an user in Active Directory
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="userPassword">User password.</param>
        /// <param name="givenName">The first name</param>
        /// <param name="lastName">The last name.</param>
        /// <param name="memberOf">Group name - hardware admin(istration) or user from dropdownlist.</param>
        private void CreateADUser(String userName, String userPassword, String givenName, String lastName, String memberOf)
        {

            PrincipalContext pc = new PrincipalContext(ContextType.Domain,  SetupFile.AD.ADDomainControllerName, SetupFile.AD.ADPath, SetupFile.AD.ADUserName, SetupFile.AD.ADUserPassword);

            UserPrincipal up = new UserPrincipal(pc);
            
                try
                {
                    up.GivenName = givenName;
                    up.Surname = lastName;
                    up.SamAccountName = userName;
                    up.UserPrincipalName = userName + SetupFile.AD.ADSAMAccountAt;

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
                    errorLabel.Text = "An exeption occured " + ex.ToString();
                }
            
        }

        /// <summary>
        /// Adds the user to group.
        /// </summary>
        /// <param name="ADUserPrincipalName">Userprincipalname from Active Directory</param>
        /// <param name="groupName">Name of the group. (gg_hardware_admin(istration), user)</param>
        public void AddUserToGroup(string ADUserPrincipalName, string groupName)
        {
            try
            {
                PrincipalContext pc = new PrincipalContext(ContextType.Domain, SetupFile.AD.ADDomainControllerName, SetupFile.AD.ADPath, SetupFile.AD.ADUserName, SetupFile.AD.ADUserPassword);
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