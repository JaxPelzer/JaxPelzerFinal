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
    public class VehiclesController : ControllerBase
    {
        private readonly finalDatabaseContext _context;

        public VehiclesController(finalDatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Vehicles
        [Authorize(Roles = "POLICE" + "," + "DMV")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vehicle>>> GetVehicles()
        {
          if (_context.Vehicles == null)
          {
              return NotFound();
          }
            return await _context.Vehicles.ToListAsync();
        }

        // GET: api/Vehicles/5
        [Authorize(Roles = "POLICE" + "," + "DMV")]
        [HttpGet("Lisence")]
        public async Task<ActionResult<Vehicle>> GetVehicle(string lisence)
        {
          if (_context.Vehicles == null)
          {
              return NotFound();
          }
            var vehicle = await _context.Vehicles.FirstOrDefaultAsync(d => d.LiscencePlateNumber == lisence);

            if (vehicle == null)
            {
                return NotFound();
            }
            
            return vehicle;
        }

        

        // POST: api/Vehicles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "DMV")]
        [HttpPost]
        public async Task<ActionResult<Vehicle>> PostVehicle(Vehicle vehicle)
        {
          if (_context.Vehicles == null)
          {
              return Problem("Entity set 'finalDatabaseContext.Vehicles'  is null.");
          }
            _context.Vehicles.Add(vehicle);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVehicle", new { id = vehicle.VehicleId }, vehicle);
        }


        private bool VehicleExists(int id)
        {
            return (_context.Vehicles?.Any(e => e.VehicleId == id)).GetValueOrDefault();
        }
    }
}
