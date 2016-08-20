namespace Restaurant_Management_System
{
    partial class frmChangeCategory
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
            this.flwLayoutChangeCategory = new System.Windows.Forms.FlowLayoutPanel();
            this.lblCategory = new System.Windows.Forms.Label();
            this.comboCategory = new System.Windows.Forms.ComboBox();
            this.btnChangeCategory = new System.Windows.Forms.Button();
            this.flwLayoutChangeCategory.SuspendLayout();
            this.SuspendLayout();
            // 
            // flwLayoutChangeCategory
            // 
            this.flwLayoutChangeCategory.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(30)))), ((int)(((byte)(6)))));
            this.flwLayoutChangeCategory.Controls.Add(this.lblCategory);
            this.flwLayoutChangeCategory.Controls.Add(this.comboCategory);
            this.flwLayoutChangeCategory.Controls.Add(this.btnChangeCategory);
            this.flwLayoutChangeCategory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flwLayoutChangeCategory.Location = new System.Drawing.Point(0, 0);
            this.flwLayoutChangeCategory.Name = "flwLayoutChangeCategory";
            this.flwLayoutChangeCategory.Size = new System.Drawing.Size(453, 28);
            this.flwLayoutChangeCategory.TabIndex = 0;
            // 
            // lblCategory
            // 
            this.lblCategory.AutoSize = true;
            this.lblCategory.ForeColor = System.Drawing.Color.White;
            this.lblCategory.Location = new System.Drawing.Point(3, 7);
            this.lblCategory.Margin = new System.Windows.Forms.Padding(3, 7, 3, 0);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(49, 13);
            this.lblCategory.TabIndex = 1;
            this.lblCategory.Text = "Category";
            // 
            // comboCategory
            // 
            this.comboCategory.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(243)))), ((int)(((byte)(248)))));
            this.comboCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboCategory.FormattingEnabled = true;
            this.comboCategory.Location = new System.Drawing.Point(58, 3);
            this.comboCategory.Name = "comboCategory";
            this.comboCategory.Size = new System.Drawing.Size(221, 21);
            this.comboCategory.TabIndex = 2;
            // 
            // btnChangeCategory
            // 
            this.btnChangeCategory.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(243)))), ((int)(((byte)(248)))));
            this.btnChangeCategory.Location = new System.Drawing.Point(285, 3);
            this.btnChangeCategory.Name = "btnChangeCategory";
            this.btnChangeCategory.Size = new System.Drawing.Size(151, 23);
            this.btnChangeCategory.TabIndex = 3;
            this.btnChangeCategory.Text = "Change";
            this.btnChangeCategory.UseVisualStyleBackColor = false;
            this.btnChangeCategory.Click += new System.EventHandler(this.btnChangeCategory_Click);
            // 
            // frmChangeCategory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(453, 28);
            this.Controls.Add(this.flwLayoutChangeCategory);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmChangeCategory";
            this.ShowInTaskbar = false;
            this.Text = "Change Category";
            this.Load += new System.EventHandler(this.frmChangeCategory_Load);
            this.flwLayoutChangeCategory.ResumeLayout(false);
            this.flwLayoutChangeCategory.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flwLayoutChangeCategory;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.ComboBox comboCategory;
        private System.Windows.Forms.Button btnChangeCategory;
    }
}