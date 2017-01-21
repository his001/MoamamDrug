using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections.Generic;
using System.Web.UI.HtmlControls;

using Moamam.Lib;
using Moamam.Data.WebControls;
using Moamam.Data.Site.Transfer;


/// <summary>
/// Transfer 제외 ITEM 관리
/// </summary>
public partial class Site_Transfer_ExecludeItem : BasePage
{
    #region Fields & Properties **********************************************************************************************

    #endregion Field & Properties
    #region Page Events **********************************************************************************************

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitVariables();
        }        
    }

    /// <summary>
    /// 페이지 유효성 검사를 수행합니다.
    /// 페이지 시작시 가장 처음에 위치합니다.
    /// </summary>
    private void InitVariables()
    {
        //페이지에 대한 권한을 검사
        if (!SecurityChecked(GetParams(), SecurityInfo)) this.RedirectToSecurityError();

        //페이지 기본 엔터 지정
        DefaultEnter(this.Page, btnSearch);
        txtItem.Focus();
        //페이지 유효성 검사
        btnSearch.OnClientClick = "javascript : return checkCondition('" + this.Form.ClientID + "','Search');";
        btnExcel.OnClientClick = "return confirm('다운로드 하시겠습니까?');";

        //업데이트패널에서 다운로드
        ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(btnExcel);
    }

    /// <summary>
    /// NButton 권한 설정 파라미터
    /// </summary>
    private Control[] GetParams()
    {
        return new Control[]
            {
                btnNew,
                btnExcel,
                btnSearch,
                btnUpload
            };
    }

    #endregion Page Events
    #region PostBack Events **********************************************************************************************

    /// <summary>
    /// 조회 버튼 클릭
    /// </summary>
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        ucPaging.PageNo = 1;
        getData();
    }

    /// <summary>
    /// 신규 버튼 클릭
    /// </summary>
    protected void btnNew_Click(object sender, EventArgs e)
    {
        try
        {
            Clear_Fields_Popup();
            hdnCMDCRUD.Value = "CREATE";
            btnAdd.Visible = true;

            //Show Popup 
            ModalPopExt.Show();
        }
        catch (Exception ex)
        {
            base.ShowMessage(ex.Message);
        }
    }

    /// <summary>
    /// 엑셀다운로드 버튼 클릭
    /// </summary>
    protected void btnExcel_Click(object sender, EventArgs e)
    {
        GetExcelDownload();
    }

    /// <summary>
    /// 팝업 이미지 닫기 버튼
    /// </summary>
    protected void btnPopupX_Click(object sender, EventArgs e)
    {
        ModalPopExt.Hide();
    }

    /// <summary>
    /// 팝업 저장 버튼 클릭
    /// </summary>
    protected void btnPopupAdd_Click(object sender, EventArgs e)
    {
        CMDCRUD();
    }

    /// <summary>
    /// 팝업 삭제 버튼 클릭
    /// </summary>
    protected void btnPopupDelete_Click(object sender, EventArgs e)
    {
        hdnCMDCRUD.Value = "DELETE";
        CMDCRUD();
    }

    /// <summary>
    /// 팝업 닫기 버튼 클릭
    /// </summary>
    protected void btnPopupExit_Click(object sender, EventArgs e)
    {
        ModalPopExt.Hide();
    }

    /// <summary>
    /// Repeater OnItemCommand, 리피터 링크버튼 클릭
    /// </summary>
    protected void rptList_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                //메뉴 수정
                if (e.CommandName == "Select")
                {
                    string[] arrStrArg = e.CommandArgument.ToString().Split(',');
                    string strITEM = arrStrArg[0].ToString();

                    txtPopup_ITEM.Text = strITEM;
                    hdnPopupITEM.Value = strITEM;

                    btnAdd.Visible = false;
                    btnDelete.Visible = true;
                    hdnCMDCRUD.Value = "DELETE";

                    //레이어팝업 호출
                    ModalPopExt.Show();
                }
            }
        }
        catch (Exception ex)
        {
            base.ShowMessage(ex.Message);
        }
    }

    #endregion PostBack Events
    #region Data Routine **********************************************************************************************

    private void getNoData()
    {
        DataTable dt = new DataTable();

        rptList.DataSource = dt;
        rptList.DataBind();
    }

    private void getData()
    {
        try
        {
            DataSet ds = null;

            ds = (new ExecludeItem()).GetExecludeItem(AntiHack.rtnSQLInj(txtItem.Text.Trim()), ucPaging.RowCount, ucPaging.PageNo);
            
            if (ds != null)
            {
                rptList.DataSource = ds.Tables[0];
                rptList.DataBind();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ucPaging.TotalCount = Convert.ToInt32(ds.Tables[0].Rows[0]["TOTAL_COUNT"]); //목록 Total 갯수 저장 
                }
                else
                {
                    getNoData();
                    ucPaging.TotalCount = 0;
                }

            }
        }
        catch (Exception ex)
        {
            base.ShowMessage(ex.Message);
        }
    }

    protected void GetExcelDownload()
    {
        try
        {
            DataTable dt = (new ExecludeItem()).GetExecludeItem(txtItem.Text.Trim(), ucPaging.RowCount, ucPaging.PageNo).Tables[0];

            dt.Columns.Remove("PAGE");
            dt.Columns.Remove("TOTAL_COUNT");
            dt.Columns.Remove("CREATE_DATE");
         
            if (dt.Rows.Count > 0)
            {
                //엑셀 헤더 설정
                string[] HeaderList = { "ACTION_TYPE"
                                        , "아이템"
                                        , "ITEM_DESC"
                                        , "CREATE_USER" };

                new ExcelHelper().ExportExcel("", dt, "Transfer 제외 Item" + Convert.ToInt32(DateTime.Today.ToString("yyyyMMdd")), HeaderList, "Transfer");
            }
            else
            {
                base.ShowMessage("데이터가 존재하지 않습니다.");
            }
        }
        catch (Exception ex)
        {
            base.ShowMessage(ex.Message);
        }
    }

    /// <summary>
    /// 팝업창 CREATE, UPDATE, DELETE
    /// </summary>
    protected void CMDCRUD()
    {
        try
        {
            string strMsg = (new ExecludeItem()).SetExecludeCrud(AntiHack.rtnSQLInj(hdnCMDCRUD.Value), AntiHack.rtnSQLInj(txtPopup_ITEM.Text), SessionAuth.GetUserID());

            if (strMsg != "OK")
            {
                base.ShowMessage(strMsg);
            }
            else
            {
                switch (hdnCMDCRUD.Value)
                {
                    case "CREATE":
                        base.ShowMessage("신규 생성되었습니다");
                        break;
                    case "UPDATE":
                        base.ShowMessage("수정되었습니다");
                        break;
                    case "DELETE":
                        base.ShowMessage("삭제되었습니다");
                        break;
                }
            }

            getData();
        }
        catch (Exception ex)
        {
            base.ShowMessage(ex.Message);
        }
    }

    #endregion
    #region Helpers **********************************************************************************************

    /// <summary>
    /// 페이징 처리
    /// </summary>
    protected void ucPaging_SelEvent(object sender, EventArgs e)
    {
        getData();
    }

    /// <summary>
    /// 팝업 필드 클리어
    /// </summary>
    private void Clear_Fields_Popup()
    {
        txtPopup_ITEM.Text = string.Empty;
        btnDelete.Visible = false;
    }

    #endregion Helpers


}