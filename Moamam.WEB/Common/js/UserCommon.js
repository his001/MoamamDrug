

var SerchCheck = "N";

function ShowLoading() {
    document.getElementById('divProgress').style.display = "";
}
function divProgressNone() {
    document.getElementById('divProgress').style.display = 'none';
}

///////////////////////////////////////////////////////////////////////////////////////////////
//  엑셀 버튼 클릭 시 Confirm 창                                                              //
///////////////////////////////////////////////////////////////////////////////////////////////
function ExcelConfirm(msg) {
    var result = confirm(msg);

    if (result) {
        //ShowLoading();
    }
    else {
        document.getElementById("divProgress").style.display = "none";
        result = false;
    }

    return result;

}
function sectionListChange(val) {
    $("#ddlToSectionList option").each(function () {
        if ($(this).val() == val) {
            $(this).attr('selected', 'selected');
        }
    });
}
///////////////////////////////////////////////////////////////////////////////////////////////
//  다운로드 버튼 클릭 시 Confirm 창                                                              //
///////////////////////////////////////////////////////////////////////////////////////////////
function ExcelConfirmCHk() {
    var returnvalue = false;
    SerchCheck = $("#hdSerchListCheck").val();
    if (SerchCheck == "Y") {
        var result = ExcelConfirm('다운로드 하시겠습니까?');
        if (result) {
            returnvalue = true;
        }
    } else {
        alert("조회후 다운로드 하세요.");
    }
    return returnvalue;
}


//아이템 리스트 밸리데이션
function SerchItemToList() {
    var item = $("#txtItem").val();
    if (item.trim().length == 0 || item == 0) {
        alert("상품코드를 입력하세요!");
        $("#txtItem").focus();
        $("#txtItem").select();
        return false;
    }
    return true;

}

///////////////////////////////////////////////////////////////////////////////////////////////
//                        아이템 검색                                                        //    
/////////////////////////////////////////////////////////////////////////////////////////////// 
//function SerchItemNameList() {
//    var length = String(txtSerchName.value).length;
//    if (length < 2) {
//        alert("조회할 아이템 명칭을 두자리 이상 입력하세요.");
//        $("#txtSerchName").focus();
//        $("#txtSerchName").select();
//        return false;
//    }
//    return true;
//}

///////////////////////////////////////////////////////////////////////////////////////////////
//  아이템 이름 검색                                                      //
///////////////////////////////////////////////////////////////////////////////////////////////
function ItemNameList() {
    result = showWindow2(String.format("../PopupPage/ItemNameList.aspx"), 460, 370, "resizable=no,status,scrollbars=no");
    result.focus();

    return false;//true;
}

///////////////////////////////////////////////////////////////////////////////////////////////
//  협력업체 이름 검색                                                      //
///////////////////////////////////////////////////////////////////////////////////////////////
function SuppNameList() {
    result = showWindow2(String.format("../PopupPage/SupplierNameList.aspx"), 460, 370, "resizable=no,status,scrollbars=no");
    result.focus();

    return false;//true;
}


///////////////////////////////////////////////////////////////////////////////////////////////
//                        숫자만 입력가능                                                       //    
///////////////////////////////////////////////////////////////////////////////////////////////
function OnCheckNumeric(obj) {
    normalize = /\D/g;

    if (obj.value != "") {
        if (normalize.test(obj.value)) {
            obj.value = obj.value.replace(/\D/g, "");
        }
    }  
}

///////////////////////////////////////////////////////////////////////////////////////////////
//  삭제 버튼 클릭 시 Confirm 창                                                              //
///////////////////////////////////////////////////////////////////////////////////////////////
function DeleteConfirm() {
    return confirm("삭제 하시겠습니까?");
}



///////////////////////////////////////////////////////////////////////////////////////////////
//  체크박스 전체선택/전체해제                                                           //
///////////////////////////////////////////////////////////////////////////////////////////////
function CheckedAll(check) {
    var _chkAll = $('#chkAll').prop('checked');
    $('input[type=checkbox][id="eachCheck"]').each(function (idx) { 
        if ($("input[type=text][id=txtAOQ]:eq(" + idx + ")").prop('disabled')) {
        } else {
            $(this).prop("checked", _chkAll);
        } 
    });
}
///////////////////////////////////////////////////////////////////////////////////////////////
//  승인처리                                                           //
///////////////////////////////////////////////////////////////////////////////////////////////
function Approval() {
    var i = 0;
    var len = $('input[type=checkbox][id*="eachCheck"]:checked').length;
    if (len > 0) {
        ShowLoading();
        return true;
    } else {
        alert("조회후 체크된 항목이 없습니다.");
        return false;
    }
}
///////////////////////////////////////////////////////////////////////////////////////////////
//  수동 발주량 수정                                                           //
///////////////////////////////////////////////////////////////////////////////////////////////
function UpdateChecked() {
    var i = 0;
    var len = $('input[type=checkbox][id*="eachCheck"]:checked').length;
    if (len > 0) {
        ShowLoading();
        return true;
    } else {
        alert("수정될 체크 항목이 없습니다.");
        return false;
    }
}

///////////////////////////////////////////////////////////////////////////////////////////////
//  ManualOrder 저장시  Validation 체크        
///////////////////////////////////////////////////////////////////////////////////////////////
function IsValidation_ManualOrder() {

    var isReturnValue = true;
    if ($("#txtAOQ_QTY").val().trim().length == 0 || $("#txtAOQ_QTY").val() == 0) {
        isReturnValue = false;
        alert("AOQ_QTY값을 입력하세요!");
        $("#txtAOQ_QTY").focus();
        $("#txtAOQ_QTY").select();
    }

    return isReturnValue;
}

///////////////////////////////////////////////////////////////////////////////////////////////
// Rounding  저장시  Validation 체크        
///////////////////////////////////////////////////////////////////////////////////////////////
function IsValidation_Rounding() {
    var isReturnValue = true;


    if ($("#ddlstrRUD_ID").val().trim().length == 0 || $("#ddlstrRUD_ID").val() == "") {
        isReturnValue = false;
        alert("Rounding 값을 선택하세요!");
    } else if ($("#ddlstrRUD_LEVEL").val().trim().length == 0 || $("#ddlstrRUD_LEVEL").val() == "") {
        isReturnValue = false;
        alert("Rounding Level을 선택하세요!");
    } else if ($("#ddlstrSUP_TERM_ID").val().trim() == "T" && ($("#txtstrRUD_START_DATE").val().trim() == "" || $("#txtstrRUD_END_DATE").val().trim() == "")) {
        isReturnValue = false;
        alert("임시설정 날짜 입력하세요!");
        if ($("#txtstrRUD_START_DATE").val().trim() == "") {
            $("#txtstrRUD_START_DATE").focus();
            $("#txtstrRUD_START_DATE").select();
        }
        if ($("#txtstrRUD_END_DATE").val().trim() == "") {
            $("#txtstrRUD_END_DATE").focus();
            $("#txtstrRUD_END_DATE").select();
        }
    } 
    return isReturnValue;
}


///////////////////////////////////////////////////////////////////////////////////////////////
// OrderMethod  저장시  Validation 체크        
///////////////////////////////////////////////////////////////////////////////////////////////
function IsValidation_OrderMethod() {
    var isReturnValue = true;


    if ($("#ddlstrREPL_METHOD").val().trim().length == 0 || $("#ddlstrREPL_METHOD").val() == "") {
        isReturnValue = false;
        alert("발주방법을 선택하세요!");
    }
    return isReturnValue;
}

///////////////////////////////////////////////////////////////////////////////////////////////
// SafertyStock  저장시  Validation 체크        
///////////////////////////////////////////////////////////////////////////////////////////////

function IsValidation_SafertyStock() {
    var isReturnValue = true; 
    if ($("#txtSFS_FIXED_VALUE").val().trim().length == 0 || $("#txtSFS_FIXED_VALUE").val() == "") {
        isReturnValue = false;
        alert("수동입력값을 선택하세요!");
    } else if ($("#txtSFS_RATE").val().trim().length == 0 || $("#txtSFS_RATE").val() == "") {
        isReturnValue = false;
        alert("Safety rate를(을) 입력하세요!");
    } else if ($("#ddlstrSFS_TERM_ID").val().trim() == "T" && ($("#txtstrSFS_START_DATE").val().trim() == "" || $("#txtstrSFS_END_DATE").val().trim() == "")) {
        isReturnValue = false;
        alert("임시설정 날짜 입력하세요!");
        if ($("#txtstrSFS_START_DATE").val().trim() == "") {
            $("#txtstrSFS_START_DATE").focus();
            $("#txtstrSFS_START_DATE").select();
        }
        if ($("#txtstrSFS_END_DATE").val().trim() == "") {
            $("#txtstrSFS_END_DATE").focus();
            $("#txtstrSFS_END_DATE").select();
        }
    } 
    return isReturnValue;
}
///////////////////////////////////////////////////////////////////////////////////////////////
//  Product  저장시  Validation 체크        
///////////////////////////////////////////////////////////////////////////////////////////////

function IsValidation_Product() {
    var isReturnValue = true;
    if ($("#txtPopup_ProductCode").val().trim().length == 0 || $("#txtPopup_ProductCode").val() == 0) {
        isReturnValue = false;
        alert("상품코드를 입력하세요!");
        $("#txtPopup_ProductCode").focus();
        $("#txtPopup_ProductCode").select();
    }
    else if ($("#txtPopup_Layersize").val().trim().length == 0 || $("#txtPopup_Layersize").val() == 0) {
        isReturnValue = false;
        alert("Layer size를 입력하세요!");
        $("#txtPopup_Layersize").focus();
        $("#txtPopup_Layersize").select();
    }
    else if ($("#txtPopup_Palletsize").val().trim().length == 0 || $("#txtPopup_Palletsize").val() == 0) {
            isReturnValue = false;
            alert("Pallet size를 입력하세요!");
            $("#txtPopup_Palletsize").focus();
            $("#txtPopup_Palletsize").select();
    }
    else if ($("#txtPopup_OrderGroup").val().trim().length == 0 || $("#txtPopup_OrderGroup").val() == 0) {
        isReturnValue = false;
        alert("발주그룹을 입력하세요!");
        $("#txtPopup_OrderGroup").focus();
        $("#txtPopup_OrderGroup").select();
    } 
    //isReturnValue = false;
    return isReturnValue;
}
///////////////////////////////////////////////////////////////////////////////////////////////
//   Cooperative 저장시  Validation 체크 
///////////////////////////////////////////////////////////////////////////////////////////////

function IsValidation_Cooperative() {
    var isReturnValue = true;

    var cbstrW_MON = document.getElementById("cbstrW_MON");
    var cbstrW_TUE = document.getElementById("cbstrW_TUE");
    var cbstrW_WED = document.getElementById("cbstrW_WED");
    var cbstrW_THU = document.getElementById("cbstrW_THU");
    var cbstrW_FRI = document.getElementById("cbstrW_FRI");
    var cbstrW_SAT = document.getElementById("cbstrW_SAT");
    var cbstrW_SUN = document.getElementById("cbstrW_SUN");

    if ($("#txtstrORDER_GROUP").val().trim().length == 0 || $("#txtstrORDER_GROUP").val() == "") {
        isReturnValue = false;
        alert("발주그룹을 입력하세요!");
        $("#txtstrORDER_GROUP").focus();
        $("#txtstrORDER_GROUP").select();
    } else if ($("#ddlstrSUP_TERM_ID").val().trim() == "T" && ($("#txtstrSUP_START_DATE").val().trim() == "" || $("#txtstrSUP_END_DATE").val().trim() == "")) {
        isReturnValue = false;
        alert("임시설정 날짜 입력하세요!");
        $("#ddlstrSUP_TERM_ID").focus();
        $("#ddlstrSUP_TERM_ID").select();
    } else if (!cbstrW_MON.checked && !cbstrW_TUE.checked && !cbstrW_WED.checked && !cbstrW_THU.checked && !cbstrW_FRI.checked && !cbstrW_SAT.checked && !cbstrW_SUN.checked) {
        isReturnValue = false;
        alert("발주일을 선택하여 주십시요.");
    }
    return isReturnValue;
}

///////////////////////////////////////////////////////////////////////////////////////////////
//  아이템 코드 9자리  
///////////////////////////////////////////////////////////////////////////////////////////////
function fnMakeItemLandgth(v) {
    var tmp="";
    var vLength = v.length;
    for (var i = 0; i < 9 - vLength; i++) {
        tmp += "0";
    }
    tmp = tmp + v;
    return tmp;
}
///////////////////////////////////////////////////////////////////////////////////////////////
//   알파벳인지 체크
/////////////////////////////////////////////////////////////////////////////////////////////// 
function isAlphabet(ch) {
    var numUnicode = ch.charCodeAt(0); // number of the decimal Unicode
    if ( 65 <= numUnicode && numUnicode <= 90 ) return true; // 대문자
    if ( 97 <= numUnicode && numUnicode <= 122 ) return true; // 소문자
    return false;
}
///////////////////////////////////////////////////////////////////////////////////////////////
//   한글인지 체크
/////////////////////////////////////////////////////////////////////////////////////////////// 
function isKorean(ch) {
    var numUnicode = ch.charCodeAt(0);
    if (44032 <= numUnicode && numUnicode <= 55203 || 12593 <= numUnicode && numUnicode <= 12643) return true;
    return false;
}
///////////////////////////////////////////////////////////////////////////////////////////////
//    
/////////////////////////////////////////////////////////////////////////////////////////////// 

///////////////////////////////////////////////////////////////////////////////////////////////
//    
/////////////////////////////////////////////////////////////////////////////////////////////// 

///////////////////////////////////////////////////////////////////////////////////////////////
//    
/////////////////////////////////////////////////////////////////////////////////////////////// 

///////////////////////////////////////////////////////////////////////////////////////////////
//    
/////////////////////////////////////////////////////////////////////////////////////////////// 


//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// JQuery 시작
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

///////////////////////////////////////////////////////////////////////////////////////////////
// 아이템 코드입력하면 이름 나오게 하는 함수
///////////////////////////////////////////////////////////////////////////////////////////////  
function jsfn_Search_Itemtext() { 
        var _ITEM = $('#txtItem').val().replace(/-/g, '');
        if (_ITEM == '') { alert('ITEM 정보가 잘못 되었습니다. 확인해  주세요.'); return false; }
        _ITEM = fnMakeItemLandgth(_ITEM);
        $('#txtItem').val(_ITEM);
        var SpName = "SP_WEB_DATA_SAMPLE2_LIST1SEARCH1_R";
        var SpParams = "ITEM" + '▥' + _ITEM;

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
            , success: function (data) {
                ItrmSuccess(data)
            }
            , error: function (msg) {
                alert(msg);
            }
        }); 
    return false;
}

///////////////////////////////////////////////////////////////////////////////////////////////
// 협력업체 코드입력하면 이름 나오게 하는 함수
/////////////////////////////////////////////////////////////////////////////////////////////// 
function jsfn_Search_Supptext() {
    var _ITEM = $('#txtSupp').val().replace(/-/g, '');
    if (_ITEM == '') { alert('협력업체 정보가 잘못 되었습니다. 확인해  주세요.'); return false; }
    var SpName = "SP_WEB_DATA_SUPPTONAME_R";
    var SpParams = "ITEM" + '▥' + _ITEM; 
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
            , success: function (data) {
                ItrmSuccessSUPP(data)
            }
            , error: function (msg) {
                fn_AjaxError(msg);
            }
        }); 
    return false;
}

///////////////////////////////////////////////////////////////////////////////////////////////
//   
///////////////////////////////////////////////////////////////////////////////////////////////