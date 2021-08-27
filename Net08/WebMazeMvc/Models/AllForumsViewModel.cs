using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMazeMvc.Models
{
    public class AllForumsViewModel
    {
        public int Page { get; set; }

        public int RecodPerPage { get; set; }

        public int TotalRecordCount { get; set; }

        public int CurrentPageStartRecordNumber
        {
            get
            {
                return (Page - 1) * RecodPerPage + 1;
            }
        }

        public List<MainForumViewModel> Forums { get; set; }
    }
}
