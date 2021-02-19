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
        private string _statusCode = string.Empty;
        private string _statusDesc = string.Empty;
        private string _isSearch = string.Empty;
        private string _pageIndex = "1";
        private string _sortColumnName = string.Empty;
        private string _sortAscDesc = string.Empty;
        private string _cUDOperationType = string.Empty;

        public virtual object Fill(IDataReader dr)
        {
            if (dr != null)
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
                                        pInfo[i].SetValue(this, dr[colName].ToString());
                                    else if (pInfo[i].PropertyType == typeof(int) || pInfo[i].PropertyType == typeof(int?))
                                    {
                                        if (ValidationHelpers.IsInteger(dr[colName].ToString()))
                                            pInfo[i].SetValue(this, int.Parse(dr[colName].ToString()));
                                    }
                                    else if (pInfo[i].PropertyType == typeof(Int64) || pInfo[i].PropertyType == typeof(Int64?))
                                    {
                                        if (ValidationHelpers.IsInteger64(dr[colName].ToString()))
                                            pInfo[i].SetValue(this, Int64.Parse(dr[colName].ToString()));
                                    }
                                    else if (pInfo[i].PropertyType == typeof(decimal) || pInfo[i].PropertyType == typeof(decimal?))
                                    {
                                        if (ValidationHelpers.IsDecimal(dr[colName].ToString()))
                                            pInfo[i].SetValue(this, decimal.Parse(dr[colName].ToString()));
                                    }
                                    else if (pInfo[i].PropertyType == typeof(double) || pInfo[i].PropertyType == typeof(double?))
                                    {
                                        if (ValidationHelpers.IsDouble(dr[colName].ToString()))
                                            pInfo[i].SetValue(this, double.Parse(dr[colName].ToString()));
                                    }
                                    else if (pInfo[i].PropertyType == typeof(float) || pInfo[i].PropertyType == typeof(float?))
                                    {
                                        if (ValidationHelpers.IsDouble(dr[colName].ToString()))
                                            pInfo[i].SetValue(this, float.Parse(dr[colName].ToString()));
                                    }
                                    else if (pInfo[i].PropertyType == typeof(bool) || pInfo[i].PropertyType == typeof(bool?))
                                    {
                                        if (ValidationHelpers.IsBool(dr[colName].ToString()))
                                            pInfo[i].SetValue(this, bool.Parse(dr[colName].ToString()));
                                    }
                                    else if (pInfo[i].PropertyType == typeof(DateTime) || pInfo[i].PropertyType == typeof(DateTime?))
                                    {
                                        if (ValidationHelpers.IsDate(dr[colName].ToString()))
                                            pInfo[i].SetValue(this, DateTime.Parse(dr[colName].ToString()));
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return this;
        }

        public virtual string PropertyToString()
        {
            StringBuilder propString = new StringBuilder();
            PropertyInfo[] pInfo = this.GetType().GetProperties();
            for (int i = 0; i < pInfo.Length; i++)
            {
                propString.Append(pInfo[i].Name);
                propString.Append(CommonEnums.PropertyValueSeperator);
                object objVal = pInfo[i].GetValue(this);
                if (objVal != null)
                {
                    propString.Append(objVal.ToString());
                }
                else
                {
                    propString.Append("");
                }
                propString.Append(CommonEnums.PropertySeperator);
            }
            return propString.ToString();
        }

        public string StatusCode {
            get {
                if (!string.IsNullOrEmpty(_statusCode))
                    return _statusCode;
                else
                    return "";
            }
            set { _statusCode = value; }
        }

        [DatabaseColumnName(ColumnName = "active_desc")]
        public string StatusDesc
        {
            get
            {
                if (!string.IsNullOrEmpty(_statusDesc))
                    return _statusDesc;
                else
                    return "";
            }
            set { _statusDesc = value; }
        }

        public string IsSearch {
            get
            {
                if (!string.IsNullOrEmpty(_isSearch))
                    return _isSearch;
                else
                    return "";
            }
            set { _isSearch = value; }
        }
        public string PageIndex {
            get
            {
                if (!string.IsNullOrEmpty(_pageIndex))
                    return _pageIndex;
                else
                    return "1";
            }
            set { _pageIndex = value; }
        }
        public string SortColumnName
        {
            get
            {
                if (!string.IsNullOrEmpty(_sortColumnName))
                    return _sortColumnName;
                else
                    return "";
            }
            set { _sortColumnName = value; }
        }
        public string SortAscDesc
        {
            get
            {
                if (!string.IsNullOrEmpty(_sortAscDesc))
                    return _sortAscDesc;
                else
                    return "";
            }
            set { _sortAscDesc = value; }
        }
        public string CUDOperationType
        {
            get
            {
                if (!string.IsNullOrEmpty(_cUDOperationType))
                    return _cUDOperationType;
                else
                    return "";
            }
            set { _cUDOperationType = value; }
        }
    }
}
