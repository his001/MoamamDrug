<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucCalendarTo.ascx.cs" Inherits="UserControls_ucCalendarTo" %>
<script>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    //  숫자만 입력                                                                                //    
    ///////////////////////////////////////////////////////////////////////////////////////////////

    function inputOnlyNumber(check, evt) {
        var charCode = (evt.which) ? evt.which : event.keyCode;

        if (charCode > 31 && (charCode < 48 || charCode > 57))
            return false;

        return true;
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////
    //  년, 월 입력시 자동 '-' 문자 입력                                                             //    
    ///////////////////////////////////////////////////////////////////////////////////////////////
    function addSeparator(e, control, format) {
        this.Format = format;
        var keycode = (e.which) ? e.which : event.keyCode;

        if (control.value.length == 4) {
            if (keycode != 8)
                control.value += '-';
        }
        if (control.value.length == 7) {
            if (keycode != 8)
                control.value += '-';
        }
    }

</script>
<asp:TextBox ID="txtTo" runat="server" CssClass="input_calendar" Width="100px" Height="20px" onkeyup="addSeparator(event, this, 'yyyy-mm-dd');" onkeypress="return inputOnlyNumber(this, event);" OnTextChanged="txtTo_TextChanged" MaxLength="10"></asp:TextBox>
<ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" 
    FirstDayOfWeek="Monday"
    Format="yyyy-MM-dd"
    TargetControlID="txtTo" PopupPosition="TopRight">
</ajaxToolkit:CalendarExtender>
