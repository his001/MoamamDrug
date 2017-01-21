<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterPage_Popup.master"  AutoEventWireup="true" CodeFile="ProductItemUpload.aspx.cs" Inherits="Site_PopupPage_ProductItemUpload" %>

<%@ Register src="~/UserControls/ucUploadResult.ascx" tagname="ucUploadResult" tagprefix="uc1" %>
<asp:Content ID="Content3" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript">
        function resizeLocal() {
            _topOffset = _topOffset + $("#condition_container").height(); +$("#report_header").height();
            $("#report_body").height(_docHeight - _topOffset - _footerHeight - 40);
        }

        function VaildationCheck() {
            if ($("#<%=FileUpload1.ClientID%>").val() == "") {
                alert("업로드 할 파일을 선택하세요!");
                return false;
            }
            else if ($("#<%=FileUpload1.ClientID%>").val() != "") {
                var ext = $("#<%=FileUpload1.ClientID%>").val().split('.').pop().toLowerCase();
                if ($.inArray(ext, ['csv']) == -1) {
                    alert("csv 파일만 업로드 할 수 있습니다!");
                    return false;
                }
            }

        ShowLoading();

        return true;
    }

    function ConfirmSave(DataItem) {
        var rowCount = "<%= rptUpload.Items.Count %>";

        if (rowCount == 0) {
            alert("반영할내역이 없습니다!");
            return false;
        }

        return confirm("데이터를 반영하시겠습니까?");
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////
    //            Transfer업로드 양식 CSV 다운로드                                                     //
    ///////////////////////////////////////////////////////////////////////////////////////////////
    function csvDownload() {
        var csvName = encodeURI("상품마스터양식.csv");
        window.location.href = "../../Download.aspx?type=D&file=" + csvName;
    }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <!-- 조회조건 -->
    <article id="search" style="padding-top:0;"> 
        <div class="search row2"><!-- 한 줄: row1, 두 줄: row2, 세 줄: row3  class="t_r"-->   
	        <div>
			    <label for="">CSV 파일선택</label> 
                <asp:FileUpload ID="FileUpload1" runat="server" Width="30%"  />  &nbsp;
                <hpf:NButton ID="btnUpload" runat="server" CssClass="btn btn_default" OnClick="btnUpload_Click" Text="정합성체크 시작" SecurityType="Save" CmdType="MASSUPLOAD"  OnClientClick="return VaildationCheck();" DisabledCss="GlodBtnNone"/>
                <hpf:NButton ID="btnSave" runat="server" CssClass="btn btn_default" OnClick="btnSave_Click" Text="데이터 반영" SecurityType="Save" CmdType="MASSUPDATE"  OnClientClick="return ConfirmSave();" DisabledCss="GlodBtnNone"/>
                <input type="button" class="btn btn_default"  style="cursor:pointer;" value="업로드 양식다운로드" onclick="csvDownload();" />&nbsp;
            </div>
            <label for="">※ 파일확장자는 CSV 입니다. ※ 업로드된 파일내용은 에러건만 보여줍니다.(최대 100건)</label> 
            <asp:Label ID="lblCount" BorderStyle="None" BorderColor="0" runat="server"></asp:Label>
	    </div>  
    </article>
    <br/>
    <!-- 조회조건 End -->





    <!-- List Start -->

    <article id="grid">
        <asp:HiddenField ID="hdnDataExists" runat="server" /> 
		<div id="Ly_grid1Scroll" style="width:100%;text-align:center;height:100%;min-width:900px;min-height:480px;overflow-x:auto;overflow-y:auto;">
        <table>
        <colgroup>
            <col style="width:80px" />
            <col style="width:80px" />
            <col style="width:80px" />
            <col style="width:200px" />
            <col style="width:300px" />
            <col style="width:50px" />
        </colgroup>
        <tr class="tr_h31">
            <td class="t_c"><b>아이템</b></td>
            <td class="t_c"><b>Dc코드</b></td>
            <td class="t_c"><b>발주그룹</b></td>
            <td class="t_c"><b>아이템명</b></td>
            <td class="t_c"><b>에러Message</b></td>
            <td class="t_c"><b>TYPE</b></td>
        </tr>
        
        <asp:Repeater ID="rptUpload" runat="server" onitemdatabound="rptUpload_ItemDataBound">
            <HeaderTemplate></HeaderTemplate>
            <ItemTemplate>
            <tr id="hidTr" runat="server">
                <td class="t_c"><%#nEval("ITEM")%></td>
                <td class="t_c"><%#this.MakeHtmlInfo(nEval("WH").ToString(),"CINT")%></td>
                <td class="t_c"><%#nEval("ORDER_GROUP")%></td>
                <td style="text-align:left;padding-left:5px;"><%#nEval("ITEM_NAME")%></td>
                <td style="text-align:left;padding-left:5px;">
                    <asp:Label ID="lblerrorMessage" runat="server" Text='<%#nEval("ERROR_MESSAGE")%>'></asp:Label>
                    <input type="hidden" id="hidStatus" runat="server" value='<%#nEval("STATUS")%>' />
                </td>
                <td class="t_c"><%#nEval("ACTION_TYPE")%></td>
            </tr>
            </ItemTemplate> 
            <FooterTemplate>
            <tr>
                <td colspan="20" style="width:100%;height:25px;text-align:center;">
                    <asp:Label ID="DataExists" runat="server" ForeColor="Red" Font-Size="13px" Font-Bold="true"><%= hdnDataExists.Value %></asp:Label>
                </td>
            </tr>
            </FooterTemplate>
        </asp:Repeater>
            <%--<tr><td class="t_c" colspan="5" style="height:5px;"></td></tr>--%>
            </table>
        </div>
    </article>
    
    <%--<p style="height:400px;"/>--%>
     
</asp:Content>