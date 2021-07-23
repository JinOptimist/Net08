using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebMazeMvc.Models.CustomValidationAttribute;

namespace WebMazeMvc.Models
{
    public class MazeViewModel
    {
        [Required]
        public string Title { get; set; }

        [Min(4)]
        [Max(100)]
        public int Width { get; set; }

        [Min(3)]
        [Max(100)]
        public int Height { get; set; }
    }
}
