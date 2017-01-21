using System;

public partial class Site_UserControls_ucCalendarFromTo : System.Web.UI.UserControl
{
    int _addDays = 0;
    protected string _promotionEventID = "";

    public int AddDays
    {
        set { _addDays = value; }
    }

    public string SelectedPromotionEventID
    {
        set
        {
            _promotionEventID = value;
        }
    }

    public string FromClientID
    {
        get { return txtFrom.ClientID.ToString(); }
    }

    public string ToClientID
    {
        get { return txtTo.ClientID.ToString(); }
    }

    public DateTime FromDate
    {
        get { return Convert.ToDateTime(txtFrom.Text); }
        set { txtFrom.Text = value.ToString("yyyy-MM-dd"); }
    }
    public DateTime ToDate
    {
        get { return Convert.ToDateTime(txtTo.Text); }
        set { txtTo.Text = value.ToString("yyyy-MM-dd"); }
    }

    public string SelectedFromDateString
    {
        get { return txtFrom.Text.Replace("-", ""); }
    }

    public string SelectedToDateString
    {
        get { return txtTo.Text.Replace("-", ""); }
    }

    public string SelectedDashedFromDateString
    {
        get { return txtFrom.Text; }
    }

    public string SelectedDashedToDateString
    {
        get { return txtTo.Text; }
    }

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        if (!IsPostBack)
        {
            //txtFrom.Text = DateTime.Today.AddDays(1 - DateTime.Today.Day).ToString("yyyy-MM-dd");
            txtFrom.Text = DateTime.Today.ToString("yyyy-MM-dd");
            txtTo.Text = DateTime.Today.AddDays(30).ToString("yyyy-MM-dd");

            if (_addDays > 0)
                txtTo.Text = DateTime.Today.AddDays(_addDays).ToString("yyyy-MM-dd");
        }
    }
}
