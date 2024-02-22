using System;
using System.Collections.Generic;

namespace gradTrackerAPI.Entities;

public partial class Alumnus
{
    public int Id { get; set; }

    public string? FirstName { get; set; }

    public string? MiddleName { get; set; }

    public string? LastName { get; set; }

    public string? Province { get; set; }

    public string? Municipality { get; set; }

    public string? Barangay { get; set; }

    public string? Street { get; set; }

    public string? Email { get; set; }

    public string? ContactNumber { get; set; }

    public string? Department { get; set; }

    public string? Program { get; set; }

    public DateTime? YearGraduated { get; set; }

    public DateTime? BirthDate { get; set; }

    public string? Sex { get; set; }
}
