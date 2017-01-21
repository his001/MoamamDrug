<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucStoreFromToTzGroup.ascx.cs" Inherits="Site_UserControls_ucStoreFromToTzGroup" %>

<asp:UpdatePanel ID="upnlStoreTZGroup" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <table>
            <tr>
                <td>
                    <asp:DropDownList ID="ddlFromStoreList" runat="server" Width="140" AutoPostBack="true" OnSelectedIndexChanged="ddlFromStoreList_SelectedIndexChanged"></asp:DropDownList>
                    &nbsp;
                </td>
                <td>
                    <asp:DropDownList ID="ddlToStoreList" runat="server" Width="140"></asp:DropDownList>
                </td>
            </tr>
        </table>
    </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="ddlFromStoreList" EventName="SelectedIndexChanged" />
    </Triggers>
</asp:UpdatePanel>