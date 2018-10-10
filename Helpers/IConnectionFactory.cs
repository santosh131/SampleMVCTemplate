using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers
{
    public interface IConnectionFactory
    {
        ICustomConnection GetConnection(string connString, CommonEnums.DataBases ce);
        ICustomConnection GetConnection(CommonEnums.DataBases ce);
    }
}
