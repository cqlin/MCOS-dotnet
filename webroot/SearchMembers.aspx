<%@ Page Title="Search Members" Language="C#" MasterPageFile="~/MasterPages/MenuMasterPage.master" AutoEventWireup="true" CodeFile="SearchMembers.aspx.cs" Inherits="SearchMembers" %>
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

    <h2>Use Phone Number to Search Members</h2><br/>

    <label>Phone Number:</label> 
    <asp:TextBox runat="server" style="width:200px; height:40px;" Font-Size="X-Large" id="txtBarcode" OnTextChanged="txtBarcode_TextChanged" CssClass="textbox" BackColor="#efefef"></asp:TextBox> (10 digits)<br/><br/> 
    <asp:Label ID="lblStatus" runat="server"></asp:Label><br/><br/>
     
    <asp:GridView ID="gvMemberList" runat="server" OnRowDataBound="gvMemberList_RowDataBound" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical">
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
    <br/>
     
</asp:Content>

