using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
 
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class NestedGridView : System.Web.UI.Page
{
    AccessData manager = new AccessData();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //BindGrid();
        }
    }

    protected void BindGrid()
    {
         
        object sumToday = null;
       
        var ordersToday = manager.ReportSelectAllOrdersToday(DateTime.Today, out sumToday);
        gvOrders.DataSource = ordersToday;
        gvOrders.DataBind();

    }
    protected void gvOrders_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblorderID = (Label)e.Row.FindControl("lblorderID");
            DetailsView DetailsView1 = (DetailsView)e.Row.FindControl("DetailsView1");
            string orderID = lblorderID.Text;

            var odsToday = manager.ReportSelectOrderDetailJoinMemberByOID(Convert.ToInt32(orderID)); 
             
            DetailsView1.DataSource = odsToday;
            DetailsView1.DataBind();
        }
    }
}

 