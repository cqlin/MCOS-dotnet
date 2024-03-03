using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Text;
using System.Runtime.Serialization.Json;  
using Newtonsoft.Json;
 


public partial class Balance : System.Web.UI.Page
{
    AccessData manager = new AccessData();

    Param thisParam = new Param();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
			int memberId = Convert.ToInt32(Request.Params["id"]);
			Member member = manager.GetMemberByID(memberId);

			var myFamily = manager.SelectMemberFamily(memberId);
			gvMemberInfo.DataSource = myFamily;
			gvMemberInfo.DataBind();
 
			lblStatus.Text = " Your record balance is $" + member.Family.Balance + ".";
			
			var dHistory = manager.SelectRecentFamilyAccountActivitiesByFID(member.Family.FAMILY_ID);
			gvDepositHistory.DataSource = dHistory;
			gvDepositHistory.DataBind();

			var anOrder = manager.SelectRecentOrdersByFamilyID(member.Family.FAMILY_ID);
			GridViewRecentOrder.DataSource = anOrder;
			GridViewRecentOrder.DataBind();
        }
        catch (Exception ex)
        {
			lblStatus.ForeColor = System.Drawing.Color.Red;
			lblStatus.Text = "Your Family Account could not be found. Please ask Admin for help.";
            string errormessage = String.Format("Error: {0} StackTrace: {1}", ex.Message, ex.StackTrace);
			//lblMessage.Text = errormessage;
        }        
	}
	 
	protected void gvMemberInfo_RowDataBound(object o, GridViewRowEventArgs e)
    {
        //Assumes the Price column is at index 2
        if (e.Row.RowType == DataControlRowType.DataRow)
            e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Right;
    }

    protected void gvDepositHistory_RowDataBound(object o, GridViewRowEventArgs e)
    {
        //Assumes the Price column is at index 2
        if (e.Row.RowType == DataControlRowType.DataRow)
            e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Right;
    }

    protected void GridViewRecentOrder_RowDataBound(object o, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
            e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Right;
    }

}