<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MenuMasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>
<%@ MasterType VirtualPath="~/MasterPages/MenuMasterPage.master" %>
 
<asp:Content ID="Content1" ContentPlaceHolderID="MCOSContentPlaceHolder" Runat="Server">

    <h2>MCOS Home. <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label></h2><br />    
    
     <asp:GridView ID="gvMenu" runat="server" OnRowDataBound="gvMenu_RowDataBound" CellPadding="4" GridLines="Vertical" ForeColor="Black" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" >
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
 

</asp:Content>

