using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Moamam.Lib;
using System.Collections.Generic;
using System.Web.UI.HtmlControls;

using Moamam.Data.Site.MasterMain;

public partial class Site_Data_SamplePop : BasePage
{
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
        // 텍스트 박스 입력 후 엔터 시 조회 버튼 Link
        //txtItem.Attributes["onkeyPress"] = "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {" + Page.GetPostBackEventReference(this.btnSearch) + "; return false; }}";
        //txtSerchName.Attributes["onkeyPress"] = "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {jsfn_Search();}}";

        //페이지에 대한 권한을 검사
        //if (!SecurityChecked(GetParams(), SecurityInfo)) this.RedirectToSecurityError();

        //페이지 기본 엔터 지정
        //DefaultEnter(this.Page, txtSerchName);  
    }


    #region "[버튼 클릭 Routine]============================================================="
    
    #endregion

    #region 데이터 조회
    private void getData()
    {

        //try
        //{
        //    DataSet ds = null;

        //    DataTable dt = new DataTable();
        //    rptList.DataSource = dt;
        //    rptList.DataBind();

        //    //hidPageNo.Value = PageNum;
        //    if (txtSerchName.Text.ToString().Length > 1)
        //    {
        //        //ds = (new ProductItem()).GetProductItemNameToList(txtSerchName.Text.ToString().Trim(), ucPaging.RowCount, Convert.ToInt32(ucPaging.PageNo));
        //        if (ds != null)
        //        {
        //            rptList.DataSource = ds.Tables[0];
        //            rptList.DataBind();
        //        }
        //    }
        //    else
        //    {
        //        //hdnDataExists.Value = "<font color='red'>검색 조건을 두자리 이상 입력하세요.</font>";
        //    }
        //}
        //catch (Exception ex)
        //{
        //    base.ShowMessage(ex.Message);
        //}
    }



    #endregion

}