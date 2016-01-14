using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Toestellenbeheer.Manage
{
    public partial class add_license : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        //Change the color when selected
        protected void hardwareLicenseSelection_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in grvHardwareLicenseSelect.Rows)
            {
                if (row.RowIndex == grvHardwareLicenseSelect.SelectedIndex)
                {
                    row.BackColor = ColorTranslator.FromHtml("#A1DCF2");
                    row.ToolTip = string.Empty;
        //Get the row
                    GridViewRow selectedRow = grvHardwareLicenseSelect.SelectedRow;


                }
                else
                {
                    row.BackColor = ColorTranslator.FromHtml("#FFFFFF");
                    row.ToolTip = "Click to select this row.";
                }
            }
        }

        protected void assaingToSelectedHardware_Click(object sender, EventArgs e)
        {
            
        }
    }
   
}