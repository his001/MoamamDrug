using Moamam.Data.Common;
using Moamam.Lib;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;

public partial class Site_Data_Data : DataCommon
{
    string fnName = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //////// xml 이용 post , get 으로 올때의 get
            fnName = Request["fnName"] != null ? Request["fnName"].ToString() : "";
            switch (fnName)
            {
                //단순SP호출 공통
                case "CommonCallSp":
                    CommonCallSp();
                    break;
                //단순SP호출 공통
                case "CommonCallSpGetJson":
                    CommonCallSpGetJson();
                    break;
                case "CommonLogin":
                    string uid = Request["uid"] != null ? Request["uid"].ToString() : "";
                    string upw = Request["upw"] != null ? Request["upw"].ToString() : "";
                    UserLogin(uid, upw);
                    break;
                case "CommonReg":
                    string regid = Request["regid"] != null ? Request["regid"].ToString() : "";
                    string regNm = Request["regNm"] != null ? Request["regNm"].ToString() : "";
                    SetUserReg(regid, regNm);
                    break;
                case "CommonPWdChange":
                    string chgid = Request["chgid"] != null ? Request["chgid"].ToString() : "";
                    string opw = Request["opw"] != null ? Request["opw"].ToString() : "";
                    string npw = Request["npw"] != null ? Request["npw"].ToString() : "";
                    UserPWdChange(chgid, opw, npw);
                    break;
                case "CommonLogOut":
                    UserLogOut();
                    break;
                default:
                    break;
            }
        }
    }

    private void CommonCallSp()
    {
        string _logParam = string.Empty;

        string SpName = Request["SpName"];
        string[] SpParams = Request["SpParams"].Split('▤');
        SqlParameterCollection param = InitSqlParameterCollection();
        if (SpParams[0] == "") { param = null; }
        else
        {
            foreach (string item in SpParams)
            {
                string KeyValue = item.Split('▥')[0];
                string ParamValue = item.Split('▥')[1];

                param.Add(new SqlParameter(KeyValue.Trim(), ParamValue));

                if (_logParam == "")
                {
                    _logParam = _logParam + "@" + KeyValue.Trim() + "='" + ParamValue + "'";
                }
                else
                {
                    _logParam = _logParam + ", @" + KeyValue.Trim() + "='" + ParamValue + "'";
                }
            }
        }
        try { QueryTrace("execute " + SpName + " " + _logParam); }
        catch (Exception e) { }
        string chkSpName = SpName.Substring(SpName.Length - 2, 2);
        if (chkSpName.IndexOf("_") > -1 && chkSpName != "_R")
        {
            Log_UserAction(chkSpName);
        }
        baseCommonSpCaller(SpName, param);
    }

    private void CommonCallSpGetJson()
    {
        string _logParam = string.Empty;

        string SpName = Request["SpName"];
        string[] SpParams = Request["SpParams"].Split('▤');
        SqlParameterCollection param = InitSqlParameterCollection();
        if (SpParams[0] == "") { param = null; }
        else
        {
            foreach (string item in SpParams)
            {
                string KeyValue = item.Split('▥')[0];
                string ParamValue = item.Split('▥')[1];
                ParamValue = Regex.Replace(ParamValue, @"\r\n?|\n", "<br />");
                param.Add(new SqlParameter(KeyValue.Trim(), ParamValue));

                if (_logParam == "")
                {
                    _logParam = _logParam + "@" + KeyValue.Trim() + "='" + ParamValue + "'";
                }
                else
                {
                    _logParam = _logParam + ", @" + KeyValue.Trim() + "='" + ParamValue + "'";
                }
            }
        }
        try { QueryTrace("execute " + SpName + " " + _logParam); }
        catch (Exception e) { }
        string chkSpName = SpName.Substring(SpName.Length - 2, 2);
        if (chkSpName.IndexOf("_") > -1 && chkSpName != "_R")
        {
            Log_UserAction(chkSpName);
        }
        baseCommonCallSpGetJson(SpName, param);
    }

    public static void QueryTrace(string query)
    {
        HttpContext ctx = HttpContext.Current;
        if (System.Configuration.ConfigurationManager.AppSettings["QueryTraceMode"].ToString().ToLower() != "true") return;

        string traceInfo =
            "URL: " + ctx.Request.Url.ToString() +
            "\r\nStacktrace:---\r\n" + query + "\r\n";

        SysLogger.WriteLog("QueryString:\r\n" + traceInfo);
    }

    public static void Log_UserAction(string _CUD)
    {
        if (_CUD == "_S") { _CUD = "SAVE"; }
        else if (_CUD == "_D") { _CUD = "DELETE"; }
        else if (_CUD == "_C") { _CUD = "CREATE"; }
        else if (_CUD == "_I") { _CUD = "INSERT"; }
        else { _CUD = "READ"; }

        if (_CUD != "READ")
        {
            try
            {
                UserInfo ui = (UserInfo)HttpContext.Current.Session[System.Configuration.ConfigurationManager.AppSettings["SysName"].ToString()];
                if (ui != null)
                {
                    (new SiteGrant()).SetUserLog(
                        ui.UserID,
                        HttpContext.Current.Session["menuCd"].ToString(),
                        _CUD);
                }
            }
            catch (Exception ex) { }
        }
    }

    public void UserLogin(string userId, string UserPwd)
    {
        string srtMsg = string.Empty;
        if (userId == "" || UserPwd == "")
        {
            srtMsg = "에러가 발생하였습니다.";
        }
        else
        {
            DataSet ds = null;
            string spName = "SPM_Web_COMMON_Tbl_member_R";
            SqlParameterCollection param = DataCommon.InitSqlParameterCollection();
            param.Add(new SqlParameter("UserID", userId));
            ds = DataCommon.CommonSpCall(spName, param);

            if (ds.Tables[0].Rows.Count > 0)
            {
                string _DbPwd = string.Empty;
                _DbPwd = ds.Tables[0].Rows[0]["UserPwd"].ToString();
                if (_DbPwd == UserPwd) // 비번이 동일하면 로그인 세션 생성
                {

                    UserInfo user = new UserInfo();
                    user.UserID = userId;
                    user.UserName = ds.Tables[0].Rows[0]["UserName"].ToString();
                    user.UserGroupName = ds.Tables[0].Rows[0]["UserGroupName"].ToString();
                    user.UserGroupCode = ds.Tables[0].Rows[0]["UserGroup"].ToString();

                    //사용자 세션 생성
                    SessionAuth.LoginProcess(user);

                    srtMsg = "loginSuccess";
                }
                else
                {
                    srtMsg = "wrongPwd";
                }
            }
            else
            {
                // ID 가 존재 하지 않음
                srtMsg = "noID";
            }
        }
        //return srtMsg;
        Response.Clear();
        Response.Write(srtMsg);
        Response.End();
    }

    public void UserLogOut() {
        SessionAuth.LogoutProcess();
        Response.Clear();
        Response.Write("LogOutOk");
        Response.End();
    }

    #region ################## return XML Sample ##################
    public void rtnXMLSample()
    {

        string sErrorType = string.Empty;       //E:Error, I:Information, C:Confirm, CT:커스텀 이벤트
        string sErrorMessage = string.Empty;

        GridHelper gridHelper = new GridHelper();
        System.Text.StringBuilder sbReturnXml = new System.Text.StringBuilder();

        DataSet ds = null;
        string userId = string.Empty;
        string spName = "SPM_Web_COMMON_Tbl_member_R";
        SqlParameterCollection param = DataCommon.InitSqlParameterCollection();
        param.Add(new SqlParameter("UserID", userId));
        ds = DataCommon.CommonSpCall(spName, param);


        if (ds != null && ds.Tables.Count > 0)
        {
            XmlResultInfo xmlResultInfo = null;
            for (int iCnt = 0; iCnt < ds.Tables.Count; iCnt++)
            {
                xmlResultInfo = new XmlResultInfo();
                if (ds.Tables[iCnt].Rows.Count > 0)
                {
                    //XML Properties
                    if (sErrorType.Length == 0 || !sErrorType.Equals("E"))
                    {
                        xmlResultInfo.Result = "S";
                    }
                    else
                    {
                        xmlResultInfo.Result = "F";
                    }
                    xmlResultInfo.ResultType = sErrorType;
                    xmlResultInfo.ResultMessage = sErrorMessage;
                    xmlResultInfo.BindID = ds.Tables[iCnt].TableName + iCnt.ToString();

                    xmlResultInfo.FormatType.Add("StockQty", GridHelper.FormatType.Number);
                    xmlResultInfo.FormatType.Add("AvailOrderQty", GridHelper.FormatType.Number);
                    xmlResultInfo.FormatType.Add("AvailReserveQty", GridHelper.FormatType.Number);

                    //결과 데이터 형태 - RecordSet Type
                    xmlResultInfo.ReturnValueType = ReturnValue.ReturnValueType.RecordSet;

                    //Sorting
                    DataView dvData = ds.Tables[iCnt].DefaultView;
                    dvData.Sort = Request["SortInfo"] != null ? Request["SortInfo"] : "";
                    DataTable dtFilterdData = dvData.ToTable();

                    sbReturnXml.Append(gridHelper.ResultDataToXmlString(dtFilterdData, xmlResultInfo));
                }
                else
                {
                    xmlResultInfo.Result = "S";
                    xmlResultInfo.ResultType = "E";
                    xmlResultInfo.ResultMessage = "Global_NoDataInform";

                    sbReturnXml.Append(gridHelper.ResultDataToXmlString(xmlResultInfo));
                }
            }
        }

        Response.Clear();
        Response.Write(gridHelper.AddRootHeader(sbReturnXml.ToString()));
        Response.End();
    }
    #endregion ################## return XML Sample ##################

    /// <summary>
    /// /암호 변경
    /// </summary>
    /// <param name="regid"></param>
    /// <param name="regNm"></param>
    /// <param name="regNm"></param>
    public void UserPWdChange(string chgid, string opw, string npw)
    {
        string srtMsg = string.Empty;
        if (chgid == "" || opw == "" || npw == "")
        {
            srtMsg = "에러가 발생하였습니다.";
        }
        else
        {
            
            DataSet ds = null;
            string spName = "SPM_Web_COMMON_Tbl_member_Pwd_S";
            SqlParameterCollection param = DataCommon.InitSqlParameterCollection();
            param.Add(new SqlParameter("UserID", chgid));
            param.Add(new SqlParameter("UserPwd", opw));
            param.Add(new SqlParameter("UserPwdNew", npw));
            ds = DataCommon.CommonSpCall(spName, param);

            if (ds.Tables[0].Rows.Count > 0)
            {
                string _RESULT = string.Empty;
                _RESULT = ds.Tables[0].Rows[0]["RESULT"].ToString();
                if (_RESULT == "OK") // insert 되면 메일 발송
                {
                    if (sendSmtp(chgid) == "success")
                    {
                        srtMsg = "PwdChgSuccess";  //
                    }
                    else
                    {
                        srtMsg = "err"; //
                    }
                }
                else
                {
                    srtMsg = "pwdDiff"; //
                }
            }
            else
            {
                srtMsg = "err"; //
            }
        }
        //return srtMsg;
        Response.Clear();
        Response.Write(srtMsg);
        Response.End();
    }

    public void SetUserReg(string regid, string regNm)
    {
        string srtMsg = string.Empty;
        if (regid == "" || regNm == "")
        {
            srtMsg = "에러가 발생하였습니다.";
        }
        else
        {
            Random r = new Random();
            int winning_number = r.Next(100000, 900000);
            string UserPwd = winning_number.ToString();

            DataSet ds = null;
            string spName = "SPM_Web_COMMON_Tbl_member_S";
            SqlParameterCollection param = DataCommon.InitSqlParameterCollection();
            param.Add(new SqlParameter("UserID", regid));
            param.Add(new SqlParameter("UserPwd", UserPwd));
            param.Add(new SqlParameter("UserName", regNm));
            ds = DataCommon.CommonSpCall(spName, param);

            if (ds.Tables[0].Rows.Count > 0)
            {
                string _RESULT = string.Empty;
                _RESULT = ds.Tables[0].Rows[0]["RESULT"].ToString();
                if (_RESULT == "OK") // insert 되면 메일 발송
                {
                    if (sendSmtp(regid, UserPwd) == "success")
                    {
                        srtMsg = "RegSuccess";  //
                    }
                    else {
                        srtMsg = "err"; //
                    }
                }
                else
                {
                    srtMsg = "SameID"; //
                }
            }
            else
            {
                srtMsg = "err"; //
            }
        }
        //return srtMsg;
        Response.Clear();
        Response.Write(srtMsg);
        Response.End();
    }

    private string sendSmtp(string uEmail , string uPwd)
    {
        string rtnMsg = string.Empty;

        SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
        client.UseDefaultCredentials = false; // 시스템에 설정된 인증 정보를 사용하지 않는다.
        client.EnableSsl = true;  // SSL을 사용한다.
        client.DeliveryMethod = SmtpDeliveryMethod.Network; // 이걸 하지 않으면 Gmail에 인증을 받지 못한다.
        client.Credentials = new System.Net.NetworkCredential("moamamdrug", "qlalfqjsghdpdy^^");

        MailAddress from = new MailAddress("moamamdrug@gmail.com", "모아맘", System.Text.Encoding.UTF8);
        MailAddress to = new MailAddress(uEmail);

        MailMessage message = new MailMessage(from, to);

        message.Body = " 귀하의 비밀번호는 " + uPwd + " 입니다.";
        string someArrows = new string(new char[] { '\u2190', '\u2191', '\u2192', '\u2193' });
        message.Body += Environment.NewLine + someArrows;
        message.BodyEncoding = System.Text.Encoding.UTF8;
        message.Subject = "안녕하세요^^ 모아맘입니다." + someArrows;
        message.SubjectEncoding = System.Text.Encoding.UTF8;

        try
        {
            // 동기로 메일을 보낸다.
            client.Send(message);
            // Clean up.
            message.Dispose();

            rtnMsg = "success";
        }
        catch (Exception ex)
        {
            rtnMsg = ex.ToString();
        }
        return rtnMsg;
    }


    private string sendSmtp(string uEmail)
    {
        string rtnMsg = string.Empty;

        SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
        client.UseDefaultCredentials = false; // 시스템에 설정된 인증 정보를 사용하지 않는다.
        client.EnableSsl = true;  // SSL을 사용한다.
        client.DeliveryMethod = SmtpDeliveryMethod.Network; // 이걸 하지 않으면 Gmail에 인증을 받지 못한다.
        client.Credentials = new System.Net.NetworkCredential("moamamdrug", "qlalfqjsghdpdy^^");

        MailAddress from = new MailAddress("moamamdrug@gmail.com", "모아맘", System.Text.Encoding.UTF8);
        MailAddress to = new MailAddress(uEmail);

        MailMessage message = new MailMessage(from, to);

        message.Body = uEmail + " 님의 비밀번호가 변경 되었습니다.";
        string someArrows = new string(new char[] { '\u2190', '\u2191', '\u2192', '\u2193' });
        message.Body += Environment.NewLine + someArrows;
        message.BodyEncoding = System.Text.Encoding.UTF8;
        message.Subject = "안녕하세요^^ 모아맘입니다." + someArrows;
        message.SubjectEncoding = System.Text.Encoding.UTF8;

        try
        {
            // 동기로 메일을 보낸다.
            client.Send(message);
            // Clean up.
            message.Dispose();

            rtnMsg = "success";
        }
        catch (Exception ex)
        {
            rtnMsg = ex.ToString();
        }
        return rtnMsg;
    }
    

}