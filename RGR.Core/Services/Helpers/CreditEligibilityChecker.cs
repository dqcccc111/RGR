using Microsoft.Extensions.Logging;
using RGR.Core.Services.Helpers.Abstractions;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("RGR.Core.Tests")]
namespace RGR.Core.Services.Helpers
{
    public class CreditEligibilityChecker : ICreditEligibilityChecker
    {
        private readonly ILogger<CreditEligibilityChecker> _logger;

        public CreditEligibilityChecker(ILogger<CreditEligibilityChecker> logger)
        {
            _logger = logger;
        }

        public bool CheckEligibility(int clientAge, decimal clientIncome, decimal loanAmount, int years)
        {
            bool isApproved = false;

            if (clientAge >= 21 && clientIncome >= loanAmount / years * 0.3m)
            {
                isApproved = true;
                _logger.LogInformation("Credit request approved.");
            }
            else
            {
                _logger.LogInformation("Credit request rejected.");
            }

            return isApproved;
        }
    }
}
