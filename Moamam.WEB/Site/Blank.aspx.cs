using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Moamam.Data.Site.BaseClass;
using Moamam.Data.Site.MasterMain;
using Moamam.Lib;

public partial class Site_Blank : BasePage
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
        //if (!SecurityChecked(GetParams(), SecurityInfo)) this.RedirectToSecurityError();

    }

    /// <summary>
    /// NButton 권한 설정 파라미터
    /// </summary>
    private Control[] GetParams()
    {
        return new Control[]
            { 
                //btnAdd,
                //btnExcel//,btnSearch //,btnDelete
            };
    }

    public void LoadData()
    {

    }
    #endregion Page Events

    protected void btnSession_Click(object sender, EventArgs e)
    {
        HttpContext.Current.Session.Abandon();
    }
}
