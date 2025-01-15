using NUnit.Framework;
using RGR.Core.Contracts;
using System;

namespace RGR.Core.Tests.ContractsTests
{
    [TestFixture]
    public class MortgageRequestContractTests
    {
        // Позитивный тест для конструктора с валидными значениями
        [Test]
        public void Constructor_ValidValues_CreatesMortgageRequestContract()
        {
            // Arrange
            decimal amount = 500000m;
            int years = 20;
            decimal interestRate = 5.5m;

            // Act
            var request = new MortgageRequestContract(amount, years, interestRate);

            // Assert
            Assert.That(request.Amount, Is.EqualTo(amount));
            Assert.That(request.Years, Is.EqualTo(years));
            Assert.That(request.InterestRate, Is.EqualTo(interestRate));
        }

        // Тест для проверки корректности свойства Amount
        [Test]
        public void Constructor_ValidAmount_CreatesCorrectAmount()
        {
            // Arrange
            decimal amount = 500000m;
            int years = 20;
            decimal interestRate = 5.5m;

            // Act
            var request = new MortgageRequestContract(amount, years, interestRate);

            // Assert
            Assert.That(request.Amount, Is.EqualTo(amount));
        }

        // Тест для проверки корректности свойства Years
        [Test]
        public void Constructor_ValidYears_CreatesCorrectYears()
        {
            // Arrange
            decimal amount = 500000m;
            int years = 20;
            decimal interestRate = 5.5m;

            // Act
            var request = new MortgageRequestContract(amount, years, interestRate);

            // Assert
            Assert.That(request.Years, Is.EqualTo(years));
        }

        // Тест для проверки корректности свойства InterestRate
        [Test]
        public void Constructor_ValidInterestRate_CreatesCorrectInterestRate()
        {
            // Arrange
            decimal amount = 500000m;
            int years = 20;
            decimal interestRate = 5.5m;

            // Act
            var request = new MortgageRequestContract(amount, years, interestRate);

            // Assert
            Assert.That(request.InterestRate, Is.EqualTo(interestRate));
        }
    }
}
