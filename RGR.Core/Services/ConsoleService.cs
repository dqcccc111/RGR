using RGR.Core.Contracts;
using RGR.Core.Services.Abstractions;
using RGR.IO.Abstractions;
using System.Globalization;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("RGR.Core.Tests")]
namespace RGR.Core.Services
{
    internal class ConsoleService : IConsoleService
    {
        private readonly IConsole _console;
        public ConsoleService(IConsole console)
        {
            _console = console;
        }

        public string? GetClientFullName()
        {
            _console.Write("Enter the client's name (Full name): ");
            var fullName = _console.ReadLine();
            if (string.IsNullOrWhiteSpace(fullName))
            {
                _console.WriteLine("Client name cannot be empty. Please enter a valid name.");
            }
            return fullName;
        }

        public int GetClientAge()
        {
            return ReadInt
                (
                    "Enter the client's age: ",
                    "Incorrect format. Please enter a valid age (over 21)."
                );
        }

        public decimal GetClientIncome()
        {
            return ReadDecimal
                (
                    "Enter the client's monthly income in rubles: ",
                    "Incorrect format. Please enter a valid income."
                );
        }

        public decimal GetLoanAmount()
        {
            return ReadDecimal
                (
                    "Enter the requested loan amount in rubles: ",
                    "Incorrect format. Please enter a valid loan amount."
                );
        }

        public int GetYears()
        {
            return ReadInt
                (
                    "Enter the loan term in years: ",
                    "Incorrect format. Please enter a valid interest years"
                );
        }

        public decimal GetInterest()
        {
            return ReadDecimal
                (
                    "Enter the interest rate: ",
                    "Incorrect format. Please enter a valid interest rate"
                );
        }

        public MortgageRequestContract GetMortgageRequest()
        {
            return new MortgageRequestContract
                (
                    GetLoanAmount(),
                    GetYears(),
                    GetInterest()
                );
        }

        public MortgageRequestContract GetMortgageRequest(decimal loanAmount, int years)
        {
            return new MortgageRequestContract
                (
                    loanAmount,
                    years,
                    GetInterest()
                );
        }

        public decimal ReadDecimal(string message, string exMessage)
        {
            _console.Write(message);
            var input = _console.ReadLine();
            input = input?.Replace("_", "");
            
            decimal output;
            while (!decimal.TryParse(input, NumberStyles.Any, CultureInfo.CurrentCulture, out output))
            {
                _console.WriteLine(exMessage);
                input = _console.ReadLine();
                input = input?.Replace("_", "");
            }

            return output;
        }

        public int ReadInt(string message, string exMessage)
        {
            _console.Write(message);
            var input = _console.ReadLine();
            input = input?.Replace("_", "");

            int output;
            while (!int.TryParse(input, NumberStyles.Any, CultureInfo.CurrentCulture, out output))
            {
                _console.WriteLine(exMessage);
                input = _console.ReadLine();
                input = input?.Replace("_", "");
            }

            return output;
        }
    }
}
