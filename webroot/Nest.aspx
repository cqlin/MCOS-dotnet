<%@ Page Language="C#" MasterPageFile="~/MasterPages/MenuMasterPage.master" AutoEventWireup="true"  CodeFile="Nest.aspx.cs" Inherits="Nest"%>
 <%@ MasterType VirtualPath="~/MasterPages/MenuMasterPage.master" %>
  
<asp:Content ID="Content1" ContentPlaceHolderID="MCOSContentPlaceHolder" Runat="Server">

    <script type="text/javascript">
        $(function () {
            $("[id$=txtDate]").datepicker({
                showOn: 'button',
                buttonImageOnly: true,
                buttonImage: 'images/calendar4.png'
            });
        });
    </script>

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

 <h2>Combine Report</h2><br/>

<asp:TextBox ID="txtDate" runat="server" type="text"  style="width:150px; height:30px;" Font-Size="Large"  CssClass="textbox" BackColor="#efefef"></asp:TextBox>
<asp:Button ID="btnSubmit" runat="server" Text="Submit" style="width:100px; height:30px;" Font-Size="Large" onclick="btnSubmit_Click" CssClass="textbox" BackColor="#efefef"/><br/><br/>  

  

<asp:GridView ID="gvOrders" runat="server" AutoGenerateColumns="False" CssClass="Grid"
    DataKeyNames="OrderID" OnRowDataBound="gvOrders_OnRowDataBound" 
    CellPadding="3" GridLines="Vertical" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px">
    <AlternatingRowStyle BackColor="#DCDCDC" />
    <Columns>
        <asp:TemplateField>
            <ItemTemplate>
                <img alt = "" style="cursor: pointer" src="images/plus.png" />
                <asp:Panel ID="pnlOrders" runat="server" Style="display: none">
                    <%--<asp:GridView ID="gvOrderDetails" runat="server" AutoGenerateColumns="false" CssClass = "ChildGrid">
                        <Columns>
                            <asp:BoundField ItemStyle-Width="150px" DataField="OrderID" HeaderText="Order ID" />
                            <asp:BoundField ItemStyle-Width="150px" DataField="ItemCode" HeaderText="Item Code" />
                            <asp:BoundField ItemStyle-Width="150px" DataField="Description" HeaderText="Description" />
                            <asp:BoundField ItemStyle-Width="150px" DataField="Quantity" HeaderText="Quantity" />
                        </Columns>
                    </asp:GridView>--%>

                     <asp:GridView ID="gvOrderDetails" runat="server" AutoGenerateColumns="False" CssClass = "ChildGrid" CellPadding="4" ForeColor="#333333" GridLines="None">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:BoundField ItemStyle-Width="150px" DataField="OrderID" HeaderText="Order ID" >
                            <ItemStyle Width="150px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField ItemStyle-Width="150px" DataField="ItemCode" HeaderText="Item Code" >
                            <ItemStyle Width="150px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField ItemStyle-Width="250px" DataField="Description" HeaderText="Description" >
                            <ItemStyle Width="250px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField ItemStyle-Width="50px" DataField="Quantity" HeaderText="Quantity" >
                            <ItemStyle Width="50px"></ItemStyle>
                            </asp:BoundField>
                        </Columns>
                        <EditRowStyle BackColor="#2461BF" />
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#EFF3FB" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#F5F7FB" />
                        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                        <SortedDescendingCellStyle BackColor="#E9EBEF" />
                        <SortedDescendingHeaderStyle BackColor="#4870BE" />
                    </asp:GridView>

                    
                </asp:Panel>
            </ItemTemplate>
        </asp:TemplateField>
                <asp:BoundField DataField="OrderID" HeaderText="Order ID" />
                <asp:BoundField DataField="MemberName" HeaderText="MemberName" />   
                <asp:BoundField DataField="CREATE_DATE" HeaderText="OrderDate" />
                <asp:BoundField DataField="OrderAmount" HeaderText="OrderAmount" />  
    </Columns> 
        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#F1F1F1" />
        <SortedAscendingHeaderStyle BackColor="#0000A9" />
        <SortedDescendingCellStyle BackColor="#CAC9C9" />
        <SortedDescendingHeaderStyle BackColor="#000065" />
</asp:GridView>  
      
</asp:Content>