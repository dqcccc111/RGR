using RGR.IO.Abstractions;
using System.Runtime.CompilerServices;
using System.Text;

[assembly: InternalsVisibleTo("RGR.Core.Tests")]
[assembly: InternalsVisibleTo("RGR.IO.Tests")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2, PublicKey=0024000004800000940000000602000000240000525341310004000001000100c547cac37abd99c8db225ef2f6c8a3602f3b3606cc9891605d02baa56104f4cfc0734aa39b93bf7852f7d9266654753cc297e7d2edfe0bac1cdcf9f717241550e0a7b191195b7667bb4f64bcb8e2121380fd1d9d46ad2d92d2d15605093924cceaf74c4861eff62abf69b9291ed0a340e113be11e6a7d3113e92484cf7045cc7")]
namespace RGR.IO
{
    internal class Console : IConsole
    {
        public Console(Encoding outputEncoding)
        {
            OutputEncoding = outputEncoding ?? throw new ArgumentNullException(nameof(outputEncoding));
        }

        public Console()
        {
        }

        public Encoding OutputEncoding
        {
            get => System.Console.OutputEncoding;

            set => System.Console.OutputEncoding = value;
        }

        public ConsoleColor TextColor
        {
            get => System.Console.ForegroundColor;
            set => System.Console.ForegroundColor = value;
        }

        public ConsoleColor BackgroundColor
        {
            get => System.Console.BackgroundColor;
            set => System.Console.BackgroundColor = value;
        }

        public void Write(string message)
        {
            System.Console.Write(message);
        }

        public void WriteLine(string message)
        {
            System.Console.WriteLine(message);
        }

        public void Clear()
        {
            System.Console.Clear();
        }

        public ConsoleKeyInfo ReadKey(bool intercept)
        {
            return System.Console.ReadKey(intercept: true);
        }

        public ConsoleKeyInfo ReadKey()
        {
            return System.Console.ReadKey();
        }

        public string? ReadLine()
        {
            return System.Console.ReadLine();
        }

        public void ResetColor()
        {
            System.Console.ResetColor();
        }

        public void SetCursorPosition(int left, int top)
        {
            System.Console.SetCursorPosition(left, top);
        }

        public (int Left, int Top) GetCursorPosition()
        {
            return System.Console.GetCursorPosition(); ;
        }

        public string GetCurrentEncoding()
        {
            return OutputEncoding.EncodingName;
        }
    }
}
