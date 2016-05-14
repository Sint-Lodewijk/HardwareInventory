using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
namespace Toestellenbeheer.Models
{
    public class MySqlAuthentication
    {
        MySqlConnection mysqlConnectie = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
        public string UserName { get; set; }
        public string Password { get; set; }
        public MySqlAuthentication()
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="MySqlAuthentication"/> class.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        public MySqlAuthentication(string username, string password)
        {
            this.UserName = username;
            this.Password = password;
        }
        public string GetGroup()
        {
            mysqlConnectie.Open();
            var CheckUser = new MySqlCommand("SELECT UserGroup FROM DBAccount WHERE UserName = ?UserName  AND PassHash = SHA2(?Password, '" + SetupFile.MySql.SHA2EncryptionKey + "')", mysqlConnectie);
            CheckUser.Parameters.AddWithValue("?UserName", UserName);
            CheckUser.Parameters.AddWithValue("?Password", Password);
            Object checkObj = CheckUser.ExecuteScalar();
            mysqlConnectie.Close();
            if (checkObj == null)
            {
                return "";
            }
            else
            {
                return checkObj.ToString();
            }
        }

    }
}