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
    string SMTPSRV_Value;
    string SMTPPORT_Value;
    string SMTPUSER_Value;
    string SMTPPASS_Value;
    string SMTPFROM_Value;
    string SMTPBCC_Value;
    string SMTPAUTH_Value;
    string SMTP_DEPOSIT_SUBJECT_Value;
  
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            List<Param> PList = new List<Param>();
            PList = (List<Param>)Session["ParamList"];

            manager.getParamValueByParamName(PList, "PICURL", out PICURL_Value);
            manager.getParamValueByParamName(PList, "SMTPSRV", out SMTPSRV_Value);
            manager.getParamValueByParamName(PList, "SMTPPORT", out  SMTPPORT_Value);
            manager.getParamValueByParamName(PList, "SMTPUSER", out SMTPUSER_Value);
            manager.getParamValueByParamName(PList, "SMTPPASS", out SMTPPASS_Value);
            manager.getParamValueByParamName(PList, "SMTPFROM", out SMTPFROM_Value);
            manager.getParamValueByParamName(PList, "SMTPBCC", out SMTPBCC_Value);
            manager.getParamValueByParamName(PList, "SMTPAUTH", out SMTPAUTH_Value);
            manager.getParamValueByParamName(PList, "SMTP_DEPOSIT_SUBJECT", out SMTP_DEPOSIT_SUBJECT_Value);
       
            Session["PICURL_Value"] = PICURL_Value;
            Session["SMTPSRV_Value"] = SMTPSRV_Value;
            Session["SMTPPORT_Value"] = SMTPPORT_Value;
            Session["SMTPUSER_Value"] = SMTPUSER_Value;
            Session["SMTPPASS_Value"] = SMTPPASS_Value;
            Session["SMTPFROM_Value"] = SMTPFROM_Value;
            Session["SMTPBCC_Value"] = SMTPBCC_Value;
            Session["SMTPAUTH_Value"] = SMTPAUTH_Value;
            Session["SMTP_DEPOSIT_SUBJECT_Value"] = SMTP_DEPOSIT_SUBJECT_Value;

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
 