function jsfn_AjaxError(x, e) {
    if (x.status != 200) {
        if (x.status == 0) {
            $().toastmessage('showErrorToast', '오프라인 상태입니다. 네트워크를 확인하세요');
        } else if (x.status == 404) {
            $().toastmessage('showErrorToast', 'Requested URL not found.');
        } else if (x.status == 500) {
            $().toastmessage('showErrorToast', 'Internel Server Error. 서버에러 입니다.관리자에게 문의 해주세요.');
        } else if (e == 'parsererror') {
            $().toastmessage('showErrorToast', 'Error.nParsing JSON Request failed. JSON Data에 문제가있습니다. 관리자에게 문의 해주세요.');
        } else if (e == 'timeout') {
            $().toastmessage('showErrorToast', 'Request Time out. 서버에 과부하가 걸린듯 합니다. 잠시후 다시 시도해 주세요.');
        } else {
            $().toastmessage('showErrorToast', 'Unknow Error.\n' + x.responseText + '\n위 내용을 캡처해서 관리자에게 문의 해주세요.');
        };
    }
    //$().toastmessage('showToast', x.responseText + '\n' + e);
}
function jsfn_alert(msg) {
    $().toastmessage('showToast', {
        text: msg,
        sticky: true,
        position: 'top-center',
        type: 'warning',
        closeText: '',
        close: function () {
            //console.log("닫기...");
        }
    });
}
function jsfn_alert2(msg , position) {
    $().toastmessage('showToast', {
        text: msg,
        sticky: true,
        position: position,    //'middle-center' 
        type: 'warning',
        closeText: '',
        close: function () {
            //console.log("닫기...");
        }
    });
}
function jsfn_alertClose(msg) {
    $().toastmessage('showToast', {
        text: msg,
        sticky: false,
        position: 'top-center',
        type: 'notice',
        closeText: '',
        close: function () {
            //console.log("toast is closed ...");
        }
    });
}


