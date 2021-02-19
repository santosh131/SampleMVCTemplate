using Helpers;
using SampleDAL;
using SampleModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace SampleBO
{
    public class CommonBO
    {
        public List<LookupModel> GetRolesLookup(string optionCode)
        {
            return ConversionHelpers.ConvertToList<LookupModel>(new CommonDAL().GetRolesLookup(optionCode));
        }

        public List<LookupModel> GetMenusLookup(string optionCode)
        {
            return ConversionHelpers.ConvertToList<LookupModel>(new CommonDAL().GetMenusLookup(optionCode));
        }

        public List<LookupModel> GetCommonLookup(string fieldCode, string optionCode)
        {
            return ConversionHelpers.ConvertToList<LookupModel>(new CommonDAL().GetCommonFieldLookup(fieldCode,optionCode));
        }
    }
}
