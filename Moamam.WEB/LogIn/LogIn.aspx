<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LogIn.aspx.cs" Inherits="LogIn_LogIn" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Drugs.or.kr</title>
    <meta name="viewport" content="width=device-width, initial-scale=1" />

    <meta http-equiv="Cache-Control" content="no-cache, no-store, must-revalidate" />
    <meta http-equiv="Pragma" content="no-cache" />
    <meta http-equiv="Expires" content="0" />
    <style type="text/css">
	    @import url(http://fonts.googleapis.com/earlyaccess/nanumgothic.css);
	    *{margin:0;padding:0px;border:0px;font-family:"Nanum Gothic"}
	    .ado_login{width:100%;height:100%;overflow:hidden;background-size:cover;background-attachment:fixed;background-repeat:no-repeat;background-position:50% 0}
	    .ado_login{background-image:url("/Images/Common/ado_bg.jpg")}
	    .style1{position:absolute;top:50%;left:50%;width:410px;height:224px;margin:-112px 0 0 -210px}
	
	    .input_aria{position:relative;top:-134px;left:52px;}
	    .input_aria > input{display:block;margin-bottom:14px;background-color:transparent;border:0 solid black;height:24px;width:215px;}
        .input_aria > input[type="text"], input[type="password"] {color:#fff;background-color:#aaa;}
	    .input_aria > input[type="submit"]{position:relative;top:-78px;left:232px;height:64px;width:80px;background-color:#ed1c24;color:#fff;font-size:15px;font-weight:bold;}
	
	    .login_footer{position:fixed;bottom:45px;width:100%;height:20px;}
	    address{font-style:normal;font-size:12px;color:#858585;text-align:center}
        /*input.inputID_PWD {background-color:#aaa;color:#fff;}*/
        input {outline:none}
        input:-webkit-autofill{-webkit-box-shadow: 0 0 0 1000px #aaa inset;}
    </style>
    <script type="text/javascript" src="/Common/js/jquery-1.11.3.min.js"></script>
    <script type="text/javascript" src="/Common/js/rsa.js"></script>
    <script type="text/javascript">
        $(document).click(function (e) {
            if ($(e.target).is('input[id*=btnLogin]')) {
               $("#<%=hdnUserId.ClientID%>").val(EncryptRSA($("#<%=txtUserID.ClientID%>").val()));
                $("#<%=txtUserID.ClientID%>").val("");

                $("#<%=hdnPassword.ClientID%>").val(EncryptRSA($("#<%=txtPassword.ClientID%>").val()));
                $("#<%=txtPassword.ClientID%>").val("");
            }
        });

        function EncryptRSA(text) {
            var rsa = new RSAKey();
            rsa.setPublic($("#<%=hdnRsaPublicModulus.ClientID%>").val(), $("#<%=hdnRsaPublicExponent.ClientID%>").val());
            var res = rsa.encrypt(text);
            return res;
        }

        function execLogin() {
            document.getElementById("<%=btnLogin.ClientID%>").click();
        }

        function checkLogin() {
            var t_userID_Null = document.getElementById("<%= txtUserID.ClientID %>").value.trim().length;
            var t_Pwd_Null = document.getElementById("<%= txtPassword.ClientID %>").value.trim().length;

            if (t_userID_Null == 0) {
                alert("아이디를 입력하세요!");
                document.getElementById("<%= txtUserID.ClientID %>").focus();
                return false;
            }
            if (t_Pwd_Null == 0) {
                alert("비밀번호를 입력하세요!");
                document.getElementById("<%= txtPassword.ClientID %>").focus();
                return false;
            }
            jsfn_SetSaveIDchkChg();
            return true;
        }
    </script>
    <script type="text/javascript">
        var __clickCnt = 0;
        $(document).ready(function () {
            jsfn_chkCookieID();
        });

        function jsfn_SetSaveIDchkChg()
        {
            var _isChk = $("#CheckSaveUserID").prop('checked');
            var _userID = $("#<%=txtUserID.ClientID%>").val();
            if (_isChk) {
                jsfn_setCookie('CookUserID', _userID, 30);
                jsfn_setCookie('CookIdAutoYN', 'Y', 30);
            } else {
                jsfn_setCookie('CookUserID', '', 30);
                jsfn_setCookie('CookIdAutoYN', 'N', 30);
            }
        }

        function jsfn_chkCookieID()
        {
            var _IdSaveAutoYN = jsfn_getCookie("CookIdAutoYN");
            var _CookUserID = jsfn_getCookie("CookUserID");
            if (_IdSaveAutoYN == 'Y') {
                $("#CheckSaveUserID").prop('checked', 'true');
                $("#txtUserID").val(_CookUserID);
                $("#<%= txtPassword.ClientID %>").focus();
            }
        }

        function jsfn_getCookie(c_name) {
            var i, x, y, ARRcookies = document.cookie.split(";");
            for (i = 0; i < ARRcookies.length; i++) {
                x = ARRcookies[i].substr(0, ARRcookies[i].indexOf("="));
                y = ARRcookies[i].substr(ARRcookies[i].indexOf("=") + 1);
                x = x.replace(/^\s+|\s+$/g, "");
                if (x == c_name) {
                    return unescape(y);
                }
            }
        }

        function jsfn_setCookie(c_name, value, exdays) {
            var exdate = new Date();
            exdate.setDate(exdate.getDate() + exdays);
            var c_value = escape(value) + ((exdays == null) ? "" : "; expires=" + exdate.toUTCString());
            document.cookie = c_name + "=" + c_value;
        }
    </script>

</head>
<body class="ado_login">
    <form id="form1" runat="server" style="width:100%;height:100%;">
    <asp:HiddenField ID="hdnRsaPublicModulus" runat="server" />
    <asp:HiddenField ID="hdnRsaPublicExponent" runat="server" />
    <asp:HiddenField ID="hdnUserId" runat="server" />
    <asp:HiddenField ID="hdnPassword" runat="server" />

	<div class="style1">
		<img src="/Images/Common/Lf.png" alt="">
		<div class="input_aria">
			<hpf:NTextBox ID="txtUserID" runat="server" TabIndex="1" CssClass="id_area b_x"></hpf:NTextBox>
			<hpf:NTextBox ID="txtPassword" runat="server" AutoCompleteType="Disabled" TextMode="Password" TabIndex="2" CssClass="pwd_area b_x"></hpf:NTextBox>
            
			<asp:Button ID="btnLogin" runat="server" OnClientClick="return checkLogin()" Text="로그인" OnClick="btnLogin_Click" style="cursor:pointer;" />

            <div id="LySavePass" style="margin-top:-60px;">
	        <input type="checkbox" id="CheckSaveUserID" name="CheckSaveUserID" />
	        <label for="CheckSaveUserID">아이디 저장</label>
            </div>
            
		</div>
	</div>
	<div class="login_footer">
		<address>COPYRIGHT @ Moamam. ALL RIGHTS RESERVED.</address>
	</div>
    </form>

</body>
</html>