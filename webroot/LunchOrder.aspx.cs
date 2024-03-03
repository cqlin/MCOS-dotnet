using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using System.Net;
using System.IO;
using System.Text;
using System.Web.Hosting;
  

public partial class LunchOrder : System.Web.UI.Page
{
    AccessData manager = new AccessData();
       
    Order order = new Order(); 
     
    Menu menuitem = new Menu();

    public List<Menu> miList = new List<Menu>();
    public List<Menu> sessionMenuList = new List<Menu>();

    string PICURL_Value;
    string SMTPSRV_Value;
    string SMTPPORT_Value;
    string SMTPUSER_Value;
    string SMTPPASS_Value;
    string SMTPFROM_Value;
    string SMTPFROM_Name;
    string SMTPBCC_Value;
    string SMTPAUTH_Value;
    string SMTP_PURCHASE_SUBJECT_Value;
   
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["SessionRoles"] == null)
			Response.Redirect("login.aspx");        

        if (!IsPostBack)
        {
            txtBarcode.Focus();
            CleanSession();
            ResetDataList();

            List<Param> PList = new List<Param>();
            PList = (List<Param>)Session["ParamList"];

            manager.getParamValueByParamName(PList, "PICURL", out PICURL_Value);
            manager.getParamValueByParamName(PList, "SMTPSRV", out SMTPSRV_Value);
            manager.getParamValueByParamName(PList, "SMTPPORT", out  SMTPPORT_Value);
            manager.getParamValueByParamName(PList, "SMTPUSER", out SMTPUSER_Value);
            manager.getParamValueByParamName(PList, "SMTPPASS", out SMTPPASS_Value);
            manager.getParamValueByParamName(PList, "SMTPFROM", out SMTPFROM_Value);
            manager.getParamValueByParamName(PList, "SMTPFROM_NAME", out SMTPFROM_Name);
            manager.getParamValueByParamName(PList, "SMTPBCC", out SMTPBCC_Value);
            manager.getParamValueByParamName(PList, "SMTPAUTH", out SMTPAUTH_Value);
            manager.getParamValueByParamName(PList, "SMTP_PURCHASE_SUBJECT", out SMTP_PURCHASE_SUBJECT_Value);
   
            Session["PICURL_Value"] = PICURL_Value;
            Session["SMTPSRV_Value"] = SMTPSRV_Value;
            Session["SMTPPORT_Value"] = SMTPPORT_Value;
            Session["SMTPUSER_Value"] = SMTPUSER_Value;
            Session["SMTPPASS_Value"] = SMTPPASS_Value;
            Session["SMTPFROM_Value"] = SMTPFROM_Value;
            Session["SMTPFROM_Name"] = SMTPFROM_Name;
            Session["SMTPBCC_Value"] = SMTPBCC_Value;
            Session["SMTPAUTH_Value"] = SMTPAUTH_Value;
            Session["SMTP_PURCHASE_SUBJECT_Value"] = SMTP_PURCHASE_SUBJECT_Value;
    
            string familyPicUrl = PICURL_Value + "F0000.jpg";
            (this.Master).ImagePath = familyPicUrl; 
        }
    }

    // deluxeCount:            31
    // hfdeluxeCount:          32
    // friedCount:             33
    // hffriedCount:           34
    // breadCount:             35
    // hfbreadCount:           36
    // deluxeFriedRiceCount:   17
    // hfdeluxeFriedRiceCount: 18
    // deluxeFriedRiceCount:   19
    // hfdeluxeFriedRiceCount: 20

    protected void CleanSession()
    {
        Session["Total"] = 0;
        Session["Balance"] = 0;
        Session["MemberSessionCode"] = null;
        Session["FamilyEmail"] = null;
        Session["FamilyID"] = null;
        Session["MemberName"] = null;
        Session["MemberID"] = null;
        Session["OrderID"] = null;
        Session["MIList"] = null;
        Session["Return"] = null;
        Session["CheckOut"] = null;  
    }

    protected void ResetDataList()
    {
        miList = null;
        menuitem = null;
        order = null;
    }

    protected void CleanScreen()
    {
        GridViewItemDetail.DataSource = null;
        GridViewItemDetail.DataBind();

        GridViewReturnOrder.DataSource = null;
        GridViewReturnOrder.DataBind();

        GridViewReturnOD.DataSource = null;
        GridViewReturnOD.DataBind();

        lblTotal.Text = Session["total"].ToString();
        lblBalance.Text = Session["Balance"].ToString();
    }
    
    protected void gvOrderMember_RowDataBound(object o, GridViewRowEventArgs e)
    {
        //Assumes the Price column is at index 4,5
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Right;
        }
    }

    protected void GridViewItemDetail_RowDataBound(object o, GridViewRowEventArgs e)
    {         
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Right;
        }
    }

    protected void GridViewReturnOrder_RowDataBound(object o, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)       
            e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Right; 
    }

    protected void GridViewReturnOD_RowDataBound(object o, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Right;
        }
    }   

    public void getTotalPrice(Menu menuitem)
    {
        if (Session["total"] == null)
            Session["total"] = Convert.ToDecimal(menuitem.Price);
        else
            Session["total"] = Convert.ToDecimal(menuitem.Price) + Convert.ToDecimal(Session["total"].ToString());

        lblTotal.Text = Session["total"].ToString();
    }

    public void getBalance(Menu menuitem)
    {
        if (menuitem != null)
            Session["Balance"] = Convert.ToDecimal(Session["Balance"].ToString()) - Convert.ToDecimal(menuitem.Price);
        lblBalance.Text = Session["Balance"].ToString();
    }

    public void Cleanbarcode()
    {
        txtBarcode.Text = "";
        txtBarcode.Focus();
    }
 
   
    protected void SendEmail(string fEmail)
    {
        MailMessage mail = new MailMessage();

        SMTPSRV_Value = Session["SMTPSRV_Value"].ToString();
        SMTPPORT_Value = Session["SMTPPORT_Value"].ToString();
        SMTPUSER_Value = Session["SMTPUSER_Value"].ToString();
        SMTPPASS_Value = Session["SMTPPASS_Value"].ToString();
        SMTPFROM_Value = Session["SMTPFROM_Value"].ToString();
        SMTPFROM_Name = Session["SMTPFROM_Name"].ToString();
        SMTPBCC_Value = Session["SMTPBCC_Value"].ToString();        
        SMTPAUTH_Value = Session["SMTPAUTH_Value"].ToString();
        SMTP_PURCHASE_SUBJECT_Value = Session["SMTP_PURCHASE_SUBJECT_Value"].ToString();

        try
        {       
			System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;//Need to have this, otherwise doesn't work

            string mName1 = Session["MemberName"].ToString();
            string mCode1 = Session["MemberSessionCode"].ToString();
            string strOID1 = Session["OrderID"].ToString();
            List<Menu> myList =  (List<Menu>)Session["MIList"];
            string strTotal1 = Session["Total"].ToString();
            string strBalance1 = Session["Balance"].ToString();
            decimal dBalance = Convert.ToDecimal(Session["Balance"].ToString());
            string strReminder = "";
            
            if (dBalance < 10.00M) 
                strReminder = "Your balance is less than $10.";
       
            string strBody = PopulateBody(mName1, mCode1, strBalance1, strTotal1, strOID1, myList, strReminder);

            mail.To.Add(new MailAddress(fEmail, mName1));
            mail.From = new MailAddress(SMTPFROM_Value, SMTPFROM_Name);
            mail.Bcc.Add(new MailAddress(SMTPBCC_Value));
   
            mail.Subject = SMTP_PURCHASE_SUBJECT_Value + " " + DateTime.Today.Date.ToString("MM/dd/yyyy");
            mail.Body =strBody;
              
            mail.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient();
            smtp.Host = SMTPSRV_Value;
            smtp.Port = Convert.ToInt32(SMTPPORT_Value);
			smtp.EnableSsl = true;
			smtp.UseDefaultCredentials = false;

            if (SMTPAUTH_Value == "Y") {
                smtp.Credentials = new NetworkCredential(SMTPUSER_Value, SMTPPASS_Value);
				//lblError.Text += "Before send,";
                smtp.Send(mail);
				//lblError.Text += "after send,";
			}
        }
        catch (Exception ex)
        {
			//lblError.Text += ex.ToString();
            throw ex;
        }

        finally
        {
            if (mail != null)
            {
                mail.Dispose();
            }
        }
    }
  
    private string PopulateBody (string mName, string mCode, string strBalance, string strTotal, string strOID, List<Menu> theList, string strRmd)
    {
        string body = string.Empty;
        using (StreamReader reader = new StreamReader(Server.MapPath("~/EmailTemplate.html")))
        {
            body = reader.ReadToEnd(); 
        }

        body = body.Replace("{MemberName}", mName);
        body = body.Replace("{MemberCode}", mCode);       
        body = body.Replace("{OrderID}", strOID);
        body = body.Replace("{OrderDetails}", GridViewToHtml(makeGridview(theList)));
        body = body.Replace("{Total}", strTotal);
        body = body.Replace("{Balance}", strBalance);
        body = body.Replace("{BalanceReminder}", strRmd);
       
        return body;
    }
    

    private string GridViewToHtml(GridView gv)
    {
        StringBuilder sb = new StringBuilder();
        StringWriter sw = new StringWriter(sb);
        HtmlTextWriter hw = new HtmlTextWriter(sw);
        gv.RenderControl(hw);
        return sb.ToString();
    }

    public GridView makeGridview(List<Menu> Dt)
    {
        GridView GV = new GridView();
        GV.DataSource = Dt;
        GV.DataBind();
        return GV;
    }
 
    
    protected void getVSOrderQuantity( bool AddOrReturn, string strItem )
    {
        if (AddOrReturn)  // Add/return
        { 
            switch (strItem.Substring(3, 2))
            {
                case "31":
                    ViewState["deluxeCount"] = Convert.ToInt16(ViewState["deluxeCount"]) + 1;
                    break;
                case "32":
                    ViewState["hfdeluxeCount"] = Convert.ToInt16(ViewState["hfdeluxeCount"]) + 1;
                    break;
                case "33":
                    ViewState["friedCount"] = Convert.ToInt16(ViewState["friedCount"]) + 1;
                    break;
                case "34":
                    ViewState["hffriedCount"] = Convert.ToInt16(ViewState["hffriedCount"]) + 1;
                    break;
                case "35":
                    ViewState["breadCount"] = Convert.ToInt16(ViewState["breadCount"]) + 1;
                    break;
                case "36":
                    ViewState["hfbreadCount"] = Convert.ToInt16(ViewState["hfbreadCount"]) + 1;
                    break;
                default:
                    break;
            }
        }
        else
        {
            switch (strItem.Substring(3, 2))
            {
                case "31":
                    ViewState["deluxeCount"] = Convert.ToInt16(ViewState["deluxeCount"]) - 1;
                    break;
                case "32":
                    ViewState["hfdeluxeCount"] = Convert.ToInt16(ViewState["hfdeluxeCount"]) - 1;
                    break;
                case "33":
                    ViewState["friedCount"] = Convert.ToInt16(ViewState["friedCount"]) - 1;
                    break;
                case "34":
                    ViewState["hffriedCount"] = Convert.ToInt16(ViewState["hffriedCount"]) -1;
                    break;
                case "35":
                    ViewState["breadCount"] = Convert.ToInt16(ViewState["breadCount"]) - 1;
                    break;
                case "36":
                    ViewState["hfbreadCount"] = Convert.ToInt16(ViewState["hfbreadCount"]) - 1;
                    break;
                default:
                    break;
            }
        }
    }

    protected bool IsReturnItemExist(Menu menuitem, List<OrderDetail> rList)
    {
        bool Exist = false;
        Menu thisItem = new Menu();
        sessionMenuList = (List<Menu>)Session["MenuList"];
        try
        {
            for (int i = 0; i < rList.Count; i++)
            {       
                    int iID = rList[i].ItemID;

                    int index = sessionMenuList.FindIndex(item => item.ID == iID);
                    if (index >= 0)
                        thisItem = sessionMenuList[index];
          
                if (menuitem.Code.Substring(3,2).Equals(thisItem.Code.Substring(3,2)))
                {
                    switch (menuitem.Code.Substring(3, 2))
                    {
                        case "31":
                            if (Convert.ToInt16(ViewState["deluxeCount"]) > 0)
                                Exist = true;
                            break;
                        case "32":
                            if (Convert.ToInt16(ViewState["hfdeluxeCount"]) > 0)
                                Exist = true;
                            break;
                        case "33":                          
                            if (Convert.ToInt16(ViewState["friedCount"]) > 0)
                                Exist = true;
                            break;
                        case "34":
                            if (Convert.ToInt16(ViewState["hffriedCount"]) > 0)
                                Exist = true;
                            break;
                        case "35":
                            if (Convert.ToInt16(ViewState["breadCount"]) > 0)
                                Exist = true;
                            break;
                        case "36":
                            if (Convert.ToInt16(ViewState["hfbreadCount"]) > 0)
                                Exist = true;
                            break;
                        default:
                            Exist = false;
                            break;
                    }
                }
            }
            return Exist;
        }
        catch (Exception ex)
        {
            string errormessage = String.Format("Error: {0} StackTrace: {1}", ex.Message, ex.StackTrace);
            return false;
        }
     }

    protected void  countVSLastOrdersItems( List<OrderDetail> lastOD)
    {
        for (int i = 0; i < lastOD.Count; i++)
        {
            string strState = lastOD[i].MenuItem.Itemcode.Substring(0, 1);
            switch (lastOD[i].MenuItem.Itemcode.Substring(3, 2))            
            {
                case "31":
                    if (strState == "B")
                        ViewState["deluxeCount"] = Convert.ToInt32(ViewState["deluxeCount"]) + lastOD[i].ItemQuantity;
                    else if (strState == "D")
                        ViewState["deluxeCount"] = Convert.ToInt32(ViewState["deluxeCount"]) - lastOD[i].ItemQuantity;
                    break;
                case "32":
                    if (strState == "B")
                        ViewState["hfdeluxeCount"] = Convert.ToInt32(ViewState["hfdeluxeCount"]) + lastOD[i].ItemQuantity;
                    else if (strState == "D")
                        ViewState["hfdeluxeCount"] = Convert.ToInt32(ViewState["hfdeluxeCount"]) - lastOD[i].ItemQuantity;
                    break;
                case "33":                      
                     if (strState == "B")
                         ViewState["friedCount"] = Convert.ToInt32(ViewState["friedCount"]) + lastOD[i].ItemQuantity;
                     else if (strState == "D")
                         ViewState["friedCount"] = Convert.ToInt32(ViewState["friedCount"]) - lastOD[i].ItemQuantity;
                    break;
                case "34":                   
                    if (strState == "B")
                        ViewState["hffriedCount"] = Convert.ToInt32(ViewState["hffriedCount"]) + lastOD[i].ItemQuantity;
                    else if (strState == "D")
                        ViewState["hffriedCount"] = Convert.ToInt32(ViewState["hffriedCount"]) - lastOD[i].ItemQuantity;
                    break;
                case "35":
                    if (strState == "B")
                        ViewState["breadCount"] = Convert.ToInt32(ViewState["breadCount"]) + lastOD[i].ItemQuantity;
                    else if (strState == "D")
                        ViewState["breadCount"] = Convert.ToInt32(ViewState["breadCount"]) - lastOD[i].ItemQuantity;
                    break;
                case "36":
                    if (strState == "B")
                        ViewState["hfbreadCount"] = Convert.ToInt32(ViewState["hfbreadCount"]) + lastOD[i].ItemQuantity;
                    else if (strState == "D")
                        ViewState["hfbreadCount"] = Convert.ToInt32(ViewState["hfbreadCount"]) - lastOD[i].ItemQuantity;
                    break;
                default:
                    break;
            }
        }
    } 
 

    public void addMIList( Menu mi, string strState)
    {
        if (Session["MIList"] != null)
            miList = (List<Menu>) Session["MIList"];
        else
            miList = new List<Menu>();

        if (miList.Count == 0)
            miList.Add(mi);
        else
        {
            int index = miList.FindIndex(item => item.Code.Substring(3, 2) == mi.Code.Substring(3, 2));
            if (index >= 0)
            {
                if (strState == "B" || strState == "R")
                {
                    miList[index].Quantity = miList[index].Quantity + 1;
                    miList[index].Subtotal = miList[index].Subtotal + miList[index].Price;
                }
                else
                {
                    miList[index].Quantity = miList[index].Quantity - 1;
                    miList[index].Subtotal = miList[index].Subtotal - miList[index].Price;
                }
            }
            else
                miList.Add(mi);
        }
    }

    // If simply return sessionMenuList[index] or thisMenu = sessionMenuList[indx], the sessionMenuList will be modified.
    public Menu getMenuItem( string barcode)
    {
        Menu thisMenu = new Menu();
        sessionMenuList = (List<Menu>)Session["MenuList"];
        int index = sessionMenuList.FindIndex(item => item.Code == barcode);
        if (index >= 0)
        {
            thisMenu.Code = sessionMenuList[index].Code;
            thisMenu.ID = sessionMenuList[index].ID;
            thisMenu.Description = sessionMenuList[index].Description;
            thisMenu.Price = sessionMenuList[index].Price;
            thisMenu.Quantity = sessionMenuList[index].Quantity;
            thisMenu.Subtotal = sessionMenuList[index].Subtotal;
            return thisMenu;
        }
        else
            return null;           
    }

    protected bool hasMoneytoBuy(decimal balance, Menu theMenu)
    {
        try {
            if (balance - theMenu.Price >= (decimal)0.00)
                return true; 
            else
                return false;
        }
        catch (Exception ex)
        {
            string errormessage = String.Format("Error: {0} StackTrace: {1}", ex.Message, ex.StackTrace);
            return false;
        }
    }

    
   
    public List<OrderDetail> prepareODList(List<Menu> mList)
    {
        List<OrderDetail> aODList = new List<OrderDetail>();

        if (mList.Count > 0)
        {
            for (int i = 0; i < mList.Count; i++)
            {
                OrderDetail od = new OrderDetail();
                od.ItemID = mList[i].ID;
                od.ItemQuantity = mList[i].Quantity;
                od.OrderID = Convert.ToInt32(Session["OrderID"]);
                aODList.Add(od);
            }
        }
        else
            aODList = null;
        return aODList;
    }
      
    protected void txtBarcode_TextChanged(object sender, EventArgs e)
    {
		List<string> UserRoles = (List<string>)Session["SessionRoles"];
        if (!UserRoles.Contains("MCOS_USER") && !UserRoles.Contains("MCOS_ADMIN")){
            txtBarcode.Focus();
            CleanSession();
			CleanScreen();
            ResetDataList();
			//this.txtBarcode.TextChanged -= txtBarcode_TextChanged;
			lblStatus.Text = "Error. Not Authorized.";
			return;
		}
		
 		Stopwatch stopWatch = new Stopwatch();
		stopWatch.Start();
        String barcode = txtBarcode.Text.ToUpper();

        statusMessage.Text = "";
        if (Session["MIList"] != null)
            miList = (List<Menu>)Session["MIList"];
      
        String firstBarcode = "";
 
        if (barcode.Length == 1)
        {
            firstBarcode = barcode.Substring(0, 1);            

            if (firstBarcode == "R" || firstBarcode == "r")  // Return Mode
            {
                if (Session["MemberSessionCode"] == null || Session["CheckOut"] != null)
                {
                    lblStatus.ForeColor = System.Drawing.Color.Red;
                    lblStatus.Text = "Error. Please Scan Meal Card first.";                   
                }
                else if ( miList.Count > 0)
                {
                    lblStatus.ForeColor = System.Drawing.Color.Red;
                    lblStatus.Text = "Error. In Purchase Mode. Can't return.";    
                }
                else
                {
                    Session["Return"] = "Return";               
         
                    var anOrder = manager.SelectOrdersByMemberIDToday(Convert.ToInt32(Session["MemberID"]));
                    GridViewReturnOrder.DataSource = anOrder;
                    GridViewReturnOrder.DataBind();

                    List<Order> rOrder = manager.SelectAllOrdersByMemberIDToday(Convert.ToInt32(Session["MemberID"]));
                    List<int> oIDList = rOrder.Select(o => o.OrderID).Distinct().ToList();
                    List<OrderDetail> ROD = manager.SelectOrderDetailsByOrderID(oIDList);
                    countVSLastOrdersItems(ROD);
                    Session["LastODList"] = ROD;

                    var lROD = manager.SelectOrderDetailsJoinItemMenuByOID(oIDList);
                    GridViewReturnOD.DataSource = lROD;
                    GridViewReturnOD.DataBind();
                    
                    lblStatus.ForeColor = System.Drawing.Color.Blue;
                    lblStatus.Text = " Returning...";
                }
            }
            else if (firstBarcode == "M" || firstBarcode == "m")
            {
                ResetDataList();
                CleanSession();
                Response.Redirect(Request.RawUrl);
            }
            else if (firstBarcode == "C" || firstBarcode == "c")
            {
                if (Session["CheckOut"] != null)
                {
                    lblStatus.ForeColor = System.Drawing.Color.Red;
                    lblStatus.Text = "Error: already Checked Out.";                    
                }
                else
                { 
                    if (Session["MemberSessionCode"] == null )
                    {
                        lblStatus.ForeColor = System.Drawing.Color.Red;
                        lblStatus.Text = "Error. Please scan Meal Card first.";
                    }
                    else if (miList == null || miList.Count == 0)
                    {
                        lblStatus.ForeColor = System.Drawing.Color.Red;
                        lblStatus.Text = "Error: Empty Order.";
                    }
                    else
                    {
                        miList.Sort(delegate(Menu x, Menu y)
                        {
                            return x.ID.CompareTo(y.ID);
                        });

                        List<OrderDetail> theODList = new List<OrderDetail>();
                        theODList = prepareODList(miList);
                        manager.InsertOrderDetailList(theODList);
 
                        if (!manager.UpdateBalance(Convert.ToInt32(Session["FamilyID"]), Convert.ToDecimal(Session["Total"])*-1))
                        {
                            lblStatus.ForeColor = System.Drawing.Color.Red;
                            lblStatus.Text = " Not enough money to check out, Please re-do.";
                        }
                        else
                        {   
                            manager.UpdateOrderByOrderID(Convert.ToInt32(Session["OrderID"]), Convert.ToDecimal(Session["Total"]), Session["SessionOperator"].ToString());

                            var myCheckOut = manager.SelectOrderJoinMemberByOID(Convert.ToInt32(Session["OrderID"]));
                            gvOrderMember.DataSource = myCheckOut;
                            gvOrderMember.DataBind();

                            Session["Balance"] = gvOrderMember.Rows[0].Cells[4].Text;

                            GridViewItemDetail.DataSource = miList;
                            GridViewItemDetail.DataBind();

                            if (Session["FamilyEmail"] != null){
								//lblError.Text += "Before call send,";
								//SendEmail(Session["FamilyEmail"].ToString());
								HostingEnvironment.QueueBackgroundWorkItem(ct => SendEmail(Session["FamilyEmail"].ToString()));
								//lblError.Text += "after call send,";
                            }else{
                               statusMessage.Text = "You haven’t set up your email yet. Please set up your email with Admin.";
							}
                            lblTotal.Text = Session["total"].ToString();
                            lblBalance.Text = Session["Balance"].ToString();
                            lblStatus.ForeColor = System.Drawing.Color.YellowGreen;
                            lblStatus.Text = "Your order is complete.";
                            Session["Checkout"] = "CheckOut";
                        }
                    }                
                }
            }            
            Cleanbarcode();
        }
        else if (barcode.Length > 1)
        {
            firstBarcode = barcode.Substring(0, 1);
       
            switch (firstBarcode)
            {
                case "M":
                    ResetDataList();
                    CleanSession();
                    CleanScreen();

                    if (manager.IsMemberExistByMemberCode(barcode))
                    {
                        Member member = manager.GetMember(barcode);

                        if (manager.IsFamilyIDExist(member.Family.FAMILY_ID))
                        {
                            manager.InsertOrder(member.MEMBER_ID);
                            order = (Order)manager.SelectOrderbyMemberID(member.MEMBER_ID);
                            Session["OrderID"] = order.OrderID;

                            var myQuery = manager.SelectOrderJoinMemberByOID(Convert.ToInt32(Session["OrderID"]));
                            gvOrderMember.DataSource = myQuery;
                            gvOrderMember.DataBind();

                            if (gvOrderMember.Rows[0].Cells[3].Text != null)
                            {    
                                Session["FamilyEmail"] = member.Family.EMAIL;                               
                                Session["Balance"] = member.Family.Balance;
                                getBalance(null); 

                                string pictureCode = member.Family.FamilyPicture;
                                PICURL_Value = Session["PICURL_Value"].ToString();
                                string familyPicUrl = PICURL_Value + pictureCode;
                                (this.Master).ImagePath = familyPicUrl; 
                               
                                Session["MemberSessionCode"] = barcode;
                                Session["MemberName"] = member.FIRST_NAME + " " + member.LAST_NAME;
                                Session["FamilyID"] = member.Family.FAMILY_ID;
                                Session["MemberID"] = member.MEMBER_ID;                               

                                ViewState["breadCount"] = 0;
                                ViewState["friedCount"] = 0;
                                ViewState["deluxeCount"] = 0;
                                ViewState["deluxeFriedRiceCount"] = 0;
                                ViewState["hfbreadCount"] = 0;
                                ViewState["hffriedCount"] = 0;
                                ViewState["hfdeluxeCount"] = 0;
                                ViewState["hfdeluxeFriedRiceCount"] = 0;

                                if (Convert.ToDecimal(Session["Balance"]) < (decimal)1.25)
                                {
                                    lblStatus.ForeColor = System.Drawing.Color.Red;
                                    lblStatus.Text = "Error: Your balance is too low.";
                                }
                                else
                                {
                                    lblStatus.ForeColor = System.Drawing.Color.Black;
                                    lblStatus.Text = "Purchase or Return? ";
                                }
                            }
                            else
                            {
                                lblStatus.ForeColor = System.Drawing.Color.Orange;
                                statusMessage.Text = "You haven’t set up your email yet. Please set up your email with Admin.";
                            }
                        }
                        else
                        {
                            lblStatus.ForeColor = System.Drawing.Color.Orange;
                            statusMessage.Text = " Your Family Account could not be found. Please ask Admin for help. ";
                        }
                    }
                    else
                    {
                        lblStatus.ForeColor = System.Drawing.Color.Orange;
                        statusMessage.Text = "Your Member Account could not be found. Please ask Admin for help.";
                    }
                   
                    Cleanbarcode();
                    break;

                case "B": // Buy                    
                    if (Session["MemberSessionCode"] == null || Session["CheckOut"] != null)
                    {
                        lblStatus.ForeColor = System.Drawing.Color.Red;
                        lblStatus.Text = "Error. Please scan Meal Card first.";
                    }
                    else
                    { 
                        menuitem = getMenuItem(barcode);

                        if (menuitem == null)  // did not find code in the menulist
                        {
                            lblStatus.ForeColor = System.Drawing.Color.Red;
                            lblStatus.Text = "Error: Invalid Menu Barcode.";
                        }
                        else
                        {
                            if (Session["Return"] != null)  // Return Mode
                            {
                                lblStatus.ForeColor = System.Drawing.Color.Red;
                                lblStatus.Text = "Error: In Return Mode. Can't buy.";
                            }
                            else if (sessionMenuList != null)  // Buy
                            {
                                if (hasMoneytoBuy(Convert.ToDecimal(Session["Balance"]), menuitem))
                                {
                                    addMIList(menuitem, "B");
                                    getVSOrderQuantity(true, barcode);
                                    getTotalPrice(menuitem);
                                    getBalance(menuitem);

                                    lblStatus.ForeColor = System.Drawing.Color.Black;
                                    lblStatus.Text = " Purchasing...";
                                }
                                else
                                {
                                    lblStatus.ForeColor = System.Drawing.Color.Red;
                                    lblStatus.Text = "Error: Your balance is too low.";
                                }

                                Session["MIList"] = miList;
                                GridViewItemDetail.DataSource = miList;
                                GridViewItemDetail.DataBind();
                            }
                            else
                                statusMessage.Text = "Please Reset the MCOS, make sure the Menu List is displayed in home page.";
                        }
                    }
                    Cleanbarcode();
                    break;

                case "D": // Delete/Return  
                    if (Session["CheckOut"] != null || Session["MemberSessionCode"] == null)
                    {
                        lblStatus.ForeColor = System.Drawing.Color.Red;
                        lblStatus.Text = "Error. Please scan Meal Card first.";
                    }                     
                    else
                    {
                        menuitem = getMenuItem(barcode);

                        if (menuitem == null)  // did not find code in the menulist
                        {
                            lblStatus.ForeColor = System.Drawing.Color.Red;
                            lblStatus.Text = "Error: Invalid Menu Barcode.";
                        }
                        else
                        {
                            List<OrderDetail> theODList = new List<OrderDetail>();

                            if (Session["Return"] != null)  // Return Mode
                                theODList = (List<OrderDetail>)Session["LastODList"];
                            else                            // Purchase Mode
                            {    
                                if (miList != null)
                                    theODList = prepareODList(miList);
                            }

                            if (theODList == null || miList == null)
                            {
                                lblStatus.ForeColor = System.Drawing.Color.Red;
                                lblStatus.Text = "Error: No item to remove.";
                            }
                            else
                            {
                                if (IsReturnItemExist(menuitem, theODList))
                                {
                                    getTotalPrice(menuitem);
                                    getBalance(menuitem);
                                    getVSOrderQuantity(false, barcode);
                                    lblStatus.ForeColor = System.Drawing.Color.Orange;

                                    if (Session["Return"] != null)
                                    {
                                        addMIList(menuitem, "R");
                                        lblStatus.Text = "Returning...Item returned.";
                                    }
                                    else
                                    {
                                        addMIList(menuitem, "D");
                                        lblStatus.Text = "Purchasing...Item removed.";
                                    }
                                }
                                else
                                {
                                    lblStatus.ForeColor = System.Drawing.Color.Red;
                                    if (Session["Return"] == null)
                                        lblStatus.Text = "Error: No more item to remove.";
                                    else
                                        lblStatus.Text = "Error: No more item to return.";
                                }

                                if (miList != null)
                                    miList.RemoveAll(x => x.Quantity == 0);

                                GridViewItemDetail.DataSource = miList;
                                GridViewItemDetail.DataBind();

                                Session["MIList"] = miList;
                            }
                        }
                    }
           
                    Cleanbarcode(); 
                    break;
              
                default:
                     lblStatus.ForeColor = System.Drawing.Color.Red;                          
                     lblStatus.Text = "Error: Invalid Barcode.";
                    break;
            }
        }
   		stopWatch.Stop();
		ElapsedTimeLabel.Text = String.Format("Response time: {0}", stopWatch.ElapsedMilliseconds / 1000.0);
}

    protected void btnB0031_Click(object sender, EventArgs e)
    {
        txtBarcode.Text = "B0031";
        txtBarcode_TextChanged(sender, e);
    }

    protected void btnB0033_Click(object sender, EventArgs e)
    {
        txtBarcode.Text = "B0033";
        txtBarcode_TextChanged(sender, e);
    }

    protected void btnB0035_Click(object sender, EventArgs e)
    {
        txtBarcode.Text = "B0035";
        txtBarcode_TextChanged(sender, e);
    }

    protected void btnC_Click(object sender, EventArgs e)
    {
        txtBarcode.Text = "C";
        txtBarcode_TextChanged(sender, e);
    }
}


//public void printOrderItemsNumber()
//{
//    statusMessage.Text = "Bread#, Fried#, Deluxe#, hfBread#, hfFried#, hfDeluxe#: "
//                + ViewState["breadCount"].ToString() + ", "
//                + ViewState["friedCount"].ToString() + ", "
//                + ViewState["deluxeCount"].ToString() + ", "
//                + ViewState["hfbreadCount"].ToString() + ", "
//                + ViewState["hffriedCount"].ToString() + ", "
//                + ViewState["hfdeluxeCount"].ToString();
//}

//protected void getOrderDetails(OrderDetail orderDetail, bool AddOrReturn, int oID, Menu mItem)
//{

//    orderDetail.OrderID = oID;
//    orderDetail.ItemID = mItem.ID;      

//    if (AddOrReturn) // Add
//        orderDetail.ItemQuantity = 1;
//    else         
//        orderDetail.ItemQuantity = -1;        
//}  