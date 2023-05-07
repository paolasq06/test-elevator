using Application.Core.Exceptions;
using CleanArch.Infra.Data.Context;
using Domain.Interfaces.User;
using Domain.Models.Rol;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infra.Data.Repository.Users
{
    public class RolRepository : IRolRepository
    {
        private ApplicationDBContext _ctx;

        public RolRepository(ApplicationDBContext ctx)
        {
            _ctx = ctx;
        }

        public IQueryable<Rol> Get()
        {
            return _ctx.Rols;
        }

        /// <summary>
        /// Method that adds Rol.
        /// </summary>
        /// <returns></returns>
        public Rol Post(Rol rol)
        {
            _ctx.Rols.Add(rol);

            try
            {
                _ctx.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new NotFoundException("Rol", "El rol no fue insertado");
            }

            return rol;
        }

        /// <summary>
        /// Method for modifying the entity.
        /// </summary>
        /// <param name="rol"></param>
        /// <returns></returns>
        public Rol Put(Rol rol)
        {
            _ctx.Entry(rol).State = EntityState.Modified;

            try
            {
                _ctx.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new NotFoundException("Rol", "El rol no fue encontrado");
            }

            return rol;
        }

        public List<Rol> PostRange(List<Rol> rol)
        {

            _ctx.Rols.AddRange(rol);

            try
            {
                _ctx.SaveChanges();
                return rol;
            }
            catch (Exception ex)
            {

                throw new BadRequestException("No se ha podido agregar los roles");
            }
        }

        /// <summary>
        /// Method that eliminates the entity that enters by parameter.
        /// </summary>
        /// <param name="rol"></param>
        /// <returns></returns>
        public bool Delete(Rol rol)
        {
            _ctx.Remove(rol);

            try
            {
                _ctx.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new NotFoundException("Rol", "El rol no fue eliminado");
            }

            return true;
        }

        public Rol PutStatusActivateRolById(Rol rol)
        {
            rol.Status = true;
            _ctx.Entry(rol).State = EntityState.Modified;

            try
            {
                _ctx.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new NotFoundException("Rol", "El rol no fue modificado");
            }

            return rol;
        }

        public Rol PutStatusDeactivateRolById(Rol rol)
        {
            rol.Status = false;
            _ctx.Entry(rol).State = EntityState.Modified;

            try
            {
                _ctx.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new NotFoundException("Rol", "El rol no fue modificado");
            }

            return rol;
        }
    }
}
