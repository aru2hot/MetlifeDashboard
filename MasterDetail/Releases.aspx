﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Releases.aspx.cs" Inherits="Releases" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="release_content" ContentPlaceHolderID="MainContent" runat="Server">


    <div class="panel panel-primary">
        <div class="panel-heading">
            MetLife Delivery Dashboard
        </div>

        <div class="panel panel-default">
            <div class="panel-body">
                <div id="main_Tabs" role="tabpanel">

                    <ul class="nav nav-tabs" role="tablist">
                        <li class="active"><a href="#tab_main_1" aria-controls="tab_main_1" role="tab" data-toggle="tab">RELEASE INFORMATION</a></li>
                    </ul>
                </div>
            </div>
        </div>
        <div class="panel-body">
            <div class="row wellCustom">
                <div class="form-group col-md-2">
                    BU<br />
                    <asp:DropDownList ID="ddl_BU" CssClass="form-control boldtext" runat="server" OnSelectedIndexChanged="ddl_BU_SelectedIndexChanged" AutoPostBack="true">
                        <asp:ListItem Value="EAD">EAD</asp:ListItem>
                        <asp:ListItem Value="RAD">RAD</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="form-group col-md-2">
                    Release Month<br />
                    <asp:DropDownList ID="ddl_ReleaseMonth" CssClass="ui-datepicker-month form-control boldtext" runat="server" AutoPostBack="true"  OnSelectedIndexChanged="ddl_ReleaseMonth_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
                <div class="form-group col-md-2">
                    PBM<br />
                    <asp:DropDownList ID="ddl_PBM" CssClass="form-control boldtext" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_PBM_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
                <div class="form-group col-md-3">
                    PBM-1<br />
                    <asp:DropDownList ID="ddl_PBM1" CssClass="form-control boldtext" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_PBM1_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
                <div class="form-group col-md-3">
                    PBM-2<br />
                    <asp:DropDownList ID="ddl_PBM2" CssClass="form-control boldtext" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_PBM2_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="row wellCustom">
                <div class="form-group col-md-3">
                    Portfolio<br />
                    <asp:DropDownList ID="ddl_Portfolio" CssClass="accordian-content form-control boldtext" runat="server" OnSelectedIndexChanged="ddl_Portfolio_SelectedIndexChanged" AutoPostBack="true">
                    </asp:DropDownList>
                </div>
                <div class="form-group col-md-3">
                    Application<br />
                    <asp:DropDownList ID="ddl_Application" CssClass="form-control boldtext" runat="server" OnSelectedIndexChanged="ddl_Application_SelectedIndexChanged" AutoPostBack="true">
                    </asp:DropDownList>
                </div>
                <div class="form-group col-md-3">
                    Release Name<br />
                    <asp:DropDownList ID="ddl_Releases" CssClass="form-control boldtext" runat="server" AutoPostBack="true">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="row wellCustom">

                <div class="form-group col-md-3">
                    Status<br />
                    <asp:RadioButtonList ID="RAG_status" runat="server" CssClass="btn-group" data-toggle="buttons" RepeatDirection="Horizontal">
                        <asp:ListItem class="btn btn-xs btn-danger">RED</asp:ListItem>
                        <asp:ListItem class="btn btn-xs btn-success">GREEN</asp:ListItem>
                        <asp:ListItem Selected="True" class="btn btn-xs btn-primary">All</asp:ListItem>
                    </asp:RadioButtonList>
                </div>
                <div class="form-group col-md-6">
                    <div class="pull-right vertical-align">
                        <asp:LinkButton ID="Search_Button" runat="server" CssClass="btn btn-sm btn-success" OnClick="Search_Button_Click">
                                    <span class="fa fa-search"></span>&nbsp;Search
                        </asp:LinkButton>
                        <asp:LinkButton ID="Reset_Button" runat="server" CssClass="btn btn-sm btn-success" OnClick="Reset_Button_Click">
                                    <span class="fa fa-refresh"></span>&nbsp;Reset
                        </asp:LinkButton>
                    </div>
                </div>
            </div>
        </div>

        <div class="panel panel-default">
            <div class="panel-body">
                <div id="Tabs" role="tabpanel">
                    <!-- Nav tabs -->
                    <ul class="nav nav-tabs" role="tablist">
                        <li><a href="#tab1" aria-controls="tab1" role="tab" data-toggle="tab">PRIOR RELEASE</a></li>
                        <li><a href="#tab2" aria-controls="tab2" role="tab" data-toggle="tab">CURRENT RELEASE</a></li>
                        <li><a href="#tab3" aria-controls="tab3" role="tab" data-toggle="tab">FUTURE RELEASE</a></li>

                    </ul>
                    <div role="tabpanel" class="tab-pane" id="tab1">

                        <div style="overflow-x: scroll; overflow-y: scroll; max-height: 400px; max-width: 1500px;">

                            <asp:GridView ID="prior_release_grid" runat="server" AutoGenerateColumns="false"
                                CssClass="tableSmall table-responsive table-bordered table-hover"
                                ShowHeaderWhenEmpty="true" EmptyDataText="No records Found" OnRowDataBound="prior_release_grid_RowDataBound" CellPadding="4" ForeColor="#333333" GridLines="None">

                                <AlternatingRowStyle BackColor="White"></AlternatingRowStyle>
                                <Columns>
                                    <asp:TemplateField HeaderText="RELEASE NAME">
                                        <ItemTemplate>
                                            <div>
                                                <%# Eval("RELEASENAME") %>
                                            </div>
                                        </ItemTemplate>


                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="APPLICATION">
                                        <ItemTemplate>
                                            <div>
                                                <%# Eval("APPLICATION") %>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="PBM1">
                                        <ItemTemplate>
                                            <div>
                                                <%# Eval("PBM1") %>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="PBM2">
                                        <ItemTemplate>
                                            <div>
                                                <%# Eval("PBM2") %>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="CURRENT PHASE">
                                        <ItemTemplate>
                                            <div>
                                                <%# Eval("CURRENTPHASE") %>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Production Defects">
                                        <ItemTemplate>
                                            <div>
                                                <%# Eval("PSDEFECTS") %>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="RELEASE DATE">
                                        <ItemTemplate>
                                            <div>
                                                <%# Eval("RELEASESTARTDATE") %>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--<asp:TemplateField HeaderText="QA START DATE">
                                                                                <ItemTemplate>
                                                                                    <div>
                                                                                        <%# Eval("QA_Start_dt") %>                                                                                       
                                                                                    </div>
                                                                                </ItemTemplate>

                                                                                <EditItemTemplate>


                                                                                    <asp:TextBox ID="QA_Start_Date" runat="server" Text=' <%# Bind("QA_Start_dt") %>' Width="70px" ReadOnly="true" ClientIDMode="Static"></asp:TextBox>

                                                                                </EditItemTemplate>

                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="QA END DATE">
                                                                                <ItemTemplate>
                                                                                    <div>
                                                                                        <%# Eval("QA_end_dt") %>
                                                                                    </div>
                                                                                </ItemTemplate>

                                                                                <EditItemTemplate>

                                                                                    <asp:TextBox ID="QA_End_Date" runat="server" Text=' <%# Bind("QA_end_dt") %>' Width="70px"></asp:TextBox>

                                                                                </EditItemTemplate>

                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="QA PROGRESS">
                                                                                <ItemTemplate>
                                                                                    <div>
                                                                                        <%# Eval("QA_Progress_PERCENT") %>
                                                                                    </div>
                                                                                </ItemTemplate>



                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="UAT START DATE">
                                                                                <ItemTemplate>
                                                                                    <div>
                                                                                        <%# Eval("UAT_Start_dt") %>
                                                                                    </div>
                                                                                </ItemTemplate>

                                                                                <EditItemTemplate>

                                                                                    <asp:TextBox ID="UAT_Start_Date" runat="server" Text=' <%# Bind("UAT_Start_dt") %>' Width="70px"></asp:TextBox>

                                                                                </EditItemTemplate>

                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="UAT END DATE">
                                                                                <ItemTemplate>
                                                                                    <div>
                                                                                        <%# Eval("UAT_End_dt") %>
                                                                                    </div>
                                                                                </ItemTemplate>
                                                                                <EditItemTemplate>

                                                                                    <asp:TextBox ID="UAT_End_Date" runat="server" Text=' <%# Bind("UAT_End_dt") %>' Width="70px"></asp:TextBox>

                                                                                </EditItemTemplate>

                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="UAT PROGRESS">
                                                                                <ItemTemplate>
                                                                                    <div>
                                                                                        <%# Eval("UAT_Progress_PERCENT") %>
                                                                                    </div>
                                                                                </ItemTemplate>

                                                                            </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="LAST MODIFIED BY">
                                        <ItemTemplate>
                                            <div>
                                                <%# Eval("Last_modified_by") %>
                                            </div>
                                        </ItemTemplate>

                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="LAST MODIFIED DATE">
                                        <ItemTemplate>
                                            <div>
                                                <%# Eval("Last_modified_date") %>
                                            </div>
                                        </ItemTemplate>

                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="MANAGER/TL ASSESSED RAG">
                                        <ItemTemplate>
                                            <div>
                                                <%# Eval("RAG_Status") %>
                                            </div>
                                        </ItemTemplate>

                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="MANGER/TL COMMENTS">
                                        <ItemTemplate>
                                            <div>
                                                <%# Eval("Comments") %>
                                            </div>
                                        </ItemTemplate>

                                        <EditItemTemplate>

                                            <asp:TextBox ID="Comments" runat="server" Text=' <%# Bind("Comments") %>' Width="70px"></asp:TextBox>

                                        </EditItemTemplate>

                                    </asp:TemplateField>


                                </Columns>
                                <EditRowStyle BackColor="#7C6F57"></EditRowStyle>

                                <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White"></FooterStyle>

                                <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White"></HeaderStyle>

                                <PagerStyle HorizontalAlign="Center" BackColor="#666666" ForeColor="White"></PagerStyle>

                                <RowStyle BackColor="#E3EAEB"></RowStyle>

                                <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333"></SelectedRowStyle>

                                <SortedAscendingCellStyle BackColor="#F8FAFA"></SortedAscendingCellStyle>

                                <SortedAscendingHeaderStyle BackColor="#246B61"></SortedAscendingHeaderStyle>

                                <SortedDescendingCellStyle BackColor="#D4DFE1"></SortedDescendingCellStyle>

                                <SortedDescendingHeaderStyle BackColor="#15524A"></SortedDescendingHeaderStyle>
                            </asp:GridView>

                        </div>

                    </div>
                    <div role="tabpanel" class="tab-pane" id="tab2">

                        <div style="overflow-x: scroll; overflow-y: scroll; max-height: 400px; max-width: 1500px;">

                            <asp:GridView ID="current_release_grid" runat="server" AutoGenerateColumns="false"
                                CssClass="tableSmall table-responsive table-bordered table-hover"
                                ShowHeaderWhenEmpty="true" DataKeyNames="UDSPROJECTKEY" AutoGenerateSelectButton="true" OnSelectedIndexChanged="current_release_grid_SelectedIndexChanged" EmptyDataText="No records Found" OnRowDataBound="current_release_grid_RowDataBound" CellPadding="4" ForeColor="#333333" GridLines="None">

                                <AlternatingRowStyle BackColor="White"></AlternatingRowStyle>
                                <Columns>
                                    <asp:TemplateField HeaderText="RELEASE NAME">
                                        <ItemTemplate>
                                            <div>
                                                <asp:Label ID="GV_CF_ReleaseName" runat="server" Text=' <%# Eval("RELEASENAME") %>'></asp:Label>

                                            </div>
                                        </ItemTemplate>

                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="APPLICATION">
                                        <ItemTemplate>
                                            <div>
                                                <asp:Label ID="GV_CF_Application" runat="server" Text='   <%# Eval("APPLICATION") %>'></asp:Label>

                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="PBM1">
                                        <ItemTemplate>
                                            <div>
                                                <asp:Label ID="GV_CF_PBM1" runat="server" Text=' <%# Eval("PBM1") %>'></asp:Label>

                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="PBM2">
                                        <ItemTemplate>
                                            <div>
                                                <asp:Label ID="GV_CF_PBM2" runat="server" Text=' <%# Eval("PBM2") %>'></asp:Label>

                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="CURRENT PHASE">
                                        <ItemTemplate>
                                            <div>
                                                <%# Eval("CURRENTPHASE") %>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="RAG" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>

                                            <asp:Label ID="GV_CF_RAG" runat="server" Text='<%# Eval("RAG").ToString() == "RED" ? "RED" : "GREEN" %>' Style="visibility: hidden;"></asp:Label>

                                            <p class="<%# Eval("RAG").ToString() == "RED" ? "RedColor" : "GreenColor" %>"><span class="fa fa-circle"></span></p>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Production Defects">
                                        <ItemTemplate>
                                            <div>
                                                <%# Eval("PSDEFECTS") %>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="RELEASE DATE">
                                        <ItemTemplate>
                                            <div>
                                                <%# Eval("RELEASESTARTDATE") %>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="QA START DATE">
                                        <ItemTemplate>
                                            <div>
                                                <%# Eval("QA_Start_dt") %>


                                                <%-- <asp:TextBox ID="QA_Start_Date1" runat="server" Text=' <%# Bind("QA_Strt_dt") %>' Width="150px" ReadOnly="true" ClientIDMode="Static" ></asp:TextBox>--%>
                                            </div>
                                        </ItemTemplate>


                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="QA END DATE">
                                        <ItemTemplate>
                                            <div>
                                                <%# Eval("QA_End_dt") %>
                                            </div>
                                        </ItemTemplate>


                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="QA PROGRESS">
                                        <ItemTemplate>
                                            <div>
                                                <%# Eval("QA_Progress_PERCENT") %>
                                            </div>
                                        </ItemTemplate>



                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="UAT START DATE">
                                        <ItemTemplate>
                                            <div>
                                                <%# Eval("UAT_Start_dt") %>
                                            </div>
                                        </ItemTemplate>

                                        <EditItemTemplate>

                                            <asp:TextBox ID="UAT_Start_Date" runat="server" Text=' <%# Bind("UAT_Start_dt") %>' Width="70px"></asp:TextBox>

                                        </EditItemTemplate>

                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="UAT END DATE">
                                        <ItemTemplate>
                                            <div>
                                                <%# Eval("UAT_End_dt") %>
                                            </div>
                                        </ItemTemplate>
                                        <EditItemTemplate>

                                            <asp:TextBox ID="UAT_End_Date" runat="server" Text=' <%# Bind("UAT_End_dt") %>' Width="70px"></asp:TextBox>

                                        </EditItemTemplate>

                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="UAT PROGRESS">
                                        <ItemTemplate>
                                            <div>
                                                <%# Eval("UAT_Progress_PERCENT") %>
                                            </div>
                                        </ItemTemplate>

                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="LAST MODIFIED BY">
                                        <ItemTemplate>
                                            <div>
                                                <%# Eval("Last_modified_by") %>
                                            </div>
                                        </ItemTemplate>

                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="LAST MODIFIED DATE">
                                        <ItemTemplate>
                                            <div>
                                                <%# Eval("Last_modified_date") %>
                                            </div>
                                        </ItemTemplate>

                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="MANAGER/TL ASSESSED RAG">
                                        <ItemTemplate>
                                            <div>
                                                <%# Eval("RAG_Status") %>
                                            </div>
                                        </ItemTemplate>

                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="MANGER/TL COMMENTS">
                                        <ItemTemplate>
                                            <div>
                                                <%# Eval("Comments") %>
                                            </div>
                                        </ItemTemplate>

                                        <EditItemTemplate>

                                            <asp:TextBox ID="Comments" runat="server" Text=' <%# Bind("Comments") %>' Width="70px"></asp:TextBox>

                                        </EditItemTemplate>

                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText=" ">

                                        <ItemTemplate>
                                            <div>


                                                <asp:Label ID="GV_CF_UDS_Project_Key" runat="server" Text='<%# Eval("UDSPROJECTKEY") %>' Visible="false"></asp:Label>

                                            </div>
                                        </ItemTemplate>

                                    </asp:TemplateField>

                                </Columns>

                                <EditRowStyle BackColor="#7C6F57"></EditRowStyle>

                                <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White"></FooterStyle>

                                <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White"></HeaderStyle>

                                <PagerStyle HorizontalAlign="Center" BackColor="#666666" ForeColor="White"></PagerStyle>

                                <RowStyle BackColor="#E3EAEB"></RowStyle>

                                <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333"></SelectedRowStyle>

                                <SortedAscendingCellStyle BackColor="#F8FAFA"></SortedAscendingCellStyle>

                                <SortedAscendingHeaderStyle BackColor="#246B61"></SortedAscendingHeaderStyle>

                                <SortedDescendingCellStyle BackColor="#D4DFE1"></SortedDescendingCellStyle>

                                <SortedDescendingHeaderStyle BackColor="#15524A"></SortedDescendingHeaderStyle>
                            </asp:GridView>
                            <asp:HiddenField ID="popid" runat="server" />
                            <cc1:modalpopupextender id="ModalPopupExtender2" runat="server" popupcontrolid="Current_Future_PopUp_Window"
                                targetcontrolid="popid">
                                                                    </cc1:modalpopupextender>
                            <asp:Panel ID="Current_Future_PopUp_Window" runat="server" CssClass="modalPopupStyle" Style="height: auto; max-height: 600px; width: 1000px; border: 2px solid white; padding: 5px; background-color: #FFFFFF; display: none;">

                                <div class="panel panel-primary">

                                    <div class="panel-heading">
                                        <div class="row">
                                            <div class="col-xs-6">
                                                <div style="vertical-align: middle;">
                                                    Current Release Details
                                                </div>
                                            </div>
                                            <div class="col-xs-6">
                                                <div class="WhiteColor" style="vertical-align: middle;">
                                                    <asp:LinkButton runat="server" CssClass="pull-right" ID="CF_Close_Button2" OnClick="CF_Close_Click" ForeColor="Black"><span class="fa fa-close"></span></asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="panel-body">

                                        <div class="row wellCustom">
                                            <div class="form-group col-md-3">
                                                <b>Release Name</b>
                                                <br />
                                                <asp:Label ID="PopUp_CF_Release_Name" runat="server" AutoPostBack="false" />
                                            </div>
                                            <div class="form-group col-md-3">
                                                <b>Application</b>
                                                <br />
                                                <asp:Label ID="PopUp_CF_Application" runat="server" AutoPostBack="false" />
                                            </div>
                                            <div class="form-group col-md-3">
                                                <b>PBM1 </b>
                                                <br />
                                                <asp:Label ID="PopUp_CF_PBM1" runat="server" AutoPostBack="false" />
                                            </div>

                                            <div class="form-group col-md-3">
                                                <b>PBM1</b>
                                                <br />
                                                <asp:Label ID="PopUp_CF_PBM2" runat="server" AutoPostBack="false" />
                                            </div>
                                        </div>


                                        <div class="row wellCustom">
                                            <div class="form-group col-md-3">
                                                <b>Current Phase</b>
                                                <br />
                                                <asp:Label ID="PopUp_CF_Current_Phase" runat="server" AutoPostBack="false" />
                                            </div>
                                            <div class="form-group col-md-3">
                                                <b>RAG</b>
                                                <br />
                                                <asp:Label ID="PopUp_CF_RAG" runat="server" AutoPostBack="false" />
                                            </div>
                                            <div class="form-group col-md-3">
                                                <b>Production Defects</b>
                                                <br />
                                                <asp:Label ID="PopUp_CF_ProductionDefects" runat="server" AutoPostBack="false" />
                                            </div>

                                            <div class="form-group col-md-3">
                                                <b>ReleaseDate </b>
                                                <br />
                                                <asp:Label ID="PopUp_CF_ReleaseDate" runat="server" AutoPostBack="false" />
                                            </div>
                                        </div>

                                        <div class="row wellCustom">
                                            <div class="form-group col-md-4">
                                                <b>QA Start Date </b>
                                                <br />
                                                <asp:TextBox ID="PopUp_CF_QAStartDate" runat="server" TextMode="Date" Text="" AutoPostBack="false" />

                                            </div>
                                            <div class="form-group col-md-4">
                                                <b>QA End Date </b>
                                                <br />
                                                <asp:TextBox ID="PopUp_CF_QAEndDate" runat="server" TextMode="Date" Text="" AutoPostBack="false" />
                                            </div>
                                            <div class="form-group col-md-4">
                                                <b>QA Progress </b>
                                                <br />
                                                <asp:TextBox ID="PopUp_CF_QAProgress" runat="server" TextMode="Number" AutoPostBack="false" />
                                            </div>


                                        </div>

                                        <div class="row wellCustom">
                                            <div class="form-group col-md-4">
                                                <b>UAT Start Date </b>
                                                <br />
                                                <asp:TextBox ID="PopUp_CF_UATStartDate" runat="server" TextMode="Date" Text="" AutoPostBack="false" />

                                            </div>
                                            <div class="form-group col-md-4">
                                                <b>UAT End Date</b>
                                                <br />
                                                <asp:TextBox ID="PopUp_CF_UATEndDate" runat="server" TextMode="Date" Text="" AutoPostBack="false" />
                                            </div>
                                            <div class="form-group col-md-4">
                                                <b>UAT Progress<br />
                                                </b>
                                                <asp:TextBox ID="PopUp_CF_UATProgress" runat="server" TextMode="Number" AutoPostBack="false" />
                                            </div>


                                        </div>

                                        <div class="row wellCustom">
                                            <div class="form-group col-md-4">
                                                <b>Manager Comments</b>

                                            </div>
                                            <div class="form-group col-md-8">
                                                <asp:TextBox TextMode="MultiLine" ID="PopUp_CF_ManagerComments" name="PopUp_CF_ManagerComments" Style="width: 1000px; height: 200px" runat="server" AutoPostBack="false" />
                                            </div>

                                        </div>

                                    </div>

                                    <div style="padding: 10px;">
                                        <asp:Button ID="CF_Update" runat="server" CssClass="btn btn-sm btn-success" OnClick="CF_Update_Click" Text="UPDATE" />
                                        <asp:Button ID="CF_Close" OnClientClick="test_Click()" runat="server" Text="CLOSE" CssClass="btn btn-sm btn-danger" OnClick="CF_Close_Click"></asp:Button>
                                    </div>

                                </div>


                            </asp:Panel>

                        </div>
                    </div>
                    <div role="tabpanel" class="tab-pane" id="tab3">
                        <div style="overflow-x: scroll; overflow-y: scroll; max-height: 400px; max-width: 1500px;">

                            <asp:GridView ID="future_release_grid" runat="server" AutoGenerateColumns="false"
                                CssClass="tableSmall table-responsive table-bordered table-hover"
                                ShowHeaderWhenEmpty="true" DataKeyNames="UDSPROJECTKEY" AutoGenerateSelectButton="true" OnSelectedIndexChanged="future_release_grid_SelectedIndexChanged" EmptyDataText="No records Found" OnRowDataBound="future_release_grid_RowDataBound" CellPadding="4" ForeColor="#333333" GridLines="None">
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775"></AlternatingRowStyle>
                                <Columns>
                                    <asp:TemplateField HeaderText="RELEASE NAME">
                                        <ItemTemplate>
                                            <div>
                                                <asp:Label ID="GV_F_ReleaseName" runat="server" Text=' <%# Eval("RELEASENAME") %>'></asp:Label>

                                            </div>
                                        </ItemTemplate>

                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="APPLICATION">
                                        <ItemTemplate>
                                            <div>
                                                <asp:Label ID="GV_F_Application" runat="server" Text='   <%# Eval("APPLICATION") %>'></asp:Label>

                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="PBM1">
                                        <ItemTemplate>
                                            <div>
                                                <asp:Label ID="GV_F_PBM1" runat="server" Text=' <%# Eval("PBM1") %>'></asp:Label>

                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="PBM2">
                                        <ItemTemplate>
                                            <div>
                                                <asp:Label ID="GV_F_PBM2" runat="server" Text=' <%# Eval("PBM2") %>'></asp:Label>

                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="CURRENT PHASE">
                                        <ItemTemplate>
                                            <div>
                                                <%# Eval("CURRENTPHASE") %>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="RAG" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>

                                            <asp:Label ID="GV_F_RAG" runat="server" Text='<%# Eval("RAG").ToString() == "RED" ? "RED" : "GREEN" %>' Style="visibility: hidden;"></asp:Label>

                                            <p class="<%# Eval("RAG").ToString() == "RED" ? "RedColor" : "GreenColor" %>"><span class="fa fa-circle"></span></p>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Production Defects">
                                        <ItemTemplate>
                                            <div>
                                                <%# Eval("PSDEFECTS") %>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="RELEASE DATE">
                                        <ItemTemplate>
                                            <div>
                                                <%# Eval("RELEASESTARTDATE") %>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="QA START DATE">
                                        <ItemTemplate>
                                            <div>
                                                <%# Eval("QA_Start_dt") %>


                                                <%-- <asp:TextBox ID="QA_Start_Date1" runat="server" Text=' <%# Bind("QA_Strt_dt") %>' Width="150px" ReadOnly="true" ClientIDMode="Static" ></asp:TextBox>--%>
                                            </div>
                                        </ItemTemplate>


                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="QA END DATE">
                                        <ItemTemplate>
                                            <div>
                                                <%# Eval("QA_End_dt") %>
                                            </div>
                                        </ItemTemplate>


                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="QA PROGRESS">
                                        <ItemTemplate>
                                            <div>
                                                <%# Eval("QA_Progress_PERCENT") %>
                                            </div>
                                        </ItemTemplate>



                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="UAT START DATE">
                                        <ItemTemplate>
                                            <div>
                                                <%# Eval("UAT_Start_dt") %>
                                            </div>
                                        </ItemTemplate>

                                        <EditItemTemplate>

                                            <asp:TextBox ID="UAT_Start_Date" runat="server" Text=' <%# Bind("UAT_Start_dt") %>' Width="70px"></asp:TextBox>

                                        </EditItemTemplate>

                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="UAT END DATE">
                                        <ItemTemplate>
                                            <div>
                                                <%# Eval("UAT_End_dt") %>
                                            </div>
                                        </ItemTemplate>
                                        <EditItemTemplate>

                                            <asp:TextBox ID="UAT_End_Date" runat="server" Text=' <%# Bind("UAT_end_dt") %>' Width="70px"></asp:TextBox>

                                        </EditItemTemplate>

                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="UAT PROGRESS">
                                        <ItemTemplate>
                                            <div>
                                                <%# Eval("UAT_Progress_PERCENT") %>
                                            </div>
                                        </ItemTemplate>

                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="LAST MODIFIED BY">
                                        <ItemTemplate>
                                            <div>
                                                <%# Eval("Last_modified_by") %>
                                            </div>
                                        </ItemTemplate>

                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="LAST MODIFIED DATE">
                                        <ItemTemplate>
                                            <div>
                                                <%# Eval("Last_modified_date") %>
                                            </div>
                                        </ItemTemplate>

                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="MANAGER/TL ASSESSED RAG">
                                        <ItemTemplate>
                                            <div>
                                                <%# Eval("RAG_Status") %>
                                            </div>
                                        </ItemTemplate>

                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="MANGER/TL COMMENTS">
                                        <ItemTemplate>
                                            <div>
                                                <%# Eval("Comments") %>
                                            </div>
                                        </ItemTemplate>

                                        <EditItemTemplate>

                                            <asp:TextBox ID="F_Comments" runat="server" Text=' <%# Bind("Comments") %>' Width="70px"></asp:TextBox>

                                        </EditItemTemplate>

                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText=" ">

                                        <ItemTemplate>
                                            <div>


                                                <asp:Label ID="GV_F_UDS_Project_Key" runat="server" Text='<%# Eval("UDSPROJECTKEY") %>' Visible="false"></asp:Label>

                                            </div>
                                        </ItemTemplate>

                                    </asp:TemplateField>

                                </Columns>

                                <EditRowStyle BackColor="#999999"></EditRowStyle>

                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White"></FooterStyle>

                                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White"></HeaderStyle>

                                <PagerStyle HorizontalAlign="Center" BackColor="#284775" ForeColor="White"></PagerStyle>

                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333"></RowStyle>

                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333"></SelectedRowStyle>

                                <SortedAscendingCellStyle BackColor="#E9E7E2"></SortedAscendingCellStyle>

                                <SortedAscendingHeaderStyle BackColor="#506C8C"></SortedAscendingHeaderStyle>

                                <SortedDescendingCellStyle BackColor="#FFFDF8"></SortedDescendingCellStyle>

                                <SortedDescendingHeaderStyle BackColor="#6F8DAE"></SortedDescendingHeaderStyle>
                            </asp:GridView>
                            <asp:HiddenField ID="popid_F" runat="server" />
                            <cc1:modalpopupextender id="ModalPopupExtender_FUTURE" runat="server" popupcontrolid="Future_PopUp_Window"
                                targetcontrolid="popid_F">
                                                                    </cc1:modalpopupextender>
                            <asp:Panel ID="Future_PopUp_Window" runat="server" CssClass="modalPopupStyle" Style="height: auto; max-height: 600px; width: 1000px; border: 2px solid white; padding: 5px; background-color: #FFFFFF; display: none;">

                                <div class="panel panel-primary">

                                    <div class="panel-heading">
                                        <div class="row">
                                            <div class="col-xs-6">
                                                <div style="vertical-align: middle;">
                                                    Future Release Details
                                                </div>
                                            </div>
                                            <div class="col-xs-6">
                                                <div class="WhiteColor" style="vertical-align: middle;">
                                                    <asp:LinkButton runat="server" CssClass="pull-right" ID="F_Close_Button2" OnClick="F_Close_Click" ForeColor="Black"><span class="fa fa-close"></span></asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="panel-body">

                                        <div class="row wellCustom">
                                            <div class="form-group col-md-3">
                                                <b>Release Name</b>
                                                <br />
                                                <asp:Label ID="PopUp_F_Release_Name" runat="server" AutoPostBack="false" />
                                            </div>
                                            <div class="form-group col-md-3">
                                                <b>Application</b>
                                                <br />
                                                <asp:Label ID="PopUp_F_Application" runat="server" AutoPostBack="false" />
                                            </div>
                                            <div class="form-group col-md-3">
                                                <b>PBM1 </b>
                                                <br />
                                                <asp:Label ID="PopUp_F_PBM1" runat="server" AutoPostBack="false" />
                                            </div>

                                            <div class="form-group col-md-3">
                                                <b>PBM1</b>
                                                <br />
                                                <asp:Label ID="PopUp_F_PBM2" runat="server" AutoPostBack="false" />
                                            </div>
                                        </div>


                                        <div class="row wellCustom">
                                            <div class="form-group col-md-3">
                                                <b>Current Phase</b>
                                                <br />
                                                <asp:Label ID="PopUp_F_Current_Phase" runat="server" AutoPostBack="false" />
                                            </div>
                                            <div class="form-group col-md-3">
                                                <b>RAG</b>
                                                <br />
                                                <asp:Label ID="PopUp_F_RAG" runat="server" AutoPostBack="false" />
                                            </div>
                                            <div class="form-group col-md-3">
                                                <b>Production Defects</b>
                                                <br />
                                                <asp:Label ID="PopUp_F_ProductionDefects" runat="server" AutoPostBack="false" />
                                            </div>

                                            <div class="form-group col-md-3">
                                                <b>ReleaseDate </b>
                                                <br />
                                                <asp:Label ID="PopUp_F_ReleaseDate" runat="server" AutoPostBack="false" />
                                            </div>
                                        </div>

                                        <div class="row wellCustom">
                                            <div class="form-group col-md-4">
                                                <b>QA Start Date </b>
                                                <br />
                                                <asp:TextBox ID="PopUp_F_QAStartDate" runat="server" TextMode="Date" Text="" AutoPostBack="false" />

                                            </div>
                                            <div class="form-group col-md-4">
                                                <b>QA End Date </b>
                                                <br />
                                                <asp:TextBox ID="PopUp_F_QAEndDate" runat="server" TextMode="Date" Text="" AutoPostBack="false" />
                                            </div>
                                            <div class="form-group col-md-4">
                                                <b>QA Progress </b>
                                                <br />
                                                <asp:TextBox ID="PopUp_F_QAProgress" runat="server" TextMode="Number" AutoPostBack="false" />
                                            </div>


                                        </div>

                                        <div class="row wellCustom">
                                            <div class="form-group col-md-4">
                                                <b>UAT Start Date </b>
                                                <br />
                                                <asp:TextBox ID="PopUp_F_UATStartDate" runat="server" TextMode="Date" Text="" AutoPostBack="false" />

                                            </div>
                                            <div class="form-group col-md-4">
                                                <b>UAT End Date</b>
                                                <br />
                                                <asp:TextBox ID="PopUp_F_UATEndDate" runat="server" TextMode="Date" Text="" AutoPostBack="false" />
                                            </div>
                                            <div class="form-group col-md-4">
                                                <b>UAT Progress<br />
                                                </b>
                                                <asp:TextBox ID="PopUp_F_UATProgress" runat="server" TextMode="Number" AutoPostBack="false" />
                                            </div>


                                        </div>

                                        <div class="row wellCustom">
                                            <div class="form-group col-md-4">
                                                <b>Manager Comments</b>

                                            </div>
                                            <div class="form-group col-md-8">
                                                <asp:TextBox TextMode="MultiLine" ID="PopUp_F_ManagerComments" name="PopUp_F_ManagerComments" Style="width: 1000px; height: 200px" runat="server" AutoPostBack="false" />
                                            </div>

                                        </div>

                                    </div>

                                    <div style="padding: 10px;">
                                        <asp:Button ID="F_Update" runat="server" CssClass="btn btn-sm btn-success" OnClick="F_Update_Click" Text="UPDATE" />
                                        <asp:Button ID="F_Close" runat="server" Text="CLOSE" CssClass="btn btn-sm btn-danger" OnClick="F_Close_Click"></asp:Button>
                                    </div>

                                </div>


                            </asp:Panel>

                        </div>
                    </div>

                </div>
                 <asp:HiddenField ID="TabName" runat="server" />
            </div>
        </div>
    </div>



</asp:Content>

