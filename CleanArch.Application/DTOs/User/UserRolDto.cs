using Domain.Models.User;
using System;

namespace Application.DTOs.User
{
    public class UserRolDto
    {
        public Guid Id { get; set; }

        public Guid RolId { get; set; }

        public Guid UserId { get; set; }

        public RolDto Rol { get; set; }

        public User.UserDto User { get; set; }
    }
}
