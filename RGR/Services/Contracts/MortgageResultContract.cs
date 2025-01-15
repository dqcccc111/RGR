namespace RGR.Services.Contracts
{
    public class MortgageResultContract
    {
        public MortgageResultContract(decimal monthlyPayment, decimal totalRepayment, decimal totalInterest)
        {
            MonthlyPayment = monthlyPayment;
            TotalRepayment = totalRepayment;
            TotalInterest = totalInterest;
        }

        public readonly decimal MonthlyPayment;
        public readonly decimal TotalRepayment;
        public readonly decimal TotalInterest;
    }
}
