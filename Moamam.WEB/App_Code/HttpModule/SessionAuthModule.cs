using System;
using System.Web;
using System.IO;

using Moamam.Data.Common;

/// <summary>
/// 인증모듈(Form인증과 같은 동작을 한다)
/// Web.config 등록 예제 1 ==> <httpModules><add name="SessionAuth" type="RetailTechFramework.HttpModule.SessionAuthModule,RetailTechFrameworkV1"/></httpModules>
///            등록 예제 2 ==> <httpModules><add name="SessionAuth" type="SessionAuthModule,App_Code"/></httpModules>
/// </summary>
public class SessionAuthModule : IHttpModule
{
    public void Init(HttpApplication context)
    {
        context.AcquireRequestState += new EventHandler(context_AcquireRequestState);    
    }

    void context_AcquireRequestState(object sender, EventArgs e)
    {
        string path = HttpContext.Current.Request.Path;

        if (Path.GetExtension(path).ToUpper() != ".ASPX")
        { 
            return;
        }

        #region ################### data.aspx 에서는 login 체크 하지 않도록 ###################
        if (System.IO.Path.GetFileName(HttpContext.Current.Request.Url.LocalPath).ToString() == "Data.aspx"
            || System.IO.Path.GetFileName(HttpContext.Current.Request.Url.LocalPath).ToString() == "DrugInfo.aspx"
            || HttpContext.Current.Request.Url.LocalPath.ToString().ToLower() == "/default.aspx"
            )    // data.aspx 에서는 login 체크 하지 않도록
        {
            return;
        }
        #endregion ################### data.aspx 에서는 login 체크 하지 않도록 ###################

        string userGroup = string.Empty;
        try
        {
            userGroup = SessionAuth.GetUserInfo(UserInfoType.UserGroupCode);
        }
        catch (Exception ex) { }

        #region ###########################   세션이 없고 쿠키는 있을때 세션 유지용  ##################################
        HttpCookie myCookie = HttpContext.Current.Request.Cookies.Get("Moamam_Drug_Cookie");
        if (myCookie != null && myCookie["UserID"] != null && myCookie["UserGroupCode"] != null && string.IsNullOrEmpty(userGroup))
        {
            UserInfo ui = new UserInfo();
            ui.UserID = AES256.AESDecrypt256(myCookie["UserID"].ToString());
            ui.UserName = AES256.AESDecrypt256(myCookie["UserName"].ToString());
            ui.UserGroupCode = AES256.AESDecrypt256(myCookie["UserGroupCode"].ToString());
            ui.UserGroupName = AES256.AESDecrypt256(myCookie["UserGroupName"].ToString());
            SessionAuth.LoginProcess(ui);

            userGroup = myCookie["UserGroupCode"].ToString();
        }
        #endregion ###########################   세션이 없고 쿠키는 있을때 세션 유지용  ##################################


        //로그인 세션이 없으면
        if (string.IsNullOrEmpty(userGroup))
        {
            //로그인 페이지가 아니면 세션완료 처리후 로그인페이지로...
            if (path.Substring(SessionAuth.BaseAppPath.Length).ToUpper() != SessionAuth.LoginPage.ToUpper())
            {
                //HttpContext.Current.Response.Redirect(SessionAuth.BaseAppPath + SessionAuth.LoginPage);
                HttpContext.Current.Response.ContentType = "text/html; charset=utf-8";
                HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
                //HttpContext.Current.Response.Write("<script>alert('웹 세션이 만료되었습니다!');</script>");
                HttpContext.Current.Response.Write("<script>parent.location.href='" + SessionAuth.BaseAppPath + SessionAuth.LoginPage + "'</script>");
                HttpContext.Current.Response.End();

                return;
            }
        }

        if (!SessionAuth.IsAccessByPage(path, userGroup))
        {
            //(1)페이지에 대한 액세스 권한 없으므로 로그인 페이지 이동 
            //HttpContext.Current.Response.Redirect(SessionAuth.BaseAppPath + SessionAuth.LoginPage + "?" +
            //    SessionAuth.ReturnUrlKey + "=" + HttpContext.Current.Request.Path);
            
            //(2)페이지에 대한 액세스 권한 없음 메세지 display
            //HttpContext.Current.Response.ContentType = "text/html; charset=utf-8";
            //HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
            //HttpContext.Current.Response.Write("<script>alert('페이지 접근권한이 없습니다!');</script>");
            //HttpContext.Current.Response.End();
        }


        //팝업페이지는 제외
        /*
        if (HttpContext.Current.Request.Url.Segments[1].ToString().Replace("/", null) != "Default.aspx")
        {
            string pathFolderName = HttpContext.Current.Request.Url.Segments[2].ToString().Replace("/", null);

            if (HttpContext.Current.Request.UrlReferrer == null &&
               path.Substring(SessionAuth.BaseAppPath.Length).ToUpper() != SessionAuth.LoginPage.ToUpper() &&
               path.Substring(SessionAuth.BaseAppPath.Length).ToUpper() != SessionAuth.DefaultPage.ToUpper() &&
               pathFolderName != "PopupPage")
            {
                #if DEBUG
                //debug mode일 경우 skip
                #else
                HttpContext.Current.Response.ContentType = "text/html; charset=utf-8";
                HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
                HttpContext.Current.Response.Write("<script>alert('잘못된 페이지 접근입니다! 메뉴를 이용하여 이동해 주세요!'); history.back();</script>");
                HttpContext.Current.Response.End();
                #endif
            }
        }
        */
      
    }

    public void Dispose() { }
}
