using System;
using System.Collections.Generic;

namespace LojinhaAPI.Domains;

public partial class OrderProduct
{
    public long Id { get; set; }

    public long OrderId { get; set; }

    public long ProductId { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
