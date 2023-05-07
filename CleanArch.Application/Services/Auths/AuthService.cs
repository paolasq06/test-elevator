
using Application.Auth.Commands;
using Application.Common.Auth;
using Application.Common.Exceptions;
using Application.Core.Exceptions;
using Application.DTOs.User;
using Application.ViewModel.Auth;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CleanArch.Application.Interfaces.Auths;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArch.Application.Services.Auths
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly IMapper _autoMapper;

        public AuthService(
            IMapper autoMapper,
            IAuthRepository authRepository
        )
        {
            _authRepository = authRepository;
            _autoMapper = autoMapper;
        }

        public async Task<UserDto> GetUserByLogin(string login)
        {
            return await _authRepository
                    .GetUsers()
                    .Where(c => c.login == login && c.Status == true)
                    .ProjectTo<UserDto>(_autoMapper.ConfigurationProvider)
                    .FirstOrDefaultAsync();
        }

        public async Task<UserDto> GetUserById(Guid? Id)
        {
            return await _authRepository
                    .GetUsers()
                    .Where(c => c.Id == Id)
                    .ProjectTo<UserDto>(_autoMapper.ConfigurationProvider)
                    .FirstOrDefaultAsync();
        }

        public async Task<AuthViewModel> GetAuth(PostLoginCommand auth)
        {
            var userDto = await _authRepository
                    .GetUsers()
                    .Where(x => x.login == auth.Login && x.PassWord == auth.Password)
                    .ProjectTo<UserDto>(_autoMapper.ConfigurationProvider)
                    .FirstOrDefaultAsync()
                    ??
                throw new UnAuthorizeException("Usuario o contraseña incorrecta");

            if(userDto.Status == false)
            {
                throw new UnAuthorizeActiveException();
            }
            return new AuthViewModel()
            {
                user = userDto,
                token = GetAuthToken(userDto)
            };
        }



        public string GetAuthToken(UserDto user)
        {
            AuthToken auth = new AuthToken() { };
            return auth.GenerateToken(user);
        }



    }
}
