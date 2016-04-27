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
using System.IO;

namespace Toestellenbeheer.Manage
{
    public partial class manage_database : System.Web.UI.Page
    {
        MySqlConnection mysqlConnectie = new MySqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var alertClose = new JSUtility("successMessageAlert");
                alertClose.CloseAlert(udpSuccess);
            }
        }
        protected void DownloadBackup(object sender, EventArgs e)
        {
            try
            {
                string path = "../Backup/";

                string filePath = (sender as LinkButton).CommandArgument;
                Response.ContentType = ContentType;
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
                Response.WriteFile(path + Path.GetFileName(filePath));
                Response.End();
            }
            catch (Exception ex)
            {
               // lblResult.Text = "Problem with downloading, please check if you added a attachment to the hardware." + ex.ToString();
            }
        }
        protected void btnBackup_Click(object sender, EventArgs e)
        {
            lblAlert.Text = "Backup successfully!";

            var showSuccess = new JSUtility("successMessageAlert");
            showSuccess.ShowJS(udpSuccess);
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

            Response.ContentType = ContentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(Server.MapPath("~/Backup/") + "mysql-backup.sql"));
            Response.WriteFile(Server.MapPath("~/Backup/") + "mysql-backup.sql");
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