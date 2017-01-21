
$(document).ready(function () { 
    jQueryReady();
    forcuseEvent();
});
//로딩시 포커스 문제
function forcuseEvent() {
    //document.getElementById("btnSearch").attributes["onfocus", "blur()"];
}
// jQuery 시작 부분
function jQueryReady()
{
    // 메뉴
    var $gnb = $("#gnb>ul");

    $gnb.find(">li>a").on("mouseenter focus", function () {
        $gnb.find(">li.on").removeClass("on").children("ul").stop();
        $(this).next().stop().parent().addClass("on");
    });
    $gnb.on("mouseleave", gnbReturn);

    function gnbReturn() {
        $gnb.find(">li.on").removeClass("on").children("ul").stop();
    }


    // 리스트(Grid) 오버, 클릭
    $("#grid table tbody tr").on({
        'mouseeneter mouseover': function (e) {
            $(this).addClass('hover').siblings().removeClass('hover');
        },
        "mouseleave mouseout": function (e) {
            $(this).removeClass('hover')
        },
        click: function (e) {
            $(this).addClass('active').siblings().removeClass('active');
        }
    });

    //jQuery UI Datepicker
    $("input[validation=date]").datepicker({
        showOn: "button",								        // 버튼과 텍스트 필드 또는 모두 캘린더에 표시할지에 대한 여부 선택, both/button,text
        buttonImage: "/Images/sub/calendar.png",	            // image 경로 지정, 버튼 이미지 선택
        buttonImageOnly: true,							        // 버튼에 있는 이미지만 표시, true/false
        buttonText: "Select date",
        changeMonth: true,								        // 월을 바꿀 수 있는 select 박스 표시, true/false
        changeYear: true,									    // 년을 바꿀 수 있는 select 박스 표시, true/false
        nextText: '다음달',								        // next 아이콘의 title 설정
        prevText: '이전달',								        // prev 아이콘의 title 설정
        showButtonPanel: true,						            // 캘린더 하단에 (today,done) 버튼 패널 표시 여부 선택
        closeText: '닫기',									    // close 버튼의 text 선택
        dateFormat: 'yy-mm-dd',						            // 표시할 날짜 형식 선택
        dayNames: ['월요일', '화요일', '수요일', '목요일', '금요일', '토요일', '일요일'],			            // 표시할 요일 이름
        dayNamesMin: ['월', '화', '수', '목', '금', '토', '일'],											    // 표시할 요일 약자 이름
        monthNames: ['1월', '2월', '3월', '4월', '5월', '6월', '7월', '8월', '9월', '10월', '11월', '12월'],	// 표시할 달 이름
        monthNamesShort: ['1', '2', '3', '4', '5', '6', '7', '8', '9', '10', '11', '12'],						// 표시할 달 약자 이름
    });
    $("input[validation=date][disabled=disabled]").datepicker("option", "disabled", true);
    $(".ui-datepicker-trigger").css("cursor","pointer");
}




// 팝업창
function showWindow(url, w, h) {
    window.open(url, '', 'width=' + w + ',height=' + h + ',resizable=no,status,scrollbars=yes');
}

function showWindow2(url, w, h) {
    window.open(url, '', 'width=' + w + ',height=' + h + ',resizable,status,scrollbars=no');
}

function showWindow3(url, w, h) {
    window.open(url, '', 'width=' + w + ',height=' + h + ',resizable,status,scrollbars');
}
function WindowOpen(url, w, h) {
    window.open(url, '', 'width=' + w + ',height=' + h + ',resizable=no,status,scrollbars=no');
}

function WindowOpenResize(url, w, h) {
    window.open(url, '', 'width=' + w + ',height=' + h + ',resizable,status,scrollbars=no');
}

// 로그아웃 확인
function ConfirmLogOut() {
    return confirm('로그아웃 하시겠습니까?');
}


// URL 파라미터값 받기
function Request(valuename) {
    var rtnval = "";
    var nowAddress = unescape(location.href);
    var parameters = (nowAddress.slice(nowAddress.indexOf("?") + 1, nowAddress.length)).split("&");

    for (var i = 0; i < parameters.length; i++) {
        var varName = parameters[i].split("=")[0];
        if (varName.toUpperCase() == valuename.toUpperCase()) {
            rtnval = parameters[i].substr(varName.length);
            break;
        }
    }
    return rtnval;
}




