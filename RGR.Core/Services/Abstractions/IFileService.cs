using RGR.Core.Contracts;

namespace RGR.Core.Services.Abstractions
{
    public interface IFileService
    {
        void SaveCreditRequest(CreditRequestContract creditRequest, string filePath);
    }
}