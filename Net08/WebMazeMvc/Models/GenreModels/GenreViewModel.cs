using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebMazeMvc.Models
{
    public class GenreViewModel
    {
        public long Id { get; set; }

        [Required (ErrorMessage ="Введите название жанра") ]
        public string GenreName { get; set; }
    }
}
