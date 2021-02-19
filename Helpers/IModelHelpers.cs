using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers
{
    public interface IModelHelpers
    {
        object Fill(IDataReader dr);
        string PropertyToString();
    }
}
