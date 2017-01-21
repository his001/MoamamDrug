#region Using Directives

using Moamam.Lib;
using System;
using System.ComponentModel;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

#endregion

namespace Moamam.Data.WebControls
{
    #region Enum : EnterKeyAction, ImeMode

    public enum EnterKeyAction
    {
        NotSet,
        NoAction,
        MoveFocus,
        Submit,
        SaveSubmit
    }

    public enum ImeMode
    {
        NotSet,
        Auto,
        Active,
        Inactive,
        Deactivated
    }

    public enum Validation
    {
        NotSet,
        Numeric,
        Decimal,
        //Alphanumeric,
        //Korean,
        //Ssn,
        //Email,
        //Password,
        //Alphanumericpassword,
        Date,
        Currency,
        //Currency_Focus,
        UnSignedNum
    }

    #endregion

    [ToolboxData("<{0}:NTextBox runat=server></{0}:NTextBox>")]
    public class NTextBox : TextBox, IControlParameter
    {
        #region Private Members

        private string _disabledCss = String.Empty;
        private string _submitButtonID = String.Empty;
        private string _exceptionDesc = String.Empty;
        private ImeMode _imeMode = ImeMode.NotSet;
        private Validation _validation = Validation.NotSet;
        private bool _nrequired = false;
        private string _formatString = "#,###";
        private EnterKeyAction _enterKeyAction = EnterKeyAction.NotSet;

        // IControlParameter 변수
        private bool _setEmptyToNull;
        private int _columnLength;
        private SqlDbType _columnDbType = SqlDbType.NVarChar;

        private bool _isInsertParameter;
        private string _insertParameterName;
        private ParameterDirection _insertDirection = ParameterDirection.Input;

        private bool _isUpdateParameter;
        private string _updateParameterName;
        private ParameterDirection _updateDirection = ParameterDirection.Input;

        private bool _isDeleteParameter;
        private string _deleteParameterName;
        private ParameterDirection _deleteDirection = ParameterDirection.Input;

        private bool _isSelectParameter;
        private string _selectParameterName;
        private ParameterDirection _selectDirection = ParameterDirection.Input;

        private bool _isBindParameter;
        private string _bindParameterName;

        #endregion

        #region Public Properties

        [Category("Appearance")]
        [DefaultValue("")]
        public string DisabledCss
        {
            get { return _disabledCss; }
            set { _disabledCss = value; }
        }

        [Category("Behavior")]
        [DefaultValue(EnterKeyAction.NotSet)]
        public EnterKeyAction EnterKeyAction
        {
            get { return _enterKeyAction; }
            set { _enterKeyAction = value; }
        }

        [Category("Behavior")]
        [DefaultValue("")]
        public string SubmitButtonID
        {
            get { return _submitButtonID; }
            set { _submitButtonID = value; }
        }

        [Category("Behavior")]
        [DefaultValue(ImeMode.NotSet)]
        public ImeMode ImeMode
        {
            get { return _imeMode; }
            set { _imeMode = value; }
        }

        [Category("Behavior")]
        [DefaultValue("")]
        public string ExceptionDesc
        {
            get { return _exceptionDesc; }
            set { _exceptionDesc = value; }
        }

        [Category("Behavior")]
        [DefaultValue(Validation.NotSet)]
        public Validation Validation
        {
            get { return _validation; }
            set { _validation = value; }
        }

        [Category("Behavior")]
        [DefaultValue(false)]
        public bool NRequired
        {
            get { return _nrequired; }
            set { _nrequired = value; }
        }

        [Category("Behavior")]
        [DefaultValue("#,###")]
        public string FormatString
        {
            get { return _formatString; }
            set { _formatString = value; }
        }

        #endregion

        #region IControlParameter Members

        [Category("ControlParameter")]
        [DefaultValue(false)]
        public bool SetEmptyToNull
        {
            get { return _setEmptyToNull; }
            set { _setEmptyToNull = value; }
        }

        [Category("ControlParameter")]
        [DefaultValue(SqlDbType.NVarChar)]
        public SqlDbType ColumnDbType
        {
            get { return _columnDbType; }
            set { _columnDbType = value; }
        }

        [Category("ControlParameter")]
        [DefaultValue(50)]
        public int ColumnLength
        {
            get { return _columnLength; }
            set
            {
                _columnLength = value;
                this.MaxLength = value;
            }
        }

        [Category("ControlInsertParameter")]
        [DefaultValue(false)]
        public bool IsInsertParameter
        {
            get { return _isInsertParameter; }
            set { _isInsertParameter = value; }
        }

        [Category("ControlInsertParameter")]
        public string InsertParameterName
        {
            get { return _insertParameterName; }
            set { _insertParameterName = value; }
        }

        [Category("ControlInsertParameter")]
        public ParameterDirection InsertDirection
        {
            get { return _insertDirection; }
            set { _insertDirection = value; }
        }

        [Category("ControlUpdateParameter")]
        [DefaultValue(false)]
        public bool IsUpdateParameter
        {
            get { return _isUpdateParameter; }
            set { _isUpdateParameter = value; }
        }

        [Category("ControlUpdateParameter")]
        public string UpdateParameterName
        {
            get { return _updateParameterName; }
            set { _updateParameterName = value; }
        }

        [Category("ControlUpdateParameter")]
        [DefaultValue(ParameterDirection.Input)]
        public ParameterDirection UpdateDirection
        {
            get { return _updateDirection; }
            set { _updateDirection = value; }
        }

        [Category("ControlDeleteParameter")]
        [DefaultValue(false)]
        public bool IsDeleteParameter
        {
            get { return _isDeleteParameter; }
            set { _isDeleteParameter = value; }
        }

        [Category("ControlDeleteParameter")]
        public string DeleteParameterName
        {
            get { return _deleteParameterName; }
            set { _deleteParameterName = value; }
        }

        [Category("ControlDeleteParameter")]
        [DefaultValue(ParameterDirection.Input)]
        public ParameterDirection DeleteDirection
        {
            get { return _deleteDirection; }
            set { _deleteDirection = value; }
        }

        [Category("ControlSelectParameter")]
        [DefaultValue(false)]
        public bool IsSelectParameter
        {
            get { return _isSelectParameter; }
            set { _isSelectParameter = value; }
        }

        [Category("ControlSelectParameter")]
        public string SelectParameterName
        {
            get { return _selectParameterName; }
            set { _selectParameterName = value; }
        }

        [Category("ControlSelectParameter")]
        [DefaultValue(ParameterDirection.Input)]
        public ParameterDirection SelectDirection
        {
            get { return _selectDirection; }
            set { _selectDirection = value; }
        }

        [Category("ControlBindParameter")]
        public bool IsBindParameter
        {
            get { return _isBindParameter; }
            set { _isBindParameter = value; }
        }

        [Category("ControlBindParameter")]
        public string BindParameterName
        {
            get { return _bindParameterName; }
            set { _bindParameterName = value; }
        }

        public void SetControlData(object o)
        {
            this.Text = (o != null ? Convert.ToString(o).Trim() : String.Empty);
        }

        public object GetControlData()
        {
            return this.Text.Trim();
        }

        public void ClearControlData()
        {
            this.Text = String.Empty;
        }

        #endregion

        #region Overrides

        // 텍스트 속성 오버라이트
        public override string Text
        {
            get
            {
                if (Validation == Validation.Currency )
                {
                    return AntiHack.rtnSQLInj(base.Text.Replace(",", ""));
                }
                else
                {
                    return AntiHack.rtnSQLInj(base.Text.Replace("<", "&lt;").Replace(">", "&gt;").
                        Replace("'", "''").Replace("\"", "\"\"").Replace("\\", "\\\\").Replace("null", "&null").Replace("#", "\\#").Replace("--", "\\--").Replace(",", "\\,"));
                }
            }
            set
            {
                base.Text = value;
            }
        }


        protected override void OnInit(EventArgs e)
        {
            Page.RegisterRequiresControlState(this);

            base.OnInit(e);
        }

        protected override void LoadControlState(object savedState)
        {
            object[] oStates = (object[])savedState;

            _setEmptyToNull = (bool)oStates[1];
            _columnLength = (int)oStates[2];
            _columnDbType = (SqlDbType)oStates[3];
            _isInsertParameter = (bool)oStates[4];
            _insertParameterName = (string)oStates[5];
            _insertDirection = (ParameterDirection)oStates[6];
            _isUpdateParameter = (bool)oStates[7];
            _updateParameterName = (string)oStates[8];
            _updateDirection = (ParameterDirection)oStates[9];
            _isDeleteParameter = (bool)oStates[10];
            _deleteParameterName = (string)oStates[11];
            _deleteDirection = (ParameterDirection)oStates[12];
            _isSelectParameter = (bool)oStates[13];
            _selectParameterName = (string)oStates[14];
            _selectDirection = (ParameterDirection)oStates[15];
            _isBindParameter = (bool)oStates[16];
            _bindParameterName = (string)oStates[17];
            _disabledCss = (string)oStates[18];
            _submitButtonID = (string)oStates[19];
            _imeMode = (ImeMode)oStates[20];
            _enterKeyAction = (EnterKeyAction)oStates[21];
            _exceptionDesc = (string)oStates[22];
            _validation = (Validation)oStates[23];
            _nrequired = (bool)oStates[24];

            base.LoadControlState(oStates[0]);
        }

        protected override object SaveControlState()
        {
            object[] oStates = new object[25];
            oStates[0] = base.SaveControlState();
            oStates[1] = _setEmptyToNull;
            oStates[2] = _columnLength;
            oStates[3] = _columnDbType;
            oStates[4] = _isInsertParameter;
            oStates[5] = _insertParameterName;
            oStates[6] = _insertDirection;
            oStates[7] = _isUpdateParameter;
            oStates[8] = _updateParameterName;
            oStates[9] = _updateDirection;
            oStates[10] = _isDeleteParameter;
            oStates[11] = _deleteParameterName;
            oStates[12] = _deleteDirection;
            oStates[13] = _isSelectParameter;
            oStates[14] = _selectParameterName;
            oStates[15] = _selectDirection;
            oStates[16] = _isBindParameter;
            oStates[17] = _bindParameterName;
            oStates[18] = _disabledCss;
            oStates[19] = _submitButtonID;
            oStates[20] = _imeMode;
            oStates[21] = _enterKeyAction;
            oStates[22] = _exceptionDesc;
            oStates[23] = _validation;
            oStates[24] = _nrequired;

            return oStates;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        protected override void Render(HtmlTextWriter writer)
        {
            if (!this.Enabled && !String.IsNullOrEmpty(_disabledCss))
            {
                this.CssClass = _disabledCss;
            }

            if (Validation == Validation.Currency)
            {
                string val = string.IsNullOrEmpty(this.Text) ? string.Empty : string.Format("{0:" + FormatString + "}", Convert.ToDecimal(this.Text));
                writer.AddAttribute("value", val);
            }
            else
            {
                string val = string.IsNullOrEmpty(this.Text) ? string.Empty : Text.Replace("&lt;", "<").Replace("&gt;", ">");
                writer.AddAttribute("value", val);

            }

            base.Render(writer);
        }

        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            switch (_enterKeyAction)
            {
                case EnterKeyAction.MoveFocus:
                    writer.AddAttribute("onkeydown", this.Attributes["onkeydown"] + "EnterToTab(); SelectNextRow('" + this.ClientID + "');");
                    break;
                case EnterKeyAction.NoAction:
                    writer.AddAttribute("onkeydown", "return EnterToNoAction();");
                    break;
                case EnterKeyAction.SaveSubmit:
                    if (!String.IsNullOrEmpty(_submitButtonID))
                    {
                        if (!DesignMode)
                        {
                            Control submitControl = this.Parent.FindControl(_submitButtonID);

                            if (submitControl == null)
                                throw new ApplicationException("Control(" + submitControl + ")'s not found.");

                            string action = string.Empty;

                            if (submitControl is NButton)
                            {
                                NButton btn = (NButton)submitControl;

                                if (!string.IsNullOrEmpty(btn.OnClientClick))
                                {
                                    action = btn.OnClientClick;
                                }
                            }
                            else if (submitControl is NLinkButton)
                            {
                                NLinkButton btn = (NLinkButton)submitControl;

                                if (!string.IsNullOrEmpty(btn.OnClientClick))
                                {
                                    action = btn.OnClientClick;
                                }
                            }

                            writer.AddAttribute("onkeydown", String.Format("{0} return EnterToSubmit('{1}');", action + " else ", submitControl.UniqueID));

                            Page.ClientScript.GetPostBackEventReference(this, String.Empty);
                        }
                    }
                    break;
                case EnterKeyAction.Submit:
                    if (!String.IsNullOrEmpty(_submitButtonID))
                    {
                        if (!DesignMode)
                        {
                            Control submitControl = this.Parent.FindControl(_submitButtonID);

                            if (submitControl is NButton)
                            {
                                NButton btn = (submitControl) as NButton;

                                if (string.IsNullOrEmpty(btn.OnClientClick))
                                {
                                    writer.AddAttribute("onkeydown", String.Format("return EnterToSubmit('{0}');", submitControl.UniqueID));
                                }
                                else
                                {
                                    writer.AddAttribute("onkeydown", "javascript : if (window.event && event.keyCode == 13) {if (" + btn.OnClientClick + ") {return EnterToSubmit('" + submitControl.UniqueID + "');} else {return false;}} else {return event.keyCode;}");
                                }
                            }
                            else if (submitControl is NLinkButton)
                            {
                                NLinkButton btn = (submitControl) as NLinkButton;

                                if (string.IsNullOrEmpty(btn.OnClientClick))
                                {
                                    writer.AddAttribute("onkeydown", String.Format("return EnterToSubmit('{0}');", submitControl.UniqueID));
                                }
                                else
                                {
                                    writer.AddAttribute("onkeydown", "javascript : if (window.event && event.keyCode == 13) {if (" + btn.OnClientClick + ") {return EnterToSubmit('" + submitControl.UniqueID + "');} else {return false;}} else {return event.keyCode;}");
                                }
                            }
                            else
                            {
                                writer.AddAttribute("onkeydown", String.Format("return EnterToSubmit('{0}');", submitControl.UniqueID));
                            }

                            Page.ClientScript.GetPostBackEventReference(this, String.Empty);
                        }
                    }
                    break;
            }

            switch (_imeMode)
            {
                case ImeMode.Active:
                    writer.AddAttribute("ime-mode", "active");
                    break;
                case ImeMode.Auto:
                    writer.AddAttribute("ime-mode", "auto");
                    break;
                case ImeMode.Deactivated:
                    writer.AddAttribute("ime-mode", "deactivated");
                    break;
                case ImeMode.Inactive:
                    writer.AddAttribute("ime-mode", "inactive");
                    break;
            }

            switch (_nrequired)
            {
                case true:
                    writer.AddAttribute("nrequired", "True");
                    break;
                case false:
                    writer.AddAttribute("nrequired", "False");
                    break;
            }

            writer.AddAttribute("exceptionDesc", _exceptionDesc);
            writer.AddAttribute("validationGroup", this.ValidationGroup);

            switch (_validation)
            {
                //case Validation.Alphanumeric:
                //    writer.AddAttribute("validation", "alphanumeric");
                //    break;
                //case Validation.Alphanumericpassword:
                //    writer.AddAttribute("validation", "alphanumericpassword");
                //    break;
                //case Validation.Email:
                //    writer.AddAttribute("validation", "email");
                //    break;
                //case Validation.Korean:
                //    writer.AddAttribute("validation", "korean");
                //    break;
                case Validation.NotSet:
                    writer.AddAttribute("validation", "NotSet");
                    break;
                case Validation.Numeric:
                    writer.AddAttribute("validation", "numeric");
                    writer.AddAttribute("onkeypress", this.Attributes["onkeypress"] + "onlyNumber();");
                    writer.AddAttribute("style", "ime-mode:disabled");
                    break;
                //case Validation.Password:
                //    writer.AddAttribute("validation", "password");
                //    break;
                //case Validation.Ssn:
                //    writer.AddAttribute("validation", "password");
                //    break;
                case Validation.Date:
                    writer.AddAttribute("validation", "date");
                    writer.AddAttribute("onkeyup", this.Attributes["onkeyup"] + "setDateFormat(this);");
                    //writer.AddAttribute("onblur", this.Attributes["onBlur"] + "Date_OnBlur(this);");
                    this.MaxLength = 10;
                    break;
                case Validation.Decimal:
                    writer.AddAttribute("validation", "decimal");
                    break;
                case Validation.Currency:
                    writer.AddAttribute("onkeypress", this.Attributes["onkeypress"] + "onlyNumber();");
                    writer.AddAttribute("onfocus", this.Attributes["onfocus"] + "NumericTextBox_OnFocus(this);");
                    writer.AddAttribute("onblur", this.Attributes["onblur"] + "NumericTextBox_OnChange(this);");
                    writer.AddAttribute("FormatString", FormatString);
                    writer.AddAttribute("style", "ime-mode:disabled");
                    break;
                //case Validation.Currency_Focus:
                //    writer.AddAttribute("onkeypress", this.Attributes["onkeypress"] + "onlyNumber();");
                //    writer.AddAttribute("FormatString", FormatString);
                //    writer.AddAttribute("style", "ime-mode:disabled");
                //    break;
                case Validation.UnSignedNum:
                    writer.AddAttribute("onkeypress", this.Attributes["onkeypress"] + "UnSignedNumber();");
                    writer.AddAttribute("FormatString", FormatString);
                    writer.AddAttribute("style", "ime-mode:disabled");
                    break;
            }

            writer.AddAttribute("onfocus", this.Attributes["onfocus"] + "this.select();");
            writer.AddAttribute("onkeydown", this.Attributes["onkeydown"] + "SelectNextRow('" + this.ClientID + "');");


            base.AddAttributesToRender(writer);
        }

        #endregion
    }
}
