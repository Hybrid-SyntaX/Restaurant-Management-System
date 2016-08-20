using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

namespace Chocolatey.Comparision
{
    public class CollectionComparer : IEqualityComparer<ICollection>
    {

        public bool Equals(ICollection x, ICollection y)
        {
            bool result = true;
            foreach (object objx in x)
            {
                foreach (object objy in y)
                {
                    if (objx.Equals(objy))
                        result &= true;
                }
            }

            return result;
        }

        public int GetHashCode(ICollection obj)
        {

            return obj.GetHashCode();
        }
    }
}
