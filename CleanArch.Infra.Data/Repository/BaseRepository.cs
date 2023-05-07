using CleanArch.Infra.Data.Context;
using Core.Models.Common;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infra.Data.Repository
{
    public class BaseRepository<T> : IRepository<T> where T : Entity 
    {
        private readonly ApplicationDBContext _ctx;
        private DbSet<T> _entities;
        public BaseRepository(ApplicationDBContext ctx)
        {
            _ctx = ctx;
            _entities = ctx.Set<T>();
        }

     
        public IQueryable<T> Get()
        {
            return  _entities;
        }

        public async Task<T> GetById(Guid id)
        {
            return await _entities.FindAsync(id);
        }

        public async Task<T> Add(T entity)
        {
            _entities.Add(entity);
            await _ctx.SaveChangesAsync();
            return entity;
        }

        public async Task<T> Put(T entity)
        {
            _entities.Update(entity);
            await _ctx.SaveChangesAsync();
            return entity;
        }
        public async Task<bool> Delete(T entity)  {
           
            _entities.Remove(entity);
            await _ctx.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteRange(List<T> entity) {
           
            _entities.RemoveRange(entity);
            await _ctx.SaveChangesAsync();
            return true;
        }
      
    }
}
