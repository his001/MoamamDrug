<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="Error.aspx.cs" Inherits="Site_Help_Error" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<title>Homeplus Auto Depot Order</title>
<% 
    Response.StatusCode = 404;
    Response.TrySkipIisCustomErrors = true;
 %>
</head>
<body>
    <form id="form1" runat="server" style="width:100%;height:100%;">

    <table style="width:100%;margin-top:30px;">
        <tr>
            <td style="padding:15px; text-align:center;">
                <img src="/Images/sub/error.png" />
            </td>
        </tr>
        <tr>
            <td style="text-align:center;line-height:170%;font-size:15px;color:#555;font-weight:bold;text-decoration:underline">
                에러가 발생 하였습니다.<br />
                관리자에게 문의 하세요.
            </td>
        </tr>
    </table>

    </form>
</body>
</html>