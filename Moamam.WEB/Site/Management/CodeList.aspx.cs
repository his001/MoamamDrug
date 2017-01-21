using System;
using System.Data;
using System.Web.UI.WebControls;

using Moamam.Lib;

using Moamam.Data.Site.Management;

public partial class Site_Management_CodeList : BasePage
{
    #region "[Page_Load Routine]==================================================================="
    /// <summary>
    /// Page_Load
    /// </summary>
    protected void Page_Load(object sender, EventArgs e)
    {
        this.ucPaging.RowCount = 10;
        this.ucPaging2.RowCount = 10;
        if (!IsPostBack)
        {
            //데이터 조회
            getData();
        }
    }
    #endregion


    #region "[버튼 클릭 Routine]============================================================="
    /// <summary>
    /// 그룹코드 버튼 클릭
    /// </summary>
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        ucPaging.PageNo = 1;
        getData();
    }

    /// <summary>
    /// 서브코드 버튼 클릭
    /// </summary>
    protected void btnSearch2_Click(object sender, EventArgs e)
    {
        ucPaging2.PageNo = 1;
        getSubData(hdnParentCODE_GROUP.Value);
    }

    //그룹코드 등록 버튼 클릭
    protected void btnInsert_Click(object sender, EventArgs e)
    {
        try
        {
            Clear_Fields();

            btnDelete.Visible = false;
            hdnGroupCdAddType.Value = "insert";
            ModalPopExt1.Show();
        }
        catch (Exception ex)
        {
            base.ShowMessage(ex.Message);
        }
    }

    //그룹코드 팝업 저장 버튼 클릭
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            if (hdnGroupCdAddType.Value == "insert")
            {
                SetGroupCdInsert();
            }
            else if (hdnGroupCdAddType.Value == "update")
            {
                SetGroupCdUpdate();
            }
        }
        catch (Exception ex)
        {
            base.ShowMessage(ex.Message);
        }
    }


    //서브코드 등록 버튼 클릭
    protected void btnInsert2_Click(object sender, EventArgs e)
    {
        try
        {
            Clear_Fields2();

            btnDelete2.Visible = false;
            hdnMenuAddType2.Value = "insert";
            ModalPopExt2.Show();
        }
        catch (Exception ex)
        {
            base.ShowMessage(ex.Message);
        }
    }


    //서브코드 저장 버튼 클릭
    protected void btnAdd2_Click(object sender, EventArgs e)
    {
        try
        {
            if (hdnMenuAddType2.Value == "insert")
            {
                SetSubCdInsert();
            }
            else if (hdnMenuAddType2.Value == "update")
            {
                SetSubCdUpdate();
            }
        }
        catch (Exception ex)
        {
            base.ShowMessage(ex.Message);
        }
    }

    //서브코드 팝업 닫기 버튼 클릭
    protected void btnExit_Click(object sender, EventArgs e)
    {
        ModalPopExt1.Hide();
    }

    //서브코드 팝업 닫기 버튼 클릭
    protected void btnExit2_Click(object sender, EventArgs e)
    {
        ModalPopExt2.Hide();
    }
    #endregion

    #region "[페이징 조회 Event Routine]============================================================="
    //그룹코드 페이징 조회
    protected void ucPaging_SelEvent(object sender, EventArgs e)
    {
        getData();
    }

    //서브코드 페이징 조회
    protected void ucPaging_SelEvent2(object sender, EventArgs e)
    {
        getSubData(hdnParentCODE_GROUP.Value);
    }
    #endregion


    #region "[RemoteDisplay Routine]============================================================="
    /// <summary>
    /// 그룹코드 검색
    /// </summary>
    private void getData()
    {
        try
        {
            DataSet ds = null;
            DataTable dt = null;

            ds = (new CodeList()).GetCodeGroupList(ucPaging.PageNo, ucPaging.RowCount);

            if (ds != null)
            {
                rptCodeGroupList.DataSource = ds.Tables[0];
                rptCodeGroupList.DataBind();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ucPaging.TotalCount = Convert.ToInt32(ds.Tables[0].Rows[0]["TOTAL_COUNT"].ToString()); //목록 Total 갯수 저장 
                }
                else
                {
                    ucPaging.TotalCount = 0;
                }
            }
        }
        catch (Exception ex)
        {
            base.ShowMessage(ex.Message);
        }
    }

    /// <summary>
    /// 서브코드 검색
    /// </summary>
    private void getSubData(string strSUB_CODE_GROUP)
    {
        try
        {
            DataSet ds = null;
            //
            ds = (new CodeList()).GetCodeSubList(strSUB_CODE_GROUP, ucPaging2.PageNo, ucPaging2.RowCount);

            if (ds != null)
            {
                rptCodeSubList.DataSource = ds.Tables[0];
                rptCodeSubList.DataBind();
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
    #endregion

    #region "[Repeater_ItemCommand Event Routine]============================================================="
    /// <summary>
    /// 그룹코드 OnItemCommand Event
    /// </summary>
    protected void rptCodeGroupList_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                //메뉴 수정
                if (e.CommandName == "groupCdSelect")
                {
                    string[] arrStrArgument = e.CommandArgument.ToString().Split(',');
                    string strCODE_GROUP = arrStrArgument[0].ToString();
                    string strCODE_ID = arrStrArgument[1].ToString();
                    string strCODE_DESC = arrStrArgument[2].ToString();


                    hdnCODE_GROUP.Value = strCODE_GROUP;
                    hdnCODE_ID.Value = strCODE_ID;
                    txtPopCODE_GROUP.Text = strCODE_GROUP;
                    txtPopCODE_ID.Text = strCODE_ID;
                    txtPopCODE_DESC.Text = strCODE_DESC;
                    btnDelete.Visible = true;
                    hdnGroupCdAddType.Value = "update";

                    ModalPopExt1.Show();
                }
                else if (e.CommandName == "subCodeSelect")
                {
                    string strGroupCd = e.CommandArgument.ToString();

                    hdnParentCODE_GROUP.Value = strGroupCd;

                    //하위메뉴 리스트 호출
                    getSubData(strGroupCd);
                }
            }
        }
        catch (Exception ex)
        {
            base.ShowMessage(ex.Message);
        }
    }

    /// <summary>
    /// 서브코드 OnItemCommand Event
    /// </summary>
    protected void rptCodeSubList_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                //메뉴 수정
                if (e.CommandName == "Select")
                {
                    string[] arrStrArg = e.CommandArgument.ToString().Split(',');
                    string strSUB_CDOE_GROUP = arrStrArg[0].ToString();
                    string strSUB_CDOE_ID = arrStrArg[1].ToString();
                    string strSUB_CDOE_DESC = arrStrArg[2].ToString();
                    string strSUB_CODE_REF_1 = arrStrArg[3].ToString();
                    string strSUB_CODE_REF_2 = arrStrArg[4].ToString();
                    string strSUB_CODE_REF_3 = arrStrArg[5].ToString();
                    string strSUB_CODE_REF_4 = arrStrArg[6].ToString();

                    hdnParentCODE_GROUP.Value = strSUB_CDOE_GROUP;
                    hdnSUB_CODE_ID.Value = strSUB_CDOE_ID;
                    txtPopSub_CODE_ID.Text = strSUB_CDOE_ID;
                    txtPopSub_CODE_DESC.Text = strSUB_CDOE_DESC;
                    txtPopSub_CODE_REF_1.Text = strSUB_CODE_REF_1;
                    txtPopSub_CODE_REF_2.Text = strSUB_CODE_REF_2;
                    txtPopSub_CODE_REF_3.Text = strSUB_CODE_REF_3;
                    txtPopSub_CODE_REF_4.Text = strSUB_CODE_REF_4;

                    btnDelete2.Visible = true;
                    hdnMenuAddType2.Value = "update";

                    //레이어팝업 호출
                    ModalPopExt2.Show();
                }
            }
        }
        catch (Exception ex)
        {
            base.ShowMessage(ex.Message);
        }
    }
    #endregion

    #region "[CRUD 버튼 클릭 Routine]============================================================="
    //마스터코드 신규 등록
    protected void SetGroupCdInsert()
    {
        try
        {            
            string strMsg = (new CodeList()).SetCodeGroupInsert(AntiHack.rtnSQLInj(txtPopCODE_GROUP.Text.Trim()), AntiHack.rtnSQLInj(txtPopCODE_ID.Text.Trim()), AntiHack.rtnSQLInj(txtPopCODE_DESC.Text.Trim()));

            if (strMsg == "OK")
            {
                base.ShowMessage("저장되었습니다");
                ModalPopExt1.Hide();
                getData();
            }
            else
            {
                base.ShowMessage(strMsg);            
            }                
        }
        catch (Exception ex)
        {
            base.ShowMessage(ex.Message);
        }
    }

    //마스터코드 수정
    protected void SetGroupCdUpdate()
    {
        try
        {
            string strMsg = (new CodeList()).SetCodeGroupUpdate(AntiHack.rtnSQLInj(hdnCODE_GROUP.Value.Trim().ToString()), AntiHack.rtnSQLInj(txtPopCODE_GROUP.Text.Trim().ToString())
                , AntiHack.rtnSQLInj(hdnCODE_ID.Value.Trim().ToString()), AntiHack.rtnSQLInj(txtPopCODE_ID.Text.Trim().ToString()), AntiHack.rtnSQLInj(txtPopCODE_DESC.Text.Trim().ToString()));

            if (strMsg == "OK")
            {
                base.ShowMessage("저장되었습니다");
                ModalPopExt1.Hide();
                getData();
            }
            else
            {
                base.ShowMessage(strMsg);
            }                
        }
        catch (Exception ex)
        {
            base.ShowMessage(ex.Message);
        }
    }

    //마스터코드 삭제 버튼 클릭
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {            
            string strMsg = (new CodeList()).SetCodeGroupDelete(hdnCODE_GROUP.Value, hdnCODE_ID.Value);

            if (strMsg == "OK")
            {
                base.ShowMessage("삭제되었습니다");
                ModalPopExt1.Hide();
                getData();
                getSubData(hdnParentCODE_GROUP.Value);
            }
            else
            {
                base.ShowMessage(strMsg);
            }                
        }
        catch (Exception ex)
        {
            base.ShowMessage(ex.Message);
        }
    }

    //하위코드 신규 등록 
    protected void SetSubCdInsert()
    {
        try
        {
            string strMsg = (new CodeList()).SetCodeSubInsert(
                AntiHack.rtnSQLInj(hdnParentCODE_GROUP.Value), AntiHack.rtnSQLInj(txtPopSub_CODE_ID.Text), AntiHack.rtnSQLInj(txtPopSub_CODE_DESC.Text), 
                AntiHack.rtnSQLInj(txtPopSub_CODE_REF_1.Text), AntiHack.rtnSQLInj(txtPopSub_CODE_REF_2.Text), AntiHack.rtnSQLInj(txtPopSub_CODE_REF_3.Text), AntiHack.rtnSQLInj(txtPopSub_CODE_REF_4.Text));

            if (strMsg == "OK")
            {
                base.ShowMessage("등록되었습니다");
                ModalPopExt2.Hide();
                getSubData(hdnParentCODE_GROUP.Value);
            }
            else
            {
                base.ShowMessage(strMsg);
            }
        }
        catch(Exception ex)
        {
            base.ShowMessage(ex.Message);
        }
    }

    //하위코드 수정
    protected void SetSubCdUpdate()
    {
        try
        {
            string strMsg = (new CodeList()).SetCodeSubUpdate(hdnParentCODE_GROUP.Value, hdnSUB_CODE_ID.Value, txtPopSub_CODE_ID.Text, txtPopSub_CODE_DESC.Text,
                AntiHack.rtnSQLInj(txtPopSub_CODE_REF_1.Text), AntiHack.rtnSQLInj(txtPopSub_CODE_REF_2.Text), AntiHack.rtnSQLInj(txtPopSub_CODE_REF_3.Text), AntiHack.rtnSQLInj(txtPopSub_CODE_REF_4.Text));

            if (strMsg == "OK")
            {
                base.ShowMessage("등록되었습니다");
                ModalPopExt2.Hide();
                getSubData(hdnParentCODE_GROUP.Value);
            }
            else
            {
                base.ShowMessage(strMsg);
            }                
        }
        catch(Exception ex)
        {
            base.ShowMessage(ex.Message);
        }
    }

    //하위코드 삭제 버튼 클릭
    protected void btnDelete2_Click(object sender, EventArgs e)
    {
        try
        {
            string strMsg = (new CodeList()).SetCodeSubDelete(hdnParentCODE_GROUP.Value, hdnSUB_CODE_ID.Value);

            if (strMsg == "OK")
            {
                base.ShowMessage("삭제되었습니다");
                ModalPopExt2.Hide();
                getSubData(hdnParentCODE_GROUP.Value);
            }
            else
            {
                base.ShowMessage(strMsg);
            }                
        }
        catch (Exception ex)
        {
            base.ShowMessage(ex.Message);
        }
    }
    #endregion

    #region "[UserDefined Funcitions]=============================================================================="
    /// <summary>
    /// 그룹코드 팝업 필드 클리어
    /// </summary>
    private void Clear_Fields()
    {
        txtPopCODE_GROUP.Text = string.Empty;
        txtPopCODE_ID.Text = string.Empty;
        txtPopCODE_DESC.Text = string.Empty;
    }

    /// <summary>
    /// 서브코드 팝업 필드 클리어
    /// </summary>
    private void Clear_Fields2()
    {
        txtPopSub_CODE_ID.Text = string.Empty;
        txtPopSub_CODE_DESC.Text = string.Empty;
        txtPopSub_CODE_REF_1.Text = string.Empty;
        txtPopSub_CODE_REF_2.Text = string.Empty;
        txtPopSub_CODE_REF_3.Text = string.Empty;
        txtPopSub_CODE_REF_4.Text = string.Empty;
    }
    #endregion


}