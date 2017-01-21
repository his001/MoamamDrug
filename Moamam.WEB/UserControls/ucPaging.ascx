<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucPaging.ascx.cs" Inherits="Site_UserControls_ucPaging" %>

<div class="paging p_a">
	<asp:Literal runat="server" ID="litPageing"></asp:Literal>
</div>
<div class="page_view f_r">
	<asp:Label ID="lblCount" BorderStyle="None" BorderColor="0" runat="server"></asp:Label>
</div>

<asp:HiddenField runat="server" ID="hidPageNo" Value="1" />
<asp:HiddenField runat="server" ID="hidTotal" Value="0" />
<asp:HiddenField runat="server" ID="hidRowCnt" Value="35" />

<asp:LinkButton runat="server" ID="btnPageing" OnClick="btnPageing_Click" ></asp:LinkButton>
<script type="text/javascript">
    function Page_Url(pageNo) {
        if ($('input[id$="<%= this.ClientID %>_hidPageNo"]') != null) {
            $('input[id$="<%= this.ClientID %>_hidPageNo"]').val(pageNo);
            document.getElementById("<%=btnPageing.ClientID%>").click();
        }
    }
</script>