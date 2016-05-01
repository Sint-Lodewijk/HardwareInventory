using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace Toestellenbeheer
{
    public partial class Download : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lnkDownload.Text = Session["FileName"].ToString();
                lnkDownload.CommandArgument = Session["FileName"].ToString();
                DownloadFile();
            }
        }
        protected void DownloadFile()
        {
            string path = Session["FilePath"].ToString();
            string filePath = Session["FileName"].ToString();
            Response.ContentType = ContentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
            Response.WriteFile(path + Path.GetFileName(filePath));
            Response.End();
        }
        protected void lnkDownload_Click(object sender, EventArgs e)
        {
            DownloadFile();
        }
    }
}