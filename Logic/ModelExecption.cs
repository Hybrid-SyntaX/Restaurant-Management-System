using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Logic
{
   public  class ModelExecption:ApplicationException
    {
       public ModelExecption()
       {

       }
       public ModelExecption(string message):base(message)
       {

       }
    }
}
