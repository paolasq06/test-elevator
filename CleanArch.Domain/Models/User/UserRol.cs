using Core.Models.Common;
using System;


namespace Domain.Models.User
{
    public class UserRol : Entity
	{
		public Guid RolId { get; set; }

        public Guid UserId { get; set; }

		public Rol.Rol Rol  { get; set; }

		public User User { get; set; }
	}
}