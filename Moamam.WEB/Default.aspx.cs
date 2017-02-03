using Moamam.Lib;
using System;

public partial class _Default : System.Web.UI.Page
{
    protected string _regip = string.Empty;
    protected string _regiD = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //SessionAuth.LogoutProcess();
            //Response.Redirect("~/LogIn/LogIn.aspx");
            _regip = CommonNet.getUserIP();
            //_regiD = SessionAuth.GetUserInfo(Moamam.Data.Common.UserInfoType.UserID).ToString();
        }
    }
}