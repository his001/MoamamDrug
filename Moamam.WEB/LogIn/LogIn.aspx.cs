using System;
using System.Web.UI;
using Moamam.Lib;
using System.Data;
using AESWeb;

using Moamam.Data.Common;
using System.Security.Cryptography;
using System.Web;
using System.Data.SqlClient;

public partial class LogIn_LogIn : System.Web.UI.Page
{
    string strP_byPass = "";
    string strP_UserID = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            if (Request["UserID"] != null) { strP_UserID = Request["UserID"]; }
            if (Request["byPass"] != null) { strP_byPass = Request["byPass"]; }
            if (strP_byPass == "Y" && strP_UserID != "") { byPassLogin(); }

            //Plusnet로그인시 사용.
            if (strP_UserID != null && strP_UserID != "")
            {
                //SetPlusnetLogin(strP_UserID);
            }
            
            txtUserID.Focus();

            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            RSAParameters privateKey = System.Security.Cryptography.RSA.Create().ExportParameters(true);
            rsa.ImportParameters(privateKey);
            string privateKeyText = rsa.ToXmlString(true);
            Session["RsaPrivateKey"] = privateKeyText;
            string publicKeyM = "";
            string publicKeyE = "";
            // byte[] -> hex 16진수
            for (int i = 0; i < privateKey.Modulus.Length; i++) { 
                publicKeyM += privateKey.Modulus[i].ToString("X2");
            }

            for (int i = 0; i < privateKey.Exponent.Length; i++) { 
                publicKeyE += privateKey.Exponent[i].ToString("X2");
            }

            hdnRsaPublicModulus.Value = publicKeyM;
            hdnRsaPublicExponent.Value = publicKeyE;        }
    }

    private void byPassLogin()
    {
        DataSet ds = null;
        ds = (new SiteUser()).GetUserLoginCheck(strP_UserID.Trim());

        if (ds.Tables[0].Rows.Count > 0)
        {
            UserInfo user = new UserInfo();
            user.UserID = txtUserID.Text;
            user.UserName = ds.Tables[0].Rows[0]["USER_NAME"].ToString();
            user.UserGroupName = ds.Tables[0].Rows[0]["SUB_CD_NM"].ToString();
            user.UserGroupCode = ds.Tables[0].Rows[0]["SUB_CD"].ToString();

            //사용자 세션 생성
            SessionAuth.LoginProcess(user);

            //사용자 첫화면 URL 조회
            DataSet ds2 = (new SiteUser()).GetUserLoginFirstUrl(user.UserGroupCode);

            if (ds2.Tables[0].Rows.Count == 0)
            {
                ShowMessage("해당시스템을 사용하실 수 없습니다.");
                Response.Redirect("~/Login/Login.aspx", false);
            }
            else
            {
                #region #### 탭 전 기존 코드 ####
                //string strMenuUrl = ds2.Tables[0].Rows[0]["MENU_URL"].ToString();
                ////"/" + ds2.Tables[0].Rows[0]["MENU_URL"].ToString() + 
                ////"?G=" + ds2.Tables[0].Rows[0]["GROUP_CD"].ToString() +
                ////"&M=" + ds2.Tables[0].Rows[0]["MENU_CD"].ToString();
                //Response.Redirect(strMenuUrl);
                //Response.End();
                #endregion #### 탭 전 기존 코드 ####
                string strMenuUrl = "/Site/Blank.aspx";
                Response.Redirect(strMenuUrl);
                Response.End();
            }
        }
    }


    #region 로그인 버튼 클릭 이벤트
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        string id = hdnUserId.Value.Trim();
        string pwd = hdnPassword.Value.Trim();

        // 암호화 개체 생성 
        RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();

        // 복호화
        string hex = id;
        byte[] ubyte = RsaHelper.StringToByteArray(hex);
        id = RsaHelper.RSADecrypt(ubyte, Context.Session["RsaPrivateKey"].ToString());

        hex = pwd;
        ubyte = RsaHelper.StringToByteArray(hex);
        pwd = RsaHelper.RSADecrypt(ubyte, Context.Session["RsaPrivateKey"].ToString());

        try
        {
            string errMsg       = "";
            //DataSet ds          = null;
            if (rtnUserInfo(id, pwd) == "loginSuccess") {
                string strMenuUrl = "/Default.aspx";
                Response.Redirect(strMenuUrl);
                Response.End();
            }

            ////ds = (new SiteUser()).GetUserLoginCheck(txtUserID.Text.Trim());
            //ds = (new SiteUser()).GetUserLoginCheck(id);
                

            //if (ds.Tables[0].Rows.Count > 0)
            //{
            //    UserInfo user       = new UserInfo();
            //    //user.UserID         = txtUserID.Text;
            //    user.UserID         = id;
            //    user.UserName       = ds.Tables[0].Rows[0]["USER_NAME"].ToString();
            //    user.UserGroupName  = ds.Tables[0].Rows[0]["SUB_CD_NM"].ToString();
            //    user.UserGroupCode  = ds.Tables[0].Rows[0]["SUB_CD"].ToString();

            //    //사용자 세션 생성
            //    SessionAuth.LoginProcess(user);

            //    #region ##############  Cookie 로 id 저장  ##############
            //    try
            //    {
            //        HttpCookie myCookie = new HttpCookie("Moamam_Drug_Cookie");
            //        myCookie["UserID"] = AES256.AESEncrypt256(id);
            //        myCookie["UserName"] = AES256.AESEncrypt256(ds.Tables[0].Rows[0]["USER_NAME"].ToString());
            //        myCookie["UserGroupCode"] = AES256.AESEncrypt256(ds.Tables[0].Rows[0]["SUB_CD"].ToString());
            //        myCookie["UserGroupName"] = AES256.AESEncrypt256(ds.Tables[0].Rows[0]["SUB_CD_NM"].ToString());

            //        myCookie.Expires = DateTime.Now.AddDays(1);
            //        Response.Cookies.Add(myCookie);
            //    }
            //    catch (Exception ex)
            //    {
            //        //Logger.Write("errLog", ex.Message.ToString());
            //    }
            //    #endregion ##############  Cookie 로 id 저장  ##############

            //    //사용자 첫화면 URL 조회
            //    DataSet ds2 = (new SiteUser()).GetUserLoginFirstUrl(user.UserGroupCode);

            //    if(ds2.Tables[0].Rows.Count == 0)
            //    {
            //        ShowMessage("해당시스템을 사용하실 수 없습니다.");
            //        Response.Redirect("~/Login/Login.aspx", false);
            //    }
            //    else
            //    {
            //        #region #### 탭 전 기존 코드 ####
            //        //string strMenuUrl = ds2.Tables[0].Rows[0]["MENU_URL"].ToString();
            //        ////"/" + ds2.Tables[0].Rows[0]["MENU_URL"].ToString() + 
            //        ////"?G=" + ds2.Tables[0].Rows[0]["GROUP_CD"].ToString() +
            //        ////"&M=" + ds2.Tables[0].Rows[0]["MENU_CD"].ToString();
            //        //Response.Redirect(strMenuUrl);
            //        //Response.End();
            //        #endregion #### 탭 전 기존 코드 ####
            //        string strMenuUrl = "/Site/Blank.aspx";
            //        Response.Redirect(strMenuUrl);
            //        Response.End();
            //    }
            //}
            //else
            //{
            //    ShowMessage("등록된 사용자가 아닙니다.");
            //}
            //ShowMessage(errMsg);
            
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message);
        }
    }
    #endregion


    public void ShowMessage(string message)
    {
        message = message.Replace("\"", "'").Replace("\r\n", "\\r\\n").Replace("\n", "\\n");

        ScriptManager.RegisterStartupScript(this, this.GetType(), "ajaxMessageScript", "alert(\"" + message + "\");", true);
    }

    private string rtnUserInfo(string userId, string UserPwd)
    {
        string srtMsg = string.Empty;
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
        return srtMsg;
    }


}//END CLASS