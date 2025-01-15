using Autofac;
using Microsoft.Extensions.Configuration;
using RGR.IO.Abstractions;

namespace RGR.IO
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
            builder.RegisterType<IO.File>()
               .As<IFile>()
               .InstancePerDependency();

            builder.RegisterType<IO.Console>()
               .As<IConsole>()
               .InstancePerDependency();
        }
    }
}
