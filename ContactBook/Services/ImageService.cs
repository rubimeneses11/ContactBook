using System;
using ContactBook.Services.Interfaces;

namespace ContactBook.Services
{
    //handling images to and from db
    public class ImageService : IImageService
    {
        private readonly string[] suffixes = { "Bytes", "KB", "MB", "GB", "TB", "PB" };
        private readonly string defaultImage = "img/DefaultContactImage.png";

        public string ConvertByteArrayToFile(byte[] fileData, string extension)
        {
            if (fileData is null) return defaultImage; //if no image in db, give the default image

            try
            {
                string imageBase64Data = Convert.ToBase64String(fileData); //byte array converted to string so it can be displayed
                return string.Format($"data:{extension};base64,{imageBase64Data}");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<byte[]> ConvertFileToByteArrayAsync(IFormFile file)
        {
            try
            {
                using MemoryStream memoryStream = new(); //create a new memory stream
                await file.CopyToAsync(memoryStream); //copy incoming file
                byte[] bytefile = memoryStream.ToArray(); //convert memory stream to array
                return bytefile; //return byte file so it can be saved to db
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}

