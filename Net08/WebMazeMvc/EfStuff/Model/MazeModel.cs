using MazeCore.Cells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMazeMvc.EfStuff.Model
{
    public class MazeModel : BaseModel
    {
        public long Id { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public virtual List<Cell> Cells { get; set; }
    }
}
