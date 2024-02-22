using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using gradTrackerAPI.Entities;

namespace gradTrackerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlumniController : ControllerBase
    {
        private readonly GradTrackerContext _context;

        public AlumniController(GradTrackerContext context)
        {
            _context = context;
        }
    }
}