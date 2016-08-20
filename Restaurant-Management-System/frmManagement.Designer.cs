namespace Restaurant_Management_System
{
    partial class frmManagement
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmManagement));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.tabFood = new System.Windows.Forms.TabPage();
            this.dgFood = new System.Windows.Forms.DataGridView();
            this.flwLayoutPnlFood = new System.Windows.Forms.FlowLayoutPanel();
            this.lblCategory = new System.Windows.Forms.Label();
            this.comboCategory = new System.Windows.Forms.ComboBox();
            this.lblSearch = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.tabEmployee = new System.Windows.Forms.TabPage();
            this.dgEmployee = new System.Windows.Forms.DataGridView();
            this.flwLayoutEmployee = new System.Windows.Forms.FlowLayoutPanel();
            this.lblSearchEmployee = new System.Windows.Forms.Label();
            this.txtSearchEmployee = new System.Windows.Forms.TextBox();
            this.tabManagement = new System.Windows.Forms.TabControl();
            this.tabStatistics = new System.Windows.Forms.TabPage();
            this.tabCtrlStatistics = new System.Windows.Forms.TabControl();
            this.tabSales = new System.Windows.Forms.TabPage();
            this.flwLayoutSalesFilter = new System.Windows.Forms.FlowLayoutPanel();
            this.chkYear = new System.Windows.Forms.CheckBox();
            this.comboYear = new System.Windows.Forms.ComboBox();
            this.chkMonth = new System.Windows.Forms.CheckBox();
            this.comboMonth = new System.Windows.Forms.ComboBox();
            this.chkDay = new System.Windows.Forms.CheckBox();
            this.comboDay = new System.Windows.Forms.ComboBox();
            this.chrtSales = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.toolbarManagement = new System.Windows.Forms.ToolStrip();
            this.tsBtnSave = new System.Windows.Forms.ToolStripButton();
            this.tsBtnDiscard = new System.Windows.Forms.ToolStripButton();
            this.tsBtnEdit = new System.Windows.Forms.ToolStripButton();
            this.tsBtnAdd = new System.Windows.Forms.ToolStripButton();
            this.tsBtnDelete = new System.Windows.Forms.ToolStripButton();
            this.tsSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnManageCategories = new System.Windows.Forms.ToolStripButton();
            this.tsBtnChangeCategory = new System.Windows.Forms.ToolStripButton();
            this.menuStripManagement = new System.Windows.Forms.MenuStrip();
            this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.subMnuSwitchToMain = new System.Windows.Forms.ToolStripMenuItem();
            this.subMnuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuLanguage = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.tabFood.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgFood)).BeginInit();
            this.flwLayoutPnlFood.SuspendLayout();
            this.tabEmployee.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgEmployee)).BeginInit();
            this.flwLayoutEmployee.SuspendLayout();
            this.tabManagement.SuspendLayout();
            this.tabStatistics.SuspendLayout();
            this.tabCtrlStatistics.SuspendLayout();
            this.tabSales.SuspendLayout();
            this.flwLayoutSalesFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chrtSales)).BeginInit();
            this.toolbarManagement.SuspendLayout();
            this.menuStripManagement.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabFood
            // 
            this.tabFood.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(30)))), ((int)(((byte)(6)))));
            this.tabFood.Controls.Add(this.dgFood);
            this.tabFood.Controls.Add(this.flwLayoutPnlFood);
            resources.ApplyResources(this.tabFood, "tabFood");
            this.tabFood.Name = "tabFood";
            this.tabFood.Enter += new System.EventHandler(this.tabFood_Enter);
            // 
            // dgFood
            // 
            this.dgFood.AllowUserToOrderColumns = true;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(247)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Maroon;
            this.dgFood.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgFood.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(243)))), ((int)(((byte)(248)))));
            this.dgFood.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            resources.ApplyResources(this.dgFood, "dgFood");
            this.dgFood.Name = "dgFood";
            this.dgFood.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            this.dgFood.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.dgFood.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;
            this.dgFood.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgFood.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dgFood_CellBeginEdit);
            this.dgFood.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgFood_CellEndEdit);
            this.dgFood.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgFood_DataError);
            this.dgFood.SelectionChanged += new System.EventHandler(this.dgFood_SelectionChanged);
            this.dgFood.UserAddedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.dgFood_UserAddedRow);
            this.dgFood.UserDeletedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.commonDataGrid_UserDeletedRow);
            this.dgFood.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.dgFood_UserDeletingRow);
            // 
            // flwLayoutPnlFood
            // 
            this.flwLayoutPnlFood.Controls.Add(this.lblCategory);
            this.flwLayoutPnlFood.Controls.Add(this.comboCategory);
            this.flwLayoutPnlFood.Controls.Add(this.lblSearch);
            this.flwLayoutPnlFood.Controls.Add(this.txtSearch);
            resources.ApplyResources(this.flwLayoutPnlFood, "flwLayoutPnlFood");
            this.flwLayoutPnlFood.Name = "flwLayoutPnlFood";
            // 
            // lblCategory
            // 
            resources.ApplyResources(this.lblCategory, "lblCategory");
            this.lblCategory.ForeColor = System.Drawing.Color.White;
            this.lblCategory.Name = "lblCategory";
            // 
            // comboCategory
            // 
            this.comboCategory.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(243)))), ((int)(((byte)(248)))));
            this.comboCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboCategory.FormattingEnabled = true;
            resources.ApplyResources(this.comboCategory, "comboCategory");
            this.comboCategory.Name = "comboCategory";
            this.comboCategory.SelectedIndexChanged += new System.EventHandler(this.comboCategory_SelectedIndexChanged);
            // 
            // lblSearch
            // 
            resources.ApplyResources(this.lblSearch, "lblSearch");
            this.lblSearch.ForeColor = System.Drawing.Color.White;
            this.lblSearch.Name = "lblSearch";
            // 
            // txtSearch
            // 
            this.txtSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(243)))), ((int)(((byte)(248)))));
            resources.ApplyResources(this.txtSearch, "txtSearch");
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // tabEmployee
            // 
            this.tabEmployee.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(30)))), ((int)(((byte)(6)))));
            this.tabEmployee.Controls.Add(this.dgEmployee);
            this.tabEmployee.Controls.Add(this.flwLayoutEmployee);
            resources.ApplyResources(this.tabEmployee, "tabEmployee");
            this.tabEmployee.Name = "tabEmployee";
            this.tabEmployee.Enter += new System.EventHandler(this.tabEmployee_Enter);
            // 
            // dgEmployee
            // 
            this.dgEmployee.AllowUserToOrderColumns = true;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(247)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Maroon;
            this.dgEmployee.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dgEmployee.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(243)))), ((int)(((byte)(248)))));
            this.dgEmployee.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            resources.ApplyResources(this.dgEmployee, "dgEmployee");
            this.dgEmployee.Name = "dgEmployee";
            this.dgEmployee.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            this.dgEmployee.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.dgEmployee.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;
            this.dgEmployee.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgEmployee.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dgEmployee_CellBeginEdit);
            this.dgEmployee.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgEmployee_CellEndEdit);
            this.dgEmployee.SelectionChanged += new System.EventHandler(this.dgEmployee_SelectionChanged);
            this.dgEmployee.UserAddedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.dgEmployee_UserAddedRow);
            this.dgEmployee.UserDeletedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.commonDataGrid_UserDeletedRow);
            this.dgEmployee.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.dgEmployee_UserDeletingRow);
            // 
            // flwLayoutEmployee
            // 
            this.flwLayoutEmployee.Controls.Add(this.lblSearchEmployee);
            this.flwLayoutEmployee.Controls.Add(this.txtSearchEmployee);
            resources.ApplyResources(this.flwLayoutEmployee, "flwLayoutEmployee");
            this.flwLayoutEmployee.Name = "flwLayoutEmployee";
            // 
            // lblSearchEmployee
            // 
            resources.ApplyResources(this.lblSearchEmployee, "lblSearchEmployee");
            this.lblSearchEmployee.ForeColor = System.Drawing.Color.White;
            this.lblSearchEmployee.Name = "lblSearchEmployee";
            // 
            // txtSearchEmployee
            // 
            this.txtSearchEmployee.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(243)))), ((int)(((byte)(248)))));
            resources.ApplyResources(this.txtSearchEmployee, "txtSearchEmployee");
            this.txtSearchEmployee.Name = "txtSearchEmployee";
            this.txtSearchEmployee.TextChanged += new System.EventHandler(this.txtSearchEmployee_TextChanged);
            // 
            // tabManagement
            // 
            resources.ApplyResources(this.tabManagement, "tabManagement");
            this.tabManagement.Controls.Add(this.tabFood);
            this.tabManagement.Controls.Add(this.tabEmployee);
            this.tabManagement.Controls.Add(this.tabStatistics);
            this.tabManagement.Multiline = true;
            this.tabManagement.Name = "tabManagement";
            this.tabManagement.SelectedIndex = 0;
            this.tabManagement.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            
            // 
            // tabStatistics
            // 
            this.tabStatistics.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(30)))), ((int)(((byte)(6)))));
            this.tabStatistics.Controls.Add(this.tabCtrlStatistics);
            resources.ApplyResources(this.tabStatistics, "tabStatistics");
            this.tabStatistics.Name = "tabStatistics";
            this.tabStatistics.Enter += new System.EventHandler(this.tabStatistics_Enter);
            // 
            // tabCtrlStatistics
            // 
            this.tabCtrlStatistics.Controls.Add(this.tabSales);
            resources.ApplyResources(this.tabCtrlStatistics, "tabCtrlStatistics");
            this.tabCtrlStatistics.Name = "tabCtrlStatistics";
            this.tabCtrlStatistics.SelectedIndex = 0;
            
            // 
            // tabSales
            // 
            this.tabSales.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(243)))), ((int)(((byte)(248)))));
            this.tabSales.Controls.Add(this.chrtSales);
            this.tabSales.Controls.Add(this.flwLayoutSalesFilter);
            resources.ApplyResources(this.tabSales, "tabSales");
            this.tabSales.Name = "tabSales";
            // 
            // flwLayoutSalesFilter
            // 
            this.flwLayoutSalesFilter.BackColor = System.Drawing.Color.Transparent;
            this.flwLayoutSalesFilter.Controls.Add(this.chkYear);
            this.flwLayoutSalesFilter.Controls.Add(this.comboYear);
            this.flwLayoutSalesFilter.Controls.Add(this.chkMonth);
            this.flwLayoutSalesFilter.Controls.Add(this.comboMonth);
            this.flwLayoutSalesFilter.Controls.Add(this.chkDay);
            this.flwLayoutSalesFilter.Controls.Add(this.comboDay);
            resources.ApplyResources(this.flwLayoutSalesFilter, "flwLayoutSalesFilter");
            this.flwLayoutSalesFilter.ForeColor = System.Drawing.Color.Coral;
            this.flwLayoutSalesFilter.Name = "flwLayoutSalesFilter";
            // 
            // chkYear
            // 
            resources.ApplyResources(this.chkYear, "chkYear");
            this.chkYear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(243)))), ((int)(((byte)(248)))));
            this.chkYear.Name = "chkYear";
            this.chkYear.UseVisualStyleBackColor = false;
            
            // 
            // comboYear
            // 
            this.comboYear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(243)))), ((int)(((byte)(248)))));
            this.comboYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.comboYear, "comboYear");
            this.comboYear.FormattingEnabled = true;
            this.comboYear.Name = "comboYear";
            
            // 
            // chkMonth
            // 
            resources.ApplyResources(this.chkMonth, "chkMonth");
            this.chkMonth.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(243)))), ((int)(((byte)(248)))));
            this.chkMonth.Name = "chkMonth";
            this.chkMonth.UseVisualStyleBackColor = false;
            // 
            // comboMonth
            // 
            this.comboMonth.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(243)))), ((int)(((byte)(248)))));
            this.comboMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.comboMonth, "comboMonth");
            this.comboMonth.FormattingEnabled = true;
            this.comboMonth.Name = "comboMonth";
            // 
            // chkDay
            // 
            resources.ApplyResources(this.chkDay, "chkDay");
            this.chkDay.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(243)))), ((int)(((byte)(248)))));
            this.chkDay.Name = "chkDay";
            this.chkDay.UseVisualStyleBackColor = false;
            // 
            // comboDay
            // 
            this.comboDay.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(243)))), ((int)(((byte)(248)))));
            this.comboDay.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.comboDay, "comboDay");
            this.comboDay.FormattingEnabled = true;
            this.comboDay.Name = "comboDay";
            
            // 
            // chrtSales
            // 
            this.chrtSales.BackColor = System.Drawing.Color.Transparent;
            chartArea1.AxisX.Interval = 1D;
            chartArea1.AxisX.IsMarginVisible = false;
            chartArea1.AxisY.IntervalAutoMode = System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode.VariableCount;
            chartArea1.AxisY.IsLabelAutoFit = false;
            chartArea1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(243)))), ((int)(((byte)(248)))));
            chartArea1.Name = "chartSales";
            this.chrtSales.ChartAreas.Add(chartArea1);
            resources.ApplyResources(this.chrtSales, "chrtSales");
            this.chrtSales.IsSoftShadows = false;
            legend1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(243)))), ((int)(((byte)(248)))));
            legend1.Name = "Legend1";
            this.chrtSales.Legends.Add(legend1);
            this.chrtSales.Name = "chrtSales";
            series1.BorderColor = System.Drawing.Color.Maroon;
            series1.ChartArea = "chartSales";
            series1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            series1.CustomProperties = "LabelStyle=Center";
            series1.EmptyPointStyle.MarkerColor = System.Drawing.Color.Maroon;
            series1.EmptyPointStyle.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Cross;
            series1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            series1.IsXValueIndexed = true;
            series1.LabelBorderWidth = 0;
            series1.Legend = "Legend1";
            series1.MarkerColor = System.Drawing.Color.Red;
            series1.MarkerSize = 15;
            series1.MarkerStep = 5;
            series1.Name = "seriesSales";
            series1.SmartLabelStyle.AllowOutsidePlotArea = System.Windows.Forms.DataVisualization.Charting.LabelOutsidePlotAreaStyle.No;
            series1.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
            series1.YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
            this.chrtSales.Series.Add(series1);
            
            // 
            // toolbarManagement
            // 
            this.toolbarManagement.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.toolbarManagement.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            resources.ApplyResources(this.toolbarManagement, "toolbarManagement");
            this.toolbarManagement.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsBtnSave,
            this.tsBtnDiscard,
            this.tsBtnEdit,
            this.tsBtnAdd,
            this.tsBtnDelete,
            this.tsSep1,
            this.btnManageCategories,
            this.tsBtnChangeCategory});
            this.toolbarManagement.Name = "toolbarManagement";
            // 
            // tsBtnSave
            // 
            this.tsBtnSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBtnSave.Image = global::Restaurant_Management_System.Properties.Resources.save;
            resources.ApplyResources(this.tsBtnSave, "tsBtnSave");
            this.tsBtnSave.Name = "tsBtnSave";
            this.tsBtnSave.Click += new System.EventHandler(this.tsBtnSave_Click);
            // 
            // tsBtnDiscard
            // 
            this.tsBtnDiscard.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBtnDiscard.Image = global::Restaurant_Management_System.Properties.Resources.discard;
            resources.ApplyResources(this.tsBtnDiscard, "tsBtnDiscard");
            this.tsBtnDiscard.Name = "tsBtnDiscard";
            this.tsBtnDiscard.Click += new System.EventHandler(this.tsBtnDiscard_Click);
            // 
            // tsBtnEdit
            // 
            this.tsBtnEdit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBtnEdit.Image = global::Restaurant_Management_System.Properties.Resources.edit;
            resources.ApplyResources(this.tsBtnEdit, "tsBtnEdit");
            this.tsBtnEdit.Name = "tsBtnEdit";
            this.tsBtnEdit.Click += new System.EventHandler(this.tsBtnEdit_Click);
            // 
            // tsBtnAdd
            // 
            this.tsBtnAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBtnAdd.Image = global::Restaurant_Management_System.Properties.Resources.add;
            resources.ApplyResources(this.tsBtnAdd, "tsBtnAdd");
            this.tsBtnAdd.Name = "tsBtnAdd";
            this.tsBtnAdd.Click += new System.EventHandler(this.tsBtnAdd_Click);
            // 
            // tsBtnDelete
            // 
            this.tsBtnDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBtnDelete.Image = global::Restaurant_Management_System.Properties.Resources.delete;
            resources.ApplyResources(this.tsBtnDelete, "tsBtnDelete");
            this.tsBtnDelete.Name = "tsBtnDelete";
            this.tsBtnDelete.Click += new System.EventHandler(this.tsBtnDelete_Click);
            // 
            // tsSep1
            // 
            this.tsSep1.Name = "tsSep1";
            resources.ApplyResources(this.tsSep1, "tsSep1");
            // 
            // btnManageCategories
            // 
            this.btnManageCategories.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnManageCategories.Image = global::Restaurant_Management_System.Properties.Resources.category;
            resources.ApplyResources(this.btnManageCategories, "btnManageCategories");
            this.btnManageCategories.Name = "btnManageCategories";
            this.btnManageCategories.Click += new System.EventHandler(this.btnAddCategory_Click);
            // 
            // tsBtnChangeCategory
            // 
            this.tsBtnChangeCategory.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBtnChangeCategory.Image = global::Restaurant_Management_System.Properties.Resources.change_category;
            resources.ApplyResources(this.tsBtnChangeCategory, "tsBtnChangeCategory");
            this.tsBtnChangeCategory.Name = "tsBtnChangeCategory";
            this.tsBtnChangeCategory.Click += new System.EventHandler(this.tsBtnChangeCategory_Click);
            // 
            // menuStripManagement
            // 
            this.menuStripManagement.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.menuStripManagement.BackgroundImage = global::Restaurant_Management_System.Properties.Resources.background3;
            resources.ApplyResources(this.menuStripManagement, "menuStripManagement");
            this.menuStripManagement.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile,
            this.mnuLanguage,
            this.mnuAbout});
            this.menuStripManagement.Name = "menuStripManagement";
            // 
            // mnuFile
            // 
            this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.subMnuSwitchToMain,
            this.subMnuExit});
            this.mnuFile.Name = "mnuFile";
            resources.ApplyResources(this.mnuFile, "mnuFile");
            // 
            // subMnuSwitchToMain
            // 
            this.subMnuSwitchToMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.subMnuSwitchToMain.Name = "subMnuSwitchToMain";
            resources.ApplyResources(this.subMnuSwitchToMain, "subMnuSwitchToMain");
            this.subMnuSwitchToMain.Click += new System.EventHandler(this.subMnuSwitchToMain_Click);
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
            // frmManagement
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(30)))), ((int)(((byte)(6)))));
            this.Controls.Add(this.tabManagement);
            this.Controls.Add(this.toolbarManagement);
            this.Controls.Add(this.menuStripManagement);
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStripManagement;
            this.Name = "frmManagement";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmManagement_FormClosed);
            this.Load += new System.EventHandler(this.frmManagement_Load);
            
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmManagement_KeyDown);
            
            this.tabFood.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgFood)).EndInit();
            this.flwLayoutPnlFood.ResumeLayout(false);
            this.flwLayoutPnlFood.PerformLayout();
            this.tabEmployee.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgEmployee)).EndInit();
            this.flwLayoutEmployee.ResumeLayout(false);
            this.flwLayoutEmployee.PerformLayout();
            this.tabManagement.ResumeLayout(false);
            this.tabStatistics.ResumeLayout(false);
            this.tabCtrlStatistics.ResumeLayout(false);
            this.tabSales.ResumeLayout(false);
            this.flwLayoutSalesFilter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chrtSales)).EndInit();
            this.toolbarManagement.ResumeLayout(false);
            this.toolbarManagement.PerformLayout();
            this.menuStripManagement.ResumeLayout(false);
            this.menuStripManagement.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }





        #endregion

        private System.Windows.Forms.ToolStripMenuItem mnuFile;
        private System.Windows.Forms.ToolStripMenuItem mnuLanguage;
        private System.Windows.Forms.MenuStrip menuStripManagement;
        private System.Windows.Forms.TabPage tabEmployee;
        private System.Windows.Forms.DataGridView dgEmployee;
        private System.Windows.Forms.TabControl tabManagement;
        private System.Windows.Forms.FlowLayoutPanel flwLayoutPnlFood;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.ComboBox comboCategory;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.ToolStrip toolbarManagement;
        private System.Windows.Forms.ToolStripButton tsBtnSave;
        private System.Windows.Forms.ToolStripButton tsBtnAdd;
        private System.Windows.Forms.ToolStripButton tsBtnEdit;
        private System.Windows.Forms.ToolStripButton tsBtnDelete;
        private System.Windows.Forms.ToolStripSeparator tsSep1;
        private System.Windows.Forms.DataGridView dgFood;
        private System.Windows.Forms.TabPage tabFood;
        private System.Windows.Forms.ToolStripButton btnManageCategories;
        private System.Windows.Forms.ToolStripMenuItem subMnuExit;
        private System.Windows.Forms.TabPage tabStatistics;
        private System.Windows.Forms.DataVisualization.Charting.Chart chrtSales;
        private System.Windows.Forms.ToolStripButton tsBtnChangeCategory;
        private System.Windows.Forms.FlowLayoutPanel flwLayoutEmployee;
        private System.Windows.Forms.Label lblSearchEmployee;
        private System.Windows.Forms.TextBox txtSearchEmployee;
        private System.Windows.Forms.ToolStripButton tsBtnDiscard;
        private System.Windows.Forms.ToolStripMenuItem subMnuSwitchToMain;
        private System.Windows.Forms.TabControl tabCtrlStatistics;
        private System.Windows.Forms.TabPage tabSales;
        private System.Windows.Forms.ComboBox comboYear;
        private System.Windows.Forms.ComboBox comboDay;
        private System.Windows.Forms.ComboBox comboMonth;
        private System.Windows.Forms.CheckBox chkYear;
        private System.Windows.Forms.CheckBox chkMonth;
        private System.Windows.Forms.CheckBox chkDay;
        private System.Windows.Forms.FlowLayoutPanel flwLayoutSalesFilter;
        private System.Windows.Forms.ToolStripMenuItem mnuAbout;

    }
}