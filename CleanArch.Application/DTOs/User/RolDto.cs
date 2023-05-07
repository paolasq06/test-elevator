using System;

namespace Application.DTOs.User
{
    public class RolDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public bool Status { get; set; }
    }
}
