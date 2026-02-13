using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InventoryService.Data;
using InventoryService.Models;

namespace InventoryService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InventoryController : ControllerBase
    {
        private readonly AppDbContext _context;

        public InventoryController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/inventory
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Inventory>>> GetAll()
        {
            var items = await _context.Inventories.ToListAsync();
            return Ok(items);
        }

        // GET: api/inventory/2
        [HttpGet("{id}")]
        public async Task<ActionResult<Inventory>> GetById(int id)
        {
            var item = await _context.Inventories.FindAsync(id);

            if (item == null)
                return NotFound();

            return Ok(item);
        }

        // POST: api/inventory
        [HttpPost]
        public async Task<ActionResult<Inventory>> Create([FromBody] Inventory inventory)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Inventories.Add(inventory);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = inventory.Id }, inventory);
        }

        // PUT: api/inventory/2
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Inventory inventory)
        {
            if (id != inventory.Id)
                return BadRequest("Route id and body id must match.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var exists = await _context.Inventories.AnyAsync(x => x.Id == id);
            if (!exists)
                return NotFound();

            _context.Entry(inventory).State = EntityState.Modified;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/inventory/2
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _context.Inventories.FindAsync(id);

            if (item == null)
                return NotFound();

            _context.Inventories.Remove(item);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
