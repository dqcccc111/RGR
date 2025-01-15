using System.Text;

namespace RGR.IO.Abstractions
{
    public interface IConsole
    {
        Encoding OutputEncoding { get; set; }

        ConsoleColor TextColor { get; set; }

        ConsoleColor BackgroundColor { get; set; }

        void Write(string message);

        void WriteLine(string message);

        void Clear();

        ConsoleKeyInfo ReadKey(bool intercept);

        ConsoleKeyInfo ReadKey();

        string? ReadLine();

        void ResetColor();

        void SetCursorPosition(int left, int top);

        (int Left, int Top) GetCursorPosition();

        string GetCurrentEncoding();
    }
}
