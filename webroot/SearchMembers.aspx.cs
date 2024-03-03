using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
  

public partial class SearchMembers : System.Web.UI.Page
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
		List<string> UserRoles = (List<string>)Session["SessionRoles"];
		if (!UserRoles.Contains("MCOS_ADMIN"))
		{
			this.txtBarcode.TextChanged -= txtBarcode_TextChanged;
			lblStatus.ForeColor = System.Drawing.Color.Red;
			lblStatus.Text = "Unauthorized.";
			return;
		}
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
        txtBarcode.Focus();
    }

    protected void CleanSession()
    {
    }

    protected void CleanScreen()
    {
        lblStatus.Text = "";

        gvMemberList.DataSource = null;
        gvMemberList.DataBind();
    }

    protected void CleanFamilyPicture()
    {
        PICURL_Value = Session["PICURL_Value"].ToString();
        string familyPicUrl = PICURL_Value + "F0000.jpg";
        (this.Master).ImagePath = familyPicUrl;
    }

    protected void gvMemberList_RowDataBound(object o, GridViewRowEventArgs e)
    {
    }

    protected void txtBarcode_TextChanged(object sender, EventArgs e)
    {
        String barcode = txtBarcode.Text;

        CleanSession();
        CleanScreen();

        if ((barcode.Length == 10) && (IsDecimal(barcode)))
        {
            String phone1 = barcode.Substring(0, 3);
            String phone2 = barcode.Substring(3, 3);
            String phone3 = barcode.Substring(6, 4);
            String phone = "(" + phone1 + ") " + phone2 + "-" + phone3;

            if (manager.IsFamilyExistByPhoneNumber(phone))
            {
                Family family = manager.GetFamily(phone);

                string pictureCode = family.FamilyPicture;
                PICURL_Value = Session["PICURL_Value"].ToString();
                string familyPicUrl = PICURL_Value + pictureCode;
                (this.Master).ImagePath = familyPicUrl;

                var aMemberList = manager.SelectMemberListByFID(family.FAMILY_ID);
                gvMemberList.DataSource = aMemberList;
                gvMemberList.DataBind();

                lblStatus.ForeColor = System.Drawing.Color.Black;
                lblStatus.Text = "Family Found by Phone Number " + phone;
                Cleanbarcode();
            }
            else
            {
                lblStatus.ForeColor = System.Drawing.Color.Red;
                lblStatus.Text = "Your Member Account could not be found by " + phone + ". Please ask Admin for help.";
                CleanFamilyPicture();
            }
        }
        else
        {
            lblStatus.ForeColor = System.Drawing.Color.Red;
            lblStatus.Text = "Please enter 10 digits of telephone number without any other characters.";
            CleanFamilyPicture();
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
 