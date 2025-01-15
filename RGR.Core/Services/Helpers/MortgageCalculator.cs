using Microsoft.Extensions.Logging;
using RGR.Core.Contracts;
using RGR.Core.Services.Helpers.Abstractions;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("RGR.Core.Tests")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2, PublicKey=0024000004800000940000000602000000240000525341310004000001000100c547cac37abd99c8db225ef2f6c8a3602f3b3606cc9891605d02baa56104f4cfc0734aa39b93bf7852f7d9266654753cc297e7d2edfe0bac1cdcf9f717241550e0a7b191195b7667bb4f64bcb8e2121380fd1d9d46ad2d92d2d15605093924cceaf74c4861eff62abf69b9291ed0a340e113be11e6a7d3113e92484cf7045cc7")]
namespace RGR.Core.Services.Helpers
{
    internal class MortgageCalculator : IMortgageCalculator
    {
        private readonly ILogger<MortgageCalculator> _logger;

        public MortgageCalculator(ILogger<MortgageCalculator> logger)
        {
            _logger = logger;
        }

        public async Task<MortgageCalculationResponseContract> CalculateMortgage(MortgageRequestContract requestContract)
        {
            try
            {
                ValidateRequest(requestContract);

                _logger.LogInformation("Starting mortgage calculation for loan amount {Amount} and term {Years} years at {InterestRate}% interest rate.",
                    requestContract.Amount, requestContract.Years, requestContract.InterestRate);

                // Number of months in the loan period
                var months = requestContract.Years * 12;

                // Monthly interest rate
                var monthlyRate = requestContract.InterestRate / 100 / 12;

                // Formula to calculate monthly payment for an annuity loan
                var monthlyPayment = requestContract.Amount * monthlyRate / (1 - (decimal)Math.Pow((double)(1 + monthlyRate), -months));

                // Total repayment amount over the term (monthly payment * number of months)
                var totalRepayment = Math.Ceiling(monthlyPayment * 100) / 100 * months;

                // Total interest paid: the difference between total repayment and the principal loan amount
                var totalInterest = totalRepayment - requestContract.Amount;

                _logger.LogInformation("Mortgage calculation completed. Monthly payment: {MonthlyPayment:F2}, Total repayment: {TotalRepayment:F2}, Total interest: {TotalInterest:F2}.",
                    monthlyPayment, totalRepayment, totalInterest);

                // Returning the response contract
                return await Task.FromResult(new MortgageCalculationResponseContract
                (
                    requestContract,
                    new MortgageResultContract
                    (
                        monthlyPayment,
                        totalRepayment,
                        totalInterest
                    )
                ));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while calculating the mortgage.");
                throw;
            }
        }

        private void ValidateRequest(MortgageRequestContract requestContract)
        {
            if (requestContract == null)
            {
                _logger.LogError("Invalid request contract: {requestContract}", requestContract);
                throw new ArgumentNullException(nameof(requestContract), "Request contract cannot be null");
            }

            if (requestContract.Amount <= 0)
            {
                _logger.LogError("Invalid loan amount: {Amount}", requestContract.Amount);
                throw new ArgumentException("Loan amount must be greater than zero.", nameof(requestContract.Amount));
            }

            if (requestContract.Years <= 0)
            {
                _logger.LogError("Invalid loan term in years: {Years}", requestContract.Years);
                throw new ArgumentException("Loan term in years must be greater than zero.", nameof(requestContract.Years));
            }

            if (requestContract.InterestRate <= 0)
            {
                _logger.LogError("Invalid interest rate: {InterestRate}", requestContract.InterestRate);
                throw new ArgumentException("Interest rate must be greater than zero.", nameof(requestContract.InterestRate));
            }
        }
    }
}
