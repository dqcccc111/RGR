using Moq;
using Microsoft.Extensions.Logging;
using RGR.Core.Services.Helpers;

namespace RGR.Core.Tests.ServicesTests.HelpersTests
{
    public class CreditEligibilityCheckerTests
    {
        private Mock<ILogger<CreditEligibilityChecker>> _loggerMock;
        private CreditEligibilityChecker _checker;

        [SetUp]
        public void Setup()
        {
            _loggerMock = new Mock<ILogger<CreditEligibilityChecker>>();
            _checker = new CreditEligibilityChecker(_loggerMock.Object);
        }

        [Test]
        public void CheckEligibility_Approved_ReturnsTrue()
        {
            // Arrange
            int clientAge = 25;
            decimal clientIncome = 50000;
            decimal loanAmount = 100000;
            int years = 10;

            // Act
            bool result = _checker.CheckEligibility(clientAge, clientIncome, loanAmount, years);

            // Assert
            Assert.That(result, Is.True);
            _loggerMock.Verify(
                x => x.Log(
                    LogLevel.Information,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("approved")),
                    null,
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()
                ),
                Times.Once);
        }

        [Test]
        public void CheckEligibility_RejectedByAge_ReturnsFalse()
        {
            // Arrange
            int clientAge = 20;
            decimal clientIncome = 50000;
            decimal loanAmount = 100000;
            int years = 10;

            // Act
            bool result = _checker.CheckEligibility(clientAge, clientIncome, loanAmount, years);

            // Assert
            Assert.That(result, Is.False);
            _loggerMock.Verify(
                x => x.Log(
                    LogLevel.Information,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("rejected")),
                    null,
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()
                ),
                Times.Once);
        }

        [Test]
        public void CheckEligibility_RejectedByIncome_ReturnsFalse()
        {
            // Arrange
            int clientAge = 25;
            decimal clientIncome = 1000;
            decimal loanAmount = 100000;
            int years = 10;

            // Act
            bool result = _checker.CheckEligibility(clientAge, clientIncome, loanAmount, years);

            // Assert
            Assert.That(result, Is.False);
            _loggerMock.Verify(
                x => x.Log(
                    LogLevel.Information,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("rejected")),
                    null,
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()
                ),
                Times.Once);
        }
    }
}