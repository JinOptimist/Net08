using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMazeMvc.EfStuff.Model;
using WebMazeMvc.EfStuff.Repositories;

namespace WebMazeMvc.Services
{
    public class CommentService
    {
        private CommentRepository _commentRepository;
        private IHttpContextAccessor _httpContextAccessor;

        public CommentService(CommentRepository commentRepository,
            IHttpContextAccessor httpContextAccessor)
        {
            _commentRepository = commentRepository;
            _httpContextAccessor = httpContextAccessor;
        }
        public Comment GetCurrent()
        {
            var idStr = _httpContextAccessor
                .HttpContext
                .User
                .Claims
                .SingleOrDefault(x => x.Type == "Id")
                ?.Value;

            if (string.IsNullOrEmpty(idStr))
            {
                return null;
            }

            var id = int.Parse(idStr);
            return _commentRepository.Get(id);
        }
    }
}
