using System;
using System.Web.UI.WebControls;
using Moamam.Lib;
using System.Data;

using Moamam.Data.Common;
using Moamam.Data.Site.Management;


public partial class Site_Management_MenuList : BasePage
{

    #region #######
    string GCD = string.Empty;
    string param_G = string.Empty;
    string param_G_Org = string.Empty;
    string param_M = string.Empty;
    string param_M_Org = string.Empty;
    #endregion #######

    #region Page_Load
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request["GCD"] != null) { GCD = Request["GCD"].ToString(); }
        if (Request["G"] != null) { param_G = Request["G"]; }
        if (Request["M"] != null) { param_M = Request["M"]; }

        if (!IsPostBack)
        {
            //사용자권한 셀렉트박스 목록 생성
            DataSet ds = (new SiteUser()).GetUserGroupList();

            if (ds != null)
            {
                //대메뉴 팝업영역
                ddlPopUserAuthCd.DataTextField = "SUB_CD_NM";
                ddlPopUserAuthCd.DataValueField = "SUB_CD";
                ddlPopUserAuthCd.DataSource = ds;
                ddlPopUserAuthCd.DataBind();
            }

            //데이터 조회
            getData();

            if (GCD != string.Empty)
            {
                getMenuData(GCD);
            }
        }
    }
    #endregion Page_Load


    #region 대메뉴 리스트 호출
    //조회 버튼 클릭
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        ucPaging.PageNo = 1;
        getData();
    }


    //리스트 호출
    private void getData()
    {
        try
        {
            DataSet ds = null;
            DataTable dt = null;

            ds = (new MenuList()).GetMenuGroupList(ucPaging.RowCount, ucPaging.PageNo);

            if (ds != null)
            {
                rptMenuGroupList.DataSource = ds.Tables[0];
                rptMenuGroupList.DataBind();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ucPaging.TotalCount = Convert.ToInt32(ds.Tables[0].Rows[0]["TOTAL_COUNT"].ToString()); //목록 Total 갯수 저장 
                }

                rptMenuList.DataSource = dt;
                rptMenuList.DataBind();
                ucPaging2.PageNo = 1;
                ucPaging2.TotalCount = 0;
            }
        }
        catch (Exception ex)
        {
            base.ShowMessage(ex.Message);
        }
    }



    protected void rptMenuGroupList_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {

                //메뉴 수정
                if (e.CommandName == "mainGroupSelect")
                {
                    string[] arrStrArgument = e.CommandArgument.ToString().Split(',');
                    string strGroupCd = arrStrArgument[0].ToString();             //대메뉴코드
                    string strGroupNm = arrStrArgument[1].ToString();             //메뉴명
                    string strUserAuthCd = arrStrArgument[2].ToString();             //메뉴권한
                    string strUseYn = arrStrArgument[3].ToString();             //사용여부(Y:사용, N:미사용)

                    hdnMainGroupCd.Value = strGroupCd;
                    txtPopGroupNm.Text = strGroupNm;
                    ddlPopUserAuthCd.SelectedValue = strUserAuthCd;
                    ddlPopUseYn.SelectedValue = strUseYn;
                    btnDelete.Visible = true;
                    hdnMainGroupAddType.Value = "update";

                    ModalPopExt1.Show();
                }
                else if (e.CommandName == "menuSelect")
                {
                    string strGroupCd = e.CommandArgument.ToString(); //대메뉴코드
                    hdnParentGroupCd.Value = strGroupCd;

                    //하위메뉴 리스트 호출
                    getMenuData(strGroupCd);

                    //getDdlUseParentMenu(strGroupCd);
                    setAuthClear(); // 권한 data 를 초기화 하기 위해
                }
                else if (e.CommandName == "menuSortUp")
                {
                    string strGroupCd = e.CommandArgument.ToString(); //대메뉴코드
                    hdnParentGroupCd.Value = strGroupCd;
                    //하위메뉴 리스트 호출
                    setMenuSortUpDown(strGroupCd, "UP");
                    SetMasterMenuReload("");
                }
                else if (e.CommandName == "menuSortDown")
                {
                    string strGroupCd = e.CommandArgument.ToString(); //대메뉴코드
                    hdnParentGroupCd.Value = strGroupCd;
                    //하위메뉴 리스트 호출
                    setMenuSortUpDown(strGroupCd, "DOWN");
                    SetMasterMenuReload("");
                }
            }
        }
        catch (Exception ex)
        {
            base.ShowMessage(ex.Message);
        }
    }

    protected void setMenuSortUpDown(string strGroupCd, string strUpDown)
    {
        try
        {
            string strMsg = (new MenuList()).SetMenuGroupUpDown(strGroupCd, strUpDown, SessionAuth.GetUserInfo(UserInfoType.UserID).ToString());
            if (strMsg == "OK")
            {
                base.ShowMessage("저장되었습니다");
                ModalPopExt1.Hide();

                SetMasterMenuReload("");
                //getData();
            }
            else
                base.ShowMessage(strMsg);
        }
        catch (Exception ex)
        {
            base.ShowMessage(ex.Message);
        }
    }

    protected void setPageSortUpDown(string strGroupCd, string strMenuCd, string strUpDown)
    {
        try
        {
            string strMsg = (new MenuList()).SetPageGroupUpDown(strGroupCd, strMenuCd, strUpDown, SessionAuth.GetUserInfo(UserInfoType.UserID).ToString());
            if (strMsg == "OK")
            {
                base.ShowMessage("저장되었습니다");
                ModalPopExt1.Hide();

                SetMasterMenuReload(strGroupCd);
            }
            else
                base.ShowMessage(strMsg);
        }
        catch (Exception ex)
        {
            base.ShowMessage(ex.Message);
        }
    }


    //페이징 조회
    protected void ucPaging_SelEvent(object sender, EventArgs e)
    {
        getData();
    }
    #endregion 대메뉴 리스트 호출


    #region 대메뉴 레이어 팝업
    //등록 버튼 클릭
    protected void btnInsert_Click(object sender, EventArgs e)
    {
        try
        {
            ClearPopup();

            btnDelete.Visible = false;
            hdnMainGroupAddType.Value = "insert";
            ModalPopExt1.Show();
        }
        catch (Exception ex)
        {
            base.ShowMessage(ex.Message);
        }
    }



    //저장 버튼 클릭
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            if (hdnMainGroupAddType.Value == "insert")
            {
                SetMainGroupInsert();
            }
            else if (hdnMainGroupAddType.Value == "update")
            {
                SetMainGroupUpdate();
            }
        }
        catch (Exception ex)
        {
            base.ShowMessage(ex.Message);
        }
    }



    //대메뉴 신규 등록
    protected void SetMainGroupInsert()
    {
        try
        {
            string strMsg = (new MenuList()).SetMenuGroupInsert(AntiHack.rtnSQLInj(txtPopGroupNm.Text.Trim()), 
                ddlPopUserAuthCd.SelectedValue.Trim(), ddlPopUseYn.SelectedValue.Trim(), SessionAuth.GetUserInfo(UserInfoType.UserID).ToString());

            if (strMsg == "OK")
            {
                base.ShowMessage("저장되었습니다");
                ModalPopExt1.Hide();

                SetMasterMenuReload("");
                //getData();
            }
            else
                base.ShowMessage(strMsg);
        }
        catch (Exception ex)
        {
            base.ShowMessage(ex.Message);
        }
    }



    //대메뉴 수정
    protected void SetMainGroupUpdate()
    {
        try
        {
            string strMsg = (new MenuList()).SetMenuGroupUpdate(
                AntiHack.rtnSQLInj(hdnMainGroupCd.Value), AntiHack.rtnSQLInj(txtPopGroupNm.Text.Trim()), ddlPopUserAuthCd.SelectedValue.Trim()
                , ddlPopUseYn.SelectedValue.Trim(), SessionAuth.GetUserInfo(UserInfoType.UserID).ToString());

            if (strMsg == "OK")
            {
                base.ShowMessage("저장되었습니다");
                ModalPopExt1.Hide();

                SetMasterMenuReload(hdnMainGroupCd.Value);
                //getData();
            }
            else
                base.ShowMessage(strMsg);
        }
        catch (Exception ex)
        {
            base.ShowMessage(ex.Message);
        }
    }



    //대메뉴 삭제 버튼 클릭
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            string strMsg = (new MenuList()).SetMenuGroupDelete(hdnMainGroupCd.Value);

            if (strMsg == "OK")
            {
                base.ShowMessage("삭제되었습니다");
                ModalPopExt1.Hide();

                SetMasterMenuReload(hdnMainGroupCd.Value);
                //getData();
            }
            else
                base.ShowMessage(strMsg);
        }
        catch (Exception ex)
        {
            base.ShowMessage(ex.Message);
        }
    }



    //대메뉴 닫기 버튼 클릭
    protected void btnExit_Click(object sender, EventArgs e)
    {
        ModalPopExt1.Hide();
    }



    //대메뉴 팝업창 초기화
    private void ClearPopup()
    {
        txtPopGroupNm.Text = string.Empty;
        ddlPopUserAuthCd.SelectedIndex = 0;
        ddlPopUseYn.SelectedIndex = 0;
    }
    #endregion 대메뉴 레이어 팝업


    #region 하위메뉴 리스트 호출
    //조회 버튼 클릭
    protected void btnSearch2_Click(object sender, EventArgs e)
    {
        ucPaging2.PageNo = 1;
        getMenuData(hdnParentGroupCd.Value);
    }


    //리스트 호출
    private void getMenuData(string strGroupCd)
    {
        try
        {
            DataSet ds = null;
            ds = (new MenuList()).GetMenuList(strGroupCd, ucPaging2.RowCount, ucPaging2.PageNo);

            hdnParentGroupCd.Value = strGroupCd;

            getDdlUseParentMenu(strGroupCd);
            if (ds != null)
            {
                rptMenuList.DataSource = ds.Tables[0];
                rptMenuList.DataBind();
                setAuthClear(); // 권한 data 를 초기화 하기 위해

                if (ds.Tables[0].Rows.Count > 0)
                {
                    ucPaging2.TotalCount = Convert.ToInt32(ds.Tables[0].Rows[0]["TOTAL_COUNT"].ToString()); //목록 Total 갯수 저장 
                }
                else
                {
                    ucPaging2.TotalCount = 0;
                }
            }
        }
        catch (Exception ex)
        {
            base.ShowMessage(ex.Message);
        }
    }

    private void getDdlUseParentMenu(string _groupCd)
    {

        try
        {

            DataSet ds = null;

            ds = (new MenuList()).GetddlUseParentMenu(_groupCd);

            if (ds != null)
            {

                ddlUseParentMenu.DataTextField = "MENU_NM";

                ddlUseParentMenu.DataValueField = "MENU_CD";

                ddlUseParentMenu.DataSource = ds;

                ddlUseParentMenu.DataBind();

            }

        }

        catch (Exception ex)
        {

            base.ShowMessage(ex.Message);

        }

    }

    protected void rptMenuList_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                //메뉴 수정
                if (e.CommandName == "Select")
                {
                    string[] arrStrArg = e.CommandArgument.ToString().Split(',');
                    string strGroupCd = arrStrArg[0].ToString();                  //그룹코드
                    string strMenuCd = arrStrArg[1].ToString();                  //메뉴코드
                    string strMenuNm = arrStrArg[2].ToString();                  //메뉴명
                    string strMenuUrl = arrStrArg[3].ToString();                  //메뉴경로
                    string strUseYn = arrStrArg[4].ToString();                  //메뉴사용여부(Y:사용, N:미사용)

                    string parentCd = arrStrArg[5].ToString();                  //부모메뉴코드

                    string useParentCd = arrStrArg[6].ToString();                  //부모메뉴를 사용하고 있는지의 여부

                    hdnNowMenuCd.Value = strMenuCd;
                    txtPopMenuNm.Text = strMenuNm;
                    txtPopMenuUrl.Text = strMenuUrl;
                    ddlPopMenuUseYn.SelectedValue = strUseYn;

                    ddlUseParentMenu.SelectedValue = parentCd;



                    if (!string.IsNullOrEmpty(useParentCd))
                    {

                        ddlUseParentMenu.Enabled = false;

                    }

                    else
                    {

                        ddlUseParentMenu.Enabled = true;
                    }

                    btnDelete2.Visible = true;
                    hdnMenuAddType2.Value = "update";

                    //레이어팝업 호출
                    ModalPopExt2.Show();
                }
                else if (e.CommandName == "authSelect")
                {
                    string[] arrStrArg = e.CommandArgument.ToString().Split(',');
                    string strGroupCd = arrStrArg[0].ToString();                  //그룹코드
                    string strMenuCd = arrStrArg[1].ToString();                  //메뉴코드

                    hdnParentGroupCd.Value = strGroupCd;
                    hdnNowMenuCd.Value = strMenuCd;

                    string struser_group = hdnUSER_AUTH_CD.Value;
                    string strMsg = (new MenuList()).setExistschkAuthData(strGroupCd, strMenuCd);

                    //하위메뉴 리스트 호출
                    getMenuData3(strGroupCd, strMenuCd);
                }
                else if (e.CommandName == "PageSortUp")
                {
                    string[] arrStrArg = e.CommandArgument.ToString().Split(',');
                    string strGroupCd = arrStrArg[0].ToString();                  //그룹코드
                    string strMenuCd = arrStrArg[1].ToString();                  //메뉴코드

                    hdnParentGroupCd.Value = strGroupCd;
                    hdnNowMenuCd.Value = strMenuCd;
                    //하위메뉴 리스트 호출
                    setPageSortUpDown(strGroupCd, strMenuCd, "UP");
                    //SetMasterMenuReload("");
                }
                else if (e.CommandName == "PageSortDown")
                {
                    string[] arrStrArg = e.CommandArgument.ToString().Split(',');
                    string strGroupCd = arrStrArg[0].ToString();                  //그룹코드
                    string strMenuCd = arrStrArg[1].ToString();                  //메뉴코드

                    hdnParentGroupCd.Value = strGroupCd;
                    hdnNowMenuCd.Value = strMenuCd;
                    //하위메뉴 리스트 호출
                    setPageSortUpDown(strGroupCd, strMenuCd, "DOWN");
                    //SetMasterMenuReload("");
                }
            }
        }
        catch (Exception ex)
        {
            base.ShowMessage(ex.Message);
        }
    }

    //페이징 조회
    protected void ucPaging_SelEvent2(object sender, EventArgs e)
    {
        getMenuData(hdnParentGroupCd.Value);
    }
    #endregion 하위메뉴 리스트 호출


    #region 하위메뉴 레이어 팝업
    //등록 버튼 클릭
    protected void btnInsert2_Click(object sender, EventArgs e)
    {
        try
        {
            ClearPopup2();

            btnDelete2.Visible = false;
            hdnMenuAddType2.Value = "insert";
            ModalPopExt2.Show();
        }
        catch (Exception ex)
        {
            base.ShowMessage(ex.Message);
        }
    }



    //저장 버튼 클릭
    protected void btnAdd2_Click(object sender, EventArgs e)
    {
        try
        {
            if (hdnMenuAddType2.Value == "insert")
            {
                SetMenuInsert();
            }
            else if (hdnMenuAddType2.Value == "update")
            {
                SetMenuUpdate();
            }
        }
        catch (Exception ex)
        {
            base.ShowMessage(ex.Message);
        }
    }



    //하위메뉴 신규 등록
    protected void SetMenuInsert()
    {
        try
        {

            string strMsg = (new MenuList()).SetMenuInsert(AntiHack.rtnSQLInj(hdnParentGroupCd.Value), AntiHack.rtnSQLInj(txtPopMenuNm.Text)
                , AntiHack.rtnSQLInj(txtPopMenuUrl.Text), AntiHack.rtnSQLInj(ddlPopMenuUseYn.SelectedValue), SessionAuth.GetUserInfo(UserInfoType.UserID).ToString(), ddlUseParentMenu.SelectedValue);

            if (strMsg == "OK")
            {
                base.ShowMessage("등록되었습니다");
                ModalPopExt2.Hide();

                SetMasterMenuReload(hdnParentGroupCd.Value);
                //getMenuData(hdnParentGroupCd.Value);
            }
            else
                base.ShowMessage(strMsg);
        }
        catch (Exception ex)
        {
            base.ShowMessage(ex.Message);
        }
    }



    //하위메뉴 수정
    protected void SetMenuUpdate()
    {
        try
        {

            string strMsg = (new MenuList()).SetMenuUpdate(AntiHack.rtnSQLInj(hdnParentGroupCd.Value), AntiHack.rtnSQLInj(hdnNowMenuCd.Value), AntiHack.rtnSQLInj(txtPopMenuNm.Text)
                , AntiHack.rtnSQLInj(txtPopMenuUrl.Text), ddlPopMenuUseYn.SelectedValue, SessionAuth.GetUserInfo(UserInfoType.UserID).ToString(), ddlUseParentMenu.SelectedValue);

            if (strMsg == "OK")
            {
                base.ShowMessage("등록되었습니다");
                ModalPopExt2.Hide();

                SetMasterMenuReload(hdnParentGroupCd.Value);
                //getMenuData(hdnParentGroupCd.Value);
            }
            else
                base.ShowMessage(strMsg);
        }
        catch (Exception ex)
        {
            base.ShowMessage(ex.Message);
        }
    }


    //하위메뉴 삭제 버튼 클릭
    protected void btnDelete2_Click(object sender, EventArgs e)
    {
        try
        {
            string strMsg = (new MenuList()).SetMenuDelete(hdnParentGroupCd.Value, hdnNowMenuCd.Value);

            if (strMsg == "OK")
            {
                base.ShowMessage("삭제되었습니다");
                ModalPopExt2.Hide();

                SetMasterMenuReload(hdnParentGroupCd.Value);
                //getMenuData(hdnParentGroupCd.Value);
            }
            else
                base.ShowMessage(strMsg);
        }
        catch (Exception ex)
        {
            base.ShowMessage(ex.Message);
        }
    }


    //하위메뉴 닫기 버튼 클릭
    protected void btnExit2_Click(object sender, EventArgs e)
    {
        ModalPopExt2.Hide();
    }


    //하위메뉴 팝업창 초기화
    private void ClearPopup2()
    {
        txtPopMenuNm.Text = string.Empty;
        txtPopMenuUrl.Text = string.Empty;
        ddlPopMenuUseYn.SelectedIndex = 0;
    }
    #endregion 하위메뉴 레이어 팝업


    #region ################### 3번째 하위메뉴 리스트 호출
    //조회 버튼 클릭
    protected void btnSearch3_Click(object sender, EventArgs e)
    {
        ucPaging3.PageNo = 1;
        getMenuData3(hdnParentGroupCd.Value, hdnNowMenuCd.Value);
    }


    //리스트 호출
    private void getMenuData3(string strGroupCd, string strMenuCd)
    {
        try
        {
            DataSet ds = null;
            ds = (new MenuList()).GetMenuList3(strGroupCd, strMenuCd, ucPaging3.RowCount, ucPaging3.PageNo);
            if (ds != null)
            {
                rptAuthList.DataSource = ds.Tables[0];
                rptAuthList.DataBind();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ucPaging3.TotalCount = Convert.ToInt32(ds.Tables[0].Rows[0]["TOTAL_COUNT"].ToString()); //목록 Total 갯수 저장 
                }
                else
                {
                    ucPaging3.TotalCount = 0;
                }
            }
        }
        catch (Exception ex)
        {
            base.ShowMessage(ex.Message);
        }
    }


    protected void rptAuthList_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                //메뉴 수정
                if (e.CommandName == "Select")
                {
                    //string[] arrStrArg = e.CommandArgument.ToString().Split(',');
                    //string strGroupCd = arrStrArg[0].ToString();                  //그룹코드
                    //string strMenuCd = arrStrArg[1].ToString();                  //메뉴코드
                    //user_group
                }
            }
        }
        catch (Exception ex)
        {
            base.ShowMessage(ex.Message);
        }
    }



    //페이징 조회
    protected void ucPaging_SelEvent3(object sender, EventArgs e)
    {
        getMenuData3(hdnParentGroupCd.Value, hdnNowMenuCd.Value);
    }

    protected void btnInsert3_Click(object sender, EventArgs e)
    {
        try
        {
            SetAuthSave();
        }
        catch (Exception ex)
        {
            base.ShowMessage(ex.Message);
        }
    }

    protected void SetAuthSave()
    {
        try
        {
            string txt_user_group = string.Empty;
            string grant_v = string.Empty;
            string grant_i = string.Empty;
            string grant_s = string.Empty;
            string grant_r = string.Empty;
            string grant_a = string.Empty;
            string strMsg = string.Empty;
            int strRuncnt = 0;
            int strErrcnt = 0;

            foreach (RepeaterItem ritem in rptAuthList.Items)
            {
                TextBox user_group = (TextBox)ritem.FindControl("user_group"); txt_user_group = user_group.Text;
                CheckBox chk_grant_v = (CheckBox)ritem.FindControl("chk_grant_v"); if (chk_grant_v.Checked) { grant_v = "1"; } else { grant_v = "0"; };
                CheckBox chk_grant_i = (CheckBox)ritem.FindControl("chk_grant_i"); if (chk_grant_i.Checked) { grant_i = "1"; } else { grant_i = "0"; };
                CheckBox chk_grant_s = (CheckBox)ritem.FindControl("chk_grant_s"); if (chk_grant_s.Checked) { grant_s = "1"; } else { grant_s = "0"; };
                CheckBox chk_grant_r = (CheckBox)ritem.FindControl("chk_grant_r"); if (chk_grant_r.Checked) { grant_r = "1"; } else { grant_r = "0"; };
                CheckBox chk_grant_a = (CheckBox)ritem.FindControl("chk_grant_a"); if (chk_grant_a.Checked) { grant_a = "1"; } else { grant_a = "0"; };

                //string groupCd, string menuCd, string usergroup, string grant_i, string grant_s, string grant_r, string grant_a, string createUser
                strMsg = (new MenuList()).SetAuthSave(hdnParentGroupCd.Value, hdnNowMenuCd.Value, txt_user_group, grant_v, grant_i, grant_s, grant_r, grant_a, SessionAuth.GetUserInfo(UserInfoType.UserID).ToString());
                strRuncnt++;
                if (strMsg != "OK")
                {
                    strErrcnt++;
                }
            }
            if (strErrcnt != 0)
            {
                base.ShowMessage(strRuncnt.ToString() + " 건 중" + strErrcnt + "건 에러가 발생 하였습니다");
            }
            else
            {
                base.ShowMessage(strRuncnt.ToString() + " 건 처리 되었습니다");
            }
            //SetMasterMenuReload(hdnParentGroupCd.Value);
            //최하위메뉴 리스트 호출
            getMenuData3(hdnParentGroupCd.Value, hdnNowMenuCd.Value);
            //base.ShowMessage(strMsg);
        }
        catch (Exception ex)
        {
            base.ShowMessage(ex.Message);
        }
    }

    /// <summary>
    /// 권한 data 를 초기화 하기 위해
    /// </summary>
    private void setAuthClear()
    {
        // 권한 data 를 초기화 하기 위해
        ucPaging3.PageNo = 1;
        ucPaging3.TotalCount = 0;
        rptAuthList.DataSource = null;
        rptAuthList.DataBind();

    }

    #endregion 3번째 하위메뉴 리스트 호출





    protected void rptMenuList_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {



        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {

            DataRowView view = (DataRowView)e.Item.DataItem;

            LinkButton lnkGroupNm = (LinkButton)e.Item.FindControl("lnkGroupNm");

            LinkButton lnkPageSortUp = (LinkButton)e.Item.FindControl("lnkPageSortUp");

            LinkButton lnkPageSortDown = (LinkButton)e.Item.FindControl("lnkPageSortDown");



            string parentCd = view["PARENT_CD"].ToString();



            if (string.IsNullOrEmpty(parentCd))
            {

                lnkGroupNm.Text = "&nbsp;" + view["MENU_NM"].ToString();

            }

            else
            {

                lnkGroupNm.Text = "&nbsp;&nbsp;&nbsp;&nbsp;ㄴ" + view["MENU_NM"].ToString(); ;

                lnkPageSortUp.Text = "";

                lnkPageSortDown.Text = "";

            }

        }

    }

    #region 기능함수모음
    //마스터페이지 메뉴 초기화
    private void SetMasterMenuReload(string strGroupCd)
    {
        //마스터페이지 메뉴캐시 초기화
        Response.Redirect("MenuList.aspx?G=" + param_G_Org + "&M=" + param_M_Org + "&GCD=" + strGroupCd);
    }
    #endregion 기능함수모음

}