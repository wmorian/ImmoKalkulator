using AutoMapper;
using kalkulator.net.Model;
using kalkulator.net.Model.DTOs;

namespace kalkulator.net;


public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Create a map between your entities and DTOs
        CreateMap<Property, PropertyDto>().ReverseMap();
        CreateMap<Calculation, CalculationDto>().ReverseMap();
        CreateMap<Depreciation, DepreciationDto>().ReverseMap();
        CreateMap<AnnualForecast, AnnualForecastDto>().ReverseMap();
        CreateMap<InitialInvestment, InitialInvestmentDto>().ReverseMap();
        CreateMap<Loan, LoanDto>().ReverseMap();
        CreateMap<OperatingCosts, OperatingCostsDto>().ReverseMap();
        CreateMap<Property, PropertyDto>().ReverseMap();
        CreateMap<PurchaseDetail, PurchaseDetailDto>().ReverseMap();
        CreateMap<Rent, RentDto>().ReverseMap();
        CreateMap<Reserves, ReservesDto>().ReverseMap();
    }
}