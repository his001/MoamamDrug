﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterPage_Modal.master" AutoEventWireup="true" CodeFile="DrugInfo.aspx.cs" Inherits="Site_Data_DrugInfo" %>
<%@ Register Src="~/UserControls/ucPagingJS.ascx" TagName="ucPaging" TagPrefix="uc1" %> 

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHead" Runat="Server">

<!-- 1.LoadPage -->
<script type="text/javascript">
    var __clickCnt = 0;
    $(document).ready(function () {
        Initialize();
    });
</script>
<!-- 2.Init -->
<script type="text/javascript">
    function Initialize() {
        $('#divProgress4js').css("display", "none");
        $('#btnSearch').click(function () { jsfn_Search(); });

        //$("#<%= txtSerchName.ClientID %>").unbind(); // 기존의 엔터 이벤트 삭제
        $("#<%= txtSerchName.ClientID %>").keyup(function (e) {
            if (e.keyCode == 13) { jsfn_Search(); }
        });
    }
</script>
<!-- 3.Validation  -->
<script type="text/javascript">

</script>
<!-- 4. Custom Function  -->
<script type="text/javascript">

    function jsfn_Search() {
        var _ItemName = $("#<%= txtSerchName.ClientID %>").val().replace(/-/g, '');
        if (_ItemName == '') { alert('아이템명을 입력해 주세요.'); return false; }
        jsfn_progressBar('Y');
        var _SelSearchType = $("#SelSearchType").val();

        var _ucPageNo = $("#LyCus_hidPageNo").val();
        if (_ucPageNo == '') { _ucPageNo = '1'; }
        var _ucRowCount = $("#LyCus_hidRowCnt").val();
        if (_ucRowCount == '') { _ucRowCount = '30'; }

        var SpName = "SPM_Web_COMMON_Tbl_DrugOrg_Info_R";
        var SpParams = "PageNo" + '▥' + _ucPageNo + '▤';
        SpParams = SpParams + "RowCount" + '▥' + _ucRowCount + '▤';
        SpParams = SpParams + _SelSearchType + '▥' + _ItemName ;

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
        , error: function (msg) { fn_AjaxError(msg); jsfn_progressBar('N'); }
        });
    }

    function fn_SetGrid(dataRes) {
        $("#dvGrid1 > tbody > tr").remove();
        var _TOTAL_COUNT = 0;
        var gData = dataRes;
        var html = '';
        if (gData.length == 0) {
            jsfn_SetPage(_TOTAL_COUNT); // ucPagingJS.ascx
            var noData_html = '<tr><td colspan="24" style="width:100%;height:25px;text-align:center;"><b><font color="red">조회된 결과가 없습니다.</font></b></td></tr>';
            $('#dvGrid1 > tbody:last').append(noData_html);
            jsfn_progressBar('N');
            return false;
        } else { _TOTAL_COUNT = gData[0].TOTAL_COUNT; }

        for (var i = 0; i < gData.length; i++) {
            html = html + '<tr class="tr_c">';
            html = html + '<td class="t_c">' + gData[i].ITEM_SEQ + '</td>';
            html = html + '<td style="color:blue;text-align:left;"><a href="javascript:parent.jsfn_SerchItemClick4PopUp(\'' + gData[i].ITEM_SEQ + '\',\'' + gData[i].ITEM_NAME + '\',\'' + gData[i].ENTP_SEQ + '\',\'' + gData[i].ENTP_NAME + '\');"> &nbsp;<asp:Label ID="lbLinkValue" runat="server" ForeColor="Blue"> ' + gData[i].ITEM_NAME + '</asp:Label></a></td>';
            html = html + '<td class="t_c">' + gData[i].CHART + '</td>';
            html = html + '<td class="t_c">' + gData[i].CLASS_NAME + '</td>';
            html = html + '</tr>';
        }
        $('#dvGrid1 > tbody:last').append(html);
        jsfn_SetPage(_TOTAL_COUNT);     // ucPagingJS.ascx
        setTimeout("jsfn_progressBar('N');", 500);
    }

</script>


<script type="text/javascript">
    function jsfn_PopClose() {
        parent.jsfn_btnPopExit();
    }
 </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentMain" Runat="Server">
<article id="grid">
    <table class="PopupTabelTitle" cellpadding="0" cellspacing="0">
        <tr>
            <td class="Popup_td" style="background-color:#003399;">&nbsp;제약품 검색</td>
            <td class="t_r" style="padding:0;margin:0;background-color:#003399;">
                <img ID="btnTsfCancel1" style="height:20px;vertical-align:middle;cursor:pointer;" src="/Images/Common/grn_pp_cls_btn_over.png" OnClick="jsfn_PopClose();"  />
            </td>

        </tr>
    </table>
    <br />
    

    <%-- ############################################ 본문 S ############################################ --%>
    <!-- List Start -->
<!-- 조회조건 Start -->
    <article id="search">
	    <div class="search row1"><!-- 한 줄: row1, 두 줄: row2, 세 줄: row3 -->
		    <div>
                <select id="SelSearchType">
                    <option value="ITEM_NAME">약이름</option>
                    <option value="ENTP_NAME">제약회사이름</option>
                    <option value="CHART">성상</option>
                    <option value="CLASS_NO">분류기호</option>
                    <option value="CLASS_NAME">분류명</option>
                    <option value="FORM_CODE_NAME">제형코드이름</option>
                </select>
                <asp:TextBox ID="txtSerchName" runat="server" CssClass="input hangleDefault" style="width:200px;height:30px;" AutoPostBack="false" onkeydown = "return (event.keyCode!=13);" ></asp:TextBox> 
                <input type="button" ID="btnSearch" class="btn btn_inquiry" value="조회" style="cursor:pointer;" />

                 외부에서 가져오기
                <asp:TextBox ID="txt_ITEM_NAME" runat="server" CssClass="input hangleDefault" style="width:200px;height:30px;" AutoPostBack="false" onkeydown = "return (event.keyCode!=13);" ></asp:TextBox> 
                <asp:Button ID="btnSearchSvr" CssClass="btn btn_inquiry" Text="가져오기" style="cursor:pointer;" />
		    </div>
	    </div>
    </article>
    <!-- 조회조건 End -->
<BR/>
<div style="width:100%;text-align:center;height:250px;overflow-x:hidden;overflow-y:auto;">
    <table width="100%" >
        <tr>
            <td style="padding-left:0;">
                <article id="grid">
		            <!--<h2 class="tit">서브 타이틀</h2>-->
		            <table id="dvGrid1">
                        <colgroup>
                            <col style="width:100px;" />
                            <col />
                        </colgroup>
			            <thead>
                        <tr>
                            <th>순번</th>
                            <th>약이름</th>
                            <th>성상</th>
                            <th>분류명</th>
                        </tr>
			            </thead>
			            <tbody>
                            <tr class="tr_c">
                                <td colspan="21" style="width:100%;height:25px;text-align:center;"></td>
                            </tr>
			            </tbody>						
		            </table>

	            </article>
            </td>

        </tr>
    </table>
    <!-- List End -->
</div> 
<%-- ############################################ 본문 E ############################################ --%>

</article>  

<!-- ############################################ Pageing Start ############################################ -->
<uc1:ucPaging ID="ucPaging" runat="server" />
<!-- ############################################ Pageing End ############################################ -->

<div style="width:100%;text-align:center;"> 
<hr /> 
</div>
</asp:Content>