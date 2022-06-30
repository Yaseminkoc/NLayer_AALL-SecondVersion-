using NLayer.Entity.Abstract;

namespace NLayer.Entity.Concrete
{
    public class ApplicationRole : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
