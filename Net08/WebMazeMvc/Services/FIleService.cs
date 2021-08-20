using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebMazeMvc.Services
{
    public class FileService
    {
        private IWebHostEnvironment _webHostEnvironment;

        public FileService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public string GetPath(long catId, string folderName )
        {
            var projectPath = _webHostEnvironment.WebRootPath;
            var fileName = $"{catId}.png";
            return Path.Combine(projectPath, $"image\\{folderName}", fileName);
        }

        public string GetCatUrl(long catId)
        {
            return $"/image/cats/{catId}.png";
        }
    }
}
