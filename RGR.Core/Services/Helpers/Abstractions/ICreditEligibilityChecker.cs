namespace RGR.Core.Services.Helpers.Abstractions
{
    public interface ICreditEligibilityChecker
    {
        bool CheckEligibility(int clientAge, decimal clientIncome, decimal loanAmount, int years);
    }
}
