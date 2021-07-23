using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMazeMvc.Models
{
    public class MazeDrawViewModel
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public string[,] Cells { get; set; } 
    }
}
