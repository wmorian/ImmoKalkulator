using AutoMapper;
using kalkulator.net.Model;
using kalkulator.net.Model.DTOs;
using kalkulator.net.Services.Metric;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace kalkulator.net.Controller;


[Route("api/[controller]")]
[ApiController]
public class MetricController(AppDbContext context, IMapper mapper) : ControllerBase
{
    private readonly AppDbContext _context = context;
    private readonly IMapper _mapper = mapper;

    [HttpGet("{calculationId}")]
    public async Task<ActionResult<Metrics>> GetMetric(int calculationId)
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
            .FirstOrDefaultAsync(c => c.Id == calculationId);

        if (calculation == null)
        {
            return NotFound();
        }

        var metricsService = new MetricsService(calculation, calculation.Property!.LivingSpace);
        var metric = metricsService.GetMetrics(41);
        return Ok(metric);
    }
}