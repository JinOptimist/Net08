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
        [DisplayName("Название лабиринта")]
        public string Title { get; set; }

        [Min(4)]
        [DisplayName("Ширина лабиринта")]
        public int Width { get; set; }

        [Min(3)]
        [DisplayName("Высота лабиринта")]
        public int Height { get; set; }
    }
}
