<%@ Page Title="My Account" Language="C#" MasterPageFile="~/MasterPages/MenuMasterPage.master" AutoEventWireup="true" CodeFile="MyAccount.aspx.cs" Inherits="MyAccount" %>
<%@ MasterType VirtualPath="~/MasterPages/MenuMasterPage.master" %>
 
<asp:Content ID="Content2" ContentPlaceHolderID="MCOSContentPlaceHolder" Runat="Server">

    <script>
        var warning = false;
        window.onbeforeunload = function () {
            if (warning) {
                return "You have made change on this page that you have not yet confirmed. If you navigate away from this page you will lose your unsaved changes";
            }
        }

        $('form').submit(function () {
            window.onbeforeunload = null;
        });
    </script>
    <h2>My Account -> Deposit Balance and Recent Transactions</h2>
    <asp:Label ID="lblStatus" runat="server"></asp:Label>
     
    <h3>Deposit Balance</h3>
    <asp:GridView ID="gvMemberInfo" runat="server" OnRowDataBound="gvMemberInfo_RowDataBound" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical">
            <AlternatingRowStyle BackColor="White" />
            <FooterStyle BackColor="#CCCC99" />
            <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
            <RowStyle BackColor="#F7F7DE" />
            <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#FBFBF2" />
            <SortedAscendingHeaderStyle BackColor="#848384" />
            <SortedDescendingCellStyle BackColor="#EAEAD3" />
            <SortedDescendingHeaderStyle BackColor="#575357" />
    </asp:GridView>
 
    <h3>Recent Deposit History</h3>
    <asp:GridView ID="gvDepositHistory" runat="server" OnRowDataBound="gvDepositHistory_RowDataBound" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical">
        <AlternatingRowStyle BackColor="White" />
        <FooterStyle BackColor="#CCCC99" />
        <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
        <RowStyle BackColor="#F7F7DE" />
        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#FBFBF2" />
        <SortedAscendingHeaderStyle BackColor="#848384" />
        <SortedDescendingCellStyle BackColor="#EAEAD3" />
        <SortedDescendingHeaderStyle BackColor="#575357" />
     </asp:GridView>

    <h3>Recent Transactions</h3>
    <asp:GridView ID="GridViewRecentOrder" runat="server" OnRowDataBound="GridViewRecentOrder_RowDataBound" CellPadding="4" ForeColor="Black" GridLines="Vertical" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px">
        <AlternatingRowStyle BackColor="White" />
        <FooterStyle BackColor="#CCCC99" />
        <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
        <RowStyle BackColor="#F7F7DE" />
        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#FBFBF2" />
        <SortedAscendingHeaderStyle BackColor="#848384" />
        <SortedDescendingCellStyle BackColor="#EAEAD3" />
        <SortedDescendingHeaderStyle BackColor="#575357" />
     </asp:GridView> <br/>      
     
</asp:Content>

