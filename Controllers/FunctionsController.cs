using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CourseWork.Models;

namespace CourseWork.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FunctionsController : ControllerBase
    {
        private readonly OrganisationContext _context;

        public FunctionsController(OrganisationContext context)
        {
            _context = context;
        }

        // GET: api/Functions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Function>>> GetFunctions()
        {
            return await _context.Functions.ToListAsync();
        }

        // GET: api/Functions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Function>> GetFunction(int? id)
        {
            var function = await _context.Functions.FindAsync(id);

            if (function == null)
            {
                return NotFound();
            }

            return function;
        }

        // PUT: api/Functions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFunction(int? id, Function function)
        {
            if (id != function.Id)
            {
                return BadRequest();
            }

            _context.Entry(function).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FunctionExists(id))
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

        private bool FunctionExists(int? id)
        {
            throw new NotImplementedException();
        }

        // POST: api/Functions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Function>> PostRole(Function function)
        {
            _context.Functions.Add(function);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFunction", new { id = function.Id }, function);
        }

        // DELETE: api/Functions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFunction(int? id)
        {
            var function = await _context.Functions.FindAsync(id);
            if (function == null)
            {
                return NotFound();
            }

            _context.Functions.Remove(function);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RoleExists(int? id)
        {
            return _context.Functions.Any(e => e.Id == id);
        }
    }
}
