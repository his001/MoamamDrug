<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterPageTabSub.master" AutoEventWireup="true" CodeFile="StoreOrderForecast.aspx.cs" Inherits="Site_Report_StoreOrderForecast" %>
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
        $("#<%=txtItem.ClientID %>").keypress(function (event) { if (event.which == 13 && $("#<%=txtItem.ClientID %>").val().length == 9) { return jsfn_Search_Item4text(); } });
        $("#<%=txtItem.ClientID %>").keyup(function (event) {
            if ((event.which >= 48 && event.which <= 57) || (event.which >= 96 && event.which <= 105) || (event.which == 8 /* backspace*/) || (event.which == 46 /* del*/) || (event.which == 32 /* space*/)) {
                if ($("#<%=txtItem.ClientID %>").val().length == 0) { $('#<%=lblItemName.ClientID%>').text('');jsfn_Search_ItemView4textYN('N'); } // 
                if ($("#<%=txtItem.ClientID %>").val().length >= 6 && $("#<%=txtItem.ClientID %>").val().length <= 8) { jsfn_Search_ItemView4text(); }
            }
        });
        //$("#<%= ddlToSectionList.ClientID %>").val('1609');
    }
</script>
<!-- 3.Validation  -->
<script type="text/javascript">
</script>
<!-- 4. Custom Function  -->
<script type="text/javascript">

    function jsfn_Search() {
        var _OrderDATE = $("#<%= txtOrderDATE.ClientID %>").val().replace(/-/g, '');
        if (_OrderDATE == '') { jsfn_alert('발주일을 선택해 주세요.'); return false; }
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
        var SpName = "SP_WEB_Site_Report_StoreOrderForecast_List1_R";
        var SpParams = "SECTIONFROM" + '▥' + _ddlFromSectionList + '▤';
        SpParams = SpParams + "SECTIONTO" + '▥' + _ddlToSectionList + '▤';
        SpParams = SpParams + "ITEM" + '▥' + _Item + '▤';
        SpParams = SpParams + "ROWCNT" + '▥' + _ucRowCount + '▤';
        SpParams = SpParams + "PAGENUM" + '▥' + _ucPageNo + '▤';
        SpParams = SpParams + "SMODE" + '▥' + 'SELECT' + '▤';
        SpParams = SpParams + "SUPPLIER" + '▥' + $('#<%=txtSuppCode.ClientID%>').val().replace(/\'/g, '') + '▤';
        SpParams = SpParams + "RUD_ID" + '▥' + $('#<%=ddlrudterm.ClientID%>').val() + '▤';
        SpParams = SpParams + "OrderDATE" + '▥' + _OrderDATE + '▤';
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
            //jsfn_alert('조회된 결과가 없습니다.');
            var noData_html = '<tr><td colspan="24" style="width:100%;height:25px;text-align:center;"><b><font color="red">조회된 결과가 없습니다.</font></b></td></tr>';
            $('#dvGrid1 > tbody:last').append(noData_html);
            jsfn_progressBarMst('N');
            return false;
        } else { _TOTAL_COUNT = gData[0].TOTAL_COUNT; }
       
        var _PRIMARY_PACK_NO = '';
        for (var i = 0; i < gData.length; i++) {

            if (gData[i].SUPPLIER == null) { gData[i].SUPPLIER=''; }

            html = html + '<tr class="tr_c">';
            html = html + '<td class="t_c" style="text-align:center;">' + gData[i].ORDER_DATE + '</td>';
            html = html + '<td class="t_c" style="text-align:center;">' + gData[i].ITEM + '</td>';
            html = html + '<td class="t_c" style="text-align:left;padding-right:5px;width:250px;overflow:hidden;text-overflow:ellipsis;white-space:nowrap;">' + gData[i].ITEM_NAME + '</td>';
            html = html + '<td class="t_c" style="text-align:center;">' + gData[i].LOC + '</td>';
            html = html + '<td class="t_c" style="text-align:center;">' + gData[i].SOURCE_WH + '</td>';
            html = html + '<td class="t_c" style="text-align:right;padding-right:5px;">' + gData[i].SUPPLIER + '</td>';
            html = html + '<td class="t_c" style="text-align:center;">' + gData[i].PRIMARY_PACK_NO + '</td>';   // TPND
            html = html + '<td class="t_c" style="text-align:right;padding-right:5px;width:100px;">' + gData[i].PRIMARY_PACK_QTY + '</td>';
            html = html + '<td class="t_c" style="text-align:right;padding-right:5px;width:100px;">' + gData[i].SUPP_PACK_SIZE + '</td>';  //SUPP PACKSIZE
            html = html + '<td class="t_c" style="text-align:right;padding-right:5px;width:100px;">' + gData[i].SOH_QTY.toLocaleString() + '</td>';
            html = html + '<td class="t_c" style="text-align:right;padding-right:5px;width:100px;">' + gData[i].SOO_QTY.toLocaleString() + '</td>';
            html = html + '<td class="t_c" style="text-align:right;padding-right:5px;width:100px;">' + gData[i].PRES_STOCK + '</td>';
            html = html + '<td class="t_c" style="text-align:right;padding-right:5px;width:100px;">' + gData[i].SALES_FORECAST.toLocaleString() + '</td>';
            html = html + '<td class="t_c" style="text-align:right;padding-right:5px;width:100px;">' + gData[i].ROQ_QTY.toLocaleString() + '</td>';
            html = html + '<td class="t_c" style="text-align:right;padding-right:5px;width:100px;">' + gData[i].ROQ_QTY_TPND.toLocaleString() + '</td>';
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

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentMain" runat="Server">
    <!-- 조회조건 -->
    <article id="search">
               
	    <div class="t_r">
                <input type="button" ID="btnSearch" class="btn btn_inquiry" value="조회" style="cursor:pointer;" />
                <hpf:NButton ID="btnExcel" runat="server" CssClass="btn btn_default" OnClick="btnExcel_Click" Text="다운로드" SecurityType="Inquiry" CmdType="EXCEL" DisabledCss="GlobalBtnNone" style="cursor:pointer;"/>
        </div>
        <div class="search row2" style="line-height: 34px;" ><!-- 한 줄: row1, 두 줄: row2, 세 줄: row3 --> 
            <div>
                <label for="">발주일</label> 
                <hpf:NTextBox ID="txtOrderDATE" runat="server" CssClass="input" style="text-align:center;width:90px;padding-left:5px;" ClientIDMode="Static" Validation="Date" ReadOnly="true" />
                <label for="">DC</label> 
                <asp:DropDownList ID="ddlDccodeList" runat="server" Width="120" ClientIDMode="Static" ></asp:DropDownList>  
                <label for="">SECTION</label> 
                <asp:DropDownList ID="ddlFromSectionList" runat="server" Width="160" ClientIDMode="Static" />
                &nbsp;~&nbsp;
                <asp:DropDownList ID="ddlToSectionList" runat="server" Width="160" ClientIDMode="Static" />

                <br />
                <label for="">ITEM</label>
                <hpf:NTextBox ID="txtItem" runat="server" CssClass="input" Style="width:75px" MaxLength="9"  Validation="Numeric" />&nbsp;
                <span id="lblItemName" runat="server"  ></span>
                &nbsp;
                <input type="button" ID="btnSearch1" class="btn btn_inquiry"  value="아이템검색"  OnClick="jsfn_ItemNameList();" style="cursor:pointer;" />
                <!-- 2016-12-13 검색 추가 부분 S -->
                <label for="">협력업체코드</label> 
                <hpf:NTextBox ID="txtSuppCode" runat="server" CssClass="input" Style="width:75px;padding-left:5px;" MaxLength="9"  Validation="Numeric" ClientIDMode="Static"/>&nbsp;
                <span id="lblSuppName" runat="server"></span>   
                &nbsp;
                <input type="button" ID="btnSearch2" class="btn btn_inquiry"  value="협력업체"  OnClick="jsfn_GetSuppName();" style="cursor:pointer;" />

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
			<thead>
                <tr style="height:35px;">
			        <td style="border:1px solid #ccc;font-size:11px;" class="t_c"><b>ORDER_DATE</b></td>
                    <td style="border:1px solid #ccc;font-size:11px;" class="t_c"><b>ITEM</b></td>
                    <td style="border:1px solid #ccc;font-size:11px;" class="t_c"><b>ITEM명</b></td>
                    <td style="border:1px solid #ccc;font-size:11px;" class="t_c"><b>LOC</b></td>
                    <td style="border:1px solid #ccc;font-size:11px;" class="t_c"><b>SOURCE_WH</b></td>
                    <td style="border:1px solid #ccc;font-size:11px;" class="t_c"><b>SUPPLIER</b></td>
                    <td style="border:1px solid #ccc;font-size:11px;" class="t_c"><b>TPND</b></td>
                    <td style="border:1px solid #ccc;font-size:11px;" class="t_c"><b>PACK_QTY</b></td>
                    <td style="border:1px solid #ccc;font-size:11px;" class="t_c"><b>SUPP<br/>PACKSIZE</b></td>
                    <td style="border:1px solid #ccc;font-size:11px;" class="t_c"><b>SOH_QTY</b></td>
                    
                    <td style="border:1px solid #ccc;font-size:11px;" class="t_c"><b>SOO_QTY</b></td>
                    <td style="border:1px solid #ccc;font-size:11px;" class="t_c"><b>PRES<br/>STOCK</b></td>
                    <td style="border:1px solid #ccc;font-size:11px;" class="t_c"><b>SALES<br/>FORECAST</b></td>
                    <td style="border:1px solid #ccc;font-size:11px;" class="t_c"><b>ROQ_QTY</b></td>
                    <td style="border:1px solid #ccc;font-size:11px;" class="t_c"><b>ROQ_QTY<br/>TPND</b></td>
                </tr>
			</thead>
                <tr>
                    <td colspan="15" style="width:100%;height:25px;text-align:center;">
                    </td>
                </tr>
                <tr>
                    <td colspan="15" style="width:100%;height:25px;text-align:center;">
                        <%--<b><asp:Label ID="DataExists" runat="server" ForeColor="Red" Font-Size="13px"></asp:Label></b>--%>
                    </td>
                </tr>
            </table> 
        </div> 
	</article>

    <uc1:ucPaging ID="ucPaging" runat="server" /> <%--OnSelEvent="ucPaging_SelEvent" --%>


<%--### 아이템 검색 팝업 레이어 팝업용도 S ###--%>
<div id="LyPop1" class="modal_pop" style="width:550px;height:450px;padding: 2px 2px 2px 2px;display:none;background-color:#FFF !important;">
</div>
<%--### 아이템 검색 팝업 레이어 팝업용도 E ###--%>
    <!-- Excel Download 용 Excel 버튼용 트리거 S -->
    <asp:UpdatePanel ID ="UpDatePanel1" runat ="server">
        <Triggers>
        <asp:PostBackTrigger ControlID="btnExcel" />
        </Triggers>
    </asp:UpdatePanel>
    <!-- Excel Download 용 Excel 버튼용 트리거 E -->

</asp:Content>
