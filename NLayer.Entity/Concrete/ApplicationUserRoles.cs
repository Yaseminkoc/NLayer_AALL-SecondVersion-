using NLayer.Entity.Abstract;

namespace NLayer.Entity.Concrete
{
    public class ApplicationUserRoles : IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public ApplicationUser User { get; set; }
        public int RoleId { get; set; }
        public ApplicationRole Role { get; set; }
    }
}
