<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucUploadCsv.ascx.cs" Inherits="UserControls_ucUploadCsv" %>

<script type="text/javascript">
function VaildationCheck() {
    if ($("#<%=fileCvs.ClientID%>").val() == "") {
        alert("업로드 할 파일을 선택하세요!");
        return false;
    }
    else if ($("#<%=fileCvs.ClientID%>").val() != "") {
        var ext = $("#<%=fileCvs.ClientID%>").val().split('.').pop().toLowerCase();
        if ($.inArray(ext, ['csv']) == -1) {
            alert("csv 파일만 업로드 할 수 있습니다!");
            return false;
        }
    }
    return true;
}

function csvDownload() {
    var file = encodeURI($("#<%=hdnDownloadForm.ClientID%>").val());
    window.location.href = "../../Download.aspx?type=D&file=" + file;
}
</script>

<asp:HiddenField ID="hdnDownloadForm" runat="server" />
<asp:UpdatePanel ID="upUpload" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <!-- fake button -->
        <asp:Button ID="btnShowPopup" runat="server" Style="display: none" />
        <!-- ModalPopupExtender -->
        <ajaxToolkit:ModalPopupExtender ID="ModalPopExtUpload" runat="server"
            PopupControlID="EditPanel"
            TargetControlID="btnShowPopup"
            CancelControlID="btnCancel">
        </ajaxToolkit:ModalPopupExtender>
        <!-- PopupPanel -->
        <asp:Panel ID="EditPanel" runat="server" CssClass="modal_bg" Style="display:none;">
            <div class="modal_pop" style="width:500px;height:140px;">
                <div class="pop_title">CVS 업로드</div>
                <br />

                <table class="pop_list">
                    <colgroup>
                        <col style="width:100px" />
                        <col />
                    </colgroup>
                    <tbody>
                        <tr>
                            <td style="text-align:center;">파일선택</td>
                            <td>
                                <asp:FileUpload ID="fileCvs" runat="server" Width="95%" />
                            </td>
                        </tr>
                    </tbody>
                </table>

                <div class="pop_bottom">
                    <asp:Button ID="btnUpload" runat="server" Text="저장" CssClass="btn btn_default" OnClick="btnUpload_Click" OnClientClick="return VaildationCheck();" />
                    <asp:Button ID="btnDown" runat="server" Text="양식다운" CssClass="btn btn_default" OnClientClick="return csvDownload();" />
                    <asp:Button ID="btnCancel" runat="server" Text="닫기" CssClass="btn btn_default" />
                </div>
            </div>
        </asp:Panel>
        <!-- popup end -->
    </ContentTemplate>
    <Triggers>
        <asp:PostBackTrigger ControlID="btnUpload" />
    </Triggers>
</asp:UpdatePanel>
