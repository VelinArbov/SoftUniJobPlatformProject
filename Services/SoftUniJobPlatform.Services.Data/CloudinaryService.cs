using System.Collections.Generic;
using System.Linq;

namespace SoftUniJobPlatform.Services.Data
{
    using System;
    using System.IO;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Http.Internal;

    public class CloudinaryService : ICloudinaryService
    {
        private readonly Cloudinary cloudinary;

        public CloudinaryService(Cloudinary cloudinary)
        {
            this.cloudinary = cloudinary;
        }

        public async Task<string> UploadFormFileAsync(IFormFile file)
        {
            byte[] destinationImage;

            using (var memory = new MemoryStream())
            {
                await file.CopyToAsync(memory);
                destinationImage = memory.ToArray();
            }

            using (var destinationStream = new MemoryStream(destinationImage))
            {

                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(file.FileName, destinationStream),
                };

                var res= await this.cloudinary.UploadAsync(uploadParams);

                return res.Uri.AbsoluteUri;
            }

        }
    }
}
