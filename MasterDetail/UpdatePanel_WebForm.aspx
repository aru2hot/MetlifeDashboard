<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UpdatePanel_WebForm.aspx.cs" Inherits="UpdatePanel_WebForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="Button1" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
    
        <br />
        <asp:Button ID="Button1" runat="server" Text="Get Current Time ( Without Page Reload )" OnClick="Button1_Click" Width="268px" />

    
        <br />
        <br />
        <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Get Current Time ( With Page Reload )" Width="267px" />
        <br />
        <br />
        <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" Text="Load Data" Width="124px" />
        <br />
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <asp:GridView ID="GridView1" runat="server">
                </asp:GridView>
            
                <br />
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="Button3" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>

        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <ContentTemplate>
                <fieldset>
                <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>

                     

                    <br />
                    <asp:Button ID="Button4" runat="server" OnClick="Button4_Click" Text="Button" />

                     

                    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel3">
                        <ProgressTemplate>

                            Progressing

                        </ProgressTemplate>
                    </asp:UpdateProgress>

                     

                    </fieldset>
            
            </ContentTemplate>
            
         

            
        </asp:UpdatePanel>
       

    </div>
    </form>
</body>
</html>
