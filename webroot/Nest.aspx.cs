using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

 

public partial class Nest : System.Web.UI.Page
{
    AccessData manager = new AccessData();

    protected void Page_Load(object sender, EventArgs e)
    {
        List<Param> PList = new List<Param>();
        PList = (List<Param>)Session["ParamList"];

        string PICURL_Value;
        manager.getParamValueByParamName(PList, "PICURL", out PICURL_Value); 

        if (!IsPostBack)
        {
            string familyPicUrl = PICURL_Value + "F0000.jpg";
            (this.Master).ImagePath = familyPicUrl;
        }     
    }
 
  
    protected void gvOrders_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        { 
            string oID = gvOrders.DataKeys[e.Row.RowIndex].Value.ToString();
            var odsToday = manager.ReportSelectOrderDetailJoinMemberByOID(Convert.ToInt32(oID));

            GridView gvOrderDetails = e.Row.FindControl("gvOrderDetails") as GridView;

            gvOrderDetails.DataSource = odsToday;
            gvOrderDetails.DataBind();             
        }
    }
 

    protected void btnSubmit_Click(object sender, EventArgs e)
    {        
        String strDate = txtDate.Text; 
        object sumToday = null;
       

        if (strDate.Trim() != "")
        {
            DateTime dt = DateTime.ParseExact(txtDate.Text, "MM/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            var ordersToday = manager.ReportSelectAllOrdersToday(dt, out sumToday);
            gvOrders.DataSource = ordersToday;
            gvOrders.DataBind();
        } 
    }  
}