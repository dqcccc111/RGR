using Moq;
using RGR.Core.Contracts;
using RGR.Core.Services;
using RGR.Core.Services.Abstractions;
using RGR.Core.Services.Helpers.Abstractions;
using RGR.IO.Abstractions;
using Microsoft.Extensions.Logging;

namespace RGR.Core.Tests.ServicesTests
{
    [TestFixture]
    public class MortgageServiceTests
    {
        private Mock<ILogger<MortgageService>> _mockLogger;
        private Mock<ICreditEligibilityChecker> _mockEligibilityChecker;
        private Mock<IMortgageCalculator> _mockMortgageCalculator;
        private Mock<IConsole> _mockConsole;
        private Mock<IFileService> _mockFileService;
        private Mock<IConsoleService> _mockConsoleService;
        private MortgageService _mortgageService;

        [SetUp]
        public void SetUp()
        {
            _mockLogger = new Mock<ILogger<MortgageService>>();
            _mockEligibilityChecker = new Mock<ICreditEligibilityChecker>();
            _mockMortgageCalculator = new Mock<IMortgageCalculator>();
            _mockConsole = new Mock<IConsole>();
            _mockFileService = new Mock<IFileService>();
            _mockConsoleService = new Mock<IConsoleService>();

            _mortgageService = new MortgageService(
                _mockLogger.Object,
                _mockEligibilityChecker.Object,
                _mockMortgageCalculator.Object,
                _mockConsole.Object,
                _mockFileService.Object,
                _mockConsoleService.Object
            );
        }

        // Тест для HandleCreditRequest (позитивный случай)
        [Test]
        public async Task HandleCreditRequest_Approved_SavesCreditRequestAndDisplaysResults()
        {
            // Arrange
            var mockMortgageResult = new MortgageCalculationResponseContract(
                new MortgageRequestContract(1000000m, 20, 5.5m),
                new MortgageResultContract(10000m, 1200000m, 200000m)
            );
            _mockConsoleService.Setup(c => c.GetClientFullName()).Returns("John Doe");
            _mockConsoleService.Setup(c => c.GetClientAge()).Returns(30);
            _mockConsoleService.Setup(c => c.GetClientIncome()).Returns(50000m);
            _mockConsoleService.Setup(c => c.GetLoanAmount()).Returns(1000000m);
            _mockConsoleService.Setup(c => c.GetYears()).Returns(20);
            _mockEligibilityChecker.Setup(e => e.CheckEligibility(30, 50000m, 1000000m, 20)).Returns(true);
            _mockConsoleService.Setup(c => c.GetMortgageRequest(1000000m, 20)).Returns(new MortgageRequestContract(1000000m, 20, 5.5m));
            _mockMortgageCalculator.Setup(m => m.CalculateMortgage(It.IsAny<MortgageRequestContract>())).ReturnsAsync(mockMortgageResult);
            _mockFileService.Setup(f => f.SaveCreditRequest(It.IsAny<CreditRequestContract>(), It.IsAny<string>()));

            // Act
            await _mortgageService.HandleCreditRequest();

            // Assert
            _mockConsole.Verify(c => c.WriteLine(It.IsAny<string>()), Times.AtLeastOnce);
            _mockFileService.Verify(f => f.SaveCreditRequest(It.IsAny<CreditRequestContract>(), It.IsAny<string>()), Times.Once);
        }

        // Тест для HandleCreditRequest (отказ в кредите)
        [Test]
        public async Task HandleCreditRequest_Rejected_DisplaysRejectionMessage()
        {
            // Arrange
            _mockConsoleService.Setup(c => c.GetClientFullName()).Returns("John Doe");
            _mockConsoleService.Setup(c => c.GetClientAge()).Returns(30);
            _mockConsoleService.Setup(c => c.GetClientIncome()).Returns(50000m);
            _mockConsoleService.Setup(c => c.GetLoanAmount()).Returns(1000000m);
            _mockConsoleService.Setup(c => c.GetYears()).Returns(20);
            _mockEligibilityChecker.Setup(e => e.CheckEligibility(30, 50000m, 1000000m, 20)).Returns(false);

            // Act
            await _mortgageService.HandleCreditRequest();

            // Assert
            _mockConsole.Verify(c => c.WriteLine(It.Is<string>(s => s.Contains("Credit request rejected"))), Times.Once);
            _mockFileService.Verify(f => f.SaveCreditRequest(It.IsAny<CreditRequestContract>(), It.IsAny<string>()), Times.Never);
        }

        // Тест для HandleMortgageCalculation (успешный расчет)
        [Test]
        public async Task HandleMortgageCalculation_Success_DisplaysResults()
        {
            // Arrange
            var mockMortgageResult = new MortgageCalculationResponseContract(
                new MortgageRequestContract(1000000m, 20, 5.5m),
                new MortgageResultContract(10000m, 1200000m, 200000m)
            );
            _mockConsoleService.Setup(c => c.GetMortgageRequest()).Returns(new MortgageRequestContract(1000000m, 20, 5.5m));
            _mockMortgageCalculator.Setup(m => m.CalculateMortgage(It.IsAny<MortgageRequestContract>())).ReturnsAsync(mockMortgageResult);

            // Act
            await _mortgageService.HandleMortgageCalculation();

            // Assert
            _mockConsole.Verify(c => c.WriteLine(It.IsAny<string>()), Times.AtLeastOnce);
        }

        // Тест для HandleMortgageCalculation (ошибка при расчете)
        [Test]
        public async Task HandleMortgageCalculation_Failure_DisplaysErrorMessage()
        {
            // Arrange
            _mockConsoleService.Setup(c => c.GetMortgageRequest()).Returns(new MortgageRequestContract(1000000m, 20, 5.5m));
            _mockMortgageCalculator.Setup(m => m.CalculateMortgage(It.IsAny<MortgageRequestContract>())).ThrowsAsync(new Exception("Calculation error"));

            // Act
            await _mortgageService.HandleMortgageCalculation();

            // Assert
            _mockConsole.Verify(c => c.WriteLine(It.Is<string>(s => s.Contains("An error occurred"))), Times.Once);
        }
    }
}
