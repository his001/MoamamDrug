using System;

public partial class UserControls_ucCalendarFrom : System.Web.UI.UserControl
{
    int _addDays = 0;

    public int AddDays
    {
        set { _addDays = value; }
    }

    public string FromClientID
    {
        get { return txtFrom.ClientID.ToString(); }
    }

    public DateTime FromDate
    {
        get { return Convert.ToDateTime(txtFrom.Text); }
        set { txtFrom.Text = value.ToString("yyyy-MM-dd"); }
    }

    public string SelectedFromDateString
    {
        get { return txtFrom.Text.Replace("-", ""); }
    }

    public string SelectedDashedFromDateString
    {
        get { return txtFrom.Text; }
    }

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        if (!IsPostBack)
        {
            //txtFrom.Text = DateTime.Today.ToString("yyyy-MM-dd");
            txtFrom.Text = "";
        }
    }
    protected void txtFrom_TextChanged(object sender, EventArgs e)
    {
        string fromTime = txtFrom.Text.Replace("-", "").Trim();
        if (fromTime.Length == 8)
            txtFrom.Text = fromTime.Substring(0, 4) + "-" + fromTime.Substring(4, 2) + "-" + fromTime.Substring(6, 2);
    }

    public void TextClear()
    {
        txtFrom.Text = "";
    }

    public void TextEnabled(bool result)
    {
        txtFrom.Enabled = result;
    }
}
