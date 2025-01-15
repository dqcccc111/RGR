namespace RGR.Core.Services.Abstractions
{
    public interface IMortgageService
    {
        Task HandleMortgageCalculation();
        Task HandleCreditRequest();
    }
}
