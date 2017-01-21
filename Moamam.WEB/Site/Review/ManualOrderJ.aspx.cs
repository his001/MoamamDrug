using Moamam.Data.Site.BaseClass;
using Moamam.Data.Site.MasterMain;
using Moamam.Lib;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Site_Review_ManualOrderJ : BasePage
{
    #region Page Events **********************************************************************************************
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitVariables();
            LoadData();
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
        DefaultEnter(this.Page, txtItem);
        txtItem.Attributes["onkeyPress"] = "if(event.which || event.keyCode){if ((event.which == 13)) {return false; }}";
        txtSuppCode.Attributes["onkeyPress"] = "if(event.which || event.keyCode){if ((event.which == 13)) {return false; }}";

        //페이지 유효성 검사
        //btnSearch.OnClientClick = "javascript : return SerchList();return checkCondition('" + this.Form.ClientID + "','Search');";
        btnExcel.OnClientClick = "return jsfn_ExcelDownloadLimit();";

        //업데이트패널에서 다운로드
        ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(btnExcel);

        //ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(btnGrid1Save);
        //ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(btnGrid1Approval);
    }

    /// <summary>
    /// NButton 권한 설정 파라미터
    /// </summary>
    private Control[] GetParams()
    {
        return new Control[]
        { 
            btnGrid1Save,
            btnGrid1Approval,
            btnExcel
        };
    }

    public void LoadData()
    {
        DataSet ds = new DataSet();
        ds = SectionList.SectionLists();

        ddlFromSectionList.DataTextField = "SECTION_NAME";
        ddlFromSectionList.DataValueField = "SECTION";
        ddlFromSectionList.DataSource = ds;
        ddlFromSectionList.DataBind();
        //ddlFromSectionList.Items.Insert(0, new ListItem("전체", ""));

        ddlToSectionList.DataTextField = "SECTION_NAME";
        ddlToSectionList.DataValueField = "SECTION";
        ddlToSectionList.DataSource = ds;
        ddlToSectionList.DataBind();
    }
    #endregion Page Events



    #region Excel Download
    protected void btnExcel_Click(object sender, EventArgs e)
    {
        GetExcelDownload();
    }

    private DataSet getExcelData()
    {
        DataSet ds = null;

        string spName = "SP_WEB_Site_Review_ManualOrderJ_R";
        SqlParameterCollection param = DataCommon.InitSqlParameterCollection();
        param.Add(new SqlParameter("SECTIONFROM", ddlFromSectionList.SelectedValue.ToString().Trim()));
        param.Add(new SqlParameter("SECTIONTO", ddlToSectionList.SelectedValue.ToString().Trim()));
        param.Add(new SqlParameter("ITEM", txtItem.Text.ToString().Trim()));
        param.Add(new SqlParameter("ROWCNT", "30"));
        param.Add(new SqlParameter("PAGENUM", "1"));
        param.Add(new SqlParameter("SMODE", "EXCEL"));
        param.Add(new SqlParameter("SUPPLIER", txtSuppCode.Text.ToString().Trim().Replace("'", "")));

        ds = DataCommon.CommonSpCall(spName, param);
        //if (ds != null && ds.Tables.Count > 0)
        return ds;
    }

    protected void GetExcelDownload()
    {
        try
        {
            DataTable dt = null;
            dt = getExcelData().Tables[0];
            if (dt.Rows.Count > 0)
            {
                //엑셀 헤더 설정
                string[] HeaderList = {
                        "발주번호"
                        , "발주일"
                        , "Dept"
                        , "Section"
                        , "DC코드"
                        , "DC명" 
                        , "협력업체코드"  
                        , "협력업체이름"  
                        , "TPND"  
                        , "TPND명"  
                        , "상품코드"  
                        , "상품명"  
                        , "sale forecast"  
                        , "ROQ"  
                        , "AOQ"   
                        , "DC SOH"   
                        , "DC OnOrder"   
                        , "Case size"   
                        , "LT"  
                        , "Safety stock"  
                        , "Rounding 조건"  
                        , "입고일"  
                        , "상태"  
                };

                new ExcelHelper().ExportExcel("", dt, "수동발주량리뷰" + Convert.ToInt32(DateTime.Today.ToString("yyyyMMdd")), HeaderList, "수동발주량리뷰");
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
    #endregion Excel Download

}
