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

        public string GetCatPath(long catId)
        {
            var projectPath = _webHostEnvironment.WebRootPath;
            var fileName = $"{catId}.png";
            return Path.Combine(projectPath, "image\\cats", fileName);
        }

        public string GetCatUrl(long catId)
        {
            return $"/image/cats/{catId}.png";
        }

        public string GetBankPath(long bankId)
        {
            var projectPath = _webHostEnvironment.WebRootPath;
            var fileName = $"{bankId}.png";
            return Path.Combine(projectPath, "image\\banks", fileName);
        }

        public string GetBankUrl(long bankId)
        {
            return $"/image/banks/{bankId}.png";
        }
    }
}
