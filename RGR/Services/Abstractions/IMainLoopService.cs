namespace RGR.Services.Abstractions
{
    public interface IMainLoopService
    {
        Task ExecuteAsync(CancellationToken token);
        Task StopTaskAsync();
    }
}
