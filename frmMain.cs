using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Logic;
using System.Threading;
using Chocolatey.Validation;
using Microsoft.Reporting.WinForms;
using System.Globalization;
using System.Collections;
namespace Restaurant_Management_System
{
    enum OrderMode
    {
        View, Submit, Edit
    }
    public partial class frmMain : Form
    {
        Customer CustomerController;
        Order OrderController;
        Order _newOrder;
        Order NewOrder
        {

            set
            {
                _newOrder = value;
            }
            get
            {
                if (_newOrder == null)
                    _newOrder = new Order();
                return _newOrder;
            }
        }
        Food FoodController;
        Category CategoryController;

        BindingList<Customer> customers;
        BindingList<Order> orders;
        BindingList<Food> foods;
        BindingList<Category> categories;

        OrderMode CurrentOrderMode;
        object[] OrderTypes;

        public Order CurrentOrder
        {
            get
            {
                if (dgOrder.SelectedRows.Count > 0)
                    return dgOrder.SelectedRows[0].DataBoundItem as Order;
                else return null;
            }
        }
        public Customer CurrentCustomer
        {
            get
            {
                if (dgCustomer.SelectedRows.Count > 0)
                    return dgCustomer.SelectedRows[0].DataBoundItem as Customer;
                else return null;
            }
        }

        public frmMain()
        {

            InitializeComponent();
            Common.Initialize();

            

            

            CurrentOrderMode = OrderMode.View;

            //Controllers
            CustomerController = new Customer();
            OrderController = new Order();
            FoodController = new Food();
            CategoryController = new Category();

            //New Order
            NewOrder = new Order();

            CustomerController.Error += new ModelErrorEventHandler(delegate(object sender, ModelErrorEventArgs e)
                {
                    Common.ShowErrorMessage(e.Exception);
                });
            //Initialize Lists
            Common.InitializeList(ref customers, ref CustomerController);
            Common.InitializeList(ref foods, ref FoodController);
            Common.InitializeList(ref orders, ref OrderController);
            Common.InitializeList(ref categories, ref CategoryController);


            categories.Insert(0, new Category() { CategoryName = Common.Words["All"], Id = Guid.Empty });



            splitContainerMain.Panel2Collapsed = true;

            InitializeOrderStatusComboBox(comboStatuses);
            InitializeOrderStatusComboBox(comboOrderStatus);
            InitializeOrderTypeComboBox(comboOrderTypes);


            InitializeCustomersDataGrid();
            InitializeOrdersDataGrid();
            InitializeOrderedFoodsDataGrid();
            InitializeFoodsDataGrid();
            InitializeSelectedFoodsDataGrid();

            Common.PopulateLanguageMenu(mnuLanguage, delegate()
            {
                InitializeLanguage();
            });

        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            if (Common.CurrentUser == null)
                new frmLogin().ShowDialog();
            else
            {
                if (Common.CurrentUser.Role != Role.Manager)
                {
                    subMnuSwitchToAdministrationPanel.Visible = false;
                }
            }
        }
        private void InitializeCustomersDataGrid()
        {
            dgCustomer.AutoGenerateColumns = false;
            dgCustomer.DataSource = CustomerController.LocalDataSource;

            Common.AddDataGridViewColumn(dgCustomer, "CustomerId");
            Common.AddDataGridViewColumn(dgCustomer, "FirstName");
            Common.AddDataGridViewColumn(dgCustomer, "LastName");
            Common.AddDataGridViewColumn(dgCustomer, "PhoneNumber");
            Common.AddDataGridViewColumn(dgCustomer, "Address");

         
            
            dgCustomer.Columns["CustomerId"].ReadOnly = true;

            dgCustomer.Columns["Address"].Width = 220;
            dgCustomer.Columns["PhoneNumber"].Width = 110;
        }
        private void InitializeOrdersDataGrid()
        {
            dgOrder.AutoGenerateColumns = false;
            dgOrder.ReadOnly = false;

            DataGridViewComboBoxColumn columnType = new DataGridViewComboBoxColumn() { Name = "OrderType", };
            DataGridViewComboBoxColumn columnStatus = new DataGridViewComboBoxColumn() { Name = "Status" };


            columnStatus.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
            columnType.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;

            Common.AddDataGridViewColumn(dgOrder, "OrderDate");


            dgOrder.Columns.Add(columnType);
            dgOrder.Columns.Add(columnStatus);


            InitializeOrderTypeDataGridComboBox();
            InitializeOrderStatusComboBox();
        }
        private void InitializeOrderStatusComboBox(ComboBox comboBox)
        {
            List<object> OrderStatuses = new List<object>()  { new { Value = OrderStatus.Unspecified, Text = Common.Words["Unspecified"] }, 
                                        new { Value = OrderStatus.Sent, Text = Common.Words["Sent"] },
                                        new { Value = OrderStatus.Delivered, Text = Common.Words["Delivered"] },
                                        new { Value = OrderStatus.Pending, Text = Common.Words["Pending"] }};
            comboBox.DisplayMember = "Text";
            comboBox.ValueMember = "Value";
            comboBox.DataSource = OrderStatuses;


        }
        private void InitializeOrderStatusComboBox()
        {
            List<object> OrderStatuses = new List<object>()  { new { Value = OrderStatus.Unspecified, Text = Common.Words["Unspecified"] }, 
                                        new { Value = OrderStatus.Sent, Text = Common.Words["Sent"] },
                                        new { Value = OrderStatus.Delivered, Text = Common.Words["Delivered"] },
                                        new { Value = OrderStatus.Pending, Text = Common.Words["Pending"] }
                                      };
            (dgOrder.Columns["Status"] as DataGridViewComboBoxColumn).DataSource = OrderStatuses;
            (dgOrder.Columns["Status"] as DataGridViewComboBoxColumn).DataPropertyName = "Status";
            (dgOrder.Columns["Status"] as DataGridViewComboBoxColumn).DisplayMember = "Text";
            (dgOrder.Columns["Status"] as DataGridViewComboBoxColumn).ValueMember = "Value";
        }
        private void InitializeOrderedFoodsDataGrid()
        {
            dgOrderedFoods.AutoGenerateColumns = false;
            dgOrderedFoods.AllowUserToDeleteRows = true;
            dgOrderedFoods.ReadOnly = false;

            Common.AddDataGridViewColumn(dgOrderedFoods, "FoodName");
            Common.AddDataGridViewColumn(dgOrderedFoods, "Price");
            Common.AddDataGridViewColumn(dgOrderedFoods, "Quantity");



            dgOrderedFoods.Columns["FoodName"].ReadOnly = true;
            dgOrderedFoods.Columns["Price"].ReadOnly = true;


        }
        private void RebindCustomersDataGrid()
        {
            //CustomerController.ReadAll();
            //InitiailzeErrorHandlers(ref CustomerController.LocalDataSource.OfType<Customer>().ToList());
            //Common.InitiailzeErrorHandlers(ref CustomerController.LocalDataSource);

            //dgCustomer.DataSource = CustomerController.LocalDataSource;
             CustomerController.ReadAll();
             dgCustomer.DataSource = CustomerController.LocalDataSource ;

        }
        private void RebindOrdersDataGrid()
        {
            FilterOrders(true);
        }

        public void InitializeLanguage()
        {
            this.Text = Common.Words["Order"];

            subMnuExit.Text = Common.Words["Exit"];
            subMnuSwitchToAdministrationPanel.Text = Common.Words["SwitchToAdministrationPanel"];
            mnuFile.Text = Common.Words["File"];
            mnuLanguage.Text = Common.Words["Language"];
            mnuAbout.Text = Common.Words["About"];


            //Datagrid Customers Columns
            dgCustomer.Columns["FirstName"].HeaderText = Common.Words["FirstName"];
            dgCustomer.Columns["LastName"].HeaderText = Common.Words["LastName"];
            dgCustomer.Columns["PhoneNumber"].HeaderText = Common.Words["PhoneNumber"];
            dgCustomer.Columns["Address"].HeaderText = Common.Words["Address"];
            dgCustomer.Columns["CustomerId"].HeaderText = Common.Words["CustomerId"];

            //Datagrid Orders Columns
            dgOrder.Columns["OrderType"].HeaderText = Common.Words["OrderType"];
            dgOrder.Columns["OrderDate"].HeaderText = Common.Words["OrderDate"];

            //DataGrid Ordered Foods Columns
            dgOrderedFoods.Columns["FoodName"].HeaderText = Common.Words["FoodName"];
            dgOrderedFoods.Columns["Price"].HeaderText = Common.Words["Price"];
            dgOrderedFoods.Columns["Quantity"].HeaderText = Common.Words["Quantity"];
            //DataGrid Foods
            dgFood.Columns["FoodName"].HeaderText = Common.Words["FoodName"];
            dgFood.Columns["Price"].HeaderText = Common.Words["Price"];
            //DataGrid Orders
            dgOrder.Columns["OrderDate"].HeaderText = Common.Words["OrderDate"];
            dgOrder.Columns["OrderType"].HeaderText = Common.Words["OrderType"];
            dgOrder.Columns["Status"].HeaderText = Common.Words["Status"];
            //DataGrid Selected Foods
            dgSelectedFoods.Columns["FoodName"].HeaderText = Common.Words["FoodName"];
            dgSelectedFoods.Columns["Quantity"].HeaderText = Common.Words["Quantity"];

            //Tabs
            tabCustomer.Text = Common.Words["Customer"];
            //tabOrder.Text = Common.Words["Order"];

            //lables
            lblSearch.Text = Common.Words["Search"];
            lblStatus.Text = Common.Words["Status"];
            //Tooltips
            tsBtnSave.Text = Common.Words["ttSave"];
            tsBtnOFSave.Text = Common.Words["ttSave"];
            tsOFIncrease.Text = Common.Words["ttIncrease"];
            tsOFDecrease.Text = Common.Words["ttDecrease"];
            tsBtnDiscard.Text = Common.Words["ttDiscardChanges"];
            tsBtnOrder.Text = Common.Words["ttOrder"];
            tsBtnReport.Text = Common.Words["ttReport"];
            tsOFBtnDelete.Text = string.Format(Common.Words["ttDelete"], Common.Words["Food"]);
            tsOFBtnAdd.Text = string.Format(Common.Words["ttAdd"], Common.Words["Food"]);
            tsBtnAdd.ToolTipText = string.Format(Common.Words["ttAdd"], Common.Words["Customer"]);
            tsBtnDelete.ToolTipText = string.Format(Common.Words["ttDelete"], Common.Words["Customer"]);
            tsBtnEdit.ToolTipText = string.Format(Common.Words["ttEdit"], Common.Words["Customer"]);



            //Labels
            lblCategory.Text = Common.Words["Category"];
            lblDiscount.Text = Common.Words["Discount"];
            lblOrderType.Text = Common.Words["OrderType"];
            lblDeliveryFee.Text = Common.Words["DeliveryFee"];

            //Buttons
            if (CurrentOrderMode == OrderMode.View || CurrentOrderMode == OrderMode.Edit)
                btnSubmitOrder.Text = Common.Words["Edit"];
            else if (CurrentOrderMode == OrderMode.Submit)
                btnSubmitOrder.Text = Common.Words["Submit"];

            conMnuBtnAdd.Text = Common.Words["Add"];
            btnCancelOrder.Text = Common.Words["Cancel"];




            (comboCategory.Items[0] as Category).CategoryName = Common.Words["All"];

            //Combo boxes
            InitializeOrderTypeComboBox();
            InitializeOrderStatusComboBox();
            InitializeOrderTypeDataGridComboBox();
            InitializeOrderStatusComboBox(comboOrderStatus);
            InitializeOrderStatusComboBox(comboStatuses);
            InitializeOrderTypeComboBox(comboOrderTypes);

            //Filters
            chkNoFilter.Text = Common.Words["NoFilter"];
            chkOrderTypes.Text = Common.Words["OrderType"];
            chkOrderStatuses.Text = Common.Words["Status"];

            //Right to Left
            // Common.ToggleRightToLeft(this);

            Common.ToggleRightToLeft(toolbarMain);
            Common.ToggleRightToLeft(tabCtrlMain);
            Common.ToggleRightToLeft(mnuStripMain);
            Common.ToggleRightToLeft(splitContainerOrder);
            Common.ToggleRightToLeft(flwLayoutCategory);
            Common.ToggleRightToLeft(flwLayoutOrder);
            Common.ToggleRightToLeft(flwLayoutOrderFilter);

            Common.ToggleRightToLeft(flwLayoutOrder, delegate()
            {
                flwLayoutOrder.Dock = DockStyle.Right;
            }, delegate()
            {
                flwLayoutOrder.Dock = DockStyle.Left;
            });

        }



        private void subMnuExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void txtSearchCustomer_TextChanged(object sender, EventArgs e)
        {
            dgCustomer.DataSource = CustomerController.Search((sender as TextBox).Text);
        }


        private void tsBtnDiscard_Click(object sender, EventArgs e)
        {

            DiscardChanges();
        }

        private void DiscardChanges()
        {
            Common.CommonDiscardChanges(customers);
            RebindCustomersDataGrid();

            tsBtnEdit.Image = Properties.Resources.edit;
            tsBtnSave.Image = Properties.Resources.save;
        }

        private void tsBtnAdd_Click(object sender, EventArgs e)
        {
            if (IsActiveTab(tabCustomer))
                dgCustomer.CurrentCell = dgCustomer.Rows[dgCustomer.Rows.Count - 1].Cells["CustomerId"];

        }

        private void tsBtnDelete_Click(object sender, EventArgs e)
        {
            if (IsActiveTab(tabCustomer))
            {
                Common.ShowDeletePrompt(dgCustomer);
                RebindCustomersDataGrid();
            }

        }

        private bool IsActiveTab(TabPage tabPage)
        {
            return Common.IsActiveTab(tabCtrlMain, tabPage);
        }
        private void tsBtnSave_Click(object sender, EventArgs e)
        {

            SaveNewRows();
        }

        private void SaveNewRows()
        {
            foreach (Customer customer in (dgCustomer.DataSource as BindingList<Customer>).Where((customer) => customer.State == ModelState.Constructed || customer.State == ModelState.Constructing))
            {
                customer.Save();
            }
            RebindCustomersDataGrid();

            tsBtnSave.Image = Properties.Resources.save;
        }
        //Editing
        private void dgCustomer_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            Common.CommonDataGridCellBeginEdit(sender as DataGridView, e);
        }
        private void dgCustomer_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            Common.ModelIsChanged += new EventHandler(Common_ModelIsChanged);
            Common.ModelIsNotChanged += new EventHandler(Common_ModelIsNotChanged);
            Common.CommonDataGridCellEndEdit(sender as DataGridView, e, delegate(Customer customer)
            {

                //Edit customer info
                if (dgCustomer.CurrentRow.Cells["FirstName"].Value != null)
                    customer.FirstName = dgCustomer.CurrentRow.Cells["FirstName"].Value.ToString();
                if (dgCustomer.CurrentRow.Cells["LastName"].Value != null)
                    customer.LastName = dgCustomer.CurrentRow.Cells["LastName"].Value.ToString();
                if (dgCustomer.CurrentRow.Cells["PhoneNumber"].Value != null)
                    customer.PhoneNumber = dgCustomer.CurrentRow.Cells["PhoneNumber"].Value.ToString();

              
                if (dgCustomer.CurrentRow.Cells["CustomerId"].Value!=null && Validator.IsInteger(dgCustomer.CurrentRow.Cells["CustomerId"].Value.ToString()))
                    customer.CustomerId = (int)dgCustomer.CurrentRow.Cells["CustomerId"].Value;
                else Common.ShowErrorMessage(Common.Words["Invalid Input(Number)"]);

                if (dgCustomer.CurrentRow.Cells["Address"].Value != null)
                    customer.Address = dgCustomer.CurrentRow.Cells["Address"].Value.ToString();
            }
            );
        }
        private void dgCustomer_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            
            //&& e.Row.DataBoundItem!=null
            //e.Row.Tag == null
            if (dgCustomer.CurrentRow.Tag == null)
            {
                Customer newCustomer = customers.AddNew();
                newCustomer.State = ModelState.Constructing;
                
                dgCustomer.CurrentRow.Tag = newCustomer;
                
                tsBtnSave.Image = Properties.Resources.changes_pending;
            }


        }




        void Common_ModelIsChanged(object sender, EventArgs e)
        {
            tsBtnEdit.Image = Properties.Resources.edit_pending;
        }
        void Common_ModelIsNotChanged(object sender, EventArgs e)
        {
            tsBtnEdit.Image = Properties.Resources.edit;
        }

        private void tabCtrlMain_Enter(object sender, EventArgs e)
        {
            InitializeLanguage();
        }

        private void tsBtnEdit_Click(object sender, EventArgs e)
        {

            SaveModifiedRows();
        }

        private void SaveModifiedRows()
        {
            foreach (Customer customer in customers.Where(customer => customer.State == ModelState.Editing))
            {
                customer.Edit();
            }
            //   Common.CommonDataGridSubmitEdit(dgCustomer.DataSource as BindingList<Customer>);
            RebindCustomersDataGrid();


            tsBtnEdit.Image = Properties.Resources.edit;
        }

        private void dgCustomer_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            e.Cancel = true;
            Common.ShowDeletePrompt(sender as DataGridView);
            RebindCustomersDataGrid();
        }



        private void DisableMainToolbarControls()
        {
            tsBtnDelete.Enabled = tsBtnReport.Enabled = tsBtnEdit.Enabled = tsBtnOrder.Enabled = false;
            splitContainerOrder.Enabled = false;

            
        }
        private void EnableMainToolbarControls()
        {
            tsBtnDelete.Enabled = tsBtnReport.Enabled = tsBtnEdit.Enabled = tsBtnOrder.Enabled = true;

            splitContainerOrder.Enabled = true;

        }



        private void tsBtnOrder_Click(object sender, EventArgs e)
        {

            dgSelectedFoods.DataSource = null;
            SubmitOrderMode();

        }

        private void tsBtnReport_Click(object sender, EventArgs e)
        {
            ShowReport();
        }
        private void ShowReport()
        {
            frmReport.SelectedOrder = CurrentOrder;

            new frmReport().ShowDialog();
        }

        private void comboCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgFood.DataSource = FoodController.ReadByCategory(comboCategory.SelectedValue as Category).Where(food=>food.Enabled==true).ToList() ;
            txtSearchFood.Text = string.Empty;
        }

        void InitializeFoodsDataGrid()
        {

            dgFood.ContextMenuStrip = conMnuOrder;
            dgFood.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgFood.AutoGenerateColumns = false;
            dgFood.DataSource = foods.Where(food => food.Enabled ==true).ToList() ;


            Common.AddDataGridViewColumn(dgFood, "FoodName");
            Common.AddDataGridViewColumn(dgFood, "Price");

            //dgFood.Columns["FoodName"].HeaderText = Common.Words["FoodName"];
            //dgFood.Columns["Price"].HeaderText = Common.Words["Price"];

            comboCategory.DataSource = categories;
            comboCategory.DisplayMember = "CategoryName";

            InitializeOrderTypeComboBox();
            //comboOrderType.SelectedItem = OrderTypes[0];
            //comboOrderType.SelectedValue = OrderType.Restaurant;



            comboOrderType.SelectedIndexChanged += new EventHandler(delegate(object esender, EventArgs ea)
            {
                if (((OrderType)comboOrderType.SelectedValue) == OrderType.Restaurant)
                {
                    txtDeliveryFee.Text = "0";
                    txtDeliveryFee.Enabled = false;
                }
                else
                {
                    txtDeliveryFee.Enabled = true;
                }
            });
        }
        private void InitializeOrderTypeDataGridComboBox()
        {
            OrderTypes = new[] { 
                    new { Value = OrderType.Restaurant, Text = Common.Words["Restaurant"] }, 
                                    new { Value = OrderType.TakeOut, Text = Common.Words["TakeOut"] } };


            (dgOrder.Columns["OrderType"] as DataGridViewComboBoxColumn).DataSource = OrderTypes;
            (dgOrder.Columns["OrderType"] as DataGridViewComboBoxColumn).DisplayMember = "Text";
            (dgOrder.Columns["OrderType"] as DataGridViewComboBoxColumn).ValueMember = "Value";
            (dgOrder.Columns["OrderType"] as DataGridViewComboBoxColumn).DataPropertyName = "OrderType";

        }
        private void InitializeOrderTypeComboBox(ComboBox comboBox = null)
        {
            if (comboBox == null)
                comboBox = comboOrderType;

            OrderTypes = new[] { new { Value = OrderType.Restaurant, Text = Common.Words["Restaurant"] }, 
                                    new { Value = OrderType.TakeOut, Text = Common.Words["TakeOut"] } };


            comboBox.SelectedValue = OrderType.Restaurant;

            comboBox.DisplayMember = "Text";
            comboBox.ValueMember = "Value";
            comboBox.DataSource = OrderTypes;


        }

        private void dgFood_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            AddFood();
        }
 
        
        private void AddFood()
        {

            if (CurrentOrderMode == OrderMode.Submit) //New
            {


                foreach (DataGridViewRow row in dgFood.SelectedRows)
                {

                    if (!NewOrder.Foods.Contains(row.DataBoundItem as Food))
                        NewOrder.Foods.Add(row.DataBoundItem as Food);
                    else
                        NewOrder.Foods[NewOrder.Foods.IndexOf(row.DataBoundItem as Food)].Quantity++;
                }


                SubmitOrderMode();
                dgSelectedFoods.DataSource = NewOrder.Foods;
            }
            else if (CurrentOrderMode == OrderMode.View || CurrentOrderMode == OrderMode.Edit) //Edit
            {
                if (CurrentOrderMode == OrderMode.View)
                {
                    EditOrderMode();
                    return;
                }

                if (CurrentOrderMode == OrderMode.Edit)
                {
                    foreach (DataGridViewRow row in dgFood.SelectedRows)
                    {
                        Food food = row.DataBoundItem as Food;

                        if (!CurrentOrder.Foods.Contains<Food>(food, new BaseModelComparer()))
                        {
                            food.State = ModelState.Constructed;
                            CurrentOrder.Foods.Add(food);
                        }
                        else if (CurrentOrder.Foods.Contains<Food>(food, new BaseModelComparer()))
                        {
                            food.State = ModelState.Constructed;
                            CurrentOrder.Foods.Single((Food f) => f.Id.Equals(food.Id)).Quantity++;
                        }
                    }
                    tsBtnOFSave.Image = Properties.Resources.changes_pending;



                }
            }

        }



        private void InitializeSelectedFoodsDataGrid()
        {
            dgSelectedFoods.AllowUserToDeleteRows = true;
            dgSelectedFoods.ReadOnly = false;
            dgSelectedFoods.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            dgSelectedFoods.UserDeletingRow += new DataGridViewRowCancelEventHandler(delegate(object sender, DataGridViewRowCancelEventArgs e)
            {

                OrderController.Foods.Remove(e.Row.DataBoundItem as Food);
            }
            );

            dgSelectedFoods.AutoGenerateColumns = false;
            if (!dgSelectedFoods.Columns.Contains("FoodName"))
            {
                Common.AddDataGridViewColumn(dgSelectedFoods, "FoodName");
                Common.AddDataGridViewColumn(dgSelectedFoods, "Quantity");

                dgSelectedFoods.Columns["FoodName"].ReadOnly = true;
            }
        }

        private void tsOFBtnAdd_Click(object sender, EventArgs e)
        {
            AddFood();
        }

        private void conMnuBtnAdd_Click(object sender, EventArgs e)
        {
            AddFood();
        }

        private void btnSubmitOrder_Click(object sender, EventArgs e)
        {
            if (CurrentOrderMode == OrderMode.Submit)
            {
                if (NewOrder.Foods.Count > 0)
                {
                    NewOrder.Customer = CurrentCustomer;
                    NewOrder.DeliveryFee = float.Parse(txtDeliveryFee.Text);
                    NewOrder.OrderType = (OrderType)comboOrderType.SelectedValue;
                    NewOrder.OrderDate = DateTime.Now;
                    NewOrder.Discount = (int)numUpDownDiscount.Value;
                    NewOrder.Status = OrderStatus.Pending;

                    NewOrder.Save();



                    dgCustomer.Enabled = true;
                    frmReport.SelectedOrder = NewOrder;

                    
                    
                    NewOrder = new Order();
                    
                    new frmReport().ShowDialog();

                    dgSelectedFoods.DataSource = null;
                    NewOrder.Foods.Clear();
                    
                }
                else MessageBox.Show(Common.Words["NoFoodIsSelected"], Common.Words["Error"], MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (CurrentOrderMode == OrderMode.View || CurrentOrderMode == OrderMode.Edit)
            {
                float deliveryFee;

                if (!float.TryParse(txtDeliveryFee.Text, out deliveryFee))
                {
                    MessageBox.Show(Common.Words["Invalid Input(Number)"]);
                    txtDeliveryFee.Text = txtDeliveryFee.Tag.ToString();
                    return;
                }

                CurrentOrder.DeliveryFee = deliveryFee;

                CurrentOrder.OrderType = (OrderType)comboOrderType.SelectedValue;
                CurrentOrder.Status = (OrderStatus)comboOrderStatus.SelectedValue;
                CurrentOrder.Discount = (int)numUpDownDiscount.Value;
                CurrentOrder.Edit();

            }
            RebindCustomersDataGrid();
            RebindOrdersDataGrid();
            ViewOrderMode();
        }
        void ViewOrderMode()
        {
            CurrentOrderMode = OrderMode.View;
            dgOrder.Visible = true;
            dgOrder.Dock = DockStyle.Fill;

            dgOrderedFoods.Visible = true;
            dgOrderedFoods.Dock = DockStyle.Fill;

            flwLayoutOrderFilter.Visible = true;
            flwLayoutOrder.Visible = true;


            btnSubmitOrder.Text = Common.Words["Edit"];
            btnCancelOrder.Text = Common.Words["Reset"];


            dgFood.Visible = false;
            dgSelectedFoods.Visible = false;

            flwLayoutCategory.Visible = false;

            tsBtnOFSave.Visible = true;
        }
        void SubmitOrderMode()
        {
            CurrentOrderMode = OrderMode.Submit;

            if (splitContainerOrder.Panel2Collapsed == true)
            {
                splitContainerOrder.Panel2Collapsed = false;
            }

            dgFood.DataSource = FoodController.ReadAll().Where(food=>food.Enabled).ToList();
            categories = new BindingList<Category>(CategoryController.ReadAll().ToList());
            categories.Insert(0, new Category() { Id = Guid.Empty, CategoryName = Common.Words["All"] });
            comboCategory.DataSource = categories;
            

            dgSelectedFoods.Visible = true;
            dgSelectedFoods.Dock = DockStyle.Fill;

            dgFood.Visible = true;
            dgFood.Dock = DockStyle.Fill;

            txtDeliveryFee.Enabled = false;

            flwLayoutOrder.Visible = true;
            flwLayoutCategory.Visible = true;
            flwLayoutOrderFilter.Visible = false;

            dgOrder.Visible = false;
            dgOrderedFoods.Visible = false;

            tsBtnOFSave.Visible = false;

            btnSubmitOrder.Text = Common.Words["Submit"];
            btnCancelOrder.Text = Common.Words["Cancel"];
            btnSubmitOrder.Visible = true;
            btnCancelOrder.Visible = true;

            //Default values
            txtDeliveryFee.Text = "0";
            numUpDownDiscount.Value = 0;

            comboOrderType.SelectedValue = OrderType.Restaurant;

        }
        void EditOrderMode()
        {
            CurrentOrderMode = OrderMode.Edit;

            dgFood.Visible = true;
            dgFood.Dock = DockStyle.Fill;



            flwLayoutCategory.Visible = true;
            flwLayoutOrderFilter.Visible = false;

            dgOrder.Visible = false;

            tsBtnOFSave.Visible = true;
        }

        private void tsBtnOFSave_Click(object sender, EventArgs e)
        {
            foreach (Food food in CurrentOrder.Foods.Where((food) => food.State == ModelState.Constructed || food.State == ModelState.Constructing))
            {

                CurrentOrder.SaveFood(food);
            }

            tsBtnOFSave.Image = Properties.Resources.save;
            ViewOrderMode();
        }

        private void dgSelectedFoods_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            e.Cancel = true;

            foreach (DataGridViewRow row in dgSelectedFoods.SelectedRows)
            {
                NewOrder.Foods.Remove(row.DataBoundItem as Food);
            }

        }

        private void btnCancelOrder_Click(object sender, EventArgs e)
        {
            if (CurrentOrderMode == OrderMode.Submit)
            {
                NewOrder = null;
                ViewOrderMode();
            }
            else if (CurrentOrderMode == OrderMode.View)
            {
                CurrentOrder.Reload();
                txtDeliveryFee.Text = CurrentOrder.DeliveryFee.ToString();
                numUpDownDiscount.Value = CurrentOrder.Discount;
                comboOrderType.SelectedValue = CurrentOrder.OrderType;
            }
        }



        private void dgCustomer_SelectionChanged(object sender, EventArgs e)
        {



            if (CurrentCustomer != null)
            {
                dgOrder.ReadOnly = true;

                ViewOrderMode();


               splitContainerMain.Panel2Collapsed = false;

                splitContainerOrder.Panel2Collapsed = true;

                dgOrder.DataSource = CurrentCustomer.Orders;


                EnableMainToolbarControls();
            }
            else
            {
                DisableMainToolbarControls();
                dgOrder.DataSource = null;
                dgOrderedFoods.DataSource = null;


            }

        }

        private void dgOrder_SelectionChanged(object sender, EventArgs e)
        {
            if (CurrentCustomer != null)
            {

                if (CurrentOrder != null)
                {
                    dgOrderedFoods.DataSource = CurrentOrder.Foods;


                    txtDeliveryFee.Tag = txtDeliveryFee.Text = CurrentOrder.DeliveryFee.ToString();
                    numUpDownDiscount.Value = CurrentOrder.Discount;

                    comboOrderStatus.SelectedValue = CurrentOrder.Status;
                    comboOrderType.SelectedValue = CurrentOrder.OrderType;

                    splitContainerOrder.Panel2Collapsed = false;
                }
                else
                {
                    dgOrderedFoods.DataSource = null;
                }
            }
        }

        #region Order

        private void chkOrderTypes_CheckedChanged(object sender, EventArgs e)
        {
            FilterOrders();
            comboOrderTypes.Enabled = chkOrderTypes.Checked;
        }

        private void chkOrderStatuses_CheckedChanged(object sender, EventArgs e)
        {
            FilterOrders();
            comboStatuses.Enabled = chkOrderStatuses.Checked;
        }
        private void FilterOrders(bool reload = false)
        {
            if (reload)
                dgOrder.DataSource = CurrentCustomer.ReadOrders();
            if (chkNoFilter.CheckState == CheckState.Checked || (chkOrderTypes.CheckState == CheckState.Unchecked && chkOrderStatuses.CheckState == CheckState.Unchecked))
            {
                dgOrder.DataSource = CurrentCustomer.Orders;


            }
            else if (chkNoFilter.CheckState == CheckState.Unchecked)
            {
                if (chkOrderStatuses.CheckState == CheckState.Checked && chkOrderTypes.CheckState == CheckState.Unchecked)
                {
                    dgOrder.DataSource = CurrentCustomer.ReadOrdersByStatus((OrderStatus)comboStatuses.SelectedValue);
                }
                else if (chkOrderTypes.CheckState == CheckState.Checked && chkOrderStatuses.CheckState == CheckState.Unchecked)
                {
                    dgOrder.DataSource = CurrentCustomer.ReadOrdersByType((OrderType)comboOrderTypes.SelectedValue);
                }
                else if (chkOrderTypes.CheckState == CheckState.Checked && chkOrderStatuses.CheckState == CheckState.Checked)
                {
                    dgOrder.DataSource = CurrentCustomer.FilterOrders(order => order.Status == (OrderStatus)comboStatuses.SelectedValue && order.OrderType == (OrderType)comboOrderTypes.SelectedValue);
                }
            }
            chkOrderStatuses.Enabled = chkOrderTypes.Enabled = !chkNoFilter.Checked;
            comboStatuses.Enabled = chkOrderStatuses.Checked & chkOrderStatuses.Enabled;
            comboOrderTypes.Enabled = chkOrderTypes.Checked & chkOrderStatuses.Enabled;

        }

        private void txtSearchFood_TextChanged(object sender, EventArgs e)
        {
            
            if ((sender as TextBox).Text != string.Empty)
                dgFood.DataSource = FoodController.Search((sender as TextBox).Text, comboCategory.SelectedValue as Category).Where(food => food.Enabled).ToList();
            else
                dgFood.DataSource = FoodController.ReadByCategory(comboCategory.SelectedValue as Category).Where(food=>food.Enabled).ToList();
        }
        private void comboOrderTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CurrentCustomer != null && !chkNoFilter.Checked)
            {

                FilterOrders();
            }
        }
        private void comboStatuses_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CurrentCustomer != null && !chkNoFilter.Checked)
            {


                FilterOrders();
            }


        }

        private void chkShowAll_CheckedChanged(object sender, EventArgs e)
        {
            FilterOrders();
        }


        private void dgOrder_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            Common.ShowDeletePrompt(dataGrid: sender as DataGridView, DeleteCanceled: delegate() { e.Cancel = true; });
        }

        private void tsOFBtnDelete_Click(object sender, EventArgs e)
        {
            if (CurrentOrderMode == OrderMode.View)
            {
                foreach (DataGridViewRow row in dgOrderedFoods.SelectedRows)
                {
                    DeleteFood(row);
                }
            }
            else if (CurrentOrderMode == OrderMode.Submit || CurrentOrderMode == OrderMode.Edit)
            {

                foreach (DataGridViewRow row in dgSelectedFoods.SelectedRows)
                {
                    NewOrder.Foods.Remove(row.DataBoundItem as Food);
                }
            }
        }



        private void DeleteFood(DataGridViewRow row)
        {
            if (row.DataBoundItem != null)
            {
                (CurrentOrder).DeleteFood(row.DataBoundItem as Food);
                dgOrderedFoods.Rows.Remove(row);
            }
        }

        private void dgOrderedFoods_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            e.Cancel = true;
            DeleteFood(e.Row);
        }

        private void tsOFIncrease_Click(object sender, EventArgs e)
        {
            if (CurrentOrderMode == OrderMode.View || CurrentOrderMode == OrderMode.Edit)
            {
                foreach (DataGridViewRow row in dgOrderedFoods.SelectedRows)
                    if (row.DataBoundItem != null)
                        (row.DataBoundItem as Food).IncreaseQuantity(CurrentOrder);
            }
            else if (CurrentOrderMode == OrderMode.Submit || CurrentOrderMode == OrderMode.Edit)
            {
                foreach (DataGridViewRow row in dgSelectedFoods.SelectedRows)
                    if (row.DataBoundItem != null)
                        (row.DataBoundItem as Food).Quantity++;
            }
        }

        private void tsOFDecrease_Click(object sender, EventArgs e)
        {

            if (CurrentOrderMode == OrderMode.View || CurrentOrderMode == OrderMode.Edit)
            {
                foreach (DataGridViewRow row in dgOrderedFoods.SelectedRows)
                    if (row.DataBoundItem != null)
                        (row.DataBoundItem as Food).DecreaseQuantity(CurrentOrder);
            }
            else if (CurrentOrderMode == OrderMode.Submit || CurrentOrderMode == OrderMode.Edit)
            {
                foreach (DataGridViewRow row in dgSelectedFoods.SelectedRows)
                    if (row.DataBoundItem != null)
                        (row.DataBoundItem as Food).Quantity--;
            }

        }


        #endregion

        private void mnuAbout_Click(object sender, EventArgs e)
        {
            new frmAbout().ShowDialog();
        }

        private void subMnuSwitchToAdministrationPanel_Click(object sender, EventArgs e)
        {
            this.Hide();
            //new frmManagement().Show();

            Common.AdministrationForm.Show();
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Application.Exit();
        }

        private void dgOrder_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            ShowReport();
        }



        private void frmMain_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.Control)
            {
                switch (e.KeyCode)
                {
                    case Keys.S: SaveNewRows(); break;
                    case Keys.E: SaveModifiedRows(); break;
                    case Keys.F: txtSearchCustomer.Focus(); break;
                }
            }
            if (e.KeyCode == Keys.F5)
                DiscardChanges();

        }

        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }


    }
}