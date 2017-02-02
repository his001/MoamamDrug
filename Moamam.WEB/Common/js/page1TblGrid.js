function jsfn_Search() {
    var _txtUserID = $("#Login_userId").val();
    if (_txtUserID == '') { jsfn_alertClose("로그인이 필요합니다!"); return false; }
    var SpName = "SPM_Web_COMMON_Tbl_BokYongHooKi_R";
    var SpParams = "UserID▥" + _txtUserID;

    $.ajax({
        url: "/Site/Data/Data.aspx"
    , type: "post"
    , async: false
    , data: {
        fnName: "CommonCallSpGetJson"
        , SpName: SpName
        , SpParams: SpParams
    }
    , dataType: "json"
    , success: function (data) { jsfn_SetGrid(data); }
    , error: function (x, e) { jsfn_AjaxError(x, e); }
    });
}

function jsfn_SetGrid(dataRes) {
    $("#dvGrid1 > tbody > tr").remove();
    var _TOTAL_COUNT = 0;
    var gData = dataRes;
    var html = '';

    if (gData.length == 0) {
        jsfn_SetPage(_TOTAL_COUNT); // ucPagingJS.ascx
        
        var noData_html = '<tr><td colspan="24" style="width:100%;height:25px;text-align:center;"><b><font color="red">조회된 결과가 없습니다.</font></b></td></tr>';
        $('#dvGrid1 > tbody:last').append(noData_html);
        jsfn_progressBarMst('N');
        return false;
    } else { _TOTAL_COUNT = gData[0].TOTAL_COUNT; }

    var _PRIMARY_PACK_NO = '';
    for (var i = 0; i < gData.length; i++) {

        if (gData[i].CureDate == null) { gData[i].CureDate = ''; }
        if (gData[i].tempC == null) { gData[i].tempC = ''; }
        if (gData[i].Feber == null) { gData[i].Feber = ''; }
        if (gData[i].HaeYeolJe == null) { gData[i].HaeYeolJe = ''; }
        if (gData[i].Yak_iLbun == null) { gData[i].Yak_iLbun = ''; }
        if (gData[i].ChamGoSaHang == null) { gData[i].ChamGoSaHang = ''; }
        if (gData[i].BokYongHooKi == null) { gData[i].BokYongHooKi = ''; }
        if (gData[i].ChouBang == null) { gData[i].ChouBang = ''; }
        if (gData[i].JungSang == null) { gData[i].JungSang = ''; }
        if (gData[i].HangSaengJeBokan == null) { gData[i].HangSaengJeBokan = ''; }
        if (gData[i].HangSaengJeEat == null) { gData[i].HangSaengJeEat = ''; }

        html = html + '<tr class="tr_c">';
        html = html + '<td class="t_c" style="text-align:center;">' + gData[i].idx + '</td>';
        html = html + '<td class="t_c" style="text-align:center;">' + gData[i].VisitDate + '</td>';
        html = html + '<td class="t_c" style="text-align:center;">' + gData[i].CureDate + '</td>';
        html = html + '<td class="t_c" style="text-align:center;">' + gData[i].tempC + '</td>';
        html = html + '<td class="t_c" style="text-align:center;">' + gData[i].Feber + '</td>';
        html = html + '<td class="t_c" style="text-align:center;">' + gData[i].HaeYeolJe + '</td>';
        html = html + '<td class="t_c" style="text-align:center;">' + gData[i].Yak_iLbun + '</td>';
        html = html + '<td class="t_c" style="text-align:center;display:none;"><div style="text-align:left;padding-right:5px;width:150px;overflow:hidden;text-overflow:ellipsis;white-space:nowrap;">' + gData[i].ChamGoSaHang + '</div></td>';
        html = html + '<td class="t_c" style="text-align:center;display:none;"><div style="text-align:left;padding-right:5px;width:150px;overflow:hidden;text-overflow:ellipsis;white-space:nowrap;">' + gData[i].BokYongHooKi + '</div></td>';
        html = html + '<td class="t_c" style="text-align:left;display:none;"><div style="text-align:left;padding-right:5px;width:150px;overflow:hidden;text-overflow:ellipsis;white-space:nowrap;">' + gData[i].ChouBang + '</div></td>';
        html = html + '<td class="t_c" style="text-align:center;display:none;">' + gData[i].JungSang + '</td>';
        html = html + '<td class="t_c" style="text-align:center;display:none;">' + gData[i].HangSaengJeBokan + '</td>';
        html = html + '<td class="t_c" style="text-align:center;display:none;">' + gData[i].HangSaengJeEat + '</td>';
        html = html + '</tr>';
    }
    $('#dvGrid1 > tbody:last').append(html);
    jsfn_SetPage(_TOTAL_COUNT);     // ucPagingJS.ascx
    setTimeout("jsfn_progressBarMst('N');", 500);
}