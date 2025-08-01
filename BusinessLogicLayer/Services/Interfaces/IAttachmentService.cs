﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services.Interfaces
{
    public interface IAttachmentService
    {
        string? Upload(IFormFile file, string folderName);
        bool Delete(string filePath);
    }
}
