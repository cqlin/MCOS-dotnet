﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Text;
using System.Runtime.Serialization.Json;  
using Newtonsoft.Json;
 


public partial class Login : System.Web.UI.Page
{
    AccessData manager = new AccessData();

    Param thisParam = new Param();

    protected void Page_Load(object sender, EventArgs e)
    {
        // only select first element
        thisParam = manager.SelectFirstParameter();
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    { 
        try
        {
			string name;
			int member;
			/*
            if (txtUserName.Text.ToUpper() == "REMLUNCH" && txtPassword.Text == "REMLUNCH")
            {
                List<string> RoleNames = new List<string>();
                RoleNames.Add("MCOS_ADMIN");

                Session["SessionRoles"] = RoleNames;
                Session["SessionOperator"] = "David King";
                Session["SessionMemberID"] = "1";

                if (RoleNames == null)                
                    statusMessage.Text = "You don't have MCOS role to login.";              
                else
                    Server.Transfer("~/Default.aspx");
            }
			else
			*/
			if(manager.loginUser(txtUserName.Text,txtPassword.Text,out name,out member))
			{
				List<string> RoleNames = manager.getUserRole(txtUserName.Text);
                Session["SessionRoles"] = RoleNames;
                Session["SessionOperator"] = name;
                Session["SessionMemberID"] = member;
                Session["SessionUser"] = txtUserName.Text;
                statusMessage.Text = "Got:"+name+","+member;              
                if (!RoleNames.Any())                
                    statusMessage.Text = "You don't have MCOS role to login.";              
                else
					Server.Transfer("~/Default.aspx");
			}
            else
            {
                statusMessage.Text = "Failed to login: ";
                errorMessage.Text = "invalid password";         
            }

            /** CBCM integrated login
            string loginURL = thisParam.PARAM_VALUE + txtUserName.Text + "&password=" + txtPassword.Text;
            var syncClient = new WebClient();
            var content = syncClient.DownloadString(loginURL);
            //     statusMessage.Text = content.ToString();

            if (content.Contains("MESSAGE"))
            {
                LoginMessage objMsg = new LoginMessage();
                objMsg = JsonConvert.DeserializeObject<LoginMessage>(content);
                statusMessage.Text = objMsg.STATUS.ToString() + ": ";
                errorMessage.Text = objMsg.MESSAGE.ToString();         
            }
            else
            {
                LoginMemberData objUser = JsonConvert.DeserializeObject<LoginMemberData>(content);

                List<string> RoleNames = new List<string>();

                for (int i = 0; i < objUser.ROLE_NAME.Count; i++)
                {
                    if (objUser.ROLE_NAME[i].ToString() == "MCOS_ADMIN" || objUser.ROLE_NAME[i].ToString() == "MCOS_USER" ||
                        objUser.ROLE_NAME[i].ToString() == "MCOS_DEPOSIT" || objUser.ROLE_NAME[i].ToString() == "MCOS_ECARD")
                    {
                        RoleNames.Add(objUser.ROLE_NAME[i]);                       
                    }
                }

                var RoleResult = String.Join(", ", RoleNames.ToArray());
                //statusMessage.Text = RoleResult.ToString();

                Session["SessionRoles"] = RoleNames;
                Session["SessionOperator"] = objUser.USERNAME;
                Session["SessionMemberID"] = objUser.MEMBER_ID;

                if (RoleNames == null)                
                    statusMessage.Text = "You don't have MCOS role to login.";              
                else
                    Server.Transfer("~/Default.aspx");
            }
            */
        }
        catch (Exception ex)
        {
            string errormessage = String.Format("Error: {0} StackTrace: {1}", ex.Message, ex.StackTrace);            
        }        
     }      
}