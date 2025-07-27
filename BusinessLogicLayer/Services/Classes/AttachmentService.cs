using BusinessLogicLayer.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services.Classes
{
    public class AttachmentService : IAttachmentService
    {
        private List<string> allowExtensions = [".jpg", ".png", ".jpeg"];
        const int _maxSize = 2_097_125;

        public string? Upload(IFormFile file, string folderName)
        {
           var extention = Path.GetExtension(file.FileName);
            if (!allowExtensions.Contains(extention) || file.Length==0||file.Length>_maxSize)
                return null;
            var fileName = $"{Guid.NewGuid()}_{file.FileName}";
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\files", folderName);
            var filePath = Path.Combine(folderPath, fileName);

            using var fileStream = new FileStream(filePath,FileMode.Create);
            file.CopyTo(fileStream);

            //return filePath;
            return fileName;
        }

        public bool Delete(string filePath)
        {
            if (!Path.Exists(filePath)) return false;
            File.Delete(filePath); return true;
        }
    }
}
