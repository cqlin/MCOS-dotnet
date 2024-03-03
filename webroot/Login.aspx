<%@ Page Language="C#" MasterPageFile="~/MasterPages/MCOSMasterPage.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MenuContentPlaceHolder" runat="Server">
    <div class="row" style="background: radial-gradient(white, rgb(16, 160, 48)); background: -moz-radial-gradient(white, rgb(16, 160, 48))">
        <div class="col-sm-4"></div>
        <div class="col-sm-4" style="background-color: rgb(224, 255, 192)">
            <h2 style="font-variant: normal">Please login</h2><br />
                <div class="form-group">
                    <label for="txtUserName">User Name:</label>
                    <asp:TextBox ID="txtUserName" class="form-control" placeholder="Enter user name" runat="server"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label for="txtPassword">Password:</label>
                    <asp:TextBox ID="txtPassword" class="form-control" placeholder="Enter password" runat="server" TextMode="Password"></asp:TextBox>
                </div>
                <asp:Label ID="statusMessage" runat="server"></asp:Label>
                <asp:Label ID="errorMessage" runat="server"></asp:Label><br />
                <asp:Button ID="submit" class="btn btn-default btn-lg" Style="font-weight: bold; margin-bottom: 30px; float: right; width: 120px; text-align: left" runat="server" Text="Login" OnClick="btnLogin_Click"></asp:Button>
        </div>
        <div class="col-sm-4"></div>
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