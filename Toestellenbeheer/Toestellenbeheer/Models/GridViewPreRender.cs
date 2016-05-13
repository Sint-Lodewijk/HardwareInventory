using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace Toestellenbeheer.Models
{
    public class GridViewPreRender
    {
        public GridView Gridview { get; set; }
        public GridViewPreRender(GridView gridview)
        {
            this.Gridview = gridview;
        }
        public void SetHeader()
        {
            if (Gridview.Rows.Count > 0)
            {
                Gridview.UseAccessibleHeader = true;
                Gridview.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }
    }
}