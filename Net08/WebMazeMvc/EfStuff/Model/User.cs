using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMazeMvc.EfStuff.Model
{
    public class User
    {
        public long Id { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public string AvatarUrl { get; set; }
    }
}
