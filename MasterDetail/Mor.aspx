<%@ Page Language="C#" EnableEventValidation ="false" AutoEventWireup="true" EnableViewState="true" CodeFile="Mor.aspx.cs" MasterPageFile="~/Site.master" Inherits="Mor" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="morpop" %>



<asp:Content ID="mor_content" ContentPlaceHolderID="MainContent" runat="server">

     <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/jquery-ui.min.js" type="text/javascript"></script>
    <link href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/themes/base/jquery-ui.css" rel="Stylesheet" type="text/css" />

    <script type="text/javascript" >
        function Show_hide_MOR_filter()
        {
            
            if(mor_filters.hidden)
            {
                mor_filters.hidden = false;
                MOR_SHOW_HIDE_FILTER.value = "HIDE FILTER"
            }
            else {
                mor_filters.hidden = true;
                MOR_SHOW_HIDE_FILTER.value = "SHOW FILTER"
            }
        }
    </script>
    <div class="panel-heading">
        MetLife Delivery Dashboard
    </div>
        
    <div class="tab-pane" id="mor_div">
        <%--<asp:UpdatePanel ID="up_mor_filters" runat="server">
            <ContentTemplate>--%>
                  <div class="tab-pane" id="mor_filters">
            <div class="panel-body">
                <div id="div_mor_filters" class="row wellCustom">
                    <div class="form-group col-md-3">
                        Portfolio<br />
                        <asp:DropDownList ID="mor_portofolio" CssClass="accordian-content form-control boldtext" runat="server" OnSelectedIndexChanged="mor_portofolio_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>

                    <div class="form-group col-md-3">
                        Project Name<br />
                        <asp:DropDownList ID="mor_projectname" CssClass="accordian-content form-control boldtext" runat="server" AutoPostBack="false">
                        </asp:DropDownList>
                    </div>

                    <div class="form-group col-md-3">
                        Category<br />
                        <asp:DropDownList ID="mor_category" CssClass="accordian-content form-control boldtext" runat="server" AutoPostBack="false">
                        </asp:DropDownList>
                    </div>

                    <%--<div class="form-group col-md-3">
                                                    Description<br />
                                                    <asp:DropDownList ID="mor_description" CssClass="accordian-content form-control boldtext" runat="server" AutoPostBack="false">
                                                    </asp:DropDownList>
                                                </div>--%>
                    <div class="form-group col-md-3">
                        Last Week Status<br />
                        <asp:DropDownList ID="mor_lastweekstatus" CssClass="accordian-content form-control boldtext" runat="server" AutoPostBack="false">
                            <asp:ListItem Selected="True">All</asp:ListItem>
                            <asp:ListItem Value="RED">Red</asp:ListItem>
                            <asp:ListItem Value="GREEN">Green</asp:ListItem>
                            <asp:ListItem Value="YELLOW">Yellow</asp:ListItem>
                            <asp:ListItem>NA</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="form-group col-md-3">
                        Current Week Status<br />
                        <asp:DropDownList ID="mor_currentweekstatus" CssClass="accordian-content form-control boldtext" runat="server" AutoPostBack="false">
                            <asp:ListItem Selected="True">All</asp:ListItem>
                            <asp:ListItem Value="RED">Red</asp:ListItem>
                            <asp:ListItem Value="GREEN">Green</asp:ListItem>
                            <asp:ListItem Value="YELLOW">Yellow</asp:ListItem>
                            <asp:ListItem>NA</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="form-group col-md-3">
                        Next Week Status<br />
                        <asp:DropDownList ID="mor_nextweekstatus" CssClass="accordian-content form-control boldtext" runat="server" AutoPostBack="false">
                            <asp:ListItem Selected="True">All</asp:ListItem>
                            <asp:ListItem Value="RED">Red</asp:ListItem>
                            <asp:ListItem Value="GREEN">Green</asp:ListItem>
                            <asp:ListItem Value="YELLOW">Yellow</asp:ListItem>
                            <asp:ListItem>NA</asp:ListItem>
                        </asp:DropDownList>
                    </div>

                </div>

                


            </div>


        </div>
        <%--    </ContentTemplate>

        </asp:UpdatePanel>--%>
      <div class="row wellCustom">
                    <div class="form-group col-md-6">
                        <div class="pull-right vertical-align">
                            <asp:LinkButton runat="server" ID="MOR_Add_Button" Text="ADD NEW" CssClass="btn btn-sm btn-success" OnClick="MOR_Add_Button_Click" />
                            <asp:LinkButton ID="MOR_Search_Button" runat="server"  CssClass="btn btn-sm btn-danger" OnClientClick="Show_hide_MOR_filter()" OnClick="MOR_Search_Button_Click">
                                    <span class="fa fa-search"></span>&nbsp;SEARCH
                            </asp:LinkButton>
                            <asp:LinkButton runat="server" ID="MOR_Reset" Text="RESET" CssClass="btn btn-sm btn-dark " OnClick="MOR_Reset_Click"
                                 />
                              <input type="button"  ID="MOR_SHOW_HIDE_FILTER" Value="SHOW FILTER" Class="btn btn-sm btn-warning " onclick="Show_hide_MOR_filter()"  />

                            <asp:LinkButton runat="server" ID="MOR_Excel_Export" Text="DOWNLOAD AS EXCEL" OnClick="MOR_Excel_Export_Click"   CssClass="btn btn-sm alert-warning " >

                            </asp:LinkButton>
                        </div>
                    </div>
                </div>

        <div class="tab-pane" id="mor_resultview">
            <morpop:ModalPopupExtender ID="ModalPopupExtender_MOR" runat="server" PopupControlID="MOR_PopUp" TargetControlID="MOR_Add_Button"
                BackgroundCssClass="modalBackground">
                <%--CancelControlID="MOR_Add_Close_Button"  TargetControlID="MOR_Add_Button"  --%>
            </morpop:ModalPopupExtender>
             <asp:UpdatePanel ID="MOR_PopUp" CssClass="modalPopupStyle" runat="server" style="height: auto; max-height: 900px; width: 1000px; border: 2px solid white; padding: 5px; background-color: #FFFFFF; display: none;">

            <ContentTemplate>
            <div class="panel panel-primary">


                <div class="panel-heading">
                    <div class="row">
                        <div class="col-xs-6">
                            <div style="vertical-align: middle;">
                                Add/Update MOR Details
                            </div>
                        </div>
                        <div class="col-xs-6">
                            <div class="WhiteColor" style="vertical-align: middle;">
                                <asp:LinkButton runat="server" CssClass="pull-right" ID="MOR_Add_Close_Linke" ForeColor="Black" OnClick="MOR_Add_Close_Linke_Click"><span class="fa fa-close"></span></asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>

                <div style="max-height: 500px; width: 980px; overflow: scroll">

                    <div class="panel-body">
                        <div>


                            <div class="panel-body">
                                <div class="row wellCustom">
                                    <div class="form-group col-md-4">
                                        Portfolio<br />
                                        <asp:DropDownList ID="MOR_Add_Portfolio" CssClass="accordian-content form-control boldtext" runat="server" AutoPostBack="false">
                                        </asp:DropDownList>
                                    </div>

                                    <div class="form-group col-md-4">
                                        Project Name<br />
                                        <asp:TextBox ID="MOR_Add_Project_Name" CssClass="accordian-content form-control boldtext" runat="server" TextMode="SingleLine" AutoPostBack="false" />

                                    </div>

                                    <div class="form-group col-md-4">
                                        Category<br />
                                        <asp:DropDownList ID="MOR_Add_Category" CssClass="accordian-content form-control boldtext" runat="server" AutoPostBack="false">
                                        </asp:DropDownList>
                                    </div>


                                </div>

                                <div class="row wellCustom">
                                    <div class="form-group col-md-12 ">
                                        Description<br />
                                        <%--<asp:TextBox Width="600px" Height="175px" Columns="100" TextMode="MultiLine" runat="server" ID="MOR_Add_Description"></asp:TextBox>--%>
                                        <textarea id="MOR_Add_Description" class="accordian-content form-control boldtext" name="MOR_Add_Description" runat="server" style="outline-width: thin; min-height: 200px; min-width: 850px; width: 900px"></textarea>
                                    </div>


                                </div>
                                <div class="row wellCustom">
                                    <div class="form-group col-md-4">
                                        Last Week Status<br />
                                        <asp:DropDownList ID="MOR_Add_LastWeekStatus" CssClass="accordian-content form-control boldtext" runat="server" AutoPostBack="false">
                                            <asp:ListItem Selected="True" Value="GREEN">Green</asp:ListItem>
                                            <asp:ListItem Value="RED">Red</asp:ListItem>
                                            <asp:ListItem Value="YELLOW">Yellow</asp:ListItem>
                                            <asp:ListItem>NA</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group col-md-4">
                                        Current Week Status<br />
                                        <asp:DropDownList ID="MOR_Add_CurrentWeekStatus" CssClass="accordian-content form-control boldtext" runat="server" AutoPostBack="false">


                                            <asp:ListItem Selected="True" Value="GREEN">Green</asp:ListItem>
                                            <asp:ListItem Value="RED">Red</asp:ListItem>
                                            <asp:ListItem Value="YELLOW">Yellow</asp:ListItem>
                                            <asp:ListItem>NA</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group col-md-4">
                                        Next Week Status<br />
                                        <asp:DropDownList ID="MOR_Add_NextWeekStatus" CssClass="accordian-content form-control boldtext" runat="server" AutoPostBack="false">
                                            <asp:ListItem Selected="True" Value="GREEN">Green</asp:ListItem>
                                            <asp:ListItem Value="RED">Red</asp:ListItem>
                                            <asp:ListItem Value="YELLOW">Yellow</asp:ListItem>
                                            <asp:ListItem>NA</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="row wellCustom">
                                    <div class="form-group col-md-12">
                                        Risks<br />
                                        <textarea id="MOR_Add_Risks" class="accordian-content form-control boldtext" name="MOR_Add_Risks" runat="server" style="outline-width: thin; min-height: 200px; min-width: 850px; width: 900px"></textarea>

                                    </div>

                                </div>




                            </div>

                        </div>


                    </div>

                </div>

            </div>



            <div class="row wellCustom">
                <div class="form-group col-md-12">
                    <div class="pull-right vertical-align">

                        <asp:Button runat="server" ID="MOR_Add_Details" Text="SAVE" CssClass="btn btn-md  btn-success" OnClick="MOR_Add_Details_Click" />


                        <asp:Button ID="MOR_Add_Close_Button" runat="server" CssClass="btn btn-md btn-danger" Text="CLOSE" OnClick="MOR_Add_Close_Button_Click"></asp:Button>
                        <asp:Label runat="server" ID="lbl_MOR_Add_Status" ForeColor="#3366ff"></asp:Label>
                    </div>
                </div>


            </div>

            </ContentTemplate>

            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="MOR_Add_Button" EventName="Click" />
            </Triggers>

        </asp:UpdatePanel>
        </div>
        <div class="tab-pane" id="mor_gridview" runat="server">
            <asp:UpdatePanel ID="mor_filtered_gridview" runat="server" UpdateMode="Always" ChildrenAsTriggers="true">
                <ContentTemplate>
                    <div style="overflow-x: scroll; overflow-y: scroll; max-height: 800px; max-width: 2000px;">
                        <asp:GridView Font-Size="XX-Small" HeaderStyle-Font-Size="Small" HeaderStyle-BorderColor="MediumSlateBlue" ID="mor_grid" CssClass="tableSmall table-responsive table-bordered table-hover" DataKeyNames="MOR_ID" OnSelectedIndexChanged="mor_grid_SelectedIndexChanged" AutoGenerateSelectButton="true" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataText="No Records Found" AutoGenerateDeleteButton="true" HeaderStyle-ForeColor="#000000" HeaderStyle-BackColor="#339966">
                            <AlternatingRowStyle BackColor="LightGray"></AlternatingRowStyle>
                            <Columns>


                                <asp:TemplateField HeaderText="MOR_ID" Visible="false">

                                    <ItemTemplate>

                                        <div>
                                            <%# Eval("MOR_ID") %>
                                        </div>

                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <div>
                                            <%# Eval("MOR_ID") %>
                                        </div>

                                    </EditItemTemplate>

                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="PORTFOLIO">

                                    <ItemTemplate>

                                        <div>
                                            <%# Eval("PORTFOLIO") %>
                                        </div>

                                    </ItemTemplate>

                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="PROJECT NAME">

                                    <ItemTemplate>

                                        <div>
                                            <%# Eval("PROJECT_NAME") %>
                                        </div>

                                    </ItemTemplate>

                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="DESCRIPTION">

                                    <ItemTemplate>

                                        <div>
                                            <%# Eval("DESCRIPTION") %>
                                        </div>

                                    </ItemTemplate>

                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="HEALTH-LAST WEEK">

                                    <ItemTemplate>

                                        <p class="<%# Eval("LAST_WEEK_COLOR").ToString() == "NA" ? "NA" : Eval("LAST_WEEK_COLOR").ToString() %>"><span class="fa fa-circle"></span></p>



                                    </ItemTemplate>

                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="HEALTH-CURRENT WEEK">

                                    <ItemTemplate>

                                        <p class="<%# Eval("CURRENT_WEEK_COLOR").ToString() == "NA" ? "NA" : Eval("CURRENT_WEEK_COLOR").ToString() %>"><span class="fa fa-circle"></span></p>



                                    </ItemTemplate>

                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="PROJECTED-NEXT WEEK">

                                    <ItemTemplate>

                                        <p class="<%# Eval("NEXT_WEEK_COLOR").ToString() == "NA" ? "NA" : Eval("NEXT_WEEK_COLOR").ToString() %>"><span class="fa fa-circle"></span></p>

                                    </ItemTemplate>

                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="RISKS/ISSUES/MITIGATION/KEY/ASKS" >
                                    <%--ItemStyle-Width="600px" ControlStyle-Width ="600px"--%>
                                    <ItemTemplate>

                                        <div style="width:600px" >
                                            <%# Eval("RISKS") %>
                                        </div>

                                    </ItemTemplate>

                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="WEEK ENDING">

                                    <ItemTemplate>

                                        <div>
                                            <%# Eval("WEEK_ENDING") %>
                                        </div>

                                    </ItemTemplate>

                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="CATEGORY">

                                    <ItemTemplate>

                                        <div>
                                            <%# Eval("CATEGORY") %>
                                        </div>

                                    </ItemTemplate>

                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>


                    </div>


                </ContentTemplate>

                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="MOR_Search_Button" EventName="Click" />


                </Triggers>

            </asp:UpdatePanel>


        </div>
    </div>


</asp:Content>
