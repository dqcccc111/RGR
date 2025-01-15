using RGR.Core.Contracts;

namespace RGR.Core.Tests.ContractsTests
{
    [TestFixture]
    public class CreditRequestContractTests
    {
        // Позитивный тест для создания объекта CreditRequestContract с валидными значениями
        [Test]
        public void Constructor_ValidInput_CreatesCreditRequestContract()
        {
            // Arrange
            var clientName = "John Doe";
            var clientAge = 30;
            var clientIncome = 50000m;
            var loanAmount = 100000m;
            var loanTerm = 15;
            var interestRate = 5.5m;
            var monthlyPayment = 8500m;
            var totalRepayment = 153000m;
            var totalInterest = 53000m;

            // Act
            var creditRequest = new CreditRequestContract(clientName, clientAge, clientIncome, loanAmount, loanTerm, interestRate, monthlyPayment, totalRepayment, totalInterest);

            // Assert
            Assert.That(creditRequest.ClientName, Is.EqualTo(clientName));
            Assert.That(creditRequest.ClientAge, Is.EqualTo(clientAge));
            Assert.That(creditRequest.ClientIncome, Is.EqualTo(clientIncome));
            Assert.That(creditRequest.LoanAmount, Is.EqualTo(loanAmount));
            Assert.That(creditRequest.LoanTerm, Is.EqualTo(loanTerm));
            Assert.That(creditRequest.InterestRate, Is.EqualTo(interestRate));
            Assert.That(creditRequest.MonthlyPayment, Is.EqualTo(monthlyPayment));
            Assert.That(creditRequest.TotalRepayment, Is.EqualTo(totalRepayment));
            Assert.That(creditRequest.TotalInterest, Is.EqualTo(totalInterest));
        }

        // Тест на создание объекта с нулевыми значениями
        [Test]
        public void Constructor_ZeroValues_CreatesCreditRequestContract()
        {
            // Arrange
            var clientName = "John Doe";
            var clientAge = 0;
            var clientIncome = 0m;
            var loanAmount = 0m;
            var loanTerm = 0;
            var interestRate = 0m;
            var monthlyPayment = 0m;
            var totalRepayment = 0m;
            var totalInterest = 0m;

            // Act
            var creditRequest = new CreditRequestContract(clientName, clientAge, clientIncome, loanAmount, loanTerm, interestRate, monthlyPayment, totalRepayment, totalInterest);

            // Assert
            Assert.That(creditRequest.ClientName, Is.EqualTo(clientName));
            Assert.That(creditRequest.ClientAge, Is.EqualTo(clientAge));
            Assert.That(creditRequest.ClientIncome, Is.EqualTo(clientIncome));
            Assert.That(creditRequest.LoanAmount, Is.EqualTo(loanAmount));
            Assert.That(creditRequest.LoanTerm, Is.EqualTo(loanTerm));
            Assert.That(creditRequest.InterestRate, Is.EqualTo(interestRate));
            Assert.That(creditRequest.MonthlyPayment, Is.EqualTo(monthlyPayment));
            Assert.That(creditRequest.TotalRepayment, Is.EqualTo(totalRepayment));
            Assert.That(creditRequest.TotalInterest, Is.EqualTo(totalInterest));
        }

        // Тест на создание объекта с максимальными значениями
        [Test]
        public void Constructor_MaxValues_CreatesCreditRequestContract()
        {
            // Arrange
            var clientName = "John Doe";
            var clientAge = int.MaxValue;
            var clientIncome = decimal.MaxValue;
            var loanAmount = decimal.MaxValue;
            var loanTerm = int.MaxValue;
            var interestRate = decimal.MaxValue;
            var monthlyPayment = decimal.MaxValue;
            var totalRepayment = decimal.MaxValue;
            var totalInterest = decimal.MaxValue;

            // Act
            var creditRequest = new CreditRequestContract(clientName, clientAge, clientIncome, loanAmount, loanTerm, interestRate, monthlyPayment, totalRepayment, totalInterest);

            // Assert
            Assert.That(creditRequest.ClientName, Is.EqualTo(clientName));
            Assert.That(creditRequest.ClientAge, Is.EqualTo(clientAge));
            Assert.That(creditRequest.ClientIncome, Is.EqualTo(clientIncome));
            Assert.That(creditRequest.LoanAmount, Is.EqualTo(loanAmount));
            Assert.That(creditRequest.LoanTerm, Is.EqualTo(loanTerm));
            Assert.That(creditRequest.InterestRate, Is.EqualTo(interestRate));
            Assert.That(creditRequest.MonthlyPayment, Is.EqualTo(monthlyPayment));
            Assert.That(creditRequest.TotalRepayment, Is.EqualTo(totalRepayment));
            Assert.That(creditRequest.TotalInterest, Is.EqualTo(totalInterest));
        }
    }
}
