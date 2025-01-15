namespace RGR.IO.Abstractions
{
    public interface IFile
    {
        string ReadAllText(string path);

        void WriteAllText(string path, string content);

        Task<string> ReadAllTextAsync(string path);

        Task WriteAllTextAsync(string path, string content);

        bool Exists(string path);

        void Delete(string path);

        void Copy(string sourcePath, string destinationPath, bool overwrite);

        void Move(string sourcePath, string destinationPath);

        string GetExtension(string path);

        string GetFileNameWithoutExtension(string path);

        string GetFileName(string path);

        string? GetDirectoryName(string path);

        void Create(string path);
    }
}
