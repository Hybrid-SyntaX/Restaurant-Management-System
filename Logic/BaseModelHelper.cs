using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Chocolatey.Iteratration;
namespace Logic
{
    public class BaseModelHelper
    {
        public static IEnumerable<IBaseModel> List;
        public static IBaseModel Read(Guid Id)
        {
            IBaseModel model = null;
            
            if (List != null)
            {
                model = (from c in List where c.Id == Id select c).First<IBaseModel>();
            }
            else
                model = (from c in List where c.Id.Equals(Id) select c).First<IBaseModel>();
                
            return model;
        }

    }
}
