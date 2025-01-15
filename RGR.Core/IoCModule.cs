using Autofac;
using RGR.Core.Services.Abstractions;
using RGR.Core.Services;
using Microsoft.Extensions.Configuration;
using RGR.Core.Services.Helpers.Abstractions;
using RGR.Core.Services.Helpers;
using static RGR.Core.Services.MortgageService;

namespace RGR.Core
{
    public class IoCModule : Module
    {
        private readonly IConfiguration _configuration;

        public IoCModule(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<MortgageCalculator>()
                   .As<IMortgageCalculator>()
                   .SingleInstance();

            builder.RegisterType<CreditEligibilityChecker>()
                   .As<ICreditEligibilityChecker>()
                   .SingleInstance();

            builder.RegisterType<FileService>()
                   .As<IFileService>()
                   .SingleInstance();

            builder.RegisterType<ConsoleService>()
                   .As<IConsoleService>()
                   .SingleInstance();

            builder.RegisterType<MortgageService>()
                   .As<IMortgageService>()
                   .InstancePerDependency();
        }
    }
}
