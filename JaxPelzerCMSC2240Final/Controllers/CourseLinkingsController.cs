using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JaxPelzerCMSC2240Final.Data;
using JaxPelzerCMSC2240Final.Models;

namespace JaxPelzerCMSC2240Final.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseLinkingsController : ControllerBase
    {
        private readonly finalDatabaseContext _context;

        public CourseLinkingsController(finalDatabaseContext context)
        {
            _context = context;
        }

        // GET: api/CourseLinkings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CourseLinking>>> GetCourseLinkings()
        {
          if (_context.CourseLinkings == null)
          {
              return NotFound();
          }
            return await _context.CourseLinkings.ToListAsync();
        }

        // GET: api/CourseLinkings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CourseLinking>> GetCourseLinking(int id)
        {
          if (_context.CourseLinkings == null)
          {
              return NotFound();
          }
            var courseLinking = await _context.CourseLinkings.FindAsync(id);

            if (courseLinking == null)
            {
                return NotFound();
            }

            return courseLinking;
        }

        // PUT: api/CourseLinkings/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCourseLinking(int id, CourseLinking courseLinking)
        {
            if (id != courseLinking.LinkId)
            {
                return BadRequest();
            }

            _context.Entry(courseLinking).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CourseLinkingExists(id))
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

        // POST: api/CourseLinkings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CourseLinking>> PostCourseLinking(CourseLinking courseLinking)
        {
          if (_context.CourseLinkings == null)
          {
              return Problem("Entity set 'finalDatabaseContext.CourseLinkings'  is null.");
          }
            _context.CourseLinkings.Add(courseLinking);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CourseLinkingExists(courseLinking.LinkId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCourseLinking", new { id = courseLinking.LinkId }, courseLinking);
        }

        // DELETE: api/CourseLinkings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourseLinking(int id)
        {
            if (_context.CourseLinkings == null)
            {
                return NotFound();
            }
            var courseLinking = await _context.CourseLinkings.FindAsync(id);
            if (courseLinking == null)
            {
                return NotFound();
            }

            _context.CourseLinkings.Remove(courseLinking);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CourseLinkingExists(int id)
        {
            return (_context.CourseLinkings?.Any(e => e.LinkId == id)).GetValueOrDefault();
        }
    }
}
