using System;
using System.Web.UI.WebControls;
using System.Data;

public enum UserControlType
{
    DropDownList,
    RadioButtonList
}

public enum UserControlAddText
{
    None,
    All,
    Select
}

public class BaseUserControl : System.Web.UI.UserControl
{
    protected UserControlType _controlType;
    protected UserControlAddText _addText;
    protected DropDownList _combo;
    protected RadioButtonList _radio;

    public UserControlType ControlType
    {
        set
        {
            _controlType = value;
            ResetControl();
        }
    }

    public bool Enabled
    {
        set
        {
            _combo.Enabled = value;
            _radio.Enabled = value;
        }
    }

    public UserControlAddText AddText
    {
        set { _addText = value; }
    }

    #region SelectedValue, SelectedIndex, ClientID
    public string SelectedValue
    {
        get
        {
            if (_controlType == UserControlType.DropDownList)
                return _combo.SelectedValue;
            else
                return _radio.SelectedValue;
        }
        set
        {
            if (_controlType == UserControlType.DropDownList)
                _combo.SelectedValue = value;
            else
                _radio.SelectedValue = value;
        }
    }

    public int SelectedIndex
    {
        get
        {
            if (_controlType == UserControlType.DropDownList)
                return _combo.SelectedIndex;
            else
                return _radio.SelectedIndex;
        }
        set
        {
            if (_controlType == UserControlType.DropDownList)
                _combo.SelectedIndex = value;
            else
                _radio.SelectedIndex = value;
        }
    }

    override public string ClientID
    {
        get
        {
            if (_controlType == UserControlType.DropDownList)
                return _combo.ClientID;
            else
                return _radio.ClientID;
        }
    }
    #endregion

    #region Constructor
    public BaseUserControl()
    {
        _combo = new DropDownList();
        _combo.Font.Names = new string[] { "Helvetica", "Arial", "Verdana", "sans-serif" };
        _combo.Font.Size = 9; //pt
        _combo.Style.Add("border", "1px #999999 solid");
        _combo.Style.Add("border-radius", "3px");

        _radio = new RadioButtonList();
        _radio.RepeatDirection = RepeatDirection.Horizontal;
        _radio.RepeatLayout = RepeatLayout.Flow;
        _radio.Font.Names = new string[] { "Helvetica", "Arial", "Verdana", "sans-serif" };
        _radio.Font.Size = 9; //9pt

        _controlType = UserControlType.DropDownList;    //default
        _addText = UserControlAddText.None; //default
    }
    #endregion

    protected void ResetControl()
    {
        this.Controls.Clear();

        if (_controlType == UserControlType.DropDownList)
            this.Controls.Add(_combo);
        else if (_controlType == UserControlType.RadioButtonList)
            this.Controls.Add(_radio);
    }

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        ResetControl();
    }

    public override void Focus()
    {
        if (_controlType == UserControlType.DropDownList)
            _combo.Focus();
        else if (_controlType == UserControlType.RadioButtonList)
            _radio.Focus();
    }

    #region protected SetComboBox
    protected void SetComboBox(DataSet ds, string textField, string valueField, string addItem)
    {
        if (_controlType == UserControlType.DropDownList)
        {
            if (ds != null)
            {
                _combo.Items.Clear();
                _combo.DataSource = ds;
                _combo.DataTextField = textField;
                _combo.DataValueField = valueField;
                _combo.DataBind();
            }

            if (!string.IsNullOrEmpty(addItem))
                _combo.Items.Insert(0, new ListItem(addItem, ""));
        }
        else if (_controlType == UserControlType.RadioButtonList)
        {
            if (ds != null)
            {
                _radio.Items.Clear();
                _radio.DataSource = ds;
                _radio.DataTextField = textField;
                _radio.DataValueField = valueField;
                _radio.DataBind();
            }

            if (!string.IsNullOrEmpty(addItem))
                _radio.Items.Insert(0, new ListItem(addItem, ""));

            if (_radio.Items.Count > 0 && _radio.SelectedValue == "")
                _radio.SelectedIndex = 0;
        }
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
    #endregion
}
