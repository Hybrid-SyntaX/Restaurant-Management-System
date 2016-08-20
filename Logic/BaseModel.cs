using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Chocolatey.Data.Common;
using Chocolatey.Iteratration;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using Restaurant_Management_System;
using System.Collections;
using System.Data.SqlServerCe;
namespace Logic
{
    public class BaseModelComparer : IEqualityComparer<IBaseModel>
    {

        public bool Equals(IBaseModel x, IBaseModel y)
        {
            return x.Id == y.Id;
        }

        public int GetHashCode(IBaseModel obj)
        {

            return obj.GetHashCode();
        }

    }
    public class ModelErrorEventArgs : EventArgs
    {
        public Exception Exception { get; set; }
        public ModelErrorEventArgs() { }
        public ModelErrorEventArgs(Exception ex)
        {
            this.Exception = ex;
        }
    }

    public delegate void ModelErrorEventHandler(object sender, ModelErrorEventArgs e);

    /// <summary>
    /// Specifies model states
    /// </summary>
    public enum ModelState
    {
        Constructing = 0,
        Constructed = 1,
        Readng = 2,
        Read = 3,
        Saving = 4,
        Saved = 5,
        Editing = 6,
        Edited = 7,
        Deleting = 8,
        Deleted = 9,
    }
    public abstract class BaseModel<T> : IBaseModel<T>, IEquatable<T> where T : IBaseModel<T>, new()
    {
        #region Events

        public event EventHandler SaveSuccessful;
        public event EventHandler DeleteSuccessful;
        public event EventHandler EditSuccessful;
        public event EventHandler ReadSuccessful;
        public event ModelErrorEventHandler Error;
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnReadSuccessful(EventArgs e = null)
        {
            if (e == null) e = EventArgs.Empty;

            if (ReadSuccessful != null)
                ReadSuccessful(this, e);

            this.State = ModelState.Read;
        }
        protected void OnDeleteSuccessful(EventArgs e = null)
        {
            if (e == null) e = EventArgs.Empty;

            if (DeleteSuccessful != null)
                DeleteSuccessful(this, e);

            this.State = ModelState.Deleted;
        }
        protected void OnSaveSuccessful(EventArgs e = null)
        {
            if (e == null) e = EventArgs.Empty;

            if (SaveSuccessful != null)
                SaveSuccessful(this, e);

            this.State = ModelState.Saved;
        }
        protected void OnEditSuccessful(EventArgs e = null)
        {
            if (e == null) e = EventArgs.Empty;

            if (EditSuccessful != null)
                EditSuccessful(this, e);

            this.State = ModelState.Edited;
        }
        protected void OnError(Exception exception)
        {
            if (Error != null)
                Error(this, new ModelErrorEventArgs(exception));
        }
        protected void NotifyPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        #endregion

        public BaseModel()
        {
            Database.Initialize<SqlCeCommand, SqlCeConnection, SqlCeDataAdapter, SqlCeCommandBuilder>(Common.ConnectionString);
            State = ModelState.Constructed;
            
        }

        #region Properties

        private Guid _id;
        /// <summary>
        /// Entity's unique ID used to perform operations on data layer.
        /// </summary>
        public virtual Guid Id
        {
            set
            {
                _id = value;
                this.NotifyPropertyChanged("Id");
            }
            get
            {
                return _id;
            }
        }

        public ModelState State { set; get; }

        public BindingList<T> LocalDataSource;

        protected DataRow dataRow;

        #endregion
        private DatabaseHelper _database;
        protected DatabaseHelper Database
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
                }

                return _database;
            }
        }

        protected IEnumerable<T> getList(DataTable dataTable)
        {
            IList<T> models = new List<T>();
            
            foreach (DataRow dr in dataTable.Rows)
            {
                T model = new T();
                model.Read(dr);
                if (this.Error != null)
                    model.Error += this.Error;
                models.Add(model);
            }
            return models;
        }


        #region CRUD

        public virtual void Save()
        {

            DataRow newRow = dataTable.NewRow();

            this.Id = Guid.NewGuid();
            if (Validate())
            {

                Fill(newRow);

                dataTable.Rows.Add(newRow);
                Database.DataAdapter.Update(dataTable);

                OnSaveSuccessful();
            }
        }
        public void Save(DataRow dataRow)
        {
            this.Read(dataRow);
            this.Save();
        }
        public virtual T Read(Guid Id)
        {
            T model;

            if (LocalDataSource != null)
                model = (from T m in LocalDataSource where m.Id == Id select m).FirstOrDefault<T>();
            else
                model = (from T m in ReadAll().OfType<T>() where m.Id.Equals(Id) select m).FirstOrDefault<T>();

            Iterator.Copy(this, model);

            OnReadSuccessful();
            return model;
        }

        public virtual void Reload()
        {
            this.Read(this.Id);
        }
        public virtual IEnumerable<T> ReadAll()
        {
            dataTable = Database.getDataTable(dataTable.TableName);

            List<T> models = getList(dataTable).ToList();
            LocalDataSource = new BindingList<T>(models);

            return models;
        }
        public virtual void Edit()
        {
            T modifiedModel = new T();
            Iterator.Copy(modifiedModel, this);

            if (this.Read(Id) != null)
            {

                if (Validate(modifiedModel))
                {
                    Fill(dataRow, modifiedModel);
                }

                Database.DataAdapter.Update(dataTable);
                OnEditSuccessful();
            }
        }
        public virtual void Delete()
        {
            if (this.Read(Id) != null)
            {
                this.dataRow.Delete();

                Database.DataAdapter.DeleteCommand.CommandText = string.Format("DELETE  FROM {0} WHERE Id=@pId", dataTable.TableName);
                Database.InitializeParameters(Database.DataAdapter.DeleteCommand.Parameters, "p", "Id");

                Database.DataAdapter.Update(dataTable);

                OnDeleteSuccessful();
            }
        }
        #endregion

        /// <summary>
        /// Clones the current object
        /// </summary>
        /// <returns>object</returns>
        public virtual object Clone()
        {
            
            return Iterator.CreateClone(this);
        }



        /// <summary>
        /// Retrives the content of dataRow into the current object
        /// </summary>
        /// <param name="dataRow">datarow</param>
        /// <returns>T</returns>
        public abstract T Read(DataRow dataRow);
        public static T Parse(DataRow dr)
        {
            return new T().Read(dr);
        }

        public abstract bool Validate(T model);
        public abstract bool Validate();




        public abstract void Fill(DataRow dr);
        public abstract void Fill(DataRow dr, T model);

        public abstract DataTable dataTable { set; get; }

        public abstract bool Equals(T other);
        public override abstract bool Equals(object obj);
         
        public override abstract int GetHashCode();



    }
}
