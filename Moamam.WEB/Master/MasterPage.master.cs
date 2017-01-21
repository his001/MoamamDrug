using Moamam.Lib;
using System;
using System.Data;
using System.Web.UI.WebControls;
using System.IO;
using AESWeb;

using Moamam.Data.Common;

public partial class Master_MasterPage : System.Web.UI.MasterPage
{

    #region Page Events **********************************************************************************************

    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            lblUpdateDaytime.Text = GetUpdateDate(); //최종업데이트 시간 가져오기

            this.lblUserSession.Text = string.Format(
                                                      "{0}(<font color='red'>{1}</font>)"
                                                    , SessionAuth.GetUserInfo(UserInfoType.UserName)
                                                    , SessionAuth.GetUserInfo(UserInfoType.UserGroupName)
                                                    );

            LoadMenu();
        }

    }

    #endregion


    #region PostBack Events **********************************************************************************************

    protected void rptMenu_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
        {
            DataRowView row = e.Item.DataItem as DataRowView;

            Repeater rptSubmenu = (Repeater)e.Item.FindControl("rptSubmenu");

            var menuCode = row["MENU_GROUP_CD"].ToString();

            if (rptSubmenu != null)
            {
                rptSubmenu.DataSource = CommonBiz.GetUserMenuSub(row["MENU_GROUP_CD"].ToString());
                rptSubmenu.DataBind();
            }
        }
    }

    protected void rptSubmenu_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
        {
            DataRowView row = e.Item.DataItem as DataRowView;

            HyperLink hlnSubmenu = (HyperLink)e.Item.FindControl("hlnSubmenu");
            //hlnSubmenu.NavigateUrl = "/" + row["MENU_URL"].ToString() +
            //    "?G=" + Server.UrlEncode(row["MENU_GROUP_CD"].ToString()) +
            //    "&M=" + Server.UrlEncode(row["MENU_CD"].ToString());
            hlnSubmenu.NavigateUrl = row["MENU_URL"].ToString();
        }
    }

    protected void btnLogOut_Click(object sender, EventArgs e)
    {
        SessionAuth.LogoutProcess();
        Response.Redirect("~/Login/Login.aspx", true);
    }
    
    #endregion


    #region Data Routine **********************************************************************************************
    private string GetUpdateDate()
    {
        string temp;
        DataSet ds = CommonBiz.GetUpdateDate();
        if (ds.Tables[0].Rows.Count > 0)
        {
            temp = "최종 업데이트 : " + string.Format("{0}", ds.Tables[0].Rows[0]["UPDATEDATE"].ToString()) + "&nbsp;&nbsp;";
        }
        else
        {
            temp = "최종 업데이트 : 업데이트되지 않았습니다.&nbsp;&nbsp;";
        }
        return temp;
    }
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
            // 접근권한 없는 페이지 강제 URL 입력 시 전페이지로 이동
            string strPageName = string.Empty;
            DataSet ds = CommonBiz.GetUserUseMenuList(strUserGroupCode);
            for (int mCnt = 0; mCnt <= ds.Tables[0].Rows.Count - 1; mCnt++)
            {
                strPageName = ds.Tables[0].Rows[mCnt]["MENU_URL"].ToString().Split('/')[2].Trim();

                if (Path.GetFileName(Request.Url.AbsolutePath) == strPageName)
                {
                    Response.Write("<script>alert('잘못된 페이지 접근입니다! 메뉴를 이용하여 이동해 주세요!'); history.back();</script>");
                    Response.End();
                    break;
                }
            }

            rptMenu.DataSource = CommonBiz.GetUserMenuGroup(strUserGroupCode);
            rptMenu.DataBind();

            MenuPath();
        }
    }

    private void MenuPath()
    {
        string path = Request.RawUrl;

        DataSet ds = (new SiteMenu()).GetPraMenu(Session["groupCd"].ToString(), Session["menuCd"].ToString());

        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            lblpath.Text = AntiHack.rtnXSS(ds.Tables[0].Rows[0]["MENU_GROUP_NM"].ToString());
            lblCurrentNoade.Text = AntiHack.rtnXSS(ds.Tables[0].Rows[0]["MENU_NM"].ToString());
        }
    }

    #endregion


}