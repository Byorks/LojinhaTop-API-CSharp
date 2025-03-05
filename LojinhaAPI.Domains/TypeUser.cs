using System;
using System.Collections.Generic;

namespace LojinhaAPI.Domains;

public partial class TypeUser
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
