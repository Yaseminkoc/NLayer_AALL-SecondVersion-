using NLayer.Entity.Abstract;

namespace NLayer.Entity.Concrete
{
    public class ApplicationUser : IEntity
    {
        public int Id { get; set; }

        public string DisplayName { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }
    }
}
