<%@ Page Title="Lunch Order" Language="C#" MasterPageFile="~/MasterPages/MenuMasterPage.master" AutoEventWireup="true" CodeFile="LunchOrder.aspx.cs" Inherits="LunchOrder" %>
<%@ MasterType VirtualPath="~/MasterPages/MenuMasterPage.master" %>
 
 
<asp:Content ID="Content2" ContentPlaceHolderID="MCOSContentPlaceHolder" Runat="Server">

    <script>
        var warning = false;
        window.onbeforeunload = function () {
            if (warning) {
                return "You have made changes on this page that you have not yet confirmed. If you navigate away from this page you will lose your unsaved changes";
            }
        }

        $('form').submit(function () {
            window.onbeforeunload = null;
        });
    </script>

     <h2>Lunch Order->         
         Balance: $<asp:Label ID="lblBalance" runat="server" Text="0.00"></asp:Label>&nbsp;&nbsp;
          Total: $<asp:Label ID="lblTotal" runat="server" Text="0.00"></asp:Label>
    </h2><br />     
    <asp:Label ID="lblStatus" runat="server"/> <asp:Label ID="statusMessage" runat="server"  ></asp:Label><br/>
	 <asp:Label ID="lblError" runat="server"></asp:Label><br/>
	 <asp:Label ID="ElapsedTimeLabel" runat="server"></asp:Label><br/>
       
    <table>
        <tr>
            <td>Barcode</td>
            <td style="padding: 0px 10px 0px 10px;"><asp:TextBox class="textbox" runat="server" id="txtBarcode"  style="width:150px; height:40px;" Font-Size="X-Large" BorderStyle="Double" CssClass="textbox" BackColor="#efefef" ></asp:TextBox></td>
            <td>
                <asp:ImageButton ID="btnGO"    ImageUrl="Images\Go.png"    AlternateText="GO"       style="width:60px; height:60px;" runat="server" OnClick="txtBarcode_TextChanged"/>
                <asp:ImageButton ID="btnB0031" ImageUrl="Images\B0031.png" AlternateText="B0031"    style="width:60px; height:60px;" runat="server" OnClick="btnB0031_Click"/>
                <asp:ImageButton ID="btnB0033" ImageUrl="Images\B0033.png" AlternateText="B0033"    style="width:60px; height:60px;" runat="server" OnClick="btnB0033_Click"/>
                <asp:ImageButton ID="btnB0035" ImageUrl="Images\B0035.png" AlternateText="B0035"    style="width:60px; height:60px;" runat="server" OnClick="btnB0035_Click"/>
                <asp:ImageButton ID="btnC"     ImageUrl="Images\C.png"     AlternateText="Checkout" style="width:60px; height:60px;" runat="server" OnClick="btnC_Click"/>
            </td>
        </tr>
    </table>
 
    
     <asp:GridView ID="gvOrderMember" runat="server" OnRowDataBound="gvOrderMember_RowDataBound" 
         CellPadding="3" GridLines="Vertical" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px">
        <AlternatingRowStyle BackColor="#DCDCDC" />
        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#F1F1F1" />
        <SortedAscendingHeaderStyle BackColor="#0000A9" />
        <SortedDescendingCellStyle BackColor="#CAC9C9" />
        <SortedDescendingHeaderStyle BackColor="#000065" />
     </asp:GridView> <br/>
 

     <asp:GridView ID="GridViewItemDetail" runat="server" OnRowDataBound="GridViewItemDetail_RowDataBound" CellPadding="3" GridLines="Vertical" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px">
        <AlternatingRowStyle BackColor="#DCDCDC" />
        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#F1F1F1" />
        <SortedAscendingHeaderStyle BackColor="#0000A9" />
        <SortedDescendingCellStyle BackColor="#CAC9C9" />
        <SortedDescendingHeaderStyle BackColor="#000065" />
     </asp:GridView> <br/>

    
    
    <asp:GridView ID="GridViewReturnOrder" runat="server" OnRowDataBound="GridViewReturnOrder_RowDataBound" CellPadding="4" ForeColor="Black" GridLines="Vertical" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px">
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

    <asp:GridView ID="GridViewReturnOD" runat="server" OnRowDataBound="GridViewReturnOD_RowDataBound" CellPadding="4" ForeColor="Black" GridLines="Vertical" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px">
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

