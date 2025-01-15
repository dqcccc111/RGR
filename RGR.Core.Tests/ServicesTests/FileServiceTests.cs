using Moq;
using RGR.Core.Contracts;
using RGR.Core.Services;
using RGR.IO.Abstractions;
using System.Text.Json;

namespace RGR.Core.Tests.ServicesTests
{
    [TestFixture]
    public class FileServiceTests
    {
        private Mock<IFile> _mockFile;
        private FileService _fileService;

        [SetUp]
        public void Setup()
        {
            _mockFile = new Mock<IFile>();
            _fileService = new FileService(_mockFile.Object);
        }

        // Позитивный тест на успешное сохранение запроса
        [Test]
        public void SaveCreditRequest_ValidRequest_SavesToFile()
        {
            // Arrange
            var creditRequest = new CreditRequestContract(
                "John Doe",
                30,
                50000m,
                100000m,
                15,
                5.5m,
                8500m,
                153000m,
                53000m
            );
            var filePath = "creditRequest.json";

            // Act
            _fileService.SaveCreditRequest(creditRequest, filePath);

            // Assert
            var expectedJson = JsonSerializer.Serialize(creditRequest, new JsonSerializerOptions { WriteIndented = true });
            _mockFile.Verify(f => f.WriteAllText(filePath, expectedJson), Times.Once);
        }

        // Негативный тест на SaveCreditRequest при ошибке записи в файл
        [Test]
        public void SaveCreditRequest_WriteToFileThrowsException_ThrowsIOException()
        {
            // Arrange
            var creditRequest = new CreditRequestContract(
                "John Doe",
                30,
                50000m,
                100000m,
                15,
                5.5m,
                8500m,
                153000m,
                53000m
            );
            var filePath = "creditRequest.json";

            // Настроим мок так, чтобы при вызове WriteAllText возникало исключение
            _mockFile.Setup(f => f.WriteAllText(filePath, It.IsAny<string>())).Throws(new IOException("Failed to write to file"));

            // Act & Assert
            var ex = Assert.Throws<IOException>(() => _fileService.SaveCreditRequest(creditRequest, filePath));
            Assert.That(ex.Message, Is.EqualTo("Failed to write to file"));
        }

        // Тест на проверку сериализации объекта
        [Test]
        public void SaveCreditRequest_SerializesObjectCorrectly()
        {
            // Arrange
            var creditRequest = new CreditRequestContract(
                "John Doe",
                30,
                50000m,
                100000m,
                15,
                5.5m,
                8500m,
                153000m,
                53000m
            );
            var filePath = "creditRequest.json";

            // Act
            _fileService.SaveCreditRequest(creditRequest, filePath);

            // Assert
            var expectedJson = JsonSerializer.Serialize(creditRequest, new JsonSerializerOptions { WriteIndented = true });
            _mockFile.Verify(f => f.WriteAllText(filePath, expectedJson), Times.Once);
        }

        // Тест на неправильный путь файла (например, пустой путь)
        [Test]
        public void SaveCreditRequest_EmptyFilePath_ThrowsArgumentException()
        {
            // Arrange
            var creditRequest = new CreditRequestContract(
                "John Doe",
                30,
                50000m,
                100000m,
                15,
                5.5m,
                8500m,
                153000m,
                53000m
            );
            var filePath = string.Empty;

            // Act & Assert
            var ex = Assert.Throws<ArgumentNullException>(() => _fileService.SaveCreditRequest(creditRequest, filePath));
            Assert.That(ex.Message, Is.EqualTo("Value cannot be null. (Parameter 'filePath')"));
        }

        // Тест на сериализацию пустого объекта
        [Test]
        public void SaveCreditRequest_EmptyCreditRequest_SavesToFile()
        {
            // Arrange
            var creditRequest = new CreditRequestContract(
                "",
                0,
                0m,
                0m,
                0,
                0m,
                0m,
                0m,
                0m
            );
            var filePath = "emptyCreditRequest.json";

            // Act
            _fileService.SaveCreditRequest(creditRequest, filePath);

            // Assert
            var expectedJson = JsonSerializer.Serialize(creditRequest, new JsonSerializerOptions { WriteIndented = true });
            _mockFile.Verify(f => f.WriteAllText(filePath, expectedJson), Times.Once);
        }
    }
}
