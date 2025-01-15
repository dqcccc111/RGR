using Microsoft.Extensions.Logging;
using RGR.Abstractions;
using RGR.Services.Abstractions;

namespace RGR
{
    public class Application : IApplication
    {
        private readonly IMainLoopService _mainLoopService;
        private readonly ILogger _logger;

        public Application(ILogger<Application> logger, IMainLoopService mainLoopService)
        {
            _mainLoopService = mainLoopService;
            _logger = logger;
        }

        public async Task Run(CancellationToken token)
        {
            _logger.LogInformation("Application started.");
            await _mainLoopService.ExecuteAsync(token);
        }

        public async Task Stop()
        {
            await _mainLoopService.StopTaskAsync();
        }
    }
}
