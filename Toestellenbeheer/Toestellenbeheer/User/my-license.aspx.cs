using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Toestellenbeheer.Models;

namespace Toestellenbeheer.User
{
    public partial class my_license : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var userID = new Models.User(Context.User.Identity.Name);
                int intEventID = userID.ReturnEventID();
                Session["eventID"] = intEventID;
            }
        }
        protected void DownloadFile(object sender, EventArgs e)
        {
            string path = "../UserUploads/License/";

            string filePath = (sender as LinkButton).CommandArgument;
            Response.ContentType = ContentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
            Response.WriteFile(path + Path.GetFileName(filePath));
            Response.End();
        }

   }
}