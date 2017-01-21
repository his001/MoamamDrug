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

public partial class Site_Report_DcAvailability : BasePage
{
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
        //DefaultEnter(this.Page, txtItem);
        //txtItem.Attributes["onkeyPress"] = "if(event.which || event.keyCode){if ((event.which == 13)) {return false; }}";
        //txtSuppCode.Attributes["onkeyPress"] = "if(event.which || event.keyCode){if ((event.which == 13)) {return false; }}";

        //페이지 유효성 검사
        //btnSearch.OnClientClick = "javascript : return SerchList();return checkCondition('" + this.Form.ClientID + "','Search');";
        btnExcel.OnClientClick = "return jsfn_ExcelDownloadLimit();";

        //업데이트패널에서 다운로드
        ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(btnExcel);

        txtOrderDATE.Text = DateTime.Now.ToString("yyyy-MM-dd");
    }

    /// <summary>
    /// NButton 권한 설정 파라미터
    /// </summary>
    private Control[] GetParams()
    {
        return new Control[]
            { 
                //btnAdd,
                btnExcel//,btnSearch //,btnDelete
            };
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

        string spName = "SP_WEB_Site_Review_AutoOrderJ_R";
        SqlParameterCollection param = DataCommon.InitSqlParameterCollection();
        param.Add(new SqlParameter("YYYYMMDD", txtOrderDATE.Text.Replace("-","")));
        param.Add(new SqlParameter("SMODE", "EXCEL"));


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
                        , "상품코드"  
                        , "상품명"  
                        , "sale forecast"  
                        , "ROQ"  
                        , "AOQ"   
                        , "DC SOH"   
                        , "DC OnOrder"   
                        , "Case size"   
                        , "TPND"  
                        , "TPND명"  
                        , "LT"  
                        , "Safety stock"  
                        , "Rounding 조건"  
                        , "입고일"  
                };

                new ExcelHelper().ExportExcel("", dt, "자동발주량리뷰" + Convert.ToInt32(DateTime.Today.ToString("yyyyMMdd")), HeaderList, "자동발주량리뷰");
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
