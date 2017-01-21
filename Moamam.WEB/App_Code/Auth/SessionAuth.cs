using System;
using System.Web;
using System.Configuration;
using System.Diagnostics;

using Moamam.Data.Common;


/// <summary>
/// 세션을 이용한 인증 처리를 위한 Helper Function 보유
/// SessionAuthModule 을 Web.config 에 등록해야 페이지 인증처리를 한다.
/// </summary>
public class SessionAuth : System.Web.UI.Page
{
    public static string SessionAuthKey = ConfigurationManager.AppSettings["SysName"].ToString();
    public static string LoginPage = ConfigurationManager.AppSettings["LoginPage"].ToString();
    public static string DefaultPage = ConfigurationManager.AppSettings["DefaultPage"].ToString();
    public static string ReturnUrlKey = "returnUrl";
    public static string BaseAppPath = "";

    static SessionAuth()
    {
        BaseAppPath = HttpContext.Current.Request.ApplicationPath;
        //루트디렉토리가 아닐때는  /WebApplication1 과 같은 형태이므로
        // "/"를 추가해줌
        if (BaseAppPath != "/")
            BaseAppPath += "/";
    }

    #region LoginProcess
    //인증 처리를 위한 메서드
    //인증처리 후 초기 액세스 페이지 또는 기본 페이지로 이동시킴
    public static void LoginProcess(string userID, string userName,
        string userGroupCode, string userGroupName, string systemCode, string systemCodeDesc)
    {
        UserInfo ui = new UserInfo();
        ui.UserID = userID;
        ui.UserName = userName;
        ui.UserGroupCode = userGroupCode;
        ui.UserGroupName = userGroupName;

        LoginProcess(ui);
    }

    public static void LoginProcess(UserInfo ui)
    {
        //인증된 이후에는 기본 페이지 또는 처음 액세스하려했던
        //페이지로 이동시킴
        HttpContext.Current.Session[SessionAuthKey] = ui;

        /* 2015-12-17 인증완료 후 로그인 사용자 권한에 따라 첫페이지 이동 처리
        string retUrl = HttpContext.Current.Request.QueryString[ReturnUrlKey];

        if (retUrl == null || retUrl == "")
            HttpContext.Current.Response.Redirect(BaseAppPath + DefaultPage);
        else
            HttpContext.Current.Response.Redirect(retUrl);
         */
    }
    #endregion LoginProcess

    public static void LogoutProcess()
    {
        HttpContext.Current.Session.Abandon();

        HttpCookie myCookie = new HttpCookie("Moamam_Drug_Cookie");
        myCookie["UserID"] = null;
        myCookie["UserName"] = null;
        myCookie["UserGroupCode"] = null;
        myCookie["UserGroupName"] = null;

        myCookie.Expires = DateTime.Now.AddDays(-1d);
        //Response.Cookies.Add(myCookie);
        HttpContext.Current.Response.Cookies.Add(myCookie);
    }

    //[Conditional("DEBUG")]
    //public static void MakeDebuggingUser()
    //{
    //    //debug모드 시에 관리자권한으로 로그인정보를 만든다.
    //    if (HttpContext.Current.Session[SessionAuthKey] == null)
    //    {
    //        UserInfo ui = new UserInfo();
    //        ui.UserID = "ADMIN";
    //        ui.UserName = "ADMIN";
    //        ui.UserGroupCode = "1";
    //        ui.UserGroupName = "관리자";

    //        HttpContext.Current.Session[SessionAuthKey] = ui;
    //    }
    //}

    public static string GetUserID()
    {
        return GetUserInfo(UserInfoType.UserID);
    }

    public static string GetUserGorupCode()
    {
        return GetUserInfo(UserInfoType.UserGroupCode);
    }

    public static string GetUserInfo(UserInfoType uiType)
    {
        if (HttpContext.Current.Session == null)
            return "";

        //MakeDebuggingUser();

        if (HttpContext.Current.Session[SessionAuthKey] == null)
        {
            return "";     //인증되지 않았음을 의미
        }
        else
        {
            string retStr = "";
            UserInfo ui = (UserInfo)HttpContext.Current.Session[SessionAuthKey];
            switch (uiType)
            {
                case UserInfoType.UserID: retStr = ui.UserID; break;
                case UserInfoType.UserName: retStr = ui.UserName; break;
                case UserInfoType.UserGroupCode: retStr = ui.UserGroupCode; break;
                case UserInfoType.UserGroupName: retStr = ui.UserGroupName; break;
            }

            return retStr;
        }
    }

    //특정 경로의 페이지 실행에 대해 권한여부를 확인함.
    public static bool IsAccessByPage(string path, string usergroup_cd)
    {
        return true;
    }
}
