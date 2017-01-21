<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterPageTabSub.master" AutoEventWireup="true" CodeFile="ProductJ.aspx.cs" Inherits="Site_MasterMain_ProductJ" %>

<%@ Register Src="~/UserControls/ucPagingJS.ascx" TagName="ucPaging" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHead" runat="Server">
    
<!-- 1.LoadPage -->
<script type="text/javascript">
    var __clickCnt = 0;
    $(document).ready(function () {
        Initialize();
        //jsfn_Search();
    });
</script>
<!-- 2.Init -->
<script type="text/javascript">
    function Initialize() {
        
        $('#btnSearch').click(function () { jsfn_Search(); });
        $("#<%=txtItem.ClientID %>").keypress(function (event) {if (event.which == 13 && $("#<%=txtItem.ClientID %>").val().length==9) {return jsfn_Search_Item4text();}});
        $("#<%=txtItem.ClientID %>").keyup(function (event) {
            if ((event.which >= 48 && event.which <= 57) || (event.which >= 96 && event.which <= 105) || (event.which == 8 /* backspace*/) || (event.which == 46 /* del*/) || (event.which == 32 /* space*/)) {
                if ($("#<%=txtItem.ClientID %>").val().length == 0) { $('#<%=lblItemName.ClientID%>').text('');jsfn_Search_ItemView4textYN('N'); } // 
                if ($("#<%=txtItem.ClientID %>").val().length >= 6 && $("#<%=txtItem.ClientID %>").val().length <= 8) { jsfn_Search_ItemView4text(); }
            }
        });
        //$("#<%= ddlToSectionList.ClientID %>").val('9902');
    }
</script>
<!-- 3.Validation  -->
<script type="text/javascript">
</script>
<!-- 4. Custom Function  -->
<script type="text/javascript">

    function jsfn_Search() {
        var _ddlFromSectionList = $("#<%= ddlFromSectionList.ClientID %>").val();
        if (_ddlFromSectionList == '' || _ddlFromSectionList == null) { jsfn_alert('시작 섹션을 선택해 주세요.'); return false; }
        var _ddlToSectionList = $("#<%= ddlToSectionList.ClientID %>").val();
        if (_ddlToSectionList == '' || _ddlToSectionList == null) { jsfn_alert('종료 섹션을 선택해 주세요.'); return false; }
        var _Item = $("#<%= txtItem.ClientID %>").val().replace(/-/g, '');

        if (parseInt(_ddlFromSectionList) > parseInt(_ddlToSectionList)) {
            jsfn_alert("SECTION값이 FROM보다 TO가 작습니다.\n다시 선택하여 조회하시길 바랍니다.");
            return false;
        }

        jsfn_progressBarMst('Y');

        var _ucPageNo = $("#LyCus_hidPageNo").val();
        if (_ucPageNo == '') { _ucPageNo = '1'; }
        var _ucRowCount = $("#LyCus_hidRowCnt").val();
        if (_ucRowCount == '') { _ucRowCount = '30'; }

        var SpName = "SP_WEB_Site_MasterMain_ProductJ_List1_R";
	    var SpParams = "SECTIONFROM" + '▥' + _ddlFromSectionList + '▤';
	    SpParams = SpParams + "SECTIONTO" + '▥' + _ddlToSectionList + '▤';
	    SpParams = SpParams + "ITEM" + '▥' + _Item + '▤';
	    SpParams = SpParams + "ROWCNT" + '▥' + _ucRowCount + '▤';
	    SpParams = SpParams + "PAGENUM" + '▥' + _ucPageNo + '▤';
	    SpParams = SpParams + "SMODE" + '▥' + 'SELECT' + '▤';
	    SpParams = SpParams + "SUPPLIER" + '▥' + $('#<%=txtSuppCode.ClientID%>').val().replace(/\'/g, '') + '▤';
        SpParams = SpParams + "RUD_ID" + '▥' + $('#<%=ddlrudterm.ClientID%>').val() + '▤';
        SpParams = SpParams + "WH" + '▥' + $('#<%=ddlDccodeList.ClientID%>').val();

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
        , error: function (msg) {
            fn_AjaxError(msg);
            jsfn_progressBarMst('N');
        }
        });

    }

    function jsfn_LinktoCorp(str) {
        var _url = '/site/MasterMain/CooperativeJ.aspx?G=2&M=26&SuppCode=' + str;
        document.location.href = _url;
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
        var _replace_val1 = '';
        var _replace_val2 = '';
        for (var i = 0; i < gData.length; i++) {
            if (gData[i].ADO_START_DATE == null) { gData[i].ADO_START_DATE = ''; }
            if (gData[i].SUP_NAME == null) { gData[i].SUP_NAME = ''; }
            if (gData[i].W_MON == null) { gData[i].W_MON = ''; }
            if (gData[i].W_TUE == null) { gData[i].W_TUE = ''; }
            if (gData[i].W_WED == null) { gData[i].W_WED = ''; }
            if (gData[i].W_THU == null) { gData[i].W_THU = ''; }
            if (gData[i].W_FRI == null) { gData[i].W_FRI = ''; }
            if (gData[i].W_SAT == null) { gData[i].W_SAT = ''; }
            if (gData[i].W_SUN == null) { gData[i].W_SUN = ''; }
            if (gData[i].LEAD_TIME == null) { gData[i].LEAD_TIME = ''; }
            
            html = html + '<tr class="tr_c">';
            html = html + '<td class="t_c" style="width:60px;">' + gData[i].ORDER_GROUP + '</td>';
            html = html + '<td class="t_c">' + gData[i].SECTION + '</td>';
            html = html + '<td class="t_c"> <a href="javascript:jsfn_SetRowSelVal(\'' + gData[i].ORDER_GROUP + '\',\'' + gData[i].SECTION + '\',\''
                + gData[i].ITEM + '\',\'' + gData[i].ITEM_NAME + '\',\'' + gData[i].WH + '\',\'' + gData[i].WH_NAME + '\',\'' + gData[i].SUPPLIER + '\',\'' + gData[i].SUP_NAME + '\',\''
                + gData[i].LAYER_SIZE + '\',\'' + gData[i].PALLET_SIZE + '\',\'' + gData[i].ADO_START_DATE + '\');"><font color="blue">' + gData[i].ITEM + '</fontr></a></td> ';
            html = html + '<td class="t_c" style="text-align:left;padding-right:5px;width:10%;overflow:hidden;text-overflow:ellipsis;white-space:nowrap;">' + gData[i].ITEM_NAME + '</td>';
            html = html + '<td class="t_c" style="width:40px;">' + gData[i].WH + '</td>';
            html = html + '<td class="t_c" style="text-align:left;padding-right:5px;width:10%;overflow:hidden;text-overflow:ellipsis;white-space:nowrap;">' + gData[i].WH_NAME + '</td>';
            //html = html + '<td class="t_c" style="width:50px;"><a href="javascript:jsfn_LinktoCorp(' + gData[i].SUPPLIER + ');"><font color="blue">' + gData[i].SUPPLIER + '</font></a></td>';
            html = html + '<td class="t_c" style="width:50px;"><a href="javascript:parent.jsfn_addTopTab(\'협력업체마스터\', \'/site/MasterMain/CooperativeJ.aspx?G=2&M=26&SuppCode=' + gData[i].SUPPLIER + '\');"><font color="blue">' + gData[i].SUPPLIER + '</font></a></td>';
            html = html + '<td class="t_c" style="text-align:left;padding-right:5px;width:10%;overflow:hidden;text-overflow:ellipsis;white-space:nowrap;">' + gData[i].SUP_NAME + '</td>';
            html = html + '<td class="t_c" style="width:80px;">' + gData[i].ADO_START_DATE + '</td>';
            html = html + '<td class="t_c">' + gData[i].LAYER_SIZE + '</td>';
            html = html + '<td class="t_c">' + gData[i].PALLET_SIZE + '</td>';
            html = html + '<td class="t_c">' + gData[i].CASE_SIZE + '</td>';
            html = html + '<td class="t_c">' + gData[i].SUP_TERM_ID + '</td>';
            html = html + '<td class="t_c">' + gData[i].W_MON + '</td>';
            html = html + '<td class="t_c">' + gData[i].W_TUE + '</td>';
            html = html + '<td class="t_c">' + gData[i].W_WED + '</td>';
            html = html + '<td class="t_c">' + gData[i].W_THU + '</td>';
            html = html + '<td class="t_c">' + gData[i].W_FRI + '</td>';
            html = html + '<td class="t_c">' + gData[i].W_SAT + '</td>';
            html = html + '<td class="t_c">' + gData[i].W_SUN + '</td>';
            html = html + '<td class="t_c">' + gData[i].LEAD_TIME + '</td>';
            html = html + '<td class="t_c">' + gData[i].REPL_METHOD + '</td>';
            //html = html + '<td class="t_c">' + gData[i].SOH + '</td>';
            //html = html + '<td class="t_c">' + gData[i].SOO + '</td>';
            html = html + '</tr>';
        }
        $('#dvGrid1 > tbody:last').append(html);
        jsfn_SetPage(_TOTAL_COUNT);     // ucPagingJS.ascx
        setTimeout("jsfn_progressBarMst('N');", 500);
    }

    function jsfn_ItemNameList() {
        var _url = '/Site/PopupPage/ItemNameListJ.aspx';
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
    function jsfn_btnPop2Exit() {
        $('#LyPop2').bPopup().close();
        $('.b-iframe').remove();
    }
    function jsfn_SetRowSelVal(req_ORDER_GROUP, req_SECTION, req_ITEM, req_ITEM_NAME, req_WH, req_WH_NAME, req_SUPPLIER, req_SUP_NAME, req_LAYER_SIZE, req_PALLET_SIZE, req_ADO_START_DATE)
    {
        
        $('#<%=selP_ORDER_GROUP.ClientID%>').val(req_ORDER_GROUP);
        $('#<%=txtP_ORDER_GROUP.ClientID%>').val(req_ORDER_GROUP);
        $('#<%=txtP_SECTION.ClientID%>').val(req_SECTION);
        

        $('#<%=txtP_ITEM.ClientID%>').val(req_ITEM);
        $('#<%=txtP_ITEM_NAME.ClientID%>').val(req_ITEM_NAME);
        
        $('#<%=txtP_WH.ClientID%>').val(req_WH);
        $('#<%=txtP_WH_NAME.ClientID%>').val(req_WH_NAME);

        $('#<%=txtP_SUPPLIER.ClientID%>').val(req_SUPPLIER);
        $('#<%=txtP_SUP_NAME.ClientID%>').val(req_SUP_NAME);

        $('#<%=txtP_LAYER_SIZE.ClientID%>').val(req_LAYER_SIZE);
        $('#<%=txtP_Pallet_SIZE.ClientID%>').val(req_PALLET_SIZE);
        
        $('#<%=txtP_ADO_START_DATE.ClientID%>').val(req_ADO_START_DATE);

        $('#LyModalPop1Layer').bPopup();
    }
    function jsfn_btnModalPopExit() {
        $('#LyModalPop1Layer').bPopup().close();
    }
</script>

<script type="text/javascript">
    function jsfn_Pop1Delete() {
        var _ITEM = $('#<%=txtP_ITEM.ClientID%>').val().replace(/-/g, '');
        var _WH = $('#<%=txtP_WH.ClientID%>').val().replace(/-/g, '');
        var _ORDER_GROUP = $('#<%=selP_ORDER_GROUP.ClientID%>').val().replace(/-/g, '');
        //var _PreORDER_GROUP = $('#<%=txtP_ORDER_GROUP.ClientID%>').val();
        var _USERID = '<%=SessionAuth.GetUserInfo(Moamam.Data.Common.UserInfoType.UserID).ToString()%>';

        if (_ITEM == '') { jsfn_alert('ITEM 정보가 잘못 되었습니다. 확인해  주세요.'); return false; }
        if (_WH == '') { jsfn_alert('WH 정보가 잘못 되었습니다. 확인해  주세요.'); return false; }
        if (_ORDER_GROUP == '') { jsfn_alert('ORDER_GROUP 정보가 잘못 되었습니다. 확인해  주세요.'); return false; }
        if (!confirm('삭제하시겠습니까?')) { return false; }

        var SpName = "SP_WEB_Site_MasterMain_ProductJ_ListPop1_D";
        var SpParams = "ITEM" + '▥' + _ITEM + '▤';
        SpParams = SpParams + "WH" + '▥' + _WH + '▤';
        SpParams = SpParams + "ORDERGROUP" + '▥' + _ORDER_GROUP + '▤';
        SpParams = SpParams + "USERID" + '▥' + _USERID;

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
        , success: function (data) { jsfn_Pop1DeleteSuccess(); }
        , error: function (x, e) { jsfn_AjaxError(x, e); }
        });
    }
    function jsfn_Pop1DeleteSuccess() {
        jsfn_alertClose('삭제 되었습니다.');
        jsfn_Search();
        $('#LyModalPop1Layer').bPopup().close();
    }

    function jsfn_Pop1Save() {
        
        var _ITEM = $('#<%=txtP_ITEM.ClientID%>').val().replace(/-/g, '');
        var _WH = $('#<%=txtP_WH.ClientID%>').val().replace(/-/g, '');
        var _ORDER_GROUP = $('#<%=selP_ORDER_GROUP.ClientID%>').val().replace(/-/g, '');
        var _PreORDER_GROUP = $('#<%=txtP_ORDER_GROUP.ClientID%>').val();
        var _ADO_START_DATE = $('#<%=txtP_ADO_START_DATE.ClientID%>').val().replace(/-/g, '');
        var _LAYERSIZE = $('#<%=txtP_LAYER_SIZE.ClientID%>').val().replace(/-/g, '');
        var _PALLETSIZE = $('#<%=txtP_Pallet_SIZE.ClientID%>').val().replace(/-/g, '');
        var _USERID = '<%=SessionAuth.GetUserInfo(Moamam.Data.Common.UserInfoType.UserID).ToString()%>';

        if (_ITEM == '') { jsfn_alert('ITEM 정보가 잘못 되었습니다. 확인해  주세요.'); return false; }
        if (_WH == '') { jsfn_alert('WH 정보가 잘못 되었습니다. 확인해  주세요.'); return false; }
        if (_ORDER_GROUP == '') { jsfn_alert('ORDER_GROUP 정보가 잘못 되었습니다. 확인해  주세요.'); return false; }

        if (!confirm('저장하시겠습니까?')) { return false; }

        var SpName = "SP_WEB_Site_MasterMain_ProductJ_ListPop1_S";
        var SpParams = "ITEM" + '▥' + _ITEM + '▤';
        SpParams = SpParams + "WH" + '▥' + _WH + '▤';
        SpParams = SpParams + "ORDERGROUP" + '▥' + _ORDER_GROUP + '▤';
        SpParams = SpParams + "PreORDER_GROUP" + '▥' + _PreORDER_GROUP + '▤';
        SpParams = SpParams + "LAYERSIZE" + '▥' + _LAYERSIZE + '▤';
        SpParams = SpParams + "PALLETSIZE" + '▥' + _PALLETSIZE + '▤';
        SpParams = SpParams + "ADO_START_DATE" + '▥' + _ADO_START_DATE + '▤';
        SpParams = SpParams + "CMDCRUD" + '▥UPD▤';
        SpParams = SpParams + "USERID" + '▥' + _USERID;

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
        jsfn_Search();
        $('#LyModalPop1Layer').bPopup().close();
    }

</script>

<script type="text/javascript">
    function jsfn_Search_Item4text() {
        var _ITEM = $('#<%=txtItem.ClientID%>').val().replace(/-/g, '');
        if (_ITEM == '') { /*jsfn_alert('ITEM 정보가 잘못 되었습니다. 확인해  주세요.');*/ return false; }
        if (_ITEM.length == 9) {
            var SpName = "SP_WEB_Common_ITEM_MASTER_1NAME_R";
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
                if (data.length == 1) {
                    $('#<%=ddlFromSectionList.ClientID%>').val(data[0].SECTION)
                    $('#<%=ddlToSectionList.ClientID%>').val(data[0].SECTION)
                    $('#<%=lblItemName.ClientID%>').text(data[0].ITEM_NAME)
                    //jsfn_Search();
                }
            }
            , error: function (x, e) { jsfn_AjaxError(x, e); }
            });
        }
        jsfn_Search_ItemView4textYN('N'); // 자동 검색어 부분도 자동으로 닫기
        return false;
    }

    function jsfn_Search_ItemView4textYN(strYN) {
        if (strYN == 'Y') {
            $("#Ly_ITEM_AppentView").css('display', 'block');
        } else {
            $("#Ly_ITEM_AppentView").css('display', 'none');
        }
    }
    function jsfn_Search_ItemView4text() {
        var _ITEM = $('#<%=txtItem.ClientID%>').val().replace(/-/g, '');
        if (_ITEM == '') { return false; }
        if (_ITEM.length >= 6) {
            var SpName = "UP_Site_Data_Sample2_List1Search2_R";
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
            , success: function (data) { fn_SetSearchItemView(data); }
            , error: function (x, e) { jsfn_AjaxError(x, e); }
            });
        }
        return false;
    }
    function fn_SetSearchItemView(data) {
        $("#Ly_ITEM_AppentView").html('');
        var HTMLstyle = '';
        if (data.length == 0) {
            jsfn_Search_ItemView4textYN('N');
            return false;
        } else {
            jsfn_Search_ItemView4textYN('Y');   // 값이 있을때만 보이자
            HTMLstyle = HTMLstyle + '<div id="lySearchItemTxtTopClose" style="text-align:right;"><a href="javascript:jsfn_Search_ItemView4textYN(\'N\');"><b>닫기</b></a></div>'
            HTMLstyle = HTMLstyle + '<div id="lySearchItemTxtTopCont" style="color:blue;">';
            for (var i = 0; i < data.length; i++) {
                HTMLstyle = HTMLstyle + "<a href=\"javascript:fn_setItemwSectionVal('" + data[i].ITEM + '|' + data[i].SECTION + "','" + data[i].ITEM_NAME + "')\"><font color=blue>" + data[i].ITEM_NAME + ' - ' + data[i].SECTION + ' - ' + data[i].ITEM + "</font></a><br/>";
            }
            HTMLstyle = HTMLstyle + "</div>";
            $("#Ly_ITEM_AppentView").html(HTMLstyle);
        }
    }

    function fn_setItemwSectionVal(data,dataName) {
        if (data.indexOf('|') > -1)
        {
            var chkArr = '';
            try {
                chkArr = data.split('|');
                
                $('#<%=ddlFromSectionList.ClientID%>').val(chkArr[1]);
                $('#<%=ddlToSectionList.ClientID%>').val(chkArr[1]);
                $('#<%=txtItem.ClientID%>').val(chkArr[0]);
                $('#<%=lblItemName.ClientID%>').text(dataName);
            } catch (e) { }
        } 
        jsfn_Search_ItemView4textYN('N');
    }
</script>

<script type="text/javascript">
    function jsfn_SerchItemClick4PopUp(Item, ItemName, section, wh) {
        $("#<%= ddlFromSectionList.ClientID %>").val(section);
        $("#<%= ddlToSectionList.ClientID %>").val(section);
        $('#<%=lblItemName.ClientID%>').text(ItemName); 
        $("#<%= txtItem.ClientID %>").val(Item);
        jsfn_btnPopExit();
    }

    // 기존 javascript 함수 
    ///////////////////////////////////////////////////////////////////////////////////////////////
    //  업로드 버튼 클릭 시 업로드 페이지 호출                                                     //
    ///////////////////////////////////////////////////////////////////////////////////////////////
    function CallUpload() {
        return showWindow(String.format("../PopupPage/ProductItemUpload.aspx?G=<%=Session["groupCd"].ToString()%>&M=<%=Session["menuCd"].ToString()%>"), 1100, 650);
        //return jsfn_UploadPop();
    }

    function jsfn_UploadPop() {
        var _url = '/Site/PopupPage/ProductItemUpload.aspx?G=<%=Session["groupCd"].ToString()%>&M=<%=Session["menuCd"].ToString()%>';
        $('#LyPopUpload1').bPopup({
            content: 'iframe',
            //contentContainer: '.contentPop',
            iframeAttr: 'scrolling="no" frameborder="0" width=920 height=570 background-color:#FFF ',
            loadUrl: _url
        });
    }
    function jsfn_UploadPopExit() {
        $('#LyPopUpload1').bPopup().close();
        $('.b-iframe').remove();
    }

</script>
<script type="text/javascript">
    //2016-12-13 협력업체 영구/임시 검색 추가 부분
    function jsfn_GetSuppName() {
        var _url = '/Site/PopupPage/SupplierNameListJ.aspx';
        $('#LyPop1').bPopup({
            content: 'iframe',
            //contentContainer: '.contentPop',
            iframeAttr: 'scrolling="no" frameborder="0" width=550 height=450 background-color:#FFF ',
            loadUrl: _url
        });
    }

    function jsfn_SupplierNameClick4PopUp(Item, ItemName) {
        $('#<%=lblSuppName.ClientID%>').text(ItemName);
        $("#<%= txtSuppCode.ClientID %>").val(Item);
        jsfn_btnPopExit();
        setTimeout('jsfn_Search();', 800);
    }
    function jsfn_Search_txtSuppCode4text() {
        var _txtSuppCode = $('#<%=txtSuppCode.ClientID%>').val().replace(/-/g, '');
        if (_txtSuppCode == '') { return false; }
        if (_txtSuppCode.length == 6) {
            var SpName = "SP_WEB_Site_PopupPage_SupplierNameListJ1_R";
            var SpParams = "PageNo" + '▥1▤';
            SpParams = SpParams + "RowCount" + '▥1▤';
            SpParams = SpParams + "ItemName" + '▥▤';
            SpParams = SpParams + "ItemCode" + '▥' + _txtSuppCode;

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
            , success: function (data) { if (data.length == 1) { $('#<%=lblSuppName.ClientID%>').text(data[0].SUP_NAME); } }
		    , error: function (x, e) { jsfn_AjaxError(x, e); }
            });
        }
        return false;
    }
    $(document).ready(function () {
        $("#<%=txtSuppCode.ClientID %>").keypress(function (event) { if (event.which == 13 && $("#<%=txtSuppCode.ClientID %>").val().length == 6) { return jsfn_Search_txtSuppCode4text(); } });
        $("#<%=txtSuppCode.ClientID %>").keyup(function (event) {
            if ((event.which >= 48 && event.which <= 57) || (event.which >= 96 && event.which <= 105) || (event.which == 8 /* backspace*/)) {
                if ($("#<%=txtSuppCode.ClientID %>").val().length == 0) { $("#<%=lblSuppName.ClientID %>").text(''); }
            }
        });
    });
</script>

<script type="text/javascript">
    function jsfn_AddItemMaster() {
        var _url = '/Site/PopupPage/ItemMasterListJ.aspx';
        $('#LyPop2').bPopup({
            content: 'iframe',
            iframeAttr: 'scrolling="no" frameborder="0" width=900 height=550 background-color:#FFF ',
            loadUrl: _url
        });
    }
</script>




</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentMain" runat="Server">
    <!-- 조회조건 -->
    <article id="search">
               
	    <div class="t_r">
            <input type="button" ID="btnSearch" class="btn btn_inquiry" value="조회" style="cursor:pointer;" />
            <hpf:NButton ID="btnExcel" runat="server" CssClass="btn btn_default" OnClick="btnExcel_Click" Text="다운로드" SecurityType="Inquiry" CmdType="EXCEL" DisabledCss="GlobalBtnNone" style="cursor:pointer;" />
            <input type="button" ID="btnUpload" Class="btn btn_default" onclick="return CallUpload();" value="업로드" style="cursor:pointer;" />

            <input type="button" ID="btnAddITEM" Class="btn btn_default" onclick="jsfn_AddItemMaster();" value="ITEM등록" style="cursor:pointer;" />
        </div>
        <div class="search row1"><!-- 한 줄: row1, 두 줄: row2, 세 줄: row3 --> 
            <div>
                <label for="">DC</label> 
                    <asp:DropDownList ID="ddlDccodeList" runat="server" Width="120" ClientIDMode="Static" ></asp:DropDownList>  
                <label for="">SECTION</label> 
                <asp:DropDownList ID="ddlFromSectionList" runat="server" Width="120" ClientIDMode="Static" />
                &nbsp;~&nbsp;
                <asp:DropDownList ID="ddlToSectionList" runat="server" Width="120" ClientIDMode="Static" />
                <label for="">ITEM</label>
                <hpf:NTextBox ID="txtItem" runat="server" CssClass="input" Style="width:75px;padding-left:5px;" MaxLength="9"  Validation="Numeric" onkeypress="if(event.keycode==13){return false;}" />&nbsp;
                <span id="lblItemName" runat="server"  ></span>
                &nbsp;&nbsp;
                <input type="button" ID="btnSearch1" class="btn btn_inquiry"  value="아이템검색"  OnClick="jsfn_ItemNameList();" style="cursor:pointer;" />
                <!-- 2016-12-13 검색 추가 부분 S -->
                <label for="">협력업체코드</label> 
                <hpf:NTextBox ID="txtSuppCode" runat="server" CssClass="input" Style="width:75px;padding-left:5px;" MaxLength="9"  Validation="Numeric" ClientIDMode="Static"/>&nbsp;
                <span id="lblSuppName" runat="server"></span>   
                &nbsp;&nbsp;
                <input type="button" ID="btnSearch2" class="btn btn_inquiry"  value="협력업체검색"  OnClick="jsfn_GetSuppName();" style="cursor:pointer;" />

                <label for="">영구/임시</label>
                <asp:DropDownList ID="ddlrudterm" runat="server" ClientIDMode="Static" Width="50">
	                <asp:ListItem Value="" Text="전체"/>
	                <asp:ListItem Value="P" Text="영구"/>
	                <asp:ListItem Value="T" Text="임시"/>
                </asp:DropDownList> 
                <!-- 2016-12-13 검색 추가 부분 E -->
                <div id="Ly_ITEM_AppentView" style="position:relative;top:0px;left:600px;width:340px;height:120px;overflow-y:scroll;z-index:999;background-color:#FFF;padding:5px 5px 5px 5px;line-height:22px;display:none;"></div>

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
			<thead style="height:24px;">
                <tr>
                    <td class="t_c" style="border:1px solid #ccc;width:80px;" rowspan="2"><b>발주그룹</b></td>
                    <td class="t_c" style="border:1px solid #ccc" rowspan="2"><b>Section</b></td>
                    <td class="t_c" style="border:1px solid #ccc" rowspan="2"><b>상품코드</b></td>
                    <td class="t_c" style="border:1px solid #ccc;width:220px;" rowspan="2"><b>상품코드명</b></td>
                    <td class="t_c" style="border:1px solid #ccc;width:100px;" rowspan="2"><b>DC<br />코드</b></td>
                    <td class="t_c" style="border:1px solid #ccc;width:220px" rowspan="2"><b>DC코드명</b></td>
                    <td class="t_c" style="border:1px solid #ccc;width:100px;" rowspan="2"><b>협력업체코드</b></td>
                    <td class="t_c" style="border:1px solid #ccc;width:220px" rowspan="2"><b>협력업체명</b></td>
                    <td class="t_c" style="border:1px solid #ccc;width:80px" rowspan="2"><b>발주<br/>시작일</b></td>
                    <td class="t_c" style="border:1px solid #ccc" rowspan="2"><b>Layer<br/>Size</b></td>
                    <td class="t_c" style="border:1px solid #ccc" rowspan="2"><b>Pallet<br/>Size</b></td>
                    <td class="t_c" style="border:1px solid #ccc" rowspan="2"><b>Case<br/>Size</b></td> 
                    <td class="t_c" colspan="8" style="border:1px solid #ccc"><b>발주</b></td> 
                    <td class="t_c" style="border:1px solid #ccc" rowspan="2"><b>Lead<br/>Time</b></td>
                    <td class="t_c" style="border:1px solid #ccc"><b>발주방법</b></td> 
                    <%--<td class="t_c" style="border:1px solid #ccc" rowspan="2"><b>SOH</b></td>
                    <td class="t_c" style="border:1px solid #ccc" rowspan="2"><b>SOO</b></td> --%>
                </tr>
                <tr > 
                    <td class="t_c" style="border:1px solid #ccc"><b>영구/임시</b></td>
                    <td class="t_c" style="border:1px solid #ccc"><b>Mon</b></td>
                    <td class="t_c" style="border:1px solid #ccc"><b>Tue</b></td>
                    <td class="t_c" style="border:1px solid #ccc"><b>Wed</b></td>
                    <td class="t_c" style="border:1px solid #ccc"><b>Thu</b></td>
                    <td class="t_c" style="border:1px solid #ccc"><b>Fri</b></td>
                    <td class="t_c" style="border:1px solid #ccc"><b>Sat</b></td>
                    <td class="t_c" style="border:1px solid #ccc"><b>Sun</b></td>
                    <td class="t_c" style="border:1px solid #ccc"><b>자동/수동</b></td>
                </tr>
			</thead>
                <tr>
                    <td colspan="22" style="width:100%;height:25px;text-align:center;">
                    </td>
                </tr>
                <%--<tr>
                    <td colspan="20" style="width:100%;height:25px;text-align:center;">
                        <b><asp:Label ID="DataExists" runat="server" ForeColor="Red" Font-Size="13px"></asp:Label></b>
                    </td>
                </tr>--%>
            </table> 
        </div> 
	</article>

    <uc1:ucPaging ID="ucPaging" runat="server" /> <%--OnSelEvent="ucPaging_SelEvent" --%>

<%--############ 모달 보여줄 페이지 S ############--%>
    <div id="LyModalPop1Layer" style="width:600px;height:300px;display:none;">
        <div class="search row1" style="background-color:#FFF;"><!-- 한 줄: row1, 두 줄: row2, 세 줄: row3 --> 
            <table style="width:580px;height:300px;" border="0">
                <colgroup>
                    <col style="width:100px;"/>
                    <col style="width:150px;" />
                    <col style="width:10px;" />
                    <col style="width:100px;" />
                    <col style="width:220px;" />
                </colgroup>
                <tr>
                    <td colspan="4" style="padding-left:5px;height:38px;">&nbsp;<b>상품 마스터 등록</b></td>
                    <td style="text-align:right;"><img ID="btnTsfCancel" style="height:20px;cursor:pointer;" src="/Images/common/grn_pp_cls_btn_over.png" OnClick="jsfn_btnModalPopExit();" /></td>
                </tr>
                <tr><td colspan="5" style="text-align:center;height:1px;padding-left:0;background-color:gray;"></td></tr>
                <tr><td colspan="5" style="text-align:center;height:12px;padding-left:0;background-color:white;"></td></tr><!-- 여백 -->
                <tr>
                    <td colspan="5">
                    <table style="text-align:left;width:100%;background-color:#cccccc;">
                        <tr style="background-color:#ffffff;border:1px solid #ccc;">
                            <th style="background-color:#efefef;height:28px;text-align:left;border:1px solid #ccc;padding-left:5px;">상품코드<//th>
                            <td style="padding:5px 0 5px 5px;"><input type="text" ID="txtP_ITEM" runat="server" readonly="readonly" style="text-align:right;padding-right:5px;"></td>
                            <td></td>
                            <th style="background-color:#efefef;height:28px;text-align:left;border:1px solid #ccc;padding-left:5px;">상품명</th>
                            <td style="padding:5px 0 5px 5px;"><input type="text" ID="txtP_ITEM_NAME" runat="server" style="width:200px;padding-left:5px;" readonly="readonly" ></td>
                        </tr>
                        <tr style="background-color:#ffffff;border:1px solid #ccc;">
                            <th style="background-color:#efefef;height:28px;text-align:left;border:1px solid #ccc;padding-left:5px;">DC 코드</th>
                            <td style="padding:5px 0 5px 5px;"><input type="text" ID="txtP_WH" runat="server" readonly="readonly" style="text-align:right;padding-right:5px;"></td>
                            <td></td>
                            <th style="background-color:#efefef;height:28px;text-align:left;border:1px solid #ccc;padding-left:5px;">DC명</th>
                            <td style="padding:5px 0 5px 5px;"><input type="text" ID="txtP_WH_NAME" runat="server" style="width:200px;padding-left:5px;" readonly="readonly"></td>
                        </tr>
                        <tr style="background-color:#ffffff;border:1px solid #ccc;">
                            <th style="background-color:#efefef;height:28px;text-align:left;border:1px solid #ccc;padding-left:5px;">협력업체코드</th>
                            <td style="padding:5px 0 5px 5px;"><input type="text" ID="txtP_SUPPLIER" runat="server" readonly="readonly" style="text-align:right;padding-right:5px;"></td>
                            <td></td>
                            <th style="background-color:#efefef;height:28px;text-align:left;border:1px solid #ccc;padding-left:5px;">협력업체명</th>
                            <td style="padding:5px 0 5px 5px;"><input type="text" ID="txtP_SUP_NAME" runat="server" style="width:200px;padding-left:5px;" readonly="readonly"></td>
                        </tr>

                        <tr style="background-color:#ffffff;border:1px solid #ccc;">
                            <th style="background-color:#efefef;height:28px;text-align:left;border:1px solid #ccc;padding-left:5px;">발주그룹</th>
                            <td style="padding:5px 0 5px 5px;"><asp:DropDownList ID="selP_ORDER_GROUP" runat="server" Width="100" ClientIDMode="Static" ></asp:DropDownList> 
                                <input type="hidden" ID="txtP_ORDER_GROUP" runat="server" >
                            </td>
                            <td></td>
                            <th style="background-color:#efefef;height:28px;text-align:left;border:1px solid #ccc;padding-left:5px;">SECTION</th>
                            <td style="padding:5px 0 5px 5px;"><input type="text" ID="txtP_SECTION" runat="server" readonly="readonly" style="text-align:right;padding-right:5px;" maxlength="1" ></td>
                        </tr>
                        <tr style="background-color:#ffffff;border:1px solid #ccc;">
                            <th style="background-color:#efefef;height:28px;text-align:left;border:1px solid #ccc;padding-left:5px;">LAYER SIZE</th>
                            <td style="padding:5px 0 5px 5px;"><hpf:NTextBox type="text" ID="txtP_LAYER_SIZE" runat="server" style="text-align:right;padding-right:5px;" Validation="Numeric" /></td>
                            <td></td>
                            <th style="background-color:#efefef;height:28px;text-align:left;border:1px solid #ccc;padding-left:5px;">PALLET SIZE</th>
                            <td style="padding:5px 0 5px 5px;"><hpf:NTextBox type="text" ID="txtP_Pallet_SIZE" runat="server" style="text-align:right;padding-right:5px;" Validation="Numeric" /></td>
                        </tr>
                        <tr style="background-color:#ffffff;border:1px solid #ccc;">
                            <th style="background-color:#efefef;height:28px;text-align:left;border:1px solid #ccc;padding-left:5px;">발주시작일</th>
                            <td style="padding:5px 0 5px 5px;" colspan="4"><hpf:NTextBox type="text" ID="txtP_ADO_START_DATE" runat="server" style="text-align:right;padding-right:5px;" Validation="Date" ReadOnly="true" /></td>
                            
                        </tr>
                        <tr><td colspan="5"></td></tr>
                    </table>
                    </td>
                </tr>
                <tr><td colspan="5" style="text-align:center;height:10px;padding-left:0;background-color:white;"></td></tr><!-- 여백 -->
                <tr><td colspan="5" style="text-align:center;height:1px;padding-left:0;background-color:gray;"></td></tr>
                <tr><td colspan="5" style="text-align:center;height:10px;padding-left:0;background-color:white;"></td></tr><!-- 여백 -->
                <tr>
                    <td colspan="5" style="text-align:center;">
                        <table style="width:100%">
                            <tr>
                                <td></td>
                                <td style="width:100px;text-align:right;"><input type="button" ID="btnPop1Save" class="btn btn_inquiry" value="저장" onclick="jsfn_Pop1Save();" style="cursor:pointer;" /></td>
                                <td style="width:20px;"></td>
                                <td style="width:100px;"><input type="button" ID="btnPop1Delete" class="btn btn_default" value="삭제" onclick="jsfn_Pop1Delete();" style="cursor:pointer;background-color:red;" /></td>
                                <td></td>
                            </tr>
                        </table>
                        
                        &nbsp;
                        <%--<input type="button" ID="btnPop1Delete" class="btn btn_default" value="삭제" onclick="jsfn_Pop1Delete();" style="cursor:pointer;" />--%>
                    </td>
                </tr>
                <tr><td colspan="5" style="text-align:center;height:10px;padding-left:0;background-color:white;"></td></tr><!-- 여백 -->
            </table>
        </div>
    </div>
<%--############ 모달 보여줄 페이지 E ############--%>

<%--### 아이템 검색 팝업 레이어 팝업용도 S ###--%>
<div id="LyPop1" class="modal_pop" style="width:550px;height:450px;padding: 2px 2px 2px 2px;display:none;background-color:#FFF !important;"></div>

<div id="LyPop2" class="modal_pop" style="width:900px;height:550px;padding: 2px 2px 2px 2px;display:none;background-color:#FFF !important;"></div>
    <%--업로드 팝업 추가--%>
<div id="LyPopUpload1" class="modal_pop" style="width:930px;height:580px;padding: 2px 2px 2px 2px;display:none;background-color:#FFF !important;"></div>
<%--### 아이템 검색 팝업 레이어 팝업용도 E ###--%>
    <!-- Excel Download 용 Excel 버튼용 트리거 S -->
    <asp:UpdatePanel ID ="UpDatePanel1" runat ="server">
        <Triggers>
        <asp:PostBackTrigger ControlID="btnExcel" />
        </Triggers>
    </asp:UpdatePanel>
    <!-- Excel Download 용 Excel 버튼용 트리거 E -->

</asp:Content>
