using RGR.Core.Contracts;
using RGR.Core.Services.Abstractions;
using RGR.IO.Abstractions;
using System.Runtime.CompilerServices;
using System.Text.Json;

[assembly: InternalsVisibleTo("RGR.Core.Tests")]
namespace RGR.Core.Services
{
    public class FileService : IFileService
    {
        private readonly IFile _file;

        public FileService(IFile file)
        {
            _file = file;
        }

        public void SaveCreditRequest(CreditRequestContract creditRequest, string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentNullException(nameof(filePath));
            }

            var creditRequestJson = JsonSerializer.Serialize(creditRequest ?? throw new ArgumentNullException(nameof(creditRequest)), new JsonSerializerOptions { WriteIndented = true });
            _file.WriteAllText(filePath, creditRequestJson);
        }
    }
}
