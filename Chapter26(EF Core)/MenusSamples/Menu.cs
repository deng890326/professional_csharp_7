using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenusSamples
{
    [Table("Menus", Schema = "mc")]
    public class Menu
    {
        public int MenuId { get; }

        [MaxLength(50)]
        public string Text { get; set; }

        [Column(TypeName = "Money")]
        public decimal Price { get; set; }

        public int MenuCardId { get; set; }

        public MenuCard MenuCard { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }
}
