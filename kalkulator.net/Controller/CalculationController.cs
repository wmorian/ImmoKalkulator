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

    // GET: api/Calculation/propertyid
    [HttpGet("{propertyId}")]
    public async Task<ActionResult<IEnumerable<CalculationDto>>> GetCalculations(int propertyId)
    {
        var calculations = await _context.Calculations
            .Where(c => c.PropertyId == propertyId)
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
    [HttpGet("single/{id}")]
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

    // POST: api/Calculation/duplicate/{calculationId}
    [HttpPost("duplicate/{calculationId}")]
    public async Task<ActionResult<CalculationDto>> DuplicateCalculation(int calculationId)
    {
        // Fetch the calculation to be duplicated including all related entities
        var calculationToDuplicate = await _context.Calculations
            .Where(c => c.Id == calculationId)
            .Include(c => c.PurchaseDetail)
            .Include(c => c.InitialInvestments)
            .Include(c => c.Rent)
            .Include(c => c.Depreciation)
            .Include(c => c.Reserves)
            .Include(c => c.Forecast)
            .Include(c => c.OperatingCosts)
                .ThenInclude(o => o!.OtherCosts)
            .Include(c => c.Loans)
            .AsNoTracking() // Important to avoid tracking errors
            .FirstOrDefaultAsync();

        if (calculationToDuplicate == null)
        {
            return NotFound($"Calculation with ID {calculationId} not found.");
        }

        // Perform the duplication
        var duplicatedCalculation = DuplicateCalculationEntity(calculationToDuplicate);

        // Add the duplicated calculation to the context and save changes
        _context.Calculations.Add(duplicatedCalculation);
        await _context.SaveChangesAsync();

        // Map the duplicated calculation to its DTO
        var duplicatedCalculationDto = _mapper.Map<CalculationDto>(duplicatedCalculation);

        // Return the duplicated calculation
        return Ok(duplicatedCalculationDto);
    }


    private bool CalculationExists(int id)
    {
        return _context.Calculations.Any(e => e.Id == id);
    }

    private Calculation DuplicateCalculationEntity(Calculation calculation)
    {
        var newCalculation = new Calculation
        {
            PropertyId = calculation.PropertyId,
        };

        if (calculation.PurchaseDetail != null)
        {
            newCalculation.PurchaseDetail = new PurchaseDetail
            {
                PurchasePrice = calculation.PurchaseDetail.PurchasePrice,
                BrokerCommissionPercentage = calculation.PurchaseDetail.BrokerCommissionPercentage,
                NotaryFeePercentage = calculation.PurchaseDetail.NotaryFeePercentage,
                LandRegistryFeePercentage = calculation.PurchaseDetail.LandRegistryFeePercentage,
                TransferTaxPercentage = calculation.PurchaseDetail.TransferTaxPercentage,
                OtherCostsPercentage = calculation.PurchaseDetail.OtherCostsPercentage
            };
        }

        if (calculation.Depreciation != null)
        {
            newCalculation.Depreciation = new Depreciation
            {
                DepreciationRate = calculation.Depreciation.DepreciationRate,
                BuildingValuePercentageOfPurchasePrice = calculation.Depreciation.BuildingValuePercentageOfPurchasePrice,
            };
        }

        if (calculation.Forecast != null)
        {
            newCalculation.Forecast = new AnnualForecast
            {
                CostIncreasePercentage = calculation.Forecast.CostIncreasePercentage,
                RentIncreasePercentage = calculation.Forecast.RentIncreasePercentage,
                ValueIncreasePercentage = calculation.Forecast.ValueIncreasePercentage
            };
        }

        if (calculation.Rent != null)
        {
            newCalculation.Rent = new Rent
            {
                ColdRentPerSquareMeter = calculation.Rent.ColdRentPerSquareMeter,
                TotalColdRent = calculation.Rent.TotalColdRent,
                ParkingSpaces = calculation.Rent.ParkingSpaces,
                Other = calculation.Rent.Other
            };
        }

        if (calculation.Reserves != null)
        {
            newCalculation.Reserves = new Reserves
            {
                CalculatedRentLossPercentage = calculation.Reserves.CalculatedRentLossPercentage,
                MaintenanceReservePerSquareMeterPerAnnum = calculation.Reserves.MaintenanceReservePerSquareMeterPerAnnum
            };
        }

        if (calculation.OperatingCosts != null)
        {
            newCalculation.OperatingCosts = new OperatingCosts
            {
                HousingAllowanceAllocable = calculation.OperatingCosts.HousingAllowanceAllocable,
                PropertyTax = calculation.OperatingCosts.PropertyTax,
                HousingAllowanceNonAllocable = calculation.OperatingCosts.HousingAllowanceNonAllocable,
                HomeownersAssociationReserve = calculation.OperatingCosts.HomeownersAssociationReserve,
                OtherCosts = calculation.OperatingCosts.OtherCosts.Select(oc => new OtherOperatingCost
                {
                    Name = oc.Name,
                    Cost = oc.Cost,
                    IsAllocable = oc.IsAllocable
                }).ToList()
            };
        }

        newCalculation.InitialInvestments = calculation.InitialInvestments
            .Select(inv => new InitialInvestment
            {
                Name = inv.Name,
                Cost = inv.Cost,
                TaxTreatment = inv.TaxTreatment,
                ValueIncrease = inv.ValueIncrease
            }).ToList();

        newCalculation.Loans = calculation.Loans.Select(loan => new Loan
        {
            LoanAmount = loan.LoanAmount,
            InterestRate = loan.InterestRate,
            InitialRepaymentRate = loan.InitialRepaymentRate,
            MonthlyPayment = loan.MonthlyPayment,
            YearOfFullRepayment = loan.YearOfFullRepayment
        }).ToList();

        return newCalculation;
    }
}