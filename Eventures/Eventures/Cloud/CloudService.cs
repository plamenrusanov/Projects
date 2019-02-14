using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Eventures.Cloud.Contracts;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Eventures.Cloud
{
    public class CloudService : ICloudService
    {
        private readonly ConnectToCloud cloud;

        public CloudService(ConnectToCloud cloud)
        {
            this.cloud = cloud;
        }

        public async Task<string> UploadImageToCloud(IFormFile formFile)
        {
            Stream stream = formFile.OpenReadStream();
            var ImageName = Guid.NewGuid().ToString();
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(ImageName, stream),
            };
            var uploadResult = cloud.cloudinary.Upload(uploadParams);
            return await Task.FromResult<string>(uploadResult.Uri.AbsolutePath);
        }
    }
}
