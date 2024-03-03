using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
 
 

public partial class _Default : System.Web.UI.Page
{
    AccessData manager = new AccessData();

    List<Param> PList = new List<Param>();
 
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!IsPostBack)
        { 
            var ParamList = manager.SelectAllParameters();
            PList = (List<Param>)ParamList;
            Session["ParamList"] = PList;

            string PICURL_Value;
            manager.getParamValueByParamName(PList, "PICURL", out PICURL_Value);

            string familyPicUrl = PICURL_Value + "F0000.jpg";
            (this.Master).ImagePath = familyPicUrl;
        }
   
        if (Session["sessionOperator"] != null)
            lblMessage.Text="Operator today is:     "+ Session["sessionOperator"].ToString();

        if (Session["MenuList"] != null)
        {
            List<Menu> MenuList = (List<Menu>)Session["MenuList"];
            gvMenu.DataSource = MenuList;
            gvMenu.DataBind();
        }
        else
        {
            var MenuList = manager.SelectAllMenu();
            gvMenu.DataSource = MenuList;
            gvMenu.DataBind();

            List<Menu> MItemsList = new List<Menu>();
            MItemsList = QuoteList();
            Session["MenuList"] = MItemsList;           
        }
    }

    protected void gvMenu_RowDataBound(object o, GridViewRowEventArgs e)
    {
        //Assumes the Price column is at index 4
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Right;
        }
    }

    public List<Menu> QuoteList()
    {
        List<Menu> migDataList = new List<Menu>();

        for (int i = 0; i < gvMenu.Rows.Count; i++)
        {
            Menu item = new Menu();
            for (int j = 0; j < gvMenu.Rows[i].Cells.Count; j++)
            {
                if (j == 0) { item.ID = int.Parse(gvMenu.Rows[i].Cells[0].Text); }
                if (j == 1) { item.Code = gvMenu.Rows[i].Cells[1].Text; }
                if (j == 2) { item.Description = gvMenu.Rows[i].Cells[2].Text; }
                if (j == 3) { item.Price = Convert.ToDecimal(gvMenu.Rows[i].Cells[3].Text); }
                if (j == 4) { item.Quantity = 1; }               
                if (j == 5) { item.Subtotal = Convert.ToDecimal(gvMenu.Rows[i].Cells[5].Text); } 
            }
            migDataList.Add(item);
        }
        return migDataList;
    }
}

  