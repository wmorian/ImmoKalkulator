using kalkulator.net.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kalkulator.net.Controller;


[Route("api/[controller]")]
[ApiController]
public class CalculationController(AppDbContext context) : ControllerBase
{
    private readonly AppDbContext _context = context;

    // GET: api/Calculation
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Calculation>>> GetCalculations()
    {
        return await _context.Calculations
            .Include(c => c.Property)
            .Include(c => c.PurchaseDetail)
            .Include(c => c.InitialInvestments)
            .Include(c => c.Rent)
            .Include(c => c.Depreciation)
            .Include(c => c.Reserves)
            .Include(c => c.Forecast)
            .Include(c => c.OperatingCosts)
                .ThenInclude(o => o!.OtherCosts)
            .Include(c => c.Loans)
            .ToListAsync();
    }

    // GET: api/Calculation/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Calculation>> GetCalculation(int id)
    {
        var calculation = await _context.Calculations
            .Include(c => c.Property)
            .Include(c => c.PurchaseDetail)
            .Include(c => c.InitialInvestments)
            .Include(c => c.Rent)
            .Include(c => c.Depreciation)
            .Include(c => c.Reserves)
            .Include(c => c.Forecast)
            .Include(c => c.OperatingCosts)
                .ThenInclude(o => o!.OtherCosts)
            .Include(c => c.Loans)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (calculation == null)
        {
            return NotFound();
        }

        return calculation;
    }

    // POST: api/Calculation
    [HttpPost]
    public async Task<ActionResult<Calculation>> PostCalculation(Calculation calculation)
    {
        // Check if the PropertyId in the calculation exists in the database
        var propertyExists = await _context.Properties.AnyAsync(p => p.Id == calculation.PropertyId);
        if (!propertyExists)
        {
            return NotFound($"No property found with ID {calculation.PropertyId}");
        }

        _context.Calculations.Add(calculation);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetCalculation), new { id = calculation.Id }, calculation);
    }


    // PUT: api/Calculation/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutCalculation(int id, Calculation calculation)
    {
        if (id != calculation.Id)
        {
            return BadRequest();
        }

        _context.Entry(calculation).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!CalculationExists(id))
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

    // DELETE: api/Calculation/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCalculation(int id)
    {
        var calculation = await _context.Calculations.FindAsync(id);
        if (calculation == null)
        {
            return NotFound();
        }

        _context.Calculations.Remove(calculation);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool CalculationExists(int id)
    {
        return _context.Calculations.Any(e => e.Id == id);
    }

}