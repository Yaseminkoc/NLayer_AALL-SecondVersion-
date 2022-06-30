using FluentValidation;
using NLayer.Entity.Dto;

namespace NLayer.Bussiness.ValidationRules.FluentValidation
{
    public class UsernameDtoValidatior : AbstractValidator<UsernameDto>
    {
        public UsernameDtoValidatior()
        {
            RuleFor(x => x.Username).NotEmpty().WithMessage("Bu Alanı Doldurmanız Zorunludur");
        }
    }
}
