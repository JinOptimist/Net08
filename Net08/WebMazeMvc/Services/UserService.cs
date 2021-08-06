﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMazeMvc.EfStuff.Model;
using WebMazeMvc.EfStuff.Repositories;

namespace WebMazeMvc.Services
{
    public class UserService
    {
        private UserRepository _userRepository;
        private IHttpContextAccessor _httpContextAccessor;
        
        public UserService(UserRepository userRepository,
            IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public User GetCurrent()
        {
            if (!_httpContextAccessor
                .HttpContext
                .User
                .Identity
                .IsAuthenticated)
                return null;

            var idStr = _httpContextAccessor
                .HttpContext
                .User
                .Claims
                .Single(x => x.Type == "Id")
                .Value;
            var id = int.Parse(idStr);
            return _userRepository.Get(id);
        }
    }
}
