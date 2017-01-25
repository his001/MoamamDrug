namespace getdrug
{
    partial class FrmMain
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다.
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.txt_Url = new System.Windows.Forms.TextBox();
            this.txt_page = new System.Windows.Forms.TextBox();
            this.lbl_Url = new System.Windows.Forms.Label();
            this.lbl_Page = new System.Windows.Forms.Label();
            this.btn_Run = new System.Windows.Forms.Button();
            this.lbl_Status = new System.Windows.Forms.Label();
            this.txt_result = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // webBrowser1
            // 
            this.webBrowser1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.webBrowser1.Location = new System.Drawing.Point(12, 39);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(386, 478);
            this.webBrowser1.TabIndex = 0;
            // 
            // txt_Url
            // 
            this.txt_Url.Location = new System.Drawing.Point(46, 12);
            this.txt_Url.Name = "txt_Url";
            this.txt_Url.Size = new System.Drawing.Size(480, 21);
            this.txt_Url.TabIndex = 1;
            this.txt_Url.Text = resources.GetString("txt_Url.Text");
            // 
            // txt_page
            // 
            this.txt_page.Location = new System.Drawing.Point(586, 12);
            this.txt_page.Name = "txt_page";
            this.txt_page.Size = new System.Drawing.Size(51, 21);
            this.txt_page.TabIndex = 2;
            this.txt_page.Text = "1";
            // 
            // lbl_Url
            // 
            this.lbl_Url.AutoSize = true;
            this.lbl_Url.Location = new System.Drawing.Point(12, 21);
            this.lbl_Url.Name = "lbl_Url";
            this.lbl_Url.Size = new System.Drawing.Size(28, 12);
            this.lbl_Url.TabIndex = 3;
            this.lbl_Url.Text = "URL";
            // 
            // lbl_Page
            // 
            this.lbl_Page.AutoSize = true;
            this.lbl_Page.Location = new System.Drawing.Point(532, 15);
            this.lbl_Page.Name = "lbl_Page";
            this.lbl_Page.Size = new System.Drawing.Size(34, 12);
            this.lbl_Page.TabIndex = 4;
            this.lbl_Page.Text = "Page";
            // 
            // btn_Run
            // 
            this.btn_Run.Location = new System.Drawing.Point(656, 9);
            this.btn_Run.Name = "btn_Run";
            this.btn_Run.Size = new System.Drawing.Size(75, 23);
            this.btn_Run.TabIndex = 5;
            this.btn_Run.Text = "실행";
            this.btn_Run.UseVisualStyleBackColor = true;
            this.btn_Run.Click += new System.EventHandler(this.btn_Run_Click);
            // 
            // lbl_Status
            // 
            this.lbl_Status.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_Status.AutoSize = true;
            this.lbl_Status.Location = new System.Drawing.Point(751, 14);
            this.lbl_Status.Name = "lbl_Status";
            this.lbl_Status.Size = new System.Drawing.Size(29, 12);
            this.lbl_Status.TabIndex = 6;
            this.lbl_Status.Text = "stop";
            // 
            // txt_result
            // 
            this.txt_result.Location = new System.Drawing.Point(405, 39);
            this.txt_result.Multiline = true;
            this.txt_result.Name = "txt_result";
            this.txt_result.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txt_result.Size = new System.Drawing.Size(398, 478);
            this.txt_result.TabIndex = 7;
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(815, 529);
            this.Controls.Add(this.txt_result);
            this.Controls.Add(this.lbl_Status);
            this.Controls.Add(this.btn_Run);
            this.Controls.Add(this.lbl_Page);
            this.Controls.Add(this.lbl_Url);
            this.Controls.Add(this.txt_page);
            this.Controls.Add(this.txt_Url);
            this.Controls.Add(this.webBrowser1);
            this.Name = "FrmMain";
            this.Text = "공공DB낱알정보";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.TextBox txt_Url;
        private System.Windows.Forms.TextBox txt_page;
        private System.Windows.Forms.Label lbl_Url;
        private System.Windows.Forms.Label lbl_Page;
        private System.Windows.Forms.Button btn_Run;
        private System.Windows.Forms.Label lbl_Status;
        private System.Windows.Forms.TextBox txt_result;
    }
}

