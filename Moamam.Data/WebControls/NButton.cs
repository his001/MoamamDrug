#region Using Directives

using System;
using System.ComponentModel;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Moamam.Data.Common;

#endregion

namespace Moamam.Data.WebControls
{

    [ToolboxData("<{0}:NButton runat=server></{0}:NButton>")]
    public class NButton : Button, IButtonExtender
    {
        #region  Private Members

        private const string SCRIPT_CONFIRM = "if (!confirm('{0}')) return false;";

        private string _disabledCss = String.Empty;

        private bool _isConfirm = false;
        private string _confirmMessage = String.Empty;
        private SecurityType _securityType = SecurityType.NotSet;
        private ButtonCmdType _CmdType;

        #endregion

        #region Properties
        [Category("CmdType")]
        [DefaultValue(ButtonCmdType.SELECT)]
        public ButtonCmdType CmdType
        {
            set { this._CmdType = (ButtonCmdType)value; }
            get { return this._CmdType; }
        }

        [Category("Appearance")]
        [DefaultValue("")]
        public string DisabledCss
        {
            get { return _disabledCss; }
            set { _disabledCss = value; }
        }

        #endregion

        #region IButtonExtender Members

        [Category("Confirm")]
        [DefaultValue(false)]
        public bool IsConfirm
        {
            get { return _isConfirm; }
            set { _isConfirm = value; }
        }

        [Category("Confirm")]
        [DefaultValue("")]
        public string ConfirmMessage
        {
            get { return _confirmMessage; }
            set { _confirmMessage = value; }
        }

        [Category("Security")]
        [DefaultValue(SecurityType.NotSet)]
        public SecurityType SecurityType
        {
            get { return _securityType; }
            set { _securityType = value; }
        }

        #endregion

        #region Overrides

        protected override void OnInit(EventArgs e)
        {
            Page.RegisterRequiresControlState(this);

            base.OnInit(e);
        }

        protected override void LoadControlState(object savedState)
        {
            object[] oStates = (object[])savedState;
            _isConfirm = (bool)oStates[1];
            _confirmMessage = (string)oStates[2];
            _securityType = (SecurityType)oStates[3];
            _disabledCss = (string)oStates[4];

            base.LoadControlState(oStates[0]);
        }

        protected override object SaveControlState()
        {
            object[] oStates = new object[5];
            oStates[0] = base.SaveControlState();
            oStates[1] = _isConfirm;
            oStates[2] = _confirmMessage;
            oStates[3] = _securityType;
            oStates[4] = _disabledCss;

            return oStates;
        }

        protected override void Render(HtmlTextWriter writer)
        {
            if (_isConfirm && !String.IsNullOrEmpty(_confirmMessage))
            {
                this.OnClientClick += String.Format(SCRIPT_CONFIRM, _confirmMessage);
            }

            if (!this.Enabled && !String.IsNullOrEmpty(_disabledCss))
            {
                this.CssClass = _disabledCss;
            }

            base.Render(writer);
        }

        protected override void OnClick(EventArgs e)
        {
            try
            {
                UserInfo ui = (UserInfo)HttpContext.Current.Session[ConfigurationManager.AppSettings["SysName"].ToString()];
                if (ui != null)
                {
                    (new SiteGrant()).SetUserLog(
                        ui.UserID,
                        HttpContext.Current.Session["menuCd"].ToString(),
                        this.CmdType.ToString());
                }
            }
            catch (Exception ex) {  }
            base.OnClick(e);
        }

        #endregion
    }
}
