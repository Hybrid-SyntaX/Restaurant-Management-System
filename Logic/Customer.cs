using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Chocolatey.Iteratration;
using System.Data;
using System.Configuration;
using System.Data.SqlServerCe;
using System.Text.RegularExpressions;
using Chocolatey.Validation;
using System.Threading;
using Restaurant_Management_System;
using Chocolatey.Data.Common;
namespace Logic
{
    //revision:6
    /// <summary>
    /// Represenets a "Customer" in the system
    /// </summary>
    public class Customer : BaseModel<Customer>
    {
        public Customer()
        {
            //Save
            Database.DataAdapter.InsertCommand.CommandText = @"INSERT INTO Customers (Id,FirstName,LastName,Address,PhoneNumber,CustomerId)  VALUES(@pId,@pFirstName,@pLastName,@pAddress,@pPhoneNumber,@pCustomerId)";
            Database.InitializeParameters(Database.DataAdapter.InsertCommand.Parameters, "p", "Id", "FirstName", "LastName", "Address", "PhoneNumber", "CustomerId");
            //Edit
            Database.DataAdapter.UpdateCommand.CommandText = @"UPDATE Customers SET FirstName=@pFirstName,LastName=@pLastName,Address=@pAddress,PhoneNumber=@pPhoneNumber,CustomerId=@pCustomerId WHERE Id=@pId";
            Database.InitializeParameters(Database.DataAdapter.UpdateCommand.Parameters, "p", "Id", "FirstName", "LastName", "Address", "PhoneNumber", "CustomerId");
            
        }
        #region Properties
        public string FirstName
        {
            get
            {
                return _firstName;
            }
            set
            {

                if (Validator.IsAlphabetical(value))
                {
                    _firstName = value;
                    this.NotifyPropertyChanged("FirstName");
                }
                else
                {
                    this.OnError(new FormatException("Invalid Input(Alphabet)"));
                }

            }
        }
        private string _firstName;

        public string LastName
        {
            get
            {
                return _lastName;
            }
            set
            {
                if (Validator.IsAlphabetical(value))
                {
                    _lastName = value;
                    this.NotifyPropertyChanged("LastName");
                }
                else
                {

                    this.OnError(new FormatException("Invalid Input(Alphabet)"));
                }


            }
        }
        private string _lastName;


        public string PhoneNumber
        {
            get
            {

                return _phoneNumber;
            }
            set
            {
                if (Validator.IsInteger(value))
                {
                    _phoneNumber = value;
                    this.NotifyPropertyChanged("PhoneNumber");
                }
                else
                {

                    this.OnError(new FormatException("Invalid Input(Number)"));
                }
            }
        }
        private string _phoneNumber;


        public int CustomerId
        {
            get
            {
                if (_customerId == 0)
                    _customerId = UniqueID.NextId(getCustomerIds());

                return _customerId;

            }
            set
            {
                if (!UniqueID.IdExists(value))
                    _customerId = value;
                else
                    _customerId = UniqueID.NextId(getCustomerIds());


                this.NotifyPropertyChanged("CustomerId");
            }
        }
        private int _customerId;


        public string Address
        {
            get
            {
                return _address;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _address = value;
                    this.NotifyPropertyChanged("Address");
                }
                else this.OnError(new FormatException("Required Field"));
            }
        }
        private string _address;


        public string FullName
        {
            get { return string.Format("{0} {1}", FirstName, LastName); }
        }

        private BindingList<Order> _orders;
        public BindingList<Order> Orders
        {
            set
            {
                _orders = value;
            }
            get
            {
                if (_orders == null)
                    _orders = new BindingList<Order>();
                return _orders;
            }
        }

        #endregion
        public BindingList<Customer> Search(string searchQuery)
        {
            return new BindingList<Customer>((from Customer customer in LocalDataSource
                                              where customer.FullName.ToLower().Contains(searchQuery.ToLower())
                                                  || customer.PhoneNumber.ToLower().Contains(searchQuery.ToLower())
                                                  || customer.CustomerId.ToString().Contains(searchQuery)
                                              select customer).ToList());
        }




        #region CRUD
        public override Customer Read(DataRow dataRow)
        {
            this.Id = Guid.Parse(dataRow["Id"].ToString());
            this.CustomerId = int.Parse(dataRow["CustomerId"].ToString());
            this.FirstName = dataRow["FirstName"].ToString();
            this.LastName = dataRow["LastName"].ToString();
            this.Address = dataRow["Address"].ToString();
            this.PhoneNumber = dataRow["PhoneNumber"].ToString();
            this.dataRow = dataRow;

            this.ReadOrders();

            this.OnReadSuccessful();
            return this;
        }   
        public override void Delete()
        {
            DeleteOrders();
            base.Delete();
        }

        #endregion
        #region Orders
        public void DeleteOrders()
        {
            foreach (Order order in Orders)
            {
                order.DeleteFoods();
            }
           Database.ExecuteNonQuery(string.Format("DELETE  FROM Orders WHERE Customer_Id='{0}'",Id.ToString()),true);
        }


        private static Order _orderController;
        public BindingList<Order> ReadOrders()
        {
            if (_orderController == null)
                _orderController = new Order();

            foreach (Order order in  _orderController.ReadAll().Where(order => order.Customer.Id.Equals(this.Id)))
            {
                order.Customer = this;
                Orders.Add(order);
            }
            return Orders;
        }
        
        public BindingList<Order> ReadOrdersByStatus(OrderStatus status)
        {
            return FilterOrders(order => order.Status == status);
        }


        //Queries
        public BindingList<Order> ReadOrdersByType(OrderType type)
        {
            return FilterOrders(order => order.OrderType == type);
        }
        public BindingList<Order> FilterOrders(Func<Order, bool> condition)
        {
            return new BindingList<Order>(Orders.Where(condition).ToList<Order>());
        }

        #endregion
        #region Misc
        public override int GetHashCode()
        {
            return FirstName.GetHashCode() ^ LastName.GetHashCode() ^ CustomerId.GetHashCode() ^ Id.GetHashCode() ^ PhoneNumber.GetHashCode() ^ Address.GetHashCode();
        }
        public override bool Equals(object obj)
        {
            return Equals(obj as Customer);
        }
        public override bool Equals(Customer other)
        {
            if (other == null) return false;
            if (other.GetType() != this.GetType()) return false;

            return (
                    this.Id.Equals(other.Id) &&
                    FirstName == other.FirstName &&
                    LastName == other.LastName &&
                    CustomerId == other.CustomerId &&
                    PhoneNumber == other.PhoneNumber &&
                    Address == other.Address
                );
        }

        ICollection getCustomerIds()
        {
            return (from DataRow dr in tblCustomers.Rows select (int)dr["CustomerId"]).ToList();
        }

        public override bool Validate()
        {
            return Validate(this);
        }
        public override bool Validate(Customer customer)
        {
            bool result = true;

            result &= CustomerId != 0;
            result &= Validator.IsAlphabetical(FirstName);
            result &= Validator.IsAlphabetical(LastName);
            result &= !Validator.IsNullOrEmptyOrWhitespace(Address);
            result &= Validator.IsInteger(PhoneNumber);

            return result;
        }
        public override void Fill(DataRow dr)
        {
            Fill(dr, this);
        }
        public override void Fill(DataRow dr, Customer customer)
        {
        
            dr["Id"] = customer.Id.ToString();
            dr["CustomerId"] = customer.CustomerId;
            dr["FirstName"] = customer.FirstName;
            dr["LastName"] = customer.LastName;
            dr["Address"] = customer.Address;
            dr["PhoneNumber"] = customer.PhoneNumber;
        }
        public override string ToString()
        {
            return FullName;
        }
        #endregion

        private static DataTable tblCustomers;
        public override DataTable dataTable
        {
            get
            {
                if (tblCustomers == null)
                    tblCustomers = new DataTable("Customers");
                return tblCustomers;
            }
            set
            {
                tblCustomers = value;
            }
        }
    }
}