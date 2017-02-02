<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucPagingJS.ascx.cs" Inherits="UserControls_ucPagingJS" %>
<!-- ############################################ Pageing Start ############################################ -->
<script type="text/javascript">
    function jsfn_SetPage(totCnt) {
        $("#LyCus_lblCount").html('Total ' + totCnt + ' 건');
        $("#LyCus_hidTotal").val(totCnt);
        //alert(_hidPageNo + ' : _hidPageNo\n' + _hidTotal + ' : _hidTotal\n' + _hidRowCnt + ' : _hidRowCnt\n');
        jsfn_reDrawPaging();
    }

    function jsfn_Page_Url(pageNo) {
        $('#LyCus_hidPageNo').val(pageNo);
        jsfn_Search();
    }
    function jsfn_LyCus_selPageRowCnt(strval) {
        $('#LyCus_hidPageNo').val('1');     // 1페이지로 변경 한다
        $('#LyCus_hidRowCnt').val(strval);
        jsfn_Search();
    }

    function jsfn_reDrawPaging() {

        var _hidTotal = $('#LyCus_hidTotal').val();
        if (_hidTotal == '0') {
            $('#LyCus_hidPageNo').val('1');
            $('#LyCus_PageLeftAreaSel').html('');
            $('#LyCus_PageArea').html('');
            return false;
        }

        var _hidRowCnt = $('#LyCus_hidRowCnt').val();
        var _hidPageNo = $('#LyCus_hidPageNo').val();
        if (_hidPageNo == '') { _hidPageNo = 1;}
        var _hidEndPage = parseInt(parseFloat(_hidTotal) / parseFloat(_hidRowCnt))+1;
        
        var _startNo = '1';
        var _prevNo = parseInt(parseInt(_hidPageNo) - 4) == 0 ? 1 : parseInt(parseInt(_hidPageNo) - 4);
        var _nextNo = parseInt(parseInt(_hidPageNo) + 4) > parseInt(_hidEndPage) ? _hidEndPage : parseInt(parseInt(_hidPageNo) + 4);
        var _endNo = _hidEndPage;

        var strHTMLSelPage = '';
        strHTMLSelPage = strHTMLSelPage + '<select id="LyCus_selPageRowCnt" style="width:80px;padding:0 0 0 5px;" onchange="jsfn_LyCus_selPageRowCnt(this.value);">';
        if (_hidRowCnt == "30") { strHTMLSelPage = strHTMLSelPage + '<option value="30" selected>30행</option>'; } else { strHTMLSelPage = strHTMLSelPage + '<option value="30">30행</option>'; }
        if (_hidRowCnt == "50") { strHTMLSelPage = strHTMLSelPage + '<option value="50" selected>50행</option>'; } else { strHTMLSelPage = strHTMLSelPage + '<option value="50">50행</option>'; }
        if (_hidRowCnt == "100") { strHTMLSelPage = strHTMLSelPage + '<option value="100" selected>100행</option>'; } else { strHTMLSelPage = strHTMLSelPage + '<option value="100">100행</option>'; }
        if (_hidRowCnt == "300") { strHTMLSelPage = strHTMLSelPage + '<option value="300" selected>300행</option>'; } else { strHTMLSelPage = strHTMLSelPage + '<option value="300">300행</option>'; }
        strHTMLSelPage = strHTMLSelPage + '</select>';
        $('#LyCus_PageLeftAreaSel').html(strHTMLSelPage);
        

        var strHTMLPage = '<table style="width:100%;margin:0 0 0 0padding:0 0 0 0;"><tr><td style="height:25px;padding:10px 0 0 0;">';
        if (_hidPageNo == '1') {
            strHTMLPage = strHTMLPage + '<button type="button" class="start" OnClick="alert(\'첫 페이지입니다.\');" title="첫 페이지입니다."></button>';
        } else { strHTMLPage = strHTMLPage + '<button type="button" class="start" onclick="jsfn_Page_Url(' + _startNo + ');" title="' + _startNo + '페이지로 이동"></button>'; }
        
        if (_prevNo > 0) { strHTMLPage = strHTMLPage + '<button type="button" class="prev" onclick="jsfn_Page_Url(' + _prevNo + ');" title="' + _prevNo + '페이지로 이동"></button>'; } else {
            strHTMLPage = strHTMLPage + '<button type="button" class="prev" OnClick="alert(\'첫 페이지 그룹입니다.\');" title="첫 페이지 그룹입니다."></button>';
        }
        strHTMLPage = strHTMLPage + '</td><td><p>';

        if (parseInt(parseInt(_hidPageNo) - 3) > 0) { strHTMLPage = strHTMLPage + '<a href="#" onclick="jsfn_Page_Url(' + parseInt(parseInt(_hidPageNo) - 3) + ');">' + parseInt(parseInt(_hidPageNo) - 3) + '</a>&nbsp;'; }
        if (parseInt(parseInt(_hidPageNo) - 2) > 0) { strHTMLPage = strHTMLPage + '<a href="#" onclick="jsfn_Page_Url(' + parseInt(parseInt(_hidPageNo) - 2) + ');">' + parseInt(parseInt(_hidPageNo) - 2) + '</a>&nbsp;'; }
        if (parseInt(parseInt(_hidPageNo) - 1) > 0) { strHTMLPage = strHTMLPage + '<a href="#" onclick="jsfn_Page_Url(' + parseInt(parseInt(_hidPageNo) - 1) + ');">' + parseInt(parseInt(_hidPageNo) - 1) + '</a>&nbsp;'; }

        strHTMLPage = strHTMLPage + '</td><td><b><a class="frst strong">' + _hidPageNo + '</a></b>&nbsp;</td><td>';

        if (parseInt(parseInt(_hidPageNo) + 1) <= _hidEndPage) { strHTMLPage = strHTMLPage + '<a href="#" onclick="jsfn_Page_Url(' + parseInt(parseInt(_hidPageNo) + 1) + ');">' + parseInt(parseInt(_hidPageNo) + 1) + '</a>&nbsp;'; }
        if (parseInt(parseInt(_hidPageNo) + 2) <= _hidEndPage) { strHTMLPage = strHTMLPage + '<a href="#" onclick="jsfn_Page_Url(' + parseInt(parseInt(_hidPageNo) + 2) + ');">' + parseInt(parseInt(_hidPageNo) + 2) + '</a>&nbsp;'; }
        if (parseInt(parseInt(_hidPageNo) + 3) <= _hidEndPage) { strHTMLPage = strHTMLPage + '<a href="#" onclick="jsfn_Page_Url(' + parseInt(parseInt(_hidPageNo) + 3) + ');">' + parseInt(parseInt(_hidPageNo) + 3) + '</a>&nbsp;'; }

        strHTMLPage = strHTMLPage + '</p></td><td>';
        strHTMLPage = strHTMLPage + '<button type="button" class="next" onclick="jsfn_Page_Url(' + _nextNo + ');" title="' + _nextNo + '페이지로 이동"></button>';
        strHTMLPage = strHTMLPage + '<button type="button" class="end" onclick="jsfn_Page_Url(' + _endNo + ');" title="' + _endNo + '페이지로 이동"></button>';
        
        strHTMLPage = strHTMLPage + '</td></tr></table>';
        $('#LyCus_PageArea').html(strHTMLPage);
    }
</script>
<div class="pager"> 
    <table style="width:100%">
        <tr>
            <td>
    <div id="LyCus_PageLeftArea" class="page_view f_l" style="padding:8px 0 0 12px;">
        <span id="LyCus_PageLeftAreaSel" style="width:100px;height:33px;text-align:center;vertical-align:middle;"></span>
    </div>
            </td>
            <td style="height:31px">
	<div class="paging p_a" id="LyCus_PageArea">
		
	</div>
            </td>
            <td>
	<div class="page_view f_r">
		<span id="LyCus_lblCount" style="display:inline-block;border-color:#000000;border-style:None;">
            Total 0 건
		</span>
	</div>

            </td>
        </tr>
    </table>

	<input type="hidden" id="LyCus_hidPageNo" value="1" onchange="jsfn_reDrawPaging();">
    <input type="hidden" id="LyCus_hidRowCnt" value="30">
	<input type="hidden" id="LyCus_hidTotal" value="0">
</div>
<!-- ############################################ Pageing End ############################################ -->