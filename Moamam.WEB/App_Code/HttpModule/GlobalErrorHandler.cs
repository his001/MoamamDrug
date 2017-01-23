using System;
using System.Web;

using Moamam.Lib;

/// <summary>
/// 사이트 오류관리
/// Web.config 등록 예제 1 ==> <httpModules><add name="SessionAuth" type="RetailTechFramework.HttpModule.GlobalErrorHandler,RetailTechFrameworkV1"/></httpModules>
///            등록 예제 2 ==> <httpModules><add name="GlobalErrorHandler" type="GlobalErrorHandler,App_Code"/></httpModules>
/// </summary>
public class GlobalErrorHandler : IHttpModule
{
    public void Init(HttpApplication context)
    {
        context.Error += new EventHandler(context_Error);
    }

    //void context_Error(object sender, EventArgs e)
    //{
    //    // At this point we have information about the error
    //    HttpContext ctx = HttpContext.Current;
    //    HttpResponse response = ctx.Response;
    //    HttpRequest request = ctx.Request;

    //    Exception exception = ctx.Server.GetLastError();

    //    response.Write("페이지에 오류가 있습니다!");


    //    response.Write("<p>-------------------------<p/><p>ErrorInfo:<p/>");
    //    response.Write("URL: " + AntiHack.rtnXSS(ctx.Request.Url.ToString()));
    //    response.Write("<br/>Error Message: " + exception.InnerException.Message);

    //    //create LOG
    //    string queryStringInfo = "";
    //    for (int i = 0; i < request.QueryString.Count; i++)
    //    {
    //        queryStringInfo += request.QueryString.Keys[i].ToString() + "=" + request.QueryString[i].ToString() + "|";
    //    }

    //    string errorInfo = 
    //        "URL: " + ctx.Request.Url.ToString() + 
    //        "\r\nStacktrace:---\r\n" + exception.InnerException.StackTrace.ToString() + 
    //        "\r\nError Message:\r\n" + exception.InnerException.Message;


    //    SysLogger.WriteLog("ErrorInfo:\r\n" + errorInfo);

    //    // --------------------------------------------------
    //    // To let the page finish running we clear the error
    //    // --------------------------------------------------
    //    ctx.Server.ClearError();
    //}

    void context_Error(object sender, EventArgs e)
    {

        string errorInfo = string.Empty;
        HttpContext ctx = HttpContext.Current;
        HttpResponse response = ctx.Response;
        HttpRequest request = ctx.Request;

        Exception exception = ctx.Server.GetLastError();
        try
        {
            if (exception.InnerException != null)
            {
                errorInfo =
                    "URL: " + ctx.Request.Url.ToString() +
                    "\r\nStacktrace:---\r\n" + exception.InnerException.StackTrace.ToString() +
                    "\r\nError Message:\r\n" + exception.InnerException.Message;
            }
            else
            {
                errorInfo =
                    "URL: " + ctx.Request.Url.ToString() +
                    "\r\nStacktrace:---\r\n" + exception.StackTrace.ToString() +
                    "\r\nError Message:\r\n" + exception.Message;
            }

            SysLogger.WriteLog("ErrorInfo:\r\n" + errorInfo);

            //if (SessionAuth.GetUserInfo(UserInfoType.UserGroupName) == "개발자")
            //{
            //    response.Write("페이지에 오류가 있습니다!");
            //    response.Write("<p>-------------------------<p/><p>ErrorInfo:<p/>");
            //    response.Write("URL: " + ctx.Request.Url.ToString());
            //    response.Write("<br/>Error Message: " + exception.Message);
            //}
            //else
            //{
            //HttpContext.Current.Response.Redirect("/Common/Error.aspx");
            //response.End();
            //}

            // --------------------------------------------------
            // To let the page finish running we clear the error
            // --------------------------------------------------
            ctx.Server.ClearError();

        }
        catch (Exception ex) { }

        HttpContext.Current.Response.Redirect("/Error.aspx");
        response.End();
    }


    public void Dispose() { }
}
