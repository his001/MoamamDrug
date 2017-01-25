
// 1 page Js
function jsfn_isFirst(strYN) {
    if (strYN == 'Y') {
        $("#LyQ1_Y1").css('display', 'none');
        $("#LyQ1_Y1_Reg1").css('display', 'block');
    } else {
        $("#LyQ1_Y1").css('display', 'none');
        $("#LyQ1_Y1_Login1").css('display', 'block');
    }
}
function jsfn_isFirst2YN(strYN) {
    // #### step 1 은 닫는다 ####
    $("#LyQ1_Y1").css('display', 'none');
    $("#LyQ1_Y1_Reg1").css('display', 'none');
    $("#LyQ1_Y1_Login1").css('display', 'none');
    if (strYN == 'Y') {
        $("#LyQ1_Y1").css('display', 'none');
        $("#LyQ2_YN_ALL").css('display', 'block');
    } else {
        $("#LyQ1_Y1").css('display', 'none');
        $("#LyQ2_YN_ALL").css('display', 'block');
    }
}
function jsfn_isFirst3YN(strYN) {
    // #### step 1 은 닫는다 ####
    $("#LyQ1_Y1").css('display', 'none');
    $("#LyQ1_Y1_Reg1").css('display', 'none');
    $("#LyQ1_Y1_Login1").css('display', 'none');
    $("#LyQ2_YN_ALL").css('display', 'none');
    if (strYN == 'Y') {
        jsfn_alertClose('축하 드려요~~!!');
        //Lysec_BokYongHooKi_LstGrid
        //document.location.href = "#Lysec_BokYongHooKi_Write";
    }
    $("#LyQ3_YN_ALL").css('display', 'block');
}

//로그인관련 아이디 저장 부분
function jsfn_SetSaveIDchkChg() {
    var _isChk = $("#CheckSaveUserID").prop('checked');
    var _userID = $("#txtUserID").val();
    if (_isChk) {
        jsfn_setCookie('CookUserID', _userID, 30);
        jsfn_setCookie('CookIdAutoYN', 'Y', 30);
    } else {
        jsfn_setCookie('CookUserID', '', 30);
        jsfn_setCookie('CookIdAutoYN', 'N', 30);
    }

    var _isPwdChk = $("#CheckAotoLogin").prop('checked');
    if (_isPwdChk) {
        var _userPWD = $("#txtPwd").val();
        jsfn_setCookie('CookUserPWD', _userPWD, 30);
        jsfn_setCookie('CookIdAutoPWD', 'Y', 30);
    }    
}
function jsfn_chkCookieID() {
    var _IdSaveAutoYN = jsfn_getCookie("CookIdAutoYN");
    var _CookUserID = jsfn_getCookie("CookUserID");
    if (_IdSaveAutoYN == 'Y') {
        $("#CheckSaveUserID").prop('checked', 'true');
        $("#txtUserID").val(_CookUserID);
        $("#txtPwd").focus();
    }

    // 자동 로그인
    var _CookIdAutoPWD = jsfn_getCookie("CookIdAutoPWD");
    var _CookUserPWD = jsfn_getCookie("CookUserPWD");
    if (_CookIdAutoPWD == 'Y') {
        $("#CheckAotoLogin").prop('checked', 'true');
        $("#txtPwd").val(_CookUserPWD);
        jsfn_LoginChk();
    }
}
function jsfn_getCookie(c_name) {
    var i, x, y, ARRcookies = document.cookie.split(";");
    for (i = 0; i < ARRcookies.length; i++) {
        x = ARRcookies[i].substr(0, ARRcookies[i].indexOf("="));
        y = ARRcookies[i].substr(ARRcookies[i].indexOf("=") + 1);
        x = x.replace(/^\s+|\s+$/g, "");
        if (x == c_name) {
            return unescape(y);
        }
    }
}
function jsfn_setCookie(c_name, value, exdays) {
    var exdate = new Date();
    exdate.setDate(exdate.getDate() + exdays);
    var c_value = escape(value) + ((exdays == null) ? "" : "; expires=" + exdate.toUTCString());
    document.cookie = c_name + "=" + c_value;
}
//암호변경
function jsfn_ChangePwd() {
    var _txtChgUserID = $("#txtChgUserID").val();
    var _txtOrgPwd = $("#txtOrgPwd").val();
    var _txtNewPwd = $("#txtNewPwd").val();
    _txtNewPwd = _txtNewPwd.replace(/'/g, '');
    _txtNewPwd = _txtNewPwd.replace(/--/g, '');
    if (_txtChgUserID == '') { jsfn_alertClose("아이디를 입력하세요!"); $("#txtChgUserID").focus(); return false; }
    if (_txtOrgPwd == '') { jsfn_alertClose("기존비밀번호를 입력하세요!"); $("#txtOrgPwd").focus(); return false; }
    if (_txtNewPwd == '') { jsfn_alertClose("신규비밀번호를 입력하세요!"); $("#txtNewPwd").focus(); return false; }
    if (_txtNewPwd.length < 7) { jsfn_alertClose("신규비밀번호는 8자 이상 이어야 합니다."); $("#txtNewPwd").focus(); return false; }
    if (_txtNewPwd.indexOf("'") > -1 || _txtNewPwd.indexOf("--") > -1) { jsfn_alertClose("비밀번호에는 ' 혹은 -- 는 입력 할 수없습니다."); $("#txtNewPwd").focus(); return false; }
    $.ajax({
        url: '/Site/Data/Data.aspx'
        , type: "post"
        , async: false
        , data: {
            fnName: "CommonPWdChange"
            , chgid: _txtChgUserID
            , opw: _txtOrgPwd
            , npw: _txtNewPwd
        }
        , success: function (msg) { jsfn_PwdChgProcMsg(msg); }
        , error: function (msg) { fn_AjaxError(msg); }
    });

}
function jsfn_PwdChgProcMsg(msg) {
    if (msg == 'PwdChgSuccess') {
        jsfn_alertClose("암호가 변경 되었습니다.");
        $("#jsfn_alertClose").css('display', 'none');
    } else if (msg == 'pwdDiff') {
        jsfn_alertClose("암호가 다릅니다.");
    } else if (msg == 'err') {
        jsfn_alertClose("에러가 발생 하였습니다.");
    }
    $("#txtOrgPwd").val('');
    $("#txtNewPwd").val('');
}

//로그인관련 체크 부분
function jsfn_LoginChk() {

    var _txtUserID = $("#txtUserID").val();
    var _txtPwd = $("#txtPwd").val();
    if (_txtUserID == '') {
        jsfn_alertClose("아이디를 입력하세요!");
        $("#txtUserID").focus();
        return false;
    }
    if (_txtPwd == '') {
        jsfn_alertClose("비밀번호를 입력하세요!");
        $("#txtPwd").focus();
        return false;
    }
    jsfn_SetSaveIDchkChg();
    jsfn_CommonLoginChk(_txtUserID, _txtPwd);
}
function jsfn_CommonLoginChk(id, pwd) {
    $.ajax({
        url: '/Site/Data/Data.aspx'
        , type: "post"
        , async: false
        , data: {
            fnName: "CommonLogin"
            , uid: id
            , upw: pwd
        }
        , success: function (msg) { jsfn_LoginProcessMsg(msg); }
        , error: function (msg) { fn_AjaxError(msg); }
    });
}
function jsfn_LoginProcessMsg(msg) {
    if (msg == 'loginSuccess') {
        jsfn_alertClose("로그인 되었습니다.");
        var _str = "<a href='#' onclick='jsfn_UserLogOut();'>" + $("#txtUserID").val() + "</a>";
        $("#LyLoginID").html(_str);
        $("#Login_userId").val($("#txtUserID").val());
        $("#LnkPassChange").css('visibility', 'visible');
        $("#Lysec_BokYongHooKi_Best").css('display', 'block');
        $("#txtChgUserID").val($("#txtUserID").val());
        jsfn_isFirst2YN('Y');
    } else if (msg == 'wrongPwd') {
        jsfn_alertClose("암호가 다릅니다.");
    } else if (msg == 'noID') {
        jsfn_alertClose("ID를 찾을 수 없습니다.");
    }
}
function jsfn_UserLogOut() {
    $.ajax({
        url: '/Site/Data/Data.aspx'
        , type: "post"
        , async: false
        , data: { fnName: "CommonLogOut" }
        , success: function (msg) {
            if (msg == 'LogOutOk') {
                jsfn_alertClose("로그아웃 되었습니다.");
                document.location.href = "/";
            }
        }
        , error: function (msg) { if (msg != '') { fn_AjaxError(msg); } }
    });
}

function jsfn_SendPwdEmail() {
    jsfn_alert("ID 의 Email 로 새로운 암호가 발송 되었습니다.");
}

//회원가입
function jsfn_RegistChk() {
    var _txtRegistID = $("#txtRegistID").val();
    var _txtMemName = $("#txtMemName").val();

    // 정규식 - 이메일 유효성 검사
    var regEmail = /([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$/;
    if (_txtRegistID == '') {
        jsfn_alert("회원가입 Email을 입력하세요!");
        $("#txtRegistID").focus();
        return false;
    } else {
        if (!regEmail.test(_txtRegistID)) {
            jsfn_alert('이메일 주소가 유효하지 않습니다');
            $("#txtRegistID").focus();
            return false;
        }
    }
    _txtMemName = _txtMemName.replace(/\'/g, '');

    $.ajax({
        url: '/Site/Data/Data.aspx'
            , type: "post"
            , async: false
            , data: {
                fnName: "CommonReg"
                , regid: _txtRegistID
                , regNm: _txtMemName
            }
            , success: function (msg) { jsfn_RegistProcessMsg(msg); }
            , error: function (msg) { fn_AjaxError(msg); }
    });
}

function jsfn_RegistProcessMsg(msg) {
    if (msg == 'RegSuccess') {
        jsfn_alert("회원가입 메일로 자동 생성된 비밀번호가 발송되었습니다.");
        setTimeout("jsfn_isFirst('N')", 2000);
    } else if (msg == 'SameID') {
        jsfn_alertClose("이미 등록된  Email 입니다.");
    } else if (msg == 'err') {
        jsfn_alertClose("에러가 발생했습니다.");
    }
}

function jsfn_setMkParam(str) {
    
    try {
        if (str != '' && str != 'undefined' ) {
            //str = str.replace(/</g,"&lt;");
            //str = str.replace(/>/g,"&gt;");
            //str = str.replace(/\"/g,"&quot;");
            //str = str.replace(/\'/g,"&#39;");
            //str = str.replace(_br, "<br />");
            //str = str.relpace(/▥/g, "▦");
            //str = str.relpace(/▤/g, "▩");
            //str = str.replace(/--/g, '');
            //str = str.relpace(/-/g, "↔");
        }
    } catch (e) {
        //alert(str + ' : str \n' + e.message);
    }
    return str;
}

//복용후기 작성
//#########################################
function jsfn_BokYongHooKi_Save() {
    var _txt_idx = $("#txt_idx").val();
    var _txtUserID = $("#Login_userId").val();
    var _txt_Visit_Date = $("#txt_Visit_Date").val().replace(/-/g,'');
    var _txt_NoPain_Date = $("#txt_NoPain_Date").val().replace(/-/g, '');
    var _txt_JngSang = $("#txt_JngSang").val();
    var _txt_tempC = $("#txt_tempC").val();
    var _rdoFeber = $(':radio[name="rdoFeber"]:checked').val();
    var _HaeYeolJeOX = $(':radio[name="rdoHaeYeolJeOX"]:checked').val();
    var _txt_ChouBang = $("#txt_ChouBang").val();
    var _txt_Yak_iLbun = $("#txt_Yak_iLbun").val();
    var _rdoHangSaengJeBokan = $(':radio[name="rdoHangSaengJeBokan"]:checked').val();
    var _rdoHangSaengJeEat = $(':radio[name="rdoHangSaengJeEat"]:checked').val();
    var _txt_ChamGoSaHang = $("#txt_ChamGoSaHang").val();
    var _txt_BokYongHooKi = $("#txt_BokYongHooKi").val();
    var _txt_Userip = $("#txt_Userip").text();
    if (_txtUserID == '') { jsfn_alertClose("로그인이 필요합니다!"); return false; }
    if (_txt_Visit_Date == '') { jsfn_alertClose('방문일자 정보가 없습니다. 확인해  주세요.'); return false; }
    if (_txt_Yak_iLbun == '') { jsfn_alertClose('처방 일수 정보가 없습니다. 확인해  주세요.'); return false; }
    if (!confirm('저장하시겠습니까?')) { return false; }

    var SpName = "SPM_Web_COMMON_Tbl_BokYongHooKi_S";
    var SpParams = "UserID" + '▥' + _txtUserID + '▤';
    SpParams = SpParams + "VisitDate" + '▥' + jsfn_setMkParam(_txt_Visit_Date) + '▤';
    SpParams = SpParams + "CureDate" + '▥' + jsfn_setMkParam(_txt_NoPain_Date) + '▤';
    SpParams = SpParams + "JungSang" + '▥' + jsfn_setMkParam(_txt_JngSang) + '▤';
    SpParams = SpParams + "tempC" + '▥' + jsfn_setMkParam(_txt_tempC) + '▤';
    SpParams = SpParams + "Feber" + '▥' + jsfn_setMkParam(_rdoFeber) + '▤';
    SpParams = SpParams + "HaeYeolJe" + '▥' + jsfn_setMkParam(_HaeYeolJeOX) + '▤';
    SpParams = SpParams + "ChouBang" + '▥' + jsfn_setMkParam(_txt_ChouBang) + '▤';
    SpParams = SpParams + "Yak_iLbun" + '▥' + jsfn_setMkParam(_txt_Yak_iLbun) + '▤';
    SpParams = SpParams + "HangSaengJeBokan" + '▥' + jsfn_setMkParam(_rdoHangSaengJeBokan) + '▤';
    SpParams = SpParams + "HangSaengJeEat" + '▥' + jsfn_setMkParam(_rdoHangSaengJeEat) + '▤';
    SpParams = SpParams + "ChamGoSaHang" + '▥' + jsfn_setMkParam(_txt_ChamGoSaHang) + '▤';
    SpParams = SpParams + "BokYongHooKi" + '▥' + jsfn_setMkParam(_txt_BokYongHooKi) + '▤';
    SpParams = SpParams + "regip" + '▥' + _txt_Userip + '▤';
    SpParams = SpParams + "idx" + '▥' + _txt_idx;

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
    , success: function (data) { jsfn_Pop1SaveSuccess(); }
    , error: function (x, e) { jsfn_AjaxError(x, e); }
    });
}

function jsfn_Pop1SaveSuccess() {
    jsfn_alertClose('저장 되었습니다.');
    //$('#LyModalPop1Layer').bPopup().close();
}


function jsfn_makeToYakReq() {
    var _SMsg = "";
    var _txt_Visit_Date = $("#txt_Visit_Date").val();
    var _txt_NoPain_Date = $("#txt_NoPain_Date").val();
    var _Gtoday = new Date();
    _Gtoday = _Gtoday.toISOString().substring(0, 10);

    _SMsg = _SMsg + _txt_Visit_Date + " ~ " + _txt_NoPain_Date + "\n";
    _SMsg = _SMsg + " XX 어린이집(유아/유치원)에 " + "\n"
    _SMsg = _SMsg + " 1일 x회 투약을 의뢰합니다. " + "\n"
    _SMsg = _SMsg + "  " + "\n"
    _SMsg = _SMsg + " ※약은 소분해서 보내주시는 것을 권장 합니다. " + "\n"
    _SMsg = _SMsg + "  " + "\n"
    _SMsg = _SMsg + _Gtoday + "  홍길동 배상 ";

    $("#txt_ToYak_messageA").val(_SMsg);
}

//jQuery UI date picker plugin Copy from SomeWhere 
(function (factory) {
    if (typeof define === "function" && define.amd) {
        // AMD. Register as an anonymous module.
        define(["../jquery.ui.datepicker"], factory);
    } else {
        // Browser globals
        factory(jQuery.datepicker);
    }
}(function (datepicker) {
    datepicker.regional['ko-KR'] = {
        closeText: '닫기',
        prevText: '&#x3C;이전 월',
        nextText: '다음 월&#x3E;',
        currentText: '오늘',
        monthNames: ['1월', '2월', '3월', '4월', '5월', '6월',
            '7월', '8월', '9월', '10월', '11월', '12월'],
        monthNamesShort: ['1월', '2월', '3월', '4월', '5월', '6월',
            '7月', '8月', '9月', '10月', '11月', '12月'],
        dayNames: ['일요일', '월요일', '화요일', '수요일', '목요일', '금요일', '토요일'],
        dayNamesShort: ['일주일', '월요일', '화요일', '수요일', '목요일', '금요일', '토요일'],
        dayNamesMin: ['일', '월', '화', '수', '목', '금', '토'],
        weekHeader: '주',
        dateFormat: 'yymmdd',
        firstDay: 1,
        isRTL: false,
        showMonthAfterYear: true,
        yearSuffix: '년'
    };
    datepicker.setDefaults(datepicker.regional['ko-KR']);

    return datepicker.regional['ko-KR'];

}));

$(function () { $(".datepicker").datepicker({ format: "yyyy-mm-dd", language: "kr" }); });


function jsfn_getUserIP(onNewIP) { //  onNewIp - your listener function for new IPs
    //compatibility for firefox and chrome
    var myPeerConnection = window.RTCPeerConnection || window.mozRTCPeerConnection || window.webkitRTCPeerConnection;
    var pc = new myPeerConnection({
        iceServers: []
    }),
    noop = function () { },
    localIPs = {},
    ipRegex = /([0-9]{1,3}(\.[0-9]{1,3}){3}|[a-f0-9]{1,4}(:[a-f0-9]{1,4}){7})/g,
    key;

    function iterateIP(ip) {
        if (!localIPs[ip]) onNewIP(ip);
        localIPs[ip] = true;
    }

    //create a bogus data channel
    pc.createDataChannel("");

    // create offer and set local description
    pc.createOffer(function (sdp) {
        sdp.sdp.split('\n').forEach(function (line) {
            if (line.indexOf('candidate') < 0) return;
            line.match(ipRegex).forEach(iterateIP);
        });

        pc.setLocalDescription(sdp, noop, noop);
    }, noop);

    //listen for candidate events
    pc.onicecandidate = function (ice) {
        if (!ice || !ice.candidate || !ice.candidate.candidate || !ice.candidate.candidate.match(ipRegex)) return;
        ice.candidate.candidate.match(ipRegex).forEach(iterateIP);
    };
}

function jsfn_getDrugInfo() {
    var _url = '/Site/Data/DrugInfo.aspx';
    $('#LyPop1').bPopup({
        content: 'iframe',
        //contentContainer: '.contentPop',
        iframeAttr: 'scrolling="no" frameborder="0" width=550 height=450 background-color:#FFF ',
        loadUrl: _url
    });
}
function jsfn_btnPopExit() {
    $('#LyPop1').bPopup().close();
    $('.b-iframe').remove();
}

function jsfn_SerchItemClick4PopUp(Item, ItemName) {
    //--옵션 목록 처음에 추가	$("#sel_ChouBangYak").prepend("<option value='0'>옵션앞에추가</option>");
    //--option 목록 끝에 추가	$("#sel_ChouBangYak").append("<option value='1'>옵션끝에 추가/option>");
    //--[SelectBox 옵션 삭제]	$("#sel_ChouBangYak option:eq(10)").remove();

    var _ItemName = ItemName.replace(/'/g, '').replace(/,/g, '');
    $('#sel_ChouBangYak').append('<option value="' + Item + '">' + _ItemName + '</option>');
    jsfn_btnPopExit();
}