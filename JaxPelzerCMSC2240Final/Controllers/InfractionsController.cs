using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JaxPelzerCMSC2240Final.Data;
using JaxPelzerCMSC2240Final.Models;
using Microsoft.AspNetCore.Authorization;

namespace JaxPelzerCMSC2240Final.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InfractionsController : ControllerBase
    {
        private readonly finalDatabaseContext _context;
        
        public InfractionsController(finalDatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Infractions
        [Authorize(Roles = "POLICE" + "," + "DMV")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Infraction>>> GetInfractions()
        {
          if (_context.Infractions == null)
          {
              return NotFound();
          }
            return await _context.Infractions.ToListAsync();
        }

        // GET: api/Infractions/5
        [Authorize(Roles = "POLICE" + "," + "DMV")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Infraction>> GetInfraction(int id)
        {
          if (_context.Infractions == null)
          {
              return NotFound();
          }
            var infraction = await _context.Infractions.FindAsync(id);

            if (infraction == null)
            {
                return NotFound();
            }

            return infraction;
        }

        private bool InfractionExists(int id)
        {
            return (_context.Infractions?.Any(e => e.InfractionId == id)).GetValueOrDefault();
        }
    }
}
