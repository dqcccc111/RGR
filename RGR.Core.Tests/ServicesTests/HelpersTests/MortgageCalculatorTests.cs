using Moq;
using Microsoft.Extensions.Logging;
using RGR.Core.Services.Helpers;
using RGR.Core.Contracts;

namespace RGR.Core.Tests.ServicesTests.HelpersTests
{
    public class MortgageCalculatorTests
    {
        private Mock<ILogger<MortgageCalculator>> _loggerMock;
        private MortgageCalculator _calculator;

        [SetUp]
        public void Setup()
        {
            _loggerMock = new Mock<ILogger<MortgageCalculator>>();
            _calculator = new MortgageCalculator(_loggerMock.Object);
        }

        [Test]
        public async Task CalculateMortgage_ValidInput_ReturnsCorrectResult()
        {
            // Arrange
            var request = new MortgageRequestContract
            (
                amount: 200000,
                years: 30,
                interestRate: 5.0m
            );

            // Act
            var result = await _calculator.CalculateMortgage(request);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Request, Is.EqualTo(request));
            Assert.That(result.Result.MonthlyPayment, Is.GreaterThan(0));
            Assert.That(result.Result.TotalRepayment, Is.GreaterThan(request.Amount));
            Assert.That(result.Result.TotalInterest, Is.EqualTo(result.Result.TotalRepayment - request.Amount));

            _loggerMock.Verify(
                x => x.Log(
                    LogLevel.Information,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Starting mortgage calculation")),
                    null,
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()
                ),
                Times.Once);

            _loggerMock.Verify(
                x => x.Log(
                    LogLevel.Information,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Mortgage calculation completed")),
                    null,
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()
                ),
                Times.Once);
        }

        [Test]
        public void CalculateMortgage_NullRequest_ThrowsArgumentException()
        {
            // Arrange
            MortgageRequestContract request = null;

            // Act & Assert
            var ex = Assert.ThrowsAsync<ArgumentNullException>(async () => await _calculator.CalculateMortgage(request));
            Assert.That(ex.Message, Is.EqualTo("Request contract cannot be null (Parameter 'requestContract')"));

            _loggerMock.Verify(
                x => x.Log(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Invalid request contract")),
                    null,
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()
                ),
                Times.Once);
        }

        [Test]
        public void CalculateMortgage_InvalidAmount_ThrowsArgumentException()
        {
            // Arrange
            var request = new MortgageRequestContract
            (
                amount: 0,
                years: 30,
                interestRate: 5.0m
            );

            // Act & Assert
            var ex = Assert.ThrowsAsync<ArgumentException>(async () => await _calculator.CalculateMortgage(request));
            Assert.That(ex.Message, Is.EqualTo("Loan amount must be greater than zero. (Parameter 'Amount')"));

            _loggerMock.Verify(
                x => x.Log(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Invalid loan amount")),
                    null,
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()
                ),
                Times.Once);
        }

        [Test]
        public void CalculateMortgage_InvalidYears_ThrowsArgumentException()
        {
            // Arrange
            var request = new MortgageRequestContract
            (
                amount: 200000,
                years: 0,
                interestRate: 5.0m
            );

            // Act & Assert
            var ex = Assert.ThrowsAsync<ArgumentException>(async () => await _calculator.CalculateMortgage(request));
            Assert.That(ex.Message, Is.EqualTo("Loan term in years must be greater than zero. (Parameter 'Years')"));

            _loggerMock.Verify(
                x => x.Log(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Invalid loan term in years")),
                    null,
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()
                ),
                Times.Once);
        }

        [Test]
        public void CalculateMortgage_InvalidInterestRate_ThrowsArgumentException()
        {
            // Arrange
            var request = new MortgageRequestContract
            (
                amount: 200000,
                years: 30,
                interestRate: 0m
            );

            // Act & Assert
            var ex = Assert.ThrowsAsync<ArgumentException>(async () => await _calculator.CalculateMortgage(request));
            Assert.That(ex.Message, Is.EqualTo("Interest rate must be greater than zero. (Parameter 'InterestRate')"));

            _loggerMock.Verify(
                x => x.Log(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Invalid interest rate")),
                    null,
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()
                ),
                Times.Once);
        }
    }
}
