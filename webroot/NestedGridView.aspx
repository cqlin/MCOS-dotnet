<%@ Page Language="C#" MasterPageFile="~/MasterPages/MenuMasterPage.master" AutoEventWireup="true"  CodeFile="NestedGridView.aspx.cs" Inherits="NestedGridView"%>
 
<asp:Content ID="Content1" ContentPlaceHolderID="MCOSContentPlaceHolder" Runat="Server">

    <script type="text/javascript">
        $("[src*=plus]").live("click", function () {
            $(this).closest("tr").after("<tr><td></td><td colspan = '999'>" + $(this).next().html() + "</td></tr>")
            $(this).attr("src", "images/minus.gif");
        });
        $("[src*=minus]").live("click", function () {
            $(this).attr("src", "images/plus.gif");
            $(this).closest("tr").next().remove();
        });
</script>
   
        <asp:GridView ID="gvOrders" runat="server" AutoGenerateColumns="false" Width="600px"
             OnRowDataBound="gvOrders_OnRowDataBound">
            <Columns>
                <asp:TemplateField ItemStyle-Width="20px">
                    <ItemTemplate>
                        <a href="JavaScript:divexpandcollapse('div<%# Eval("OrderID") %>');">
                            <img id='imgdiv<%# Eval("OrderID") %>' width="9px" border="0" src="Images/plus.gif"
                                alt="" /></a>                        
                    </ItemTemplate>
                    <ItemStyle Width="20px" VerticalAlign="Middle"></ItemStyle>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Order ID">
                <ItemTemplate>
                 <asp:Label ID="lblorderID" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "OrderID") %>'></asp:Label>
                </ItemTemplate>
                </asp:TemplateField>                
                <asp:BoundField DataField="Member_ID" HeaderText="Customer ID" />
                <asp:BoundField DataField="MemberName" HeaderText="Member Name" />   
                <asp:BoundField DataField="CREATE_DATE" HeaderText="Order Date" />
                <asp:BoundField DataField="OrderAmount" HeaderText="Order Amount" />  
                <asp:TemplateField>
                    <ItemTemplate>
                        <tr>
                           <td colspan="100%" style="background:#F5F5F5">
                             <div id='div<%# Eval("OrderID") %>'  style="overflow:auto; display:none; position: relative; left: 15px; overflow: auto">
                               <asp:DetailsView id="DetailsView1" DataKeyNames="OrderID" Runat="server" Width="300px" Font-Names="Calibri"/>                               
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>              
            <HeaderStyle BackColor="#3E3E3E" ForeColor="White" Font-Names="Calibri"/>
            <RowStyle Font-Names="Calibri"/>
        </asp:GridView>   

</asp:Content>