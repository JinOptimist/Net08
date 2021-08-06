using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebMazeMvc.Models.CustomValidationAttribute;

namespace WebMazeMvc.Models
{
    public class MazeViewModel
    {
        [Required]
        [DisplayName("Название")]
        public string Title { get; set; }

        [Min(4)]
        [DisplayName("Ширина")]
        public int Width { get; set; }

        [Min(3)]
        [DisplayName("Высота")]
        public int Height { get; set; }
    }
}
