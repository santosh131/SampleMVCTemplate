using Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleModels
{
   public class MenuCategory:ModelHelpers
    {
        [DatabaseColumnName(ColumnName = "menu_category_id")]
        public string MenuCategoryId { get; set; }

        [DatabaseColumnName(ColumnName = "menu_category_name_txt")]
        public string MenuCategoryName { get; set; }
    }
}
