using System;
using System.Collections.Generic;

namespace WebApplication1.Entities;

public partial class Role
{
    public int Id { get; set; }

    public string Code { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public bool IsSystem { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();

    public virtual ICollection<Permission> Permissions { get; set; } = new List<Permission>();
}
