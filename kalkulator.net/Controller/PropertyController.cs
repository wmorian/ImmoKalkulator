using AutoMapper;
using kalkulator.net.Model;
using kalkulator.net.Model.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace kalkulator.net.Controller;

[Route("api/[controller]")]
[ApiController]
public class PropertyController(AppDbContext context, IMapper mapper) : ControllerBase
{
    private readonly AppDbContext _context = context;
    private readonly IMapper _mapper = mapper;

    // GET: api/Property
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PropertyDto>>> GetProperties()
    {
        var properties = await _context.Properties
            .Include(p => p.Calculations)
            .ToListAsync();

        return _mapper.Map<List<PropertyDto>>(properties);
    }

    // GET: api/Property/5
    [HttpGet("{id}")]
    public async Task<ActionResult<PropertyDetailDto>> GetProperty(int id)
    {
        var property = await _context.Properties
            .Include(p => p.Calculations)
            .ThenInclude(c => c.PurchaseDetail)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (property == null)
        {
            return NotFound();
        }

        return _mapper.Map<PropertyDetailDto>(property);
    }

    // POST: api/Property
    [HttpPost]
    public async Task<ActionResult<PropertyDto>> PostProperty(PropertyDto propertyDto)
    {
        var property = _mapper.Map<Property>(propertyDto);
        _context.Properties.Add(property);
        await _context.SaveChangesAsync();

        return Created();
    }

    // PUT: api/Property/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutProperty(int id, PropertyDto propertyDto)
    {
        if (id != propertyDto.Id)
        {
            return BadRequest();
        }

        var property = _mapper.Map<Property>(propertyDto);
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