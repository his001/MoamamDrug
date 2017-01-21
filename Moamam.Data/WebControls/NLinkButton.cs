#region Using Directives

using System;
using System.ComponentModel;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.Design;
using System.Drawing.Design;

using Moamam.Data.Common;

#endregion

namespace Moamam.Data.WebControls
{
    public enum ButtonCmdType
    {
        SELECT,
        INSERT,
        DELETE,
        UPDATE,
        PRINT,
        EXCEL,
        MASSUPDATE,
        MASSUPLOAD,
        EXECUTE,
        VIEW
    }

    [ToolboxData("<{0}:NLinkButton runat=server></{0}:NLinkButton>")]
    public class NLinkButton : LinkButton, IButtonExtender
    {
        #region  Private Members

        private const string SCRIPT_CONFIRM = "if (!confirm('{0}')) return false;";
        private const string HTML_IMAGE = "<img src=\"{0}\" alt=\"{1}\" align=\"absmiddle\" style=\"border:0;\" />";

        private string _disabledCss = String.Empty;
        private string _imageUrl = String.Empty;
        private string _disableImageUrl = String.Empty;

        private bool _isConfirm;
        private string _confirmMessage;
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

        [DefaultValue("")]
        [Category("Appearance")]
        [Editor(typeof(ImageUrlEditor), typeof(UITypeEditor))]
        public string ImageUrl
        {
            get { return _imageUrl; }
            set { _imageUrl = value; }
        }

        [DefaultValue("")]
        [Category("Appearance")]
        [Editor(typeof(ImageUrlEditor), typeof(UITypeEditor))]
        public string DisableImageUrl
        {
            get { return _disableImageUrl; }
            set { _disableImageUrl = value; }
        }

        #endregion

        #region IButtonExtender Members

        [Category("Confirm")]
        public bool IsConfirm
        {
            get { return _isConfirm; }
            set { _isConfirm = value; }
        }

        [Category("Confirm")]
        public string ConfirmMessage
        {
            get { return _confirmMessage; }
            set { _confirmMessage = value; }
        }

        [Category("Security")]
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
            _imageUrl = (string)oStates[5];
            _disableImageUrl = (string)oStates[6];

            base.LoadControlState(oStates[0]);
        }

        protected override object SaveControlState()
        {
            object[] oStates = new object[7];
            oStates[0] = base.SaveControlState();
            oStates[1] = _isConfirm;
            oStates[2] = _confirmMessage;
            oStates[3] = _securityType;
            oStates[4] = _disabledCss;
            oStates[5] = _imageUrl;
            oStates[6] = _disableImageUrl;

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

            if (this.Enabled && !String.IsNullOrEmpty(_imageUrl))
            {
                this.Text = String.Format(HTML_IMAGE, this.ResolveClientUrl(_imageUrl), this.Text);
            }
            else if (!this.Enabled && !String.IsNullOrEmpty(_disableImageUrl))
            {
                this.Text = String.Format(HTML_IMAGE, this.ResolveClientUrl(_disableImageUrl), this.Text);
            }

            base.Render(writer);
        }

        protected override void OnClick(EventArgs e)
        {
            UserInfo ui = (UserInfo)HttpContext.Current.Session[ConfigurationManager.AppSettings["SysName"].ToString()];

            if (ui != null)
            {
                (new SiteGrant()).SetUserLog(
                    ui.UserID,
                    HttpContext.Current.Session["menuCd"].ToString(),
                    this.CmdType.ToString());
            }

            base.OnClick(e);
        }

        #endregion
    }
}
