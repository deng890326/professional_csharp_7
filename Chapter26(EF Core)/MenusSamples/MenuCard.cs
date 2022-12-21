using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MenusSamples
{
    [Table("MenuCards", Schema = "mc")]
    public class MenuCard
    {
        public int MenuCardId { get; }

        [MaxLength(120)]
        public string Title { get; set; }
        public List<Menu> Menus { get; } = new List<Menu>();
        public override string ToString()
        {
            return Title;
        }
    }
}