using System;
using System.Collections;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Text.RegularExpressions; 

using Moamam.Data.Common;
using Moamam.Data.WebControls;
using System.Text;


public class BasePage : System.Web.UI.Page
{
    public int PageRowCount = 15;
    protected override void OnPreInit(EventArgs e)
    {
        base.OnPreInit(e);
    }

    protected override void OnInit(EventArgs e)
    {
        if(!IsPostBack)
        {
            if (System.IO.Path.GetFileName(Request.Url.LocalPath).ToString() != "Data.aspx" && System.IO.Path.GetFileName(Request.Url.LocalPath).ToString() != "DrugInfo.aspx")
            {
                Session["groupCd"] = Request["G"] != null ? Request["G"] : "0";
                Session["menuCd"] = Request["M"] != null ? Request["M"] : "0";
                if (Session["userID"] == null ) { Session["userID"] = SessionAuth.GetUserInfo(Moamam.Data.Common.UserInfoType.UserID).ToString(); }
            }
        }
        base.OnInit(e);
    }




    #region ===/ 권한 컨트롤 체크 /===================================================================

    private Hashtable _Security = null;
    public Hashtable SecurityInfo
    {
        get
        {
            if ((_Security == null && SessionAuth.GetUserGorupCode() != null) || (_Security.Count == 0))
            {
                _Security = CommonBiz.GetGrantData(Session["menuCd"].ToString(), SessionAuth.GetUserGorupCode());
            }

            return _Security;
        }
    }
    protected bool SecurityChecked(Control[] controls, Hashtable haSecu)
    {

        bool Return = false;

        if ((UserInfo)HttpContext.Current.Session[SessionAuth.SessionAuthKey] != null && haSecu.Count > 0)
        {

            foreach (Control control in controls)
            {

                if (control is NButton)
                {

                    NButton btn = (NButton)control;

                    if (haSecu.ContainsKey(btn.SecurityType.ToString().ToUpper()))
                    {

                        btn.Enabled = haSecu[btn.SecurityType.ToString().ToUpper()].ToString().Equals("0") ? false : true;
                        btn.Visible = haSecu[btn.SecurityType.ToString().ToUpper()].ToString().Equals("0") ? false : true;

                    }

                    else
                    {

                        btn.Enabled = false;
                        btn.Visible = false;

                    }

                }

                else if (control is NLinkButton)
                {

                    NLinkButton btn = (NLinkButton)control;



                    if (haSecu.ContainsKey(btn.SecurityType.ToString().ToUpper()))
                    {

                        btn.Enabled = haSecu[btn.SecurityType.ToString().ToUpper()].ToString().Equals("0") ? false : true;



                        if (!btn.Enabled) btn.Attributes["style"] = "color:#bbb";

                    }

                }

            }

            Return = haSecu["VIEW"].ToString().Equals("0") ? false : true;

        }

        return Return;

    }


    protected virtual void RedirectToSecurityError()
    {
        if ((UserInfo)HttpContext.Current.Session[SessionAuth.SessionAuthKey] != null)
        {
            string msg = "해당 페이지에 대한 권한이 없습니다.";
            //ShowMessage(msg);

            ScriptManager.RegisterStartupScript(this, this.GetType(), "ajaxMessageScript", string.Format("alert('{0}');history.back(-1);", msg), true);
        }
        else
        {
            SessionAuth.LogoutProcess();
            //Response.Redirect("~/Login/Login.aspx?", true);
            Response.Redirect("~/Default.aspx", true);
        }
    }

    #endregion ===/ 권한 컨트롤 체크 /===================================================================




    protected void DefaultEnter(Page page, Control control)
    {
        control.Focus();
    }

    public void ShowMessage(string message)
    {
        message = message.Replace("\"", "'").Replace("\r\n", "\\r\\n").Replace("\n", "\\n");

        ScriptManager.RegisterStartupScript(this, this.GetType(), "ajaxMessageScript", "alert(\"" + message + "\");", true);
    }

    protected internal object nEval(string expression)
    {
        string val = DataBinder.Eval(this.GetDataItem(), expression).ToString();
        if (!CheckXSS(val)) { 
            val = Server.HtmlEncode(val); 
        }
        return val;
    }


    public static bool CheckXSS(string inputParameter)
    {
        if (string.IsNullOrEmpty(inputParameter))
            return true;

        // Following regex convers all the js events and html tags mentioned in followng links.
        //https://www.owasp.org/index.php/XSS_Filter_Evasion_Cheat_Sheet                 
        //https://msdn.microsoft.com/en-us/library/ff649310.aspx

        var pattren = new StringBuilder();

        //Checks any js events i.e. onKeyUp(), onBlur(), alerts and custom js functions etc.             
        pattren.Append(@"((alert|on\w+|function\s+\w+)\s*\(\s*(['+\d\w](,?\s*['+\d\w]*)*)*\s*\))");

        //Checks any html tags i.e. <script, <embed, <object etc.
        pattren.Append(@"|(<(script|iframe|embed|frame|frameset|object|img|applet|body|html|style|layer|link|ilayer|meta|bgsound))");

        return !Regex.IsMatch(System.Web.HttpUtility.UrlDecode(inputParameter), pattren.ToString(), RegexOptions.IgnoreCase | RegexOptions.Compiled);
    }

    public string rtnXSS(string str)
    {
        if (!CheckXSS(str))
        {
            str = Server.HtmlEncode(str);
        }
        return str;
    }
}