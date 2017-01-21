<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterPage.master" AutoEventWireup="true" CodeFile="Sample3.aspx.cs" Inherits="Site_Data_Sample3" %>
<%@ Register Src="~/UserControls/ucPagingJS.ascx" TagName="ucPaging" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHead" runat="Server">
    
<!-- 1.LoadPage -->
<script type="text/javascript">
    var __clickCnt = 0;
    $(document).ready(function () {
        Initialize();
        jsfn_Search();
        jsfn_CheckBoxBind();// 바인딩이 되면 체크박스 체크를 바인딩 한다.
    });
</script>
<!-- 2.Init -->
<script type="text/javascript">
    function Initialize() {
        $('#divProgress4js').css("display", "none");
        jsfn_setScreenHeight();
        $('#btnSearch').click(function () { jsfn_Search(); });
        $('#btnGrid1Save').click(function () { jsfn_Grid1Save(); });
        $("#<%=txtItem.ClientID %>").keypress(function (event) {if (event.which == 13 && $("#<%=txtItem.ClientID %>").val().length==9) {return jsfn_Search_Item4text();}});
        $("#<%=txtItem.ClientID %>").keyup(function (event) {
            if ((event.which >= 48 && event.which <= 57) || (event.which >= 96 && event.which <= 105) || (event.which == 8 /* backspace*/) || (event.which == 46 /* del*/) || (event.which == 32 /* space*/)) {
                if ($("#<%=txtItem.ClientID %>").val().length == 0) { $('#<%=lblItemName.ClientID%>').text('');jsfn_Search_ItemView4textYN('N'); } // 
                if ($("#<%=txtItem.ClientID %>").val().length >= 6 && $("#<%=txtItem.ClientID %>").val().length <= 8) { jsfn_Search_ItemView4text(); }
            }
        });
    }
    function jsfn_setScreenHeight() {
        var _topMenu = "200";
        var _SearchArea = "100";
        var _ContHeader = "30";
        var _Show_contHeight = window.screen.height - parseInt(_topMenu) - parseInt(_SearchArea) - parseInt(_ContHeader);
        if (parseInt(_Show_contHeight) > 500) {
            $(".content_aria").css('height', _Show_contHeight + 'px');
        }
    }
</script>
<!-- 3.Validation  -->
<script type="text/javascript">
    function jsfn_CheckBoxBind()
    {
        $("input[name=grid1_ORDER_GROUP]").on('change', function () {
            var index = $("input[name=grid1_ORDER_GROUP]").index(this);
            $("input[name=rpt1_chkBox]:eq(" + index + ")").prop("checked", "checked");
        });

        $("input[name=grid1_REPL_METHOD]").on('change', function () {
            var index = $("input[name=grid1_REPL_METHOD]").index(this);
            $("input[name=rpt1_chkBox]:eq(" + index + ")").prop("checked", "checked");
        });
    }

    function jsfn_Grid1Save() {
        var _chkCt = 0;
        var _jsuccessCnt = 0;
        var _grid1_ITEM = '', _grid1_WH = '', _grid1_SUPPLIER = '', _grid1_REPL_METHOD = '', _grid1_ORDER_GROUP = '',index = 0;

        $('input[type=checkbox][name="rpt1_chkBox"]').each(function () {
            if ($(this).prop("checked")) { _chkCt++; }
        });
        if (!confirm(_chkCt + '건을 저장하시겠습니까?')) { return false; }
        $('input[type=checkbox][name="rpt1_chkBox"]').each(function () {
            if ($(this).prop("checked")) {
                _grid1_ITEM = $("input[name=grid1_hdn_ITEM]:eq(" + index + ")").val();
                _grid1_WH = $("input[name=grid1_hdn_WH]:eq(" + index + ")").val();
                _grid1_SUPPLIER = $("input[name=grid1_hdn_SUPPLIER]:eq(" + index + ")").val();
                _grid1_REPL_METHOD = $("input[name=grid1_REPL_METHOD]:eq(" + index + ")").val();
                _grid1_ORDER_GROUP = $("input[name=grid1_ORDER_GROUP]:eq(" + index + ")").val();
                //alert(index + ' : index -> ' + _grid1_ITEM);
                _jsuccessCnt = _jsuccessCnt + jsfn_Master1Save(_grid1_ITEM, _grid1_WH, _grid1_REPL_METHOD, _grid1_ORDER_GROUP);
            }
            index++;
        });
        alert('총 ' + _chkCt + '건 중 ' + _jsuccessCnt + '건 수정 되었습니다.');
        jsfn_Search();
    }

    function jsfn_Master1Save(_item, _wh, _replMeth, _orderGp) {
        var _successCnt = 0;
        var _USERID = '<%=SessionAuth.GetUserInfo(Moamam.Data.Common.UserInfoType.UserID).ToString()%>';
        if (_item == '') { return false; }
        if (_wh == '') { return false; }
        var SpName = "UP_Site_Data_Sample3_Grid1_S";
        var SpParams = "ITEM" + '▥' + _item + '▤';
        SpParams = SpParams + "WH" + '▥' + _wh + '▤';
        SpParams = SpParams + "REPL_METHOD" + '▥' + _replMeth + '▤';
        SpParams = SpParams + "ORDER_GROUP" + '▥' + _orderGp + '▤';
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
        , success: function (data) { _successCnt = 1; }
        , error: function (x, e) { jsfn_AjaxError(x, e); }
        });
        return _successCnt;
    }



</script>
<!-- 4. Custom Function  -->
<script type="text/javascript">

    function jsfn_Search() {
        var _ddlFromSectionList = $("#<%= ddlFromSectionList.ClientID %>").val().replace(/-/g, '');
        if (_ddlFromSectionList == '') { alert('시작 섹션을 선택해 주세요.'); return false; }
        var _ddlToSectionList = $("#<%= ddlToSectionList.ClientID %>").val().replace(/-/g, '');
        if (_ddlToSectionList == '') { alert('종료 섹션을 선택해 주세요.'); return false; }
        var _Item = $("#<%= txtItem.ClientID %>").val().replace(/-/g, '');

        if (parseInt(_ddlFromSectionList) > parseInt(_ddlToSectionList)) {
            alert("SECTION값이 FROM보다 TO가 작습니다.\n다시 선택하여 조회하시길 바랍니다.");
            return false;
        }

        jsfn_progressBarMst('Y');

        var _ucPageNo = $("#LyCus_hidPageNo").val();
        if (_ucPageNo == '') { _ucPageNo = '1'; }
        var _ucRowCount = $("#LyCus_hidRowCnt").val();
        if (_ucRowCount == '') { _ucRowCount = '30'; }

        var SpName = "UP_Site_Data_Sample2_List1_R";
        var SpParams = "PageNo" + '▥' + _ucPageNo + '▤';
        SpParams = SpParams + "RowCount" + '▥' + _ucRowCount + '▤';
        SpParams = SpParams + "ITEM" + '▥' + _Item + '▤';
        SpParams = SpParams + "SECTIONFROM" + '▥' + _ddlFromSectionList + '▤';
        SpParams = SpParams + "SECTIONTO" + '▥' + _ddlToSectionList;

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
        , success: function (data) { fn_SetGrid(data); }
        , error: function (x, e) { jsfn_AjaxError(x, e); jsfn_progressBarMst('N'); }
        });

    }

    function fn_SetGrid(dataRes) {
        $("#dvGrid1 > tbody > tr").remove();
        var _TOTAL_COUNT = 0;
        var gData = dataRes;
        var html = '';
        if (gData.length == 0) {
            jsfn_SetPage(_TOTAL_COUNT); // ucPagingJS.ascx
            alert('조회된 결과가 없습니다.');
            jsfn_progressBarMst('N');
            return false;
        } else { _TOTAL_COUNT = gData[0].TOTAL_COUNT; }
        var _replace_val1 = '';
        var _replace_val2 = '';
        for (var i = 0; i < gData.length; i++) {
            if (gData[i].ORDER_GROUP != null) { _replace_val1 = gData[i].ORDER_GROUP; } else { _replace_val1 = ''; }    // DB 에서 null 로 넘어 올때
            if (gData[i].REPL_METHOD != null) { _replace_val2 = gData[i].REPL_METHOD; } else { _replace_val2 = ''; }     // DB 에서 null 로 넘어 올때

            html = html + '<tr class="tr_c">';
            html = html + '<td class="t_c" ><input type="checkbox" id="rpt1_chkBox_' + i + '" name="rpt1_chkBox">';
            // #### 저장을 위한 히든 text 추가 S
            html = html + '<input type="hidden" id="grid1_ITEM" name="grid1_hdn_ITEM" value="' + gData[i].ITEM + '">';
            html = html + '<input type="hidden" id="grid1_WH" name="grid1_hdn_WH" value="' + gData[i].WH + '">';
            html = html + '<input type="hidden" id="grid1_SUPPLIER" name="grid1_hdn_SUPPLIER" value="' + gData[i].SUPPLIER + '">';
            // #### 저장을 위한 히든 text 추가 E
            html = html + '</td>';

            html = html + '<td class="t_c" style="text-align:left;">' + gData[i].ITEM_NAME + '</td>';
            html = html + '<td class="t_c" > <a href="javascript:jsfn_SetRowSelVal(\'' + gData[i].ITEM + '\',\'' + gData[i].WH + '\',\'' + gData[i].SUPPLIER + '\',\'' + gData[i].SUP_NAME + '\',\'' + gData[i].REPL_METHOD
                + '\',\'' + gData[i].ORDER_GROUP + '\');"><font color="blue">' + gData[i].ITEM + '</fontr></a></td> ';
            html = html + '<td class="t_c">' + gData[i].WH + '</td>';
            html = html + '<td class="t_c">' + gData[i].SUPPLIER + '</td>';
            html = html + '<td class="t_c" style="text-align:left;width:120px;overflow:hidden;text-overflow:ellipsis;white-space:nowrap;">' + gData[i].SUP_NAME + '</td>';
            html = html + '<td class="t_c"><input type="text" id="grid1_ORDER_GROUP" name="grid1_ORDER_GROUP" value="' + _replace_val1 + '" style="width:80px"></td>';
            html = html + '<td class="t_c"><input type="text" id="grid1_REPL_METHOD" name="grid1_REPL_METHOD" value="' + _replace_val2 + '" style="width:80px"></td>';

            html = html + '</tr>';
        }
        $('#dvGrid1 > tbody:last').append(html);
        jsfn_SetPage(_TOTAL_COUNT);     // ucPagingJS.ascx
        setTimeout("jsfn_progressBarMst('N');", 500);

        //jsfn_CheckBoxBind();// 바인딩이 되면 체크박스 체크를 바인딩 한다.
    }

    function jsfn_ItemNameList() {
        var _url = '/Site/Data/SamplePop.aspx';
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

    function jsfn_SetRowSelVal(req_ITEM, req_WH, req_SUPPLIER, req_SUP_NAME, req_REPL_METHOD, req_ORDER_GROUP)
    {
        $('#<%=lblstrITEM.ClientID%>').val(req_ITEM);
        $('#<%=lblstrWH.ClientID%>').val(req_WH);
        $('#<%=lblstrSUPPLIER.ClientID%>').val(req_SUPPLIER);
        $('#<%=lblstrSUP_NAME.ClientID%>').val(req_SUP_NAME);
        $('#<%=lblstrORDER_GROUP.ClientID%>').val(req_ORDER_GROUP);
        $('#<%=ddlstrREPL_METHOD.ClientID%>').val(req_REPL_METHOD);
        $('#LyModalPop1Layer').bPopup();
    }
    function jsfn_btnModalPopExit() {
        $('#LyModalPop1Layer').bPopup().close();
    }
</script>

<script type="text/javascript">
    function jsfn_Pop1Save() {

        var _ITEM = $('#<%=lblstrITEM.ClientID%>').val().replace(/-/g, '');
        var _WH = $('#<%=lblstrWH.ClientID%>').val().replace(/-/g, '');
        var _SUPPLIER = $('#<%=lblstrSUPPLIER.ClientID%>').val().replace(/-/g, '');
        var _SUP_NAME = $('#<%=lblstrSUP_NAME.ClientID%>').val().replace(/-/g, '');
        var _REPL_METHOD = $('#<%=ddlstrREPL_METHOD.ClientID%>').val().replace(/-/g, '');
        var _ORDER_GROUP = $('#<%=lblstrORDER_GROUP.ClientID%>').val().replace(/-/g, '');
        var _USERID = '<%=SessionAuth.GetUserInfo(Moamam.Data.Common.UserInfoType.UserID).ToString()%>';
        if (_ITEM == '') { alert('ITEM 정보가 잘못 되었습니다. 확인해  주세요.'); return false; }
        if (_WH == '') { alert('WH 정보가 잘못 되었습니다. 확인해  주세요.'); return false; }
        var SpName = "UP_Site_Data_Sample2_Pop1_S";
        var SpParams = "ITEM" + '▥' + _ITEM + '▤';
        SpParams = SpParams + "WH" + '▥' + _WH + '▤';
        SpParams = SpParams + "REPL_METHOD" + '▥' + _REPL_METHOD + '▤';
        SpParams = SpParams + "CMDCRUD" + '▥U▤';
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

    function jsfn_Pop1Delete() {
        var _ITEM = $('#<%=lblstrITEM.ClientID%>').val().replace(/-/g, '');
        var _WH = $('#<%=lblstrWH.ClientID%>').val().replace(/-/g, '');
        var _SUPPLIER = $('#<%=lblstrSUPPLIER.ClientID%>').val().replace(/-/g, '');
        var _SUP_NAME = $('#<%=lblstrSUP_NAME.ClientID%>').val().replace(/-/g, '');
        var _REPL_METHOD = $('#<%=ddlstrREPL_METHOD.ClientID%>').val().replace(/-/g, '');
        var _ORDER_GROUP = $('#<%=lblstrORDER_GROUP.ClientID%>').val().replace(/-/g, '');
        var _USERID = '<%=SessionAuth.GetUserInfo(Moamam.Data.Common.UserInfoType.UserID).ToString()%>';
        if (_ITEM == '') { alert('ITEM 정보가 잘못 되었습니다. 확인해  주세요.'); return false; }
        if (_WH == '') { alert('WH 정보가 잘못 되었습니다. 확인해  주세요.'); return false; }

        var SpName = "UP_Site_Data_Sample2_Pop1_S";
        var SpParams = "ITEM" + '▥' + _ITEM + '▤';
        SpParams = SpParams + "WH" + '▥' + _WH + '▤';
        SpParams = SpParams + "REPL_METHOD" + '▥' + _REPL_METHOD + '▤';
        SpParams = SpParams + "CMDCRUD" + '▥D▤';
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
        alert('삭제 되었습니다.');
        jsfn_Search();
        $('#LyModalPop1Layer').bPopup().close();
    }
</script>

<script type="text/javascript">
    function jsfn_Search_Item4text() {
        var _ITEM = $('#<%=txtItem.ClientID%>').val().replace(/-/g, '');
        if (_ITEM == '') { /*alert('ITEM 정보가 잘못 되었습니다. 확인해  주세요.');*/ return false; }
        if (_ITEM.length == 9) {
            var SpName = "UP_Site_Data_Sample2_List1Search1_R";
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
        return showWindow(String.format("../PopupPage/OrderMethodUpload.aspx?G=<%=Session["groupCd"].ToString()%>&M=<%=Session["menuCd"].ToString()%>"), 1100, 650);
    }

    function jsfn_CheckedAll() {
        var _chkAll = $('#rpt1_chkBox_All').prop('checked');
        $('input[type=checkbox][name="rpt1_chkBox"]').each(function () {           //id*="eachCheck"
            if ($(this).prop('disabled')) {
            } else {
                $(this).prop("checked", _chkAll);
            }
        });
    }

</script>


</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentMain" runat="Server">
    <!-- 조회조건 -->
    <article id="search">
               
	    <div class="t_r">
            <input type="button" ID="btnSearch" class="btn btn_inquiry" value="조회" style="cursor:pointer;" />
            <input type="button" ID="btnGrid1Save" class="btn btn_default" value="저장" style="cursor:pointer;" />
            <hpf:NButton ID="btnExcel" runat="server" CssClass="btn btn_default" OnClick="btnExcel_Click" Text="다운로드" SecurityType="Inquiry" CmdType="EXCEL" DisabledCss="GlobalBtnNone"/>
    
            <%--<input type="button" ID="btnUpload" Class="btn btn_default" onclick="return CallUpload();" value="업로드" />--%>
        </div>
        <div class="search row1"><!-- 한 줄: row1, 두 줄: row2, 세 줄: row3 --> 
            <div>
                <label for="">SECTION</label> 
                <asp:DropDownList ID="ddlFromSectionList" runat="server" Width="260" ClientIDMode="Static" />
                &nbsp;~&nbsp;
                <asp:DropDownList ID="ddlToSectionList" runat="server" Width="260" ClientIDMode="Static" />
                <label for="">ITEM</label>
                <hpf:NTextBox ID="txtItem" runat="server" CssClass="input" Style="width:75px" MaxLength="9"  Validation="Numeric" />&nbsp;&nbsp; <%--ClientIDMode="Static" --%>
                <span id="lblItemName" runat="server"  >&nbsp;</span>

                &nbsp;&nbsp;
                <input type="button" ID="btnSearch1" class="btn btn_inquiry"  value="아이템검색"  OnClick="jsfn_ItemNameList();" style="cursor:pointer;" />
                <%-- 아이템 검색시 하단에 레이어 추가 되도록 --%>
                <div id="Ly_ITEM_AppentView" style="position:relative;top:0px;left:690px;width:340px;height:120px;overflow-y:scroll;z-index:999;background-color:#FFF;padding:5px 5px 5px 5px;line-height:22px;display:none;"> 
                </div>

            </div>
	    </div> 

    </article>
    <!-- 조회조건 End -->

    <br />
    
    <!-- List Start -->
	<article id="grid">
        <div id="Ly_grid1Scroll" style="width:100%;text-align:center;height:580px;min-width:1024px;overflow-x:auto;overflow-y:auto;">
		    <table id="dvGrid1">
                <colgroup>
                    <col style="width:80px;" /><%--체크박스--%>
                    <col style="width:300px;"/>
                    <col style="width:100px;" /><%--상품코드--%>
                    <col style="width:100px;" /><%--DC코드--%>
                    <col style="width:100px;" /><%--협력업체코드--%>
                    <col style="width:300px;" /><%--협력업체이름--%>
                    <col style="width:100px;" /><%--발주그룹--%> 
                    <col style="width:100px;" /><%--발주방법--%>
                </colgroup>
			    <thead> 
                    <tr style="height:32px;">
                        <td style="border:1px solid #ccc" class="t_c"><input id="rpt1_chkBox_All" name="rpt1_chkBox_All" type="checkbox" onclick="jsfn_CheckedAll();"/></td>
                        <td style="border:1px solid #ccc" class="t_c"><b>상품명</b></td>
                        <td style="border:1px solid #ccc" class="t_c"><b>상품코드</b></td>
                        <td style="border:1px solid #ccc" class="t_c"><b>DC코드</b></td>
                        <td style="border:1px solid #ccc" class="t_c"><b>협력업체코드</b></td>
                        <td style="border:1px solid #ccc" class="t_c"><b>협력업체이름</b></td>
                        <td style="border:1px solid #ccc" class="t_c"><b>발주그룹</b></td> 
                        <td style="border:1px solid #ccc" class="t_c"><b>발주방법</b></td> 
                    </tr>
                </thead>
                <tr>
                    <td colspan="8" style="width:100%;height:25px;text-align:center;">
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
            <table style="width:580px;height:300px;" cellpadding="0" cellspacing="0" border="0">
                <colgroup>
                    <col style="width:100px;"/>
                    <col style="width:150px;" />
                    <col style="width:10px;" />
                    <col style="width:100px;" />
                    <col style="width:220px;" />
                </colgroup>
                <tr>
                    <td colspan="4" style="padding-left:5px;height:38px;">&nbsp;<h2>발주방법 등록</h2></td>
                    <td style="text-align:right;"><img ID="btnTsfCancel" style="height:20px;cursor:pointer;" src="/Images/common/grn_pp_cls_btn_over.png" OnClick="jsfn_btnModalPopExit();" /></td>
                </tr>
                <tr>
                    <td colspan="5" style="text-align:center;height:1px;padding-left:0;background-color:gray;"></td>
                </tr>
                <tr><td colspan="5"></td></tr>
                <tr>
                    <td class="row1" style="padding-left:5px;height:32px;">상품코드</td>
                    <td><input type="text" ID="lblstrITEM" runat="server" readonly="readonly" ></td>
                    <td></td>
                    <td class="row1" style="padding-left:5px;height:32px;">DC 코드</td>
                    <td><input type="text" ID="lblstrWH" runat="server" readonly="readonly" ></td>
                </tr>
                <tr>
                    <td class="row1" style="padding-left:5px;height:32px;">협력업체코드</td>
                    <td><input type="text" ID="lblstrSUPPLIER" runat="server" readonly="readonly" ></td>
                    <td></td>
                    <td class="row1" style="padding-left:5px;height:32px;">협력업체명</td>
                    <td><input type="text" ID="lblstrSUP_NAME" runat="server" style="width:200px;" readonly="readonly" ></td>
                </tr>
                <tr>
                    <td class="row1" style="padding-left:5px;height:32px;">발주그룹</td>
                    <td><input type="text" ID="lblstrORDER_GROUP" runat="server" readonly="readonly" ></td>
                    <td></td>
                    <td class="row1" style="padding-left:5px;height:32px;">발주방법</td>
                    <td>
                        <asp:DropDownList runat="server" ID="ddlstrREPL_METHOD" ClientIDMode="Static">
                            <asp:ListItem Value="" Selected="True">선택</asp:ListItem>
                            <asp:ListItem Value="A">자동</asp:ListItem>
                            <asp:ListItem Value="M">수동</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr><td colspan="5"></td></tr>
                <tr><td colspan="5"></td></tr>
                <tr>
                    <td colspan="5" style="text-align:center;height:1px;padding-left:0;background-color:gray;"></td>
                </tr>
                <tr>
                    <td colspan="5" style="text-align:center;">
                        <input type="button" ID="btnPop1Save" class="btn btn_inquiry" value="저장" onclick="jsfn_Pop1Save();" style="cursor:pointer;" />
                        <input type="button" ID="btnPop1Delete" class="btn btn_default" value="삭제" onclick="jsfn_Pop1Delete();" style="cursor:pointer;" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
<%--############ 모달 보여줄 페이지 E ############--%>

<%--### 아이템 검색 팝업 레이어 팝업용도 S ###--%>
<div id="LyPop1" class="modal_pop" style="width:550px;height:450px;padding: 2px 2px 2px 2px;display:none;background-color:#FFF !important;">
</div>
<%--### 아이템 검색 팝업 레이어 팝업용도 E ###--%>
</asp:Content>