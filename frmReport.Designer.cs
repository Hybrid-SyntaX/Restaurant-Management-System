namespace Restaurant_Management_System
{
    partial class frmReport
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
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.rptViewInvoice = new Microsoft.Reporting.WinForms.ReportViewer();
            this.OrderBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.CustomerBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.OrderBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CustomerBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // rptViewInvoice
            // 
            this.rptViewInvoice.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rptViewInvoice.KeepSessionAlive = false;
            reportDataSource1.Name = "Foods";
            reportDataSource1.Value = this.OrderBindingSource;
            this.rptViewInvoice.LocalReport.DataSources.Add(reportDataSource1);
            this.rptViewInvoice.LocalReport.EnableExternalImages = true;
            this.rptViewInvoice.LocalReport.EnableHyperlinks = true;
            this.rptViewInvoice.LocalReport.ReportEmbeddedResource = "Restaurant_Management_System.Invoice.rdlc";
            this.rptViewInvoice.LocalReport.ReportPath = "Invoice.rdlc";
            this.rptViewInvoice.Location = new System.Drawing.Point(0, 0);
            this.rptViewInvoice.Name = "rptViewInvoice";
            this.rptViewInvoice.ShowBackButton = false;
            this.rptViewInvoice.ShowFindControls = false;
            this.rptViewInvoice.ShowPageNavigationControls = false;
            this.rptViewInvoice.ShowRefreshButton = false;
            this.rptViewInvoice.ShowStopButton = false;
            this.rptViewInvoice.ShowZoomControl = false;
            this.rptViewInvoice.Size = new System.Drawing.Size(398, 535);
            this.rptViewInvoice.TabIndex = 0;
            // 
            // OrderBindingSource
            // 
            this.OrderBindingSource.DataMember = "Foods";
            this.OrderBindingSource.DataSource = typeof(Logic.Order);
            // 
            // CustomerBindingSource
            // 
            this.CustomerBindingSource.DataSource = typeof(Logic.Customer);
            // 
            // frmReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(398, 535);
            this.Controls.Add(this.rptViewInvoice);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmReport";
            this.Text = "frmReport";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmReport_FormClosing);
            this.Load += new System.EventHandler(this.frmReport_Load);
            ((System.ComponentModel.ISupportInitialize)(this.OrderBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CustomerBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.BindingSource OrderBindingSource;
        private Microsoft.Reporting.WinForms.ReportViewer rptViewInvoice;
        private System.Windows.Forms.BindingSource CustomerBindingSource;
    }
}