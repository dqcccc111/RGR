using RGR.Core.Contracts;

namespace RGR.Core.Services.Abstractions
{
    public interface IConsoleService
    {
        string? GetClientFullName();

        int GetClientAge();

        decimal GetClientIncome();

        decimal GetLoanAmount();

        int GetYears();

        public decimal GetInterest();

        MortgageRequestContract GetMortgageRequest(decimal loanAmount, int years);

        MortgageRequestContract GetMortgageRequest();

        decimal ReadDecimal(string message, string exMessage);

        int ReadInt(string message, string exMessage);
    }
}
