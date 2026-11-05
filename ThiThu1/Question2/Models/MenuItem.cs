using System;
using System.Collections.Generic;

namespace Question2.Models;

public partial class MenuItem
{
    public int MenuItemId { get; set; }

    public string ItemName { get; set; } = null!;

    public string ItemCode { get; set; } = null!;

    public decimal Price { get; set; }

    public int PreparationMinutes { get; set; }

    public int CategoryId { get; set; }

    public int KitchenId { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual Kitchen Kitchen { get; set; } = null!;
}
