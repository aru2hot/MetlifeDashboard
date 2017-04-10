<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Modal_WebForm.aspx.cs" Inherits="Modal_WebForm" %>


register the AJAX Control Toolkit Library by putting the following line just below the @PageDirective
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <style type="text/css">
     
            .Popup
        {
            background-color: #FFFFFF;
            border-width: 3px;
            border-style: solid;
            border-color: black;
            padding-top: 10px;
            padding-left: 10px;
            width: 400px;
            height: 350px;
        }

              .Background
        {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.8;
        }
</style>
    
</head>
   
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        
        <asp:Button ID="btn_open" runat="server" Text="Open"/>

       <cc1:ModalPopupExtender id="modal1" runat="server"  PopupControlID="modal_panel"  TargetControlID="btn_open"   BackgroundCssClass="Background">
           <%--CancelControlID="btn_close"--%>

       </cc1:ModalPopupExtender>

        <asp:Panel id="modal_panel" runat="server"  CssClass="Popup" HorizontalAlign="Center">
           
            <table cellpadding="5px" cellspacing="5px" align="center">
                <tr>
                    <td>
             <asp:Label ID="Label1" runat="server" Text="Label" >Username</asp:Label>
                        </td>
                    <td>
                  
            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                        </td>
                </tr>
               <tr>
                    <td>
             <asp:Label ID="Label2" runat="server" Text="Label" >Password</asp:Label>
                        </td>
                    <td>
                  
            <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                        </td>
                </tr>
            
                  </table>

            <asp:Button ID="btn_close" runat="server" Text="Close" />
        </asp:Panel>
    
    </form>
</body>
</html>
