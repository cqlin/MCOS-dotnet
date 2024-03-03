<%@ Page Language="C#" MasterPageFile="~/MasterPages/MenuMasterPage.master" AutoEventWireup="true" CodeFile="Report.aspx.cs" Inherits="Report" %>
<%@ MasterType VirtualPath="~/MasterPages/MenuMasterPage.master" %>
 
<asp:Content ID="Content1" ContentPlaceHolderID="MCOSContentPlaceHolder" Runat="Server">

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
    <h2>Sunday Reports</h2> 
    <h3>Please select Report type and click the button to generate the report that you need.</h3><br/>

    <label for="reportType"> Report Type:  </label>
    <asp:DropDownList ID="ddReportType" style="width:220px; height:30px;" Font-Size="Large" runat="server" CssClass="textbox" BackColor="#efefef"></asp:DropDownList> 
    <asp:Button ID="btnSubmit" style="width:150px; height:30px;" Font-Size="Large" runat="server" Text="Submit" OnClick="btnSubmit_Click" CssClass="textbox" BackColor="#efefef" /><br /><br /> 
  
      <asp:UpdatePanel ID="UpdatePanel1" runat="server" >
        <ContentTemplate>            
            <asp:Panel ID="panel1" runat="server" Visible="false">
                <label>&nbsp;</label>                
                <asp:Button ID="btnExcel" style="width:150px; height:30px;" Font-Size="Large" runat="server" Text="Export to Excel" OnClick="btnExcel_Click" CssClass="textbox" BackColor="#efefef"/><br /><br /> 
                <asp:GridView ID="GridView1" runat="server" OnRowDataBound="GridView1_RowDataBound" OnPageIndexChanging="GridView1_PageIndexChanging" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical">
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

                <label>Sum: $</label><asp:Label ID="lblSum" runat="server"></asp:Label><br/> 
                 
            </asp:Panel>
        </ContentTemplate>
        <Triggers><asp:AsyncPostBackTrigger ControlID="btnSubmit"  EventName="Click" /></Triggers>
    </asp:UpdatePanel>
	
	<br/>
	<br/>
 
	<asp:Label ID="lblStatus" runat="server"></asp:Label>
    <asp:Button ID="btnAsst" style="width:250px; height:30px;" Font-Size="Large" runat="server" Text="Top off Assist Account" OnClick="btnAsst_Click" CssClass="textbox" BackColor="#efefef" /><br /><br /> 
 	<asp:Label ID="lblAsst" runat="server"></asp:Label>

</asp:Content>
