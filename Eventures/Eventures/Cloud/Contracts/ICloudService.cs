using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Eventures.Cloud.Contracts
{
    public interface ICloudService
    {
        Task<string> UploadImageToCloud(IFormFile formFile);
    }
}