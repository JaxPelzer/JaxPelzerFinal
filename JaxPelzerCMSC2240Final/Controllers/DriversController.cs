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
    public class DriversController : ControllerBase
    {
        private readonly finalDatabaseContext _context;

        public DriversController(finalDatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Drivers
        [Authorize(Roles = "POLICE" + "," + "DMV")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Driver>>> GetDrivers()
        {
          if (_context.Drivers == null)
          {
              return NotFound();
          }
            return await _context.Drivers.ToListAsync();
        }
        // GET: api/Drivers/5
        [Authorize(Roles = "POLICE" + "," + "DMV")]
        [HttpGet("id")]
        public async Task<ActionResult<Driver>> GetDriverID(int id)
        {
            if (_context.Drivers == null)
            {
                return NotFound();
            }
            var driver = await _context.Drivers.FirstOrDefaultAsync(d => d.DriverId == id);

            if (driver == null)
            {
                return NotFound();
            }

            return driver;
        }

        // GET: api/Drivers/5
        [Authorize(Roles = "POLICE" + "," + "DMV")]
        [HttpGet("FirstName")]
        public async Task<ActionResult<Driver>> GetDriverFirstName(string fname)
        {
          if (_context.Drivers == null)
          {
              return NotFound();
          }
            var driver = await _context.Drivers.FirstOrDefaultAsync(d => d.FName == fname);

            if (driver == null)
            {
                return NotFound();
            }

            return driver;
        }
        // GET: api/Drivers/5
        [Authorize(Roles = "POLICE" + "," + "DMV")]
        [HttpGet("LastName")]
        public async Task<ActionResult<Driver>> GetDriverLastName(string lname)
        {
            if (_context.Drivers == null)
            {
                return NotFound();
            }
            var driver = await _context.Drivers.FirstOrDefaultAsync(d => d.LName == lname);

            if (driver == null)
            {
                return NotFound();
            }

            return driver;
        }
        


        [Authorize(Roles = "POLICE" + "," + "DMV")]
        [HttpGet("SociaLSecuirty")]
        public async Task<ActionResult<Driver>> GetDriverSSN(string ssn)
        {
            if (_context.Drivers == null)
            {
                return NotFound();
            }
            var driver = await _context.Drivers.FirstOrDefaultAsync(d => d.Ssn == ssn);

            if (driver == null)
            {
                return NotFound();
            }

            return driver;
        }
        

        

        // POST: api/Drivers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "DMV")]
        [HttpPost]
        public async Task<ActionResult<Driver>> PostDriver(Driver driver)
        {
          if (_context.Drivers == null)
          {
              return Problem("Entity set 'finalDatabaseContext.Drivers'  is null.");
          }
            _context.Drivers.Add(driver);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDriver", new { id = driver.DriverId }, driver);
        }



        private bool DriverExists(int id)
        {
            return (_context.Drivers?.Any(e => e.DriverId == id)).GetValueOrDefault();
        }
    }
}
