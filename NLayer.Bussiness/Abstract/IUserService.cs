using NLayer.Core.Results.Abstract;
using NLayer.Entity.Concrete;
using NLayer.Entity.Dto;

namespace NLayer.Bussiness.Abstract
{
    public interface IUserService
    {
        Task<IDataResult<ApplicationUser>> LoginAsync(LoginDto loginDto);
        Task<IDataResult<ApplicationUser>> GetUserByUsername(UsernameDto usernameDto);

        Task<IDataResult<ApplicationUser>> SaveUser(UsernameDto usernameDto);
    }
}
