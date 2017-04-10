$(document).ready(function () {

    $(' #financial-data1 tr:odd, #upcoming-release-data tr:odd, #financial-data2 tr:odd, #releases-two .release-data tr:odd').css('background', '#E4F5FF');
    $("#tabs").tabs();
    $("#metric-tabs").tabs();
    $("#accordion").accordion({

        autoHeight: false,
        active: false
    });
    $('.add').click(function () {
        var h = $(document).height();
        $('#black').css('height', h);
        $('#black').css('z-index', '4000');
        $('#black').css('display', 'block');
        $('.pop-up-content').css('display', 'block');
        $('#black').css('visibility', 'visible');
        return true;
    });

    $('#black').click(function (e) {
        if ($(e.target).attr('id') == "black") { $('#black').css('display', 'none'); }
    });

    $(document).keyup(function (e) { if (e.keyCode == 27 || e.charCode == 27) { $('#black').css('display', 'none'); } });


    $(' #upcoming-release-data tr td a').mouseover(function () {
        $('#application-details-popup').show();
    });
    $(' #upcoming-release-data tr td a').mouseout(function () {
        $('#application-details-popup').hide();
    });


    $('.close-img').click(function () {
        $('.pop-up-content').hide();
        $('#black').hide();
    });
    $('.application-details-popup').tooltip({
        content: '<span class="arrow" style="position:relative; top:50px; left:-3px"><img src="images/tooltip-arrow.png" alt=""/></span><div class="popup-wrap" style="background: #555; height:110px;width:370px; border-radius: 5px; left:16px; top:10px; filter: alpha(opacity=70) !important; position:absolute; opacity: 0.7; behavior: url(javascripts/PIE.htc); "></div><div class="popup-container" style="font-family:arial !important; background-clip:padding-border; left:13px; border-radius:5px; position:relative; top:-13px; "><h2 style="border-radius:3px 3px 0 0;">MetClarity-Release</h2><div class="popup-content" style="border-radius: 0 0 3px 3px;" > <h3>Features</h3><p>This release involves changes in the existing analytic module so it can extract, transform, store and present data from redisigned HFM</p></div></div>',
        position: {
            my: 'left center',
            at: 'right center'

        }

    });
    popupcontainer = $('.popup-container').height() + 20;
    $('.popup-wrap').css('height', popupcontainer + 'px');

    /*** Changes for Metrics -Start***/

    /***For Menu- Header Color Change -Start 18-05-2014***/
   // $($("#tabs").children().children()[4]).children().click();
    $(".nav-header a").click(function () {
        $(".nav-header a").css("color", "#fff");
        $("#" + $(this).parent().parent().prev()[0].id).css("color", "#000");
        $("#" + $(this)[0].id).css("color", "#000");
    });
    /***For Menu- Header Color Change -End 18-05-2014***/

    privilageReset();
    $(".first-child-node").click();

    $.each($(".tree-pnl-cls ul").prev(), function (i, val) {
        $(val).find('span')[0].className = 'tree-icon-cls tree-expand-img';
    });

    $(".breadcrumb").children().children().last().css("text-decoration", "underline");
    /*** Changes for Metrics -End***/
});
/*** Changes for Metrics -Start***/
function privilageAction(scope, level) {
    $(function () {

        $(".tree-pnl-parent-list").find('li').removeClass('tree-pnl-cls-selected');
        $(scope).parent().addClass('tree-pnl-cls-selected');
        privilageReset();

        $("#deliveryMetrics").show();
        $("#financialMetrics").show();
        $("#operationalMetrics").show();
        $("#codequality").show();
        $("#deliverymetricsTab").css("display", "inline-block");
        $("#deliveryMetrics").children().click();
        /*$("#financial-metrics").css("display","none");
        $("#operational-metrics").css("display","none");
        $("#code-quality").css("display","none");*/
        

    });

}

function privilageReset() {

    //$("#deliveryMetrics").hide();
  //  $("#financialMetrics").hide();
//    $("#operationalMetrics").hide();
  //  $("#codequality").hide();
   // $("#delivery-metrics").css("display", "none");
  //  $("#financial-metrics").css("display", "none");
   // $("#operational-metrics").css("display", "none");
  //  $("#code-quality").css("display", "none");

}
function nodeToggle(scope) {

    if ($(scope).hasClass('tree-expand-img')) {
        $(scope).removeClass('tree-expand-img');
        $(scope).addClass('tree-collapsed-img');
        $(scope).parent().next().slideToggle();
    } else if ($(scope).hasClass('tree-collapsed-img')) {
        $(scope).removeClass('tree-collapsed-img');
        $(scope).addClass('tree-expand-img');
        $(scope).parent().next().slideToggle()
    }

}

function treePanelToggle(scope) {

    if ($(scope).hasClass('tree-panel-toggle-expand-cls')) {
        $(scope).removeClass('tree-panel-toggle-expand-cls');
        $(scope).addClass('tree-panel-toggle-collapse-cls');
        $(scope).parent().next().slideToggle();
    } else if ($(scope).hasClass('tree-panel-toggle-collapse-cls')) {
        $(scope).removeClass('tree-panel-toggle-collapse-cls');
        $(scope).addClass('tree-panel-toggle-expand-cls');
        $(scope).parent().next().slideToggle();
    }

}
/*** Changes for Metrics -End***/	