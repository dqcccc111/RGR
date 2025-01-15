using Autofac;
using Microsoft.Extensions.Configuration;
using RGR.Core.Services;
using RGR.Core.Services.Abstractions;
using RGR.Core.Services.Helpers.Abstractions;
using Moq;
using RGR.Core.Services.Helpers;
using Microsoft.Extensions.Logging;
using RGR.IO.Abstractions;
using NLog.Extensions.Logging;

namespace RGR.Core.Tests
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

            // Регистрация ILoggerFactory и ILogger
            builder.RegisterInstance(LoggerFactory.Create(loggingBuilder =>
            {
                loggingBuilder.AddNLog();
            })).As<ILoggerFactory>().SingleInstance();

            builder.RegisterGeneric(typeof(Logger<>)).As(typeof(ILogger<>)).SingleInstance();

            // Регистрация конфигурации
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();

            // Регистрация модуля IoC
            builder.RegisterModule(new RGR.IO.IoCModule(_mockConfiguration.Object));
            builder.RegisterModule(new RGR.Core.IoCModule(_mockConfiguration.Object));

            // Регистрация IConsole для ConsoleService
            builder.RegisterType<RGR.IO.Console>().As<IConsole>().SingleInstance();

            _container = builder.Build();
        }

        [TearDown]
        public void TearDown()
        {
            // Dispose the container to release resources
            _container.Dispose();
        }

        [Test]
        public void TestMortgageCalculatorRegistration()
        {
            // Act
            var mortgageCalculator = _container.Resolve<IMortgageCalculator>();

            // Assert
            Assert.That(mortgageCalculator, Is.Not.Null);
            Assert.That(mortgageCalculator, Is.InstanceOf<MortgageCalculator>());
        }

        [Test]
        public void TestCreditEligibilityCheckerRegistration()
        {
            // Act
            var eligibilityChecker = _container.Resolve<ICreditEligibilityChecker>();

            // Assert
            Assert.That(eligibilityChecker, Is.Not.Null);
            Assert.That(eligibilityChecker, Is.InstanceOf<CreditEligibilityChecker>());
        }

        [Test]
        public void TestFileServiceRegistration()
        {
            // Act
            var fileService = _container.Resolve<IFileService>();

            // Assert
            Assert.That(fileService, Is.Not.Null);
            Assert.That(fileService, Is.InstanceOf<FileService>());
        }

        [Test]
        public void TestConsoleServiceRegistration()
        {
            // Act
            var consoleService = _container.Resolve<IConsoleService>();

            // Assert
            Assert.That(consoleService, Is.Not.Null);
            Assert.That(consoleService, Is.InstanceOf<ConsoleService>());
        }

        [Test]
        public void TestMortgageServiceRegistration()
        {
            // Act
            var mortgageService = _container.Resolve<IMortgageService>();

            // Assert
            Assert.That(mortgageService, Is.Not.Null);
            Assert.That(mortgageService, Is.InstanceOf<MortgageService>());
        }

        [Test]
        public void TestLifetimeForSingletons()
        {
            // Act
            var mortgageCalculator1 = _container.Resolve<IMortgageCalculator>();
            var mortgageCalculator2 = _container.Resolve<IMortgageCalculator>();

            // Assert
            Assert.That(mortgageCalculator1, Is.SameAs(mortgageCalculator2)); // Ensure they are the same instance
        }

        [Test]
        public void TestLifetimeForInstancePerDependency()
        {
            // Act
            var mortgageService1 = _container.Resolve<IMortgageService>();
            var mortgageService2 = _container.Resolve<IMortgageService>();

            // Assert
            Assert.That(mortgageService1, Is.Not.SameAs(mortgageService2)); // Ensure they are different instances
        }
    }
}
