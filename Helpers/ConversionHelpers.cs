using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Helpers
{
    public static class ConversionHelpers
    {        
        public static List<T> ConvertToList<T>(IDataReader dr)
        {
           
            List<T> lst = Activator.CreateInstance<List<T>>();

            T t1 = Activator.CreateInstance<T>();
            MethodInfo methodInfo = t1.GetType().GetMethod("Fill", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);

            ParameterInfo[] parameters = methodInfo.GetParameters();

            while (dr.Read())
            { 
                T t = Activator.CreateInstance<T>();
                
                dynamic result = null;
                if (parameters.Length == 0)
                {
                    result = methodInfo.Invoke(t, null);
                    lst.Add(result);
                }
                else
                {
                    object[] parametersArray = new object[] { dr};                            
                    result = methodInfo.Invoke(t, parametersArray);
                    lst.Add(result);
                }
            }
            return lst;
        }
    }
}
