using NLayer.DataAccess.Abstract;
using NLayer.DataAccess.Core.Concrete;
using NLayer.Entity.Concrete;

namespace NLayer.DataAccess.Concrete.EntityFramework
{
    public class EfUserRolesDal : GenericRepository<ApplicationUserRoles>, IUserRolesDal
    {
        public EfUserRolesDal(EfContext context) : base(context)
        {
        }
    }
}
