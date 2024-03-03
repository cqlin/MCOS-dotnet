using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

public partial class CheckBalance : System.Web.UI.Page
{
    AccessData manager = new AccessData();

    string PICURL_Value;
  
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtBarcode.Focus();
            CleanSession();
            CleanScreen();

            List<Param> PList = new List<Param>();
            PList = (List<Param>)Session["ParamList"];

            manager.getParamValueByParamName(PList, "PICURL", out PICURL_Value);
            Session["PICURL_Value"] = PICURL_Value;
    
            string familyPicUrl = PICURL_Value + "F0000.jpg";
            (this.Master).ImagePath = familyPicUrl;

            /*
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
            */
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

    public void Cleanbarcode()
    {
        txtBarcode.Text = "";
    }


    protected void CleanSession()
    {
    }

    protected void CleanScreen()
    {
        lblStatus.Text = "";

        gvMemberInfo.DataSource = null;
        gvMemberInfo.DataBind();

        gvDepositHistory.DataSource = null;
        gvDepositHistory.DataBind();
    }

    protected void txtBarcode_TextChanged(object sender, EventArgs e)
    {
        String barcode = txtBarcode.Text.ToUpper();
        String firstBarcode = "";

        if (barcode.Length > 0)
        {
            firstBarcode = barcode.Substring(0, 1);
        }

        if (barcode.Length == 1 && firstBarcode == "M")
        {
            CleanSession();
            Response.Redirect(Request.RawUrl);
            txtBarcode.Focus();
        }
        else
        {
			// Classic ASP.NET - WebForms

			// *** Create META tag and add to header controls
			HtmlMeta RedirectMetaTag = new HtmlMeta();
			RedirectMetaTag.HttpEquiv = "Refresh";

			RedirectMetaTag.Content = string.Format("{0}; URL={1}", 15, "CheckBalance.aspx");

			this.Header.Controls.Add(RedirectMetaTag);
			
			//Or directly send with http header
			//Response.AppendHeader("Refresh", "5; url=CheckBalance.aspx");

            switch (firstBarcode)
            {
                case "M":

                    CleanSession();
                    CleanScreen();

                    if (manager.IsMemberExistByMemberCode(barcode))
                    {
                        Member member = manager.GetMember(barcode);

                        if (manager.IsFamilyIDExist(member.Family.FAMILY_ID))
                        {
                            string pictureCode = member.Family.FamilyPicture;
                            PICURL_Value = Session["PICURL_Value"].ToString();
                            string familyPicUrl = PICURL_Value + pictureCode;
                            (this.Master).ImagePath = familyPicUrl;

                            var myFamily = manager.SelectMemberFamily(member.MEMBER_ID);
                            gvMemberInfo.DataSource = myFamily;
                            gvMemberInfo.DataBind();
                 
                            lblStatus.Text = " Your record balance is $" + member.Family.Balance + ".";
                            Session["BalanceBefore"] = member.Family.Balance;
                            
                            var dHistory = manager.SelectRecentFamilyAccountActivitiesByFID(member.Family.FAMILY_ID);
                            gvDepositHistory.DataSource = dHistory;
                            gvDepositHistory.DataBind();

                            var anOrder = manager.SelectRecentOrdersByFamilyID(member.Family.FAMILY_ID);
                            GridViewRecentOrder.DataSource = anOrder;
                            GridViewRecentOrder.DataBind();
                        }
                        else
                        {
                            lblStatus.ForeColor = System.Drawing.Color.Black;
                            lblStatus.Text = "Your Family Account could not be found. Please ask Admin for help.";
                        }
                    }
                    else
                    {
                        lblStatus.ForeColor = System.Drawing.Color.Black;
                        lblStatus.Text = "Your Member Account could not be found. Please ask Admin for help.";
                    }

                    Cleanbarcode();
                    break;

                default:
                    lblStatus.ForeColor = System.Drawing.Color.Red;
                    lblStatus.Text = " Error: Invalid Barcode.";
                    break;
            }
			txtBarcode.Focus();
        }
    }
  

}
 