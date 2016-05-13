using System;
using System.Collections.Generic;
using System.Data;
using System.DirectoryServices;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Toestellenbeheer.Models;
namespace Toestellenbeheer.Overview
{
    public partial class user_overview : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Models.User get = new Models.User();
            DataTable dt = get.ReturnDataTable();
            gv.DataSource = dt;
            gv.DataBind();
        }
        protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gv.PageIndex = e.NewPageIndex;
            gv.DataBind();
        }

        protected void grvPreRender(object sender, EventArgs e)
        {
            var gridview = sender as GridView;
            var Sort = new GridViewPreRender(gridview);
            Sort.SetHeader();
        }
    }
}