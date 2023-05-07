using Domain.Models.User;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Interfaces.User
{
    public interface IUserRepository
    {
        IQueryable<Domain.Models.User.User> Get();
        Domain.Models.User.User Post(Domain.Models.User.User user);
        Domain.Models.User.User Put(Domain.Models.User.User user);

        Domain.Models.User.User PutStatusActivateUserById(Domain.Models.User.User user);

        Domain.Models.User.User PutStatusDeactivateUserById(Domain.Models.User.User user);
        List<Domain.Models.User.User> PostRange(List<Domain.Models.User.User> user);
        bool Delete(Domain.Models.User.User user);
        bool DeleteUserRolRange(UserRol userRol);
        bool PostSubCampaingUser(Domain.Models.User.User user, int idCampaing);

    }
}
