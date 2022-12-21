using System;
using System.Collections.Generic;

namespace ScaffoldSample.Models;

public partial class Menu
{
    public int MenuId { get; set; }

    public string Text { get; set; } = null!;

    public decimal Price { get; set; }

    public int MenuCardId { get; set; }

    public virtual MenuCard MenuCard { get; set; } = null!;
}
