using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Toestellenbeheer.Archive
{
    public partial class hardware_history : System.Web.UI.Page
    {
        MySqlConnection mysqlConnectie = new MySqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                getHardware();

            }
        }
        protected void grvHardware_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(grvHardware, "Select$" + e.Row.RowIndex);
                e.Row.ToolTip = "Click to select this row.";
            }
        }
        protected void grvHardware_SelectedIndexChanged(object sender, EventArgs e)
        {
            String strInternalNr = grvHardware.SelectedDataKey.Value.ToString();
            mysqlConnectie.Open();
            MySqlCommand getPeopleLinked = new MySqlCommand("SELECT * FROM archive JOIN people on archive.eventID = people.eventID where internalNr = '" + strInternalNr + "'", mysqlConnectie);
            MySqlDataAdapter adpa = new MySqlDataAdapter(getPeopleLinked);
            getPeopleLinked.ExecuteNonQuery();
            getPeopleLinked.Dispose();
            DataSet ds = new DataSet();
            adpa.Fill(ds);
            grvHardware.DataSource = ds;
            grvHardware.DataBind();
        }
        protected void getHardware()
        {
            mysqlConnectie.Open();
            MySqlCommand getHardware = new MySqlCommand("SELECT pictureLocation, DATE_FORMAT(purchaseDate, '%Y-%m-%d') 'purchaseDate', typeNr, manufacturerName, serialNr, internalNr, warranty, extraInfo, DATE_FORMAT(addedDate, '%Y-%m-%d') 'addedDate', attachmentLocation FROM hardware", mysqlConnectie);
            MySqlDataAdapter adpa = new MySqlDataAdapter(getHardware);
            getHardware.ExecuteNonQuery();
            getHardware.Dispose();
            DataSet ds = new DataSet();
            adpa.Fill(ds);
            grvHardware.DataSource = ds;
            grvHardware.DataBind();
        }
    }
}