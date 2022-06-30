using FluentValidation;
using Microsoft.EntityFrameworkCore;
using NLayer.Bussiness.Abstract;
using NLayer.Bussiness.ValidationRules.FluentValidation;
using NLayer.Core.Results.Abstract;
using NLayer.Core.Results.Concrete;
using NLayer.Core.Validation;
using NLayer.DataAccess.Abstract;
using NLayer.Entity.Concrete;
using NLayer.Entity.Dto;

namespace NLayer.Bussiness.Concrete
{
    public class RoleManager : IRoleService
    {
        private IRoleDal _roleDal;
        private IUserRolesDal _applicationUserRolesDal;
        public RoleManager(IRoleDal roleDal, IUserRolesDal applicationUserRolesDal)
        {
            _roleDal = roleDal;
            _applicationUserRolesDal = applicationUserRolesDal;
        }

        public async Task<IDataResult<List<ApplicationRole>>> GetAllRolesAsync()
        {
            var resultData = await _roleDal.GetAllAsync().ToListAsync();
            if (resultData.Count > 0)
            {
                return new SuccessDataResult<List<ApplicationRole>>(resultData);
            }
            else
            {
                return new ErrorDataResult<List<ApplicationRole>>("Kayıtlı Hiç Bir Rol Bulunamadı");
            }
        }

        public async Task<IResult> AddRoleToUser(AddNewRoleDto addNewRoleDto)
        {
            try
            {
                ValidationTool.Validate(new AddNewRoleDtoValidator(), addNewRoleDto);

                await _applicationUserRolesDal.AddAsync(new ApplicationUserRoles() {UserId = addNewRoleDto.UserId, RoleId = addNewRoleDto.RoleId});
                return new SuccessResult($"{addNewRoleDto.UserId} Id'li Kullanıcıya {addNewRoleDto.RoleId} Id'li Role Başarıyla Eklendi");
            }
            catch (ValidationException validationException)
            {
                return new ValidationErrorDataResult<ApplicationUser>(validationException.Errors);
            }
        }

        public async Task<IDataResult<List<string>>> GetUserRolesAsync(int userId)
        {
            List<string> resultData = new List<string>();
            foreach (ApplicationUserRoles applicationUserRole in await _applicationUserRolesDal.Where(x => x.UserId == userId).ToListAsync())
            {
                var role = await _roleDal.Where(x => x.Id == applicationUserRole.RoleId).FirstOrDefaultAsync();
                if (role != null)
                {
                    resultData.Add(role.Name);
                }
            }

            return new SuccessDataResult<List<string>>(resultData);
        }
    }
}
