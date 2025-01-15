namespace RGR.Abstractions
{
    public interface IApplication
    {
        Task Run(CancellationToken token);

        Task Stop();
    }
}
