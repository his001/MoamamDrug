<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterPageTabSub.master" AutoEventWireup="true" CodeFile="ManualOrderJ.aspx.cs" Inherits="Site_Review_ManualOrderJ" %>
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
        //###### NButton 에 보이고 안보이고에 따른 분기 S ###### 
        if ($('#<%=btnGrid1Save.ClientID%>').css("display") == 'none') {
            $('#btn1Save').css("display", "none");
        } else {
            $('#btn1Save').click(function () { jsfn_Grid1Save('UPD'); });
        }
        if ($('#<%=btnGrid1Approval.ClientID%>').css("display") == 'none') {
            $('#btn1Approval').css("display", "none");
        } else {
            $('#btn1Approval').click(function () { jsfn_Grid1Save('APP'); });
        }
        //###### NButton 에 보이고 안보이고에 따른 분기 E ###### 
        $("#<%=txtItem.ClientID %>").keypress(function (event) { if (event.which == 13 && $("#<%=txtItem.ClientID %>").val().length == 9) { return jsfn_Search_Item4text(); } });
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
    function jsfn_CheckedAll() {
        var _chkAll = $('#rpt1_chkBox_All').prop('checked');
        $('input[type=checkbox][name="rpt1_chkBox"]').each(function () {           //id*="eachCheck"
            if ($(this).prop('disabled')) {
            } else {
                $(this).prop("checked", _chkAll);
            }
        });
    }
    function jsfn_CheckBoxBind() {
        $("input[name=grid1_AOQ_QTY]").on('change', function () {
            var index = $("input[name=grid1_AOQ_QTY]").index(this);
            $("input[name=rpt1_chkBox]:eq(" + index + ")").prop("checked", "checked");
        });
    }
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

        var _cbRoq0 = '';
        if ($("#<%=cbRoq0.ClientID %>").prop('checked')) { _cbRoq0 = 'Y'; } else { _cbRoq0 = 'N'; };

        
        jsfn_progressBarMst('Y');

        var _ucPageNo = $("#LyCus_hidPageNo").val();
        if (_ucPageNo == '') { _ucPageNo = '1'; }
        var _ucRowCount = $("#LyCus_hidRowCnt").val();
        if (_ucRowCount == '') { _ucRowCount = '30'; }

        var SpName = "SP_WEB_Site_Review_ManualOrderJ_R";
        var SpParams = "SECTIONFROM" + '▥' + _ddlFromSectionList + '▤';
        SpParams = SpParams + "SECTIONTO" + '▥' + _ddlToSectionList + '▤';
        SpParams = SpParams + "ITEM" + '▥' + _Item + '▤';
        SpParams = SpParams + "ROWCNT" + '▥' + _ucRowCount + '▤';
        SpParams = SpParams + "PAGENUM" + '▥' + _ucPageNo + '▤';
        SpParams = SpParams + "SMODE" + '▥' + 'SELECT' + '▤';
        SpParams = SpParams + "SUPPLIER" + '▥' + $('#<%=txtSuppCode.ClientID%>').val().replace(/\'/g, '') + '▤';
        SpParams = SpParams + "ROQ_YN" + '▥' + _cbRoq0;


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
        if (gData.length == 0) {
            jsfn_SetPage(_TOTAL_COUNT); // ucPagingJS.ascx
            var noData_html = '<tr><td colspan="24" style="width:100%;height:25px;text-align:center;"><b><font color="red">조회된 결과가 없습니다.</font></b></td></tr>';
            $('#dvGrid1 > tbody:last').append(noData_html);
            jsfn_progressBarMst('N');
            return false;
        } else { _TOTAL_COUNT = gData[0].TOTAL_COUNT; }
        for (var i = 0; i < gData.length; i++) {
            if (gData[i].ORDER_NO == null) { gData[i].ORDER_NO = ''; }
            if (gData[i].RECEIPT_DATE == null) { gData[i].RECEIPT_DATE = ''; }
            html = html + '<tr class="tr_c">';
            html = html + '<td class="t_c" ><input type="checkbox" id="rpt1_chkBox_' + i + '" name="rpt1_chkBox">';
            html = html + '<td class="t_c" style="text-align:center;width:50px;">' + gData[i].ORDER_NO + '</td>';
            html = html + '<td class="t_c">' + gData[i].ORDER_DATE + '</td>';
            html = html + '<td class="t_c">' + gData[i].DEPT + '</td>';
            html = html + '<td class="t_c">' + gData[i].SECTION + '</td>';
            html = html + '<td class="t_c">' + gData[i].SOURCE_WH + '</td>';
            html = html + '<td class="t_c" style="text-align:left;padding-right:5px;overflow:hidden;text-overflow:ellipsis;white-space:nowrap;">' + gData[i].WH_NAME + '</td>';
            html = html + '<td class="t_c">' + gData[i].SUPPLIER + '</td>';
            html = html + '<td class="t_c" style="text-align:left;padding-right:5px;overflow:hidden;text-overflow:ellipsis;white-space:nowrap;">' + gData[i].SUP_NAME + '</td>';
            html = html + '<td class="t_c">' + gData[i].TPND_ITEM + '</td>';
            html = html + '<td class="t_c" style="text-align:left;padding-right:5px;width:250px;overflow:hidden;text-overflow:ellipsis;white-space:nowrap;">' + gData[i].TPND_ITEM_NAME + '</td>';
            html = html + '<td class="t_c">' + gData[i].ITEM + '</td>';
            html = html + '<td class="t_c" style="text-align:left;padding-right:5px;overflow:hidden;text-overflow:ellipsis;white-space:nowrap;">' + gData[i].ITEM_NAME + '</td>';
            html = html + '<td class="t_c" style="text-align:right;padding-right:5px;">' + gData[i].SALES_FORECAST.toLocaleString() + '</td>';
            html = html + '<td class="t_c" style="text-align:right;padding-right:5px;">' + gData[i].ROQ_QTY + '</td>';
            html = html + '<td class="t_c"><input type="text" id="grid1_AOQ_QTY" name="grid1_AOQ_QTY" value="' + gData[i].AOQ_QTY + '" style="width:60px;text-align:right;padding-right:5px;"></td>';
            html = html + '<td class="t_c" style="text-align:right;padding-right:5px;">' + gData[i].DC_SOH.toLocaleString() + '</td>';
            html = html + '<td class="t_c" style="text-align:right;padding-right:5px;">' + gData[i].DC_SOO + '</td>';
            html = html + '<td class="t_c" style="text-align:right;padding-right:5px;">' + gData[i].CASE_SIZE + '</td>';

            html = html + '<td class="t_c" style="text-align:right;padding-right:5px;">' + gData[i].LEAD_TIME + '</td>';
            html = html + '<td class="t_c" style="text-align:right;padding-right:5px;">' + gData[i].SFS_QTY + '</td>';
            html = html + '<td class="t_c">' + gData[i].RUD_ID + '</td>';
            html = html + '<td class="t_c">' + gData[i].RECEIPT_DATE + '</td>';
            html = html + '<td class="t_c">' + gData[i].STATUS_HAN + '';
            html = html + '<input type="text" id="grid1_hdn_STATUS" name="grid1_hdn_STATUS" value="' + gData[i].STATUS + '" style="display:none;">';
            html = html + '<input type="text" id="grid1_hdn_ITEM" name="grid1_hdn_ITEM" value="' + gData[i].ITEM + '" style="display:none;">';
            html = html + '<input type="text" id="grid1_hdn_SOURCE_WH" name="grid1_hdn_SOURCE_WH" value="' + gData[i].SOURCE_WH + '" style="display:none;">';
            html = html + '<input type="text" id="grid1_hdn_ORDER_DATE" name="grid1_hdn_ORDER_DATE" value="' + gData[i].ORDER_DATE + '" style="display:none;">';
            html = html + '</td>';
            html = html + '</tr>';
        }
        $('#dvGrid1 > tbody:last').append(html);
        jsfn_SetPage(_TOTAL_COUNT);     // ucPagingJS.ascx
        setTimeout("jsfn_progressBarMst('N');", 500);
        setTimeout("jsfn_CheckBoxBind();", 1000);
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

    function fn_setItemwSectionVal(data, dataName) {
        if (data.indexOf('|') > -1) {
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
            , success: function (data) { if (data.length == 1) {$('#<%=lblSuppName.ClientID%>').text(data[0].SUP_NAME)} }
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

<%-- #### 체크박스 저장 #### --%>
<script type="text/javascript">
    function jsfn_Grid1Save(strApp) {
        var _chkCt = 0;
        var _jsuccessCnt = 0;
        var _grid1_ITEM = '', _grid1_SOURCE_WH = '', _grid1_ORDER_DATE = '', _grid1_AOQ_QTY = '', index = 0;

        $('input[type=checkbox][name="rpt1_chkBox"]').each(function () {
            if ($(this).prop("checked")) { _chkCt++; }
        });
        var _cfgMsg = '';
        var _cfgMsg2 = '';
        if (strApp == 'UPD') { _cfgMsg = '건을 수정 하시겠습니까?'; _cfgMsg2 = ' 수정 되었습니다.'; } else { _cfgMsg = '건을 승인 하시겠습니까?'; _cfgMsg2 = ' 승인 되었습니다.'; }
        if (!confirm(_chkCt + '' + _cfgMsg)) { return false; }
        $('input[type=checkbox][name="rpt1_chkBox"]').each(function () {
            if ($(this).prop("checked")) {
                _grid1_ITEM         = $("input[name=grid1_hdn_ITEM]:eq(" + index + ")").val();
                _grid1_SOURCE_WH    = $("input[name=grid1_hdn_SOURCE_WH]:eq(" + index + ")").val();
                _grid1_ORDER_DATE   = $("input[name=grid1_hdn_ORDER_DATE]:eq(" + index + ")").val();
                _grid1_AOQ_QTY      = $("input[name=grid1_AOQ_QTY]:eq(" + index + ")").val();
                //jsfn_alert(index + ' : index -> ' + _grid1_ITEM);
                _jsuccessCnt = _jsuccessCnt + jsfn_Master1Save(_grid1_ITEM, _grid1_SOURCE_WH, _grid1_ORDER_DATE, _grid1_AOQ_QTY, strApp);
            }
            index++;
        });
        jsfn_alert('총 ' + _chkCt + '건 중 ' + _jsuccessCnt + '' + _cfgMsg2);
        jsfn_Search();
    }

    function jsfn_Master1Save(_item, _source_wh, _order_date, _aoq_qty, _strApp) {
        // _strApp  'UPD' ,  'APP'
        var _successCnt = 0;
        var _USERID = '<%=SessionAuth.GetUserInfo(Moamam.Data.Common.UserInfoType.UserID).ToString()%>';
        if (_item == '') { return false; }
        if (_source_wh == '') { return false; }
        if (_order_date == '') { return false; }
        if (_aoq_qty == '') { return false; }
        
        var SpName = "SP_WEB_Site_Review_ManualOrderJ_S";
        var SpParams = "ITEM" + '▥' + _item + '▤';
        SpParams = SpParams + "SOURCE_WH" + '▥' + _source_wh + '▤';
        SpParams = SpParams + "ORDER_DATE" + '▥' + _order_date + '▤';
        SpParams = SpParams + "AOQ_QTY" + '▥' + _aoq_qty + '▤';
        SpParams = SpParams + "USERID" + '▥' + _USERID + '▤';
        SpParams = SpParams + "MODE" + '▥' + _strApp;

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
        , success: function (data) { _successCnt = 1; }
        , error: function (x, e) { jsfn_AjaxError(x, e); }
        });
        return _successCnt;
    }
</script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentMain" runat="Server">
    <!-- 조회조건 -->
    <article id="search">
               
	    <div class="t_r">
            <input type="button" ID="btnSearch" class="btn btn_inquiry" value="조회" style="cursor:pointer;" />
            
            <input type="button" ID="btn1Save" class="btn btn_default" value="수정" style="cursor:pointer;" />
            <input type="button" ID="btn1Approval" class="btn btn_default" value="승인" style="cursor:pointer;" />
            <div id="ltBtnHdn" style="display:none;">
                <hpf:NButton ID="btnGrid1Save" runat="server" CssClass="btn btn_default" Text="수정" SecurityType="Save" CmdType="UPDATE" DisabledCss="GlobalBtnNone" style="cursor:pointer;" Enabled="False" />
                <hpf:NButton ID="btnGrid1Approval" runat="server" CssClass="btn btn_default" Text="승인" SecurityType="Approval" CmdType="UPDATE" DisabledCss="GlobalBtnNone" style="cursor:pointer;" Enabled="False" />
            </div>
            <hpf:NButton ID="btnExcel" runat="server" CssClass="btn btn_default" OnClick="btnExcel_Click" Text="다운로드" SecurityType="Inquiry" CmdType="EXCEL" DisabledCss="GlobalBtnNone" style="cursor:pointer;"/>
                
        </div>
        <div class="search row1"><!-- 한 줄: row1, 두 줄: row2, 세 줄: row3 --> 
            <div>
                <label for="">SECTION</label> 
                <asp:DropDownList ID="ddlFromSectionList" runat="server" Width="150" ClientIDMode="Static" />
                &nbsp;~&nbsp;
                <asp:DropDownList ID="ddlToSectionList" runat="server" Width="150" ClientIDMode="Static" />
                <label for="">ITEM</label>
                <hpf:NTextBox ID="txtItem" runat="server" CssClass="input" Style="width:75px;padding-left:5px;" MaxLength="9"  Validation="Numeric" onkeypress="if(event.keycode==13){return false;}" />&nbsp;
                <span id="lblItemName" runat="server"  >&nbsp;</span>
                &nbsp;&nbsp;
                <input type="button" ID="btnSearch1" class="btn btn_inquiry"  value="아이템검색"  OnClick="jsfn_ItemNameList();" style="cursor:pointer;" />
                <!-- 2016-12-13 검색 추가 부분 S -->
                <label for="">협력업체코드</label> 
                <hpf:NTextBox ID="txtSuppCode" runat="server" CssClass="input" Style="width:75px;padding-left:5px;" MaxLength="9"  Validation="Numeric" ClientIDMode="Static"/>&nbsp;
                <span id="lblSuppName" runat="server"></span>   
                &nbsp;&nbsp;
                <input type="button" ID="btnSearch2" class="btn btn_inquiry"  value="협력업체검색"  OnClick="jsfn_GetSuppName();" style="cursor:pointer;" />
                <!-- 2016-12-13 검색 추가 부분 E -->
                
                &nbsp;&nbsp;
                <label for="cbRoq0" style="cursor:pointer;">ROQ 0수량 제외</label> 
                <asp:CheckBox runat="server" ID="cbRoq0" ClientIDMode="Static" Text=""/>
                
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
			    <thead> 
                <tr class="tr_h31" style="height:35px;">
                    <td style="border:1px solid #ccc" class="t_c"><input id="rpt1_chkBox_All" name="rpt1_chkBox_All" type="checkbox" onclick="jsfn_CheckedAll();"/></td>
                    <td style="border:1px solid #ccc;font-size:11px;width:60px;" class="t_c"><b>발주번호</b></td>
                    <td style="border:1px solid #ccc;font-size:11px;width:130px;" class="t_c"><b>발주일</b></td>
                    <td style="border:1px solid #ccc;font-size:11px;" class="t_c"><b>Dept</b></td>
                    <td style="border:1px solid #ccc;font-size:11px;" class="t_c"><b>Section</b></td>
                    <td style="border:1px solid #ccc;font-size:11px;width:100px;" class="t_c"><b>DC코드</b></td>
                    <td style="border:1px solid #ccc;font-size:11px;" class="t_c"><b>DC명</b></td>
                    <td style="border:1px solid #ccc;font-size:11px;width:120px;" class="t_c"><b>협력업체<br />코드</b></td>
                    <td style="border:1px solid #ccc;font-size:11px;" class="t_c"><b>협력업체이름</b></td>
                    <td style="border:1px solid #ccc;font-size:11px;" class="t_c"><b>TPND</b></td> 
                    <td style="border:1px solid #ccc;font-size:11px;" class="t_c"><b>TPND명</b></td> 
                    <td style="border:1px solid #ccc;font-size:11px;" class="t_c"><b>상품코드</b></td>
                    <td style="border:1px solid #ccc;font-size:11px;" class="t_c"><b>상품명</b></td>
                    <td style="border:1px solid #ccc;font-size:11px;" class="t_c"><b>Sale<br/>Forecast</b></td>
                    <td style="border:1px solid #ccc;font-size:11px;" class="t_c"><b>ROQ</b></td> 
                    <td style="border:1px solid #ccc;font-size:11px;" class="t_c"><b>AOQ</b></td> 
                    <td style="border:1px solid #ccc;font-size:11px;" class="t_c"><b>DC<br/>SOH</b></td> 
                    <td style="border:1px solid #ccc;font-size:11px;" class="t_c"><b>DC<br/>OnOrder</b></td> 
                    <td style="border:1px solid #ccc;font-size:11px;" class="t_c"><b>Case<br/>Size</b></td> 
                    <td style="border:1px solid #ccc;font-size:11px;" class="t_c"><b>LT</b></td> 
                    <td style="border:1px solid #ccc;font-size:11px;" class="t_c"><b>Safety<br/>Stock</b></td> 
                    <td style="border:1px solid #ccc;font-size:11px;" class="t_c"><b>Roun<br/>ding</b></td> 
                    <td style="border:1px solid #ccc;font-size:11px;width:120px;" class="t_c"><b>입고일</b></td>
                    <td style="border:1px solid #ccc;font-size:11px;" class="t_c"><b>STATUS</b></td>
                </tr>
                </thead>
                <tr>
                    <td colspan="25" style="width:100%;height:25px;text-align:center;">
                    </td>
                </tr>
            </table> 
        </div> 
	</article>

    <uc1:ucPaging ID="ucPaging" runat="server" />


<%--### 아이템 검색 팝업 레이어 팝업용도 S ###--%>
<div id="LyPop1" class="modal_pop" style="width:550px;height:450px;padding: 2px 2px 2px 2px;display:none;background-color:#FFF !important;">
</div>
<%--### 아이템 검색 팝업 레이어 팝업용도 E ###--%>
    <!-- Excel Download 용 Excel 버튼용 트리거 S -->
    <asp:UpdatePanel ID ="UpDatePanel1" runat ="server">
        <Triggers><asp:PostBackTrigger ControlID="btnExcel" /></Triggers>
    </asp:UpdatePanel>
    <!-- Excel Download 용 Excel 버튼용 트리거 E -->


</asp:Content>
