using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers
{
   public static class ValidationHelpers
    {
        public static bool IsInteger(string val)
        {
            try
            {
                int.Parse(val);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool IsInteger64(string val)
        {
            try
            {
                Int64.Parse(val);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool IsDecimal(string val)
        {
            try
            {
                decimal.Parse(val);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool IsDouble(string val)
        {
            try
            {
                double.Parse(val);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool IsFloat(string val)
        {
            try
            {
                float.Parse(val);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool IsDate(string val)
        {
            try
            {
                DateTime.Parse(val);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool IsBool(string val)
        {
            try
            {
                bool.Parse(val);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
