﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPages_MenuMasterPage : System.Web.UI.MasterPage
{
    AccessData manager = new AccessData();
    public string ImagePath
    {
        get { return imageFamily.ImageUrl; }
        set { imageFamily.ImageUrl = value; }
    }

    protected void Page_Init(object sender, EventArgs e)
    {
        if (Session["SessionRoles"] == null)
			Response.Redirect("login.aspx");        
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["SessionRoles"] != null)
        {
            List<string> UserRoles = (List<string>)Session["SessionRoles"];
    
            System.Web.UI.WebControls.MenuItem menuOrder   = (System.Web.UI.WebControls.MenuItem) Menu1.Items[1];
            System.Web.UI.WebControls.MenuItem menuDeposit = (System.Web.UI.WebControls.MenuItem) Menu1.Items[2];
            System.Web.UI.WebControls.MenuItem menuSReport = (System.Web.UI.WebControls.MenuItem) Menu1.Items[3];
            System.Web.UI.WebControls.MenuItem menuCReport = (System.Web.UI.WebControls.MenuItem) Menu1.Items[4];
            System.Web.UI.WebControls.MenuItem menuSearch  = (System.Web.UI.WebControls.MenuItem) Menu1.Items[5];
            System.Web.UI.WebControls.MenuItem menuAccount = (System.Web.UI.WebControls.MenuItem) Menu1.Items[6];
            System.Web.UI.WebControls.MenuItem menuAddUser = (System.Web.UI.WebControls.MenuItem) Menu1.Items[7];

            if (!UserRoles.Contains("MCOS_DEPOSIT") && !UserRoles.Contains("MCOS_ADMIN"))
                Menu1.Items.Remove(menuDeposit);
            if (!UserRoles.Contains("MCOS_USER") && !UserRoles.Contains("MCOS_ADMIN"))
                Menu1.Items.Remove(menuOrder);
            if (!UserRoles.Contains("MCOS_ADMIN"))
            {
                Menu1.Items.Remove(menuSReport);
                Menu1.Items.Remove(menuCReport);
				Menu1.Items.Remove(menuAddUser);
            }
            if (!UserRoles.Contains("MCOS_DEPOSIT") && !UserRoles.Contains("MCOS_ADMIN"))
                Menu1.Items.Remove(menuSearch);
            if (!UserRoles.Contains("MCOS_DEPOSIT") && !UserRoles.Contains("MCOS_ADMIN") && !UserRoles.Contains("MCOS_ECARD"))
                Menu1.Items.Remove(menuAccount);
			if (Session["ParamList"] == null)
			{
				var ParamList = manager.SelectAllParameters();
				Session["ParamList"] = (List<Param>)ParamList;
			}
        }
        else
            Response.Redirect("login.aspx");        
    }     
}
