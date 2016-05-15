using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Toestellenbeheer.Models;

namespace Toestellenbeheer.Add
{
    public partial class add_db_user : System.Web.UI.Page
    {
        MySqlConnection mysqlConnectie = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void CreateAccount_Click(object sender, EventArgs e)
        {
            if (Password.Text == ConfirmPassword.Text)
            {
                if (drpRoleSelect.SelectedValue != "noneSelect")
                {
                    try
                    {
                        mysqlConnectie.Open();
                        var CreateDBUser = new MySqlCommand("INSERT INTO DBAccount (UserName, PassHash, UserGroup, ADAccount) VALUES (@UserName, SHA2(@Password, '" + Models.SetupFile.MySql.SHA2EncryptionKey + "'), @UserGroup, @ADAccount)", mysqlConnectie);
                        CreateDBUser.Parameters.AddWithValue("@UserName", UserName.Text);
                        CreateDBUser.Parameters.AddWithValue("@Password", Password.Text);
                        CreateDBUser.Parameters.AddWithValue("@UserGroup", drpRoleSelect.SelectedValue);
                        CreateDBUser.Parameters.AddWithValue("@ADAccount", ADAccount.Text);
                        CreateDBUser.ExecuteNonQuery();
                        var ShowSuccessAlert = new JSUtility();
                        ShowSuccessAlert.ShowAlert(this, "<strong>Success!</strong> The account is created!", "alert-success");
                    }
                    catch (MySqlException ex)
                    {
                        if (ex.Number == 1062)
                        {
                            var ShowSuccessAlert = new JSUtility();
                            ShowSuccessAlert.ShowAlert(this, "<strong>Warning!</strong> The account is already exist in the database!", "alert-danger");
                        }
                    }
                    finally
                    {
                        mysqlConnectie.Close();
                    }
                }
                else
                {

                    var ShowSuccessAlert = new JSUtility();
                    ShowSuccessAlert.ShowAlert(this, "<strong>Warning!</strong> A valid group is needed in order to create a new DB account!", "alert-warning");
                }
            }
            else
            {
                var ShowSuccessAlert = new JSUtility();
                ShowSuccessAlert.ShowAlert(this, "<strong>Warning!</strong> The passwords aren't the same!", "alert-warning");
            }
        }
    }
}