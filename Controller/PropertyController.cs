using kalkulator.net.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace kalkulator.net.Controller;

[Route("api/[controller]")]
[ApiController]
public class PropertyController(AppDbContext context) : ControllerBase
{
    private readonly AppDbContext _context = context;

    // GET: api/Property
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Property>>> GetProperties()
    {
        return await _context.Properties
            .Include(p => p.Calculations)
            .ToListAsync();
    }

    // GET: api/Property/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Property>> GetProperty(int id)
    {
        var property = await _context.Properties.FindAsync(id);

        if (property == null)
        {
            return NotFound();
        }

        return property;
    }

    // POST: api/Property
    [HttpPost]
    public async Task<ActionResult<Property>> PostProperty(Property property)
    {
        _context.Properties.Add(property);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetProperty), new { id = property.Id }, property);
    }

    // PUT: api/Property/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutProperty(int id, Property property)
    {
        if (id != property.Id)
        {
            return BadRequest();
        }

        _context.Entry(property).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!PropertyExists(id))
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

    // DELETE: api/Property/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProperty(int id)
    {
        var property = await _context.Properties.FindAsync(id);
        if (property == null)
        {
            return NotFound();
        }

        _context.Properties.Remove(property);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool PropertyExists(int id)
    {
        return _context.Properties.Any(e => e.Id == id);
    }

}