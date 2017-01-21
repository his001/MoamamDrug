using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Moamam.Lib;
using System.Collections.Generic;
using System.Web.UI.HtmlControls;

using Moamam.Data.Site.MasterMain;
public partial class Site_PopupPage_ItemNameList : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    { 
    }

    #region "[페이징 처리 Routine]==================================================================="
    protected void ucPaging_SelEvent(object sender, EventArgs e)
    { 
        getDateItemName();
    }
    /// <summary>
    /// OnInit
    /// </summary>
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        txtSerchName.Attributes["onkeyPress"] = "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {" + Page.GetPostBackEventReference(this.btnSerchItemName) + "; return false; }}";
        
    }

    #endregion

    #region "[버튼 클릭 Routine]=============================================================" 
    /// <summary>
    /// 아이템 목록 조회 버튼 클릭
    /// </summary>
    protected void btnSerchItemName_Click(object sender, EventArgs e)
    { 
        this.ucPaging.PageNo = 1;
        this.ucPaging.RowCount = 30;
        getDateItemName();
       // ScriptManager.RegisterStartupScript(this, this.GetType(), "ajaxMessageScript", "divProgressNone();", true);
    }
    #endregion


    #region 데이터 조회
    private void getDateItemName()
    {

        try
        {
            DataSet ds = null;


            DataTable dt = new DataTable();
            rptTransfer1.DataSource = dt;
            rptTransfer1.DataBind();


            string PageNum = ucPaging.PageNo.ToString();// hidPageNo.Value; 
             
            //페이지번호 콤마로 구분값으로 될시
            string[] ch = PageNum.Split(',');
            if (ch.Length > 0) PageNum = ch[0];

            //페이지번호 재설정
            if (Convert.ToInt32(PageNum) < 1) PageNum = "1";

            //hidPageNo.Value = PageNum;
            ucPaging.PageNo = Convert.ToInt32(PageNum);
            if (txtSerchName.Text.ToString().Length > 1)
            {
                ds = (new ProductItem()).GetProductItemNameToList(txtSerchName.Text.ToString().Trim(), ucPaging.RowCount, Convert.ToInt32(ucPaging.PageNo), ddlName.SelectedValue);
                int pageNum=Convert.ToInt32(ucPaging.PageNo);
                if (ds != null)
                {
                    rptTransfer1.DataSource = ds.Tables[0];
                    rptTransfer1.DataBind();
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ucPaging.TotalCount =Convert.ToInt32(ds.Tables[0].Rows[0]["TOTAL_COUNT"].ToString()); //목록 Total 갯수 저장  
                        ucPaging.PageNo = pageNum;
                        

                    }
                    else
                    {
                        hdnDataExists.Value = "데이터가 존재하지 않습니다."; 
                        ucPaging.TotalCount = 0;
                        ucPaging.PageNo = 0;
                    }
                }
            }
            else
            {
                hdnDataExists.Value = "<font color='red'>검색 조건을 두자리 이상 입력하세요.</font>"; 
                ucPaging.PageNo = 0;
                ucPaging.TotalCount = 0;
            }

        }
        catch (Exception ex)
        {
            base.ShowMessage(ex.Message);
        }
    }
    #endregion

    #region 함수
    /// <summary>
    /// MakeHtmlInfo
    /// </summary> 
    protected string MakeHtmlInfo(string SendValue, string chk)
    {
        string tmp = "&nbsp;";
        int rint = 0;

        if (chk == "YN")
        {
            if (!string.IsNullOrEmpty(SendValue))
            {
                tmp = SendValue;
            }
            else
            {
                tmp = "N";
            }
        }
        else if (chk == "CINT")
        {
            if (!string.IsNullOrEmpty(SendValue))
            {
                decimal d = Convert.ToDecimal(SendValue);
                rint = Convert.ToInt32(d);
                tmp = rint.ToString();
            }
            else
            {
                tmp = "";
            }
        }
        else if (chk == "LEN")
        {
            if (SendValue.Length > 10)
                SendValue = SendValue.Substring(0, 10) + "...";

            tmp = SendValue;
        }
        else
        {
            if (!string.IsNullOrEmpty(SendValue))
            {
                tmp = SendValue;
            }
        }
        return tmp;
    }

    #endregion
}