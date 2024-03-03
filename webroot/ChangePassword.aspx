<%@ Page Title="Change Password" Language="C#" MasterPageFile="~/MasterPages/MenuMasterPage.master" AutoEventWireup="true" CodeFile="ChangePassword.aspx.cs" Inherits="ChangePassword" %>
<%@ MasterType VirtualPath="~/MasterPages/MenuMasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MCOSContentPlaceHolder" Runat="Server">

    <div class="row" style="width:750px">
		<h2 style="font-variant: normal">Change Password</h2><br />
		<div class="form-group">
			<label for="txtPasswordOld">Old Password:</label>
			<asp:TextBox ID="txtPasswordOld" class="form-control" placeholder="Enter password" runat="server" TextMode="Password"></asp:TextBox>
		</div>
		<div class="form-group">
			<label for="txtPassword1">New Password:</label>
			<asp:TextBox ID="txtPassword1" class="form-control" placeholder="Enter password" runat="server" TextMode="Password"></asp:TextBox>
		</div>
		<div class="form-group">
			<label for="txtPassword2">Confirm Password:</label>
			<asp:TextBox ID="txtPassword2" class="form-control" placeholder="Enter password" runat="server" TextMode="Password"></asp:TextBox>
		</div>
		<asp:Label ID="statusMessage" runat="server"></asp:Label>
		<asp:Label ID="errorMessage" runat="server"></asp:Label><br />
		<asp:Button ID="submit" class="btn btn-default btn-lg" Style="font-weight: bold; margin-bottom: 30px; float: right; width: 120px; text-align: middle" runat="server" Text="Submit" OnClick="btnSubmit_Click"></asp:Button>
    </div>
 	
    <script>
        var aspPrefix = "#MenuContentPlaceHolder_";
        function submitMouseout() {
            $(aspPrefix + "submit").css({
                'color': 'rgb(80, 124, 209)',
                'background-color': 'white',
                'background-image': 'url(images/login.png)',
                'background-repeat': 'no-repeat',
                'background-size': '20% 60%',
                'background-position': 'right 10px center'
            });
        }
        function submitMouseover() {
            $(aspPrefix + "submit").css({
                'color': 'white',
                'background-color': 'rgb(80, 124, 209)',
                'background-image': 'url(images/login-inv.png)',
                'background-repeat': 'no-repeat',
                'background-size': '20% 60%',
                'background-position': 'right 10px center'
            });
        }
        $(document).ready(function () {
            $(aspPrefix + "statusMessage").css({ 'color': 'red' });
            $(aspPrefix + "errorMessage").css({ 'color': 'red' });
            $(aspPrefix + "submit").mouseover(submitMouseover);
            $(aspPrefix + "submit").mouseout(submitMouseout);
            $(aspPrefix + "submit").click(function () {
                $(aspPrefix + "statusMessage").hide(); $(aspPrefix + "errorMessage").hide();
            });
            submitMouseout();
        });
    </script>     
</asp:Content>

