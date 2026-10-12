using System;
using System.Collections.Generic;

namespace WebApplication1.Entities;

public partial class UserSession
{
    public long Id { get; set; }

    public int UserId { get; set; }

    public string RefreshTokenHash { get; set; } = null!;

    public string? UserAgent { get; set; }

    public string? IpAddress { get; set; }

    public DateTime ExpiresAt { get; set; }

    public DateTime? RevokedAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual User User { get; set; } = null!;
}
