using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using Chocolatey.Iteratration;
using System.Configuration;
using System.Data.SqlServerCe;
using Restaurant_Management_System;
using System.Text.RegularExpressions;
using System.Data;
using Chocolatey.Validation;
using System.Security.Cryptography;
using System.ComponentModel;
namespace Logic
{
    //revision:6
    /// <summary>
    /// Represents an "Employee" in the system
    /// </summary>
    public class Employee : BaseModel<Employee>
    {

        public Employee()
        {
            //Save
            Database.DataAdapter.InsertCommand.CommandText = @"INSERT INTO Employees (Id,FirstName,LastName,Username,Password,Role)     VALUES(@pId,@pFirstName,@pLastName,@pUsername,@pPassword,@pRole)";
            Database.InitializeParameters(Database.DataAdapter.InsertCommand.Parameters, "p", "Id", "FirstName", "LastName", "Username", "Password", "Role");

            //Edit
            Database.DataAdapter.UpdateCommand.CommandText = @"UPDATE Employees SET FirstName=@pFirstName,LastName=@pLastName,Username=@pUsername,Password=@pPassword,Role=@pRole WHERE Id=@pId";
            Database.InitializeParameters(Database.DataAdapter.UpdateCommand.Parameters, "p", "Id", "FirstName", "LastName", "Username", "Password", "Role");
        }
        #region Properties
        public string FullName
        {
            get
            {
                return string.Format("{0} {1}", FirstName, LastName);
            }
        }
        public string Username
        {
            get
            {
                return _username;
            }
            set
            {
                if (value != null)
                {
                    _username = value;
                    this.NotifyPropertyChanged("Username");
                }
                else this.OnError(new Exception("Required Field"));
            }
        }
        private string _username;


        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                if (value != null)
                {
                    _password = value;
                    this.NotifyPropertyChanged("Password");
                }
                else this.OnError(new Exception("Required Field"));
            }
        }
        private string _password;

        public Role Role
        {
            get
            {
                return _role;

            }
            set
            {
                if (value == Logic.Role.Default)
                    value = Logic.Role.Cashier;

                _role = value;
                this.NotifyPropertyChanged("Role");
            }
        }
        private Role _role;

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
                else this.OnError(new FormatException("Invalid Input(Alphabet)"));

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
                else this.OnError(new FormatException("Invalid Input(Alphabet)"));
            }
        }
        private string _lastName;

        private static DataTable tblEmployees;
        public override DataTable dataTable
        {
            get
            {
                if (tblEmployees == null)
                    tblEmployees = new DataTable("Employees");
                return tblEmployees;
            }
            set
            {
                tblEmployees = value;
            }
        }

        #endregion

        #region Misc
        public BindingList<Employee> Search(string searchQuery)
        {
            return new BindingList<Employee>((from Employee employee in LocalDataSource
                                              where employee.FullName.ToLower().Contains(searchQuery)
                                              select employee).ToList<Employee>());
        }
        public Role Authenticate()
        {

            var result = from Employee employee in ReadAll()
                         where employee.Username.ToLower() == this.Username.ToLower() &&
                                employee.Password == Common.SHA1Hash(this.Password)
                         select employee;


            if (result.Count<Employee>() > 0)
            {
                Iterator.Copy(this, result.First<Employee>());
                return this.Role;
            }
            else
                return Role.Unauthorized;
        }
        public override string ToString()
        {
            return FullName;
        }
        #endregion

        #region CRUD
        public override Employee Read(DataRow dataRow)
        {
            this.Id = Guid.Parse(dataRow["Id"].ToString());
            this.FirstName = dataRow["FirstName"].ToString();
            this.LastName = dataRow["LastName"].ToString();
            this.Role = (Role)int.Parse(dataRow["Role"].ToString());
            this.Username = dataRow["Username"].ToString();
            this.Password = dataRow["Password"].ToString();
            this.dataRow = dataRow;

            OnReadSuccessful();
            return this;
        }
        #endregion


        public override int GetHashCode()
        {
            return this.Id.GetHashCode() ^ this.LastName.GetHashCode() ^ this.FirstName.GetHashCode() ^ this.LastName.GetHashCode() ^ this.FirstName.GetHashCode() ^ this.Username.GetHashCode() ^ this.Password.GetHashCode() ^ this.Role.GetHashCode();
        }
        public override bool Equals(object obj)
        {
            return Equals(obj as Employee);
        }
        public override bool Equals(Employee other)
        {
            if (other == null) return false;
            if (other.GetType() != this.GetType()) return false;
            return
                (
                    Id.Equals(other.Id) &&
                    FirstName == other.FirstName &&
                    LastName == other.LastName &&
                    Username == other.Username &&
                    Password == other.Password &&
                    Role.Equals(other.Role)
                );
        }

        public override bool Validate()
        {
            return Validate(this);
        }
        public override bool Validate(Employee employee)
        {

            bool result = true;

            result &= Validator.IsAlphabetical(employee.FirstName);
            result &= Validator.IsAlphabetical(employee.LastName);
            result &= !Validator.IsNullOrEmptyOrWhitespace(employee.Username);
            result &= !Validator.IsNullOrEmptyOrWhitespace(employee.Password);

            return result;
        }
        public override void Fill(DataRow dr)
        {
            Fill(dr, this);
        }
        public override void Fill(DataRow dr, Employee employee)
        {
            dr["Id"] = employee.Id.ToString();
            dr["FirstName"] = employee.FirstName;
            dr["LastName"] = employee.LastName;
            dr["Username"] = employee.Username;
            if (employee != this)
            {
                if (employee.Password != this.Password)
                    dr["Password"] = Common.SHA1Hash(employee.Password);
                else
                    dr["Password"] = employee.Password ;

            }
            else if (employee == this)
            {
                dr["Password"] = Common.SHA1Hash(employee.Password);
            }

            dr["Role"] = employee.Role;
        }


    }
}
