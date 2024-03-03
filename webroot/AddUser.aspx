<%@ Page Language="C#" MasterPageFile="~/MasterPages/MenuMasterPage.master" AutoEventWireup="true" CodeFile="AddUser.aspx.cs" Inherits="AddUser" %>
<%@ MasterType VirtualPath="~/MasterPages/MenuMasterPage.master" %>
 
<asp:Content ID="Content1" ContentPlaceHolderID="MCOSContentPlaceHolder" Runat="Server">
	<h2>Add Operator</h2> 
	<div class="form-group">
	<label for="username">Operator Login ID:</label>
	<asp:TextBox ID="username" class="form-control" placeholder="Enter user name" runat="server"></asp:TextBox>
	</div>
	<div class="form-group">
	<label for="member">Member ID:</label>
	<asp:TextBox ID="member" class="form-control" placeholder="Enter member ID" runat="server"></asp:TextBox>
	</div>
	<div class="form-group">
	<label for="txtPassword">Password:</label>
	<asp:TextBox ID="txtPassword" class="form-control" placeholder="Enter password" runat="server" TextMode="Password"></asp:TextBox>
	</div>
	<div class="form-group">
    <label for="role"> Role:  </label>
    <asp:DropDownList ID="ddRole" style="width:220px; height:30px;" Font-Size="Large" runat="server" CssClass="textbox" BackColor="#efefef"></asp:DropDownList>
	</div>
	<asp:Label ID="lblStatus" runat="server"></asp:Label>
	<asp:Label ID="lblError" runat="server"></asp:Label><br />
	<asp:Button ID="btnSubmit" class="btn btn-default btn-lg" Style="font-weight: bold; margin-bottom: 30px; float: right; width: 120px; text-align: middle" runat="server" Text="Submit" OnClick="btnSubmit_Click"></asp:Button>
  
</asp:Content>
