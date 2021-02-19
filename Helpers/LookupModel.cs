using Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers
{
    [Serializable]
    public class LookupModel : ModelHelpers
    {
        [DatabaseColumnName(ColumnName = "id_txt")]
        public string Id { get; set; }

        [DatabaseColumnName(ColumnName = "code_txt")]
        public string Code { get; set; }

        [DatabaseColumnName(ColumnName = "desc_txt")]
        public string Description { get; set; }
    }
}
