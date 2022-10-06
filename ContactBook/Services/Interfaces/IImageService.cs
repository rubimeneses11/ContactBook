using System;

namespace ContactBook.Services.Interfaces
{
    public interface IImageService
    {
        //HTTP upload request will always be of type IFormFile
        public Task<byte[]> ConvertFileToByteArrayAsync(IFormFile file);

        //holds data of the image
        public string ConvertByteArrayToFile(byte[] fileData, string extension);
    }
}

