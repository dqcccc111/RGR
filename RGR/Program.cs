using Autofac;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using RGR.Abstractions;
using RGR.Services;
using RGR.Services.Abstractions;
using Microsoft.Extensions.Configuration;

namespace RGR
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var container = ConfigureContainer();

            using (var scope = container.BeginLifetimeScope())
            {
                var app = scope.Resolve<IApplication>();
                var cts = new CancellationTokenSource();

                Console.CancelKeyPress += (_, e) =>
                {
                    e.Cancel = true;
                    app.Stop().Wait();
                    cts.Cancel();
                };

                await app.Run(cts.Token);
            }
        }

        private static IContainer ConfigureContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterInstance(LoggerFactory.Create(loggingBuilder =>
            {
                loggingBuilder.AddNLog();
            })).As<ILoggerFactory>().SingleInstance();

            builder.RegisterGeneric(typeof(Logger<>)).As(typeof(ILogger<>)).SingleInstance();

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();

            RegisterModules(builder, configuration);

            builder.RegisterType<Application>().As<IApplication>().SingleInstance();
            builder.RegisterType<MainLoopService>().As<IMainLoopService>().SingleInstance();

            return builder.Build();
        }

        private static void RegisterModules(ContainerBuilder builder, IConfiguration configuration)
        {
            builder.RegisterModule(new RGR.IO.IoCModule(configuration));
            builder.RegisterModule(new RGR.Core.IoCModule(configuration));
        }
    }
}