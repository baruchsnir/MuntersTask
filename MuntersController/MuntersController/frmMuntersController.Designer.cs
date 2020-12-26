using FT.CommonDll;

namespace MuntersController
{
    partial class frmMuntersController
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            Layers_Handler.instance().MessageBoxEven -= this.OnMessageBoxEvent;
            Layers_Handler.instance().DisplayEvent -= this.OnDisplayEvent;
            Layers_Handler.instance().LogEvent -= this.OnChangLogLinesEvent;
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMuntersController));
            this.pnlUser = new System.Windows.Forms.Panel();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.lblUserName = new System.Windows.Forms.Label();
            this.btnLogin = new System.Windows.Forms.Button();
            this.statusBar = new System.Windows.Forms.StatusStrip();
            this.toolStripBetsStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.tmrUpdateLogs = new System.Windows.Forms.Timer(this.components);
            this.pnlTest = new System.Windows.Forms.Panel();
            this.btnTestStatus = new System.Windows.Forms.Button();
            this.redLog = new System.Windows.Forms.RichTextBox();
            this.pnlUser.SuspendLayout();
            this.statusBar.SuspendLayout();
            this.pnlTest.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlUser
            // 
            this.pnlUser.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlUser.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlUser.Controls.Add(this.txtPassword);
            this.pnlUser.Controls.Add(this.lblPassword);
            this.pnlUser.Controls.Add(this.txtUserName);
            this.pnlUser.Controls.Add(this.lblUserName);
            this.pnlUser.Controls.Add(this.btnLogin);
            this.pnlUser.Location = new System.Drawing.Point(16, 37);
            this.pnlUser.Margin = new System.Windows.Forms.Padding(4);
            this.pnlUser.Name = "pnlUser";
            this.pnlUser.Size = new System.Drawing.Size(1318, 106);
            this.pnlUser.TabIndex = 73;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(152, 59);
            this.txtPassword.Margin = new System.Windows.Forms.Padding(4);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(181, 22);
            this.txtPassword.TabIndex = 69;
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(28, 63);
            this.lblPassword.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(69, 17);
            this.lblPassword.TabIndex = 70;
            this.lblPassword.Text = "Password";
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(152, 14);
            this.txtUserName.Margin = new System.Windows.Forms.Padding(4);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(181, 22);
            this.txtUserName.TabIndex = 69;
            // 
            // lblUserName
            // 
            this.lblUserName.AutoSize = true;
            this.lblUserName.Location = new System.Drawing.Point(28, 18);
            this.lblUserName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(79, 17);
            this.lblUserName.TabIndex = 70;
            this.lblUserName.Text = "User Name";
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(1124, 18);
            this.btnLogin.Margin = new System.Windows.Forms.Padding(4);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(149, 28);
            this.btnLogin.TabIndex = 71;
            this.btnLogin.Text = "Run Test";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // statusBar
            // 
            this.statusBar.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripBetsStatus});
            this.statusBar.Location = new System.Drawing.Point(0, 542);
            this.statusBar.Name = "statusBar";
            this.statusBar.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this.statusBar.Size = new System.Drawing.Size(1347, 22);
            this.statusBar.TabIndex = 74;
            this.statusBar.Text = "statusStrip1";
            // 
            // toolStripBetsStatus
            // 
            this.toolStripBetsStatus.Name = "toolStripBetsStatus";
            this.toolStripBetsStatus.Size = new System.Drawing.Size(0, 16);
            // 
            // tmrUpdateLogs
            // 
            this.tmrUpdateLogs.Interval = 1000;
            this.tmrUpdateLogs.Tick += new System.EventHandler(this.tmrUpdateLogs_Tick);
            // 
            // pnlTest
            // 
            this.pnlTest.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlTest.Controls.Add(this.btnTestStatus);
            this.pnlTest.Controls.Add(this.redLog);
            this.pnlTest.Location = new System.Drawing.Point(16, 150);
            this.pnlTest.Name = "pnlTest";
            this.pnlTest.Size = new System.Drawing.Size(1318, 389);
            this.pnlTest.TabIndex = 75;
            // 
            // btnTestStatus
            // 
            this.btnTestStatus.BackColor = System.Drawing.Color.Red;
            this.btnTestStatus.Enabled = false;
            this.btnTestStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 22.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTestStatus.ForeColor = System.Drawing.Color.Yellow;
            this.btnTestStatus.Location = new System.Drawing.Point(911, 32);
            this.btnTestStatus.Name = "btnTestStatus";
            this.btnTestStatus.Size = new System.Drawing.Size(347, 107);
            this.btnTestStatus.TabIndex = 3;
            this.btnTestStatus.Text = "Pass Test";
            this.btnTestStatus.UseVisualStyleBackColor = false;
            this.btnTestStatus.Visible = false;
            // 
            // redLog
            // 
            this.redLog.AcceptsTab = true;
            this.redLog.BackColor = System.Drawing.Color.White;
            this.redLog.Dock = System.Windows.Forms.DockStyle.Left;
            this.redLog.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.redLog.Location = new System.Drawing.Point(0, 0);
            this.redLog.Name = "redLog";
            this.redLog.ReadOnly = true;
            this.redLog.Size = new System.Drawing.Size(866, 389);
            this.redLog.TabIndex = 2;
            this.redLog.Text = "";
            this.redLog.WordWrap = false;
            // 
            // frmMuntersController
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1347, 564);
            this.Controls.Add(this.pnlTest);
            this.Controls.Add(this.statusBar);
            this.Controls.Add(this.pnlUser);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmMuntersController";
            this.Text = "Munters Controller";
            this.Load += new System.EventHandler(this.frmSendBets_Load);
            this.pnlUser.ResumeLayout(false);
            this.pnlUser.PerformLayout();
            this.statusBar.ResumeLayout(false);
            this.statusBar.PerformLayout();
            this.pnlTest.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlUser;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.StatusStrip statusBar;
        private System.Windows.Forms.ToolStripStatusLabel toolStripBetsStatus;
        private System.Windows.Forms.Timer tmrUpdateLogs;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.Panel pnlTest;
        private System.Windows.Forms.Button btnTestStatus;
        private System.Windows.Forms.RichTextBox redLog;
    }
}

