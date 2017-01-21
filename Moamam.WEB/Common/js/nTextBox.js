/*******************************************************************************/
// 입력 제한
/*******************************************************************************/	
//숫자만을 기입
function onlyNumber() { 
    // alert(event.keyCode);
    if ((event.keyCode < 48 || event.keyCode > 57)
		&& event.keyCode != 13
		&& event.keyCode != 9
		&& event.keyCode != 8
		&& event.keyCode != 45)
        //엔터와 백스페이스,탭키,스페이스키,델리트,그레이키숫자값,방향키값,키보드위숫자값만 입력가능하게 한다.
        try { event.preventDefault(); } catch (e) { event.returnValue = false; }
}

function onlyDecimal() { 
    // alert(event.keyCode);
    if ((event.keyCode < 48 || event.keyCode > 57)
		&& event.keyCode != 13
		&& event.keyCode != 9
		&& event.keyCode != 8
		&& event.keyCode != 45
		&& event.keyCode != 46)
        //엔터와 백스페이스,탭키,스페이스키,델리트,그레이키숫자값,방향키값,키보드위숫자값만 입력가능하게 한다.
        try { event.preventDefault(); } catch (e) { event.returnValue = false; }
}


//양수만 입력
function UnSignedNumber() { 
    if ((event.keyCode < 48 || event.keyCode > 57)
		&& event.keyCode != 13
		&& event.keyCode != 9
		&& event.keyCode != 8)
        try { event.preventDefault(); } catch (e) { event.returnValue = false; }
}

//입력된 값에 날짜형식으로 '-' 추가한다.
function setDateFormat(obj) { 
    var val = obj.value;
    val = val.replace(/-/g, '');
    val = val.replace(/\D/g, "");

    if (val.length > 6) {
        val = val.substr(0, 4) + "-" + val.substr(4, 2) + "-" + val.substr(6);
    } else if (val.length > 4) {
        val = val.substr(0, 4) + "-" + val.substr(4);
    }

    obj.value = val;
}

// 금액 표시
function NumericTextBox_OnChange(obj) {
    obj.value = NumericTextBox_FormatString(obj.value, obj.attributes["FormatString"].value);
}

// 금액 표시
function NumericTextBox_OnFocus(obj) {
    if (obj.value.indexOf(',') > 0) {
        obj.value = obj.value.toString().replace(/\$|\\|\,/g, '');
    }
}

// 금액 표시
function NumericTextBox_FormatString(value, formatString) {
    var length = 0;
    var returnValue = "";
    var num = new Number();
    var absValue;

    value = value.toString().replace(/\$|\\|\,/g, '').replace(/^\s+|\s+$/g, '');
    if (formatString.split('.').length == 2)
        length = formatString.split('.')[1].length;

    if (!isNaN(value) && value != "") {
        num = (parseFloat(value)).toFixed(length);

        if (formatString.split('.')[0] == "#,###")
            absValue = parseFloat(value).toLocaleString().split('.')[0];
        else
            absValue = parseFloat(value).toString().split('.')[0];

        absValue =
        returnValue = ((parseFloat(value) < 0) ? "" : "")
            + absValue
            + ((length > 0) ? "." + num.split('.')[1] : "");
    }
    else {
        num = (parseFloat(0)).toFixed(length);
        returnValue = num.split('.')[0]
            + ((length > 0) ? "." + num.split('.')[1] : "");
    }
    if (value == 0) {
        return "";
    }
    return returnValue;
}


/*******************************************************************************/
// 유효성 처리
/*******************************************************************************/	
// Validation Check
function checkCondition(frmname, valG) {
    var frmobj = eval('document.forms[\'' + frmname + '\']');
    var elements = frmobj.elements;

    for (var i = 0; i < elements.length ; i++) {
        var elementtype = new String(elements[i].type).toLowerCase();
        var hname = null;

        if (elements[i].getAttribute("validationgroup") != null) {
            if (elements[i].getAttribute("validationgroup").toLowerCase() == valG.toLowerCase()) {
                if (typeof (elements[i].getAttribute("exceptiondesc")) != 'undefined') {
                    hname = elements[i].getAttribute("exceptiondesc");
                }

                //필수 텍스트 필드에 대한 처리
                if (elements[i].getAttribute("nrequired").toLowerCase() == 'true') {
                    if (elements[i].value == "") {
                        elements[i].focus();
                        alert(hname + "을(를) 입력하세요");
                        return false;
                    }
                }

                // 텍스트 필드에 대한 처리
                if (elementtype == 'text' || elementtype == 'textarea' || elementtype == 'password' || elementtype == 'file') {
                    // 고정 길이에 대한 처리
                    if (elements[i].getAttribute("maxlength") != null) {
                        if (elements[i].getAttribute("maxlength") != '2147483647') {
                            if (elements[i].value.length > 0) {
                                if (elements[i].value.length > parseInt(elements[i].getAttribute("maxlength"))) {
                                    if (typeof (elements[i].getAttribute("exceptiondesc")) != 'undefined') {
                                        alert(hname + '를 ' + elements[i].getAttribute("maxlength") + '글자이하로 입력해 주세요.');
                                    } else {
                                        alert('이 항목을 ' + elements[i].getAttribute("maxlength") + '글자이하로 입력해 주세요.');
                                    }

                                    elements[i].focus();
                                    elements[i].select();
                                    return false;
                                }
                            }
                        }
                    }

                    // 고정 최소 길이에 대한 처리
                    if (elements[i].getAttribute("minlength") != null) {
                        if (elements[i].getAttribute("minlength") != '2147483647') {
                            if (elements[i].value.length > 0) {
                                if (elements[i].value.length < parseInt(elements[i].getAttribute("minlength"))) {
                                    if (typeof (elements[i].getAttribute("exceptiondesc")) != 'undefined') {
                                        alert(hname + '를 ' + elements[i].getAttribute("minlength") + '글자로 입력해 주세요.');
                                    } else {
                                        alert('이 항목을 ' + elements[i].getAttribute("minlength") + '글자로 입력해 주세요.');
                                    }

                                    elements[i].focus();
                                    elements[i].select();
                                    return false;
                                }
                            }
                        }
                    }

                    //날짜에 대한 처리(영문자, 숫자만 허용할 경우)
                    if (new String(elements[i].getAttribute("validation")).toLowerCase() == 'date') {
                        if (typeof (elements[i].value) != 'undefined') {
                            if (!checkday(elements[i])) {
                                if (elements[i].value != '') {
                                    //alert(hname+'을(를) 입력해 주세요.');

                                    elements[i].focus();
                                    elements[i].select();
                                    return false;
                                }
                            }
                        }
                    }
                    // 숫자만 입력받는 필드에 대한 처리
                    if (new String(elements[i].getAttribute("validation")).toLowerCase() == 'numeric') {
                        if (!numericCheck(elements[i].value)) {
                            if (hname == null) {
                                alert('이 항목에는 숫자만 입력해 주세요.');
                            }
                            else {
                                alert(hname + '에는 숫자만 입력해 주세요.');
                            }
                            elements[i].focus();
                            elements[i].select();
                            return false;
                        }
                    }

                    // 소수과 숫자만 입력받는 필드에 대한 처리
                    if (new String(elements[i].getAttribute("validation")).toLowerCase() == 'decimal') {
                        if (!decimalCheck(elements[i].value)) {
                            if (hname == null) {
                                alert('이 항목에 숫자만 입력해 주세요.');
                            }
                            else {
                                alert(hname + '에는 숫자만 입력해 주세요.');
                            }
                            elements[i].focus();
                            elements[i].select();
                            return false;
                        }
                    }
                }
            }
        }
    }
    return true;
}

function decimalCheck(value)
{
    value = value.replace(/,/g,'');
	var objReg = new RegExp('[^0-9]');
	if (objReg.test(value.replace('.','')))
	{
		return false;
	} else {
		return true;
	}
}

function checkday(form_name) {
   var str;
   str= eval(form_name).value;
   if (str == "") {
		return false;
   }else{
		if(str.match(/[0-9/-]+/)!=str)
		{
			alert("날짜에는 숫자만 됩니다 !");
			eval(form_name).value = "";
			return false;
		}else{
			var che_year;
		 	var che_month;
		 	var che_day;

          	if(str.length==10){
  		 		che_year  = str.substr(0,4)
  		 		che_month = str.substr(5,2)
  		 		che_day   = str.substr(8,2)
  		 		che_chk1  = str.substr(4,1)
  		 		che_chk2  = str.substr(7,1)

  		    if(che_chk1!="-" || che_chk2 !="-")
  		    {  
  		      alert ("유효한 날짜가 아닙니다.");
              return false;
              }
            if (checkdate(che_year,che_month,che_day) == false) {
              alert ("유효한 날짜가 아닙니다.");
              return false;
              }
      			} else{
                alert ("유효한 날짜가 아닙니다.");
  				return false;
  		    	}
        }
        
        return true;
	}
}

function numericCheck(value)
{
    value = value.replace(/,/g,'');
	var objReg = new RegExp('[^0-9]');
	if (objReg.test(value))
	{
		return false;
	} else {
		return true;
	}
}


/*******************************************************************************/
// 키보드 이벤트 처리
/*******************************************************************************/	
// 리스트바인딩 안 텍스트박스 화살표로 이동
function SelectNextRow(objID) {
    try {
        if (window.event.keyCode == 38 || window.event.keyCode == 40 || window.event.keyCode == 13) {
            var flagIdx = 1;
            if (window.event.keyCode == 38) flagIdx = -1;
            var obj = $get(objID);
            var arrName = obj.name.split('$');
            var targetId = '';

            for (var i = 0; i < arrName.length - 2; i++) {
                targetId += arrName[i] + '_';
            }
            var idx = parseInt(arrName[arrName.length - 2].substring(4), 0) + flagIdx;
            targetId += 'ctrl' + idx;
            targetId += '_' + arrName[arrName.length - 1];
            var objTarget = $get(targetId);
            objTarget.focus();
            window.event.returnValue = false;
        }
    }
    catch (e) {
    }
}


function EnterToTab() {
    if (window.event && event.keyCode == 13) {
        event.keyCode = 9;
        return event.keyCode;
    }
}

function EnterToNoAction() {
    if (window.event && event.keyCode == 13) {
        return false;
    }
}

function EnterToSubmit(submitButton, submitArgs) {
    if (window.event && event.keyCode == 13) {
        __doPostBack(submitButton, submitArgs);
        return false;
    }
}
