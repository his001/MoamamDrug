using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Moamam.Lib;
using Moamam.Data.Common;
using Moamam.Data.Site.Management;


public partial class Site_Management_UserList : BasePage
{

    #region Page_Load
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // 텍스트 박스 입력 후 엔터 시 조회 버튼 Link
            txtUserId.Attributes["onkeyPress"]      = "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {" + Page.GetPostBackEventReference(this.btnSearch) + "; return false; }}";
            txtUserName.Attributes["onkeyPress"]    = "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {" + Page.GetPostBackEventReference(this.btnSearch) + "; return false; }}";
            

            //사용자권한 셀렉트박스 목록 생성
            DataSet ds = GetAuthList();
            
            if(ds != null)
            {
                //조회영역
                ddlUserAuth.DataTextField   = "SUB_CD_NM";
                ddlUserAuth.DataValueField  = "SUB_CD";
                ddlUserAuth.DataSource      = ds;
                ddlUserAuth.DataBind();
                ddlUserAuth.Items.Insert(0, new ListItem("전체", ""));

                //팝업영역
                ddlPopUserAuth.DataTextField    = "SUB_CD_NM";
                ddlPopUserAuth.DataValueField   = "SUB_CD";
                ddlPopUserAuth.DataSource       = ds;
                ddlPopUserAuth.DataBind();
            }

            //데이터 조회
            getData();
        }
    }
    #endregion Page_Load


    #region 리스트 조회
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
            DataSet ds          = null;
            DataTable dt = null;

            ds = (new UserList()).GetUserList(txtUserId.Text.Trim(), txtUserName.Text.Trim(), ddlUserAuth.SelectedValue, ucPaging.RowCount, ucPaging.PageNo);

            if (ds != null)
            {
                rptUserList.DataSource = ds.Tables[0];
                rptUserList.DataBind();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ucPaging.TotalCount = Convert.ToInt32(ds.Tables[0].Rows[0]["TOTAL_COUNT"].ToString()); //목록 Total 갯수 저장 
                }
                else
                {
                    rptUserList.DataSource = dt;
                    rptUserList.DataBind();
                    ucPaging.TotalCount = 0;
                }
            }
        }
        catch (Exception ex)
        {
            base.ShowMessage(ex.Message);
        }
    }


    protected void rptUserList_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                string[] arrStrArgument = e.CommandArgument.ToString().Split(',');
                string strUserId        = arrStrArgument[0].ToString();             //사용자아이디
                string strUserName      = arrStrArgument[1].ToString();             //사용자명
                string strAuth          = arrStrArgument[2].ToString();             //권한
                string strUseYn         = arrStrArgument[3].ToString();             //사용여부(Y:사용, N:미사용)

                //사용자 수정
                if (e.CommandName == "select")
                {
                    txtPopUserId.Text               = strUserId;
                    txtPopUserName.Text             = strUserName;
                    ddlPopUserAuth.SelectedValue    = strAuth;
                    ddlPopUseYn.SelectedValue       = strUseYn;
                    btnDelete.Visible               = true;
                    btnTescoAuthSearch.Visible      = false;
                    hdnAddType.Value                = "update";
                }

                ModalPopExt1.Show();
            }
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
    #endregion 리스트 조회


    #region 팝업 기능
    //등록 버튼 클릭
    protected void btnInsert_Click(object sender, EventArgs e)
    {
        try
        {
            ClearPopup();

            btnTescoAuthSearch.Visible  = true;
            btnDelete.Visible           = false;
            hdnAddType.Value            = "insert";
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
            if (hdnAddType.Value == "insert")
            {
                SetUserInsert();
            }
            else if (hdnAddType.Value == "update")
            {
                SetUserUpdate();
            }
            getData();
        }
        catch (Exception ex)
        {
            base.ShowMessage(ex.Message);
        }
    }


    //사용자 신규등록
    protected void SetUserInsert()
    {
        try
        {
            string strMsg = (new UserList()).SetUserInsert(AntiHack.rtnSQLInj(txtPopUserId.Text.Trim()), AntiHack.rtnSQLInj(txtPopUserName.Text.Trim()), ddlPopUserAuth.SelectedValue, ddlPopUseYn.SelectedValue);

            if (strMsg == "OK")
            {
                base.ShowMessage("저장되었습니다");
                ModalPopExt1.Hide();
                getData();
            }
            else
                base.ShowMessage(strMsg);
        }
        catch (Exception ex)
        {
            base.ShowMessage(ex.Message);
        }
    }


    //사용자 정보수정
    protected void SetUserUpdate()
    {
        try
        {
            string strMsg = (new UserList()).SetUserUpdate(txtPopUserId.Text.Trim(), ddlPopUserAuth.SelectedValue, ddlPopUseYn.SelectedValue);

            if (strMsg == "OK")
            {
                base.ShowMessage("저장되었습니다");
                ModalPopExt1.Hide();
                getData();
            }
            else
                base.ShowMessage(strMsg);
        }
        catch (Exception ex)
        {
            base.ShowMessage(ex.Message);
        }
    }



    //삭제 버튼 클릭
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            string strMsg = (new UserList()).SetUserDelete(AntiHack.rtnSQLInj(txtPopUserId.Text.Trim()));

            if (strMsg == "OK")
            {
                base.ShowMessage("삭제되었습니다");
                ModalPopExt1.Hide();
                getData();
            }
            else
                base.ShowMessage(strMsg);

            getData();
        }
        catch (Exception ex)
        {
            base.ShowMessage(ex.Message);
        }
    }

    //닫기 버튼 클릭
    protected void btnExit_Click(object sender, EventArgs e)
    {
        ModalPopExt1.Hide();
    }


    //팝업창 초기화
    private void ClearPopup()
    {
        txtPopUserId.Text               = string.Empty;
        txtPopUserName.Text             = string.Empty;
        ddlPopUserAuth.SelectedIndex    = 0;
        ddlPopUseYn.SelectedIndex       = 0;
    }
    #endregion 팝업 기능


    #region 사용자권한 셀렉트박스 목록 생성
    protected DataSet GetAuthList()
    {
        DataSet ds = null;

        try
        {
            ds = (new SiteUser()).GetUserGroupList();
        }
        catch (Exception ex)
        {
            base.ShowMessage(ex.Message);
        }

        return ds;
    }
    #endregion


}//END CLASS