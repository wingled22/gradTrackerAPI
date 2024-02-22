using System;
using System.Collections.Generic;

namespace gradTrackerAPI.Entities;

public partial class EmploymentHistory
{
    public int Id { get; set; }

    public string? CompanyName { get; set; }

    public string? Position { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }
}
