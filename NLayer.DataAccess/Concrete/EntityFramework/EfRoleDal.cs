using NLayer.DataAccess.Abstract;
using NLayer.DataAccess.Core.Concrete;
using NLayer.Entity.Concrete;

namespace NLayer.DataAccess.Concrete.EntityFramework
{
    public class EfRoleDal : GenericRepository<ApplicationRole>, IRoleDal
    {
        public EfRoleDal(EfContext context) : base(context)
        {
        }
    }
}
