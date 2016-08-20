using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using Logic;
using System.Data.SqlServerCe;
using System.Configuration;
using System.IO;
using Chocolatey.Data.ConfigFile;
using System.Security.Cryptography;
using System.Windows.Forms;
using System.Globalization;
using System.Text.RegularExpressions;
using Chocolatey.Data.Common;
using System.ComponentModel;
namespace Restaurant_Management_System
{
    public static class Common
    {
        public static Dictionary<string, string> Words;
        public static Employee CurrentUser;
        public static string ConnectionString;

        private static frmMain _mainForm;
        public static frmMain MainForm
        {
            get
            {
                if (_mainForm == null)
                    _mainForm = new frmMain();

                return _mainForm;
            }
        }

        private static frmManagement _administrationForm;
        public static frmManagement AdministrationForm
        {
            get
            {
                if (_administrationForm == null)
                    _administrationForm = new frmManagement();

                return _administrationForm;

            }
        }

        private static ConfigFileParser configFileParser;


        private static DatabaseHelper _database;
        public static DatabaseHelper Database
        {
            set
            {
                _database = value;
            }
            get
            {
                if (_database == null)
                {
                    _database = new DatabaseHelper();
                    _database.Initialize<SqlCeCommand, SqlCeConnection, SqlCeDataAdapter, SqlCeCommandBuilder>(ConnectionString);
                }

                return _database;
            }
        }

        public static event EventHandler ModelIsChanged;
        public static event EventHandler ModelIsNotChanged;


        public static string MD5Hash(string str)
        {
            return Convert.ToBase64String(MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(str)));
        }
        public static string SHA1Hash(string str)
        {
            return Convert.ToBase64String(SHA1.Create().ComputeHash(Encoding.UTF8.GetBytes(str)));
        }

        public static Dictionary<string, CultureInfo> Languages;
        public static CultureInfo CurrentLanguage;

        public static void InitializeDatabase()
        {

            ConnectionString = ConfigurationManager.ConnectionStrings["RestaurantDB"].ConnectionString;
            if (!File.Exists("RestaurantDB.sdf"))
            {
                SqlCeEngine sqlCeEngine = new SqlCeEngine(ConnectionString);
                sqlCeEngine.CreateDatabase();

                Dictionary<string, string> Tables = new Dictionary<string, string>();

                //Categories Table
                Tables.Add("Categories", @"CREATE  TABLE Categories (
                          Id NCHAR(38) NOT NULL DEFAULT newid(),
                          CategoryName NVARCHAR(45) NOT NULL ,
                          PRIMARY KEY (Id) );");
                //Foods Table
                Tables.Add("Foods", @"CREATE  TABLE Foods (
                          Id NCHAR(38) NOT NULL DEFAULT newid(),
                          Category_Id NCHAR(38) NOT NULL ,
                          FoodName NVARCHAR(64) NOT NULL ,
                          Price FLOAT NULL DEFAULT 0 ,
                          Enabled BIT DEFAULT 1,
                          PRIMARY KEY (Id) ,
                          CONSTRAINT fk_Foods_Categories1
                            FOREIGN KEY (Category_Id )
                            REFERENCES Categories (Id )
                            ON DELETE NO ACTION
                            ON UPDATE NO ACTION);");
                //Customers Table
                Tables.Add("Customers", @"CREATE  TABLE Customers (
                          Id NCHAR(38) NOT NULL DEFAULT newid(),
                          CustomerId INT NOT NULL ,
                          FirstName NVARCHAR(256) NOT NULL ,
                          LastName NVARCHAR(256) NOT NULL ,
                          PhoneNumber NVARCHAR(20)  NULL ,
                          Address NVARCHAR(1024) NULL ,
                          PRIMARY KEY (Id),
                          CONSTRAINT UQ_Customers_CustomerId UNIQUE (CustomerId));");
                //Orders Table
                Tables.Add("Orders", @"CREATE  TABLE Orders (
                          Id NCHAR(38) NOT NULL DEFAULT newid(),
                          Customer_Id NCHAR(38) NOT NULL ,
                          OrderDate DATETIME NULL ,
                          Discount TINYINT NULL ,
                          OrderType TINYINT NOT NULL ,
                          DeliveryFee FLOAT NOT NULL DEFAULT 0,
                          Status TINYINT NULL ,
                          PRIMARY KEY (Id) ,
                          CONSTRAINT fk_Orders_Customers1
                            FOREIGN KEY (Customer_Id )
                            REFERENCES Customers (Id )
                            ON DELETE NO ACTION
                            ON UPDATE NO ACTION);");
                //Foods_Orders Table
                Tables.Add("Foods_Orders", @"CREATE  TABLE Foods_Orders (
                          Food_Id NCHAR(38) NOT NULL ,
                          Order_Id NCHAR(38) NOT NULL ,
                          Quantity INT NULL ,
                          PRIMARY KEY (Food_Id, Order_Id) ,
                          CONSTRAINT fk_Foods_Orders_Foods
                            FOREIGN KEY (Food_Id )
                            REFERENCES Foods (Id )
                            ON DELETE NO ACTION
                            ON UPDATE NO ACTION,
                          CONSTRAINT fk_Foods_Orders_Orders1
                            FOREIGN KEY (Order_Id )
                            REFERENCES Orders (Id )
                            ON DELETE NO ACTION
                            ON UPDATE NO ACTION);");
                //Employees Table
                Tables.Add("Employees", @"CREATE  TABLE Employees (
                          Id NCHAR(38) NOT NULL DEFAULT newid(),
                          FirstName NVARCHAR(256) NOT NULL ,
                          LastName NVARCHAR(256) NOT NULL ,
                          Username NVARCHAR(256) NOT NULL ,
                          Password NVARCHAR(256) NOT NULL ,
                          Role TINYINT NOT NULL ,
                          PRIMARY KEY (Id) ,
                          CONSTRAINT UQ_Employees_Username UNIQUE (Username));");

                Database.Connect();
                foreach (KeyValuePair<string, string> table in Tables)
                {
                    if (!Database.TableExists(table.Key))
                        Database.ExecuteNonQuery(table.Value, false);
                }

                Database.Disconnect();
            }


        }
        public static void HandleSqlCeException(SqlCeException ex)
        {
            string FieldName = null;

            switch (ex.NativeError)
            {
                case 25005:  //null value
                    FieldName = ex.Errors[0].ErrorParameters[0].ToString();
                    MessageBox.Show(string.Format(Common.Words["FieldIsEmpty"], Common.Words[FieldName]), Common.Words["EmptyField_Title"], MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;

                case 25016:
                    FieldName = Regex.Match(ex.Message, string.Format(@"(?<=Constraint name = UQ_{0}_)(\w+)", ex.Errors[0].ErrorParameters[0].ToString())).Value;
                    MessageBox.Show(string.Format(Common.Words["DuplicateValue"], Common.Words[FieldName]), Common.Words["DuplicateValue_Title"], MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case 25025: MessageBox.Show(Common.Words["FoodBelognsToOrder"], Common.Words["Error"], MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                default: MessageBox.Show(string.Format(Common.Words["UnknownError"], ex.NativeError.ToString()), Common.Words["UnknownError_Title"], MessageBoxButtons.OK, MessageBoxIcon.Error); break;

            }
        }
        public static void Initialize()
        {

            InitializeDatabase();
            Common.Words = new Dictionary<string, string>();


            if (configFileParser == null)
            {

                if (ConfigurationManager.AppSettings.AllKeys.Contains("DefaultLanguage"))
                {
                    CurrentLanguage = new CultureInfo(ConfigurationManager.AppSettings["DefaultLanguage"]);
                    configFileParser = new ConfigFileParser(string.Format(@"Languages\{0}.ini", CurrentLanguage.Name));
                }

            }
            Words = configFileParser.getDictionary();

        }

        public static void ChangeLanguage(string languageName)
        {
            configFileParser = new ConfigFileParser(string.Format(@"Languages\{0}.ini", languageName));
            Words = configFileParser.getDictionary();
        }


        public static void ToggleRightToLeft(Control control, Action leftToRight = null, Action rightToLeft = null)
        {
            if (Common.Words["RightToLeft"] == "1")
            {

                control.RightToLeft = RightToLeft.Yes;
                if (rightToLeft != null)
                    rightToLeft();


            }
            else if (Common.Words["RightToLeft"] == "0")
            {

                control.RightToLeft = RightToLeft.No;
                if (leftToRight != null)
                    leftToRight();

            }

        }



        public static void ShowDeletePrompt(DataGridView dataGrid, Action DeleteCanceled = null)
        {
            foreach (DataGridViewRow row in (dataGrid as DataGridView).SelectedRows)
            {
                if (row.Index != dataGrid.NewRowIndex && row.Index != -1)
                {
                    DialogResult dialogResult = MessageBox.Show(string.Format(Common.Words["DeletePrompt"], (row.DataBoundItem as IBaseModel).ToString()), Common.Words["DeletePrompt_Title"], MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dialogResult == DialogResult.Yes)
                    {
                        try
                        {
                            (row.DataBoundItem as IBaseModel).Delete();
                        }
                        catch (SqlCeException ex)
                        {
                            HandleSqlCeException(ex);
                        }
                    }
                    else
                    {
                        if (DeleteCanceled != null)
                            DeleteCanceled();
                        continue;
                    }
                }
            }

        }



        public static void CommonDataGridCellBeginEdit(DataGridView dataGrid, DataGridViewCellCancelEventArgs e)
        {
            if (dataGrid.Rows[e.RowIndex].Tag == null && dataGrid.Rows[e.RowIndex].DataBoundItem != null)
            {
                dataGrid.Rows[e.RowIndex].Tag = (dataGrid.Rows[e.RowIndex].DataBoundItem as IBaseModel).Clone();
            }
        }
        public static void CommonDataGridSubmitEdit(IEnumerable<IBaseModel> list)
        {
            try
            {
                foreach (IBaseModel model in list.Where(model => model.State == ModelState.Editing))
                {
                    model.Edit();
                }
            }
            catch (SqlCeException ex)
            {
                Common.HandleSqlCeException(ex);
            }
        }
        public static void CommonDiscardChanges<EntityType>(IList<EntityType> list) where EntityType : IBaseModel
        {
            List<EntityType> tempModels = new List<EntityType>();
            foreach (EntityType model in list)
                tempModels.Add(model);


            foreach (EntityType model in tempModels.Where((model) => model.State == ModelState.Constructed
                                                                            || model.State == ModelState.Editing
                                                                            || model.State == ModelState.Constructing))
            {
                list.Remove(model);
            }
        }


        public static void CommonDataGridCellEndEdit<EntityType>(DataGridView dataGrid, DataGridViewCellEventArgs e, Action<EntityType> editModel) where EntityType : IBaseModel, new()
        {
            if (e.RowIndex != -1 && e.RowIndex != dataGrid.NewRowIndex)
            {
                if (dataGrid.Rows[e.RowIndex].Tag != null && (dataGrid.Rows[e.RowIndex].Tag as IBaseModel).State != ModelState.Constructing)
                    if (!(dataGrid.Rows[e.RowIndex].DataBoundItem as IBaseModel).Equals(dataGrid.Rows[e.RowIndex].Tag as IBaseModel))
                    {
                        (dataGrid.Rows[e.RowIndex].DataBoundItem as IBaseModel).State = ModelState.Editing;

                        if (ModelIsChanged != null)
                            ModelIsChanged(dataGrid, e);
                    }
                    else
                    {

                        (dataGrid.Rows[e.RowIndex].DataBoundItem as IBaseModel).State = (dataGrid.Rows[e.RowIndex].Tag as IBaseModel).State;

                        if (ModelIsNotChanged != null)
                            ModelIsNotChanged(dataGrid, e);
                    }

                //new
                if (dataGrid.CurrentRow.Tag != null)
                {

                    if ((dataGrid.CurrentRow.Tag as IBaseModel).State == ModelState.Constructing ||
                        (dataGrid.CurrentRow.DataBoundItem as IBaseModel).State == ModelState.Editing)
                    {

                        EntityType newModel = new EntityType();

                        if (((EntityType)(dataGrid.CurrentRow.Tag)).State == ModelState.Constructing)
                        {
                            newModel = (EntityType)dataGrid.CurrentRow.Tag;

                        }
                        else if ((dataGrid.CurrentRow.DataBoundItem as IBaseModel).State == ModelState.Editing)
                        {
                            newModel = (EntityType)dataGrid.CurrentRow.DataBoundItem;
                        }

                        editModel(newModel);
                    }

                }
            }
        }
 
 
        public static void PopulateLanguageMenu(ToolStripMenuItem languageMenu, Action InitializeLanguage)
        {
            if (Languages == null)
                Languages = new Dictionary<string, CultureInfo>();
            foreach (string languageFile in Directory.GetFiles("Languages"))
            {
                string lang = languageFile.Remove(languageFile.LastIndexOf('.')).Substring(languageFile.IndexOf("\\") + 1);
                if (!Languages.ContainsKey(lang))
                    Languages.Add(lang, new CultureInfo(lang));

                ToolStripMenuItem subMnuLanguage = new ToolStripMenuItem(Languages[lang].NativeName);
                subMnuLanguage.Tag = Languages[lang];
                subMnuLanguage.BackColor = System.Drawing.Color.FromArgb(255, 192, 128);

                subMnuLanguage.Click += delegate(object sender, EventArgs e)
                {
                    Common.ChangeLanguage(((sender as ToolStripMenuItem).Tag as CultureInfo).Name);
                    CurrentLanguage = (sender as ToolStripMenuItem).Tag as CultureInfo;

                    MainForm.InitializeLanguage();
                    AdministrationForm.InitializeLanguage();
                    InitializeLanguage();

                    if (ConfigurationManager.AppSettings.AllKeys.Contains("DefaultLanguage"))
                    {
                        Configuration configs = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                        configs.AppSettings.Settings["DefaultLanguage"].Value = CurrentLanguage.Name;
                        configs.Save(ConfigurationSaveMode.Modified);
                        ConfigurationManager.RefreshSection("appSettings");
                    }
                };

                languageMenu.DropDownItems.Add(subMnuLanguage);
            }
        }


        public static void AddDataGridViewColumn(DataGridView dataGrid, string fieldName)
        {
            DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn();
            column.HeaderText = Common.Words[fieldName];
            column.Name = fieldName;
            column.DataPropertyName = fieldName;
            dataGrid.Columns.Add(column);
        }
        public static void ShowErrorMessage(string errorMessage)
        {
            if (Common.Words.ContainsKey(errorMessage))
                MessageBox.Show(Common.Words[errorMessage], Common.Words["Invalid Input"], MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
                MessageBox.Show(errorMessage, Common.Words["Invalid Input"], MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        public static void ShowErrorMessage(Exception ex)
        {
            ShowErrorMessage(ex.Message);
        }
        public static bool IsActiveTab(TabControl tab, TabPage tabPage)
        {
            return tab.SelectedTab.Equals(tabPage);
        }
        public static void InitiailzeErrorHandlers(ref IBindingList list)
        {
            foreach (IBaseModel model in list)
            {
                model.Error += new ModelErrorEventHandler(delegate(object sender, ModelErrorEventArgs e)
                {
                    Common.ShowErrorMessage(e.Exception);
                });
            }
        }
        public static void InitializeList<T>(ref BindingList<T> list, ref T Controller) where T : IBaseModel<T>, new()
        {
            if (list == null)
                list = new BindingList<T>();
            if (Controller == null)
                Controller = new T();


            Controller.Error += new ModelErrorEventHandler(delegate(object sender, ModelErrorEventArgs e)
            {
                Common.ShowErrorMessage(e.Exception);
            });
            foreach (T model in Controller.ReadAll())
            {
                model.Error += new ModelErrorEventHandler(delegate(object sender, ModelErrorEventArgs e)
                {
                    Common.ShowErrorMessage(e.Exception);
                });
                list.Add(model);
            }
        }
        public static void ToggleControlsDisableEnable(DataGridView dataGrid, Action enable, Action disable)
        {
            foreach (DataGridViewRow row in dataGrid.SelectedRows)
                if (row.Index != dataGrid.NewRowIndex && row.Index != -1)
                    enable();
                else
                    disable();
        }
    }
}