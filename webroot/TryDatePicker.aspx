<%@ Page Language="C#" MasterPageFile="~/MasterPages/MenuMasterPage.master" AutoEventWireup="true"  CodeFile="TryDatePicker.aspx.cs" Inherits="TryDatePicker"%>
 
  
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

    
  <asp:TextBox ID="txtDate" type="text" runat="server" ></asp:TextBox>
    <asp:Button ID="btnSubmit" runat="server" Text="Submit"
        onclick="btnSubmit_Click" />
    
    <asp:Label ID="lblOutput" runat="server" Text="Label"></asp:Label>

 
</asp:Content>
 