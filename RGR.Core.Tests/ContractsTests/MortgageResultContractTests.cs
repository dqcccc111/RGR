using RGR.Core.Contracts;

namespace RGR.Core.Tests.ContractsTests
{
    [TestFixture]
    public class MortgageResultContractTests
    {
        // Позитивный тест для конструктора
        [Test]
        public void Constructor_ValidValues_CreatesMortgageResultContract()
        {
            // Arrange
            decimal monthlyPayment = 1000m;
            decimal totalRepayment = 12000m;
            decimal totalInterest = 5000m;

            // Act
            var result = new MortgageResultContract(monthlyPayment, totalRepayment, totalInterest);

            // Assert
            Assert.That(result.MonthlyPayment, Is.EqualTo(monthlyPayment));
            Assert.That(result.TotalRepayment, Is.EqualTo(totalRepayment));
            Assert.That(result.TotalInterest, Is.EqualTo(totalInterest));
        }

        // Тест на проверку значения MonthlyPayment
        [Test]
        public void Constructor_ValidMonthlyPayment_CreatesCorrectMonthlyPayment()
        {
            // Arrange
            decimal monthlyPayment = 1000m;
            decimal totalRepayment = 12000m;
            decimal totalInterest = 5000m;

            // Act
            var result = new MortgageResultContract(monthlyPayment, totalRepayment, totalInterest);

            // Assert
            Assert.That(result.MonthlyPayment, Is.EqualTo(1000m));
        }

        // Тест на проверку значения TotalRepayment
        [Test]
        public void Constructor_ValidTotalRepayment_CreatesCorrectTotalRepayment()
        {
            // Arrange
            decimal monthlyPayment = 1000m;
            decimal totalRepayment = 12000m;
            decimal totalInterest = 5000m;

            // Act
            var result = new MortgageResultContract(monthlyPayment, totalRepayment, totalInterest);

            // Assert
            Assert.That(result.TotalRepayment, Is.EqualTo(12000m));
        }

        // Тест на проверку значения TotalInterest
        [Test]
        public void Constructor_ValidTotalInterest_CreatesCorrectTotalInterest()
        {
            // Arrange
            decimal monthlyPayment = 1000m;
            decimal totalRepayment = 12000m;
            decimal totalInterest = 5000m;

            // Act
            var result = new MortgageResultContract(monthlyPayment, totalRepayment, totalInterest);

            // Assert
            Assert.That(result.TotalInterest, Is.EqualTo(5000m));
        }
    }
}
