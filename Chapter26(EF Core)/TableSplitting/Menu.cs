using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableSplitting
{
    public class Menu
    {
        public int MenuId { get; }
        public string Title { get; set; }   
        public string SubTitle { get; set; }
        public decimal Price { get; set; }
        public MenuDetails Details { get; set; }
    }
}
