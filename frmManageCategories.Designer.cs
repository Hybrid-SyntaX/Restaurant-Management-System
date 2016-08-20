namespace Restaurant_Management_System
{
    partial class frmManageCategories
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmManageCategories));
            this.flwLayoutNewCategory = new System.Windows.Forms.FlowLayoutPanel();
            this.grpNewCategory = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.lblCategory = new System.Windows.Forms.Label();
            this.txtCategory = new System.Windows.Forms.TextBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.imgLstCategory = new System.Windows.Forms.ImageList(this.components);
            this.grpModifyCategory = new System.Windows.Forms.GroupBox();
            this.flwModifyCategory = new System.Windows.Forms.FlowLayoutPanel();
            this.lblCategory2 = new System.Windows.Forms.Label();
            this._txtCategory = new System.Windows.Forms.TextBox();
            this.comboCategory = new System.Windows.Forms.ComboBox();
            this.btnDeleteCategory = new System.Windows.Forms.Button();
            this.btnModifyCategory = new System.Windows.Forms.Button();
            this.flwLayoutNewCategory.SuspendLayout();
            this.grpNewCategory.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.grpModifyCategory.SuspendLayout();
            this.flwModifyCategory.SuspendLayout();
            this.SuspendLayout();
            // 
            // flwLayoutNewCategory
            // 
            this.flwLayoutNewCategory.BackColor = System.Drawing.Color.Transparent;
            this.flwLayoutNewCategory.Controls.Add(this.grpNewCategory);
            this.flwLayoutNewCategory.Controls.Add(this.grpModifyCategory);
            resources.ApplyResources(this.flwLayoutNewCategory, "flwLayoutNewCategory");
            this.flwLayoutNewCategory.Name = "flwLayoutNewCategory";
            // 
            // grpNewCategory
            // 
            this.grpNewCategory.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.grpNewCategory.Controls.Add(this.flowLayoutPanel1);
            this.grpNewCategory.ForeColor = System.Drawing.Color.Black;
            resources.ApplyResources(this.grpNewCategory, "grpNewCategory");
            this.grpNewCategory.Name = "grpNewCategory";
            this.grpNewCategory.TabStop = false;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(30)))), ((int)(((byte)(6)))));
            this.flowLayoutPanel1.Controls.Add(this.lblCategory);
            this.flowLayoutPanel1.Controls.Add(this.txtCategory);
            this.flowLayoutPanel1.Controls.Add(this.btnAdd);
            this.flowLayoutPanel1.ForeColor = System.Drawing.Color.White;
            resources.ApplyResources(this.flowLayoutPanel1, "flowLayoutPanel1");
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            // 
            // lblCategory
            // 
            resources.ApplyResources(this.lblCategory, "lblCategory");
            this.lblCategory.ForeColor = System.Drawing.Color.White;
            this.lblCategory.Name = "lblCategory";
            // 
            // txtCategory
            // 
            resources.ApplyResources(this.txtCategory, "txtCategory");
            this.txtCategory.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(243)))), ((int)(((byte)(248)))));
            this.txtCategory.Name = "txtCategory";
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(243)))), ((int)(((byte)(248)))));
            resources.ApplyResources(this.btnAdd, "btnAdd");
            this.btnAdd.ImageList = this.imgLstCategory;
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // imgLstCategory
            // 
            this.imgLstCategory.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgLstCategory.ImageStream")));
            this.imgLstCategory.TransparentColor = System.Drawing.Color.Transparent;
            this.imgLstCategory.Images.SetKeyName(0, "add_category.png");
            this.imgLstCategory.Images.SetKeyName(1, "category.png");
            this.imgLstCategory.Images.SetKeyName(2, "delete_category.png");
            this.imgLstCategory.Images.SetKeyName(3, "modify_generic.png");
            this.imgLstCategory.Images.SetKeyName(4, "delete_generic.png");
            this.imgLstCategory.Images.SetKeyName(5, "add_generic.png");
            // 
            // grpModifyCategory
            // 
            this.grpModifyCategory.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.grpModifyCategory.Controls.Add(this.flwModifyCategory);
            this.grpModifyCategory.ForeColor = System.Drawing.Color.Black;
            resources.ApplyResources(this.grpModifyCategory, "grpModifyCategory");
            this.grpModifyCategory.Name = "grpModifyCategory";
            this.grpModifyCategory.TabStop = false;
            // 
            // flwModifyCategory
            // 
            this.flwModifyCategory.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(30)))), ((int)(((byte)(6)))));
            this.flwModifyCategory.Controls.Add(this.lblCategory2);
            this.flwModifyCategory.Controls.Add(this._txtCategory);
            this.flwModifyCategory.Controls.Add(this.comboCategory);
            this.flwModifyCategory.Controls.Add(this.btnDeleteCategory);
            this.flwModifyCategory.Controls.Add(this.btnModifyCategory);
            this.flwModifyCategory.ForeColor = System.Drawing.Color.White;
            resources.ApplyResources(this.flwModifyCategory, "flwModifyCategory");
            this.flwModifyCategory.Name = "flwModifyCategory";
            // 
            // lblCategory2
            // 
            resources.ApplyResources(this.lblCategory2, "lblCategory2");
            this.lblCategory2.ForeColor = System.Drawing.Color.White;
            this.lblCategory2.Name = "lblCategory2";
            // 
            // _txtCategory
            // 
            this._txtCategory.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(243)))), ((int)(((byte)(248)))));
            resources.ApplyResources(this._txtCategory, "_txtCategory");
            this._txtCategory.Name = "_txtCategory";
            // 
            // comboCategory
            // 
            this.comboCategory.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(243)))), ((int)(((byte)(248)))));
            this.comboCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboCategory.FormattingEnabled = true;
            resources.ApplyResources(this.comboCategory, "comboCategory");
            this.comboCategory.Name = "comboCategory";
            // 
            // btnDeleteCategory
            // 
            this.btnDeleteCategory.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(243)))), ((int)(((byte)(248)))));
            resources.ApplyResources(this.btnDeleteCategory, "btnDeleteCategory");
            this.btnDeleteCategory.ImageList = this.imgLstCategory;
            this.btnDeleteCategory.Name = "btnDeleteCategory";
            this.btnDeleteCategory.UseVisualStyleBackColor = false;
            this.btnDeleteCategory.Click += new System.EventHandler(this.btnDeleteCategory_Click);
            // 
            // btnModifyCategory
            // 
            this.btnModifyCategory.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(243)))), ((int)(((byte)(248)))));
            resources.ApplyResources(this.btnModifyCategory, "btnModifyCategory");
            this.btnModifyCategory.ImageList = this.imgLstCategory;
            this.btnModifyCategory.Name = "btnModifyCategory";
            this.btnModifyCategory.UseVisualStyleBackColor = false;
            this.btnModifyCategory.Click += new System.EventHandler(this.btnModifyCategory_Click);
            // 
            // frmManageCategories
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.Controls.Add(this.flwLayoutNewCategory);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmManageCategories";
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.frmNewCategory_Load);
            this.flwLayoutNewCategory.ResumeLayout(false);
            this.grpNewCategory.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.grpModifyCategory.ResumeLayout(false);
            this.flwModifyCategory.ResumeLayout(false);
            this.flwModifyCategory.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flwLayoutNewCategory;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.TextBox txtCategory;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.GroupBox grpNewCategory;
        private System.Windows.Forms.GroupBox grpModifyCategory;
        private System.Windows.Forms.Button btnDeleteCategory;
        private System.Windows.Forms.ComboBox comboCategory;
        private System.Windows.Forms.Label lblCategory2;
        private System.Windows.Forms.ImageList imgLstCategory;
        private System.Windows.Forms.Button btnModifyCategory;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flwModifyCategory;
        private System.Windows.Forms.TextBox _txtCategory;

    }
}