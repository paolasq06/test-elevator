using CleanArch.Infra.Data.Context;
using Domain.Interfaces;
using Domain.Models.User;
using System;
using System.Linq;

namespace Infra.Data.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private ApplicationDBContext _ctx;

        public AuthRepository(ApplicationDBContext ctx)
        {
            _ctx = ctx;
        }

        public bool ValidateByLogin(string login)
        {

            try
            {
                var user = _ctx.Users.Where(c => c.login == login && c.Status == true).FirstOrDefault();
                return user == null ? false : true;
            }
            catch (Exception)
            {
                return false;
            }
            
        }

  
        public IQueryable<User> GetUsers()
        {
            return _ctx.Users;
        }

    }
}
