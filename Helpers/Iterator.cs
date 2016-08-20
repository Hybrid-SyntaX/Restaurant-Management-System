using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
namespace Chocolatey.Iteratration
{
    public class Iterator
    {
        public static List<MemberInfo> GetMember(object baseMember, string memberName, BindingFlags flags)
        {
            return baseMember.GetType().GetMember(memberName, MemberTypes.All, flags).ToList<MemberInfo>();
        }
        public static List<MemberInfo> GetMember(object baseMember, string memberName)
        {
            var flags = BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance;
            return baseMember.GetType().GetMember(memberName, MemberTypes.All, flags).ToList<MemberInfo>();
        }
        public static Type getMemberType(MemberInfo member)
        {

            if (member is FieldInfo)
                return (member as FieldInfo).FieldType;
            else if (member is PropertyInfo)
                return (member as PropertyInfo).PropertyType;
            else if (member is MethodInfo)
                return (member as MethodInfo).ReturnType;
            else return null;
        }


        public static object GetMemberValue(object obj, MemberInfo member)
        {
            if (member is FieldInfo)
            {
                return (member as FieldInfo).GetValue(obj);
            }
            else if (member is PropertyInfo)
            {
                if ((member as PropertyInfo).GetValue(obj, null) != null)
                    return (member as PropertyInfo).GetValue(obj, null);

            }
            return null;
        }
        public static void SetMemberValue(object obj, MemberInfo member, object value)
        {
            if (member is FieldInfo)
            {
                if (value is Guid)
                    (member as FieldInfo).SetValue(obj, Guid.Parse(value.ToString()));
                else
                    (member as FieldInfo).SetValue(obj, value);
            }
            else if (member is PropertyInfo)
            {
                if ((member as PropertyInfo).GetSetMethod() != null)
                    if (getMemberType(member) == typeof(Guid))
                        (member as PropertyInfo).SetValue(obj, Guid.Parse(value.ToString()), null);
                    else
                    {
                        if ((getMemberType(member) == typeof(float)))
                            (member as PropertyInfo).SetValue(obj, Convert.ToSingle(value), null);
                        else
                            (member as PropertyInfo).SetValue(obj, value, null);

                        //(member as PropertyInfo).SetValue(entity, Convert.ChangeType(value, Convert.GetTypeCode(value)), null);

                    }

            }
        }
        public static void Copy(object obj1, object obj2)
        {
            if (obj2 != null)
            {
                foreach (MemberInfo member in obj2.GetType().GetMembers(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
                {
                    Iterator.SetMemberValue(obj1, member, Iterator.GetMemberValue(obj2, member));
                }
            }
        }
        public static bool IsEqual(object obj1, object obj2)
        {
            bool result = true;
            if ((obj1 != null && obj2 != null) && (obj1.GetType()==obj2.GetType()))
            {
                foreach (MemberInfo member in obj2.GetType().GetFields())
                {
                    if (Iterator.GetMemberValue(obj1, member) != null && Iterator.GetMemberValue(obj2, member) != null)
                        result &= Iterator.GetMemberValue(obj1, member).Equals(Iterator.GetMemberValue(obj2, member));
                }
                foreach (MemberInfo member in obj2.GetType().GetProperties())
                {
                    if (Iterator.GetMemberValue(obj1, member) != null && Iterator.GetMemberValue(obj2, member) != null)
                        result &= Iterator.GetMemberValue(obj1, member).Equals(Iterator.GetMemberValue(obj2, member));

                }
            }
            else return false;
            
            return result;
        }
        public static int GetHashCode(object obj)
        {
            int result = 0;
            

            return result;
           // int result
        }
        public  static object CreateInstance(Type type)
        {
            try
            {
                return type.Assembly.CreateInstance(type.FullName, true);
            }
            catch (TargetInvocationException ex)
            {
                throw ex.InnerException;
            }
        }
        public static object CreateClone(object obj)
        {
            object clone = Iterator.CreateInstance(obj.GetType());
            Iterator.Copy(clone, obj);

            return clone;
        }

    }
}
