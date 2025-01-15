using Microsoft.Extensions.Logging;
using RGR.Core.Services.Abstractions;
using RGR.Services.Abstractions;
using RGR.IO.Abstractions;

namespace RGR.Services
{
    public class MainLoopService : IMainLoopService
    {
        private readonly ILogger<MainLoopService> _logger;
        private readonly IMortgageService _mortgageService;
        private readonly IConsole _console;
        private bool _isRunning;

        public MainLoopService(ILogger<MainLoopService> logger, IMortgageService mortgageService, IConsole console)
        {
            _logger = logger;
            _mortgageService = mortgageService;
            _console = console;
            _isRunning = true;
        }

        public async Task ExecuteAsync(CancellationToken token)
        {
            _logger.LogInformation("Main loop started.");

            try
            {
                while (_isRunning && !token.IsCancellationRequested)
                {
                    _logger.LogInformation("Main loop iteration...");
                    
                    _console.Clear();
                    _console.OutputEncoding = System.Text.Encoding.UTF8;
                    _console.WriteLine("Welcome to the mortgage loan system.");
                    _console.WriteLine("1. Calculate mortgage");
                    _console.WriteLine("2. Apply for a loan");
                    _console.WriteLine("3. Exit");
                    _console.Write("Select an option: ");

                    var input = _console.ReadLine();

                    try
                    {
                        if (input == "1")
                        {
                            _logger.LogInformation("Mortgage calculation selected.");
                            await _mortgageService.HandleMortgageCalculation();
                        }
                        else if (input == "2")
                        {
                            _logger.LogInformation("Credit requesting selected");
                            await _mortgageService.HandleCreditRequest();
                        }
                        else if (input == "3")
                        {
                            _logger.LogInformation("Exiting the program.");
                            Environment.Exit(0);
                        }
                        else
                        {
                            _logger.LogWarning("Incorrect input.");
                            _console.WriteLine("Incorrect input. Please try again.");
                            await Task.Delay(1000);
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, ex.Message + Environment.NewLine);
                        _console.WriteLine(ex.Message);
                        _console.WriteLine("Press any key to continue...");
                        _console.ReadKey();
                        _console.Clear();
                    }
                }
            }
            catch (OperationCanceledException)
            {
                _logger.LogInformation("Main loop cancellation requested.");
            }
            finally
            {
                _logger.LogInformation("Main loop stopped.");
            }
        }

        private Task StopTask()
        {
            _logger.LogInformation("Stopping the main loop...");
            _isRunning = false;
            return Task.CompletedTask;
        }

        public async Task StopTaskAsync()
        {
            await StopTask();
        }
    }
}
