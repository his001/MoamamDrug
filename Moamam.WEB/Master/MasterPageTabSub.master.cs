using System;


public partial class Master_MasterPageTabSub : System.Web.UI.MasterPage
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

            //LoadMenu();
        }

    }

    #endregion


    #region Data Routine **********************************************************************************************

    #endregion


}