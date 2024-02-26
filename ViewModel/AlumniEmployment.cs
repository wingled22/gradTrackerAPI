using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gradTrackerAPI.ViewModel
{
    public class AlumniEmployment
    {
        public int Id { get; set; }

        public string? CompanyName { get; set; }

        public string? Position { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public int? AlumniId { get; set; }
    }
}