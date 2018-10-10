using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Helpers
{
    public class ModelHelpers : IModelHelpers
    {
        public virtual object Fill(IDataReader dr)
        {
            PropertyInfo[] pInfo = this.GetType().GetProperties();
            for (int i = 0; i < pInfo.Length; i++)
            {
                object[] attrs = pInfo[i].GetCustomAttributes(typeof(DatabaseColumnName), false);
                if (attrs != null && attrs.Length > 0)
                {
                    string colName = ((DatabaseColumnName)attrs[0]).ColumnName;
                    if (!string.IsNullOrEmpty(colName))
                    {
                        if (dr.IsColumnExists(colName))
                        {
                            if (dr[colName] != null && dr[colName] != DBNull.Value)
                            {
                                if (pInfo[i].PropertyType == typeof(string))
                                    pInfo[i].SetValue(this.GetType(), dr[colName].ToString());
                                else if (pInfo[i].PropertyType == typeof(int))
                                {
                                    if (ValidationHelpers.IsInteger(dr[colName].ToString()))
                                        pInfo[i].SetValue(this.GetType(), int.Parse(dr[colName].ToString()));
                                }
                                else if (pInfo[i].PropertyType == typeof(Int64))
                                {
                                    if (ValidationHelpers.IsInteger64(dr[colName].ToString()))
                                        pInfo[i].SetValue(this.GetType(), Int64.Parse(dr[colName].ToString()));
                                }
                                else if (pInfo[i].PropertyType == typeof(decimal))
                                {
                                    if (ValidationHelpers.IsDecimal(dr[colName].ToString()))
                                        pInfo[i].SetValue(this.GetType(), decimal.Parse(dr[colName].ToString()));
                                }
                                else if (pInfo[i].PropertyType == typeof(double))
                                {
                                    if (ValidationHelpers.IsDouble(dr[colName].ToString()))
                                        pInfo[i].SetValue(this.GetType(), double.Parse(dr[colName].ToString()));
                                }
                                else if (pInfo[i].PropertyType == typeof(float))
                                {
                                    if (ValidationHelpers.IsDouble(dr[colName].ToString()))
                                        pInfo[i].SetValue(this.GetType(), float.Parse(dr[colName].ToString()));
                                }
                                else if (pInfo[i].PropertyType == typeof(bool))
                                {
                                    if (ValidationHelpers.IsBool(dr[colName].ToString()))
                                        pInfo[i].SetValue(this.GetType(), bool.Parse(dr[colName].ToString()));
                                }
                                else if (pInfo[i].PropertyType == typeof(DateTime))
                                {
                                    if (ValidationHelpers.IsDate(dr[colName].ToString()))
                                        pInfo[i].SetValue(this.GetType(), DateTime.Parse(dr[colName].ToString()));
                                }
                            }
                        }
                    }
                }
            }

            return this;
        }
    }
}
