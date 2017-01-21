using System;

public partial class Site_UserControls_ucCalendar : System.Web.UI.UserControl
{
    int _addDays = 0;

    public int AddDays
    {
        set { _addDays = value; }
    }

    public string GetDateCilentID
    {
        get { return txtCalendar.ClientID.ToString(); }
    }

    public DateTime SetDate
    {
        set
        {
            CalendarExtender1.SelectedDate = value;
            txtCalendar.Text = ((DateTime)CalendarExtender1.SelectedDate).ToString("yyyy-MM-dd");
        }
        get
        {
            return Convert.ToDateTime(txtCalendar.Text);
        }
    }

    public string SetDateString
    {
        set
        {
            if (value.Length == 10)
                CalendarExtender1.SelectedDate = DateTime.Parse(value);
            else if (value.Length == 8)
                CalendarExtender1.SelectedDate = DateTime.Parse(value.Substring(0, 4) + "-" + value.Substring(4, 2) + "-" + value.Substring(6, 2));

            txtCalendar.Text = ((DateTime)CalendarExtender1.SelectedDate).ToString("yyyy-MM-dd");
        }
    }

    public string SelectedDateString
    {
        get { return txtCalendar.Text.Replace("-", ""); }
    }

    public string SelectedDashedDateString
    {
        get { return txtCalendar.Text; }
    }

    public bool Enabled
    {
        set
        {
            txtCalendar.Enabled = value;
        }
    }

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        if (!IsPostBack)
        {
            txtCalendar.Text = DateTime.Today.ToString("yyyy-MM-dd");

            if (_addDays > 0)
                txtCalendar.Text = DateTime.Today.AddDays(_addDays).ToString("yyyy-MM-dd");
        }
    }
}
