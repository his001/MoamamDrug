using Moamam.Lib;
using System;
using System.Data;
using System.Web.UI.WebControls;
using System.IO;

using Moamam.Data.Common;

public partial class Master_MasterPage_Modal : System.Web.UI.MasterPage
{

    #region Page Events **********************************************************************************************

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //this.lblUserSession.Text = string.Format(
            //        "{0}(<font color='red'>{1}</font>)"
            //    , SessionAuth.GetUserInfo(UserInfoType.UserName)
            //    , SessionAuth.GetUserInfo(UserInfoType.UserGroupName)
            //    );

            LoadMenu();
        }

    }

    #endregion


    #region Data Routine **********************************************************************************************

    void LoadMenu()
    {
        string strUserGroupCode = SessionAuth.GetUserGorupCode();

        if (strUserGroupCode == "")
        {
            SessionAuth.LogoutProcess();
            Response.Redirect("~/Login/Login.aspx", true);
        }
        else
        {
            
        }
    }


    #endregion


}