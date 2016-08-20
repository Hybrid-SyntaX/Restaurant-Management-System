using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Logic;
using Microsoft.Reporting.WinForms;
namespace Restaurant_Management_System
{
    public partial class frmReport : Form
    {
        public static Order SelectedOrder;
        public frmReport()
        {
            InitializeComponent();
        }
        private void InitializeLanguage()
        {
            //Laebls
            rptViewInvoice.LocalReport.SetParameters(new ReportParameter("lblFoodName", Common.Words["FoodName"]));
            rptViewInvoice.LocalReport.SetParameters(new ReportParameter("lblFoodPrice", Common.Words["Price"]));
            rptViewInvoice.LocalReport.SetParameters(new ReportParameter("lblFoodQuantity", Common.Words["Quantity"]));
            rptViewInvoice.LocalReport.SetParameters(new ReportParameter("lblCustomerFullName", Common.Words["Customer"]));
            rptViewInvoice.LocalReport.SetParameters(new ReportParameter("lblCustomerId", Common.Words["CustomerId"]));
            rptViewInvoice.LocalReport.SetParameters(new ReportParameter("lblCustomerAddress", Common.Words["Address"]));
            rptViewInvoice.LocalReport.SetParameters(new ReportParameter("lblCustomerPhoneNumber", Common.Words["PhoneNumber"]));
            rptViewInvoice.LocalReport.SetParameters(new ReportParameter("lblTotalPrice", Common.Words["TotalPrice"]));
            rptViewInvoice.LocalReport.SetParameters(new ReportParameter("lblOrderDate", Common.Words["OrderDate"]));
            rptViewInvoice.LocalReport.SetParameters(new ReportParameter("lblDiscount", Common.Words["Discount"]));
            rptViewInvoice.LocalReport.SetParameters(new ReportParameter("lblDeliveryFee", Common.Words["DeliveryFee"]));
            rptViewInvoice.LocalReport.SetParameters(new ReportParameter("lblOrderType", Common.Words["OrderType"]));
            rptViewInvoice.LocalReport.SetParameters(new ReportParameter("lblRawPrice", Common.Words["RawPrice"]));



            this.Text = Common.Words["ReportTitle"];
        }
        private void InitializeReportViewer()
        {
            rptViewInvoice.ProcessingMode = ProcessingMode.Local;


            rptViewInvoice.LocalReport.ReportPath = @"Invoice.rdlc";

            rptViewInvoice.LocalReport.DataSources["Foods"].Value = SelectedOrder.Foods;
            rptViewInvoice.LocalReport.SetParameters(new ReportParameter("CustomerFullName", SelectedOrder.Customer.FullName));
            rptViewInvoice.LocalReport.SetParameters(new ReportParameter("CustomerId", SelectedOrder.Customer.CustomerId.ToString()));
            rptViewInvoice.LocalReport.SetParameters(new ReportParameter("CustomerAddress", SelectedOrder.Customer.Address));
            rptViewInvoice.LocalReport.SetParameters(new ReportParameter("CustomerPhoneNumber", SelectedOrder.Customer.PhoneNumber.ToString()));
            rptViewInvoice.LocalReport.SetParameters(new ReportParameter("OrderDate", SelectedOrder.OrderDate.ToString()));
            rptViewInvoice.LocalReport.SetParameters(new ReportParameter("TotalPrice", SelectedOrder.TotalPrice.ToString("N", Common.CurrentLanguage.NumberFormat)));
            rptViewInvoice.LocalReport.SetParameters(new ReportParameter("RawPrice", SelectedOrder.RawPrice.ToString("N", Common.CurrentLanguage.NumberFormat)));
            rptViewInvoice.LocalReport.SetParameters(new ReportParameter("Discount", SelectedOrder.DiscountedPrice.ToString("N", Common.CurrentLanguage.NumberFormat) + string.Format(" ({0}{1}) ", SelectedOrder.Discount, Common.CurrentLanguage.NumberFormat.PercentSymbol)));

            rptViewInvoice.LocalReport.SetParameters(new ReportParameter("DeliveryFee", SelectedOrder.DeliveryFee.ToString("N", Common.CurrentLanguage.NumberFormat)));
            rptViewInvoice.LocalReport.SetParameters(new ReportParameter("OrderType", Common.Words[SelectedOrder.OrderType.ToString()]));


            rptViewInvoice.RefreshReport();

        }
        private void frmReport_Load(object sender, EventArgs e)
        {
            InitializeLanguage();

            InitializeReportViewer();
        }

        private void rptViewInvoice_Load(object sender, EventArgs e)
        {

        }

        private void frmReport_FormClosing(object sender, FormClosingEventArgs e)
        {
            rptViewInvoice.LocalReport.ReleaseSandboxAppDomain();
        }
    }
}