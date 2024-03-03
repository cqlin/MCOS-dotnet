using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
  

public partial class MyAccount : System.Web.UI.Page
{
    AccessData manager = new AccessData();

    string PICURL_Value;
  
    protected void Page_Load(object sender, EventArgs e)
    {
		List<string> UserRoles = (List<string>)Session["SessionRoles"];
        if (!IsPostBack && (UserRoles.Contains("MCOS_DEPOSIT") || UserRoles.Contains("MCOS_ADMIN") || UserRoles.Contains("MCOS_ECARD")))
        {

            List<Param> PList = new List<Param>();
            PList = (List<Param>)Session["ParamList"];

            manager.getParamValueByParamName(PList, "PICURL", out PICURL_Value);
            Session["PICURL_Value"] = PICURL_Value;
			
            int memberID = Int32.Parse(Session["SessionMemberID"].ToString());
            Member member = manager.GetMemberByID(memberID);
            string familyPicUrl = PICURL_Value + member.Family.FamilyPicture;
            (this.Master).ImagePath = familyPicUrl;

            var myFamily = manager.SelectMemberFamily(memberID);
            gvMemberInfo.DataSource = myFamily;
            gvMemberInfo.DataBind();

            var dHistory = manager.SelectRecentFamilyAccountActivitiesByFID(member.Family.FAMILY_ID);
            gvDepositHistory.DataSource = dHistory;
            gvDepositHistory.DataBind();

            var anOrder = manager.SelectRecentOrdersByFamilyID(member.Family.FAMILY_ID);
            GridViewRecentOrder.DataSource = anOrder;
            GridViewRecentOrder.DataBind();
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
 