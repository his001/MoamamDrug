<%@ Page Language="C#" MasterPageFile="~/Master/MasterPage_Panel.master" AutoEventWireup="true" CodeFile="UserList.aspx.cs" Inherits="Site_Management_UserList" %>

<%@ Register Src="~/UserControls/ucPaging.ascx" TagName="ucPaging" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHead" runat="Server">
    <script type="text/javascript">
        
        //조회버튼 클릭
        function FindConfirm()
        {
            var txtUserId   = $("#<%= txtUserId.ClientID %>").val();
            var txtUserName = $("#<%= txtUserName.ClientID %>").val();

            if (txtUserId.length == 0 && txtUserName.length == 0)
            {
                alert("조회조건을 입력하세요");
                return false;
            }

            return true;
        }


        //팝업 저장버튼 클릭
        function IsValidation()
        {
            if(confirm("저장하시겠습니까?"))
            {
                return true;
            }

            return false;
        }

        //팝업 삭제버튼 클릭
        function DeleteConfirm()
        {
            if(confirm("삭제하시겠습니까?"))
            {
                return true;
            }

            return false;
        }


        //홈플러스 아이디조회 팝업호출
        function GetPopupTescoUser()
        {
            window.open("../PopupPage/TescoUserSearch.aspx", "_blank", "width=400,height=400,toolbar=no,location=no,directories=no,resizable=no,scrollbars=no");
            return false;
        }


        //레이어팝업창 사용자정보 설정
        function GetUserParent(id, name)
        {
            $("#<%= txtPopUserId.ClientID %>").val(id);
            $("#<%= txtPopUserName.ClientID %>").val(name);
            $("#<%= ddlPopUserAuth.ClientID %>").focus();
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMain" runat="Server">
    <!-- 조회조건 Start -->
    <article id="search">
	    <div class="t_r">
			<hpf:NButton ID="btnSearch" runat="server" CssClass="btn btn_inquiry" OnClick="btnSearch_Click" Text="조회" SecurityType="Inquiry" CmdType="SELECT" />&nbsp;
			<asp:Button ID="btnInsert" runat="server" CssClass="btn btn_default" Text="신규" onclick="btnInsert_Click" />
	    </div>
	    <div class="search row1"><!-- 한 줄: row1, 두 줄: row2, 세 줄: row3 -->
		    <div>
			    <label for="">사용자 아이디</label>
				<asp:TextBox ID="txtUserId" runat="server" CssClass="input" />
			    <label for="">사용자명</label>
				<asp:TextBox ID="txtUserName" runat="server" CssClass="input" MaxLength="30" />
			    <label for="">권한</label>
				<asp:DropDownList ID="ddlUserAuth" runat="server"></asp:DropDownList>
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
                <col style="width:200px" />
                <col style="width:200px" />
                <col style="width:200px" />
                <col style="width:200px" />
                <col style="width:200px" />
            </colgroup>
			<thead>
                <tr>
                    <th>사용자 아이디</th>
                    <th>사용자명</th>
                    <th>권한</th>
                    <th>사용여부</th>
                    <th>등록일</th>
                </tr>
			</thead>
        </table>
		<table>
            <colgroup>
				<col style="width:200px" />
				<col style="width:200px" />
				<col style="width:200px" />
				<col style="width:200px" />
				<col style="width:200px" />
            </colgroup>
			<tbody>
                <asp:Repeater ID="rptUserList" runat="server" OnItemCommand="rptUserList_ItemCommand" >
                    <ItemTemplate>
                        <tr>
                            <td class="t_c">
                                <asp:LinkButton runat="server" ID="lnkUserId" CommandName="select" CommandArgument='<%# string.Format("{0},{1},{2},{3}", nEval("USER_ID"), nEval("USER_NAME"), nEval("SUB_CD"), nEval("USE_YN")) %>'>
                                    <%#nEval("USER_ID")%>
                                </asp:LinkButton>
                            </td>
                            <td class="t_c"><%#nEval("USER_NAME")%></td>
                            <td class="t_c"><%#nEval("SUB_CD_NM")%></td>
                            <td class="t_c"><%#nEval("USE_YN_TEXT")%></td>
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
    <asp:Button ID="btnShowPopup" runat="server" Style="display: none" />
    <ajaxToolkit:ModalPopupExtender ID="ModalPopExt1" runat="server"
        BackgroundCssClass="modalBackground"
        PopupControlID="EditPanel"
        TargetControlID="btnShowPopup"
        CancelControlID="btnCancel"
        PopupDragHandleControlID="HeaderPanel">
    </ajaxToolkit:ModalPopupExtender>
    <!-- PopupPanel -->
    <asp:Panel ID="EditPanel" runat="server" CssClass="modalPopup" Style="display: none;" Width="320px" Height="230px">
        <asp:Panel ID="HeaderPanel" runat="server" CssClass="PopupPanel">
            <div>
                <table class="PopupTabelTitle">
                    <tr>
                        <td class="Popup_td">&nbsp;사용자 등록</td>
                    </tr>
                </table>
            </div>
        </asp:Panel>
        <br />
        <div style="width: 100%; text-align: center;">
            <table style="text-align:left;width:100%;background-color:#cccccc;padding:5px;">
                <tr style="background-color: #ffffff;">
                    <th style="background-color: #efefef; width: 70px">사용자아이디</th>
                    <td style="width: 200px">
                        <asp:TextBox ID="txtPopUserId" runat="server" Width="150px" CssClass="input" Style="text-align: left;" Enabled="false" />
                        <asp:Button ID="btnTescoAuthSearch" runat="server" CssClass="button_style" Text="찾기" OnClientClick="return GetPopupTescoUser();" />
                    </td>
                </tr>
                <tr style="background-color: #ffffff;">
                    <th style="background-color: #efefef; width: 80px">사용자명</th>
                    <td style="width: 180px">
                        <asp:TextBox ID="txtPopUserName" runat="server" Width="150px" CssClass="input" Style="text-align: left;" Enabled="false" />
                    </td>
                </tr>
                <tr style="background-color: #ffffff;">
                    <th style="background-color: #efefef;">권한</th>
                    <td>
                        <asp:DropDownList ID="ddlPopUserAuth" runat="server"></asp:DropDownList>
                    </td>
                </tr>
                <tr style="background-color: #ffffff;">
                    <th style="background-color: #efefef;">사용여부</th>
                    <td>
                        <asp:DropDownList ID="ddlPopUseYn" runat="server">
                            <asp:ListItem Text="사용" Value="Y" />
                            <asp:ListItem Text="미사용" Value="N" />
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
        </div>
        <div style="width: 100%; text-align: center;">
            <br />
            <hr />
            <hpf:NButton ID="btnAdd" runat="server" Text="저장" CssClass="btn btn_default" OnClientClick="return IsValidation();" OnClick="btnAdd_Click" SecurityType="Save" CmdType="INSERT" />
            <hpf:NButton ID="btnDelete" runat="server" Text="삭제" CssClass="btn btn_default" OnClientClick="return DeleteConfirm();" OnClick="btnDelete_Click" SecurityType="Save" CmdType="DELETE" />
            <asp:Button ID="btnCancel" runat="server" Text="닫기" CssClass="btn btn_default" OnClick="btnExit_Click" />
            <asp:HiddenField ID="hdnAddType" runat="server" />
        </div>
    </asp:Panel>
    <!-- popup end -->

</asp:Content>