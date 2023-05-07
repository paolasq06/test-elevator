using Core.Models.Common;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IIntRepository<T> where T : EntityWithIntId
    {
        IQueryable<T> Get();
        Task<T> GetById(int id);
        Task<T> Add(T entity);
        Task<T> Put(T entity);
        Task<bool> Delete(T entity);
      
    }
}
