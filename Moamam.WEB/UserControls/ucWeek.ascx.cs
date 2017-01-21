using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OracleClient;
using Moamam.Lib;
using System.ComponentModel;

public partial class Site_UserControls_ucWeek : System.Web.UI.UserControl
{
    protected UserControlAddText _addText = UserControlAddText.None;
    protected string _weekEventID = "";

    public string LabelFromToText
    {
        get
        {
            return txtFROM_TO.Text.ToString();
        }
        set
        {
            txtFROM_TO.Text = value;
        }
    }


    public bool Enabled
    {
        set
        {
            cbxWeekEvent.Enabled = value;
        }
    }

    public bool AutoPostBack
    {
        set
        {
            cbxWeekEvent.AutoPostBack = value;
        }
    }


    public string SelectedWeekEventID
    {
        set
        {
            _weekEventID = value;
        }
    }

    public UserControlAddText AddText
    {
        set
        {
            _addText = value;
        }
    }

    #region SelectedValue, SelectedIndex, ClientID
    public string SelectedValue
    {
        get
        {
            return cbxWeekEvent.SelectedValue;
        }
        set
        {
            cbxWeekEvent.SelectedValue = value;
        }
    }

    public int SelectedIndex
    {
        get
        {
            return cbxWeekEvent.SelectedIndex;
        }
        set
        {
            cbxWeekEvent.SelectedIndex = value;
        }
    }

    override public string ClientID
    {
        get
        {
            return cbxWeekEvent.ClientID;
        }
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
        }
    }

    protected void Application_Start()
    {
        Application["WeekList"] = WeekList.WeekLists();
    }

    public void LoadData()
    {
        Application_Start();

        DataSet ds = new DataSet();

        if (Application["WeekList"] != null)
        {
            List<Week> li = (List<Week>)Application["WeekList"];

            //convert array to datatable
            ds.Tables.Add(ConvertToDataTable(li));

            SetComboBox(ds, "DESC_WEEK_NO", "WEEK");
            txtFROM_TO.Text = ds.Tables[0].Rows[0]["ST_DATE"].ToString() + " ~ " + ds.Tables[0].Rows[0]["ED_DATE"].ToString();
        }
    }

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
    }

    public override void Focus()
    {
        cbxWeekEvent.Focus();
    }

    #region protected SetComboBox
    protected void SetComboBox(DataSet ds, string textField, string valueField, string addItem)
    {
        if (ds != null)
        {
            cbxWeekEvent.Items.Clear();
            cbxWeekEvent.DataSource = ds;
            cbxWeekEvent.DataTextField = textField;
            cbxWeekEvent.DataValueField = valueField;
            cbxWeekEvent.DataBind();
        }

        if (!string.IsNullOrEmpty(addItem))
            cbxWeekEvent.Items.Insert(0, new ListItem(addItem, ""));
    }

    protected void SetComboBox(DataSet ds, string textField, string valueField)
    {
        if (_addText == UserControlAddText.All)
            SetComboBox(ds, textField, valueField, "전체");
        else if (_addText == UserControlAddText.Select)
            SetComboBox(ds, textField, valueField, "선택하세요");
        else
            SetComboBox(ds, textField, valueField, null);
    }

    protected void SetComboBox(string addItem)
    {
        SetComboBox(null, null, null, addItem);
    }

    protected void dropWeek_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<Week> li = (List<Week>)Application["WeekList"];

        for (int i = 0; i < li.Count; i++)
        {
            if (li[i].WEEK.ToString() == cbxWeekEvent.SelectedValue)
            {
                txtFROM_TO.Text = li[i].ST_DATE.ToString() + " ~ " + li[i].ED_DATE.ToString();
                break;
            }
        }
    }


    /// <summary>
    /// List 형태를 데이타 테이블로 변환
    /// </summary>
    static public DataTable ConvertToDataTable<T>(IList<T> data)
    {
        PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
        DataTable table = new DataTable();

        foreach (PropertyDescriptor prop in properties)
            table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);

        foreach (T item in data)
        {
            if (item != null)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
        }

        return table;
    }
    #endregion
   
}