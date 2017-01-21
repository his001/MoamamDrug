<%@ Page Language="C#" MasterPageFile="~/Master/MasterPage_Panel.master" AutoEventWireup="true" CodeFile="MenuList.aspx.cs" Inherits="Site_Management_MenuList" %>

<%@ Register Src="~/UserControls/ucPaging.ascx" TagName="ucPaging" TagPrefix="uc1" %>
<%@ Register Src="~/UserControls/ucPaging.ascx" TagName="ucPaging" TagPrefix="uc2" %>
<%@ Register Src="~/UserControls/ucPaging.ascx" TagName="ucPaging" TagPrefix="uc3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHead" runat="Server">
    <script type="text/javascript">
        //대메뉴 팝업 저장버튼 클릭
        function GetMenuGroupIsValidation() {
            var txtPopGroupNm = document.getElementById("<%= txtPopGroupNm.ClientID %>");
            if (txtPopGroupNm.value == "") {
                alert("메뉴명을 입력하세요.");
                txtPopGroupNm.focus();
                return false;
            }
            if (confirm("저장하시겠습니까?")) {
                return true;
            }
            return false;
        }

        //대메뉴 팝업 삭제버튼 클릭
        function GetMenuGroupDeleteConfirm() {
            if (confirm("삭제하시겠습니까?")) {
                return true;
            }
            return false;
        }

        //하위메뉴 등록버튼 클릭
        function GetCheckParentGroupCd() {
            var txtHdnParentGroupCd = document.getElementById("<%= hdnParentGroupCd.ClientID %>");
            if (txtHdnParentGroupCd.value == "") {
                alert("대메뉴를 선택하세요.");
                return false;
            }
            return true;
        }


        //하위메뉴 팝업 저장버튼 클릭
        function GetMenuIsValidation() {
            var txtPopMenuNm = document.getElementById("<%= txtPopMenuNm.ClientID %>");  //하위메뉴명
            var txtPopMenuUrl = document.getElementById("<%= txtPopMenuUrl.ClientID %>"); //하위메뉴경로

            if (txtPopMenuNm.value == "") {
                alert("메뉴명을 입력하세요.");
                txtPopMenuNm.focus();
                return false;
            }
            if (txtPopMenuUrl.value == "") {
                alert("메뉴경로를 입력하세요.");
                txtPopMenuUrl.focus();
                return false;
            }

            if (confirm("저장하시겠습니까?")) {
                return true;
            }

            return false;
        }


        //하위메뉴 삭제
        function GetMenuDeleteConfirm() {
            if (confirm("삭제하시겠습니까?")) {
                return true;
            }

            return false;
        }

        //
        function jsfn_saveAuth() {
            var _chkMenu = document.getElementById("<%= hdnParentGroupCd.ClientID %>");
            var _chkPage = document.getElementById("<%= hdnNowMenuCd.ClientID %>");
            //alert(_chkMenu + ' : _chkMenu \n' + _chkPage + ' : _chkPage \n');
            if (_chkMenu.value == "") {
                alert("대메뉴를 선택하세요.");
                return false;
            }
            if (_chkPage.value == "") {
                alert("페이지를 선택하세요.");
                return false;
            }

            return true;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMain" runat="Server">
    



    <!-- 조회조건 -->
    <article id="search">
        <h2 class="tit">메뉴</h2>
	    <div class="t_r">
            <asp:Button ID="btnSearch" runat="server" CssClass="btn btn_inquiry" OnClick="btnSearch_Click" Text="조회" Visible="false" />&nbsp;
            <asp:Button ID="btnInsert" runat="server" CssClass="btn btn_default" OnClick="btnInsert_Click" Text="등록" />
	    </div>
    </article>
    <!-- 조회조건 End -->


    <br />
	<article id="grid">
		<table>
            <colgroup>
                <col />
                <col style="width:180px" />
                <col style="width:180px" />
                <col style="width:180px" />
                <col style="width:180px" />
                <col style="width:180px" />
            </colgroup>
			<thead>
                <tr>
                    <th>메뉴명</th>
                    <th>사용자그룹</th>
                    <th>사용여부</th>
                    <th>최종수정일</th>
                    <th>하위메뉴설정</th>
                    <th>순서변경</th>
                </tr>
			</thead>
		</table>
		<table>
            <colgroup>
                <col />
                <col style="width:180px" />
                <col style="width:180px" />
                <col style="width:180px" />
                <col style="width:180px" />
                <col style="width:180px" />
            </colgroup>
			<tbody>
                <asp:Repeater ID="rptMenuGroupList" runat="server" OnItemCommand="rptMenuGroupList_ItemCommand" >
                    <ItemTemplate>
                        <tr>
                            <td>
                                <asp:LinkButton runat="server" ID="lnkGroupNm" CommandName="mainGroupSelect" CommandArgument='<%# string.Format("{0},{1},{2},{3}", nEval("GROUP_CD"), nEval("GROUP_NM"), nEval("USER_AUTH_CD"), nEval("USE_YN")) %>'>
                                    &nbsp;<%#nEval("GROUP_NM")%>
                                </asp:LinkButton>
                            </td>
                            <td><%#nEval("SUB_CD_NM")%></td>
                            <td><%#nEval("USE_YN_TEXT")%></td>
                            <td><%#nEval("UPDATE_DATE")%></td>
                            <td>
                                <asp:LinkButton runat="server" ID="lnkGroupCd" CommandName="menuSelect" CommandArgument='<%#nEval("GROUP_CD") %>'>
                                    하위메뉴설정
                                </asp:LinkButton>
                            </td>
                            <td style="text-align:center;">
                                <asp:LinkButton runat="server" ID="lnkSortUp" CommandName="menuSortUp" CommandArgument='<%#nEval("GROUP_CD") %>'>▲</asp:LinkButton>
                                <asp:LinkButton runat="server" ID="lnkSortDown" CommandName="menuSortDown" CommandArgument='<%#nEval("GROUP_CD") %>'>▼</asp:LinkButton>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                <tr class="pager">
                    <td colspan="6">
                        <uc1:ucPaging ID="ucPaging" OnSelEvent="ucPaging_SelEvent" runat="server" />
                    </td>
                </tr>
			</tbody>						
		</table>
	</article>





    <!------------------------------------------------- 상단, 하단 리스트 구분 ------------------------------------------------->



    <br /><br /><br />
    <!-- 조회조건 -->
    <article id="search">
        <h2 class="tit">페이지</h2>
	    <div class="t_r">
            <asp:Button ID="BtnSearch2" runat="server" CssClass="btn btn_inquiry" OnClick="btnSearch2_Click" Text="조회" Visible="false" />&nbsp;
            <asp:Button ID="BtnInsert2" runat="server" CssClass="btn btn_default" OnClick="btnInsert2_Click" Text="등록" OnClientClick="return GetCheckParentGroupCd();" />
	    </div>
    </article>
    <!-- 조회조건 End -->


    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <asp:Label ID="lblCount2" BorderStyle="None" BorderColor="0" runat="server"></asp:Label>&nbsp;&nbsp;
            <asp:HiddenField ID="hdnParentGroupCd" runat="server" />
            <asp:HiddenField ID="hdnNowMenuCd" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
	<article id="grid">
		<!--<h2 class="tit">서브 타이틀</h2>-->
		<table>
            <colgroup>
                <col />
                <col style="width:300px" />
                <col style="width:130px" />
                <col style="width:130px" />
                <col style="width:130px" />
                <col style="width:130px" />
            </colgroup>
            <thead>
                <tr>
                    <th><b>페이지명</b></th>
                    <th><b>경로</b></th>
                    <th><b>사용여부</b></th>
                    <th><b>최종수정일</b></th>
                    <th><b>권한설정</b></th>
                    <th>순서변경</th>
                </tr>
            </thead>
		</table>
        <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Always">
            <ContentTemplate>
		<table>
            <colgroup>
                <col />
                <col style="width:300px" />
                <col style="width:130px" />
                <col style="width:130px" />
                <col style="width:130px" />
                <col style="width:130px" />
            </colgroup>
			<tbody>
                <asp:Repeater ID="rptMenuList" runat="server" OnItemCommand="rptMenuList_ItemCommand" OnItemDataBound="rptMenuList_ItemDataBound">
                    <ItemTemplate>
                        <tr>
                            <td>
                                <asp:LinkButton runat="server" ID="lnkGroupNm" CommandName="Select" CommandArgument='<%# string.Format("{0},{1},{2},{3},{4},{5},{6}", nEval("GROUP_CD"), nEval("MENU_CD"), nEval("MENU_NM"), nEval("MENU_URL"), nEval("USE_YN"), nEval("PARENT_CD"), nEval("USE_PARENT_CD")  ) %>'>
                                </asp:LinkButton>
                            </td>
                            <td>&nbsp;<%#nEval("MENU_URL")%></td>
                            <td style="text-align:center;"><%#nEval("USE_YN_TEXT")%></td>
                            <td><%#nEval("UPDATE_DATE")%></td>
                            <td>
                                <asp:LinkButton runat="server" ID="lnkGroupAuth" CommandName="authSelect" CommandArgument='<%# string.Format("{0},{1}", nEval("GROUP_CD"),nEval("MENU_CD")) %>'>
                                    권한설정
                                </asp:LinkButton>
                            </td>
                            <td style="text-align:center;">
                                <asp:LinkButton runat="server" ID="lnkPageSortUp" CommandName="PageSortUp" CommandArgument='<%# string.Format("{0},{1}", nEval("GROUP_CD"),nEval("MENU_CD")) %>'>▲</asp:LinkButton>
                                <asp:LinkButton runat="server" ID="lnkPageSortDown" CommandName="PageSortDown" CommandArgument='<%# string.Format("{0},{1}", nEval("GROUP_CD"),nEval("MENU_CD")) %>'>▼</asp:LinkButton>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                <tr class="pager">
                    <td colspan="6">
                        <uc2:ucPaging ID="ucPaging2" OnSelEvent="ucPaging_SelEvent2" runat="server" />
                    </td>
                </tr>
			</tbody>						
		</table>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnSearch2" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
	</article>


        <!------------------------------------------------- 상단, 하단 리스트 구분 ------------------------------------------------->


    <!------------------------------------------------- 권한설정 리스트 구분 ------------------------------------------------->
    <br /><br /><br />
    <!-- 조회조건 -->
    <article id="search">
        <h2 class="tit">권한설정</h2>
	    <div class="t_r">
            <asp:Button ID="BtnSearch3" runat="server" CssClass="btn btn_inquiry" OnClick="btnSearch3_Click" Text="조회" Visible="false" />&nbsp;
            <asp:Button ID="BtnInsert3" runat="server" CssClass="btn btn_default" OnClick="btnInsert3_Click" Text="저장" OnClientClick="return jsfn_saveAuth();" />
	    </div>
    </article>
    <!-- 조회조건 End -->

    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <asp:Label ID="lblCount3" BorderStyle="None" BorderColor="0" runat="server"></asp:Label>&nbsp;&nbsp;
            <asp:HiddenField ID="hdnUSER_AUTH_CD" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>

	<article id="grid">
		<!--<h2 class="tit">서브 타이틀</h2>-->
		<table>
            <colgroup>
                <col style="width:300px" />
                <col style="width:100px" />
                <col style="width:100px" />
                <col style="width:100px" />
                <col style="width:100px" />
                <col style="width:100px" />
                <col />
            </colgroup>
            <thead>
                <tr>
                    <th><b>사용자그룹</b></th>
                    <th><b>사용여부</b></th>
                    <th><b>조회</b></th>
                    <th><b>저장</b></th>
                    <th><b>레포트</b></th>
                    <th><b>승인</b></th>
                    <th><b>최종수정일</b></th>
                </tr>
            </thead>
		</table>
        <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Always">
            <ContentTemplate>
		<table>
            <colgroup>
                <col style="width:300px" />
                <col style="width:100px" />
                <col style="width:100px" />
                <col style="width:100px" />
                <col style="width:100px" />
                <col style="width:100px" />
                <col />
            </colgroup>
			<tbody>
                <asp:Repeater ID="rptAuthList" runat="server" OnItemCommand="rptAuthList_ItemCommand">
                    <ItemTemplate>
                        <tr>
                            <td><%#nEval("code_desc")%>
                                <asp:TextBox ID="user_group" runat="server" Text='<%#nEval("user_group")%>' Visible="false"></asp:TextBox>
                            </td>
                            <td style="text-align:center">
                                <asp:CheckBox ID="chk_grant_v" runat="server" Checked='<%#nEval("grant_v").ToString() == "1" %>' />
                            </td>
                            <td style="text-align:center">
                                <asp:CheckBox ID="chk_grant_i" runat="server" Checked='<%#nEval("grant_i").ToString() == "1" %>' />
                            </td>
                            <td style="text-align:center">
                                 <asp:CheckBox ID="chk_grant_s" runat="server" Checked='<%#nEval("grant_s").ToString() == "1" %>' />
                            </td>
                            <td style="text-align:center">
                                <asp:CheckBox ID="chk_grant_r" runat="server" Checked='<%#nEval("grant_r").ToString() == "1" %>' />
                            </td>
                            <td style="text-align:center">
                                <asp:CheckBox ID="chk_grant_a" runat="server" Checked='<%#nEval("grant_a").ToString() == "1" %>' />
                            </td>
                            <td><%#nEval("update_date")%></td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                <tr class="pager">
                    <td colspan="7">
                        <uc3:ucPaging ID="ucPaging3" OnSelEvent="ucPaging_SelEvent3" runat="server" />
                    </td>
                </tr>
			</tbody>
		</table>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnSearch3" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
	</article>
    <!------------------------------------------------- 권한설정 리스트 구분 ------------------------------------------------->





    <!--  모달패널 -->




    <!-- UpdataPanel 대메뉴 Start -->
    <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <!-- fake button -->
            <asp:Button ID="btnShowPopup" runat="server" Style="display: none" />
            <!-- ModalPopupExtender -->
            <ajaxToolkit:ModalPopupExtender ID="ModalPopExt1" runat="server"
                PopupControlID="EditPanel"
                TargetControlID="btnShowPopup"
                CancelControlID="btnCancel">
            </ajaxToolkit:ModalPopupExtender>
            <!-- PopupPanel -->
            <asp:Panel ID="EditPanel" runat="server" CssClass="modal_bg" Style="display:none;">
                <div class="modal_pop">
                    <div class="pop_title">메뉴 등록</div>
                    <br />

                    <table class="pop_list">
                        <colgroup>
                            <col style="width:100px" />
                            <col />
                        </colgroup>
                        <tbody>
                            <tr>
                                <td>메뉴명</td>
                                <td>
                                    <asp:TextBox ID="txtPopGroupNm" runat="server" Width="200px" CssClass="input" Style="text-align: left;" />
                                </td>
                            </tr>
                            <tr>
                                <td>메뉴권한</td>
                                <td>
                                    <asp:DropDownList ID="ddlPopUserAuthCd" runat="server"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>사용여부</td>
                                <td>
                                    <asp:DropDownList ID="ddlPopUseYn" runat="server">
                                        <asp:ListItem Text="사용" Value="Y" />
                                        <asp:ListItem Text="미사용" Value="N" />
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </tbody>
                    </table>

                    <div class="pop_bottom">
                        <asp:Button ID="btnAdd" runat="server" Text="저장" CssClass="btn btn_default" OnClientClick="return GetMenuGroupIsValidation();" OnClick="btnAdd_Click" />
                        <asp:Button ID="btnDelete" runat="server" Text="삭제" CssClass="btn btn_default" OnClientClick="return GetMenuGroupDeleteConfirm();" OnClick="btnDelete_Click" />
                        <asp:Button ID="btnCancel" runat="server" Text="닫기" CssClass="btn btn_default" OnClick="btnExit_Click" />
                        <asp:HiddenField ID="hdnMainGroupAddType" runat="server" />
                        <asp:HiddenField ID="hdnMainGroupCd" runat="server" />
                    </div>
                </div>
            </asp:Panel>
            <!-- popup end -->
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="rptMenuGroupList" EventName="ItemCommand" />
        </Triggers>
    </asp:UpdatePanel>
    <!-- UpdataPanel 대메뉴 End -->


    <!-- UpdataPanel 하단메뉴 Start -->
    <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <!-- fake button -->
            <asp:Button ID="btnShowPopup2" runat="server" Style="display: none" />
            <!-- ModalPopupExtender -->
            <ajaxToolkit:ModalPopupExtender ID="ModalPopExt2" runat="server"
                PopupControlID="EditPanel2"
                TargetControlID="btnShowPopup2"
                CancelControlID="btnCancel2">
            </ajaxToolkit:ModalPopupExtender>
            <!-- PopupPanel -->
            <asp:Panel ID="EditPanel2" runat="server" CssClass="modal_bg" Style="display: none;">
                <div class="modal_pop">
                    <div class="pop_title">페이지 등록</div>
                    <br />

                    <table class="pop_list">
                        <colgroup>
                            <col style="width:100px" />
                            <col />
                        </colgroup>
                        <tr>
                            <td>메뉴명</td>
                            <td>
                                <asp:TextBox ID="txtPopMenuNm" runat="server" Width="200px" CssClass="input" Style="text-align: left;" />
                            </td>
                        </tr>
                        <tr>
                            <td>메뉴경로</td>
                            <td>
                                <asp:TextBox ID="txtPopMenuUrl" runat="server" Width="200px" CssClass="input" Style="text-align: left;" />
                            </td>
                        </tr>
                        <tr>
                            <td>부모메뉴사용</td>
                            <td>
                                <asp:DropDownList ID="ddlUseParentMenu" runat="server"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>사용여부</td>
                            <td>
                                <asp:DropDownList ID="ddlPopMenuUseYn" runat="server">
                                    <asp:ListItem Text="사용" Value="Y" />
                                    <asp:ListItem Text="미사용" Value="N" />
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>

                    <div class="pop_bottom">
                        <asp:Button ID="btnAdd2" runat="server" Text="저장" CssClass="btn btn_default" OnClientClick="return GetMenuIsValidation();" OnClick="btnAdd2_Click" />
                        <asp:Button ID="btnDelete2" runat="server" Text="삭제" CssClass="btn btn_default" OnClientClick="return GetMenuDeleteConfirm();" OnClick="btnDelete2_Click" />
                        <asp:Button ID="btnCancel2" runat="server" Text="닫기" CssClass="btn btn_default" OnClick="btnExit2_Click" />
                        <asp:HiddenField ID="hdnMenuAddType2" runat="server" />
                    </div>
                </div>
            </asp:Panel>
            <!-- popup end -->
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="rptMenuList" EventName="ItemCommand" />
        </Triggers>
    </asp:UpdatePanel>
    <!-- UpdataPanel 하단메뉴 End -->
</asp:Content>