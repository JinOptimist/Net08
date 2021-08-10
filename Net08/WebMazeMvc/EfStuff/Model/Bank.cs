using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMazeMvc.EfStuff.Model
{
    public class Bank : BaseModel
    {

        public string Name { get; set; }

        public string Country { get; set; }

        public virtual List<User> Clients { get; set; }
    }
}
