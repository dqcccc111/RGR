namespace RGR.Core.Contracts
{
    public class CreditRequestContract
    {
        public string ClientName { get; }
        public int ClientAge { get; }
        public decimal ClientIncome { get; }
        public decimal LoanAmount { get; }
        public int LoanTerm { get; }
        public decimal InterestRate { get; }
        public decimal MonthlyPayment { get; }
        public decimal TotalRepayment { get; }
        public decimal TotalInterest { get; }

        public CreditRequestContract(string clientName, int clientAge, decimal clientIncome, decimal loanAmount, int loanTerm, decimal interestRate, decimal monthlyPayment, decimal totalRepayment, decimal totalInterest)
        {
            ClientName = clientName;
            ClientAge = clientAge;
            ClientIncome = clientIncome;
            LoanAmount = loanAmount;
            LoanTerm = loanTerm;
            InterestRate = interestRate;
            MonthlyPayment = monthlyPayment;
            TotalRepayment = totalRepayment;
            TotalInterest = totalInterest;
        }
    }
}
