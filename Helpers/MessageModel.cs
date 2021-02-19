using Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers
{
    [Serializable]
    public class MessageModel : ModelHelpers
    {
        public string MessageCode { get; set; }
        public string MessageText { get; set; }
        public string HtmlContent { get; set; }
        public int TotalPageCount { get; set; }
    }
}
