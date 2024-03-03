using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AddUser : System.Web.UI.Page
{
    AccessData manager = new AccessData();

    protected void Page_Load(object sender, EventArgs e)
    {
		List<string> UserRoles = (List<string>)Session["SessionRoles"];
		if (!UserRoles.Contains("MCOS_ADMIN"))
		{
			this.btnSubmit.Click -= btnSubmit_Click;
			lblStatus.Text = "Unauthorized.";
			return;
		}
        List<Param> PList = new List<Param>();
        PList = (List<Param>)Session["ParamList"];

        string PICURL_Value;
        manager.getParamValueByParamName(PList, "PICURL", out PICURL_Value);
		lblError.Text = "";
		lblStatus.Text = "";

        if (!IsPostBack)
        {
            string familyPicUrl = PICURL_Value + "F0000.jpg";
            (this.Master).ImagePath = familyPicUrl;
            PopulateDropDown();
        }    
 
    }

    //add a list of reports to allow user to select
    private void PopulateDropDown()
    {
        List<string> roleList = new List<string>();
        roleList.Add("MCOS_GUEST");
        roleList.Add("MCOS_ECARD");
        roleList.Add("MCOS_DEPOSIT");
        roleList.Add("MCOS_USER");
        roleList.Add("MCOS_ADMIN");
      
        ddRole.DataSource = roleList;
        ddRole.DataBind();
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
		try{
			List<string> UserRoles = (List<string>)Session["SessionRoles"];
			if (!UserRoles.Contains("MCOS_ADMIN"))
			{
				this.btnSubmit.Click -= btnSubmit_Click;
				return;
			}
			String memberId = member.Text;
			if(memberId.StartsWith("M"))
			{
				memberId = manager.GetMember(memberId).MEMBER_ID.ToString();
			}
			manager.addUser(username.Text,memberId,txtPassword.Text,ddRole.SelectedValue);
			lblStatus.Text = "User added:"+username.Text;
		}
		catch (Exception ex)
        {
            string errormessage = String.Format("Error: {0} StackTrace: {1}", ex.Message, ex.StackTrace);
			lblError.Text = errormessage;
        }        
 
    }

}