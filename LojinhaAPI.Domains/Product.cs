using System;
using System.Collections.Generic;

namespace LojinhaAPI.Domains;

public partial class Product
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();
}
