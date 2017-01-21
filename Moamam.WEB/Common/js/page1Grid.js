//<!-- 5. grid  -->
var _MaingrdID;
_MaingrdID = $("#grdMasterContent");

function jsfn_getGridContentHeight() {
    var totalHeight = $(window).height();
    totalHeight = totalHeight / 2;
    var menuHeight = 70;//$("#menu-condition").outerHeight();
    var topHeight = 70;
    return (totalHeight - topHeight - menuHeight);
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
    , success: function (data) {jsfn_SetGrid(data); }
    , error: function (x, e) { jsfn_AjaxError(x, e); }
    });
}

function jsfn_SetGrid(mydata) {

    if (mydata) {
        $("#grdMasterContent").clearGridData();
        //$("#grdMasterContent").setGridWidth($(window).width());
        $("#grdMasterContent").setGridParam({ data: mydata }).trigger("reloadGrid");
        //.setGridWidth($("#grdMasterContent").width())
        //.setGridHeight(jsfn_getGridContentHeight())
        //.trigger("reloadGrid");
    } else {
        //_MaingrdID.setGridWidth($('#grdMasterContent').width())
        //.setGridHeight(fn_getGridContentHeight())
        $("#grdMasterContent").trigger("reloadGrid");
    }
}

function jsfn_InitGrid() {
    ////alert('fn_InitGrid() - start' + $("#grdMasterContent").width());
    ////VisitDate, CureDate, JungSang,tempC, Feber, HaeYeolJe, ChouBang, Yak_iLbun
    ////alert('Grd-1');
    $('#grdMasterContent').jqGrid({
        caption: " 투약정보",
        datatype: "local",
        loadtext: '로딩중..',
        colNames: ['idx', '방문날짜', '완치날짜', '체온', '열', '해열제', '투약일', '참고사항', '복용후기', '처방', '증상'
            , 'HangSaengJeBokan','HangSaengJeEat', 'rowstate'],
        colModel: [
            { name: 'idx', index: 'idx', width: 60, key: true, classes: 'grdEditBgcolor', align: 'center' },
            { name: 'VisitDate', index: 'VisitDate', width: 100, editable: false, edittype: 'text', classes: 'grdEditBgcolor', align: 'center' },
            { name: 'CureDate', index: 'CureDate', width: 100, editable: true, edittype: 'text', classes: 'grdEditBgcolor', align: 'center' } ,
            { name: 'tempC', index: 'tempC', width: 80, editable: true, edittype: 'text', classes: 'grdEditBgcolor' },
            { name: 'Feber', index: 'Feber', width: 80, editable: true, edittype: 'text', classes: 'grdEditBgcolor' },
            { name: 'HaeYeolJe', index: 'HaeYeolJe', width: 80, editable: true, edittype: 'checkbox', classes: 'grdEditBgcolor' },
            { name: 'Yak_iLbun', index: 'Yak_iLbun', width: 80, align: 'center' },
            { name: 'ChamGoSaHang', index: 'ChamGoSaHang', width: 250, classes: 'grdEditBgcolor', align: 'left', formatter: function (v) { return '<div style="max-height: 35px">' + v + '</div>'; } },
            { name: 'BokYongHooKi', index: 'BokYongHooKi', width: 250, classes: 'grdEditBgcolor', align: 'left', formatter: function (v) { return '<div style="max-height: 35px">' + v + '</div>'; } },
            { name: 'ChouBang', index: 'ChouBang', width: 250, classes: 'grdEditBgcolor', align: 'left', formatter: function (v) { return '<div style="max-height: 35px">' + v + '</div>'; } },
            { name: 'JungSang', index: 'JungSang', width: 250, classes: 'grdEditBgcolor', align: 'left', formatter: function (v) { return '<div style="max-height: 35px">' + v + '</div>'; } },
            { name: 'HangSaengJeBokan', hidden: true },
            { name: 'HangSaengJeEat', hidden: true },
            { name: 'rowstate', hidden: true }
        ],
        pager: '#pager',
        width: $(window).width(),//jsfn_getGridContentSubWidth(true),
        height: 200,//jsfn_getGridContentSubHeight(true),
        onSortCol: function (name, index) { /*alert("Column Name: "+name+" Column Index: "+index);*/ },
        //ondblClickRow: function (id) { /*alert("You double click row with id: "+id);*/ },
        viewrecords: true,
        rownumbers: true,
        multiselect: false,
        rowNum: 10,
        rowList: [10, 30, 50, 100, 999],
        cellEdit: false,
        altRows: true,
        //altclass: 'someClass',
        shrinkToFit: false,
        cellsubmit: 'clientArray',
        //styleUI: 'Bootstrap',
        cellurl: '#'
        ///////////////////////////////// 행에서 Edit 가능하도록 변경 park.heesoo //////////////////////////////////////
      //  beforeSelectRow: function (rowid, e) {
      //      var $self = $(this),
      //          iCol = $.jgrid.getCellIndex($(e.target).closest("td")[0]),
      //          cm = $self.jqGrid("getGridParam", "colModel");

      //      //if (cm[iCol].name === "NICKNM") { var strNICKNM = _MaingrdID.getCell(rowid, 'NICKNM'); }
      //      return true; // false를 리턴하면 로우 셀렉트가 안 되니까.. 주의!
      //  },
      //  beforeSubmitCell: function (rowid, cellname, value) {   // submit 전
      //      if (cellname == 'PAGECODE') {
      //          var strUSERID = value;
      //          _MaingrdID.jqGrid('setColProp', 'PAGECODE', { editable: false });
      //      }
      //      return { "id": rowid, "cellName": cellname, "cellValue": value };
      //  },
      //  afterSubmitCell: function (res) {    // 변경 후
      //      var aResult = $.parseJSON(res.responseText);
      //      var userMsg = "";
      //      //if ((aResult.msg == "success")) { userMsg = "데이터가 변경되었습니다."; }
      //      return [(aResult.msg == "success") ? true : false, userMsg];
      //  }
        //, loadComplete: function () { },
      //, afterEditCell: function (rowid, cellname, value, iRow, iCol) {
      //    _MaingrdID.jqGrid('setCell', rowid, 'rowstate', 'E');
      //    $("#" + iRow + "_" + cellname).bind('blur', function () { _MaingrdID.saveCell(iRow, iCol); });  // 저장시 마지막 저장 건이 반영 안되는 이슈 때문에 
      //}
      , onSelectRow: function (id) {
          //if (id && id !== lastSel) {
          //    _MaingrdID.restoreRow(lastSel);
          //    lastSel = id;
          //}
          //_MaingrdID.editRow(id, true);
      }
     , ondblClickRow: function (rowid) {
         var grid = $('#grdMasterContent');

         var _txt_idx           = grid.jqGrid('getCell', rowid, 'idx');
         var _txt_Visit_Date    = grid.jqGrid('getCell', rowid, 'VisitDate');
         var _txt_NoPain_Date   = grid.jqGrid('getCell', rowid, 'CureDate');
         var _txt_tempC         = grid.jqGrid('getCell', rowid, 'tempC');
         var _rdoFeber          = grid.jqGrid('getCell', rowid, 'Feber');
         var _HaeYeolJeOX       = grid.jqGrid('getCell', rowid, 'HaeYeolJe');
         var _txt_ChouBang      = grid.jqGrid('getCell', rowid, 'ChouBang');
         var _txt_Yak_iLbun     = grid.jqGrid('getCell', rowid, 'Yak_iLbun');
         var _txt_ChamGoSaHang  = grid.jqGrid('getCell', rowid, 'ChamGoSaHang');
         var _txt_BokYongHooKi  = grid.jqGrid('getCell', rowid, 'BokYongHooKi');
         var _txt_JngSang       = grid.jqGrid('getCell', rowid, 'JungSang');

         var _rdoHangSaengJeBokan = grid.jqGrid('getCell', rowid, 'HangSaengJeBokan');
         var _rdoHangSaengJeEat = grid.jqGrid('getCell', rowid, 'HangSaengJeEat');

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
         _txt_ChouBang = jsfn_ReplTxt(_txt_ChouBang);
         _txt_ChamGoSaHang = jsfn_ReplTxt(_txt_ChamGoSaHang);
         _txt_BokYongHooKi = jsfn_ReplTxt(_txt_BokYongHooKi);
         _txt_JngSang = jsfn_ReplTxt(_txt_JngSang);

         $("#txt_ChouBang").val(_txt_ChouBang);
         $("#txt_ChamGoSaHang").val(_txt_ChamGoSaHang);
         $("#txt_BokYongHooKi").val(_txt_BokYongHooKi);
         $("#txt_JngSang").val(_txt_JngSang);

         jsfn_GotoLayer('Lysec_BokYongHooKi_Write');
     }
        ///////////////////////////////// 행에서 Edit 가능하도록 변경 park.heesoo //////////////////////////////////////
    });
    //$("#grdMasterContent").jqGrid("setLabel", "AGE_HAN", "", { "text-align": "right" });
    //$("#grdMasterContent").jqGrid("setLabel", "AREA", "", { "text-align": "center" });
    //$("#grdMasterContent").jqGrid('navGrid', "#pager", { add: false, edit: false, del: false, search: true, refresh: true });
    //$("#grdMasterContent").jqGrid('setGridParam', { datatype: 'json' }).trigger('reloadGrid');
}
function jsfn_GotoLayer(strId) {
    try{
        var offset = $("#" + strId).offset();
        $('html,body').animate({scrollTop : offset.top}, 300);
    }catch(e){}
}
function jsfn_ReplTxt(str) {
    var _repl1 = '<div style="max-height: 35px">';
    var _repl2 = '</div>';
    str = str.replace(_repl1, '');
    str = str.replace(_repl2, '');
    str = str.replace(/<br>/g, '\n');
    return str;
}

function AddGridEditButton() {
    //행추가팝업으로띄울실
    //var ids = _MaingrdID.jqGrid('getDataIDs');
    //alert(ids);
    //  for (var i = 0; i < ids.length; i++) {
    //      var cl = ids[i];
    //      btnEdit = "<button class='btn' style='font-size: 8pt; height: 22px;' type='button' onclick=\"fn_EditRow('" + cl + "');\"><i class='icon-edit'></i>수정</button>";
    //      _MaingrdID.jqGrid('setRowData', ids[i], { act: btnEdit });
    //  }
}
//function fn_EditRow(rowID) {
//    var data = $('#grdMasterContent').jqGrid('getRowData', rowID);
//    data.act = ' ';
//    $.wf.ShowModalView3('/GTO/GTO010COP', { action: 'edit', data: JSON.stringify(data) }, { width: 450, height: 322, resizable: 'no' });
//}


///////////// 화면 크기에 맞추기  /////////////
//function jsfn_SetScreen() {
//    //jsfn_SetTabwidth();
//    //jsfn_SetLyMiddleHeight();
//    $('.ui-widget-overlay').width($(window).width());
//    $('.ui-widget-overlay').height($(window).height());
//}

//function jsfn_SetTabwidth() {
//    var totalWidth = $(window).width();
//    var leftWidth = 200;
//    $('.easyui-tabs').width(totalWidth - leftWidth);
//    //alert(totalWidth - leftWidth);
//}

//function jsfn_SetLyMiddleHeight() {
//    var totalHeight = $(window).height();
//    var menuHeight = 250;
//    $('.LyMTopTab').height(totalHeight - menuHeight);
//    $('.LyMiddle').height(totalHeight - menuHeight - 50);
//}

//function jsfn_SetLyMiddleHeightRtn() {
//    var totalHeight = $(window).height();
//    //var menuHeight = $(".easyui-tabs").outerHeight();
//    var menuHeight = 250;
//    return (totalHeight - menuHeight);

//}

//function jsfn_SetLyMiddleHeightT1() {
//    var totalHeight = $(window).height();
//    var menuHeight = 250;
//    $('.panel panel-default').height(totalHeight - menuHeight);
//    //$('.layer_border_01 shadow01 panel-body panel-body-noheader panel-body-noborder').height(totalHeight - menuHeight - 50);
//    $('.ui-jqgrid-view').height(totalHeight - menuHeight - 380);
//    $('#grdMasterContent').height(totalHeight - menuHeight - 400);

//}



///////////// 화면 크기에 맞추기  /////////////


//function jsfn_getGridContentHeight(isCommon) {
//    var totalHeight = $(window).height();
//    var menuHeight = 20;//$(".easyui-tabs").outerHeight();

//    return (totalHeight - menuHeight);
//}

//function jsfn_getGridContentSubHeight(isCommon) {
//    var _totalHeight = $(window).height();
//    //var _topMenuH = 250;
//    //var _scrHeight = _totalHeight - _topMenuH;
//    return (_scrHeight);
//}


////function jsfn_getGridContentWidth(isCommon) {
////    var totalWidth = $(window).width();
////    var leftWidth = 250;
////    if (!isCommon) {
////        topHeight -= 13;
////    }
////    return (totalWidth - leftWidth);
////}

//function jsfn_getGridContentSubWidth(isCommon) {
//    var totalWidth = $(window).width();
//    var leftWidth = 5;
//    //if (!isCommon) {
//    //    topHeight -= 13;
//    //}
//    return (totalWidth - leftWidth);
//}

