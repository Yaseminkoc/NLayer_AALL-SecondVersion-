using NLayer.Core.Results.Abstract;
using NLayer.Entity.Concrete;
using NLayer.Entity.Dto;

namespace NLayer.Bussiness.Abstract
{
    public interface IRoleService
    {
        Task<IDataResult<List<ApplicationRole>>> GetAllRolesAsync();
        Task<IResult> AddRoleToUser(AddNewRoleDto addNewRoleDto);
        Task<IDataResult<List<string>>> GetUserRolesAsync(int userId);
    }
}
