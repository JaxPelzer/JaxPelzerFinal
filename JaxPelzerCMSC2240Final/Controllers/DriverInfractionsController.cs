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
    public class DriverInfractionsController : ControllerBase
    {
        private readonly finalDatabaseContext _context;

        public DriverInfractionsController(finalDatabaseContext context)
        {
            _context = context;
        }

        // GET: api/DriverInfractions
        [Authorize(Roles = "POLICE"+","+"DMV")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InfractionDriverLink>>> GetInfractionDriverLinks()
        {
          if (_context.InfractionDriverLinks == null)
          {
              return NotFound();
          }
            return await _context.InfractionDriverLinks.ToListAsync();
        }

        // GET: api/DriverInfractions/5
        [Authorize(Roles = "POLICE" + "," + "DMV")]
        [HttpGet("{id}")]
        public async Task<ActionResult<InfractionDriverLink>> GetInfractionDriverLink(int id)
        {
          if (_context.InfractionDriverLinks == null)
          {
              return NotFound();
          }
            var infractionDriverLink = await _context.InfractionDriverLinks.FindAsync(id);

            if (infractionDriverLink == null)
            {
                return NotFound();
            }

            return infractionDriverLink;
        }

       

        // POST: api/DriverInfractions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "POLICE")]
        [HttpPost]
        public async Task<ActionResult<InfractionDriverLink>> PostInfractionDriverLink(InfractionDriverLink infractionDriverLink)
        {
          if (_context.InfractionDriverLinks == null)
          {
              return Problem("Entity set 'finalDatabaseContext.InfractionDriverLinks'  is null.");
          }
            _context.InfractionDriverLinks.Add(infractionDriverLink);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInfractionDriverLink", new { id = infractionDriverLink.VehicleId }, infractionDriverLink);
        }



        private bool InfractionDriverLinkExists(int id)
        {
            return (_context.InfractionDriverLinks?.Any(e => e.VehicleId == id)).GetValueOrDefault();
        }
    }
}
