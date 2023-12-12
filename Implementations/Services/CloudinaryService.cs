using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using CMS_appBackend.DTOs.ResponseModels;
using CMS_appBackend.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;

namespace CMS_appBackend.Implementations.Services
{
    public class CloudinaryService
    {
        private readonly Cloudinary _cloudinary;

        public CloudinaryService(IOptions<CloudinarySettings> cloudinarySettings)
        {
            var cloudinaryUrl = Environment.GetEnvironmentVariable("CLOUDINARY_URL");
            var cloudinaryUri = new Uri(cloudinaryUrl);
            var cloudName = cloudinaryUri.UserInfo.Split(':')[0];
            var apiKey = cloudinaryUri.UserInfo.Split(':')[1];
            var apiSecret = cloudinaryUri.AbsolutePath.Substring(1);

            var account = new Account(cloudName, apiKey, apiSecret);
            _cloudinary = new Cloudinary(account);
        }

        // public string GetImageUrl(string publicId)
        // {
        //     var getParams = new GetResourceParams(publicId)
        //     {
        //         ResourceType = ResourceType.Image,
        //     };
        //     var cloudinaryResult = _cloudinary.GetResource(getParams);

        //     // Assuming you want the URL of the first (original) transformation
        //     var imageUrl = cloudinaryResult.Url;

        //     return imageUrl;
        // }

        public async Task<string> UploadImageToCloudinaryAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return null; // or throw an exception or handle accordingly
            }
            // Convert the image to base64
            string base64String = await ConvertImageToBase64Async(file);

            // Upload the base64-encoded image to Cloudinary
            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(
                    file.FileName,
                    new MemoryStream(Convert.FromBase64String(base64String))
                )
                // Additional parameters like folder, tags, etc.
            };

            var uploadResult = await _cloudinary.UploadAsync(uploadParams);

            // Return the URL of the uploaded image
            return uploadResult.SecureUrl.AbsoluteUri;
        }

        private async Task<string> ConvertImageToBase64Async(IFormFile file)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                byte[] imageBytes = memoryStream.ToArray();
                return Convert.ToBase64String(imageBytes);
            }
        }
    }
}
