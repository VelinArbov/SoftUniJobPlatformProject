namespace SoftUniJobPlatform.Services.Data
{
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Http.Internal;

    public interface ICloudinaryService
    {
        Task<string> UploadFormFileAsync(IFormFile file);

    }
}
