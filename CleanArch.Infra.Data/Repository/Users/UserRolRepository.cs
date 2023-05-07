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
    public class UserRolRepository : IUserRolRepository
    {
        private ApplicationDBContext _ctx;

        public UserRolRepository(ApplicationDBContext ctx)
        {
            _ctx = ctx;
        }

        public IQueryable<UserRol> Get()
        {
            return _ctx.UserRols;
        }

        /// <summary>
        /// Method that adds Rol.
        /// </summary>
        /// <returns></returns>
        public UserRol Post(UserRol userRol)
        {
            _ctx.UserRols.Add(userRol);

            try
            {
                _ctx.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new NotFoundException("ScheduledAppointment", "El usuario rol no fue insertado");
            }

            return userRol;
        }

        /// <summary>
        /// Method for modifying the entity.
        /// </summary>
        /// <param name="userRol"></param>
        /// <returns></returns>
        public UserRol Put(UserRol userRol)
        {
            _ctx.Entry(userRol).State = EntityState.Modified;

            try
            {
                _ctx.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new NotFoundException("UserRol", "El usuario rol no fue encontrado");
            }

            return userRol;
        }

        public List<UserRol> PostRange(List<UserRol> userRol)
        {

            _ctx.UserRols.AddRange(userRol);

            try
            {
                _ctx.SaveChanges();
                return userRol;
            }
            catch (Exception ex)
            {

                throw new BadRequestException("No se ha podido agregar los usuarios roles");
            }
        }

        /// <summary>
        /// Method that eliminates the entity that enters by parameter.
        /// </summary>
        /// <param name="userRol"></param>
        /// <returns></returns>
        public bool Delete(UserRol userRol)
        {
            _ctx.Remove(userRol);

            try
            {
                _ctx.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new NotFoundException("User", "El usuario rol no fue eliminado");
            }

            return true;
        }

        public bool DeleteRange(List<UserRol> userRol)
        {
            _ctx.RemoveRange(userRol);

            try
            {
                _ctx.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new NotFoundException("User", "El usuario rol no fue eliminado");
            }

            return true;
        }

    }
}
