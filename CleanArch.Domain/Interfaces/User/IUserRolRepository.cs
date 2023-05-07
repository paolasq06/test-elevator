using Domain.Models.User;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Interfaces.User
{
    public interface IUserRolRepository
    {
        IQueryable<UserRol> Get();
        UserRol Post(UserRol userRol);
        UserRol Put(UserRol userRol);
        List<UserRol> PostRange(List<UserRol> userRol);
        bool Delete(UserRol userRol);
        bool DeleteRange(List<UserRol> userRol);
    }
}
