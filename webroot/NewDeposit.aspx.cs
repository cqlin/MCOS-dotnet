using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
  

public partial class NewDeposit : System.Web.UI.Page
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
            txtBarcode.Focus();
            CleanSession();
            CleanScreen();

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
    
            string familyPicUrl = PICURL_Value + "F0000.jpg";
            (this.Master).ImagePath = familyPicUrl;
        }
    }

    public void Cleanbarcode()
    {
        txtBarcode.Text = "";
        txtDepositAmount.Focus();
    }

    public void CleanAmount()
    {
        txtDepositAmount.Text = "";
        txtBarcode.Focus();
    }


    protected void CleanSession()
    {
        Session["MemberSessionCode"] = null;
        Session["MemberID"] = null;
        Session["FamilyID"] = null;
        Session["Balance"] = null;
        Session["Deposit"] = null;
        Session["DepositComplete"] = null;
    }

    protected void CleanScreen()
    {
        txtDepositAmount.Text = "";
        lblStatus.Text = "";
        txtAfterDeposit.Text = "";
        lblBeforeDeposit.Text = "";
        lblAfterDeposit.Text = "";
        lblDepositHistory.Text = "";

        gvMemberInfo.DataSource = null;
        gvMemberInfo.DataBind();

        gridNewDeposits.DataSource = null;
        gridNewDeposits.DataBind();

        gvDepositHistory.DataSource = null;
        gvDepositHistory.DataBind();
    }


    protected void gvMemberInfo_RowDataBound(object o, GridViewRowEventArgs e)
    {
        //Assumes the Price column is at index 2
        if (e.Row.RowType == DataControlRowType.DataRow)
            e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Right;
    }

    protected void gridNewDeposits_RowDataBound(object o, GridViewRowEventArgs e)
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

    protected void SendEmail(string fEmail)
    {
        MailMessage mail = new MailMessage();

        SMTPSRV_Value = Session["SMTPSRV_Value"].ToString();
        SMTPPORT_Value = Session["SMTPPORT_Value"].ToString();
        SMTPUSER_Value = Session["SMTPUSER_Value"].ToString();
        SMTPPASS_Value = Session["SMTPPASS_Value"].ToString();
        SMTPFROM_Value = Session["SMTPFROM_Value"].ToString();
        SMTPBCC_Value = Session["SMTPBCC_Value"].ToString();
        SMTPAUTH_Value = Session["SMTPAUTH_Value"].ToString();
        SMTP_DEPOSIT_SUBJECT_Value = Session["SMTP_DEPOSIT_SUBJECT_Value"].ToString();

        try
        {
            mail.To.Add(new MailAddress(fEmail));
            mail.From = new MailAddress(SMTPFROM_Value);
            mail.Bcc.Add(new MailAddress(SMTPBCC_Value));

            mail.Subject = SMTP_DEPOSIT_SUBJECT_Value + " " + DateTime.Today.Date.ToString("MM/dd/yyyy");

            string mName1 = Session["MemberName"].ToString();
            string mCode1 = Session["MemberSessionCode"].ToString();
            string strDeposit1 = Session["Deposit"].ToString();
            string strBalance1 = Session["Balance"].ToString();

            string strBody = PopulateBody(mName1, mCode1, strBalance1, strDeposit1);
            mail.Body = strBody;

            mail.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient();
            smtp.Host = SMTPSRV_Value;
            smtp.Port = Convert.ToInt32(SMTPPORT_Value);

            if (SMTPAUTH_Value == "Y")
                smtp.Credentials = new NetworkCredential(SMTPUSER_Value, SMTPPASS_Value);

            smtp.Send(mail);
        }

        catch (Exception ex)
        {
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

    private string PopulateBody(string mName, string mCode, string strBalance, string strDeposit)
    {
        string body = string.Empty;
        using (StreamReader reader = new StreamReader(Server.MapPath("~/DepositEmail.html")))
        {
            body = reader.ReadToEnd();
        }

        body = body.Replace("{MemberName}", mName);
        body = body.Replace("{MemberCode}", mCode);
        body = body.Replace("{Deposit}", strDeposit);
        body = body.Replace("{Balance}", strBalance);

        return body;
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
                            Session["MemberSessionCode"] = barcode;
                            Session["MemberID"] = member.MEMBER_ID;
                            Session["FamilyID"] = member.Family.FAMILY_ID;
                            Session["FamilyEmail"] = member.Family.EMAIL;
                            Session["MemberName"] = member.FIRST_NAME + " " + member.LAST_NAME;

                            string pictureCode = member.Family.FamilyPicture;
                            PICURL_Value = Session["PICURL_Value"].ToString();
                            string familyPicUrl = PICURL_Value + pictureCode;
                            (this.Master).ImagePath = familyPicUrl;

                            lblBeforeDeposit.Text = "Before Deposit: ";
                            var myFamily = manager.SelectMemberFamily(member.MEMBER_ID);
                            gvMemberInfo.DataSource = myFamily;
                            gvMemberInfo.DataBind();
                 
                            lblStatus.Text = " Your record balance is $" + member.Family.Balance + ".";
                            Session["BalanceBefore"] = member.Family.Balance;
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
        }
    }
  

    protected void btnAddDeposit_Click(object sender, EventArgs e)
    {
        if (Session["DepositComplete"] != null)
        {
            lblStatus.ForeColor = System.Drawing.Color.Red;
            lblStatus.Text = " Error: Deposit is already complete.";
        }
        else
        {
            if (Session["FamilyID"] == null)
            {
                lblStatus.ForeColor = System.Drawing.Color.Orange;
                lblStatus.Text = "Your Family Account could not be found. Please ask Admin for help. ";
            }
            else
            {
                if (!IsDecimal(txtDepositAmount.Text))
                {
                    lblStatus.ForeColor = System.Drawing.Color.Orange;
                    lblStatus.Text = " Please enter the correct format for Deposit.";
                    txtDepositAmount.Text = "";
                    txtDepositAmount.Focus();
                }
                else
                {
                    decimal dDeposit = decimal.Parse(txtDepositAmount.Text);
                    
                    if ((dDeposit > 200.00M) || (dDeposit < -200.00M))
                    {
                        lblStatus.ForeColor = System.Drawing.Color.Orange;
                        lblStatus.Text = " Enter Deposit Amount within the range -200 and +200.";
                        txtDepositAmount.Text = "";
                        txtDepositAmount.Focus();
                    } 
                    else
                    {
                        manager.InsertDepositHistory(Convert.ToInt32(Session["MemberID"]), Convert.ToInt32(Session["FamilyID"]), Decimal.Parse(txtDepositAmount.Text), DropDownDepositType.Text, Session["SessionOperator"].ToString());

                        if (!manager.UpdateBalance(Convert.ToInt32(Session["FamilyID"]), decimal.Parse(txtDepositAmount.Text)))
                        {
                            lblStatus.ForeColor = System.Drawing.Color.Red;
                            lblStatus.Text = " Not enough Money to withdraw. Please re-do. ";
                        }
                        else
                        {

                            var query = manager.SelectMemberFamily(Convert.ToInt32(Session["MemberID"]));
                            lblAfterDeposit.Text = "After Deposit: ";
                            gridNewDeposits.DataSource = query;
                            gridNewDeposits.DataBind();

                            txtAfterDeposit.Text = "$" + txtDepositAmount.Text + " added. Your Current Balance is $" +
                                                       gridNewDeposits.Rows[0].Cells[2].Text + ".";

                            Session["Balance"] = gridNewDeposits.Rows[0].Cells[2].Text;
                            Session["Deposit"] = txtDepositAmount.Text;

                            var dHistory = manager.SelectRecentFamilyAccountActivitiesByFID(Convert.ToInt32(Session["FamilyID"]));
                            lblDepositHistory.Text = "Deposit History: ";
                            gvDepositHistory.DataSource = dHistory;
                            gvDepositHistory.DataBind();

//                            if (Session["FamilyEmail"] != null)
//                                SendEmail(Session["FamilyEmail"].ToString());
//                            else
//                            {
//                                lblStatus.ForeColor = System.Drawing.Color.Black;
//                                lblStatus.Text = "You haven’t set up your email yet. Please set up your email with Admin.";
//                            }
                            lblStatus.ForeColor = System.Drawing.Color.YellowGreen;
                            lblStatus.Text = " Your Deposit is complete.";
                            Session["DepositComplete"] = "DepositComplete";
                            CleanAmount();
                        }
                    }
                }
            }
        }
    }


    public bool IsDecimal(string stringInput)
    {
        try
        {
            Decimal.Parse(stringInput);
            return true;
        }
        catch
        {
            return false;
        }
    }
}


   


 