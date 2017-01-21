<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucCalendar.ascx.cs" Inherits="Site_UserControls_ucCalendar" %>
<asp:TextBox ID="txtCalendar" runat="server" CssClass="input_calendar"></asp:TextBox>
<ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" 
    FirstDayOfWeek="Monday"
    Format="yyyy-MM-dd"
    TargetControlID="txtCalendar" >
</ajaxToolkit:CalendarExtender>
