using System;
using System.Collections.Generic;

namespace gradTrackerAPI.Entities;

public partial class AspNetUser
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public string UserName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string UserType { get; set; } = null!;

    public string? NormalizedUserName { get; set; }

    public string? Email { get; set; }

    public string? NormalizedEmail { get; set; }

    public bool EmailConfirmed { get; set; }

    public string? PasswordHash { get; set; }

    public string? SecurityStamp { get; set; }

    public string? ConcurrencyStamp { get; set; }

    public string? PhoneNumber { get; set; }

    public bool PhoneNumberConfirmed { get; set; }

    public bool TwoFactorEnabled { get; set; }

    public DateTimeOffset? LockoutEnd { get; set; }

    public bool LockoutEnabled { get; set; }

    public int AccessFailedCount { get; set; }

    public virtual ICollection<UserClaim> UserClaims { get; } = new List<UserClaim>();

    public virtual ICollection<UserLogin> UserLogins { get; } = new List<UserLogin>();

    public virtual ICollection<UserToken> UserTokens { get; } = new List<UserToken>();

    public virtual ICollection<AspNetRole> Roles { get; } = new List<AspNetRole>();
}
