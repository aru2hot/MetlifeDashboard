<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Test.aspx.cs" Inherits="Test" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:DropDownList ID="ddl" runat="server" OnSelectedIndexChanged="ddl_SelectedIndexChanged">
            <asp:ListItem>Selected</asp:ListItem>
            <asp:ListItem>All</asp:ListItem>
            <asp:ListItem>Changed</asp:ListItem>
        </asp:DropDownList>
    
            <asp:Button runat="server" ID="btn" Text="Clcick" OnClick="btn_Click" />
        <br />
        <asp:TextBox ID="txt" runat="server"></asp:TextBox>
    </div>


        <asp:GridView ID="mor_grid" runat="server">
        </asp:GridView>

         <asp:Button runat="server" ID="btn_Excel" Text="Excel" OnClick="btn_Excel_Click"
              />
    </form>
</body>
</html>
