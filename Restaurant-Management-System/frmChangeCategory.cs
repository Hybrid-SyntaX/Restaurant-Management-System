using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Logic;
namespace Restaurant_Management_System
{
    public partial class frmChangeCategory : Form
    {
        public static event EventHandler CategoryChangedSuccessfully;
        Category CategoryController;
        Food FoodController;
        public static BindingList<Food> foods;
        public  bool CategoryChanged;
        public static Category newCategory;
        public frmChangeCategory()
        {
            InitializeComponent();
            Common.Initialize();
            CategoryController = new Category();
            FoodController = new Food();
            newCategory = new Category();
        }

        private void frmChangeCategory_Load(object sender, EventArgs e)
        {
            btnChangeCategory.Text = Common.Words["ChangeCategory"];
            lblCategory.Text = Common.Words["Category"];
            Common.ToggleRightToLeft(flwLayoutChangeCategory);
            this.Text = Common.Words["ChangeCategory"];
            comboCategory.DataSource = CategoryController.ReadAll();
            comboCategory.DisplayMember = "CategoryName";
        }

        private void btnChangeCategory_Click(object sender, EventArgs e)
        {
            CategoryChanged = true;
            foreach (Food food in foods)
            {
                if (!food.Category.Equals(comboCategory.SelectedValue as Category))
                {
                    food.Category = comboCategory.SelectedValue as Category;
                    food.Edit();
           
                    CategoryChanged &= true;
                }
                else
                {
                    CategoryChanged &= false;
                }
                //food.Edit(food.Id, new Food() { Category = comboCategory.SelectedValue as Category });
            }
            FoodController.ReadAll();
            foods = new BindingList<Food>();
            foods = FoodController.LocalDataSource as BindingList<Food>;

            newCategory = comboCategory.SelectedValue as Category;



            if (CategoryChanged)
            {
                MessageBox.Show(Common.Words["CategoryChangedSuccessfully"], Common.Words["Message"], MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (CategoryChangedSuccessfully != null)
                    CategoryChangedSuccessfully(this, EventArgs.Empty);
            }
            else
            {
                MessageBox.Show(Common.Words["SameCategory"], Common.Words["Error"], MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

                this.Hide();
            

        }
    }
}
