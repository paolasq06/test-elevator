using Core.Models.Common;

namespace Domain.Models.Rol
{
    public class Rol : Entity
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public bool Status { get; set; } = true;
    }
}
