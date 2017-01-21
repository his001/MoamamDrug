using System;
using System.Web;

using Moamam.Lib;

/// <summary>
/// ����Ʈ ��������
/// Web.config ��� ���� 1 ==> <httpModules><add name="SessionAuth" type="RetailTechFramework.HttpModule.GlobalErrorHandler,RetailTechFrameworkV1"/></httpModules>
///            ��� ���� 2 ==> <httpModules><add name="GlobalErrorHandler" type="GlobalErrorHandler,App_Code"/></httpModules>
/// </summary>
public class GlobalErrorHandler : IHttpModule
{
    public void Init(HttpApplication context)
    {
        context.Error += new EventHandler(context_Error);
    }

    void context_Error(object sender, EventArgs e)
    {
        // At this point we have information about the error
        HttpContext ctx = HttpContext.Current;
        HttpResponse response = ctx.Response;
        HttpRequest request = ctx.Request;

        Exception exception = ctx.Server.GetLastError();

        response.Write("�������� ������ �ֽ��ϴ�!");


        response.Write("<p>-------------------------<p/><p>ErrorInfo:<p/>");
        response.Write("URL: " + AntiHack.rtnXSS(ctx.Request.Url.ToString()));
        response.Write("<br/>Error Message: " + exception.InnerException.Message);

        //create LOG
        string queryStringInfo = "";
        for (int i = 0; i < request.QueryString.Count; i++)
        {
            queryStringInfo += request.QueryString.Keys[i].ToString() + "=" + request.QueryString[i].ToString() + "|";
        }

        string errorInfo = 
            "URL: " + ctx.Request.Url.ToString() + 
            "\r\nStacktrace:---\r\n" + exception.InnerException.StackTrace.ToString() + 
            "\r\nError Message:\r\n" + exception.InnerException.Message;


        SysLogger.WriteLog("ErrorInfo:\r\n" + errorInfo);

        // --------------------------------------------------
        // To let the page finish running we clear the error
        // --------------------------------------------------
        ctx.Server.ClearError();
    }

    public void Dispose() { }
}
