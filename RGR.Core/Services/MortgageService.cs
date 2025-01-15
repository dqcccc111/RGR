using Microsoft.Extensions.Logging;
using RGR.Core.Contracts;
using RGR.Core.Services.Abstractions;
using RGR.Core.Services.Helpers.Abstractions;
using RGR.IO.Abstractions;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("RGR.Core.Tests")]
namespace RGR.Core.Services
{

    internal partial class MortgageService : IMortgageService
    {
        private readonly ILogger<MortgageService> _logger;
        private readonly ICreditEligibilityChecker _eligibilityChecker;
        private readonly IMortgageCalculator _mortgageCalculator;
        private readonly IConsole _console;
        private readonly IFileService _fileService;
        private readonly IConsoleService _consoleService;
        public MortgageService
        (
            ILogger<MortgageService> logger,
            ICreditEligibilityChecker eligibilityChecker,
            IMortgageCalculator mortgageCalculator,
            IConsole console,
            IFileService fileService,
            IConsoleService consoleService
        )
        {
            _logger = logger;
            _eligibilityChecker = eligibilityChecker;
            _mortgageCalculator = mortgageCalculator;
            _console = console;
            _fileService = fileService;
            _consoleService = consoleService;
        }

        public async Task HandleCreditRequest()
        {
            try
            {
                _logger.LogInformation("Starting credit request handling.");

                _console.WriteLine("Credit application form:");

                var clientFullName = _consoleService.GetClientFullName();
                if (string.IsNullOrWhiteSpace(clientFullName)) return;

                var nameParts = clientFullName.Split(' ');
                if (nameParts.Length < 2) return;

                string surname = nameParts[0];
                string initials = string.Join("", nameParts.Skip(1).Select(n => n.Substring(0, 1).ToUpper()));

                var clientAge = _consoleService.GetClientAge();
                var clientIncome = _consoleService.GetClientIncome();
                var loanAmount = _consoleService.GetLoanAmount();
                var years = _consoleService.GetYears();

                bool isApproved = _eligibilityChecker.CheckEligibility(clientAge, clientIncome, loanAmount, years);

                if (isApproved)
                {
                    _console.WriteLine("Credit request approved. Calculating mortgage conditions...");

                    var mortgageRequest = _consoleService.GetMortgageRequest(loanAmount, years);


                    var result = await _mortgageCalculator.CalculateMortgage(mortgageRequest);

                    _console.WriteLine($"Monthly payment: {result.Result.MonthlyPayment:F2} ₽");
                    _console.WriteLine($"Total repayment amount: {result.Result.TotalRepayment:F2} ₽");
                    _console.WriteLine($"Total interest paid: {result.Result.TotalInterest:F2} ₽");

                    var creditRequest = new CreditRequestContract(
                        clientFullName,
                        clientAge,
                        clientIncome,
                        loanAmount,
                        result.Request.Years,
                        result.Request.InterestRate,
                        result.Result.MonthlyPayment,
                        result.Result.TotalRepayment,
                        result.Result.TotalInterest
                    );

                    string fileName = $"credit_request_{DateTime.Now:yyyyMMdd_HHmmss}_{surname}{initials}.json";
                    string creditRequestsFilePath = Path.Combine("CreditRequestsJsonDataBase", fileName);
                    _fileService.SaveCreditRequest(creditRequest, creditRequestsFilePath);

                    _logger.LogInformation($"Credit request saved to {creditRequestsFilePath}");
                }
                else
                {
                    _console.WriteLine("Credit request rejected.");
                }

                await Task.Delay(1000);
                _console.WriteLine("Press any key to continue...");
                _console.ReadKey();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unknown error during credit request handling.");
                _console.WriteLine("An error occurred. Please try again.");
            }
        }

        public async Task HandleMortgageCalculation()
        {
            try
            {
                _logger.LogInformation("Starting mortgage calculation.");

                _console.WriteLine("Mortgage monthly payment and total debt calculation form:");

                var mortgageRequest = _consoleService.GetMortgageRequest();

                // Mortgage calculation
                _logger.LogInformation("Starting mortgage calculation.");
                var result = await _mortgageCalculator.CalculateMortgage(mortgageRequest);

                // Display result
                _console.WriteLine($"Monthly payment: {result.Result.MonthlyPayment:F2} ₽");
                _console.WriteLine($"Total repayment amount: {result.Result.TotalRepayment:F2} ₽");
                _console.WriteLine($"Total interest paid: {result.Result.TotalInterest:F2} ₽");

                _logger.LogInformation("Mortgage calculation completed successfully.");

                _console.WriteLine("Press any key to continue...");
                _console.ReadKey();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unknown error during mortgage calculation.");
                _console.WriteLine("An error occurred. Please try again.");
            }
        }
    }
}
