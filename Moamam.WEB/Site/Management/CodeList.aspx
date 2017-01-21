<%@ Page Language="C#" MasterPageFile="~/Master/MasterPage_Panel.master" AutoEventWireup="true" CodeFile="CodeList.aspx.cs" Inherits="Site_Management_CodeList" %>

<%@ Register Src="~/UserControls/ucPaging.ascx" TagName="ucPaging" TagPrefix="uc1" %>
<%@ Register Src="~/UserControls/ucPaging.ascx" TagName="ucPaging" TagPrefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHead" runat="Server">
    <script type="text/javascript">
        
        //마스터코드 등록 버튼
        function GetGroupCdIsValidation()
        {
            var txtPopCODE_DESC = document.getElementById("<%=txtPopCODE_DESC.ClientID%>");

            if (txtPopCODE_DESC.value == "")
            {
                alert("마스터코드명을 입력하세요.");
                txtPopCODE_DESC.focus();
                return false;
            }

            if(confirm("등록하시겠습니까?"))
            {
                return true;
            }

            return false;
        }


        //마스터코드 삭제 버튼
        function GetGroupCdDeleteConfirm()
        {
            if(confirm("삭제하시겠습니까?"))
            {
                return true;
            }

            return false;
        }


        //서브코드 등록 버튼
        function GetCheckParentGroupCd()
        {
            var hdnParentCODE_GROUP = document.getElementById("<%=hdnParentCODE_GROUP.ClientID%>");

            if (hdnParentCODE_GROUP.value == "")
            {
                alert("마스터코드를 선택하세요.");
                return false;
            }

            return true;
        }


        //서브코드 저장 버튼
        function SetSubCdIsValidation()
        {
            var txtPopSub_CODE_ID       = document.getElementById("<%=txtPopSub_CODE_ID.ClientID%>");
            var txtPopSub_CODE_DESC     = document.getElementById("<%=txtPopSub_CODE_DESC.ClientID%>");
            var txtPopSub_CODE_REF_1    = document.getElementById("<%=txtPopSub_CODE_REF_1.ClientID%>");
            var txtPopSub_CODE_REF_3    = document.getElementById("<%=txtPopSub_CODE_REF_3.ClientID%>");

            if (txtPopSub_CODE_ID.value == "")
            {
                alert("서브코드를 입력하세요.");
                txtPopSub_CODE_ID.focus();
                return false;
            }
            if (txtPopSub_CODE_DESC.value == "")
            {
                alert("서브코드명을 입력하세요.");
                txtPopSub_CODE_DESC.focus();
                return false;
            }
            if (txtPopSub_CODE_REF_1.value == "" && txtPopSub_CODE_REF_3.value == "")
            {
                alert("참조코드1 또는 3번 중 1항목은 필수입력란입니다.");
                txtPopSub_CODE_REF_1.focus();
                return false;
            }

            if(confirm("저장하시겠습니까?"))
            {
                return true;
            }

            return false;
        }


        //서브코드 삭제 버튼
        function GetSubCdDeleteConfirm()
        {
            if(confirm("삭제하시겠습니까?"))
            {
                return true;
            }

            return false;
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////
        //                        숫자만 입력가능                                                       //    
        ///////////////////////////////////////////////////////////////////////////////////////////////
        function OnCheckNumeric(obj) {
            normalize = /\D/g;

            if (obj.value != "") {
                if (normalize.test(obj.value)) {
                    obj.value = obj.value.replace(/\D/g, "");
                }
            }
        }


        function GetNumberHipun(check_num) {
            var inText = check_num.value;
            var ret;
            
            var pattern = /^[+-]?\d*(\.?\d*)$/;

            for (var i = 0; i < inText.length; i++) {
                ret = inText.charCodeAt(i);
                if (ret != 45) {
                    if ((ret < 48) || (ret > 57)) {
                        //alert("숫자 또는 하이푼만 입력하시기 바랍니다.");
                        check_num.value = check_num.value.replace(/\D/g, "");
                        check_num.focus();
                        return false;
                    }
                }
            }
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMain" runat="Server">

    <!-- 조회조건 Start -->
    <article id="search">
	    <div class="t_r">
            <hpf:NButton ID="btnSearch" runat="server" CssClass="btn btn_inquiry" OnClick="btnSearch_Click" Text="조회" Visible="false" SecurityType="Inquiry" CmdType="SELECT" />&nbsp;
            <hpf:NButton ID="btnInsert" runat="server" CssClass="btn btn_default" OnClick="btnInsert_Click" Text="등록" SecurityType="NotSet" />
	    </div>
    </article>
    <!-- 조회조건 End -->


    <br />
	<article id="grid">
		<!--<h2 class="tit">서브 타이틀</h2>-->
		<table>
            <colgroup>
                <col style="width:100px" />               
                <col style="width:100px" />
                <col />
                <col style="width:180px" />
            </colgroup>
			<thead>
                <tr>
                    <th>그룹코드</th>
                    <th>코드ID</th>
                    <th>코드명</th>
                    <th>하위메뉴설정</th>
                </tr>
			</thead>
		</table>
		<table>
            <colgroup>
                <col style="width:100px" />               
                <col style="width:100px" />
                <col />
                <col style="width:180px" />
            </colgroup>
			<tbody>
                <asp:Repeater ID="rptCodeGroupList" runat="server" OnItemCommand="rptCodeGroupList_ItemCommand" >
                    <ItemTemplate>
                        <tr>
                            <td class="t_c"><%#nEval("CODE_GROUP")%></td>
                            <td class="t_c"><%#nEval("CODE_ID")%></td>
                            <td>
                                <asp:LinkButton runat="server" ID="lnkGroupCdNm" CommandName="groupCdSelect" CommandArgument='<%# string.Format("{0},{1},{2}", nEval("CODE_GROUP"), nEval("CODE_ID"), nEval("CODE_DESC")) %>'>
                                    &nbsp;<%#nEval("CODE_DESC")%>
                                </asp:LinkButton>
                            </td>                            
                            <td class="t_c">
                                <asp:LinkButton runat="server" ID="lnkGroupCd" CommandName="subCodeSelect" CommandArgument='<%# nEval("CODE_ID") %>'>
                                    하위코드설정
                                </asp:LinkButton>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                <tr class="pager">
                    <td colspan="4">
                        <uc1:ucPaging ID="ucPaging" OnSelEvent="ucPaging_SelEvent" runat="server" />
                    </td>
                </tr>
			</tbody>						
		</table>
    </article>
    <!-- List End -->


    <!------------------------------------------------- 상단, 하단 리스트 구분 ------------------------------------------------->

    <br />
    <!-- 조회조건 Start -->
    <asp:HiddenField ID="hdnParentCODE_GROUP" runat="server" />
    <article id="search">
	    <div class="t_r">
            <hpf:NButton ID="BtnSearch2" runat="server" CssClass="btn btn_inquiry" OnClick="btnSearch2_Click" Text="조회" Visible="false" SecurityType="Inquiry" CmdType="SELECT" />&nbsp;
            <hpf:NButton ID="BtnInsert2" runat="server" CssClass="btn btn_default" OnClick="btnInsert2_Click" Text="등록" OnClientClick="return GetCheckParentGroupCd();" SecurityType="NotSet" />
	    </div>
    </article>
    <!-- 조회조건 End -->
    <br />
    <!-- List Start -->
	<article id="grid">
		<!--<h2 class="tit">서브 타이틀</h2>-->
		<table>
            <colgroup>
                <col style="width:100px" />
                <col style="width:100px" />
                <col />
                <col style="width:180px" />
                <col style="width:180px" />
                <col style="width:180px" />
                <col style="width:180px" />
            </colgroup>
			<thead>
                <tr>
                    <th>그룹코드</th>
                    <th>서브코드</th>
                    <th>서브코드명</th>
                    <th>참조코드1</th>
                    <th>참조코드2</th>
                    <th>참조코드3</th>
                    <th>참조코드4</th>
                </tr>
			</thead>
		</table>
		<table>
            <colgroup>
                <col style="width:100px" />
                <col style="width:100px" />
                <col />
                <col style="width:180px" />
                <col style="width:180px" />
                <col style="width:180px" />
                <col style="width:180px" />
            </colgroup>
			<tbody>
                <asp:Repeater ID="rptCodeSubList" runat="server" OnItemCommand="rptCodeSubList_ItemCommand">
                    <ItemTemplate>
                        <tr>
                            <td><%#nEval("CODE_GROUP")%></td>
                            <td><%#nEval("CODE_ID")%></td>
                            <td>
                                <asp:LinkButton runat="server" ID="lnkGroupCdNm" CommandName="Select" CommandArgument='<%# string.Format("{0},{1},{2},{3},{4},{5},{6}", nEval("CODE_GROUP"), nEval("CODE_ID"), nEval("CODE_DESC"), nEval("CODE_REF_1"), nEval("CODE_REF_2"), nEval("CODE_REF_3"), nEval("CODE_REF_4")) %>'>
                                    &nbsp;<%#nEval("CODE_DESC")%>
                                </asp:LinkButton>
                            </td>
                            <td><%#nEval("CODE_REF_1")%></td>
                            <td><%#nEval("CODE_REF_2")%></td>
                            <td><%#nEval("CODE_REF_3")%></td>
                            <td><%#nEval("CODE_REF_4")%></td>
                         </tr>
                    </ItemTemplate>
                </asp:Repeater>
                <tr class="pager">
                    <td colspan="7">
                        <uc2:ucPaging ID="ucPaging2" OnSelEvent="ucPaging_SelEvent2" runat="server" />
                    </td>
                </tr>
			</tbody>						
		</table>
    </article>
    <!-- List End -->




    <!--  모달패널 -->




    <!-- UpdataPanel 대메뉴 Start -->
    <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <!-- fake button -->
            <asp:Button ID="btnShowPopup" runat="server" Style="display: none" />
            <!-- ModalPopupExtender -->
            <ajaxToolkit:ModalPopupExtender ID="ModalPopExt1" runat="server"
                BackgroundCssClass="modalBackground"
                PopupControlID="EditPanel"
                TargetControlID="btnShowPopup"
                CancelControlID="btnCancel"
                PopupDragHandleControlID="HeaderPanel">
            </ajaxToolkit:ModalPopupExtender>
            <!-- PopupPanel -->
            <asp:Panel ID="EditPanel" runat="server" CssClass="modalPopup" Style="display: none;" Width="300px" Height="190px">
                <asp:Panel ID="HeaderPanel" runat="server" CssClass="PopupPanel">
                    <div>
                        <table class="PopupTabelTitle">
                            <tr>
                                <td class="Popup_td">&nbsp;마스터코드 등록</td>
                            </tr>
                        </table>
                    </div>
                </asp:Panel>
                <br />
                <div style="width: 100%; text-align: center;">
                    <table style="text-align: left; width: 100%; background-color: #cccccc;">
                        <tr style="background-color: #ffffff;">
                            <th style="background-color: #efefef; width: 70px">그룹코드</th>
                            <td style="width: 200px">                                
                                <asp:TextBox ID="txtPopCODE_GROUP" runat="server" Width="200px" CssClass="input" style="text-align:left;" onkeyup="OnCheckNumeric(this)"></asp:TextBox>
                            </td>
                        </tr>
                        <tr style="background-color: #ffffff;">
                            <th style="background-color: #efefef; width: 70px">코드ID</th>
                            <td style="width: 200px">                                
                                <asp:TextBox ID="txtPopCODE_ID" runat="server" Width="200px" CssClass="input" style="text-align:left;" onkeyup="OnCheckNumeric(this)"></asp:TextBox>
                            </td>
                        </tr>
                        <tr style="background-color: #ffffff;">
                            <th style="background-color: #efefef; width: 70px">코드명</th>
                            <td style="width: 200px">
                                <asp:TextBox ID="txtPopCODE_DESC" runat="server" Width="200px" CssClass="input" Style="text-align: left;" />
                            </td>
                        </tr>
                    </table>
                </div>
                <div style="width: 100%; text-align: center;">
                    <br />
                    <hr />
                    <hpf:NButton ID="btnAdd" runat="server" Text="저장" CssClass="btn btn_default" OnClientClick="return GetGroupCdIsValidation();" OnClick="btnAdd_Click" SecurityType="Save" CmdType="UPDATE" />
                    <hpf:NButton ID="btnDelete" runat="server" Text="삭제" CssClass="btn btn_default" OnClientClick="return GetGroupCdDeleteConfirm();" OnClick="btnDelete_Click" SecurityType="Save" CmdType="DELETE" />
                    <hpf:NButton ID="btnCancel" runat="server" Text="닫기" CssClass="btn btn_default" OnClick="btnExit_Click" />
                    <asp:HiddenField ID="hdnGroupCdAddType" runat="server" />
                    <asp:HiddenField ID="hdnCODE_GROUP" runat="server" />
                    <asp:HiddenField ID="hdnCODE_ID" runat="server" />
                </div>
            </asp:Panel>
            <!-- popup end -->
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="rptCodeGroupList" EventName="ItemCommand" />
        </Triggers>
    </asp:UpdatePanel>
    <!-- UpdataPanel 대메뉴 End -->



    <!-- UpdataPanel 하단메뉴 Start -->
    <asp:UpdatePanel ID="UpdatePanel8" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <!-- fake button -->
            <asp:Button ID="btnShowPopup2" runat="server" Style="display: none" />
            <!-- ModalPopupExtender -->
            <ajaxToolkit:ModalPopupExtender ID="ModalPopExt2" runat="server"
                BackgroundCssClass="modalBackground"
                PopupControlID="EditPanel2"
                TargetControlID="btnShowPopup2"
                CancelControlID="btnCancel2"
                PopupDragHandleControlID="HeaderPanel">
            </ajaxToolkit:ModalPopupExtender>
            <!-- PopupPanel -->
            <asp:Panel ID="EditPanel2" runat="server" CssClass="modalPopup" Style="display: none;" Width="300px" Height="280px">
                <asp:Panel ID="Panel2" runat="server" CssClass="PopupPanel">
                    <div>
                        <table class="PopupTabelTitle">
                            <tr>
                                <td class="Popup_td">&nbsp;하위메뉴 등록</td>
                            </tr>
                        </table>
                    </div>
                </asp:Panel>
                <br />
                <div style="width: 100%; text-align: center;">
                    <table style="text-align: left; width: 100%; background-color: #cccccc;">
                        <tr style="background-color: #ffffff;">
                            <th style="background-color: #efefef; width: 70px">서브코드</th>
                            <td style="width: 200px">                                
                                <asp:TextBox ID="txtPopSub_CODE_ID" runat="server" CssClass="input" style="text-align:center;width:200px;" MaxLength="6" onkeyup="OnCheckNumeric(this)"></asp:TextBox>
                            </td>
                        </tr>
                        <tr style="background-color: #ffffff;">
                            <th style="background-color: #efefef; width: 70px">서브코드명</th>
                            <td style="width: 200px">
                                <asp:TextBox ID="txtPopSub_CODE_DESC" runat="server" CssClass="input" Style="text-align:left;width:200px;" />
                            </td>
                        </tr>
                        <tr style="background-color: #ffffff;">
                            <th style="background-color: #efefef; width: 70px">참조코드1</th>
                            <td style="width: 200px">
                                <asp:TextBox ID="txtPopSub_CODE_REF_1" runat="server" CssClass="input" style="text-align:left;width:200px;" MaxLength="7" onkeyup="return GetNumberHipun(this);"></asp:TextBox>
                            </td>
                        </tr>
                        <tr style="background-color: #ffffff;">
                            <th style="background-color: #efefef; width: 70px">참조코드2</th>
                            <td style="width: 200px">
                                <asp:TextBox ID="txtPopSub_CODE_REF_2" runat="server" CssClass="input" style="text-align:left;width:200px;" MaxLength="7" onkeyup="return GetNumberHipun(this);"></asp:TextBox>
                            </td>
                        </tr>
                        <tr style="background-color: #ffffff;">
                            <th style="background-color: #efefef; width: 70px">참조코드3</th>
                            <td style="width: 200px">
                                <asp:TextBox ID="txtPopSub_CODE_REF_3" runat="server" CssClass="input" Style="text-align:left;width:200px" />
                            </td>
                        </tr>
                        <tr style="background-color: #ffffff;">
                            <th style="background-color: #efefef; width: 70px">참조코드4</th>
                            <td style="width: 200px">
                                <asp:TextBox ID="txtPopSub_CODE_REF_4" runat="server" Width="200px" CssClass="input" Style="text-align: left;" />
                            </td>
                        </tr>
                    </table>
                </div>
                <div style="width: 100%; text-align: center;">
                    <br />
                    <hr />
                    <hpf:NButton ID="btnAdd2" runat="server" Text="저장" CssClass="btn btn_default" OnClientClick="return SetSubCdIsValidation();" OnClick="btnAdd2_Click" SecurityType="Save" CmdType="UPDATE" />
                    <hpf:NButton ID="btnDelete2" runat="server" Text="삭제" CssClass="btn btn_default" OnClientClick="return GetSubCdDeleteConfirm();" OnClick="btnDelete2_Click" SecurityType="Save" CmdType="DELETE" />
                    <hpf:NButton ID="btnCancel2" runat="server" Text="닫기" CssClass="btn btn_default" OnClick="btnExit2_Click" />
                    <asp:HiddenField ID="hdnMenuAddType2" runat="server" />
                    <asp:HiddenField ID="hdnSUB_CODE_ID" runat="server" />

                </div>
            </asp:Panel>
            <!-- popup end -->
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="rptCodeSubList" EventName="ItemCommand" />
        </Triggers>
    </asp:UpdatePanel>
    <!-- UpdataPanel 하단메뉴 End -->


</asp:Content>