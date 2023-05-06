using CrudOperationsInNetCore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CrudOperationsInNetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GorselController : ControllerBase
    {
        private readonly GorselprgContext _dbContext;

        public GorselController(GorselprgContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Gorselprg>>> GetGorselprgs()
        {
            if(_dbContext.Gorselprg == null)
            {
                return NotFound();
            }
            return await _dbContext.Gorselprg.ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Gorselprg>> GetGorselprg(int id)
        {
            if (_dbContext.Gorselprg == null)
            {
                return NotFound();
            }
            var gorselprg = await _dbContext.Gorselprg.FindAsync(id);
            if(gorselprg == null)
            {
                return NotFound();
            }
            return gorselprg;
        }
        [HttpPost]
        public async Task<ActionResult<Gorselprg>> PostGorselprg(Gorselprg gorselprg)
        {
            _dbContext.Gorselprg.Add(gorselprg);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetGorselprg), new { id = gorselprg.ID }, gorselprg);
        }
        [HttpPut]
        public async Task<IActionResult> PutGorselprg(int id, Gorselprg gorselprg)
        {
            if (id != gorselprg.ID)
            {
                return BadRequest();
            }
            _dbContext.Entry(gorselprg).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GorselprgAvailable(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Ok();
        }
        private bool GorselprgAvailable(int id)
        {
            return (_dbContext.Gorselprg?.Any(x => x.ID == id)).GetValueOrDefault();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGorselprg(int id)
        {
            if (_dbContext.Gorselprg == null)
            {
                return NotFound();
            }
            var gorselprg = await _dbContext.Gorselprg.FindAsync(id);
            if (gorselprg == null)
            {
                return NotFound();
            }
            _dbContext.Gorselprg.Remove(gorselprg);

            await _dbContext.SaveChangesAsync();

            return Ok();
        }
    }
}
