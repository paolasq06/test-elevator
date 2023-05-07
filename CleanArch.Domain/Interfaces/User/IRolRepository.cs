using Domain.Models.Rol;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Interfaces.User
{
    public interface IRolRepository
    {
        IQueryable<Rol> Get();
        Rol Post(Rol rol);
        Rol Put(Rol rol);
        List<Rol> PostRange(List<Rol> rol);
        bool Delete(Rol entity);
        Rol PutStatusActivateRolById(Rol rol);
        Rol PutStatusDeactivateRolById(Rol rol);
    }
}
