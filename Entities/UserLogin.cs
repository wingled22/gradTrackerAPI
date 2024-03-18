using System;
using System.Collections.Generic;

namespace gradTrackerAPI.Entities;

public partial class UserLogin
{
    public string LoginProvider { get; set; } = null!;

    public string ProviderKey { get; set; } = null!;

    public string? ProviderDisplayName { get; set; }

    public Guid UserId { get; set; }

    public virtual AspNetUser User { get; set; } = null!;
}
