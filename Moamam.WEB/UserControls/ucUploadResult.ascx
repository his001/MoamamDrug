<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucUploadResult.ascx.cs" Inherits="Site_UserControls_ucUploadResult" %>
<table style="border: solid 1px #cccccc; background-color:#ffffff;">
    <tr>
        <th style="text-align:center; width:80px">업로드결과</th>
        <td><img src="../Image/success16.png" alt="" /></td>
        <td style="width:40px;">
            <asp:Label ID="lblSuccess" runat="server" ForeColor="Green"></asp:Label></td>
        <td><img src="../Image/error16.png" alt="" /></td>
        <td style="width:40px;">
            <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label></td>
    </tr>
</table>
