using Application.Common.Auth;
using CleanArchitecture.Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Security.Claims;

namespace CleanArchitecture.WebUI.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ClaimsIdentity _identity = null;
        public Guid? Id { get; set; } = null;

        public CurrentUserService(
            IHttpContextAccessor httpContextAccessor
        )
        {
            _httpContextAccessor = httpContextAccessor;

            _identity = (ClaimsIdentity)_httpContextAccessor
                        .HttpContext?
                        .User?
                        .Identity;
            if(_identity?.FindFirst("Id") != null)
            Id = Guid.Parse(_identity?.FindFirst("Id")?.Value);

        }
        public AuthUserInfo GetUserInfo()
        {
            _identity = (ClaimsIdentity)_httpContextAccessor
                        .HttpContext
                        .User
                        .Identity;
            var userInfo = new AuthUserInfo() { };
            if (_identity?.FindFirst("Id") != null)
                userInfo.Id = Guid.Parse(_identity?.FindFirst("Id")?.Value);
            userInfo.Name = _identity?.FindFirst(ClaimTypes.Name)?.Value;
            userInfo.Email = _identity?.FindFirst(ClaimTypes.Email)?.Value;
            userInfo.Rols = _identity?.FindFirst("UserRols")?.Value;


            return userInfo;
        }

        public string UserId => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
    }
}
