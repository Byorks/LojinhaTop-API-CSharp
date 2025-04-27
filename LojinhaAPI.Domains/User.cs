using System;
using System.Collections.Generic;

namespace LojinhaAPI.Domains;

public partial class User
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public long TypeUserId { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual TypeUser TypeUser { get; set; } = null!;

    public User(string name, string email, long typeUserId)
    {
        Name = name;
        Email = email;
        TypeUserId = typeUserId;
    }

    public void  Update(string name, string email, long typeUserId)
    {
        Name = name;
        Email = email;
        TypeUserId = typeUserId;
    }
}
