using System;
using System.Collections.Generic;

namespace Question2.Models;

public partial class Kitchen
{
    public int KitchenId { get; set; }

    public string KitchenName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public virtual ICollection<MenuItem> MenuItems { get; set; } = new List<MenuItem>();
}
