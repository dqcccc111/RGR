using NUnit.Framework;
using RGR.Core.Contracts;

namespace RGR.Core.Tests.ContractsTests
{
    [TestFixture]
    public class MortgageCalculationResponseContractTests
    {
        // Позитивный тест для конструктора
        [Test]
        public void Constructor_ValidValues_CreatesMortgageCalculationResponseContract()
        {
            // Arrange
            var request = new MortgageRequestContract(100000m, 10, 5.5m); // Пример инициализации (зависит от конструктора MortgageRequestContract)
            var result = new MortgageResultContract(1000m, 12000m, 50000m); // Пример инициализации (зависит от конструктора MortgageResultContract)

            // Act
            var response = new MortgageCalculationResponseContract(request, result);

            // Assert
            Assert.That(response.Request, Is.EqualTo(request));
            Assert.That(response.Result, Is.EqualTo(result));
        }

        // Тест на проверку корректности значений в Request
        [Test]
        public void Constructor_ValidRequest_CreatesCorrectRequest()
        {
            // Arrange
            var request = new MortgageRequestContract(100000m, 10, 5.5m);
            var result = new MortgageResultContract(1000m, 12000m, 50000m);

            // Act
            var response = new MortgageCalculationResponseContract(request, result);

            // Assert
            Assert.That(response.Request.Amount, Is.EqualTo(100000m));
            Assert.That(response.Request.Years, Is.EqualTo(10));
            Assert.That(response.Request.InterestRate, Is.EqualTo(5.5m));
        }

        // Тест на проверку корректности значений в Result
        [Test]
        public void Constructor_ValidResult_CreatesCorrectResult()
        {
            // Arrange
            var request = new MortgageRequestContract(100000m, 10, 5.5m);
            var result = new MortgageResultContract(1000m, 12000m, 50000m);

            // Act
            var response = new MortgageCalculationResponseContract(request, result);

            // Assert
            Assert.That(response.Result.MonthlyPayment, Is.EqualTo(1000m));
            Assert.That(response.Result.TotalRepayment, Is.EqualTo(12000m));
            Assert.That(response.Result.TotalInterest, Is.EqualTo(50000m));
        }
    }
}
