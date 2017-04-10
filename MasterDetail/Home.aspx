<%@ Page Title="About" Language="C#"  AutoEventWireup="true" CodeFile="Home.aspx.cs" Inherits="Home" %>

<!DOCTYPE html >
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=Edge">
    <title>Metlife Delivery Dashboard - Home</title>
    <link rel="stylesheet" type="text/css" href="css/jcarousel.connected-carousels.css">
    <link rel="stylesheet" type="text/css" href="css/style.css">
    <!--[if gte IE 7]>
	<link rel="stylesheet" type="text/css" href="css/ie7-and-up.css" />
<![endif]-->
    <!--[if IE 8]>     <html class="ie8"> <![endif]-->
    <script type="text/javascript" src="javascripts/jquery.js"></script>
    <script type="text/javascript" src="javascripts/jquery.jcarousel.min.js"></script>
    <script type="text/javascript" src="javascripts/jcarousel.connected-carousels.js"></script>
    <script language="javascript" type="text/javascript">
        $(function () {


            $('#wrapper-container').css('min-height', '510px');
            $(window).on("resize load", function (event) {

                var window_height = $(window).height();
                if (window_height >= 600) {
                    $('#wrapper-container').css('min-height', '400px');
                    var main_container_height = window_height - 152;

                    if (main_container_height < 400) {
                        main_container_height = 400;
                        $('#wrapper-container').css('height', main_container_height);
                        var container_height = $('.contanier').height();
                        var sub = main_container_height - container_height;
                        $('.contanier').css('margin-top', ((sub / 2) + 1));
                    } else {
                        $('#wrapper-container').css('height', main_container_height);
                        var container_height = $('.contanier').height();
                        var sub = main_container_height - container_height;
                        $('.contanier').css('margin-top', ((sub / 2) + 1));
                    }

                }

                if (window_height < 600) {
                    $('#wrapper-container').css('height', '510px');
                    var main_container_height = $('#wrapper-container').innerHeight();
                    var container_height = $('.contanier').innerHeight();
                    var sub = main_container_height - container_height;
                    $('.contanier').css('margin-top', ((sub / 2) + 1));

                    //}
                }
            });
            $('.EAD-hidden-cls,.PM-hidden-cls').css('width', '0');
            $('.EAD-hidden-cls div ,.PM-hidden-cls div').css('padding', '15px');
            $('.EAD-hidden-cls div,.PM-hidden-cls div').css('width', '0');
        });
        function hiddenEADBtn() {

            $('.EAD-hidden-cls').click();
        }
        function hiddenPMBtn() {

            $('.PM-hidden-cls').click();
        }
    </script>
 
</head>
<body>
    <form id="form1" runat="server">
    <div class="wrapper">
        <div id="wrapper-header">
            <div id="wrapper-sub-header">
                <div class="header-part">
                    <div class="logo-left-part">
                        <a href="">
                            <img src="images/cognizant-logo.png" alt=""></a>
                    </div>
                    <div class="logo-right-part">
                        <a href=""></a>
                        <h1><span>Delivery
                            <br>
                            Dashboard</span></h1>
                    </div>
                </div>
            </div>
        </div>
        <div id="wrapper-container">
            <div class="contanier">
                <div class="connected-carousels">
                    <div class="stage">
                        <div class="carousel carousel-stage" id="a">
                            <ul>

                                <li>
                                    <span class="carousel-content-align">
                                        <a href="PreviousMonthRelease.aspx?DIV=EAD&LOB=0">
                                            <div class="enter-application">
                                                <img src="images/ead-summary-icon.png" alt="">
                                                <span>Summary View</span>
                                            </div>
                                        </a>
                                        <a href="#" onclick="hiddenEADBtn();">
                                            <div class="investments">
                                                <img src="images/portfolio-details-icon.png" alt="">
                                                <span>Portfolio View</span>
                                            </div>
                                        </a>
                                    </span>
                                </li>
                                <li>
                                    <span class="carousel-content-align">
                                        <a href="#">
                                            <div class="corp-systems">
                                                <img src="images/product-manufacture-icon.png" alt="">
                                                <span>Product Manufacture</span>
                                            </div>
                                        </a>
                                        <a href="#">
                                            <div class="channel-int">
                                                <img src="images/comp-compliance-icon.png" alt="">
                                                <span>Comp & Compliance</span>
                                            </div>
                                        </a>
                                        <a href="#">
                                            <div class="emp-benefits">
                                                <img src="images/annuity-investments-icon.png" alt="">
                                                <span>Annuity & Investment</span>
                                            </div>
                                        </a>
                                       
                                         <a href="SolutionDevelopment.aspx">
                                            <div class="enter-application">
                                                <img src="images/enterprise-application-icon.png" alt="">
                                                <span>Solution Delivery & Business Engagement</span>
                                            </div>
                                        </a>

                                  
                                        
                                        <asp:LinkButton id="EAD" runat="server" OnClick="page_redirect" >

                                                <div class="enter-application">
                                              
                                                <img src="images/enterprise-application-icon.png" alt="">
                                                
                                                <span>EAD</span>

                                                    </div>

                                        </asp:LinkButton>

                                        <asp:LinkButton id="RAD" runat="server" OnClick="page_redirect" >

                                                <div class="emp-benefits">
                                              
                                                <img src="images/portfolio-details-icon.png" alt=""/>
                                                
                                                <span>RAD</span>

                                                    </div>

                                        </asp:LinkButton>

                                      
                                   
                                    </span>
                                </li>
                                <li>
                                    <span class="carousel-content-align">
                                        <a href="PreviousMonthRelease.aspx?LOB=1">
                                            <div class="corp-systems">
                                                <img src="images/corporate-systems-icon.png" alt="">
                                                <span>Corporate Systems</span>
                                            </div>
                                        </a>
                                        <a href="PreviousMonthRelease.aspx?LOB=2">
                                            <div class="channel-int">
                                                <img src="images/global-employee-benefits-icon.png" alt="">
                                                <span>Global Employee Benefits</span>
                                            </div>
                                        </a>
                                        <a href="PreviousMonthRelease.aspx?LOB=3">
                                            <div class="emp-benefits">
                                                <img src="images/channels-integration-icon.png" alt="">
                                                <span>Channels and Intergration</span>
                                            </div>
                                        </a>
                                        <a href="PreviousMonthRelease.aspx?LOB=4">
                                            <div class="enter-application">
                                                <img src="images/enterprise-application-icon.png" alt="">
                                                <span>Enterprise Application & Information Services</span>
                                            </div>
                                        </a>
                                        <a href="PreviousMonthRelease.aspx?LOB=5">
                                            <div class="investments">
                                                <img src="images/investments-icon.png" alt="">
                                                <span>Investments</span>
                                            </div>
                                        </a>
                                        <!--<a href="ead-key-releases.html"><div class="corporate-PM"><img src="images/corporate-pm-icon.png" alt="">
										<span>Corporate PM</span></div></a>-->
                                        <a href="#" onclick="hiddenPMBtn();">
                                            <div class="prod-management">
                                                <img src="images/sample-icon.png" alt="">
                                                <span>Production Management</span>
                                            </div>
                                        </a>
                                    </span>

                                </li>
                                <li>
                                    <span class="carousel-content-align">
                                        <a href="#">
                                            <div class="channel-int">
                                                <img src="images/corporate-pm-icon.png" alt="">
                                                <span>Corporate PM</span>
                                            </div>
                                        </a>
                                        <a href="#">
                                            <div class="corporate-PM">
                                                <img src="images/investment-pm-icon.png" alt="">
                                                <span>Investment PM</span>
                                            </div>
                                        </a>
                                    </span>
                                </li>
                                <!-- <li>
									<span class="carousel-content-align"> 
										<a href="ead-key-releases.html"><div class="corp-systems"><img src="images/corporate-systems-icon.png" alt="">
										<span>Corporate Systems</span></div></a>
										<a href="#"><div class="emp-benefits"><img src="images/global-employee-benefits-icon.png" alt="">
										<span>Global Employee Benefits</span></div></a>
										<a href="#"><div class="channel-int"><img src="images/channels-integration-icon.png"  alt="">
										<span>Channels and Intergration</span></div></a>
										<a href="#"><div class="enter-application"><img src="images/enterprise-application-icon.png"  alt="">
										<span>Enterprise Application & Information Services</span>
										</div></a>
										<a href="#"><div class="investments"><img src="images/investments-icon.png" alt="">
										<span>Investments</span>
										</div></a>
										</span>
									</li>
									<li>
									<span class="carousel-content-align">
										<a href="#"><div class="corp-systems"><img src="images/corporate-systems-icon.png" alt="">
										<span>Corporate Systems</span></div></a>
										
										<a href="#"><div class="enter-application"><img src="images/enterprise-application-icon.png"  alt="">
										<span>Enterprise Application & Information Services</span>
										</div></a>
										<a href="#"><div class="investments"><img src="images/investments-icon.png" alt="">
										<span>Investments</span>
										</div></a>
										</span>
									</li>-->

                            </ul>
                        </div>
                    </div>

                    <div class="navigation">
                        <!--<a href="#" class="prev prev-navigation"><img src="images/left-arrow.png" alt=""></a>
		                    <a href="#" class="next next-navigation"><img src="images/right-arrow.png"  alt=""></a>-->
                        <div class="carousel carousel-navigation">
                            <ul>

                                <li class="EAD-cls">
                                    <div>Enterprise Application Development</div>
                                </li>
                                <li class="RAD-cls">
                                    <div>Regional Application Development</div>
                                </li>
                                <li class="EAD-hidden-cls">
                                    <div></div>
                                </li>
                                <li class="PM-hidden-cls">
                                    <div></div>
                                </li>
                                <!--<li class="hidden-cls"><div>Production Management</div></li>
		                            <li class="hidden-cls"><div>Investments</div></li>-->

                            </ul>
                        </div>
                    </div>
                </div>
                <!--connected carousel-->
            </div>
            <!--contanier-->
        </div>
        <!--wrapper-container-->
        <div id="wrapper-footer">
            <div id="wrapper-sub-footer">
                <div class="footer-logo-left">
                    <p>Cognizant Technology Solutions &copy; 2016
                        <%--<asp:Button ID="Button1" runat="server"  Text="Button" />--%>
                    </p>
                </div>
                <div class="footer-logo-right">
                    <div class="metlife-logo">
                        <a href="">
                            <img src="images/metlife-logo.png" alt=""></a>
                    </div>
                    <!--<div class="cartoon-logo">
						<a href=""><img src="images/metlife-snoopy.png" alt=""></a>
					</div>-->
                </div>
            </div>
        </div>
        <!--wrapper-footer-->
    </div>
    </form>
</body>
</html>
