using System;
using System.Collections.Generic;

namespace LojinhaAPI.Models;

public partial class User
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public long TypeUserId { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual TypeUser TypeUser { get; set; } = null!;
}
