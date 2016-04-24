using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using Toestellenbeheer.Models;
using System.Data;

namespace Toestellenbeheer.Manage
{
    public partial class manage_database : System.Web.UI.Page
    {
        MySqlConnection mysqlConnectie = new MySqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnBackup_Click(object sender, EventArgs e)
        {
            Backup();
        }
        private void Backup()
        {
            var backupcmd = new MySqlCommand();
            backupcmd.Connection = mysqlConnectie;
            var backup = new MySqlBackup(backupcmd);
            mysqlConnectie.Open();
            backup.ExportInfo.AddCreateDatabase = true;
            backup.ExportToFile(Server.MapPath("~/Backup/") + "mysql-backup.sql");
            mysqlConnectie.Close();
        }
        protected void btnRestore_Click(object sender, EventArgs e)
        {

            //Save the uploaded file to the predefined path
            fileRestore.PostedFile.SaveAs(Server.MapPath("~/Backup/") + "mysql-restore.sql");
            var restorecmd = new MySqlCommand();
            restorecmd.Connection = mysqlConnectie;
            var restore = new MySqlBackup(restorecmd);

            mysqlConnectie.Open();
            restore.ImportInfo.DatabaseDefaultCharSet = "utf8";
            restore.ImportFromFile(Server.MapPath("~/Backup/") + "mysql-restore.sql");
            mysqlConnectie.Close();

        }



        protected void btnTruncate_Click(object sender, EventArgs e)
        {
            Backup();
            var table = new MySqlUtility();
            DataTable dt = table.DtTable();
            mysqlConnectie.Open();
            var setCheck0 = new MySqlCommand("SET FOREIGN_KEY_CHECKS = 0", mysqlConnectie);
            setCheck0.ExecuteNonQuery();

            for (int i = 0; i < dt.Rows.Count; ++i)
            {
                MySqlCommand truncateTable = new MySqlCommand("TRUNCATE " + dt.Rows[i].ItemArray[0].ToString(), mysqlConnectie);
                truncateTable.ExecuteNonQuery();
            }

            var setCheck1 = new MySqlCommand("SET FOREIGN_KEY_CHECKS = 1", mysqlConnectie);
            setCheck1.ExecuteNonQuery();
            mysqlConnectie.Close();
        }
    }
}