using RGR.IO.Abstractions;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("RGR.Core.Tests")]
[assembly: InternalsVisibleTo("RGR.IO.Tests")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2, PublicKey=0024000004800000940000000602000000240000525341310004000001000100c547cac37abd99c8db225ef2f6c8a3602f3b3606cc9891605d02baa56104f4cfc0734aa39b93bf7852f7d9266654753cc297e7d2edfe0bac1cdcf9f717241550e0a7b191195b7667bb4f64bcb8e2121380fd1d9d46ad2d92d2d15605093924cceaf74c4861eff62abf69b9291ed0a340e113be11e6a7d3113e92484cf7045cc7")]
namespace RGR.IO
{
    internal class File : IFile
    {
        public string ReadAllText(string path)
        {
            return System.IO.File.ReadAllText(path);
        }

        public void WriteAllText(string path, string content)
        {
            System.IO.File.WriteAllText(path, content);
        }

        public async Task<string> ReadAllTextAsync(string path)
        {
            return await System.IO.File.ReadAllTextAsync(path);
        }

        public async Task WriteAllTextAsync(string path, string content)
        {
            await System.IO.File.WriteAllTextAsync(path, content);
        }

        public bool Exists(string path)
        {
            return System.IO.File.Exists(path);
        }

        public void Delete(string path)
        {
            System.IO.File.Delete(path);
        }

        public void Copy(string sourcePath, string destinationPath, bool overwrite)
        {
            System.IO.File.Copy(sourcePath, destinationPath, overwrite);
        }

        public void Move(string sourcePath, string destinationPath)
        {
            System.IO.File.Move(sourcePath, destinationPath);
        }

        public string GetExtension(string path)
        {
            return System.IO.Path.GetExtension(path);
        }

        public string GetFileNameWithoutExtension(string path)
        {
            return System.IO.Path.GetFileNameWithoutExtension(path);
        }

        public string GetFileName(string path)
        {
            return System.IO.Path.GetFileName(path);
        }

        public string? GetDirectoryName(string path)
        {
            return System.IO.Path.GetDirectoryName(path);
        }

        public void Create(string path)
        {
            using (System.IO.File.Create(path)) { }
        }
    }
}
