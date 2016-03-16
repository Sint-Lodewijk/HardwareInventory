using Microsoft.AspNet.Identity;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Toestellenbeheer.Users
{

    public partial class request_hardware : System.Web.UI.Page
    {
        MySqlConnection mysqlConnectie = new MySqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void typeList_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                mysqlConnectie.Open();
                MySqlCommand getAssociatedHardwareFromType = new MySqlCommand("SELECT manufacturerName, serialNr, internalNr,  pictureLocation, modelNr FROM hardware JOIN type ON type.typeNr = hardware.typeNr WHERE type = '" + drpTypeList.SelectedValue.ToString() + "'", mysqlConnectie);
                MySqlDataAdapter adpa = new MySqlDataAdapter(getAssociatedHardwareFromType);
                getAssociatedHardwareFromType.ExecuteNonQuery();
                getAssociatedHardwareFromType.Dispose();
                DataSet ds = new DataSet();
                adpa.Fill(ds);
                grvAvailibleHardwareType.DataSource = ds;
                grvAvailibleHardwareType.DataBind();
                int intTotalAssociatedCount = grvAvailibleHardwareType.Rows.Count;
                if (intTotalAssociatedCount == 0)
                {
                    lblProblem.Text = "No availible hardware of this type of hardware";
                }
                else
                {
                    lblProblem.Text = intTotalAssociatedCount + " queries listed out.";
                }
            }
            catch (MySqlException ex)
            {
                lblProblem.Text = "MySQL exception occured: " + ex.InnerException.ToString();
            }
            catch (Exception ex)
            {
                lblProblem.Text = ex.ToString();
            }
            finally
            {
                mysqlConnectie.Close();
            }
        }

        protected void grvAvailibleHardwareType_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(grvAvailibleHardwareType, "Select$" + e.Row.RowIndex);
                e.Row.ToolTip = "Click to select this row.";
            }

        }
        void requestSelectedHardware(String internalNr)
        {

        }

        protected void grvAvailibleHardwareType_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnRequest.Visible = true;

        }
    }
}