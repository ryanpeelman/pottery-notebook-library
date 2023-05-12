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
    public class ClayProductController : ControllerBase
    {
        private readonly ProductContext _context;

        public ClayProductController(ProductContext context)
        {
            _context = context;
        }

        // GET: api/ClayProduct
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClayProduct>>> GetClays()
        {
          if (_context.Clays == null)
          {
              return NotFound();
          }
            return await _context.Clays.ToListAsync();
        }

        // GET: api/ClayProduct/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ClayProduct>> GetClayProduct(int id)
        {
          if (_context.Clays == null)
          {
              return NotFound();
          }
            var clayProduct = await _context.Clays.FindAsync(id);

            if (clayProduct == null)
            {
                return NotFound();
            }

            return clayProduct;
        }

        // PUT: api/ClayProduct/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClayProduct(int id, ClayProduct clayProduct)
        {
            if (id != clayProduct.Id)
            {
                return BadRequest();
            }

            _context.Entry(clayProduct).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClayProductExists(id))
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

        // POST: api/ClayProduct
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ClayProduct>> PostClayProduct(ClayProduct clayProduct)
        {
          if (_context.Clays == null)
          {
              return Problem("Entity set 'ProductContext.Clays'  is null.");
          }
            _context.Clays.Add(clayProduct);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetClayProduct", new { id = clayProduct.Id }, clayProduct);
        }

        // DELETE: api/ClayProduct/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClayProduct(int id)
        {
            if (_context.Clays == null)
            {
                return NotFound();
            }
            var clayProduct = await _context.Clays.FindAsync(id);
            if (clayProduct == null)
            {
                return NotFound();
            }

            _context.Clays.Remove(clayProduct);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ClayProductExists(int id)
        {
            return (_context.Clays?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
