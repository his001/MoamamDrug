using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Site_UserControls_ucUploadResult : System.Web.UI.UserControl
{
    public int SuccessCount
    {
        get { return Convert.ToInt16(lblSuccess.Text.Replace(",", "")); }
        set { lblSuccess.Text = string.Format("{0:N0}", value); }
    }

    public int ErrorCount
    {
        get { return Convert.ToInt16(lblError.Text.Replace(",", "")); }
        set { lblError.Text = string.Format("{0:N0}", value); }
    }

    public void InitControl()
    {
        SuccessCount = 0;
        ErrorCount = 0;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitControl();
        }
    }
}
