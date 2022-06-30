using FluentValidation;
using NLayer.Entity.Dto;

namespace NLayer.Bussiness.ValidationRules.FluentValidation
{
    public class LoginDtoValidator : AbstractValidator<LoginDto>
    {
        public LoginDtoValidator()
        {
            RuleFor(x => x.Password).NotEmpty().WithMessage("Bu Alanı Doldurmanız Zorunludur");
            RuleFor(x => x.Username).NotEmpty().WithMessage("Bu Alanı Doldurmanız Zorunludur");
        }
    }
}
