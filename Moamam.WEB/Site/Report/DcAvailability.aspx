<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterPageTabSub.master" AutoEventWireup="true" CodeFile="DcAvailability.aspx.cs" Inherits="Site_Report_DcAvailability" %>
<%--<%@ Register Src="~/UserControls/ucPagingJS.ascx" TagName="ucPaging" TagPrefix="uc1" %>--%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHead" runat="Server">
    
<!-- 1.LoadPage -->
<script type="text/javascript">
    var __clickCnt = 0;
    $(document).ready(function () {
        Initialize();
        jsfn_Search();
    });
</script>
<!-- 2.Init -->
<script type="text/javascript">
    function Initialize() {
        $('#btnSearch').click(function () { jsfn_Search(); });
        //$('#Ly_grid1Scroll').css('height', (parseInt($('#Ly_grid1Scroll').css('height').replace('px','')) + 60) + 'px');
    }
</script>
<!-- 3.Validation  -->
<script type="text/javascript">
</script>
<!-- 4. Custom Function  -->
<script type="text/javascript">

    function jsfn_Search() {
        var _OrderDATE = $("#<%= txtOrderDATE.ClientID %>").val().replace(/-/g, '');
        if (_OrderDATE == '') { jsfn_alert('발주일을 선택해 주세요.'); return false; }

        jsfn_progressBarMst('Y');

        var _ucPageNo = $("#LyCus_hidPageNo").val();
        if (_ucPageNo == '') { _ucPageNo = '1'; }
        var _ucRowCount = $("#LyCus_hidRowCnt").val();
        if (_ucRowCount == '') { _ucRowCount = '30'; }

        var SpName = "SP_WEB_Site_Report_DcAvailability_R";
        var SpParams = "YYYYMMDD" + '▥' + _OrderDATE + '▤';
        SpParams = SpParams + "SMODE" + '▥' + 'SELECT';

        $.ajax({
            url: '/Site/Data/Data.aspx'
        , type: "post"
        , async: false
        , data: {
            fnName: "CommonCallSpGetJson"
            , SpName: SpName
            , SpParams: SpParams
        }
        , dataType: "json"
        , success: function (data) { jsfn_SetGrid(data); }
        , error: function (x, e) { jsfn_AjaxError(x, e); jsfn_progressBarMst('N'); }
        });
    }

    function jsfn_SetGrid(dataRes) {
        $("#dvGrid1 > tbody > tr").remove();
        var _TOTAL_COUNT = 0;
        var gData = dataRes;
        var html = '';
        var html2 = '';
        var html3 = '';
        if (gData.length == 0) {
            //jsfn_SetPage(_TOTAL_COUNT); // ucPagingJS.ascx
            var noData_html = '<tr><td colspan="25" style="width:100%;height:25px;text-align:center;"><b><font color="red">조회된 결과가 없습니다.</font></b></td></tr>';
            $('#dvGrid1 > tbody:last').append(noData_html);
            jsfn_progressBarMst('N');
            return false;
        } else {
            //_TOTAL_COUNT = gData[0].TOTAL_COUNT;
        }

        // T3 가용성 값의 변수
        var Ta_YYYY_TCNT = 0, Ta_Q1TCNT = 0, Ta_Q2TCNT = 0, Ta_Q3TCNT = 0, Ta_Q4TCNT = 0, Ta_M1TCNT = 0, Ta_M2TCNT = 0, Ta_M3TCNT = 0, Ta_M4TCNT = 0, Ta_M5TCNT = 0, Ta_M6TCNT = 0, Ta_M7TCNT = 0, Ta_M8TCNT = 0, Ta_M9TCNT = 0, Ta_M10TCNT = 0, Ta_M11TCNT = 0, Ta_M12TCNT = 0, Ta_W1TCNT = 0, Ta_W2TCNT = 0, Ta_W3TCNT = 0, Ta_W4TCNT = 0, Ta_W5TCNT = 0;
        
        var _NormalCnt = 0, _NormalCntF = 0;
        var _PromotionCnt = 0, _PromotionCntF = 0;
        for (var i = 0; i < gData.length; i++) { if (gData[i].PROM_YN == 'N') { _NormalCnt++; } if (gData[i].PROM_YN == 'Y') { _PromotionCnt++; } }

        for (var i = 0; i < gData.length; i++) {

            if (i == 0) { html = html + '<tr style="background-color:#ffffff;height:5px;"><td colspan="25" style="background-color:#ffffff;height:5px;"></td></tr><tr><td colspan="25" style="text-align:left;background-color:#ffffff;"><b>Total SKU<b></td></tr>'; }

            html = html + '<tr class="tr_c">';
            if (gData[i].PROM_YN == 'N' && _NormalCntF==0) { html = html + '<td class="t_c" rowspan="' + _NormalCnt + '"> Normal </td>'; }
            if (gData[i].PROM_YN == 'Y' && _PromotionCntF == 0) { html = html + '<td class="t_c" rowspan="' + _PromotionCnt + '"> Promotion </td>'; }

            if (gData[i].WH == null) { if (gData[i].PROM_YN == null) { html = html + '<td class="t_c" colspan="2" style="background-color:#f3b03d" > Total </td>'; } else { html = html + '<td class="t_c" style="background-color:#fdf7d5" > Total </td>'; } } else { html = html + '<td class="t_c">' + gData[i].WH + '</td>'; }
            html = html + '<td class="t_c"></td>';
            html = html + '<td class="t_c" style="text-align:right;padding-right:5px;">' + gData[i].YYYY_TCNT.toLocaleString() + '</td>';
            html = html + '<td class="t_c" style="text-align:right;padding-right:5px;">' + gData[i].Q1TCNT.toLocaleString() + '</td>';
            html = html + '<td class="t_c" style="text-align:right;padding-right:5px;">' + gData[i].Q2TCNT.toLocaleString() + '</td>';
            html = html + '<td class="t_c" style="text-align:right;padding-right:5px;">' + gData[i].Q3TCNT.toLocaleString() + '</td>';
            html = html + '<td class="t_c" style="text-align:right;padding-right:5px;">' + gData[i].Q4TCNT.toLocaleString() + '</td>';
            html = html + '<td class="t_c" style="text-align:right;padding-right:5px;">' + gData[i].M1TCNT.toLocaleString() + '</td>';
            html = html + '<td class="t_c" style="text-align:right;padding-right:5px;">' + gData[i].M2TCNT.toLocaleString() + '</td>';
            html = html + '<td class="t_c" style="text-align:right;padding-right:5px;">' + gData[i].M3TCNT.toLocaleString() + '</td>';
            html = html + '<td class="t_c" style="text-align:right;padding-right:5px;">' + gData[i].M4TCNT.toLocaleString() + '</td>';
            html = html + '<td class="t_c" style="text-align:right;padding-right:5px;">' + gData[i].M5TCNT.toLocaleString() + '</td>';
            html = html + '<td class="t_c" style="text-align:right;padding-right:5px;">' + gData[i].M6TCNT.toLocaleString() + '</td>';
            html = html + '<td class="t_c" style="text-align:right;padding-right:5px;">' + gData[i].M7TCNT.toLocaleString() + '</td>';
            html = html + '<td class="t_c" style="text-align:right;padding-right:5px;">' + gData[i].M8TCNT.toLocaleString() + '</td>';
            html = html + '<td class="t_c" style="text-align:right;padding-right:5px;">' + gData[i].M9TCNT.toLocaleString() + '</td>';
            html = html + '<td class="t_c" style="text-align:right;padding-right:5px;">' + gData[i].M10TCNT.toLocaleString() + '</td>';
            html = html + '<td class="t_c" style="text-align:right;padding-right:5px;">' + gData[i].M11TCNT.toLocaleString() + '</td>';
            html = html + '<td class="t_c" style="text-align:right;padding-right:5px;">' + gData[i].M12TCNT.toLocaleString() + '</td>';
            html = html + '<td class="t_c" style="text-align:right;padding-right:5px;">' + gData[i].W1TCNT.toLocaleString() + '</td>';
            html = html + '<td class="t_c" style="text-align:right;padding-right:5px;">' + gData[i].W2TCNT.toLocaleString() + '</td>';
            html = html + '<td class="t_c" style="text-align:right;padding-right:5px;">' + gData[i].W3TCNT.toLocaleString() + '</td>';
            html = html + '<td class="t_c" style="text-align:right;padding-right:5px;">' + gData[i].W4TCNT.toLocaleString() + '</td>';
            html = html + '<td class="t_c" style="text-align:right;padding-right:5px;">' + gData[i].W5TCNT.toLocaleString() + '</td>';
            html = html + '</tr>';



            if (i == 0) { html2 = html2 + '<tr style="background-color:#ffffff;height:5px;"><td colspan="25" style="background-color:#ffffff;height:5px;"></td></tr><tr><td colspan="25" style="text-align:left;background-color:#ffffff;"><b>In stock<b></td></tr>'; }
            html2 = html2 + '<tr class="tr_c">';
            if (gData[i].PROM_YN == 'N' && _NormalCntF == 0) { html2 = html2 + '<td class="t_c" rowspan="' + _NormalCnt + '"> Normal </td>'; }
            if (gData[i].PROM_YN == 'Y' && _PromotionCntF == 0) { html2 = html2 + '<td class="t_c" rowspan="' + _PromotionCnt + '"> Promotion </td>'; }
            if (gData[i].WH == null) { if (gData[i].PROM_YN == null) { html2 = html2 + '<td class="t_c" colspan="2" style="background-color:#f3b03d" > Total </td>'; } else { html2 = html2 + '<td class="t_c" style="background-color:#fdf7d5" > Total </td>'; } } else { html2 = html2 + '<td class="t_c">' + gData[i].WH + '</td>'; }
            html2 = html2 + '<td class="t_c"></td>';
            html2 = html2 + '<td class="t_c" style="text-align:right;padding-right:5px;">' + gData[i].YYYY_ICNT.toLocaleString() + '</td>';
            html2 = html2 + '<td class="t_c" style="text-align:right;padding-right:5px;">' + gData[i].Q1ICNT.toLocaleString() + '</td>';
            html2 = html2 + '<td class="t_c" style="text-align:right;padding-right:5px;">' + gData[i].Q2ICNT.toLocaleString() + '</td>';
            html2 = html2 + '<td class="t_c" style="text-align:right;padding-right:5px;">' + gData[i].Q3ICNT.toLocaleString() + '</td>';
            html2 = html2 + '<td class="t_c" style="text-align:right;padding-right:5px;">' + gData[i].Q4ICNT.toLocaleString() + '</td>';
            html2 = html2 + '<td class="t_c" style="text-align:right;padding-right:5px;">' + gData[i].M1ICNT.toLocaleString() + '</td>';
            html2 = html2 + '<td class="t_c" style="text-align:right;padding-right:5px;">' + gData[i].M2ICNT.toLocaleString() + '</td>';
            html2 = html2 + '<td class="t_c" style="text-align:right;padding-right:5px;">' + gData[i].M3ICNT.toLocaleString() + '</td>';
            html2 = html2 + '<td class="t_c" style="text-align:right;padding-right:5px;">' + gData[i].M4ICNT.toLocaleString() + '</td>';
            html2 = html2 + '<td class="t_c" style="text-align:right;padding-right:5px;">' + gData[i].M5ICNT.toLocaleString() + '</td>';
            html2 = html2 + '<td class="t_c" style="text-align:right;padding-right:5px;">' + gData[i].M6ICNT.toLocaleString() + '</td>';
            html2 = html2 + '<td class="t_c" style="text-align:right;padding-right:5px;">' + gData[i].M7ICNT.toLocaleString() + '</td>';
            html2 = html2 + '<td class="t_c" style="text-align:right;padding-right:5px;">' + gData[i].M8ICNT.toLocaleString() + '</td>';
            html2 = html2 + '<td class="t_c" style="text-align:right;padding-right:5px;">' + gData[i].M9ICNT.toLocaleString() + '</td>';
            html2 = html2 + '<td class="t_c" style="text-align:right;padding-right:5px;">' + gData[i].M10ICNT.toLocaleString() + '</td>';
            html2 = html2 + '<td class="t_c" style="text-align:right;padding-right:5px;">' + gData[i].M11ICNT.toLocaleString() + '</td>';
            html2 = html2 + '<td class="t_c" style="text-align:right;padding-right:5px;">' + gData[i].M12ICNT.toLocaleString() + '</td>';
            html2 = html2 + '<td class="t_c" style="text-align:right;padding-right:5px;">' + gData[i].W1ICNT.toLocaleString() + '</td>';
            html2 = html2 + '<td class="t_c" style="text-align:right;padding-right:5px;">' + gData[i].W2ICNT.toLocaleString() + '</td>';
            html2 = html2 + '<td class="t_c" style="text-align:right;padding-right:5px;">' + gData[i].W3ICNT.toLocaleString() + '</td>';
            html2 = html2 + '<td class="t_c" style="text-align:right;padding-right:5px;">' + gData[i].W4ICNT.toLocaleString() + '</td>';
            html2 = html2 + '<td class="t_c" style="text-align:right;padding-right:5px;">' + gData[i].W5ICNT.toLocaleString() + '</td>';
            html2 = html2 + '</tr>';
            
            if (gData[i].YYYY_ICNT == '') { Ta_M12TCNT = 0; } else { Ta_YYYY_TCNT = Math.round(parseFloat(gData[i].YYYY_TCNT) / parseFloat(gData[i].YYYY_ICNT) * 100) / 100.0; }
            if (gData[i].Q1ICNT == '') { Ta_Q1TCNT = 0; } else { Ta_Q1TCNT = Math.round(parseFloat(gData[i].Q1TCNT) / parseFloat(gData[i].Q1ICNT)*100)/100.0; }
            if (gData[i].Q2ICNT == '') { Ta_Q2TCNT = 0; } else { Ta_Q2TCNT = Math.round(parseFloat(gData[i].Q2TCNT) / parseFloat(gData[i].Q2ICNT) * 100) / 100.0; }
            if (gData[i].Q3ICNT == '') { Ta_Q3TCNT = 0; } else { Ta_Q3TCNT = Math.round(parseFloat(gData[i].Q3TCNT) / parseFloat(gData[i].Q3ICNT) * 100) / 100.0; }
            if (gData[i].Q4ICNT == '') { Ta_Q4TCNT = 0; } else { Ta_Q4TCNT = Math.round(parseFloat(gData[i].Q4TCNT) / parseFloat(gData[i].Q4ICNT) * 100) / 100.0; }
            if (gData[i].M1ICNT == '') { Ta_M1TCNT = 0; } else { Ta_M1TCNT = Math.round(parseFloat(gData[i].M1TCNT) / parseFloat(gData[i].M1ICNT) * 100) / 100.0; }
            if (gData[i].M2ICNT == '') { Ta_M2TCNT = 0; } else { Ta_M2TCNT = Math.round(parseFloat(gData[i].M2TCNT) / parseFloat(gData[i].M2ICNT) * 100) / 100.0; }
            if (gData[i].M3ICNT == '') { Ta_M3TCNT = 0; } else { Ta_M3TCNT = Math.round(parseFloat(gData[i].M3TCNT) / parseFloat(gData[i].M3ICNT) * 100) / 100.0; }
            if (gData[i].M4ICNT == '') { Ta_M4TCNT = 0; } else { Ta_M4TCNT = Math.round(parseFloat(gData[i].M4TCNT) / parseFloat(gData[i].M4ICNT) * 100) / 100.0; }
            if (gData[i].M5ICNT == '') { Ta_M5TCNT = 0; } else { Ta_M5TCNT = Math.round(parseFloat(gData[i].M5TCNT) / parseFloat(gData[i].M5ICNT) * 100) / 100.0; }
            if (gData[i].M6ICNT == '') { Ta_M6TCNT = 0; } else { Ta_M6TCNT = Math.round(parseFloat(gData[i].M6TCNT) / parseFloat(gData[i].M6ICNT) * 100) / 100.0; }
            if (gData[i].M7ICNT == '') { Ta_M7TCNT = 0; } else { Ta_M7TCNT = Math.round(parseFloat(gData[i].M7TCNT) / parseFloat(gData[i].M7ICNT) * 100) / 100.0; }
            if (gData[i].M8ICNT == '') { Ta_M8TCNT = 0; } else { Ta_M8TCNT = Math.round(parseFloat(gData[i].M8TCNT) / parseFloat(gData[i].M8ICNT) * 100) / 100.0; }
            if (gData[i].M9ICNT == '') { Ta_M9TCNT = 0; } else { Ta_M9TCNT = Math.round(parseFloat(gData[i].M9TCNT) / parseFloat(gData[i].M9ICNT) * 100) / 100.0; }
            if (gData[i].M10ICNT == '') { Ta_M10TCNT = 0; } else { Ta_M10TCNT = Math.round(parseFloat(gData[i].M10TCNT) / parseFloat(gData[i].M10ICNT) * 100) / 100.0; }
            if (gData[i].M11ICNT == '') { Ta_M11TCNT = 0; } else { Ta_M11TCNT = Math.round(parseFloat(gData[i].M11TCNT) / parseFloat(gData[i].M11ICNT) * 100) / 100.0; }
            if (gData[i].M12ICNT == '') { Ta_M12TCNT = 0; } else { Ta_M12TCNT = Math.round(parseFloat(gData[i].M12TCNT) / parseFloat(gData[i].M12ICNT) * 100) / 100.0; }
            if (gData[i].W1ICNT == '') { Ta_W1TCNT = 0; } else { Ta_W1TCNT = Math.round(parseFloat(gData[i].W1TCNT) / parseFloat(gData[i].W1ICNT) * 100) / 100.0; }
            if (gData[i].W2ICNT == '') { Ta_W2TCNT = 0; } else { Ta_W2TCNT = Math.round(parseFloat(gData[i].W2TCNT) / parseFloat(gData[i].W2ICNT) * 100) / 100.0; }
            if (gData[i].W3ICNT == '') { Ta_W3TCNT = 0; } else { Ta_W3TCNT = Math.round(parseFloat(gData[i].W3TCNT) / parseFloat(gData[i].W3ICNT) * 100) / 100.0; }
            if (gData[i].W4ICNT == '') { Ta_W4TCNT = 0; } else { Ta_W4TCNT = Math.round(parseFloat(gData[i].W4TCNT) / parseFloat(gData[i].W4ICNT) * 100) / 100.0; }
            if (gData[i].W5ICNT == '') { Ta_W5TCNT = 0; } else { Ta_W5TCNT = Math.round(parseFloat(gData[i].W5TCNT) / parseFloat(gData[i].W5ICNT) * 100) / 100.0; }


            if (i == 0) { html3 = html3 + '<tr style="background-color:#ffffff;height:5px;"><td colspan="25" style="background-color:#ffffff;height:5px;"></td></tr><tr><td colspan="25" style="text-align:left;background-color:#ffffff;"><b>Availability<b></td></tr>'; }
            html3 = html3 + '<tr class="tr_c">';
            if (gData[i].PROM_YN == 'N' && _NormalCntF == 0) { html3 = html3 + '<td class="t_c" rowspan="' + _NormalCnt + '"> Normal </td>'; }
            if (gData[i].PROM_YN == 'Y' && _PromotionCntF == 0) { html3 = html3 + '<td class="t_c" rowspan="' + _PromotionCnt + '"> Promotion </td>'; }
            if (gData[i].WH == null) { if (gData[i].PROM_YN == null) { html3 = html3 + '<td class="t_c" colspan="2" style="background-color:#f3b03d" > Total </td>'; } else { html3 = html3 + '<td class="t_c" style="background-color:#fdf7d5" > Total </td>'; } } else { html3 = html3 + '<td class="t_c">' + gData[i].WH + '</td>'; }
            html3 = html3 + '<td class="t_c"></td>';
            html3 = html3 + '<td class="t_c" style="text-align:right;padding-right:5px;">' + Ta_YYYY_TCNT.toLocaleString() + '</td>';
            html3 = html3 + '<td class="t_c" style="text-align:right;padding-right:5px;">' + Ta_Q1TCNT.toLocaleString() + '</td>';
            html3 = html3 + '<td class="t_c" style="text-align:right;padding-right:5px;">' + Ta_Q2TCNT.toLocaleString() + '</td>';
            html3 = html3 + '<td class="t_c" style="text-align:right;padding-right:5px;">' + Ta_Q3TCNT.toLocaleString() + '</td>';
            html3 = html3 + '<td class="t_c" style="text-align:right;padding-right:5px;">' + Ta_Q4TCNT.toLocaleString() + '</td>';
            html3 = html3 + '<td class="t_c" style="text-align:right;padding-right:5px;">' + Ta_M1TCNT.toLocaleString() + '</td>';
            html3 = html3 + '<td class="t_c" style="text-align:right;padding-right:5px;">' + Ta_M2TCNT.toLocaleString() + '</td>';
            html3 = html3 + '<td class="t_c" style="text-align:right;padding-right:5px;">' + Ta_M3TCNT.toLocaleString() + '</td>';
            html3 = html3 + '<td class="t_c" style="text-align:right;padding-right:5px;">' + Ta_M4TCNT.toLocaleString() + '</td>';
            html3 = html3 + '<td class="t_c" style="text-align:right;padding-right:5px;">' + Ta_M5TCNT.toLocaleString() + '</td>';
            html3 = html3 + '<td class="t_c" style="text-align:right;padding-right:5px;">' + Ta_M6TCNT.toLocaleString() + '</td>';
            html3 = html3 + '<td class="t_c" style="text-align:right;padding-right:5px;">' + Ta_M7TCNT.toLocaleString() + '</td>';
            html3 = html3 + '<td class="t_c" style="text-align:right;padding-right:5px;">' + Ta_M8TCNT.toLocaleString() + '</td>';
            html3 = html3 + '<td class="t_c" style="text-align:right;padding-right:5px;">' + Ta_M9TCNT.toLocaleString() + '</td>';
            html3 = html3 + '<td class="t_c" style="text-align:right;padding-right:5px;">' + Ta_M10TCNT.toLocaleString() + '</td>';
            html3 = html3 + '<td class="t_c" style="text-align:right;padding-right:5px;">' + Ta_M11TCNT.toLocaleString() + '</td>';
            html3 = html3 + '<td class="t_c" style="text-align:right;padding-right:5px;">' + Ta_M12TCNT.toLocaleString() + '</td>';
            html3 = html3 + '<td class="t_c" style="text-align:right;padding-right:5px;">' + Ta_W1TCNT.toLocaleString() + '</td>';
            html3 = html3 + '<td class="t_c" style="text-align:right;padding-right:5px;">' + Ta_W2TCNT.toLocaleString() + '</td>';
            html3 = html3 + '<td class="t_c" style="text-align:right;padding-right:5px;">' + Ta_W3TCNT.toLocaleString() + '</td>';
            html3 = html3 + '<td class="t_c" style="text-align:right;padding-right:5px;">' + Ta_W4TCNT.toLocaleString() + '</td>';
            html3 = html3 + '<td class="t_c" style="text-align:right;padding-right:5px;">' + Ta_W5TCNT.toLocaleString() + '</td>';
            html3 = html3 + '</tr>';

            if (gData[i].PROM_YN == 'N') { _NormalCntF++; }
            if (gData[i].PROM_YN == 'Y') { _PromotionCntF++; }
             
        }
        $('#dvGrid1 > tbody:last').append(html);
        $('#dvGrid1 > tbody:last').append(html2);
        $('#dvGrid1 > tbody:last').append(html3);
        //jsfn_SetPage(_TOTAL_COUNT);     // ucPagingJS.ascx
        setTimeout("jsfn_progressBarMst('N');", 500);
    }

    
    function jsfn_btnPopExit() {
        $('#LyPop1').bPopup().close();
        $('.b-iframe').remove();
    }


    function jdfn_ExcelReport() {
        var tab_text = "<table border='2px'><tr bgcolor='#87AFC6'>";
        var textRange; var j = 0;
        tab = document.getElementById('dvGrid1');

        for (j = 0 ; j < tab.rows.length ; j++) {
            tab_text = tab_text + tab.rows[j].innerHTML + "</tr>";
        }

        tab_text = tab_text + "</table>";
        tab_text = tab_text.replace(/<A[^>]*>|<\/A>/g, "");//remove if u want links in your table
        tab_text = tab_text.replace(/<img[^>]*>/gi, ""); // remove if u want images in your table
        tab_text = tab_text.replace(/<input[^>]*>|<\/input>/gi, ""); // reomves input params

        var ua = window.navigator.userAgent;
        var msie = ua.indexOf("MSIE ");

        if (msie > 0 || !!navigator.userAgent.match(/Trident.*rv\:11\./))      // If Internet Explorer
        {
            ifr4Excel.document.open("txt/html", "replace");
            ifr4Excel.document.write(tab_text);
            ifr4Excel.document.close();
            ifr4Excel.focus();
            sa = ifr4Excel.document.execCommand("SaveAs", true, "DC_Availability.xls");
            return (sa);
        }
        else	//크롬
        {
            //sa = window.open('data:application/vnd.ms-excel,' + encodeURIComponent(tab_text));
            var uri = 'data:application/vnd.ms-excel;base64,';
            var template = '<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="http://www.w3.org/TR/REC-html40"><head><!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet><x:Name>{worksheet}</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet></x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]--></head><body><table>{table}</table></body></html>';
            var base64 = function (s) { return window.btoa(unescape(encodeURIComponent(s))) };
            var format = function (s, c) { return s.replace(/{(\w+)}/g, function (m, p) { return c[p]; }) };
            var ctx = { worksheet: 'Availability', table: tab_text }

            var link = document.createElement("a");
            link.download = "DC_Availability.xls";
            link.href = uri + base64(format(template, ctx));
            link.click();
        }
    }
</script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentMain" runat="Server">
    <!-- 조회조건 -->
    <article id="search">
               
	    <div class="t_r">
                <input type="button" ID="btnSearch" class="btn btn_inquiry" value="조회" style="cursor:pointer;" />
                <input type="button" id="HTML_Excel" onclick="jdfn_ExcelReport()" value="다운로드" class="btn btn_default" style="cursor:pointer;"/>
                <div id="LyExcelHdn" style="display:none">
                <hpf:NButton ID="btnExcel" runat="server" CssClass="btn btn_default" OnClick="btnExcel_Click" Text="다운로드" SecurityType="Inquiry" CmdType="EXCEL" DisabledCss="GlobalBtnNone" style="cursor:pointer;"/>
                </div>
        </div>
        <div class="search row1"><!-- 한 줄: row1, 두 줄: row2, 세 줄: row3 --> 
            <div>
                <label for="">날자</label> 
                <hpf:NTextBox ID="txtOrderDATE" runat="server" CssClass="input" style="text-align:center;width:90px;padding-left:5px;" ClientIDMode="Static" Validation="Date" ReadOnly="true" />
            </div>
	    </div> 

    </article>
    <!-- 조회조건 End -->

    <br />
    
    <!-- List Start -->
	<article id="grid">
        <div id="Ly_grid1Scroll" style="width:100%;text-align:center;height:580px;min-width:1024px;overflow-x:auto;overflow-y:auto;">
		    <table id="dvGrid1" style="min-width:1850px;">
            <colgroup>
                <col />
            </colgroup>
			<thead> 
                <tr class="tr_h31" style="height:35px;"> 
                    <td style="border:1px solid #ccc;font-size:11px;" class="t_c"><b>Promotion</b></td>
                    <td style="border:1px solid #ccc;font-size:11px;" class="t_c"><b>WH</b></td>
                    <td style="border:1px solid #ccc;font-size:11px;" class="t_c"><b></b></td>
                    <td style="border:1px solid #ccc;font-size:11px;" class="t_c"><b>YTD</b></td>
                    <td style="border:1px solid #ccc;font-size:11px;" class="t_c"><b>Q1</b></td>
                    <td style="border:1px solid #ccc;font-size:11px;" class="t_c"><b>Q2</b></td>
                    <td style="border:1px solid #ccc;font-size:11px;" class="t_c"><b>Q3</b></td>
                    <td style="border:1px solid #ccc;font-size:11px;" class="t_c"><b>Q4</b></td>
                    <td style="border:1px solid #ccc;font-size:11px;" class="t_c"><b>M1</b></td>
                    <td style="border:1px solid #ccc;font-size:11px;" class="t_c"><b>M2</b></td>
                    <td style="border:1px solid #ccc;font-size:11px;" class="t_c"><b>M3</b></td>
                    <td style="border:1px solid #ccc;font-size:11px;" class="t_c"><b>M4</b></td>
                    <td style="border:1px solid #ccc;font-size:11px;" class="t_c"><b>M5</b></td>
                    <td style="border:1px solid #ccc;font-size:11px;" class="t_c"><b>M6</b></td>
                    <td style="border:1px solid #ccc;font-size:11px;" class="t_c"><b>M7</b></td>
                    <td style="border:1px solid #ccc;font-size:11px;" class="t_c"><b>M8</b></td>
                    <td style="border:1px solid #ccc;font-size:11px;" class="t_c"><b>M9</b></td>
                    <td style="border:1px solid #ccc;font-size:11px;" class="t_c"><b>M10</b></td>
                    <td style="border:1px solid #ccc;font-size:11px;" class="t_c"><b>M11</b></td>
                    <td style="border:1px solid #ccc;font-size:11px;" class="t_c"><b>M12</b></td>
                    <td style="border:1px solid #ccc;font-size:11px;" class="t_c"><b>W1</b></td>
                    <td style="border:1px solid #ccc;font-size:11px;" class="t_c"><b>W2</b></td>
                    <td style="border:1px solid #ccc;font-size:11px;" class="t_c"><b>W3</b></td>
                    <td style="border:1px solid #ccc;font-size:11px;" class="t_c"><b>W4</b></td>
                    <td style="border:1px solid #ccc;font-size:11px;" class="t_c"><b>W5</b></td>
                </tr>
            </thead>
                <tr>
                    <td colspan="25" style="width:100%;height:25px;text-align:center;">
                    </td>
                </tr>
            </table> 
        </div> 
	</article>

   <%-- <uc1:ucPaging ID="ucPaging" runat="server" />--%> <%--OnSelEvent="ucPaging_SelEvent" --%>


<%--### 아이템 검색 팝업 레이어 팝업용도 S ###--%>
<div id="LyPop1" class="modal_pop" style="width:550px;height:450px;padding: 2px 2px 2px 2px;display:none;background-color:#FFF !important;">
</div>
<%--### 아이템 검색 팝업 레이어 팝업용도 E ###--%>
    <!-- Excel Download 용 Excel 버튼용 트리거 S -->
    <asp:UpdatePanel ID ="UpDatePanel1" runat ="server">
        <Triggers>
        <asp:PostBackTrigger ControlID="btnExcel" />
        </Triggers>
    </asp:UpdatePanel>
    <!-- Excel Download 용 Excel 버튼용 트리거 E -->
<iframe id="ifr4Excel" style="display:none"></iframe>
</asp:Content>
