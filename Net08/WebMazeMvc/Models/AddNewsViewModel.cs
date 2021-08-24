using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMazeMvc.Models
{
    public class AddNewsViewModel
    {
        public long Id { get; set; }

        public string Title { get; set; }

        public string Source { get; set; }

        public long CreaterId { get; set; }

        public string Topic { get; set; }

        public IFormFile NewsFile { get; set; }

        public List<CommentViewModel> CommentsFromForum { get; set; }
    }
}
