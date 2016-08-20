using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Chocolatey.Data.Common;
using Chocolatey.Iteratration;
using Chocolatey.Validation;
using System.Data.SqlServerCe;
using System.Data;
using System.Reflection;
using System.ComponentModel;
using Restaurant_Management_System;
namespace Logic
{
    //revision :4
    /// <summary>
    /// Represents a "Food" in the system
    /// </summary>
    public class Food : BaseModel<Food>
    {
        public Food()
        {
            //Save
            Database.DataAdapter.InsertCommand.CommandText = @"INSERT INTO Foods (Id,FoodName,Price,Category_Id,Enabled)    VALUES(@pId,@pFoodName,@pPrice,@pCategory_Id,@pEnabled)";
            Database.InitializeParameters(Database.DataAdapter.InsertCommand.Parameters, "p", "Id", "FoodName", "Price", "Category_Id", "Enabled");

            //Edit
            Database.DataAdapter.UpdateCommand.CommandText = @"UPDATE Foods SET FoodName=@pFoodName,Price=@pPrice,Category_Id=@pCategory_Id,Enabled=@pEnabled WHERE Id=@pId";
            Database.InitializeParameters(Database.DataAdapter.UpdateCommand.Parameters, "p", "Id", "FoodName", "Price", "Category_Id", "Enabled");

        }
        #region Properties
        public string FoodName
        {
            get
            {
                return _foodName;
            }
            set
            {

                if (Validator.IsAlphabetical(value))
                {
                    _foodName = value;
                    this.NotifyPropertyChanged("FoodName");
                }
                else
                {
                    this.OnError(new FormatException("Invalid Input(Alphabet)"));
                }
            }
        }
        private string _foodName;
        
        public float Price
        {
            get
            {
                return _price;
            }
            set
            {

                _price = value;
                this.NotifyPropertyChanged("Price");

            }
        }
        private float _price;

        public Category Category
        {
            get
            {
                if (_category == null)
                    _category = new Category();
                return _category;
            }
            set
            {
                _category = value;
                this.NotifyPropertyChanged("Category");
            }
        }
        private Category _category;


        public int Quantity
        {
            get
            {
                if (_quantity == 0)
                    _quantity = 1;

                return _quantity;
            }
            set
            {
                _quantity = value;
                this.NotifyPropertyChanged("Quantity");
            }
        }
        private int _quantity;

        public bool Enabled
        {
            get
            {
                return _enabled;
            }
            set
            {
                _enabled = value;
            }
        }
        public bool _enabled;
        #endregion

        public BindingList<Food> Search(string searchQuery,Category category=null)
        {
            
            if (category != null && !category.Id.Equals(Guid.Empty))
            {
                return new BindingList<Food>((from Food food in LocalDataSource
                                              where food.FoodName != null && food.FoodName.ToLower().Contains(searchQuery.ToLower()) && food.Category.Equals(category)
                                              select food).ToList());
            }
            else
            {
                return new BindingList<Food>((from Food food in LocalDataSource
                                              where food.FoodName != null && food.FoodName.ToLower().Contains(searchQuery.ToLower())
                                              select food).ToList());
            }
        }
        public BindingList<Food> ReadByCategory(Category category)
        {

            if (category != null)
            {
                if (category.Id != Guid.Empty)
                {
                    return new BindingList<Food>((from Food food in LocalDataSource where food.Category.Id == category.Id select food).ToList());
                }
                else return LocalDataSource;
            }
            else return null;

        }

        private static DataTable tblFoods;
        #region CRUD
        public override Food Read(DataRow dataRow)
        {
            this.Id = Guid.Parse(dataRow["Id"].ToString());
            this.FoodName = dataRow["FoodName"].ToString();
            this.Price = Sanitizer.SanitizeFloat(dataRow["Price"].ToString());
            this.Enabled = Sanitizer.SanitizeBool(dataRow["Enabled"], true);
            this.Category = new Category().Read(Guid.Parse(dataRow["Category_Id"].ToString()));
            this.dataRow = dataRow;

            OnReadSuccessful();
            return this;
        } 
        #endregion



        public void IncreaseQuantity(Order order)
        {
            Database.Connect();

            Database.DataAdapter.UpdateCommand.CommandText = "UPDATE Foods_Orders SET Quantity=@pQuantity WHERE Food_Id=@pFood_Id AND Order_Id=@pOrder_Id";


            Database.InitializeParameters(Database.DataAdapter.UpdateCommand.Parameters, "p", "Order_Id", "Food_Id", "Quantity");
            Database.DataAdapter.UpdateCommand.Parameters["pOrder_ID"].Value = order.Id.ToString();
            Database.DataAdapter.UpdateCommand.Parameters["pFood_Id"].Value = this.Id.ToString();
            Database.DataAdapter.UpdateCommand.Parameters["pQuantity"].Value = ++Quantity;

            Database.DataAdapter.UpdateCommand.ExecuteNonQuery();

            Database.Disconnect();
        }
        public void DecreaseQuantity(Order order)
        {
            Database.Connect();

            Database.DataAdapter.UpdateCommand.CommandText = "UPDATE Foods_Orders SET Quantity=@pQuantity WHERE Food_Id=@pFood_Id AND Order_Id=@pOrder_Id";

            Database.InitializeParameters(Database.DataAdapter.UpdateCommand.Parameters, "p", "Order_Id", "Food_Id", "Quantity");
            Database.DataAdapter.UpdateCommand.Parameters["pOrder_ID"].Value = order.Id.ToString();
            Database.DataAdapter.UpdateCommand.Parameters["pFood_Id"].Value = this.Id.ToString();
            Database.DataAdapter.UpdateCommand.Parameters["pQuantity"].Value = --Quantity;

            Database.DataAdapter.UpdateCommand.ExecuteNonQuery();

            Database.Disconnect();
        }

        public override string ToString()
        {
            return FoodName;
        }


        public override int GetHashCode()
        {
            return
                this.FoodName.GetHashCode() ^ this.Price.GetHashCode() ^ this.Id.GetHashCode() ^ this.Category.GetHashCode();
        }
        public override bool Equals(Food other)
        {
            if (other == null) return false;
            if (this.GetType() != other.GetType()) return false;
            return (
                Id.Equals(other.Id) &&
                Category.Equals(other.Category) &&
                FoodName == other.FoodName &&
                Price == other.Price &&
                Enabled == other.Enabled
                );
        }
        
        public override bool Equals(object obj)
        {
            return Equals(obj as Food);
        }
        public override bool Validate()
        {
            return Validate(this);
        }
       
        public override bool Validate(Food food)
        {
            bool result = true;

            result &= Validator.IsAlphabetical(food.FoodName);
            result &= Category.Validate();
            result &= Price != 0.0f;


            return result;
        }
        public override void Fill(DataRow dr)
        {
            Fill(dr, this);
        }
        public override void Fill(DataRow dr, Food food)
        {
            dr["Id"] = food.Id.ToString();
            dr["FoodName"] = food.FoodName;
            dr["Price"] = food.Price;
            dr["Category_Id"] = food.Category.Id;
            dr["Enabled"] = food.Enabled;

        }

        public override DataTable dataTable
        {
            get
            {
                if (tblFoods == null)
                    tblFoods = new DataTable("Foods");
                return tblFoods;
            }
            set
            {
                tblFoods = value;
            }
        }
    }
}