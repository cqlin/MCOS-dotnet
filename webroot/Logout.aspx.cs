using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Logout : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["SessionOperator"] != null || Session["SessionRoles"] != null)
        {
            
            Session["SessionOperator"] = null;
            Session["SessionRoles"] = null;
            Session["MenuList"] = null; 

            /* Starts here */
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.Now.AddDays(-1));
            Response.Cache.SetNoStore();
            /* Ends here */           
         }  
         Response.Redirect("login.aspx");
     }
}