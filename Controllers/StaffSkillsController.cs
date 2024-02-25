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
    public class StaffSkillsController : ControllerBase
    {
        private readonly OrganisationContext _context;

        public StaffSkillsController(OrganisationContext context)
        {
            _context = context;
        }

        // GET: api/StaffSkills
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StaffSkill>>> GetStaffSkills()
        {
            return await _context.StaffSkills.ToListAsync();
        }

        // GET: api/StaffSkills/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StaffSkill>> GetStaffSkill(int? id)
        {
            var staffSkill = await _context.StaffSkills.FindAsync(id);

            if (staffSkill == null)
            {
                return NotFound();
            }

            return staffSkill;
        }

        // PUT: api/StaffSkills/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStaffSkill(int? id, StaffSkill staffSkill)
        {
            if (id != staffSkill.StaffSkillId)
            {
                return BadRequest();
            }

            _context.Entry(staffSkill).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StaffSkillExists(id))
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

        // POST: api/StaffSkills
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<StaffSkill>> PostStaffSkill(StaffSkill staffSkill)
        {
            _context.StaffSkills.Add(staffSkill);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStaffSkill", new { id = staffSkill.StaffSkillId }, staffSkill);
        }

        // DELETE: api/StaffSkills/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStaffSkill(int? id)
        {
            var staffSkill = await _context.StaffSkills.FindAsync(id);
            if (staffSkill == null)
            {
                return NotFound();
            }

            _context.StaffSkills.Remove(staffSkill);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StaffSkillExists(int? id)
        {
            return _context.StaffSkills.Any(e => e.StaffSkillId == id);
        }
    }
}
