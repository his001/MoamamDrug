function jsfn_SaveBokYongDrug() {
    var _txt_idx = $("#txt_idx").val();
    if (_txt_idx == '') { return false; }
    var _chkCt = 0;
    var _jsuccessCnt = 0;
    var index = 0;
    var _gridDrug1_hdn_ITEM_SEQ = '';
    var _gridDrug1_hdn_ITEM_MEMO = '';
    $('input[type=checkbox][name="rpt1_chkBox"]').each(function () {
        if ($(this).prop("checked")) {
            _gridDrug1_hdn_ITEM_SEQ = $("input[name=gridDrug1_hdn_ITEM_SEQ]:eq(" + index + ")").val();
            _gridDrug1_hdn_ITEM_MEMO = $("input[name=gridDrug1_hdn_ITEM_MEMO]:eq(" + index + ")").val();
            _jsuccessCnt = _jsuccessCnt + jsfn_SaveBokYongDrugDtl(_txt_idx, _gridDrug1_hdn_ITEM_SEQ, _gridDrug1_hdn_ITEM_MEMO);
        }
        index++;
    });
}

function jsfn_SaveBokYongDrugDtl(_idx, _ITEM_SEQ, _ITEM_MEMO) {
    var _txtUserID = $("#Login_userId").val();
    var _txt_Userip = $("#txt_Userip").text();
    var _successCnt = 0;
    if (_idx == '') { return false; }
    if (_ITEM_SEQ == '') { return false; }
    _ITEM_MEMO = _ITEM_MEMO.replace(/'/g, '');

    var SpName = "SPM_Web_COMMON_Tbl_BokYongHooKi_DrugInfo_S";
    var SpParams = "idx" + '▥' + _idx + '▤';
    SpParams = SpParams + "ITEM_SEQ" + '▥' + _ITEM_SEQ + '▤';
    SpParams = SpParams + "ITEM_MEMO" + '▥' + _ITEM_MEMO + '▤';
    SpParams = SpParams + "regip" + '▥' + _txt_Userip + '▤';
    SpParams = SpParams + "userId" + '▥' + _txtUserID;

    $.ajax({
    url: '/Site/Data/Data.aspx'
    ,type: "post"
    ,async: false
    ,data: {
        fnName: "CommonCallSpGetJson"
        ,SpName: SpName
        ,SpParams: SpParams
    }
    , dataType: "json"
    , success: function (data) { _successCnt = 1; }
    , error: function (x, e) { jsfn_AjaxError(x, e); }
    });
    return _successCnt;
}

function jsfn_SearchDrug() {
    var _txt_idx = $("#txt_idx").val();
    if (_txt_idx == '') { return false; }
    var SpName = "SPM_Web_COMMON_Tbl_BokYongHooKi_DrugInfo_R";
    var SpParams = "idx▥" + _txt_idx;

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
    , success: function (data) { jsfn_SetGridDrug(data); }
    , error: function (x, e) { jsfn_AjaxError(x, e); }
    });
}

function jsfn_SetGridDrug(dataRes) {
    $("#dvGrid1Drug > tbody > tr").remove();
    var gData = dataRes;
    var html = '';

    if (gData.length == 0) {
        var noData_html = '<tr><td colspan="4" style="width:100%;height:25px;text-align:center;"><b><font color="red">입력된 약정보가 없습니다.</font></b></td></tr>';
        $('#dvGrid1Drug > tbody:last').append(noData_html);
        return false;
    }

    for (var i = 0; i < gData.length; i++) {
        if (gData[i].ITEM_SEQ == null) { gData[i].ITEM_SEQ = ''; }
        if (gData[i].ITEM_NAME == null) { gData[i].ITEM_NAME = ''; }
        if (gData[i].ITEM_MEMO == null) { gData[i].ITEM_MEMO = ''; }

        html = html + '<tr class="tr_c">';
        html = html + '<td class="t_c" style="text-align:center;"><input type="checkbox" id="rpt1_chkBox_' + i + '" name="rpt1_chkBox" checked="checked" disabled="disabled">' + gData[i].ITEM_NAME
        html = html + '<input type="text" id="gridDrug1_hdn_ITEM_SEQ" name="gridDrug1_hdn_ITEM_SEQ" value="' + gData[i].ITEM_SEQ + '" style="display:none;"></td>';
        html = html + '<td class="t_c" style="text-align:left;"><input type="text" id="gridDrug1_hdn_ITEM_MEMO" name="gridDrug1_hdn_ITEM_MEMO" value="' + gData[i].ITEM_MEMO + '"></td>';
        html = html + '</tr>';
    }
    $('#dvGrid1Drug > tbody:last').append(html);
    setTimeout("jsfn_progressBarMst('N');", 500);
}

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

    var _txt_idx = '';
    var _txt_Visit_Date = '';
    var _txt_NoPain_Date = '';
    var _txt_tempC = '';
    var _rdoFeber = '';
    var _HaeYeolJeOX = '';
    var _txt_ChouBang = '';
    var _txt_Yak_iLbun = '';
    var _txt_ChamGoSaHang = '';
    var _txt_BokYongHooKi = '';
    var _txt_JngSang = '';
    var _rdoHangSaengJeBokan = '';
    var _rdoHangSaengJeEat = '';

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

        _txt_idx = ''; _txt_Visit_Date = ''; _txt_NoPain_Date = ''; _txt_tempC = ''; _rdoFeber = ''; _HaeYeolJeOX = '';
        _txt_ChouBang = ''; _txt_Yak_iLbun = ''; _txt_ChamGoSaHang = ''; _txt_BokYongHooKi = ''; _txt_JngSang = '';
        _rdoHangSaengJeBokan = '';_rdoHangSaengJeEat = '';

        _txt_idx = gData[i].idx;
        _txt_Visit_Date = gData[i].VisitDate;
        _txt_NoPain_Date = gData[i].CureDate;
        _txt_tempC = gData[i].tempC;
        _rdoFeber = gData[i].Feber;
        _HaeYeolJeOX = gData[i].HaeYeolJe;
        _txt_ChouBang = gData[i].ChouBang;
        _txt_Yak_iLbun = gData[i].Yak_iLbun;
        _txt_ChamGoSaHang = gData[i].ChamGoSaHang;
        _txt_BokYongHooKi = gData[i].BokYongHooKi;
        _txt_JngSang = gData[i].JungSang;

        _txt_ChouBang = escape(_txt_ChouBang.replace(/\n/g, '<br/>').replace(/'/g, '`'));
        _txt_ChamGoSaHang = escape(_txt_ChamGoSaHang.replace(/\n/g, '<br/>').replace(/'/g, '`'));
        _txt_BokYongHooKi = escape(_txt_BokYongHooKi.replace(/\n/g, '<br/>').replace(/'/g, '`'));
        _txt_JngSang = escape(_txt_JngSang.replace(/\n/g, '<br/>').replace(/'/g, '`'));

        _rdoHangSaengJeBokan = gData[i].HangSaengJeBokan;
        _rdoHangSaengJeEat = gData[i].HangSaengJeEat;

        html = html + '<tr class="tr_c">';
        html = html + '<td class="t_c" style="text-align:center;">' + gData[i].idx + '</td>';
        html = html + '<td class="t_c" style="text-align:center;"><a href="javascript:jsfn_showToYakInfo(\'' + _txt_idx + '\',\'' + _txt_Visit_Date + '\',\'' + _txt_NoPain_Date + '\',\'' + _txt_tempC + '\',\'' + _rdoFeber + '\',\'' + _HaeYeolJeOX + '\',\'' + _txt_ChouBang + '\',\'' + _txt_Yak_iLbun + '\',\'' + _txt_ChamGoSaHang + '\',\'' + _txt_BokYongHooKi + '\',\'' + _txt_JngSang + '\',\'' + _rdoHangSaengJeBokan + '\',\'' + _rdoHangSaengJeEat + '\');">' + gData[i].VisitDate + '</a></td>';
        html = html + '<td class="t_c" style="text-align:center;">' + gData[i].CureDate + '</td>';
        html = html + '<td class="t_c" style="text-align:center;">' + gData[i].tempC + '</td>';
        html = html + '<td class="t_c" style="text-align:center;">' + gData[i].Feber + '</td>';
        html = html + '<td class="t_c" style="text-align:center;">' + gData[i].HaeYeolJe + '</td>';
        html = html + '<td class="t_c" style="text-align:center;">' + gData[i].Yak_iLbun + '</td>';
        html = html + '<td class="t_c" style="text-align:center;display:none;"><div style="text-align:left;padding-right:5px;width:150px;overflow:hidden;text-overflow:ellipsis;white-space:nowrap;">' + unescape(_txt_ChamGoSaHang) + '</div></td>';
        html = html + '<td class="t_c" style="text-align:center;display:none;"><div style="text-align:left;padding-right:5px;width:150px;overflow:hidden;text-overflow:ellipsis;white-space:nowrap;">' + unescape(_txt_BokYongHooKi) + '</div></td>';
        html = html + '<td class="t_c" style="text-align:left;display:none;"><div style="text-align:left;padding-right:5px;width:150px;overflow:hidden;text-overflow:ellipsis;white-space:nowrap;">' + unescape(_txt_ChouBang) + '</div></td>';
        html = html + '<td class="t_c" style="text-align:center;display:none;">' + unescape(_txt_ChamGoSaHang) + '</td>';
        html = html + '<td class="t_c" style="text-align:center;display:none;">' + gData[i].HangSaengJeBokan + '</td>';
        html = html + '<td class="t_c" style="text-align:center;display:none;">' + gData[i].HangSaengJeEat + '</td>';
        html = html + '</tr>';
    }
    $('#dvGrid1 > tbody:last').append(html);
    jsfn_SetPage(_TOTAL_COUNT);     // ucPagingJS.ascx
    setTimeout("jsfn_progressBarMst('N');", 500);
}

function jsfn_showToYakInfo(_txt_idx, _txt_Visit_Date, _txt_NoPain_Date, _txt_tempC, _rdoFeber, _HaeYeolJeOX, _txt_ChouBang, _txt_Yak_iLbun, _txt_ChamGoSaHang, _txt_BokYongHooKi, _txt_JngSang, _rdoHangSaengJeBokan, _rdoHangSaengJeEat) {
    
    $("#txt_idx").val(_txt_idx);
    $("#txt_Visit_Date").val(_txt_Visit_Date);
    $("#txt_NoPain_Date").val(_txt_NoPain_Date);
    $("#txt_tempC").val(_txt_tempC);
    //alert(_rdoFeber + ' : _rdoFeber \n' + _HaeYeolJeOX + ' : _HaeYeolJeOX \n' + _rdoHangSaengJeBokan + ' : _rdoHangSaengJeBokan \n' + _rdoHangSaengJeEat + ' : _rdoHangSaengJeEat \n');
    $('input:radio[name=rdoFeber]:input[value=' + _rdoFeber + ']').attr("checked", true);
    $('input:radio[name=rdoHaeYeolJeOX]:input[value=' + _HaeYeolJeOX + ']').attr("checked", true);
    $('input:radio[name=rdoHangSaengJeBokan]:input[value=' + _rdoHangSaengJeBokan + ']').attr("checked", true);
    $('input:radio[name=rdoHangSaengJeEat]:input[value=' + _rdoHangSaengJeEat + ']').attr("checked", true);
    $("#txt_Yak_iLbun").val(_txt_Yak_iLbun);
    _txt_ChouBang = jsfn_ReplTxtTBL(unescape(_txt_ChouBang));
    _txt_ChamGoSaHang = jsfn_ReplTxtTBL(unescape(_txt_ChamGoSaHang));
    _txt_BokYongHooKi = jsfn_ReplTxtTBL(unescape(_txt_BokYongHooKi));
    _txt_JngSang = jsfn_ReplTxtTBL(unescape(_txt_JngSang));
    $("#txt_ChouBang").val(_txt_ChouBang);
    $("#txt_ChamGoSaHang").val(_txt_ChamGoSaHang);
    $("#txt_BokYongHooKi").val(_txt_BokYongHooKi);
    $("#txt_JngSang").val(_txt_JngSang);
    jsfn_GotoLayerTBL('Lysec_BokYongHooKi_Write');
}

function jsfn_GotoLayerTBL(strId) {
    try {
        location.hash = '';
        document.location.href = "Default.aspx#" + strId;
    } catch (e) { }
}

function jsfn_ReplTxtTBL(str) {
    var _repl1 = '<div style="max-height: 35px">';
    var _repl2 = '</div>';
    str = str.replace(_repl1, '');
    str = str.replace(_repl2, '');
    str = str.replace(/<br \/>/g, '\n');
    return str;
}
