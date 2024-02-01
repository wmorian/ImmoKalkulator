using AutoMapper;
using kalkulator.net.Model;
using kalkulator.net.Model.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace kalkulator.net.Controller;


[Route("api/[controller]")]
[ApiController]
public class CalculationController(AppDbContext context, IMapper mapper) : ControllerBase
{
    private readonly AppDbContext _context = context;
    private readonly IMapper _mapper = mapper;

    // GET: api/Calculation
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CalculationDto>>> GetCalculations()
    {
        var calculations = await _context.Calculations
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

        return _mapper.Map<List<CalculationDto>>(calculations);
    }

    // GET: api/Calculation/5
    [HttpGet("{id}")]
    public async Task<ActionResult<CalculationDto>> GetCalculation(int id)
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

        return _mapper.Map<CalculationDto>(calculation);
    }

    // POST: api/Calculation
    [HttpPost]
    public async Task<ActionResult<CalculationDto>> PostCalculation(CalculationDto calculationDto)
    {
        // Check if the PropertyId in the calculation exists in the database
        var propertyExists = await _context.Properties.AnyAsync(p => p.Id == calculationDto.PropertyId);
        if (!propertyExists)
        {
            return NotFound($"No property found with ID {calculationDto.PropertyId}");
        }

        var calculation = _mapper.Map<Calculation>(calculationDto);

        _context.Calculations.Add(calculation);
        await _context.SaveChangesAsync();

        return Created();
    }


    // PUT: api/Calculation/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutCalculation(int id, CalculationDto calculationDto)
    {
        if (id != calculationDto.Id)
        {
            return BadRequest();
        }

        var calculation = _mapper.Map<Calculation>(calculationDto);
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