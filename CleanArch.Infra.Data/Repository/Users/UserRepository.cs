using Application.Core.Exceptions;
using CleanArch.Infra.Data.Context;
using Domain.Interfaces.User;
using Domain.Models.User;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infra.Data.Repository.Users
{
    public class UserRepository : IUserRepository
    {
        private ApplicationDBContext _ctx;

        public UserRepository(ApplicationDBContext ctx)
        {
            _ctx = ctx;
        }

        public IQueryable<User> Get()
        {
            return _ctx.Users;
        }

        /// <summary>
        /// Method that adds User.
        /// </summary>
        /// <returns></returns>
        public User Post(User user)
        {
            _ctx.Users.Add(user);

            try
            {
                _ctx.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new NotFoundException("User", "El usuario no fue insertado");
            }

            return user;
        }

        /// <summary>
        /// Method for modifying the entity.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public User Put(User user)
        {
            _ctx.Entry(user).State = EntityState.Modified;

            try
            {
                _ctx.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new NotFoundException("User", "El usuario no fue modificado");
            }

            return user;
        }

        /// <summary>
        /// Method for modifying the entity.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public User PutStatusActivateUserById(User user)
        {
            user.Status = true;
            _ctx.Entry(user).State = EntityState.Modified;

            try
            {
                _ctx.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new NotFoundException("User", "El usuario no fue modificado");
            }

            return user;
        }

        /// <summary>
        /// Method for modifying the entity.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public User PutStatusDeactivateUserById(User user)
        {
            user.Status = false;
            _ctx.Entry(user).State = EntityState.Modified;

            try
            {
                _ctx.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new NotFoundException("User", "El usuario no fue modificado");
            }

            return user;
        }

        public List<User> PostRange(List<User> user)
        {

            _ctx.Users.AddRange(user);

            try
            {
                _ctx.SaveChanges();
                return user;
            }
            catch (Exception ex)
            {

                throw new BadRequestException("El usuario no fue insertado");
            }
        }

        /// <summary>
        /// Method that eliminates the entity that enters by parameter.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool Delete(User user)
        {
            _ctx.Remove(user);

            try
            {
                _ctx.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new NotFoundException("User", "El usuario no fue eliminado");
            }

            return true;
        }

        public bool DeleteUserRolRange(UserRol userRol)
        {
            _ctx.Remove(userRol);

            try
            {
                _ctx.SaveChanges();
            }
            catch (Exception ex)
            {

                throw new NotFoundException("User Rol", "El rol del usuario no fue eliminado");
            }

            return true;
        }

        public bool PostSubCampaingUser(User user, int campaing)
        {
     
            try
            {
                _ctx.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new NotFoundException("User", "El usuario no fué modificado");
            }

            return true;
        }
    }
}
