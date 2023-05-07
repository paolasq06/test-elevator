using Core.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IRepository<T> where T : Entity
    {
        IQueryable<T> Get();
        Task<T> GetById(Guid id);
        Task<T> Add(T entity);
        Task<T> Put(T entity);
        Task<bool> Delete(T entity);
        Task<bool> DeleteRange(List<T> entity);
      
    }
}
