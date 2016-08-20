using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Reflection;
using System.Data.Linq;
namespace Chocolatey.Data.Common
{

    public class DatabaseHelper
    {

        private DataContext dataContext;

        public DataContext DataContext
        {
            get { return dataContext; }
            set { dataContext = value; }
        }

        private static DataSet dataSet;

        public static DataSet DataSet
        {
            get
            {
                if (dataSet == null)
                    dataSet = new DataSet("DatabaseHelperDS");
                return dataSet;
            }
            set { dataSet = value; }
        }

        private DbCommand command;

        public DbCommand Command
        {
            get { return command; }
            set { command = value; }
        }
        private DbConnection connection;
        public DbConnection Connection
        {
            get { return connection; }
            set { connection = value; }
        }
        private DbDataAdapter dataAdapter;
        public DbDataAdapter DataAdapter
        {
            set
            {
                dataAdapter = value;
            }
            get
            {
                return dataAdapter;
            }
        }
        private DbCommandBuilder commandBuilder;

        public DbCommandBuilder CommandBuilder
        {
            get { return commandBuilder; }
            set { commandBuilder = value; }
        }
        public DatabaseHelper()
        {
            DataSet = new DataSet();
        }
        public void Initialize<TypedCommand, TypedConnection, TypedDataAdapter, TypedCommandBuilder>(string connectionString = null)
            where TypedCommand : DbCommand, new()
            where TypedConnection : DbConnection, new()
            where TypedDataAdapter : DbDataAdapter, new()
            where TypedCommandBuilder : DbCommandBuilder, new()
        {

            this.DataAdapter = new TypedDataAdapter();
            this.Connection = new TypedConnection();

            this.Connection.ConnectionString = connectionString;
            this.Command = new TypedCommand();
            this.Command.Connection = this.Connection;

#if true

            this.DataAdapter.InsertCommand = new TypedCommand();
            this.DataAdapter.InsertCommand.Connection = this.Connection;

            this.DataAdapter.SelectCommand = new TypedCommand();
            this.DataAdapter.SelectCommand.Connection = this.Connection;

            this.DataAdapter.DeleteCommand = new TypedCommand();
            this.DataAdapter.DeleteCommand.Connection = this.Connection;

            this.DataAdapter.UpdateCommand = new TypedCommand();
            this.DataAdapter.UpdateCommand.Connection = this.Connection;

#else
            this.DataAdapter.InsertCommand = Command;
            this.DataAdapter.SelectCommand = Command;
            this.DataAdapter.DeleteCommand = Command;
            this.DataAdapter.UpdateCommand = Command;
#endif
            this.CommandBuilder = new TypedCommandBuilder();
            this.CommandBuilder.DataAdapter = DataAdapter;

        }
        public DbParameter CreateParameter(string name, string sourceColumn, object value = null)
        {

            DbParameter parameter = this.Command.CreateParameter();

            parameter.ParameterName = name;
            parameter.SourceColumn = sourceColumn;
            parameter.Value = value;

            return parameter;
        }
        public DbParameter CreateParameter(string name, int size, string sourceColumn, object value = null)
        {

            DbParameter parameter = this.Command.CreateParameter();

            parameter.ParameterName = name;
            parameter.Size = size;
            parameter.SourceColumn = sourceColumn;
            parameter.Value = value;

            return parameter;
        }
        public DbParameter CreateParameter(string name, DbType dataType, int size, string sourceColumn, object value = null)
        {


            DbParameter parameter = this.Command.CreateParameter();

            parameter.ParameterName = name;
            parameter.DbType = dataType;
            parameter.Size = size;
            parameter.SourceColumn = sourceColumn;
            parameter.Value = value;

            return parameter;
        }
        public bool TableExists(string tableName)
        {
            //this.Command.CommandText=@"SELECT table_name FROM information_schema.tables WHERE table_name = '"+tableName+"';";
            // this.Command.CommandText = @"SELECT count(*) FROM "+tableName;
            //this.Command.CommandText = "IF " + tableName + " EXISTS";
            this.Command.CommandText = "SELECT count(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = '" + tableName + "'";


            if ((int)this.Command.ExecuteScalar() != 0)
                return true;
            return false;
        }
        public void Connect()
        {

            if (this.Connection.State != ConnectionState.Open)
                this.Connection.Open();
        }
        public void Disconnect()
        {
            if (this.Connection.State != ConnectionState.Closed)
                this.Connection.Close();
        }


        public Dictionary<Type, object> DataTypes { set; get; }
        public object ConvertToSqlDataType(MemberInfo member)
        {
            if (member is PropertyInfo)
                return ConvertToSqlDataType(member as PropertyInfo);
            else
                return ConvertToSqlDataType(member as FieldInfo);

        }
        public object ConvertToSqlDataType(FieldInfo field)
        {
            if (field != null)
                if (DataTypes.ContainsKey(field.FieldType))
                {
                    return DataTypes[field.FieldType];
                }

            return null;
        }
        public object ConvertToSqlDataType(PropertyInfo property)
        {
            if (DataTypes.ContainsKey(property.PropertyType))
            {
                return DataTypes[property.PropertyType];
            }
            else return null;
        }
        public DataTable getDataTable(string tableName)
        {
            DataTable dataTable = new DataTable(tableName);
            this.DataAdapter.SelectCommand.CommandText = string.Format("SELECT * FROM {0}", tableName);


            this.DataAdapter.Fill(dataTable);
            return dataTable;
        }
        public void SyncronizeDataTable(DataTable dataTable)
        {
            if (dataTable.Rows.Count == 0)
            {
                this.DataAdapter.Fill(dataTable);

            }
            else
            {
                DataTable dataTableChanges = dataTable.GetChanges(DataRowState.Added | DataRowState.Modified);
                if (dataTableChanges != null)
                    this.DataAdapter.Update(dataTableChanges);
                else
                    this.DataAdapter.Update(dataTable);
            }
        }
        public void ExecuteNonQuery(string statement, bool connect = true)
        {
            if (connect)
                this.Connect();

            Command.CommandText = statement;
            Command.ExecuteNonQuery();

            if (connect)
                this.Disconnect();
        }
        public void InitializeParameters(DbParameterCollection paramteres, params  string[] fields)
        {
            InitializeParameters(paramteres, null, fields);
        }
        public void InitializeParameters(DbParameterCollection paramteres, string prefix, params string[] fields)
        {
            paramteres.Clear();
            foreach (string field in fields)
            {
                string paramName = prefix + field;
                string filedName = field;
                paramteres.Add(CreateParameter(paramName, filedName));
            }
        }
    }
}
