namespace Restaurant_Management_System
{
    partial class frmLogin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLogin));
            this.lblUsername = new System.Windows.Forms.Label();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.btnLogin = new System.Windows.Forms.Button();
            this.flwLayoutLogin = new System.Windows.Forms.FlowLayoutPanel();
            this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.subMnuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuLanguage = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuStripLogin = new System.Windows.Forms.MenuStrip();
            this.flwLayoutLogin.SuspendLayout();
            this.mnuStripLogin.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblUsername
            // 
            this.lblUsername.ForeColor = System.Drawing.Color.White;
            resources.ApplyResources(this.lblUsername, "lblUsername");
            this.lblUsername.Name = "lblUsername";
            // 
            // txtUsername
            // 
            this.txtUsername.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(243)))), ((int)(((byte)(248)))));
            resources.ApplyResources(this.txtUsername, "txtUsername");
            this.txtUsername.Name = "txtUsername";
            // 
            // txtPassword
            // 
            this.txtPassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(243)))), ((int)(((byte)(248)))));
            resources.ApplyResources(this.txtPassword, "txtPassword");
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.UseSystemPasswordChar = true;
            // 
            // lblPassword
            // 
            this.lblPassword.ForeColor = System.Drawing.Color.White;
            resources.ApplyResources(this.lblPassword, "lblPassword");
            this.lblPassword.Name = "lblPassword";
            // 
            // btnLogin
            // 
            this.btnLogin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(243)))), ((int)(((byte)(248)))));
            resources.ApplyResources(this.btnLogin, "btnLogin");
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.UseVisualStyleBackColor = false;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // flwLayoutLogin
            // 
            this.flwLayoutLogin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(30)))), ((int)(((byte)(6)))));
            this.flwLayoutLogin.Controls.Add(this.lblUsername);
            this.flwLayoutLogin.Controls.Add(this.txtUsername);
            this.flwLayoutLogin.Controls.Add(this.lblPassword);
            this.flwLayoutLogin.Controls.Add(this.txtPassword);
            this.flwLayoutLogin.Controls.Add(this.btnLogin);
            resources.ApplyResources(this.flwLayoutLogin, "flwLayoutLogin");
            this.flwLayoutLogin.Name = "flwLayoutLogin";
            // 
            // mnuFile
            // 
            this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.subMnuExit});
            this.mnuFile.Name = "mnuFile";
            resources.ApplyResources(this.mnuFile, "mnuFile");
            // 
            // subMnuExit
            // 
            this.subMnuExit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.subMnuExit.Name = "subMnuExit";
            resources.ApplyResources(this.subMnuExit, "subMnuExit");
            this.subMnuExit.Click += new System.EventHandler(this.subMnuExit_Click);
            // 
            // mnuLanguage
            // 
            this.mnuLanguage.Name = "mnuLanguage";
            resources.ApplyResources(this.mnuLanguage, "mnuLanguage");
            // 
            // mnuAbout
            // 
            this.mnuAbout.Name = "mnuAbout";
            resources.ApplyResources(this.mnuAbout, "mnuAbout");
            this.mnuAbout.Click += new System.EventHandler(this.mnuAbout_Click);
            // 
            // mnuStripLogin
            // 
            this.mnuStripLogin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.mnuStripLogin.BackgroundImage = global::Restaurant_Management_System.Properties.Resources.background3;
            resources.ApplyResources(this.mnuStripLogin, "mnuStripLogin");
            this.mnuStripLogin.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile,
            this.mnuLanguage,
            this.mnuAbout});
            this.mnuStripLogin.Name = "mnuStripLogin";
            // 
            // frmLogin
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.flwLayoutLogin);
            this.Controls.Add(this.mnuStripLogin);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmLogin";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmLogin_FormClosed);
            this.Load += new System.EventHandler(this.frmLogin_Load);
            this.flwLayoutLogin.ResumeLayout(false);
            this.flwLayoutLogin.PerformLayout();
            this.mnuStripLogin.ResumeLayout(false);
            this.mnuStripLogin.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.FlowLayoutPanel flwLayoutLogin;
        private System.Windows.Forms.ToolStripMenuItem mnuFile;
        private System.Windows.Forms.ToolStripMenuItem subMnuExit;
        private System.Windows.Forms.ToolStripMenuItem mnuLanguage;
        private System.Windows.Forms.ToolStripMenuItem mnuAbout;
        private System.Windows.Forms.MenuStrip mnuStripLogin;
    }
}