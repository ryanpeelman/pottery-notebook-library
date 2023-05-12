using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace pottery_notebook_library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GlazeProductController : ControllerBase
    {
        private readonly ProductContext _context;

        public GlazeProductController(ProductContext context)
        {
            _context = context;
        }

        // GET: api/GlazeProduct
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GlazeProduct>>> GetGlazes()
        {
          if (_context.Glazes == null)
          {
              return NotFound();
          }
            return await _context.Glazes.ToListAsync();
        }

        // GET: api/GlazeProduct/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GlazeProduct>> GetGlazeProduct(int id)
        {
          if (_context.Glazes == null)
          {
              return NotFound();
          }
            var glazeProduct = await _context.Glazes.FindAsync(id);

            if (glazeProduct == null)
            {
                return NotFound();
            }

            return glazeProduct;
        }

        // PUT: api/GlazeProduct/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGlazeProduct(int id, GlazeProduct glazeProduct)
        {
            if (id != glazeProduct.Id)
            {
                return BadRequest();
            }

            _context.Entry(glazeProduct).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GlazeProductExists(id))
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

        // POST: api/GlazeProduct
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<GlazeProduct>> PostGlazeProduct(GlazeProduct glazeProduct)
        {
          if (_context.Glazes == null)
          {
              return Problem("Entity set 'ProductContext.Glazes'  is null.");
          }
            _context.Glazes.Add(glazeProduct);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGlazeProduct", new { id = glazeProduct.Id }, glazeProduct);
        }

        // DELETE: api/GlazeProduct/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGlazeProduct(int id)
        {
            if (_context.Glazes == null)
            {
                return NotFound();
            }
            var glazeProduct = await _context.Glazes.FindAsync(id);
            if (glazeProduct == null)
            {
                return NotFound();
            }

            _context.Glazes.Remove(glazeProduct);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GlazeProductExists(int id)
        {
            return (_context.Glazes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
