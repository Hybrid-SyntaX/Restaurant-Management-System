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
    public partial class frmManageCategories : Form
    {
        Category CategoryController;
        public static bool FullCategoryDeleted;
        public static BindingList<Category> categories;
        public frmManageCategories()
        {
            InitializeComponent();
            Common.Initialize();
            CategoryController = new Category();
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Category newCategory = new Category();
            newCategory.SaveSuccessful += new EventHandler(newCategory_Saved);

            if (txtCategory.Text != null)
            {
                newCategory.CategoryName = txtCategory.Text.Trim();
                newCategory.Save();
                txtCategory.Text = string.Empty;
            }
        }
        void newCategory_Saved(object sender, EventArgs e)
        {

            categories = new BindingList<Category>();
            //categories.Add(new Category() { CategoryName = Common.Words["All"], Id = Guid.Parse("00000000-0000-0000-0000-000000000000") });
            foreach (Category category in (sender as Category).ReadAll())
            {
                categories.Add(category);
            }


            comboCategory.DataSource = categories;
            comboCategory.DisplayMember = "CategoryName";

            if (categories.Count > 0)
                grpModifyCategory.Enabled = true;
            else
                grpModifyCategory.Enabled = false;
        }

        private void btnCancel_Click_1(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void frmNewCategory_Load(object sender, EventArgs e)
        {
            this.Text = Common.Words["MangeCategories"];
            lblCategory.Text = Common.Words["Category"];
            lblCategory2.Text = Common.Words["Category"];
            grpModifyCategory.Text = Common.Words["ModifyCategory"];
            grpNewCategory.Text = Common.Words["NewCategory"];

            Common.ToggleRightToLeft(flwLayoutNewCategory);
            Common.ToggleRightToLeft(grpModifyCategory);
            Common.ToggleRightToLeft(grpNewCategory);
            Common.ToggleRightToLeft(flwLayoutNewCategory);
            Common.ToggleRightToLeft(flwModifyCategory);
            Common.ToggleRightToLeft(_txtCategory);



            categories = new BindingList<Category>(CategoryController.ReadAll().OfType<Category>().ToList());
            //Common.InitializeList(ref categories, ref CategoryController);


            comboCategory.DataSource = categories;
            comboCategory.DisplayMember = "CategoryName";

            if (categories.Count == 0)
                grpModifyCategory.Enabled = false;

            //grpModifyCategory = false;


        }

        private void btnDeleteCategory_Click(object sender, EventArgs e)
        {
            if (comboCategory.SelectedValue != null && categories.Count>1)
            {
                (comboCategory.SelectedValue as Category).DeleteSuccessful += new EventHandler(newCategory_Saved);
                DialogResult dialogResult = MessageBox.Show(string.Format(Common.Words["DeletePrompt"], (comboCategory.SelectedValue as Category).CategoryName), Common.Words["DeletePrompt_Title"], MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dialogResult == DialogResult.Yes)
                {
                    frmChangeCategory.foods = (comboCategory.SelectedValue as Category).ReadFoods();
                    if (frmChangeCategory.foods.Count == 0)
                    {
                        (comboCategory.SelectedValue as Category).Delete();
                        MessageBox.Show(Common.Words["DeletedMessage"], Common.Words["DeletedMessage_Title"], MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show(Common.Words["CategoryHasRows"], Common.Words["Warning"], MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        frmChangeCategory.CategoryChangedSuccessfully += new EventHandler(frmChangeCategory_CategoryChangedSuccessfully);
                        new frmChangeCategory().ShowDialog();
                    }


                }
            }
            else if (categories.Count <= 1)
            {
                MessageBox.Show(Common.Words["OnlyOneCategoryExists"],Common.Words["Error"],MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        void frmChangeCategory_CategoryChangedSuccessfully(object sender, EventArgs e)
        {
            (comboCategory.SelectedValue as Category).DeleteSuccessful += new EventHandler(newCategory_Saved);
            if ((sender as frmChangeCategory).CategoryChanged)
            {
                (comboCategory.SelectedValue as Category).Delete();
                MessageBox.Show(Common.Words["DeletedMessage"], Common.Words["DeletedMessage_Title"], MessageBoxButtons.OK, MessageBoxIcon.Information);
                FullCategoryDeleted = true;
            }
            else
            {
                MessageBox.Show(Common.Words["CannotDeleteCategory"], Common.Words["Error"], MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        bool EditMode = false;
        private void btnModifyCategory_Click(object sender, EventArgs e)
        {
            if (!EditMode)
            {
                comboCategory.Visible = false;
                _txtCategory.Location = comboCategory.Location;
                _txtCategory.Size = comboCategory.Size;
                _txtCategory.Text = comboCategory.Text;
                _txtCategory.Visible = true;
                EditMode = true;
            }
            else if (EditMode)
            {
                (comboCategory.SelectedValue as Category).EditSuccessful += new EventHandler(newCategory_Saved);
                (comboCategory.SelectedValue as Category).CategoryName = _txtCategory.Text;
                (comboCategory.SelectedValue as Category).Edit();

                _txtCategory.Visible = false;
                comboCategory.Visible = true;
                comboCategory.Text = _txtCategory.Text;
                EditMode = false;
            }
        }

    }
}
