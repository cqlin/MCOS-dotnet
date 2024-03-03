<%@ Page Title="Deposit" Language="C#" MasterPageFile="~/MasterPages/MenuMasterPage.master" AutoEventWireup="true" CodeFile="NewDeposit.aspx.cs" Inherits="NewDeposit" %>
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

     <h2>Deposit: &nbsp;&nbsp;&nbsp;&nbsp; Message:<asp:Label ID="lblStatus" runat="server"></asp:Label> </h2><br/>
    

     <label>Scan Barcode Here:</label> 
     <asp:TextBox runat="server" style="width:150px; height:40px;" Font-Size="X-Large" 
            id="txtBarcode" OnTextChanged="txtBarcode_TextChanged" CssClass="textbox" BackColor="#efefef"></asp:TextBox><br/><br/> 
     
    <label><asp:Label ID="lblBeforeDeposit" runat="server"></asp:Label></label>  
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
          </asp:GridView><br/>
           
    <label>Deposit Amount: </label> 
             
    <asp:TextBox ID="txtDepositAmount" style="width:100px; height:40px;" Font-Size="X-Large" runat="server" CssClass="textbox" BackColor="#efefef"></asp:TextBox>
   
    <asp:DropDownList ID="DropDownDepositType" style="width:75px; height:30px;" Font-Size="Large" runat="server">
                <asp:ListItem Text ="Cash" Value="Cash"></asp:ListItem>
                <asp:ListItem Text ="Check" Value="Check"></asp:ListItem>
            </asp:DropDownList>     
    <asp:Button ID="btnAdd" style="width:150px; height:40px;" Font-Size="X-Large" runat="server" Text="Add Deposit" OnClick="btnAddDeposit_Click" CssClass="textbox" BackColor="#efefef"/><br/> 
         
    <h2><label >&nbsp;&nbsp;&nbsp;&nbsp; </label><asp:Label ID="txtAfterDeposit" runat="server"></asp:Label></h2><br/> 
    
    <label><asp:Label ID="lblAfterDeposit" runat="server"></asp:Label></label> 
    <asp:GridView ID="gridNewDeposits" runat="server" OnRowDataBound="gridNewDeposits_RowDataBound" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical" >
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
   </asp:GridView><br/>
   
    <label ><asp:Label ID="lblDepositHistory" runat="server"></asp:Label> </label>
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
     
     
</asp:Content>

