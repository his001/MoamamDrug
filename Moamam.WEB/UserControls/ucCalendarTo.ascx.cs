using System;

public partial class UserControls_ucCalendarTo : System.Web.UI.UserControl
{
    int _addDays = 0;

    public int AddDays
    {
        set { _addDays = value; }
    }

    public string ToClientID
    {
        get { return txtTo.ClientID.ToString(); }
    }

    public DateTime ToDate
    {
        get { return Convert.ToDateTime(txtTo.Text); }
        set { txtTo.Text = value.ToString("yyyy-MM-dd"); }
    }

    public string SelectedToDateString
    {
        get { return txtTo.Text.Replace("-", ""); }
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
            //txtTo.Text = DateTime.Today.ToString("yyyy-MM-dd");
            txtTo.Text = "";

            if (_addDays > 0)
                txtTo.Text = DateTime.Today.AddDays(_addDays).ToString("yyyy-MM-dd");
        }
    }
    protected void txtTo_TextChanged(object sender, EventArgs e)
    {
        string toTime = txtTo.Text.Replace("-", "").Trim();
        if (toTime.Length == 8)
            txtTo.Text = toTime.Substring(0, 4) + "-" + toTime.Substring(4, 2) + "-" + toTime.Substring(6, 2);
    }

    public void TextClear()
    {
        txtTo.Text = "";
    }

    public void TextEnabled(bool result)
    {
        txtTo.Enabled = result;
    }
}