<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterPage_Panel.master" AutoEventWireup="true" CodeFile="ExecludeItem.aspx.cs" Inherits="Site_Transfer_ExecludeItem" %>
<%@ Register Src="~/UserControls/ucPaging.ascx" TagName="ucPaging" TagPrefix="uc1" %>
<%@ Register src="~/UserControls/ucWeek.ascx" tagname="ucWeek" tagprefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHead" runat="Server">
<script type="text/javascript">

    ///////////////////////////////////////////////////////////////////////////////////////////////
    //  업로드 버튼 클릭 시 업로드 페이지 호출                                                     //
    ///////////////////////////////////////////////////////////////////////////////////////////////
    function CallUpload() {
        return showWindow(String.format("../PopupPage/ExecludeItemUpload.aspx"), 1000, 700);            
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////
    //    Validation 체크                                                                        //    
    ///////////////////////////////////////////////////////////////////////////////////////////////
    function IsValidation() {
        var isReturnValue = true;
        if ($("#<%=txtPopup_ITEM.ClientID%>").val().trim().length == 0 || $("#<%=txtPopup_ITEM.ClientID%>").val() == 0) {
            isReturnValue = false;
            alert("아이템을 입력하세요!");
            $("#<%=txtPopup_ITEM.ClientID%>").focus();
            $("#<%=txtPopup_ITEM.ClientID%>").select();
        }

        return isReturnValue;
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////
    //  삭제 버튼 클릭 시 Confirm 창                                                              //
    ///////////////////////////////////////////////////////////////////////////////////////////////
    function DeleteConfirm() {
        return confirm("삭제 하시겠습니까?");
    }

</script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMain" runat="Server">

    <!-- 조회조건 -->
    <article id="search">
	    <div class="t_r">
            <hpf:NButton ID="btnSearch" runat="server" CssClass="btn btn_inquiry" OnClick="btnSearch_Click" Text="조회" SecurityType="Inquiry" CmdType="SELECT" />&nbsp;
            <asp:Button ID="btnNew" runat="server" CssClass="btn btn_default" Text="신규" onclick="btnNew_Click" />&nbsp;
            <hpf:NButton ID="btnExcel" runat="server" CssClass="btn btn_default" OnClick="btnExcel_Click" Text="다운로드" SecurityType="Inquiry" CmdType="EXCEL" /> &nbsp;
            <asp:Button ID="btnUpload" runat="server" CssClass="btn btn_default" OnClientClick="return CallUpload();" Text="업로드" />
	    </div>
	    <div class="search row1"><!-- 한 줄: row1, 두 줄: row2, 세 줄: row3 -->
		    <div>
			    <label for="">ITEM</label>
			    <hpf:NTextBox ID="txtItem" runat="server" CssClass="input" Style="width:200px;" MaxLength="9" Validation="UnSignedNum" />
                <label for="">필수항목(예제)</label>
			    <hpf:NTextBox ID="txtIn" runat="server" CssClass="input" Style="width:200px;" MaxLength="9" NRequired="true" ValidationGroup="Search" ExceptionDesc="필수항목(예제)" Validation="Date" />
		    </div>
	    </div>
    </article>
    <!-- 조회조건 End -->

    <br />

    <!-- List Start -->
	<article id="grid">
		<!--<h2 class="tit">서브 타이틀</h2>-->
		<table>
            <colgroup>
                <col style="width:150px;" />
                <col style="width:200px;" />
                <col />
                <col style="width:150px;" />
                <col style="width:200px;" />
            </colgroup>
			<thead>
                <tr>
                    <th>ACTION_TYPE</th>
                    <th>아이템</th>
                    <th>아이템명</th>
                    <th>생성자</th>
                    <th>생성일자</th>
                </tr>
			</thead>
		</table>
		<table>
            <colgroup>
                <col style="width:150px;" />
                <col style="width:200px;" />
                <col />
                <col style="width:150px;" />
                <col style="width:200px;" />
            </colgroup>
			<tbody>
                <asp:Repeater ID="rptList" runat="server" OnItemCommand="rptList_ItemCommand">
                    <ItemTemplate>
                        <tr>
                            <td class="t_c"><%#nEval("ACTION_TYPE")%></td>
                            <td class="t_c">
                                <asp:LinkButton runat="server" ID="lnkITEM" CommandName="Select" CommandArgument='<%# string.Format("{0}", nEval("ITEM")) %>'><%#nEval("ITEM")%></asp:LinkButton>
                            </td>
                            <td><hpf:NTextBox ID="txtItemName" runat="server" CssClass="input" Width="90%" Text='<%#nEval("ITEM_DESC")%>' /></td>
                            <td class="t_c"><%#nEval("CREATE_USER")%></td>
                            <td class="t_c"><%#nEval("CREATE_DATE")%></td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                <tr class="pager">
                    <td colspan="5">

                    <uc1:ucPaging ID="ucPaging" OnSelEvent="ucPaging_SelEvent" runat="server" />

                    </td>
                </tr>
			</tbody>						
		</table>

	</article>
    <!-- List End -->



    <!-- ModalPopupExtender Start -->
    <asp:Button ID="btnShowPopup" runat="server" Style="display:none;" />
    <ajaxToolkit:ModalPopupExtender ID="ModalPopExt" runat="server"
        BackgroundCssClass="modalBackground"
        PopupControlID="EditPanel"
        TargetControlID="btnShowPopup"
        CancelControlID="btnCancel"
        PopupDragHandleControlID="HeaderPanel">
    </ajaxToolkit:ModalPopupExtender>
    <!-- PopupPanel -->
    <asp:Panel ID="EditPanel" runat="server" CssClass="modalPopup" Style="display:none;width:300px;height:130px;">
        <asp:Panel ID="HeaderPanel" runat="server" CssClass="PopupPanel">
            <div>
                <table class="PopupTabelTitle">
                    <tr>
                        <td class="Popup_td">&nbsp; Transfer 제외 Item 등록</td>
                        <td style="padding:0;margin:0;text-align;right;">
                            <asp:ImageButton ID="btnTsfCancel" runat="server" Height="20px" ImageUrl="~/Images/common/grn_pp_cls_btn_over.png" OnClick="btnPopupX_Click" />
                        </td>

                    </tr>
                </table>
            </div>
        </asp:Panel>
        <br />
        <div style="width:100%;text-align:center;">
            <table style="text-align:left;width:100%;background-color:#cccccc;padding:5px;">
                <colgroup>
                    <col style="width:120px" />
                    <col style="width:130px" />
                    <col style="width:120px" />
                    <col />
                </colgroup>
                <tr style="background-color:#ffffff;">
                    <th style="background-color:#efefef;width:80px;">아이템</th>
                    <td>
                        <hpf:NTextBox ID="txtPopup_ITEM" runat="server" CssClass="input" style="text-align:center;width:90px;" MaxLength="9" Validation="Numeric"></hpf:NTextBox>                    
                    </td>
                </tr>
            </table>
        </div>
        <div style="width:100%;text-align:center;">
            <br />
            <hr />
            <asp:Button ID="btnAdd" runat="server" Text="저장" CssClass="btn btn_default" OnClientClick="return IsValidation();" OnClick="btnPopupAdd_Click" />
            <asp:Button ID="btnDelete" runat="server" Text="삭제" CssClass="btn btn_default" OnClientClick="return DeleteConfirm();" OnClick="btnPopupDelete_Click" />
            <asp:Button ID="btnCancel" runat="server" Text="닫기" CssClass="btn btn_default" OnClick="btnPopupExit_Click" />
            <asp:HiddenField ID="hdnPopupITEM" runat="server" />
            <asp:HiddenField ID="hdnCMDCRUD" runat="server" />
        </div>
    </asp:Panel>
    <!-- ModalPopupExtender End -->


</asp:Content>