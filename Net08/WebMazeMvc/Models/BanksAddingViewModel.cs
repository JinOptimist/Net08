using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMazeMvc.Models
{
    public class BanksAddingViewModel
    {
        public string Name { get; set; }

        public string Country { get; set; }

        public string Url { get; set; }

        public IFormFile BankLogoFile { get; set; }
    }
}
