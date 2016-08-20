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

    public partial class frmLogin : Form
    {   
        int attempts;
        Employee EmployeeController;
        public frmLogin()
        {

            InitializeComponent();
            attempts = 1;
            Common.Initialize();
            EmployeeController = new Employee();

            if (EmployeeController.ReadAll().OfType<Employee>().Count() == 0)
            {
                Employee admin = new Employee()
                {
                    Id = Guid.NewGuid(),
                    Username = "admin",
                    Password = "admin",
                    FirstName = "admin",
                    LastName = "admin",
                    Role = Role.SuperAdmin

                };
                
                admin.Save();
            }
        }
        
        private void btnLogin_Click(object sender, EventArgs e)
        {

            EmployeeController.Username = txtUsername.Text;
            EmployeeController.Password = txtPassword.Text;
            if (EmployeeController.Authenticate() != Role.Unauthorized)
            {

                if (EmployeeController.Role != Role.SuperAdmin && EmployeeController.Username!="admin")
                    MessageBox.Show(string.Format(Common.Words["authorized_message"],EmployeeController.FullName));
                else
                    MessageBox.Show(string.Format(Common.Words["authorized_message"],Common.Words["SuperAdmin"]));

                Common.CurrentUser = EmployeeController;

                if (EmployeeController.Role == Role.Cashier)
                {
                    new frmMain().Show();
                    this.Hide();
                }
                else if (EmployeeController.Role == Role.Manager || EmployeeController.Role == Role.SuperAdmin)
                {
                    new frmManagement().Show();
                    this.Hide();
                }

            }
            else
            {
                MessageBox.Show(Common.Words["not_authorized_message"]);
                if (attempts == 3)
                {
                    Application.Exit();
                }

                attempts++;
            }



        }

        private void frmLogin_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            Common.PopulateLanguageMenu(mnuLanguage, delegate()
            {
                InitializeLanguage();
            });
            InitializeLanguage();
        }
        void InitializeLanguage()
        {
            lblUsername.Text = Common.Words["Username"];
            lblPassword.Text = Common.Words["Password"];
            mnuFile.Text = Common.Words["File"];
            subMnuExit.Text = Common.Words["Exit"];
            mnuLanguage.Text = Common.Words["Language"];
            mnuAbout.Text = Common.Words["About"];
            btnLogin.Text = Common.Words["Login"];
            this.Text = Common.Words["Login"];
            Common.ToggleRightToLeft(flwLayoutLogin);
            Common.ToggleRightToLeft(mnuStripLogin);

        }

        private void subMnuExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void mnuAbout_Click(object sender, EventArgs e)
        {
            new frmAbout().ShowDialog();
        }


    }
}
