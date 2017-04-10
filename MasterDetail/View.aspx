<%@ Page Title="View" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="View.aspx.cs" Inherits="View" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <%-- <link href="Content/bootstrap.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.5.0/css/font-awesome.min.css">
     <link href="Content/jquery.dataTables.min.css" rel="stylesheet" />--%>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>

    <%--     <link href="Content/NewStyle.css" rel="stylesheet" />
    <link href="css/ART_Custom_Bootstrap_v4_GENERAL.css" rel="stylesheet" /> --%>



    <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/jquery-ui.min.js" type="text/javascript"></script>
    <link href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/themes/base/jquery-ui.css" rel="Stylesheet" type="text/css" />

    <script type="text/javascript">
        function test_click() {
            alert("sdsd");
        }

    </script>

    <script type="text/javascript">
        $("[src*=plus]").live("click", function () {
            $(this).closest("tr").after("<tr><td></td><td colspan = '999'>" + $(this).next().html() + "</td></tr>")
            $(this).attr("src", "images/minus1.png");
            var gvOrder = $(this).closest("tr").find("[id*=gvOrders]");
            if (gvOrder.length > 0) {
                var ddlValue = $("[id*=ddl_grouping_filter]").find("option:selected").text();
                $('table.hidecolumn thead th').each(function (index) {
                    if (ddlValue == $(this).text()) {
                        $("table.hidecolumn tbody tr").each(function () {
                            $(this).find("td").eq(index).css("display", "none");
                        });
                        $(this).css("display", "none");
                        //  return false;
                    }
                });

                FreezeGrid(gvOrder[0].id, true, 400);
            }
        });
        $("[src*=minus]").live("click", function () {
            $(this).attr("src", "images/plus1.png");
            $(this).closest("tr").next().remove();
        });
        var GridId = "<%=gvCustomers.ClientID %>";
        window.onload = function () {
            FreezeGrid(GridId, false, 500);
        }

        $(function () {
            $(".btn-pref .btn").click(function () {
                $(".btn-pref .btn").removeClass("btn-primary").addClass("btn-default");
                $(this).removeClass("btn-default").addClass("btn-primary");
            });
        });
        $("[id*=Check_All]").live("click", function () {
            var chkHeader = $(this);
            var grid = $(this).closest("table");
            $("input[type=checkbox]", grid).each(function () {
                if (chkHeader.is(":checked")) {
                    $(this).attr("checked", "checked");
                    $("td", $(this).closest("tr")).addClass("selected");
                } else {
                    $(this).removeAttr("checked");
                    $("td", $(this).closest("tr")).removeClass("selected");
                }
            });
        });
        $("[id*=chk1]").live("click", function () {
            var grid = $(this).closest("table");
            var chkHeader = $("[id*=Check_All]", grid);
            if (!$(this).is(":checked")) {
                $("td", $(this).closest("tr")).removeClass("selected");
                chkHeader.removeAttr("checked");
            } else {
                $("td", $(this).closest("tr")).addClass("selected");
                if ($("[id*=chk1]", grid).length == $("[id*=chk1]:checked", grid).length) {
                    chkHeader.attr("checked", "checked");
                }
            }
        });


        function hide_search_div() {
            div_mor_filters.hidden = true;
        }



    </script>

    <%--   <div>

        <asp:TextBox ID="sample" runat="server" Text=' ' Width="70px"  ></asp:TextBox>
    </div>--%>

    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">

        <ContentTemplate>
            <div class="panel panel-primary">
                <div class="panel-heading">
                    MetLife Delivery Dashboard
                </div>

                <div class="panel panel-default">
                    <div class="panel-body">
                        <div id="main_Tabs" role="tabpanel">
                            <!-- Nav tabs -->
                            <ul class="nav nav-tabs" role="tablist">
                                <li class="active"><a href="#tab_main_1" aria-controls="tab_main_1" role="tab" data-toggle="tab">METRICS/RELEASES</a></li>
                               

                            </ul>

                            <div class="tab-content" style="padding-top: 20px">
                                <div role="tabpanel" class="tab-pane active" id="tab_main_1">
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">

                                        <ContentTemplate>
                                            <div class="panel-body">
                                                <div class="row wellCustom">
                                                    <div class="form-group col-md-2">
                                                        BU<br />
                                                        <asp:DropDownList ID="ddl_BU" CssClass="form-control boldtext" runat="server" OnSelectedIndexChanged="ddl_BU_SelectedIndexChanged1" AutoPostBack="true">
                                                            <asp:ListItem Value="EAD">EAD</asp:ListItem>
                                                            <asp:ListItem Value="RAD">RAD</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="form-group col-md-2">
                                                        Release Month<br />
                                                        <asp:DropDownList ID="ddl_ReleaseMonth" CssClass="ui-datepicker-month form-control boldtext" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_ReleaseMonth_SelectedIndexChanged1">
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
                                                        Grouping<br />
                                                        <asp:DropDownList runat="server" CssClass="form-control boldtext" ID="ddl_grouping_filter">
                                                            <asp:ListItem Value="PORTFOLIO">PORTFOLIO</asp:ListItem>
                                                            <asp:ListItem Value="PBM">PBM</asp:ListItem>
                                                            <asp:ListItem Value="Release Name">Release Name</asp:ListItem>
                                                            <asp:ListItem Value="Application Name">Application Name</asp:ListItem>
                                                            <asp:ListItem Value="Release Month">Release Month</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
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
                                            <asp:LinkButton ID="Red_Project_List" CssClass="btn btn-sm btn-danger" runat="server"
                                                OnClick="Bind_RedProjectList_On_GridView">
                <span class="fa fa-expand"></span>&nbsp;RED Project List
                                            </asp:LinkButton>

                                            <div class="panel panel-default">
                                                <div class="panel-body">
                                                    <div id="Tabs" role="tabpanel">
                                                        <!-- Nav tabs -->
                                                        <ul class="nav nav-tabs" role="tablist">
                                                            <li class="active"><a href="#tab1" aria-controls="tab1" role="tab" data-toggle="tab">METRICS</a></li>
                                                            <li><a href="#tab2" aria-controls="tab2" role="tab" data-toggle="tab">PRIOR RELEASE</a></li>
                                                            <li><a href="#tab3" aria-controls="tab3" role="tab" data-toggle="tab">CURRENT RELEASE</a></li>
                                                            <li><a href="#tab4" aria-controls="tab4" role="tab" data-toggle="tab">FUTURE RELEASE</a></li>

                                                        </ul>

                                                        <div class="tab-content" style="padding-top: 20px">
                                                            <div role="tabpanel" class="tab-pane active" id="tab1">
                                                                <asp:GridView ID="gvCustomers" runat="server" AutoGenerateColumns="false"
                                                                    CssClass="tableSmall table-responsive table-bordered table-hover"
                                                                    OnRowCreated="gvCustomers_RowCreated"
                                                                    OnRowDataBound="OnRowDataBound" ShowHeaderWhenEmpty="true" EmptyDataText="No records Found">
                                                                    <Columns>
                                                                        <asp:TemplateField>
                                                                            <ItemTemplate>

                                                                                <img alt="" class="collapseImg" style="cursor: pointer" src="images/plus1.png" />
                                                                                <asp:Panel ID="pnlOrders" runat="server" Style="display: none">
                                                                                    <div class="panel panel-info">
                                                                                        <div class="panel-body">
                                                                                            <div>
                                                                                                <asp:GridView ID="gvOrders" runat="server" AutoGenerateColumns="false"
                                                                                                    CssClass="tableSmallChild table-bordered table-responsive table-hover hidecolumn"
                                                                                                    OnRowCreated="gvOrders_RowCreated" OnRowDataBound="OnRowChildDataBound" ShowHeader="true">
                                                                                                    <%--<AlternatingRowStyle CssClass="alternateItemStyle" />
                            <HeaderStyle CssClass="headerStyle" />--%>
                                                                                                    <Columns>

                                                                                                        <asp:TemplateField HeaderText="PORTFOLIO" ItemStyle-Width="150px">
                                                                                                            <ItemTemplate>
                                                                                                                <div>
                                                                                                                    <%# Eval("PORTFOLIO") %>
                                                                                                                </div>
                                                                                                            </ItemTemplate>
                                                                                                        </asp:TemplateField>

                                                                                                        <%--<asp:BoundField ItemStyle-Width="150px" DataField="PORTFOLIO" HeaderText="PORTFOLIO" ItemStyle-CssClass="itemStyle1"/>--%>

                                                                                                        <asp:TemplateField HeaderText="RAG" ItemStyle-Width="100px" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center">
                                                                                                            <ItemTemplate>
                                                                                                                <p class="<%# Eval("RAG").ToString() == "RED" ? "RedColor" : "GreenColor" %>"><span class="fa fa-circle"></span></p>
                                                                                                            </ItemTemplate>
                                                                                                        </asp:TemplateField>

                                                                                                        <%--   <asp:BoundField ItemStyle-Width="150px" DataField="RAG" HeaderText="RAG"  />--%>

                                                                                                        <asp:TemplateField HeaderText="PBM" ItemStyle-Width="150px">
                                                                                                            <ItemTemplate>
                                                                                                                <div>
                                                                                                                    <%# Eval("PBM") %>
                                                                                                                </div>
                                                                                                            </ItemTemplate>
                                                                                                        </asp:TemplateField>

                                                                                                        <%--<asp:BoundField ItemStyle-Width="150px" DataField="PBM" HeaderText="PBM"  />--%>



                                                                                                        <asp:TemplateField HeaderText="Application Name" ItemStyle-Width="150px">
                                                                                                            <ItemTemplate>
                                                                                                                <div>
                                                                                                                    <%# Eval("Application") %>
                                                                                                                </div>
                                                                                                            </ItemTemplate>
                                                                                                        </asp:TemplateField>

                                                                                                        <%--<asp:BoundField ItemStyle-Width="150px" DataField="Application" HeaderText="Application Name" />--%>


                                                                                                        <asp:TemplateField HeaderText="Release Name" ItemStyle-Width="150px">
                                                                                                            <ItemTemplate>
                                                                                                                <div>
                                                                                                                    <%# Eval("RELEASENAME") %>
                                                                                                                </div>
                                                                                                            </ItemTemplate>
                                                                                                        </asp:TemplateField>

                                                                                                        <%-- <asp:BoundField ItemStyle-Width="150px" DataField="RELEASENAME" HeaderText="Release Name" />--%>

                                                                                                        <asp:TemplateField HeaderText="CURRENT PHASE" ItemStyle-Width="150px">
                                                                                                            <ItemTemplate>
                                                                                                                <div>
                                                                                                                    <%# Eval("CURRENTPHASE") %>
                                                                                                                </div>
                                                                                                            </ItemTemplate>
                                                                                                        </asp:TemplateField>

                                                                                                        <asp:TemplateField HeaderText="Requirement Quality Score" ItemStyle-Width="150px" ItemStyle-HorizontalAlign="Center">
                                                                                                            <ItemTemplate>
                                                                                                                <div>
                                                                                                                    <%# Eval("req_quality_score") %>
                                                                                                                </div>
                                                                                                            </ItemTemplate>
                                                                                                        </asp:TemplateField>

                                                                                                        <%--<asp:BoundField ItemStyle-Width="150px" DataField="req_quality_score" HeaderText="Requirement Quality Score" />--%>

                                                                                                        <asp:TemplateField HeaderText="Requirement Stability Score" ItemStyle-Width="150px" ItemStyle-HorizontalAlign="Center">
                                                                                                            <ItemTemplate>
                                                                                                                <div>
                                                                                                                    <%# Eval("req_stab_score") %>
                                                                                                                </div>
                                                                                                            </ItemTemplate>
                                                                                                        </asp:TemplateField>

                                                                                                        <%--   <asp:BoundField ItemStyle-Width="150px" DataField="req_stab_score" HeaderText="Requirement Stability Score" />--%>

                                                                                                        <asp:TemplateField HeaderText="Design Review Coverage Score" ItemStyle-Width="150px" ItemStyle-HorizontalAlign="Center">
                                                                                                            <ItemTemplate>
                                                                                                                <div>
                                                                                                                    <%# Eval("des_rev_cov_score") %>
                                                                                                                </div>
                                                                                                            </ItemTemplate>
                                                                                                        </asp:TemplateField>

                                                                                                        <%--<asp:BoundField ItemStyle-Width="150px" DataField="des_rev_cov_score" HeaderText="Design Review Coverage Score" />--%>


                                                                                                        <asp:TemplateField HeaderText="Code Review Coverage Score" ItemStyle-Width="150px" ItemStyle-HorizontalAlign="Center">
                                                                                                            <ItemTemplate>
                                                                                                                <div>
                                                                                                                    <%# Eval("code_rev_cov_score") %>
                                                                                                                </div>
                                                                                                            </ItemTemplate>
                                                                                                        </asp:TemplateField>

                                                                                                        <%-- <asp:BoundField ItemStyle-Width="150px" DataField="code_rev_cov_score" HeaderText="Code Review Coverage Score" />
                                                                                                        --%>

                                                                                                        <asp:TemplateField HeaderText="Unit Test Coverage Score" ItemStyle-Width="150px" ItemStyle-HorizontalAlign="Center">
                                                                                                            <ItemTemplate>
                                                                                                                <div>
                                                                                                                    <%# Eval("unit_test_cov_score") %>
                                                                                                                </div>
                                                                                                            </ItemTemplate>
                                                                                                        </asp:TemplateField>
                                                                                                        <%--   <asp:BoundField ItemStyle-Width="150px" DataField="unit_test_cov_score" HeaderText="Unit Test Coverage Score" />--%>

                                                                                                        <asp:BoundField DataField="on_time_del_score" HeaderText="On Time Delivery Score" ItemStyle-Width="150px" ItemStyle-HorizontalAlign="Center" />

                                                                                                        <asp:BoundField DataField="delivery_defect_density_score" HeaderText="Delivery Defect Density Score" ItemStyle-Width="150px" ItemStyle-HorizontalAlign="Center" />

                                                                                                    </Columns>
                                                                                                </asp:GridView>

                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                    <%--        <div style="width:1100px;background-color:red">--%>
                                                                                </asp:Panel>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:BoundField ItemStyle-Width="150px" ItemStyle-CssClass="itemStyle1" />
                                                                        <asp:TemplateField HeaderText="Status" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                                                                            <ItemTemplate>
                                                                                <p class="<%# Eval("RAG").ToString() == "NA" ? "NA" : Eval("RAG").ToString() %>"><span class="fa fa-circle"></span></p>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <%-- <asp:BoundField ItemStyle-Width="150px" ItemStyle-CssClass="itemStyle1" />--%>

                                                                        <asp:BoundField ItemStyle-Width="150px" ItemStyle-CssClass="itemStyle1" />

                                                                        <asp:BoundField ItemStyle-Width="150px" ItemStyle-CssClass="itemStyle1" ItemStyle-HorizontalAlign="Center" />

                                                                        <asp:BoundField ItemStyle-Width="150px" ItemStyle-CssClass="itemStyle1" ItemStyle-HorizontalAlign="Center" />

                                                                        <asp:BoundField ItemStyle-Width="150px" ItemStyle-CssClass="itemStyle1" ItemStyle-HorizontalAlign="Center" />

                                                                        <asp:BoundField ItemStyle-Width="150px" ItemStyle-CssClass="itemStyle1" ItemStyle-HorizontalAlign="Center" />

                                                                        <asp:BoundField ItemStyle-Width="150px" ItemStyle-CssClass="itemStyle1" ItemStyle-HorizontalAlign="Center" />

                                                                        <asp:BoundField ItemStyle-Width="150px" ItemStyle-CssClass="itemStyle1" ItemStyle-HorizontalAlign="Center" />

                                                                        <asp:BoundField ItemStyle-Width="150px" ItemStyle-CssClass="itemStyle1" ItemStyle-HorizontalAlign="Center" />
                                                                        <%--  <asp:BoundField ItemStyle-Width="150px"  ItemStyle-CssClass="itemStyle1"/>  
        <asp:BoundField ItemStyle-Width="150px"  ItemStyle-CssClass="itemStyle1"/>
        <asp:BoundField ItemStyle-Width="150px"  ItemStyle-CssClass="itemStyle1"/>
        <asp:BoundField ItemStyle-Width="150px"  ItemStyle-CssClass="itemStyle1"/>
        <asp:BoundField ItemStyle-Width="150px"  ItemStyle-CssClass="itemStyle1"/>
        <asp:BoundField ItemStyle-Width="150px"  ItemStyle-CssClass="itemStyle1"/>
        <asp:BoundField ItemStyle-Width="150px"  ItemStyle-CssClass="itemStyle1"/>
        <asp:BoundField ItemStyle-Width="150px"  ItemStyle-CssClass="itemStyle1"/>
        <asp:BoundField ItemStyle-Width="150px"  ItemStyle-CssClass="itemStyle1"/>--%>
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </div>

                                                            <div role="tabpanel" class="tab-pane" id="tab2">

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

                                                            <div role="tabpanel" class="tab-pane" id="tab3">

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
                                                                    <cc1:ModalPopupExtender ID="ModalPopupExtender2" runat="server" PopupControlID="Current_Future_PopUp_Window"
                                                                        TargetControlID="popid">
                                                                    </cc1:ModalPopupExtender>
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
                                                            <div role="tabpanel" class="tab-pane" id="tab4">
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
                                                                    <cc1:ModalPopupExtender ID="ModalPopupExtender_FUTURE" runat="server" PopupControlID="Future_PopUp_Window"
                                                                        TargetControlID="popid_F">
                                                                    </cc1:ModalPopupExtender>
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
                                                    </div>
                                                    <asp:HiddenField ID="TabName" runat="server" />
                                                </div>
                                            </div>

                                            <!-- Progressing Status - Image -->
                                            <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1">

                                                <ProgressTemplate>

                                                    <div class="updateprogress" style="text-align: center; color: white;">

                                                        <div>

                                                            <img alt="Progressing" src="loader.gif" />

                                                        </div>

                                                    </div>

                                                </ProgressTemplate>

                                            </asp:UpdateProgress>


                                        </ContentTemplate>

                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="Search_Button" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="Reset_Button" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="gvCustomers" EventName="PageIndexChanging" />

                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>

       

                            </div>
                        </div>
                    </div>
                </div>


            </div>
        </ContentTemplate>

        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ddl_ReleaseMonth" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddl_PBM" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddl_Portfolio" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddl_Application" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddl_Releases" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="Reset_Button" EventName="Click" />
        </Triggers>

    </asp:UpdatePanel>





    <asp:Panel ID="ModalPopUp" runat="server">

        <asp:Button ID="popup" Text="popup" runat="server" Style="display: none;" />

        <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" PopupControlID="Modal_Div"
            TargetControlID="popup" BackgroundCssClass="modalBackground">
            <%--CancelControlID="btn_close"--%>
        </cc1:ModalPopupExtender>


        <asp:UpdatePanel ID="Modal_Div" CssClass="modalPopupStyle" runat="server" style="height: auto; max-height: 600px; width: 660px; border: 2px solid white; padding: 5px; background-color: #FFFFFF; display: none;">

            <ContentTemplate>
                <div class="panel panel-primary">


                    <div class="panel-heading">
                        <div class="row">
                            <div class="col-xs-6">
                                <div style="vertical-align: middle;">
                                    Red Project List
                                </div>
                            </div>
                            <div class="col-xs-6">
                                <div class="WhiteColor" style="vertical-align: middle;">
                                    <asp:LinkButton runat="server" CssClass="pull-right" ID="close_popup" ForeColor="Black" OnClick="btn_close_Click"><span class="fa fa-close"></span></asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </div>

                    <table>

                        <tr>

                            <th style="background-color: #FFFFFF; border: 1px solid black; width: 56px; margin: 7px; padding: 6px; text-align: center;"></th>

                            <th style="background-color: #FFFFFF; border: 1px solid black; width: 323px; margin: 7px; padding: 6px;">Project / Release Name</th>

                            <th style="background-color: #FFFFFF; border: 1px solid black; width: 164px; margin: 7px; padding: 6px;">PBM</th>


                            <th style="background-color: #FFFFFF; border: 1px solid black; width: 56px; margin: 7px; padding: 6px;">RAG </th>

                        </tr>

                    </table>

                    <div class="panel-body">
                        <div>

                            <asp:GridView ID="Modal_GridView" runat="server" AutoGenerateColumns="false"
                                CssClass="tableSmall table-bordered table-responsive table-hover test"
                                OnPreRender="Modal_GridView_PreRender">

                                <Columns>

                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50px" HeaderStyle-Width="50px">
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="Check_All" runat="server" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <div>
                                                <asp:CheckBox ID="chk1" Checked="false" runat="server" />
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Release Name" ItemStyle-Width="300px" HeaderStyle-Width="300px">
                                        <ItemTemplate>
                                            <div>
                                                <%# Eval("RELEASENAME") %>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="PBM" ItemStyle-Width="150px" HeaderStyle-Width="150px">
                                        <ItemTemplate>
                                            <div>
                                                <%# Eval("PBM") %>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <%--    <asp:TemplateField HeaderText="Email">
                <ItemTemplate>
                    <div style="width: 200px; overflow-wrap:break-word;  height:auto; text-align:left;">
                        <%# Eval("Email") %>
                    </div>
                </ItemTemplate>
            </asp:TemplateField>--%>

                                    <asp:TemplateField HeaderText="RAG Status" ItemStyle-Width="100px" HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <p class="<%# Eval("RAG").ToString() == "RED" ? "RedColor" : "GreenColor" %>"><span class="fa fa-circle"></span></p>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>
                                <HeaderStyle CssClass="headerFixed" />
                            </asp:GridView>

                        </div>

                        <div style="padding: 10px;">
                            <asp:LinkButton ID="Email" runat="server" CssClass="btn btn-sm btn-success" OnClick="Email_Send">
                                    <span class="fa fa-envelope"></span>&nbsp;Send Email
                            </asp:LinkButton>
                            <asp:LinkButton ID="btn_close" runat="server" CssClass="btn btn-sm btn-success" OnClick="btn_close_Click">
                                    <span class="fa fa-close"></span>&nbsp;Close
                            </asp:LinkButton>
                        </div>
                    </div>

                </div>



            </ContentTemplate>

            <Triggers>
                <%--                <asp:AsyncPostBackTrigger ControlID="Check_All" EventName="CheckedChanged" />--%>

                <asp:AsyncPostBackTrigger ControlID="btn_close" EventName="Click" />

                <asp:AsyncPostBackTrigger ControlID="close_popup" EventName="Click" />

                <asp:AsyncPostBackTrigger ControlID="Red_Project_List" EventName="Click" />

            </Triggers>

        </asp:UpdatePanel>

    </asp:Panel>

</asp:Content>





