using NLayer.DataAccess.Abstract;
using NLayer.DataAccess.Core.Concrete;
using NLayer.Entity.Concrete;

namespace NLayer.DataAccess.Concrete.EntityFramework
{
    public class EfUserDal : GenericRepository<ApplicationUser>, IUserDal
    {
        public EfUserDal(EfContext context) : base(context)
        {
        }
    }
}
