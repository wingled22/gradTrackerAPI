using System;
using System.Collections.Generic;

namespace gradTrackerAPI.Entities;

public partial class Role
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public string? NormalizedName { get; set; }

    public string? ConcurrencyStamp { get; set; }
}
