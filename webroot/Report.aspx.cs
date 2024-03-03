using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Report : System.Web.UI.Page
{
    AccessData manager = new AccessData();

    protected void Page_Load(object sender, EventArgs e)
    {
		List<string> UserRoles = (List<string>)Session["SessionRoles"];
		if (!UserRoles.Contains("MCOS_ADMIN"))
		{
			this.btnSubmit.Click -= btnSubmit_Click;
			this.btnExcel.Click -= btnExcel_Click;
			return;
		}
        List<Param> PList = new List<Param>();
        PList = (List<Param>)Session["ParamList"];

        string PICURL_Value;
        manager.getParamValueByParamName(PList, "PICURL", out PICURL_Value);

        if (!IsPostBack)
        {
            string familyPicUrl = PICURL_Value + "F0000.jpg";
            (this.Master).ImagePath = familyPicUrl;
            PopulateDropDown();
        }    
 
        ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
        scriptManager.RegisterPostBackControl(this.btnExcel);
    }

    protected void GridView1_RowDataBound(object o, GridViewRowEventArgs e)
    {         
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Right;
            if (e.Row.Cells.Count > 3)
            {
                e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;
            }
        }
    }

    protected void GVODList_RowDataBound(object o, GridViewRowEventArgs e)
    {         
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Right;            
        }
    } 

    //add a list of reports to allow user to select
    private void PopulateDropDown()
    {
        List<string> reportList = new List<string>();
        reportList.Add("Order List");
        reportList.Add("Order Detail List");
        reportList.Add("Order Summary List");
        reportList.Add("Deposit List");
      
        ddReportType.DataSource = reportList;
        ddReportType.DataBind();
    }

    //Based on the report user selected, different data will be retrieved
    private void PopulateGrid()
    {
		List<string> UserRoles = (List<string>)Session["SessionRoles"];
        if (!UserRoles.Contains("MCOS_ADMIN")){
			lblStatus.ForeColor = System.Drawing.Color.Red;
            lblStatus.Text = "Unauthorized.";
			return;
		}
        string selectReport = ddReportType.SelectedValue;

        object sumOrderToday = null;
        object summaryToday = null;
        object sumDepositToday = null;

      
        if (selectReport == "Order List")
        {
            var ordersToday = manager.ReportSelectAllOrdersToday(DateTime.Today, out sumOrderToday); 
      
            GridView1.DataSource = ordersToday;
            GridView1.DataBind(); 
           
            lblSum.Text = sumOrderToday.ToString();
        }
        else if (selectReport == "Order Detail List")
        {
            var odToday = manager.ReportSelectAllOrderDetailsToday(DateTime.Today, out summaryToday);
            GridView1.DataSource = odToday;
            GridView1.DataBind(); 

            manager.ReportSelectAllOrdersToday(DateTime.Today, out sumOrderToday);
            lblSum.Text = sumOrderToday.ToString();
        }
        else if (selectReport == "Order Summary List")
        {
            var odToday = manager.ReportSelectAllOrderDetailsToday(DateTime.Today, out summaryToday);
            GridView1.DataSource = summaryToday;
            GridView1.DataBind();
 
            manager.ReportSelectAllOrdersToday(DateTime.Today, out sumOrderToday);
            lblSum.Text = sumOrderToday.ToString();
        }
        else if (selectReport == "Deposit List")
        {
            var depositToday = manager.ReportSelectAllDepositToday(DateTime.Today, out sumDepositToday);
            GridView1.DataSource = depositToday;
            GridView1.DataBind(); 
  
            lblSum.Text = sumDepositToday.ToString();
        }   

        if (GridView1.Rows.Count > 0)
            panel1.Visible = true;
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        PopulateGrid();
    }

    //When Excel button is clicked, call GridViewExportUtil
    protected void btnExcel_Click(object sender, EventArgs e)
    {
         GridViewExportUtil.Export("Report.xls", GridView1);
    }
      
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        //PopulateGrid();
    }
	
	protected void btnAsst_Click(object sender, EventArgs e)
    {
		lblAsst.Text = "";
		List<string> UserRoles = (List<string>)Session["SessionRoles"];
        if (!UserRoles.Contains("MCOS_ADMIN")){
			lblAsst.ForeColor = System.Drawing.Color.Red;
            lblAsst.Text = "Unauthorized.";
			return;
		}
		int ret = manager.UpdateAsstBalance(Session["SessionOperator"].ToString());
		lblAsst.ForeColor = System.Drawing.Color.Green;
		lblAsst.Text = "Updated records:"+ret;
    }


}