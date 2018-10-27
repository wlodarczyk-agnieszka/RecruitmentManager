using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace RecruitmentManager.ViewModels
{
    public class FileUpload
    {
        public IFormFile File { get; set; }
    }
}
