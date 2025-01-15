using Moq;
using NUnit.Framework;
using RGR.Core.Services;
using RGR.IO.Abstractions;

namespace RGR.Core.Tests.ServicesTests
{
    public class ConsoleServiceTests
    {
        private Mock<IConsole> _mockConsole;
        private ConsoleService _consoleService;

        [SetUp]
        public void Setup()
        {
            _mockConsole = new Mock<IConsole>();
            _consoleService = new ConsoleService(_mockConsole.Object);
        }

        // Позитивный тест для GetClientFullName
        [Test]
        public void GetClientFullName_ValidInput_ReturnsFullName()
        {
            // Arrange
            _mockConsole.Setup(c => c.ReadLine()).Returns("John Doe");

            // Act
            var result = _consoleService.GetClientFullName();

            // Assert
            Assert.That(result, Is.EqualTo("John Doe"));
            _mockConsole.Verify(c => c.Write(It.IsAny<string>()), Times.Once);
        }

        // Негативный тест для GetClientFullName (пустое имя)
        [Test]
        public void GetClientFullName_EmptyInput_PromptsForValidInput()
        {
            // Arrange
            _mockConsole.SetupSequence(c => c.ReadLine())
                        .Returns("")    // Пустой ввод на первом шаге
                        .Returns("John Doe"); // Вводим правильное имя на втором шаге

            // Act
            var result = _consoleService.GetClientFullName();

            // Assert
            Assert.That(result, Is.EqualTo(string.Empty));

            _mockConsole.Verify(c => c.WriteLine("Client name cannot be empty. Please enter a valid name."), Times.Exactly(1));
        }


        // Тест на GetClientAge с правильным вводом
        [Test]
        public void GetClientAge_ValidAge_ReturnsAge()
        {
            // Arrange
            _mockConsole.Setup(c => c.ReadLine()).Returns("25");

            // Act
            var result = _consoleService.GetClientAge();

            // Assert
            Assert.That(result, Is.EqualTo(25));
        }

        // Негативный тест на GetClientAge с неправильным вводом
        [Test]
        public void GetClientAge_InvalidInput_RetriesUntilValidInput()
        {
            // Arrange
            _mockConsole.SetupSequence(c => c.ReadLine())
                        .Returns("abc")
                        .Returns("25");

            // Act
            var result = _consoleService.GetClientAge();

            // Assert
            Assert.That(result, Is.EqualTo(25));
            _mockConsole.Verify(c => c.WriteLine("Incorrect format. Please enter a valid age (over 21)."), Times.Once);
        }

        // Тест на GetClientIncome с правильным вводом
        [Test]
        public void GetClientIncome_ValidIncome_ReturnsIncome()
        {
            // Arrange
            _mockConsole.Setup(c => c.ReadLine()).Returns("50000");

            // Act
            var result = _consoleService.GetClientIncome();

            // Assert
            Assert.That(result, Is.EqualTo(50000));
        }

        // Тест на GetLoanAmount с правильным вводом
        [Test]
        public void GetLoanAmount_ValidLoanAmount_ReturnsLoanAmount()
        {
            // Arrange
            _mockConsole.Setup(c => c.ReadLine()).Returns("100000");

            // Act
            var result = _consoleService.GetLoanAmount();

            // Assert
            Assert.That(result, Is.EqualTo(100000));
        }

        // Проверка исключений для неверного ввода на ReadDecimal
        [Test]
        public void ReadDecimal_InvalidInput_ThrowsException()
        {
            // Arrange
            _mockConsole.SetupSequence(c => c.ReadLine())
                        .Returns("abc")
                        .Returns("1000");

            // Act
            var result = _consoleService.ReadDecimal("Enter amount: ", "Invalid input!");

            // Assert
            Assert.That(result, Is.EqualTo(1000));
            _mockConsole.Verify(c => c.WriteLine("Invalid input!"), Times.Once);
        }

        // Тест на GetMortgageRequest с использованием параметров
        [Test]
        public void GetMortgageRequest_ValidRequest_ReturnsRequest()
        {
            // Arrange
            _mockConsole.Setup(c => c.ReadLine()).Returns("5"); // Для interest rate

            // Act
            var request = _consoleService.GetMortgageRequest(100000, 10);

            // Assert
            Assert.That(request.Amount, Is.EqualTo(100000));
            Assert.That(request.Years, Is.EqualTo(10));
            Assert.That(request.InterestRate, Is.EqualTo(5m));
        }

        // Проверка исключения для неверного ввода (позитивный случай)
        [Test]
        public void GetYears_InvalidInput_PromptsUntilValidInput()
        {
            // Arrange
            _mockConsole.SetupSequence(c => c.ReadLine())
                        .Returns("abc")
                        .Returns("10");

            // Act
            var result = _consoleService.GetYears();

            // Assert
            Assert.That(result, Is.EqualTo(10));
            _mockConsole.Verify(c => c.WriteLine("Incorrect format. Please enter a valid interest years"), Times.Once);
        }
    }
}
