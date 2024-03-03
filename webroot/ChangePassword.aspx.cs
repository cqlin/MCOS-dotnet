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

public partial class ChangePassword : System.Web.UI.Page
{
    AccessData manager = new AccessData();
    string PICURL_Value;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            List<Param> PList = new List<Param>();
            PList = (List<Param>)Session["ParamList"];

            manager.getParamValueByParamName(PList, "PICURL", out PICURL_Value);
            Session["PICURL_Value"] = PICURL_Value;
            string familyPicUrl = PICURL_Value + "F0000.jpg";
            (this.Master).ImagePath = familyPicUrl;
		}		
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    { 
        try
        {
			string user = (string)Session["SessionUser"];
            if (txtPassword1.Text == txtPassword2.Text)
            {
                if(manager.changePassword(user, txtPasswordOld.Text, txtPassword1.Text) > 0)
                    statusMessage.Text = "Password changed successfully.";              
                else
                    statusMessage.Text = "Error: Wrong old password.";              
            }
            else
            {
                statusMessage.Text = "Error: New passwords don't match.";
            }
        }
        catch (Exception ex)
        {
            string errormessage = String.Format("Error: {0} StackTrace: {1}", ex.Message, ex.StackTrace);
			statusMessage.Text = errormessage;
        }        
     }      
}
 