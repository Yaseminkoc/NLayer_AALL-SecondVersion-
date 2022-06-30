using FluentValidation;
using NLayer.DataAccess.Abstract;
using NLayer.Entity.Dto;

namespace NLayer.Bussiness.ValidationRules.FluentValidation
{
    public class AddNewRoleDtoValidator : AbstractValidator<AddNewRoleDto>
    {
        private IUserRolesDal _applicationUserRolesDal;
        public AddNewRoleDtoValidator(IUserRolesDal applicationUserRolesDal)
        {
            _applicationUserRolesDal = applicationUserRolesDal;
        }

        public AddNewRoleDtoValidator()
        {
            RuleFor(x => x.RoleId).NotEmpty().WithMessage("Bu Alanı Doldurmanız Zorunludur");
            RuleFor(x => x.UserId).NotEmpty().WithMessage("Bu Alanı Doldurmanız Zorunludur")
                .Must((x, y) => !userHasThisRole(x.UserId, x.RoleId))
                .WithMessage("Kullanıcı Zaten Bu Role Sahip");
        }
        public bool userHasThisRole(int UserId, int RoleId)
        {
            return _applicationUserRolesDal.Where(x => x.UserId == UserId && x.RoleId == RoleId).FirstOrDefault() != null;  
        }
    }
}
