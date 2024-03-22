using System;
using System.Collections.Generic;

namespace gradTrackerEntities.Entities;

public partial class AspNetRole
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public string? NormalizedName { get; set; }

    public string? ConcurrencyStamp { get; set; }

    public virtual ICollection<RoleClaim> RoleClaims { get; } = new List<RoleClaim>();

    public virtual ICollection<AspNetUser> Users { get; } = new List<AspNetUser>();
}
