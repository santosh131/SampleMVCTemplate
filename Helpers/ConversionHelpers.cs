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
        public static List<T> ConvertToList<T>(IDataReader dr, bool isClosed = true)
        {
            List<T> lst = Activator.CreateInstance<List<T>>();
            T t1 = Activator.CreateInstance<T>();
            MethodInfo methodInfo = t1.GetType().GetMethod("Fill", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
            ParameterInfo[] parameters = methodInfo.GetParameters();
            if (dr != null)
            {
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
                        object[] parametersArray = new object[] { dr };
                        result = methodInfo.Invoke(t, parametersArray);
                        lst.Add(result);
                    }
                }
                if (isClosed)
                {
                    if (!dr.IsClosed)
                        dr.Close();
                }
            }
            return lst;
        }

        public static List<T> ConvertToList<T>(string text)
        {
            List<T> lst = Activator.CreateInstance<List<T>>();
            string[] splittedRows = text.Split(new string[] { CommonEnums.ListRecordSeperatorSplitter }, StringSplitOptions.None);
            foreach (var item in splittedRows)
            {
                if (!string.IsNullOrEmpty(item))
                {
                    T t = Activator.CreateInstance<T>();
                    string[] splittedProps = item.Split(new string[] { CommonEnums.PropertySeperator }, StringSplitOptions.None);
                    for (int i = 0; i < splittedProps.Length; i++)
                    {
                        if (!string.IsNullOrEmpty(splittedProps[i]))
                        {
                            string[] splittedPropsValue = splittedProps[i].Split(new string[] { CommonEnums.PropertyValueSeperator }, StringSplitOptions.None);
                            PropertyInfo pInfo = t.GetType().GetProperty(splittedPropsValue[0]);
                            if (pInfo != null)
                            {
                                if (pInfo.PropertyType == typeof(string))
                                    pInfo.SetValue(t, splittedPropsValue[1].ToString());
                                else if (pInfo.PropertyType == typeof(int))
                                {
                                    if (ValidationHelpers.IsInteger(splittedPropsValue[1].ToString()))
                                        pInfo.SetValue(t, int.Parse(splittedPropsValue[1].ToString()));
                                }
                                else if (pInfo.PropertyType == typeof(Int64))
                                {
                                    if (ValidationHelpers.IsInteger64(splittedPropsValue[1].ToString()))
                                        pInfo.SetValue(t, Int64.Parse(splittedPropsValue[1].ToString()));
                                }
                                else if (pInfo.PropertyType == typeof(decimal))
                                {
                                    if (ValidationHelpers.IsDecimal(splittedPropsValue[1].ToString()))
                                        pInfo.SetValue(t, decimal.Parse(splittedPropsValue[1].ToString()));
                                }
                                else if (pInfo.PropertyType == typeof(double))
                                {
                                    if (ValidationHelpers.IsDouble(splittedPropsValue[1].ToString()))
                                        pInfo.SetValue(t, double.Parse(splittedPropsValue[1].ToString()));
                                }
                                else if (pInfo.PropertyType == typeof(float))
                                {
                                    if (ValidationHelpers.IsDouble(splittedPropsValue[1].ToString()))
                                        pInfo.SetValue(t, float.Parse(splittedPropsValue[1].ToString()));
                                }
                                else if (pInfo.PropertyType == typeof(bool))
                                {
                                    if (ValidationHelpers.IsBool(splittedPropsValue[1].ToString()))
                                        pInfo.SetValue(t, bool.Parse(splittedPropsValue[1].ToString()));
                                }
                                else if (pInfo.PropertyType == typeof(DateTime))
                                {
                                    if (ValidationHelpers.IsDate(splittedPropsValue[1].ToString()))
                                        pInfo.SetValue(t, DateTime.Parse(splittedPropsValue[1].ToString()));
                                }
                            }
                        }
                    }
                    lst.Add(t);
                }
            }
            return lst;
        } 
    }
}
