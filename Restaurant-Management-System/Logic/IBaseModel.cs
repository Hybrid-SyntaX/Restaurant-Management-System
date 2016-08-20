using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Data;
using System.ComponentModel;
namespace Logic
{
    public interface IBaseModel : ICloneable, INotifyPropertyChanged
    {
        Guid Id { set; get; }
        ModelState State { set; get; }

        //CRUD
        void Save();
        void Save(DataRow dataRow);
        void Edit();
        void Delete();

        bool Validate();
        void Fill(DataRow dataRow);
        DataTable dataTable { set; get; }

        //Events
        event EventHandler SaveSuccessful;
        event EventHandler DeleteSuccessful;
        event EventHandler EditSuccessful;
        event EventHandler ReadSuccessful;
        event ModelErrorEventHandler Error;
    }
    public interface IBaseModel<T> : IBaseModel where T : IBaseModel, IBaseModel<T>, new()
    {
        T Read(DataRow dataRow);
        T Read(Guid Id);

        bool Validate(T model);
        void Fill(DataRow dr, T model);
        IEnumerable<T> ReadAll();
    }
    
}
