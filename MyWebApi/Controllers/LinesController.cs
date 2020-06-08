using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyWebApi;

namespace MyWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LinesController : ControllerBase
    {
        private readonly MyWebDbContext _context;

        public LinesController(MyWebDbContext context)
        {
            _context = context;
        }

        // GET: api/Lines
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Line>>> GetLine()
        {
            return await _context.Line.ToListAsync();
        }

        // GET: api/Lines/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Line>> GetLine(int id)
        {
            var line = await _context.Line.FindAsync(id);

            if (line == null)
            {
                return NotFound();
            }

            return line;
        }

        // PUT: api/Lines/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLine(int id, Line line)
        {
            if (id != line.LineId)
            {
                return BadRequest();
            }

            _context.Entry(line).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LineExists(id))
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

        // POST: api/Lines
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Line>> PostLine(Line line)
        {
            _context.Line.Add(line);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (LineExists(line.LineId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetLine", new { id = line.LineId }, line);
        }

        // DELETE: api/Lines/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Line>> DeleteLine(int id)
        {
            var line = await _context.Line.FindAsync(id);
            if (line == null)
            {
                return NotFound();
            }

            _context.Line.Remove(line);
            await _context.SaveChangesAsync();

            return line;
        }

        private bool LineExists(int id)
        {
            return _context.Line.Any(e => e.LineId == id);
        }
    }
}
