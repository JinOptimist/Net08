using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace WebMazeMvc.Models
{
    public class AddNewsViewModel
    {
        public long Id { get; set; }

        public string Title { get; set; }

        public string Source { get; set; }

        public long CreaterId { get; set; }

        public string Topic { get; set; }

        public string Url { get; set; }

        public IFormFile NewsFile { get; set; }

        public List<CommentViewModel> CommentsFromForum { get; set; }
    }
}
