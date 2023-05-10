using System;
using System.Collections.Generic;

namespace WebApplication4.Models;

public partial class Group
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public override string ToString()
    {
        return this.Name;
    }

    public string Description { get; set; } = null!;

    public virtual List<UserAcc> UserAccs { get; set; } = new List<UserAcc>();
}
