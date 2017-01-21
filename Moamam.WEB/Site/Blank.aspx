<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterPageTab.master" AutoEventWireup="true" CodeFile="Blank.aspx.cs" Inherits="Site_Blank" %>

<%--<%@ Register Src="~/UserControls/ucPagingJS.ascx" TagName="ucPaging" TagPrefix="uc1" %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentHead" runat="Server">
<!-- 1.LoadPage -->
<script type="text/javascript">
    var __clickCnt = 0;
    $(document).ready(function () {
        //Initialize();
        //jsfn_Search();
    });
</script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentMain" runat="Server">
    <!-- 조회조건 -->
    <article id="search">
               
	    <div class="t_r">

        </div>
        <div class="search row1"><!-- 한 줄: row1, 두 줄: row2, 세 줄: row3 --> 
            <div style="height:420px;">
                <label for=""> 안녕하세요 ^^ ADO 시스템에 로그인 하셨습니다.</label> 
            </div>
	    </div> 
        <div id="LyHdn" style="display:none;"><asp:Button ID="btnSession" runat="server" Text="세션 삭제 테스트" OnClick="btnSession_Click" /></div>
    </article>
    <!-- 조회조건 End -->

    <br />
    

</asp:Content>
