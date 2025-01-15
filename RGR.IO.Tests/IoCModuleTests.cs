using Autofac;
using Microsoft.Extensions.Configuration;
using Moq;
using RGR.IO.Abstractions;

namespace RGR.IO.Tests
{
    [TestFixture]
    public class IoCModuleTests
    {
        private IContainer _container;
        private Mock<IConfiguration> _mockConfiguration;

        [SetUp]
        public void SetUp()
        {
            _mockConfiguration = new Mock<IConfiguration>();
            var builder = new ContainerBuilder();

            // Регистрация модуля IoC
            builder.RegisterModule(new IoCModule(_mockConfiguration.Object));

            // Строим контейнер
            _container = builder.Build();
        }

        [TearDown]
        public void TearDown()
        {
            // Освобождаем ресурсы
            _container.Dispose();
        }

        [Test]
        public void TestFileServiceRegistration()
        {
            // Act
            var fileService = _container.Resolve<IFile>();

            // Assert
            Assert.That(fileService, Is.Not.Null);
            Assert.That(fileService, Is.InstanceOf<IO.File>());
        }

        [Test]
        public void TestConsoleServiceRegistration()
        {
            // Act
            var consoleService = _container.Resolve<IConsole>();

            // Assert
            Assert.That(consoleService, Is.Not.Null);
            Assert.That(consoleService, Is.InstanceOf<IO.Console>());
        }

        [Test]
        public void TestLifetimeForFileService()
        {
            // Act
            var fileService1 = _container.Resolve<IFile>();
            var fileService2 = _container.Resolve<IFile>();

            // Assert
            Assert.That(fileService1, Is.Not.SameAs(fileService2)); // Проверяем, что это разные экземпляры
        }

        [Test]
        public void TestLifetimeForConsoleService()
        {
            // Act
            var consoleService1 = _container.Resolve<IConsole>();
            var consoleService2 = _container.Resolve<IConsole>();

            // Assert
            Assert.That(consoleService1, Is.Not.SameAs(consoleService2)); // Проверяем, что это разные экземпляры
        }
    }
}
