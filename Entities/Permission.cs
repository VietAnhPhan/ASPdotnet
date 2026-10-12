using System;
using System.Collections.Generic;

namespace WebApplication1.Entities;

public partial class Permission
{
    public int Id { get; set; }

    public string Code { get; set; } = null!;

    public string Module { get; set; } = null!;

    public string Name { get; set; } = null!;

    public virtual ICollection<Role> Roles { get; set; } = new List<Role>();
}
