using System;
using ContactBook.Services.Interfaces;

namespace ContactBook.Services
{
    //handling images to and from db
    public class ImageService : IImageService
    {
        private readonly string[] suffixes = { "Bytes", "KB", "MB", "GB", "TB", "PB" }; //suffixes to describe the byte array
        private readonly string defaultImage = "img/DefaultContactImage.png"; //display default image if empty

        //FROM DB
        public string ConvertByteArrayToFile(byte[] fileData, string extension)
        {
            if (fileData is null) return defaultImage; //if no image in db, give the default image

            try
            {
                string imageBase64Data = Convert.ToBase64String(fileData); //byte array converted to string 
                return string.Format($"data:{extension};base64,{imageBase64Data}"); //string passed thru a html image tag so it can be displayed
            }
            catch (Exception)
            {
                throw;
            }
        }

        //TO DB
        public async Task<byte[]> ConvertFileToByteArrayAsync(IFormFile file)
        {
            try
            {
                //from the users computer to our image service
                using MemoryStream memoryStream = new(); //create a new memory stream
                await file.CopyToAsync(memoryStream); //copy incoming file
                byte[] byteFile = memoryStream.ToArray(); //convert memory stream to array
                return byteFile; //return byte file so it can be saved to db
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}

