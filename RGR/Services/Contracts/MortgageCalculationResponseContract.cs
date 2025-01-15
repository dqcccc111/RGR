namespace RGR.Services.Contracts
{
    public class MortgageCalculationResponseContract
    {
        public readonly MortgageRequestContract Request;
        public readonly MortgageResultContract Result;

        public MortgageCalculationResponseContract(MortgageRequestContract request, MortgageResultContract result)
        {
            Request = request;
            Result = result;
        }
    }
}
