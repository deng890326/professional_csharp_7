using System;
using System.Collections.Generic;

namespace ScaffoldSample.Models;

public partial class MenuCard
{
    public int MenuCardId { get; set; }

    public string Title { get; set; } = null!;

    public virtual ICollection<Menu> Menus { get; } = new List<Menu>();
}
