using System;
using System.Collections.Generic;

namespace LojinhaAPI.Domains;

public partial class Order
{
    public long Id { get; set; }

    public long UserId { get; set; }

    public virtual ICollection<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();

    public virtual User User { get; set; } = null!;
}
