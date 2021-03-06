﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>
<%@ Register Src="~/UserControls/ucPagingJS.ascx" TagName="ucPaging" TagPrefix="uc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<title>Drugs</title>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<meta name="viewport" content="width=device-width, initial-scale=1, user-scalable=no" />
<link rel="stylesheet" href="assets/css/main.css" />
<!--[if lte IE 9]><link rel="stylesheet" href="assets/css/ie9.css" /><![endif]-->
<noscript><link rel="stylesheet" href="assets/css/noscript.css" /></noscript>


<link rel="stylesheet" href="http://code.jquery.com/ui/1.10.2/themes/smoothness/jquery-ui.css" />
<script src="/Common/js/jquery.min.js"></script>
<script src="http://code.jquery.com/ui/1.10.2/jquery-ui.js"></script>

<script src="/Common/js/jquery.bpopup.min.js"></script>
<script src="/Common/js/jquery.toastmessage.js"></script>
<link href="/Common/css/jquery.toastmessage.css" rel="stylesheet" />
<script src="/Common/js/moamam.js"></script>
<script src="/Common/js/page1.js"></script>
<script src="/Common/js/page1TblGrid.js"></script>
<script src="/assets/js/skel.min.js"></script>
<script src="/assets/js/util.js"></script>
<script src="/assets/js/main.js"></script>
</head>
<body>
    <form id="form1" runat="server">
<!-- Wrapper -->
			<div id="wrapper">

				<!-- Header -->
					<header id="header">
						<div class="logo">
                            <span class="icon fa-ambulance"></span>
						</div>
						<div class="content">
							<div class="inner">
								<h1>첫 방문 이신가요?</h1>
								<p><a href="#intro"><strong>Yes</strong></a> or <a href="#LymainLogin"><strong>No</strong></a></p>
                                    <a href="#LymainPwdChange">암호변경</a>
                                
							</div>
						</div>
						<nav>
							<ul>
                                <!--<li><a href="#intro"><span id="LyLoginID">처음오셨나요?</span></a></li>-->
                                <li><a href="#Lysec_BokYongHooKi_LstGrid">복용 후기 목록</a></li>
                                <li><a href="#Lysec_BokYongHooKi_Write">복용 후기 작성</a></li>
                                <li><a href="#contact">투약의뢰서</a></li>
							</ul>
						</nav>
					</header>

				<!-- Main -->
					<div id="main">

						<!-- Intro 회원가입 -->
						<article id="intro">
							<%--<h2 class="major">회원가입/로그인</h2>
							<span class="image main"><img src="images/pic01.jpg" alt="" /></span>--%>
<!-- 회원가입 폼 -->
<header><h2>회원가입</h2></header>
<table border="0" style="width:100%">
    <tr>
        <td colspan="3">
            IP : <span id="txt_Userip"><%=_regip%></span>
        </td>
    </tr>
    <tr>
        <td style="vertical-align:middle;">이름</td>
        <td colspan="2"><input type="text" id="txtMemName" maxlength="50" /></td>
    </tr>
    <tr><td colspan="3" style="height:10px;"></td></tr>
    <tr>
        <td style="vertical-align:middle;">E-mail</td>
        <td colspan="2"><input type="text" id="txtRegistID" maxlength="50" /></td>
    </tr>
    <tr><td colspan="3" style="height:10px;"></td></tr>
    <tr>
        <td colspan="3" style="height:10px;">
            <input type="button" id="btnRegister" value="회원가입" onclick="jsfn_RegistChk();" />
        </td>
    </tr>
</table>

<div id="LyQ1_Y1_Reg1" style="display:none;">
</div>

<div id="LyQ1_Y1_Login1" style="display:none;">

</div>
						</article>

                        <%-- 로그인 --%>
                        <article id="LymainLogin">
							<h2 class="major">회원가입/로그인</h2>
<header><h2>로그인</h2></header>
<table border="0" style="width:100%">
    <tr>
        <td style="vertical-align:middle;">E-mail</td>
        <td><input type="text" id="txtUserID" maxlength="50" /></td>
        <td rowspan="3" style="padding-left:10px;"></td>
    </tr>
    <tr><td colspan="3" style="height:10px;"></td></tr>
    <tr>
        <td style="vertical-align:middle;">암호</td>
        <td><input type="password" id="txtPwd" maxlength="50" /></td>
    </tr>
    <tr>
        <td colspan="3" style="height:10px;">
            <table>
                <tr>
                    <td> </td>
                    <td style="text-align:right;">
                        <input type="checkbox" id="CheckSaveUserID" name="CheckSaveUserID" style="cursor:pointer;" />
                    </td>
                    <td style="text-align:left;">
                        <label for="CheckSaveUserID" style="cursor:pointer;">아이디 저장</label>
                    </td>
                    <td>
                        <table>
                            <tr>
                                <td style="text-align:right;">
                                    <input type="checkbox" id="CheckAotoLogin" name="CheckAotoLogin" style="cursor:pointer;" />
                                </td>
                                <td style="text-align:left;"><label for="CheckAotoLogin" style="cursor:pointer;">자동로그인</label></td>
                                <td></td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="4" style="height:50px;"></td>
                </tr>
                <tr>
                    <td colspan="4">
                        <input type="button" id="btnLogin" value="Log In" onclick="jsfn_LoginChk();" />
                        <input type="button" id="btnIDFind" value="비밀번호 찾기" onclick="jsfn_SendPwdEmail();" />
                    </td>
                </tr>
                <tr>
                    <td colspan="4"></td>
                </tr>
            </table>
        </td>
    </tr>
</table>
                        </article>

                        <article id="LymainPwdChange">
							<h2 class="major">암호변경</h2>
            <table border="0" style="width:100%">
                <tr>
                    <td style="vertical-align:middle;">E-mail</td>
                    <td><input type="text" id="txtChgUserID" maxlength="50" /></td>
                    <td rowspan="3" style="padding-left:10px;"></td>
                </tr>
                <tr><td colspan="3" style="height:10px;"></td></tr>
                <tr>
                    <td style="vertical-align:middle;">암호</td>
                    <td><input type="password" id="txtOrgPwd" maxlength="50" /></td>
                </tr>
                <tr><td colspan="3" style="height:10px;"></td></tr>
                <tr>
                    <td style="vertical-align:middle;">변경할암호</td>
                    <td><input type="password" id="txtNewPwd" maxlength="50" /></td>
                </tr>
                <tr><td colspan="3" style="height:10px;"></td></tr>
                <tr>
                    <td colspan="3" style="height:10px;">
                        <input type="button" id="btnChgPwd" value="암호변경" onclick="jsfn_ChangePwd();" />
                    </td>
                </tr>
            </table>
                        </article>
						<!-- Lysec_BokYongHooKi_LstGrid 복용 후기 리스트 -->
                        <article id="Lysec_BokYongHooKi_LstGrid" style="width:100%;">
                            <h2 class="major">복용 후기 목록</h2>
                            <div id="LyBtnGrdSearch" style="text-align:right;padding:0 15px 15px 0;"><input type="button" id="btnSearch" class="btn btn_inquiry" value="조회" style="cursor:pointer;" onclick="jsfn_Search();" /></div>
                            <div id="jqGrid">
<!-- List Start -->
		    <table id="dvGrid1" style="min-width:1000px;left:0;">
			<thead>
                <tr style="height:35px;">
<td style="color:white;"><b>idx</b></td>
<td style="color:white;"><b>방문날짜</b></td>
<td style="color:white;"><b>완치날짜</b></td>
<td style="color:white;"><b>체온</b></td>
<td style="color:white;"><b>열</b></td>
<td style="color:white;"><b>해열제</b></td>
<td style="color:white;"><b>투약일</b></td>

<td style="color:white;display:none;"><b>참고사항</b></td>
<td style="color:white;display:none;"><b>복용후기</b></td>
<td style="color:white;display:none;"><b>처방</b></td>
<td style="color:white;display:none;"><b>증상</b></td>
<td style="color:white;display:none;"><b>HangSaengJeBokan</b></td>
<td style="color:white;display:none;"><b>HangSaengJeEat</b></td>
                </tr>
			</thead>
                <tr>
                    <td colspan="15" style="width:100%;height:25px;text-align:center;"></td>
                </tr>
            </table> 

    <uc1:ucPaging ID="ucPaging" runat="server" />
                            </div>
                        </article>

						<!-- 복용 후기 쓰기 Lysec_BokYongHooKi_Write -->
                        <article id="Lysec_BokYongHooKi_Write">
                            <h2 class="major">복용 후기 작성</h2>
                            <!--<span class="image main"><img src="images/pic03.jpg" alt="" /></span>-->
                            <div class="content box style2">

                                <div class="row">
                                    <div class="field half first">방문날짜</div>
                                    <div class="field half">
                                        <input type="text" id="txt_Visit_Date" class="datepicker" />
                                        <input type="hidden" id="txt_idx" value="0" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="field half first">완치날짜</div>
                                    <div class="field half"><input type="text" id="txt_NoPain_Date" class="datepicker" /></div>
                                </div>

                                <div class="row">
                                    <!-- 8 : 4 비율 -->
                                    <div class="col-xs-8 col-sm-8 col-md-8">
<textarea id="txt_JngSang" name="txt_JngSang" style="height:250px;padding-right:10px;font-size:12px;">
< 증상 >
ex) 기침, 콧물

</textarea>
                                    </div>
                                    <div class="col-xs-4 col-sm-4 col-md-4">
                                        <div class="row">
                                            <div class="row">
                                                <div class="field half first">체온 </div>
                                                <div class="field half">
                                                    <table>
                                                        <tr>
                                                            <td><input type="text" id="txt_tempC" maxlength="4" onkeyup="onlyDecimal();" style="height:30px;" /></td>
                                                            <td style="width:60px;">℃</td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-xs-12">
                                                    <table>
                                                        <tr style="height:2px;background-color:white;">
                                                            <td style="background-color:green;width:33%;text-align:center;"><input type="radio" name="rdoFeber" id="rdoFeberGraan" value="G" checked /><label for="rdoFeberGraan" style="color:white;cursor:pointer;">정상</label></td>
                                                            <td style="background-color:yellow;width:33%;text-align:center;"><input type="radio" name="rdoFeber" id="rdoFeberYellow" value="Y" /><label for="rdoFeberYellow" style="cursor:pointer;color:#000;">미열</label></td>
                                                            <td style="background-color:red;width:33%;text-align:center;"><input type="radio" name="rdoFeber" id="rdoFeberRed" value="R" /><label for="rdoFeberRed" style="cursor:pointer;">고열</label></td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="field half first">
                                                    해열제
                                                </div>
                                                <div class="field half">
                                                    <table>
                                                        <tr>
                                                            <td style="width:40px"><input type="radio" name="rdoHaeYeolJeOX" id="rdoHaeYeolJeO" value="O" /> <label for="rdoHaeYeolJeO" style="cursor:pointer;">O</label></td>
                                                            <td style="width:40px"><input type="radio" name="rdoHaeYeolJeOX" id="rdoHaeYeolJeX" value="X" checked="checked" /><label for="rdoHaeYeolJeX" style="cursor:pointer;">X</label></td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-xs-12">※ 같은계열 4시간,<br />다른계열 2시간 간격</div>
                                            </div>
                                        </div>
                                    </div>
                                </div>


                                <div class="row">
                                    <!-- 8 : 4 비율 -->
                                    <div class="col-xs-8 col-sm-8 col-md-8">
<textarea id="txt_ChouBang" name="txt_ChouBang" style="height:120px;padding-right:10px;font-size:12px;">
< 처방 >
ex) 투세프건조시럽

</textarea>
<br />
<a href="javascript:jsfn_getDrugInfo();">처방-약종류 불러오기</a><br />
<%--<select id="sel_ChouBangYak" size="5" style="width:99%"></select>--%>
<table id="dvGrid1Drug" style="min-width:99%;left:0;">
<thead>
    <tr style="height:35px;">
<td style="color:white;"><b>약이름</b></td>
<td style="color:white;"><b>메모</b></td>
<td style="display:none;"><b>약고유순번</b></td>
    </tr>
</thead>
    <tr>
        <td colspan="3" style="width:100%;height:25px;text-align:center;"></td>
    </tr>
</table> 

                                    </div>
                                    <div class="field half first">
                                        <input type="text" id="txt_Yak_iLbun" maxlength="4" class="numericOnly" style="height:30px;" />
                                    </div>
                                    <div class="field half">
                                        일분
                                    </div>
                                    <div class="field half first">
                                        항생제 보관방법
                                    </div>
                                    <div class="field half">
                                        <table>
                                            <tr>
                                                <td>
                                                    <input type="radio" name="rdoHangSaengJeBokan" id="rdoHangSaengJeBokanCold" value="C" checked="checked" /><label for="rdoHangSaengJeBokanCold" style="cursor:pointer;">냉장</label>
                                                </td>
                                                <td>
                                                    <input type="radio" name="rdoHangSaengJeBokan" id="rdoHangSaengJeBokanSilOn" value="S" /><label for="rdoHangSaengJeBokanSilOn" style="cursor:pointer;">실온</label>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div>
                                        <table>
                                            <tr>
                                                <td>
                                                    <input type="radio" name="rdoHangSaengJeEat" id="rdoHangSaengJeEatA" value="A" /><label for="rdoHangSaengJeEatA" style="cursor:pointer;">식전</label>
                                                </td>
                                                <td>
                                                    <input type="radio" name="rdoHangSaengJeEat" id="rdoHangSaengJeEatB" value="B" /><label for="rdoHangSaengJeEatB" style="cursor:pointer;">식간</label>
                                                </td>
                                                <td>
                                                    <input type="radio" name="rdoHangSaengJeEat" id="rdoHangSaengJeEatC" value="C" checked="checked" /><label for="rdoHangSaengJeEatC" style="cursor:pointer;">식후</label>
                                                </td>
                                            </tr>
                                        </table>

                                    </div>
                                </div>


                                <div class="row">
                                    <div class="col-xs-12 col-sm-12 col-md-12">
<textarea id="txt_ChamGoSaHang" name="txt_ChamGoSaHang" style="height:150px;padding-right:10px;font-size:12px;">
< 참고사항 > * 어린이집 선생님께 남기실 말씀도 적어주세요.
○ 철분제와 같이 복용 X , 철분제 복용 잠시 중단
○ 만약 2개 이상일 때 최소 5분 간격

</textarea>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-xs-12 col-sm-12 col-md-12">
<textarea id="txt_BokYongHooKi" name="txt_BokYongHooKi" style="height:150px;padding-right:10px;font-size:12px;">
< 복용후기 >
ex) 항생제 복용 때문에 설사함

</textarea>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-xs-12 col-sm-12 col-md-12">
                                        <a href="#contact" onclick="javascript:jsfn_makeToYakReq();">투약 의뢰서 작성하기</a>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-xs-12 col-sm-12 col-md-12">
                                        <input type="button" id="btnBokYongHooKiSave" value="저장" onclick="jsfn_BokYongHooKi_Save();" />
                                    </div>
                                </div>

                            </div>
                        </article>

						<!-- Contact -->
						<article id="contact">
							<h2 class="major">투약의뢰서</h2>

                            <div class="content">
                                <div class="box">
                                    <form method="post" action="#">
                                        <div class="field half first"><input type="text" id="txt_ChildName" name="txt_ChildName" placeholder="아이이름" maxlength="10" /></div>
                                        <div class="field half"><input type="text" name="txt_HP" placeholder="긴급연락처" maxlength="15" /></div>

                                        <div class="field half first"><input type="text" id="txt_ReceptName" name="txt_ReceptName" placeholder="수신인이름" maxlength="10" /></div>
                                        <div class="field half"><input type="text" id="txt_ReceptHP" name="txt_ReceptHP" placeholder="수신번호" maxlength="15" /></div>

                                        <div class="field">
<textarea id="txt_ToYak_messageA" name="txt_ToYak_messageA" placeholder="Message" rows="7">
YYYY/MM/DD ~ YYYY/MM/DD
XX 어린이집(유아/유치원)에
1일 x회 투약을 의뢰합니다.

※약은 소분해서 보내주시는 것을 권장 합니다.

2017-01-10 홍길동 배상
</textarea>
                                        </div>


                                        <ul class="actions">
                                            <li><input type="button" value="발송하기"  onclick="jsfn_alertClose('발송기능은 아직 준비중입니다.');" /></li>
                                        </ul>
                                    </form>
                                </div>
                            </div>								

							<ul class="icons">
								<li><a href="#" class="icon fa-twitter"><span class="label">Twitter</span></a></li>
								<li><a href="#" class="icon fa-facebook"><span class="label">Facebook</span></a></li>
								<li><a href="#" class="icon fa-instagram"><span class="label">Instagram</span></a></li>
								<li><a href="#" class="icon fa-github"><span class="label">GitHub</span></a></li>
							</ul>
						</article>

						<!-- Elements -->
						

					</div>

				<!-- Footer -->
					<footer id="footer">
						<p class="copyright">&copy; Drugs.or.kr </p>
					</footer>

			</div>

		<!-- BG -->
			<div id="bg"></div>

    <div id="LyPop1" class="modal_pop" style="width:550px;height:450px;padding: 2px 2px 2px 2px;display:none;background-color:#FFF !important;"></div>

    <script type="text/javascript">
        //#########################################
        $(document).ready(function () {
            jsfn_chkCookieID();
            $("#txtPwd").keypress(function (event) { if (event.which == 13) { jsfn_LoginChk(); } });
            //jsfn_getUserIP(function (ip) { $("#txt_Userip").html(ip); });
            $(".numericOnly").keypress(function (e) {
                if (String.fromCharCode(e.keyCode).match(/[^0-9]/g)) return false;
            });
            var _Glovaltoday = new Date();
            $("#txt_Visit_Date").val(_Glovaltoday.toISOString().substring(0, 10));
            //jsfn_InitGrid();
        });
        //#########################################
    </script>
    <input type="hidden" id="Login_userId"/>
    <div id="divProgress4Mstjs" class="dvProgressMst" style="position:absolute;top:50%;left:46%;z-index:999;width:110px;height:110px;display:none;"><img src="/Images/common/loading_4.gif" alt="" style="width:110px;height:110px;"></div>
    </form>
</body>
</html>
