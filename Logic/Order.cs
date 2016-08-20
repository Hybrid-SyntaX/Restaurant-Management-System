using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using Chocolatey.Iteratration;
using System.Data;
using System.Configuration;
using System.ComponentModel;
using Chocolatey.Validation;
using System.Data.SqlServerCe;
using Restaurant_Management_System;
namespace Logic
{
    //revision:5
    /// <summary>
    /// Represents an "Order" in the systmem
    /// </summary>
    public class Order : BaseModel<Order>
    {
        public Order()
        {
            //Save
            Database.DataAdapter.InsertCommand.CommandText = @"INSERT INTO Orders (Id,Customer_Id,DeliveryFee,OrderDate,OrderType,Discount,Status)  VALUES(@pId,@pCustomer_Id,@pDeliveryFee,@pOrderDate,@pOrderType,@pDiscount,@pStatus)";
            Database.InitializeParameters(Database.DataAdapter.InsertCommand.Parameters, "p", "Id", "Discount", "DeliveryFee", "OrderDate", "OrderType", "Customer_Id", "Status");

            //Edit
            Database.DataAdapter.UpdateCommand.CommandText = @"UPDATE Orders SET Discount=@pDiscount,DeliveryFee=@pDeliveryFee,Status=@pStatus,OrderType=@pOrderType,Customer_Id=@pCustomer_Id WHERE Id=@pId";
            Database.InitializeParameters(Database.DataAdapter.UpdateCommand.Parameters, "p", "Id", "Discount", "DeliveryFee", "OrderType", "Customer_Id", "Status");
        }



        #region Properties

        public BindingList<Food> Foods
        {
            get
            {
                if (_foods == null)
                    _foods = new BindingList<Food>();

                return _foods;
            }
            set
            {
                _foods = value;
                this.NotifyPropertyChanged("Foods");
            }
        }
        private BindingList<Food> _foods;


        public Customer Customer
        {
            get
            {
                if (_customer == null)
                {
                    _customer = new Customer();
                }
                return _customer;
            }
            set
            {
                _customer = value;
                this.NotifyPropertyChanged("Customer");
            }
        }
        private Customer _customer;



        public DateTime OrderDate
        {
            get
            {
                return _orderDate;
            }
            set
            {
                _orderDate = value;
                this.NotifyPropertyChanged("OrderDate");
            }
        }
        private DateTime _orderDate;


        public int Discount
        {
            get
            {
                return _discount;
            }
            set
            {
                _discount = value;
                this.NotifyPropertyChanged("Discount");
            }
        }
        private int _discount;

        public OrderType OrderType
        {
            get
            {
                return _orderType;
            }
            set
            {
                _orderType = value;
                this.NotifyPropertyChanged("OrderType");
            }
        }
        private OrderType _orderType;


        public OrderStatus Status
        {
            get
            {
                return _status;
            }
            set
            {
                _status = value;
                this.NotifyPropertyChanged("Status");
            }
        }
        private OrderStatus _status;


        public float DeliveryFee
        {
            get { return _deliveryFee; }
            set
            {
                _deliveryFee = value;
                this.NotifyPropertyChanged("DeliveryFee");
            }
        }
        private float _deliveryFee;
        #endregion

        #region Order Methods
        public double getTotalSales(Func<Order, bool> condition)
        {
            if (LocalDataSource == null)
                ReadAll();

            double sales = 0.0;
            if (LocalDataSource.Count > 0)
            {
                foreach (Order order in LocalDataSource.OfType<Order>().Where(condition))
                {
                    sales += order.TotalPrice;
                }
            }

            return sales;
        }
        public double getTotalHourlySales(int Year, int Month, int Day, int Hour)
        {
            return getTotalSales(order => order.OrderDate.Year == Year && order.OrderDate.Month == Month && order.OrderDate.Day == Day && order.OrderDate.Hour == Hour);
        }
        public double getTotalDailySales(int Year, int Month, int Day)
        {
            return getTotalSales(order => order.OrderDate.Year == Year && order.OrderDate.Month == Month && order.OrderDate.Day == Day);
        }
        public double getTotalAnnualSales(int Year)
        {
            return getTotalSales(order => order.OrderDate.Year == Year);

        }
        public double getTotalMonthlySales(int Year, int Month)
        {
            return getTotalSales(order => order.OrderDate.Month == Month && order.OrderDate.Year == Year);
        }

        public double RawPrice
        {
            get
            {
                double rawPrice = 0.0;

                foreach (Food food in Foods)
                    rawPrice += (food.Price * food.Quantity);

                rawPrice += DeliveryFee;

                return rawPrice;
            }
        }
        public double TotalPrice
        {
            get
            {
                return RawPrice - DiscountedPrice;
            }
        }
        public double DiscountedPrice
        {
            get
            {
                return (RawPrice * ((float)Discount / 100));
            }
        }
        public Order FirstOrder
        {

            get
            {
                if (LocalDataSource != null && LocalDataSource.Count > 0)
                {
                    Order firstOrder = LocalDataSource.OfType<Order>().First<Order>();
                    foreach (Order order in LocalDataSource)
                    {
                        if (order.OrderDate <= firstOrder.OrderDate)
                            firstOrder = order;
                    }
                    return firstOrder;
                }
                else return null;
            }
        }
        public Order LastOrder
        {
            get
            {
                Order lastOrder = null;
                if (LocalDataSource != null && LocalDataSource.Count > 0)
                {
                    lastOrder = LocalDataSource.OfType<Order>().First();
                    foreach (Order order in LocalDataSource)
                    {
                        if (order.OrderDate >= lastOrder.OrderDate)
                            lastOrder = order;
                    }
                    return lastOrder;
                }
                else return null;
            }
        }

        #endregion
        private static DataTable tblOrders;
        private static DataTable tblFoods;
        public override DataTable dataTable
        {
            get
            {
                if (tblOrders == null)
                    tblOrders = new DataTable("Orders");
                return tblOrders;
            }
            set
            {
                tblOrders = value;
            }
        }

        #region CRUD
        public override void Save()
        {
            base.Save();
            SaveFoods();
        }
        public override Order Read(DataRow dataRow)
        {
            this.Id = Guid.Parse(dataRow["Id"].ToString());
            this.Discount = int.Parse(dataRow["Discount"].ToString());
            this.DeliveryFee = float.Parse(dataRow["DeliveryFee"].ToString());
            this.OrderDate = DateTime.Parse(dataRow["OrderDate"].ToString());
            this.Status = (OrderStatus)int.Parse(dataRow["Status"].ToString());
            this.OrderType = (OrderType)int.Parse(dataRow["OrderType"].ToString());
            this.Customer = new Customer()
            {
                Id = Guid.Parse(dataRow["Customer_Id"].ToString())
            };

            this.dataRow = dataRow;

            ReadFoods();

            OnReadSuccessful();
            return this;
        }


        public override void Delete()
        {
            DeleteFoods();
            base.Delete();
        }
        #endregion


        #region Foods CRUD
        public void SaveFood(Food food)
        {

            DataRow newRow = tblFoods.NewRow();

            newRow["Food_Id"] = food.Id;
            newRow["Quantity"] = food.Quantity;
            newRow["Order_Id"] = this.Id;
            
            tblFoods.Rows.Add(newRow);

            Database.DataAdapter.InsertCommand.CommandText = @"INSERT INTO Foods_Orders (Food_Id,Order_Id,Quantity) 
                                                                VALUES(@pFood_Id,@pOrder_Id,@pQuantity)";

            Database.InitializeParameters(Database.DataAdapter.InsertCommand.Parameters, "p", "Food_Id", "Order_Id", "Quantity");

            Database.DataAdapter.Update(tblFoods);
        }
        public void SaveFoods()
        {
            InitializeFoodsTable();

            foreach (Food food in Foods)
            {


                DataRow newRow = tblFoods.NewRow();

                newRow["Food_Id"] = food.Id.ToString();
                newRow["Quantity"] = food.Quantity.ToString();
                newRow["Order_Id"] = this.Id;

                tblFoods.Rows.Add(newRow);
            }

            Database.DataAdapter.InsertCommand.CommandText = @"INSERT INTO Foods_Orders (Food_Id,Order_Id,Quantity) 
                                                                VALUES(@pFood_Id,@pOrder_Id,@pQuantity)";

            Database.InitializeParameters(Database.DataAdapter.InsertCommand.Parameters, "p", "Food_Id", "Order_Id", "Quantity");

            Database.DataAdapter.Update(tblFoods);


        }

        private static void InitializeFoodsTable()
        {
            if (tblFoods == null)
            {
                tblFoods = new DataTable("Foods");

                //Junction table
                tblFoods.Columns.Add("Food_Id");
                tblFoods.Columns.Add("Order_Id");
                tblFoods.Columns.Add("Quantity");

                //Food table
                tblFoods.Columns.Add("Id");
                tblFoods.Columns.Add("FoodName");
                tblFoods.Columns.Add("Price");
                tblFoods.Columns.Add("Enabled");
                tblFoods.Columns.Add("Category_Id");

                tblFoods.PrimaryKey = new DataColumn[] { tblFoods.Columns["Food_Id"], tblFoods.Columns["Order_Id"] };
            }
        }

        public List<Food> ReadFoods()
        {

            Foods.Clear();
            InitializeFoodsTable();

            Database.DataAdapter.SelectCommand.Parameters.Clear();
            Database.DataAdapter.SelectCommand.CommandText = @"SELECT Foods.*,Foods_Orders.* FROM Foods INNER JOIN Foods_Orders ON 
                                                                      (Foods.Id=Foods_Orders.Food_Id) INNER JOIN Orders ON 
                                                                          (Orders.Id=Foods_Orders.Order_Id) WHERE
                                                                              Orders.Id=@pId";

            Database.DataAdapter.SelectCommand.Parameters.Add(Database.CreateParameter("pId", "Id", Id.ToString()));

            Database.DataAdapter.Fill(tblFoods);


            foreach (DataRow dr in tblFoods.Select(string.Format("Order_Id='{0}'", Id.ToString())))
            {
                Food food = new Food();

                food.Read(dr);
                food.Quantity = int.Parse(dr["Quantity"].ToString());
                Foods.Add(food);

            }


            return Foods.ToList<Food>();
        }
        public void DeleteFood(Food food)
        {

            Database.DataAdapter.DeleteCommand.CommandText = "DELETE  FROM Foods_Orders WHERE Food_Id=@pFood_Id AND Order_Id=@pOrder_Id";
             Database.InitializeParameters(Database.DataAdapter.DeleteCommand.Parameters, "p", "Food_Id", "Order_Id");

            tblFoods.Rows.Find(new object[] { food.Id, Id }).Delete();

            Database.DataAdapter.Update(tblFoods);
        }
        public void DeleteFoods()
        {

            Database.DataAdapter.DeleteCommand.CommandText = "DELETE  FROM Foods_Orders WHERE Food_Id=@pFood_Id AND Order_Id=@pOrder_Id";
            Database.InitializeParameters(Database.DataAdapter.DeleteCommand.Parameters, "p", "Food_Id","Order_Id");
            foreach (Food food in Foods)
                tblFoods.Rows.Find(new object[] { food.Id, Id }).Delete();


            Database.DataAdapter.Update(tblFoods);

        }
        #endregion

        public override string ToString()
        {
            return Common.Words["Order"];
        }

        #region Validation
        public override bool Validate()
        {
            return Validate(this);
        }
        public override bool Validate(Order order)
        {
            bool result = true;

            result &= order.Customer.Validate();
            return result;
        }
        #endregion
        #region Fill
        public override void Fill(DataRow dr)
        {
            Fill(dr, this);
        }
        public override void Fill(DataRow dr, Order order)
        {
            dr["Id"] = order.Id.ToString();
            dr["Discount"] = order.Discount;
            dr["DeliveryFee"] = order.DeliveryFee;
            dr["Customer_Id"] = order.Customer.Id;
            dr["OrderDate"] = order.OrderDate;
            dr["Status"] = (int)order.Status;
            dr["OrderType"] = (int)order.OrderType;
        }
        #endregion
        #region Equals

        public override int GetHashCode()
        {
            return this.Customer.GetHashCode() ^ this.DeliveryFee.GetHashCode() ^ this.Discount.GetHashCode() ^ this.Id.GetHashCode() ^ this.OrderDate.GetHashCode() ^ this.OrderType.GetHashCode() ^ this.Status.GetHashCode();
        }
        public override bool Equals(object obj)
        {
            return Equals(obj as Order);
        }
        public override bool Equals(Order other)
        {
            bool result = true;
            if (other == null) return false;
            if (other.GetType() != this.GetType()) return false;

            result &= this.Customer.Equals(other.Customer);
            result &= this.DeliveryFee == other.DeliveryFee;
            result &= this.Discount == other.Discount;
            result &= this.Id.Equals(other.Id);
            result &= this.OrderDate.Equals(other.OrderDate);
            result &= this.OrderType.Equals(other.OrderType);
            result &= this.Status.Equals(other.Status);
            return result;
        }
        #endregion
    }
}
