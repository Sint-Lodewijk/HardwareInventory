using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace Toestellenbeheer
{
    public partial class Success : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["SuccessInfo"] != null)
            {
                lblSuccessInfo.Text = Session["SuccessInfo"].ToString();
            }
        }
    }
}