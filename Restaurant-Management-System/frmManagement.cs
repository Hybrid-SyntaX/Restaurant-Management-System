using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Logic;
using System.IO;
using System.Security.Cryptography;
using System.Data.SqlServerCe;
using System.Threading;
using System.Text.RegularExpressions;
using Chocolatey.Validation;
using Chocolatey.Iteratration;
using System.Windows.Forms.DataVisualization.Charting;
namespace Restaurant_Management_System
{

    public partial class frmManagement : Form
    {
        //Lists
        BindingList<Food> foods;
        BindingList<Category> categories;
        BindingList<Employee> employees;
        BindingList<Order> orders;

        //Controllers
        Food FoodController;
        Category CategoryController;
        Employee EmployeeController;
        Order OrderController;


        public frmManagement()
        {

            InitializeComponent();

            Common.Initialize();



            //Initializing contollers
            FoodController = new Food();
            EmployeeController = new Employee();
            OrderController = new Order();
            CategoryController = new Category();



            //Initializing lists
            Common.InitializeList(ref foods, ref FoodController);
            Common.InitializeList(ref employees, ref EmployeeController);
            Common.InitializeList(ref orders, ref OrderController);
            Common.InitializeList(ref categories, ref CategoryController);



            categories.Insert(0, new Category() { CategoryName = Common.Words["All"], Id = Guid.Empty });

            Common.PopulateLanguageMenu(mnuLanguage, delegate()
            {
                InitializeLanguage();
            });

            InitializeStatistics();
            InitializeLanguage();
            InitializeFoodsDataGrid();
            InitializeEmployeesDataGrid();
        }


        #region Statistics
        private void InitializeStatistics()
        {

            FilterSales();

            chkYear.CheckedChanged += new EventHandler(delegate(object sender, EventArgs e)
            {
                FilterSales();
            });
            chkDay.CheckedChanged += new EventHandler(delegate(object sender, EventArgs e)
            {
                FilterSales();
            });
            chkMonth.CheckedChanged += new EventHandler(delegate(object sender, EventArgs e)
            {
                FilterSales();
            });

            comboYear.SelectedIndexChanged += new EventHandler(delegate(object sender, EventArgs e)
            {
                FilterSales();
            });

            comboMonth.SelectedIndexChanged += new EventHandler(delegate(object sender, EventArgs e)
            {
                FilterSales();
            });
            comboDay.SelectedIndexChanged += new EventHandler(delegate(object sender, EventArgs e)
            {
                FilterSales();
            });
        }

        private void FilterSales()
        {
            if (comboYear.Items.Count == 0)
            {
                List<int> years = orders.Select(order => order.OrderDate.Year).Distinct().ToList();
                years.Sort();
                foreach (int year in years)
                {

                    comboYear.Items.Add(year);
                }
                if (comboYear.Items.Count > 0)
                    comboYear.SelectedItem = comboYear.Items[0];
            }

            if (comboMonth.Items.Count == 0)
            {

                for (int month = 1; month <= 12; comboMonth.Items.Add(month), month++) ;
                comboMonth.SelectedItem = comboMonth.Items[0];
            }
            if (comboDay.Items.Count == 0)
            {
                //int daysInMonth = DateTime.DaysInMonth((int)comboYear.SelectedItem, (int)comboMonth.SelectedItem);
                int daysInMonth = 31;
                for (int day = 1; day <= daysInMonth; comboDay.Items.Add(day), day++) ;
                comboDay.SelectedItem = comboDay.Items[0];
            }

            if (!chkDay.Checked && !chkMonth.Checked && !chkYear.Checked) ViewAnnuallySales();
            else if (chkYear.Checked && !chkMonth.Checked && !chkDay.Checked) ViewMonthlySales();
            else if (chkMonth.Checked && chkYear.Checked && !chkDay.Checked) ViewDailySales();
            else if (chkDay.Checked && chkMonth.Checked && chkYear.Checked) ViewHourlySales();

            if (!chkYear.Checked)
                chkMonth.Checked = chkMonth.Enabled = false;
            if (!chkMonth.Checked)
                chkDay.Checked = chkDay.Enabled = false;

            comboYear.Enabled = chkYear.Checked & chkYear.Enabled;

            chkMonth.Enabled = comboYear.Enabled & chkYear.Checked;
            comboMonth.Enabled = chkMonth.Enabled & chkMonth.Checked;


            chkDay.Enabled = comboMonth.Enabled;
            comboDay.Enabled = chkDay.Checked & chkDay.Enabled;

        }
        private void ViewCustomSales(Func<Order, bool> condition, int i, int n)
        {
            chrtSales.Series["seriesSales"].Points.Clear();
            while (i <= n)
            {
                chrtSales.Series["seriesSales"].Points.AddXY(i, OrderController.getTotalSales(condition));
                i++;
            }
        }
        private void ViewHourlySales()
        {


            chrtSales.Series["seriesSales"].Points.Clear();
            chrtSales.ChartAreas["chartSales"].AxisX.IntervalOffsetType = DateTimeIntervalType.Hours;
            chrtSales.ChartAreas["chartSales"].AxisX.Title = Common.Words["Hour"];
            chrtSales.Series["seriesSales"].LegendText = string.Format(Common.Words["SalesIn"], Common.Words["Hour"]);

            if (comboYear.SelectedItem != null && comboMonth.SelectedItem != null && comboDay.SelectedItem != null)
            {
                for (int hour = 0; hour <= 23; hour++)
                {
                    chrtSales.Series["seriesSales"].Points.AddXY(hour, OrderController.getTotalHourlySales((int)comboYear.SelectedItem, (int)comboMonth.SelectedItem, (int)comboDay.SelectedItem, hour));
                }

            }
        }

        private void ViewDailySales()
        {

            chrtSales.Series["seriesSales"].Points.Clear();
            chrtSales.ChartAreas["chartSales"].AxisX.IntervalOffsetType = DateTimeIntervalType.Days;
            chrtSales.ChartAreas["chartSales"].AxisX.Title = Common.Words["Day"];
            chrtSales.Series["seriesSales"].LegendText = string.Format(Common.Words["SalesIn"], Common.Words["Day"]);


            if (comboYear.SelectedItem != null && comboMonth.SelectedItem != null)
            {
                int daysInMonth = DateTime.DaysInMonth((int)comboYear.SelectedItem, (int)comboMonth.SelectedItem);
                for (int day = 1; day <= daysInMonth; day++) //31
                {
                    chrtSales.Series["seriesSales"].Points.AddXY(day, OrderController.getTotalDailySales((int)comboYear.SelectedItem, (int)comboMonth.SelectedItem, day));

                }

            }
        }

        private void ViewAnnuallySales()
        {
            chrtSales.Series["seriesSales"].Points.Clear();

            chrtSales.ChartAreas["chartSales"].AxisX.IntervalOffsetType = DateTimeIntervalType.Years;
            chrtSales.ChartAreas["chartSales"].AxisX.Title = Common.Words["Year"];
            chrtSales.Series["seriesSales"].LegendText = string.Format(Common.Words["SalesIn"], Common.Words["Year"]);

            if (OrderController.FirstOrder != null)
                for (int year = OrderController.FirstOrder.OrderDate.Year; year <= OrderController.LastOrder.OrderDate.Year; year++)
                {

                    chrtSales.Series["seriesSales"].Points.AddXY(year, OrderController.getTotalAnnualSales(year));

                }


        }

        private void ViewMonthlySales()
        {
            chrtSales.Series["seriesSales"].Points.Clear();
            chrtSales.ChartAreas["chartSales"].AxisX.IntervalOffsetType = DateTimeIntervalType.Months;
            chrtSales.ChartAreas["chartSales"].AxisX.Title = Common.Words["Month"];
            chrtSales.Series["seriesSales"].LegendText = string.Format(Common.Words["SalesIn"], Common.Words["Month"]);

            if (comboYear.SelectedItem != null)
            {
                for (int month = 1; month <= 12; month++)
                {
                    chrtSales.Series["seriesSales"].Points.AddXY(month, OrderController.getTotalMonthlySales((int)comboYear.SelectedItem, month));
                }
            }

        }

        #endregion
       
        private void frmManagement_Load(object sender, EventArgs e)
        {

        }
        public void InitializeLanguage()
        {

            //tooltips
            tsBtnSave.ToolTipText = Common.Words["ttSave"];
            btnManageCategories.Text = Common.Words["ttManageCategory"];
            tsBtnChangeCategory.Text = Common.Words["ttChangeCategory"];
            tsBtnDiscard.Text = Common.Words["ttDiscardChanges"];

            if (tabManagement.SelectedTab.Equals(tabFood))
            {
                tsBtnAdd.ToolTipText = string.Format(Common.Words["ttAdd"], Common.Words["Food"]);
                tsBtnDelete.ToolTipText = string.Format(Common.Words["ttDelete"], Common.Words["Food"]);
                tsBtnEdit.ToolTipText = string.Format(Common.Words["ttEdit"], Common.Words["Food"]);
            }
            else if (tabManagement.SelectedTab.Equals(tabEmployee))
            {
                tsBtnAdd.ToolTipText = string.Format(Common.Words["ttAdd"], Common.Words["Employee"]);
                tsBtnDelete.ToolTipText = string.Format(Common.Words["ttDelete"], Common.Words["Employee"]);
                tsBtnEdit.ToolTipText = string.Format(Common.Words["ttEdit"], Common.Words["Employee"]);
            }

            //Checkboxes
            chkDay.Text = Common.Words["Day"];
            chkMonth.Text = Common.Words["Month"];
            chkYear.Text = Common.Words["Year"];




            //laebls
            lblSearch.Text = Common.Words["Search"];
            lblCategory.Text = Common.Words["Category"];
            lblSearchEmployee.Text = Common.Words["Search"];
            categories[0].CategoryName = Common.Words["All"];
            //menus
            subMnuExit.Text = Common.Words["Exit"];
            mnuFile.Text = Common.Words["File"];
            mnuLanguage.Text = Common.Words["Language"];
            mnuAbout.Text = Common.Words["About"];
            subMnuSwitchToMain.Text = Common.Words["SwitchToMainPanel"];



            this.Text = Common.Words["AdminPanelTitle"];
            //Tabs
            tabEmployee.Text = Common.Words["Employee"];
            tabFood.Text = Common.Words["Food"];
            tabStatistics.Text = Common.Words["Statistics"];
            tabSales.Text = Common.Words["Sales"];
            //dgFood columns
            if (dgFood.Columns.Count > 0)
            {
                dgFood.Columns["FoodName"].HeaderText = Common.Words["FoodName"];
                dgFood.Columns["Price"].HeaderText = Common.Words["Price"];
                dgFood.Columns["Enabled"].HeaderText = Common.Words["Enabled"];
            }
            //dgEmployee columns
            if (dgEmployee.Columns.Count > 0)
            {
                dgEmployee.Columns["FirstName"].HeaderText = Common.Words["FirstName"];
                dgEmployee.Columns["LastName"].HeaderText = Common.Words["LastName"];
                dgEmployee.Columns["Username"].HeaderText = Common.Words["Username"];
                dgEmployee.Columns["Password"].HeaderText = Common.Words["Password"];
            }

            chrtSales.Series["seriesSales"].LegendText = string.Format(Common.Words["SalesIn"], Common.Words["Year"]);

            Common.ToggleRightToLeft(flwLayoutPnlFood);
            Common.ToggleRightToLeft(tabEmployee);
            Common.ToggleRightToLeft(tabFood);
            Common.ToggleRightToLeft(tabManagement);
            Common.ToggleRightToLeft(toolbarManagement);
            Common.ToggleRightToLeft(dgFood);
            Common.ToggleRightToLeft(dgEmployee);
            Common.ToggleRightToLeft(menuStripManagement);

            InitializeEmployeeRole();
        }


        #region Populating Datagrids

        private void InitializeFoodsDataGrid()
        {
            dgFood.AutoGenerateColumns = false;
            dgFood.DataSource = FoodController.LocalDataSource;


            Common.AddDataGridViewColumn(dgFood, "FoodName");
            Common.AddDataGridViewColumn(dgFood, "Price");
            DataGridViewCheckBoxColumn columnEnabled = new DataGridViewCheckBoxColumn() { Name = "Enabled", DataPropertyName = "Enabled" };


            columnEnabled.Width = 60;

            dgFood.Columns.Add(columnEnabled);
            comboCategory.DataSource = categories;
            comboCategory.DisplayMember = "CategoryName";

        }
        private void HideSuperAdmin()
        {
            EmployeeController.LocalDataSource.RemoveAt(0);
        }
        private void InitializeEmployeesDataGrid()
        {


            dgEmployee.AutoGenerateColumns = false;


            HideSuperAdmin();
            dgEmployee.DataSource = EmployeeController.LocalDataSource;

            //dgEmployee.DataSource = employees;


            Common.AddDataGridViewColumn(dgEmployee, "FirstName");
            Common.AddDataGridViewColumn(dgEmployee, "LastName");
            Common.AddDataGridViewColumn(dgEmployee, "Username");
            Common.AddDataGridViewColumn(dgEmployee, "Password");
            dgEmployee.Columns.Add(new DataGridViewComboBoxColumn() { Name = "Role" });


            InitializeEmployeeRole();

        }
        public void InitializeEmployeeRole()
        {
            if (dgEmployee.Columns.Contains("Role"))
            {
                List<object> roles = new List<object>() { 
                            new { Value = Role.Default, Text = Common.Words["Default"] } ,
                            new { Value = Role.Manager, Text = Common.Words["Manager"] } ,
                            new { Value = Role.Cashier, Text = Common.Words["Cashier"] } };

                (dgEmployee.Columns["Role"] as DataGridViewComboBoxColumn).DataSource = roles;
                (dgEmployee.Columns["Role"] as DataGridViewComboBoxColumn).DisplayMember = "Text";
                (dgEmployee.Columns["Role"] as DataGridViewComboBoxColumn).ValueMember = "Value";
                (dgEmployee.Columns["Role"] as DataGridViewComboBoxColumn).DataPropertyName = "Role";
                (dgEmployee.Columns["Role"] as DataGridViewComboBoxColumn).HeaderText = Common.Words["Role"];
            }
        }

        #endregion
    
        private void comboCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtSearch.Text = string.Empty;
            if ((comboCategory.SelectedValue as Category).Id.Equals(Guid.Empty))
            {
                dgFood.AllowUserToAddRows = false;
                tsBtnAdd.Enabled = false;
                tsBtnSave.Enabled = false;
            }
            else
            {
                dgFood.AllowUserToAddRows = true;
                tsBtnAdd.Enabled = true;
                tsBtnSave.Enabled = true;
            }

            dgFood.DataSource = FoodController.ReadByCategory(comboCategory.SelectedValue as Category);
        }

        #region Delete
        private void RebindFoodsDataGrid(bool currentCategory = true)
        {
            foods = new BindingList<Food>(FoodController.ReadAll().OfType<Food>().ToList());


            if (currentCategory)
            {
                dgFood.DataSource = FoodController.ReadByCategory((comboCategory.SelectedValue as Category));
            }
            else
            {
                dgFood.DataSource = FoodController.LocalDataSource;
            }

        }
        private void RebindEmployeesDataGrid(bool hardRebind = false)
        {
            employees = new BindingList<Employee>(EmployeeController.ReadAll().OfType<Employee>().ToList());
            HideSuperAdmin();
            dgEmployee.DataSource = EmployeeController.LocalDataSource;


        }
        private void tsBtnDelete_Click(object sender, EventArgs e)
        {
            if (IsActiveTab(tabFood))
            {
                int orderFoodsCount = 0;
                foreach (DataGridViewRow row in dgFood.SelectedRows)
                {
                    orderFoodsCount = (from Order order in orders where order.Foods.Contains(row.DataBoundItem as Food) select order).Count();
                }
                if (orderFoodsCount == 0)
                {
                    Common.ShowDeletePrompt(dgFood);
                    RebindFoodsDataGrid();
                }
                else
                {
                    MessageBox.Show(string.Format(Common.Words["FoodBelognsToOrder"], orderFoodsCount.ToString()), Common.Words["Error"], MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (IsActiveTab(tabEmployee))
            {
                Common.ShowDeletePrompt(dgEmployee);
                RebindEmployeesDataGrid();
            }
        }
        private void dgFood_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            e.Cancel = true;
            int orderFoodsCount = 0;
            foreach (DataGridViewRow row in dgFood.SelectedRows)
            {
                orderFoodsCount = (from Order order in orders where order.Foods.Contains(row.DataBoundItem as Food) select order).Count();
            }
            if (orderFoodsCount == 0)
            {

                Common.ShowDeletePrompt(sender as DataGridView);
                RebindFoodsDataGrid();
            }
            else
            {
                MessageBox.Show(string.Format(Common.Words["FoodBelognsToOrder"], orderFoodsCount.ToString()), Common.Words["Error"], MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void dgEmployee_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            e.Cancel = true;
            Common.ShowDeletePrompt(sender as DataGridView);
            RebindEmployeesDataGrid();

        }
        private void commonDataGrid_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            MessageBox.Show(Common.Words["DeletedMessage"], Common.Words["DeletedMessage_Title"], MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        #endregion

        private void dgFood_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

            if (e.Exception is FormatException)
            {

                if (dgFood.Columns[e.ColumnIndex].ValueType == typeof(Single) || dgFood.Columns[e.ColumnIndex].ValueType == typeof(Int32))
                {
                    MessageBox.Show(Common.Words["Invalid Input(Number)"], Common.Words["Invalid Input"], MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (dgFood.Columns[e.ColumnIndex].ValueType == typeof(String))
                    MessageBox.Show(Common.Words["Invalid Input"], Common.Words["Invalid Input"], MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            Search();
        }
        private void Search()
        {
            if (IsActiveTab(tabFood))
            {
                dgFood.DataSource = FoodController.Search(txtSearch.Text, comboCategory.SelectedValue as Category);
            }
            else if (IsActiveTab(tabEmployee))
            {
                dgEmployee.DataSource = EmployeeController.Search(txtSearchEmployee.Text);
            }
        }


        #region New Employee/Food
        private void dgEmployee_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {

            //Employee newEmployee = new Employee(); //employees.AddNew();
            //employees.Add(newEmployee);
            Employee newEmployee = employees.AddNew();

            newEmployee.State = ModelState.Constructing;

            dgEmployee.CurrentRow.Tag = newEmployee;

            tsBtnSave.Image = Properties.Resources.changes_pending;

        }
        private void dgFood_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            try
            {
                Food newFood = foods.AddNew();
                newFood.State = ModelState.Constructing;
                newFood.Category = comboCategory.SelectedValue as Category;
                newFood.Enabled = true;
                dgFood.CurrentRow.Cells["Enabled"].Value = true;
                dgFood.CurrentRow.Tag = newFood;

                tsBtnSave.Image = Properties.Resources.changes_pending;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
        #endregion



        private void btnAddCategory_Click(object sender, EventArgs e)
        {
            new frmManageCategories().ShowDialog();
            if (frmManageCategories.categories != null && frmManageCategories.categories.Count != 0)
            {
                BindingList<Category> newCategories = frmManageCategories.categories;

                newCategories.Insert(0, new Category() { CategoryName = Common.Words["All"], Id = Guid.Empty });

                comboCategory.DataSource = newCategories;

                comboCategory.SelectedItem = newCategories.Last();
                if (frmManageCategories.FullCategoryDeleted)
                    RebindFoodsDataGrid(true);
            }
        }

        private void tsBtnAdd_Click(object sender, EventArgs e)
        {
            if (IsActiveTab(tabFood))
                dgFood.CurrentCell = dgFood.Rows[dgFood.Rows.Count - 1].Cells["FoodName"];
            else if (IsActiveTab(tabEmployee))
                dgEmployee.CurrentCell = dgEmployee.Rows[dgEmployee.Rows.Count - 1].Cells["FirstName"];

        }

        private void subMnuExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void tsBtnChangeCategory_Click(object sender, EventArgs e)
        {
            if (dgFood.SelectedRows.Count > 0)
            {
                frmChangeCategory.foods = new BindingList<Food>();
                frmChangeCategory.CategoryChangedSuccessfully += new EventHandler(frmChangeCategory_CategoryChangedSuccessfully);
                foreach (DataGridViewRow row in dgFood.SelectedRows)
                {
                    frmChangeCategory.foods.Add(row.DataBoundItem as Food);
                }

                new frmChangeCategory().ShowDialog();
            }
        }

        void frmChangeCategory_CategoryChangedSuccessfully(object sender, EventArgs e)
        {
            FoodController.LocalDataSource = frmChangeCategory.foods;

            comboCategory.Text = frmChangeCategory.newCategory.CategoryName;
        }




        private void txtSearchEmployee_TextChanged(object sender, EventArgs e)
        {

            Search();

        }
        private void tsBtnDiscard_Click(object sender, EventArgs e)
        {
            DiscardChanges();

        }

        private void DiscardChanges()
        {
            if (IsActiveTab(tabFood))
            {
                Common.CommonDiscardChanges(foods);
                RebindFoodsDataGrid(true);

            }
            else if (IsActiveTab(tabEmployee))
            {
                Common.CommonDiscardChanges(employees);
                RebindEmployeesDataGrid();
            }
            tsBtnEdit.Image = Properties.Resources.edit;
            tsBtnSave.Image = Properties.Resources.save;
        }

        private bool IsActiveTab(TabPage tab)
        {
            return Common.IsActiveTab(tabManagement, tab);
        }
        #region Edit


        private void dgFood_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {

            Common.CommonDataGridCellBeginEdit(sender as DataGridView, e);

        }
        private void dgEmployee_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {


            Common.CommonDataGridCellBeginEdit(sender as DataGridView, e);



        }
        private void dgFood_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

            Common.ModelIsChanged += new EventHandler(Common_ModelIsChanged);
            Common.ModelIsNotChanged += new EventHandler(Common_ModelIsNotChanged);
            Common.CommonDataGridCellEndEdit(sender as DataGridView, e, delegate(Food food)
            {
                //Food info
                if (!string.IsNullOrEmpty((string)dgFood.CurrentRow.Cells["FoodName"].Value))
                    food.FoodName = dgFood.CurrentRow.Cells["FoodName"].Value.ToString();
                if (dgFood.CurrentRow.Cells["Price"].Value != null && Validator.IsFloat(dgFood.CurrentRow.Cells["Price"].Value.ToString()))
                    food.Price = (float)dgFood.CurrentRow.Cells["Price"].Value;
                else Common.ShowErrorMessage("Invalid Input(Number");

                food.Enabled = (bool)dgFood.CurrentRow.Cells["Enabled"].Value;
            }
            );

        }

        void Common_ModelIsNotChanged(object sender, EventArgs e)
        {
            tsBtnEdit.Image = Properties.Resources.edit;
        }

        void Common_ModelIsChanged(object sender, EventArgs e)
        {
            tsBtnEdit.Image = Properties.Resources.edit_pending;
        }
        private void dgEmployee_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            Common.ModelIsChanged += new EventHandler(Common_ModelIsChanged);
            Common.ModelIsNotChanged += new EventHandler(Common_ModelIsNotChanged);
            Common.CommonDataGridCellEndEdit(sender as DataGridView, e, delegate(Employee employee)
            {

                if (!string.IsNullOrEmpty((string)dgEmployee.CurrentRow.Cells["FirstName"].Value))
                    employee.FirstName = dgEmployee.CurrentRow.Cells["FirstName"].Value.ToString();
                if (!string.IsNullOrEmpty((string)dgEmployee.CurrentRow.Cells["LastName"].Value))
                    employee.LastName = dgEmployee.CurrentRow.Cells["LastName"].Value.ToString();
                if (!string.IsNullOrEmpty((string)dgEmployee.CurrentRow.Cells["Username"].Value))
                    employee.Username = dgEmployee.CurrentRow.Cells["Username"].Value.ToString();
                if (!string.IsNullOrEmpty((string)dgEmployee.CurrentRow.Cells["Password"].Value))
                    employee.Password = dgEmployee.CurrentRow.Cells["Password"].Value.ToString();
                if (dgEmployee.CurrentRow.Cells["Role"].Value != null)
                    employee.Role = (Role)dgEmployee.CurrentRow.Cells["Role"].Value;
            });

        }
        private void tsBtnEdit_Click(object sender, EventArgs e)
        {
            SaveModifiedRows();
        }

        private void SaveModifiedRows()
        {

            if (IsActiveTab(tabFood))
            {
                Common.CommonDataGridSubmitEdit(foods);
                RebindFoodsDataGrid(true);
            }
            else if (IsActiveTab(tabEmployee))
            {
                Common.CommonDataGridSubmitEdit(employees);
                RebindEmployeesDataGrid();
            }
            tsBtnEdit.Image = Properties.Resources.edit;
        }

        #endregion

        private void subMnuSwitchToMain_Click(object sender, EventArgs e)
        {
            /*
            frmMain mainForm = new frmMain();
            
            this.Hide();
            mainForm.Show();
            */
            this.Hide();
            Common.MainForm.Show();
        }

        private void mnuAbout_Click(object sender, EventArgs e)
        {
            new frmAbout().ShowDialog();
        }



        private void dgEmployee_SelectionChanged(object sender, EventArgs e)
        {
            Common.ToggleControlsDisableEnable(sender as DataGridView, enable: delegate()
            {
                tsBtnDelete.Enabled = true;
            },
            disable: delegate()
            {
                tsBtnDelete.Enabled = false;
            }
            );
        }



        private void dgFood_SelectionChanged(object sender, EventArgs e)
        {

            Common.ToggleControlsDisableEnable(dgFood,
            enable: delegate()
            {
                tsBtnDelete.Enabled = true;
                tsBtnChangeCategory.Enabled = true;

            },
            disable: delegate()
            {
                tsBtnDelete.Enabled = false;
                tsBtnChangeCategory.Enabled = false;
            });
        }

        private void frmManagement_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control)
            {
                switch (e.KeyCode)
                {
                    case Keys.S: SaveNewRows(); break;
                    case Keys.E: SaveModifiedRows(); break;
                    case Keys.F: txtSearch.Focus(); break;
                }
            }
            if (e.KeyCode == Keys.F5)
                DiscardChanges();


        }

        private void tsBtnSave_Click(object sender, EventArgs e)
        {
            SaveNewRows();
        }

        private void SaveNewRows()
        {
            if (IsActiveTab(tabFood))
            {
                foreach (Food food in foods.Where((food) => food.State == ModelState.Constructed || food.State == ModelState.Constructing))
                {
                    food.Save();
                }
                RebindFoodsDataGrid(true);
            }
            else if (IsActiveTab(tabEmployee))
            {
                try
                {

                    foreach (Employee employee in EmployeeController.LocalDataSource.OfType<Employee>().Where((employee) => employee.State == ModelState.Constructed || employee.State == ModelState.Constructing))
                    {
                        employee.Save();
                    }
                }
                catch (SqlCeException ex)
                {
                    Common.HandleSqlCeException(ex);
                }
                RebindEmployeesDataGrid();
            }
            tsBtnSave.Image = Properties.Resources.save;
        }

        //Tab events
        private void tabStatistics_Enter(object sender, EventArgs e)
        {
            InitializeLanguage();
            toolbarManagement.Visible = false;
        }

        private void tabEmployee_Enter(object sender, EventArgs e)
        {
            InitializeLanguage();
            tsBtnAdd.Enabled = true;
            tsBtnSave.Enabled = true;

            toolbarManagement.Visible = true;
            tsBtnChangeCategory.Visible = false;
            btnManageCategories.Visible = false;
        }

        private void tabFood_Enter(object sender, EventArgs e)
        {
            InitializeLanguage();
            tsBtnChangeCategory.Visible = true;
            btnManageCategories.Visible = true;
            toolbarManagement.Visible = true;
            if (comboCategory.Text == (comboCategory.Items[0] as Category).CategoryName)
            {
                dgFood.AllowUserToAddRows = false;
                tsBtnSave.Enabled = false;
                tsBtnAdd.Enabled = false;
            }
        }

        private void frmManagement_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void dgFood_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.RowIndex != dgFood.NewRowIndex)
            {
                (dgFood.Rows[e.RowIndex].DataBoundItem as Food).Enabled = !(dgFood.Rows[e.RowIndex].DataBoundItem as Food).Enabled;
            }
        }





    }
}
