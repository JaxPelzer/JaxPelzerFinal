using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JaxPelzerFinal.Data;
using JaxPelzerFinal.Models;

namespace JaxPelzerFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchoolusersController : ControllerBase
    {
        private readonly Cmsc2240finaldatabaseContext _context;

        public SchoolusersController(Cmsc2240finaldatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Schoolusers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Schooluser>>> GetSchoolusers()
        {
          if (_context.Schoolusers == null)
          {
              return NotFound();
          }
            return await _context.Schoolusers.ToListAsync();
        }

        // GET: api/Schoolusers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Schooluser>> GetSchooluser(int id)
        {
          if (_context.Schoolusers == null)
          {
              return NotFound();
          }
            var schooluser = await _context.Schoolusers.FindAsync(id);

            if (schooluser == null)
            {
                return NotFound();
            }

            return schooluser;
        }

        // PUT: api/Schoolusers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSchooluser(int id, Schooluser schooluser)
        {
            if (id != schooluser.UserId)
            {
                return BadRequest();
            }

            _context.Entry(schooluser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SchooluserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Schoolusers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Schooluser>> PostSchooluser(Schooluser schooluser)
        {
          if (_context.Schoolusers == null)
          {
              return Problem("Entity set 'Cmsc2240finaldatabaseContext.Schoolusers'  is null.");
          }
            _context.Schoolusers.Add(schooluser);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (SchooluserExists(schooluser.UserId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetSchooluser", new { id = schooluser.UserId }, schooluser);
        }

        // DELETE: api/Schoolusers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSchooluser(int id)
        {
            if (_context.Schoolusers == null)
            {
                return NotFound();
            }
            var schooluser = await _context.Schoolusers.FindAsync(id);
            if (schooluser == null)
            {
                return NotFound();
            }

            _context.Schoolusers.Remove(schooluser);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SchooluserExists(int id)
        {
            return (_context.Schoolusers?.Any(e => e.UserId == id)).GetValueOrDefault();
        }
    }
}
