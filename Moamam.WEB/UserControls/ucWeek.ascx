<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucWeek.ascx.cs" Inherits="Site_UserControls_ucWeek" %>

<asp:UpdatePanel ID="upnlweek" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <asp:DropDownList ID="cbxWeekEvent" AutoPostBack="true" runat="server" OnSelectedIndexChanged="dropWeek_SelectedIndexChanged"></asp:DropDownList>&nbsp;
        <asp:Label ID="txtFROM_TO" runat="server"></asp:Label>
    </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="cbxWeekEvent" EventName="SelectedIndexChanged" />
    </Triggers>
</asp:UpdatePanel>