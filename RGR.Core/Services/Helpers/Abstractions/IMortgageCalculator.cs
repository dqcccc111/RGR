using RGR.Core.Contracts;

namespace RGR.Core.Services.Helpers.Abstractions
{
    public interface IMortgageCalculator
    {
        Task<MortgageCalculationResponseContract> CalculateMortgage(MortgageRequestContract requestContract);
    }
}
