using System.Collections.ObjectModel;

namespace RGR.Services.Contracts
{
    public class MortgageRequestContract
    {
        public MortgageRequestContract(decimal amount, int years, decimal interestRate)
        {
            Amount = amount;
            Years = years;
            InterestRate = interestRate;
        }

        public readonly decimal Amount;
        public readonly int Years;
        public readonly decimal InterestRate;
    }
}
