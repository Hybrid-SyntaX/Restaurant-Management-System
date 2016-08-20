using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Data;
using Chocolatey.Iteratration;
using Restaurant_Management_System;
using System.ComponentModel;
using Chocolatey.Validation;
using System.Data.SqlServerCe;
namespace Logic
{
    //revision :3
    /// <summary>
    /// Represents a "Category" in the system
    /// </summary>
    public class Category : BaseModel<Category>
    {
        public Category()
        {
            //Save            
            Database.DataAdapter.InsertCommand.CommandText = @"INSERT INTO Categories (Id,CategoryName) VALUES(@pId,@pCategoryName)";
            Database.InitializeParameters(Database.DataAdapter.InsertCommand.Parameters, "p", "Id", "CategoryName");
            //Edit
            Database.DataAdapter.UpdateCommand.CommandText = @"UPDATE Categories SET CategoryName=@pCategoryName WHERE Id=@pId";
            Database.InitializeParameters(Database.DataAdapter.UpdateCommand.Parameters, "p", "Id", "CategoryName");
        }
        #region Properties

        public string CategoryName
        {
            get
            {
                return _categoryName;
            }
            set
            {
                if (Validator.IsAlphabetical(value))
                {
                    _categoryName = value;
                    this.NotifyPropertyChanged("CategoryName");
                }
                else
                {
                    OnError(new FormatException("Invalid Input(Alphabet)"));
                }
            }
        }
        private string _categoryName;

        public BindingList<Food> ReadFoods()
        {
            return new BindingList<Food>((from Food food in new Food().ReadAll() where food.Category.Id == this.Id select food).ToList());
        }

        #endregion

        private static DataTable tblCategories;

        #region CRUD
        public override Category Read(DataRow dataRow)
        {
            this.Id = Guid.Parse(dataRow["Id"].ToString());
            this.CategoryName = dataRow["CategoryName"].ToString();
            this.dataRow = dataRow;

            OnReadSuccessful();
            return this;
        }
        #endregion

        #region Misc

        public override int GetHashCode()
        {
            return Id.GetHashCode() ^ CategoryName.GetHashCode();
        }

        public override bool Equals(Category other)
        {
            if (other == null) return false;
            if (other.GetType() != this.GetType()) return false;
            return (
                    Id.Equals(other.Id) &&
                    CategoryName == other.CategoryName
                );
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Category);
        }
        public override bool Validate()
        {
            return Validate(this);
        }

        public override bool Validate(Category category)
        {

            bool result = true;

            result &= category.Id != Guid.Empty;
            result &= Validator.IsAlphabetical(category.CategoryName);
            return result;
        }
        public override void Fill(DataRow dr)
        {
            Fill(dr, this);
        }
        public override void Fill(DataRow dr, Category category)
        {

            dr["Id"] = category.Id.ToString();
            dr["CategoryName"] = category.CategoryName;

        }

        #endregion
        public override DataTable dataTable
        {

            get
            {
                if (tblCategories == null)
                    tblCategories = new DataTable("Categories");
                return tblCategories;
            }
            set
            {
                tblCategories = value;
            }
        }

    }
}
