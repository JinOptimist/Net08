using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMazeMvc.Models
{
    public class AllIformationViewModle
    {
        public string Title { get; set; }

        public string Source { get; set; }

        public string Topic { get; set; }

        public List<CommentViewModel> CommentsFromForum { get; set; }
    }
}
