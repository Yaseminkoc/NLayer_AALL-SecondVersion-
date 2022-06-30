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
    public class UserManager : IUserService
    {
        private IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public async Task<IDataResult<ApplicationUser>> LoginAsync(LoginDto loginDto)
        {
            try
            {
                ValidationTool.Validate(new LoginDtoValidator(), loginDto);//fluent validation errorlarını burda yakalıyoruz

                var user = await _userDal.Where(x => x.Username == loginDto.Username && x.Password == loginDto.Password).FirstOrDefaultAsync();
                if (user == null)
                {
                    return new ErrorDataResult<ApplicationUser>("Hatalı Giriş Denemesi");
                }
                else
                {
                    return new SuccessDataResult<ApplicationUser>(user);
                }
            }
            catch (ValidationException validationException)
            {
                return new ValidationErrorDataResult<ApplicationUser>(validationException.Errors);
            }
        }

        public async Task<IDataResult<ApplicationUser>> GetUserByUsername(UsernameDto usernameDto)
        {
            try
            {
                ValidationTool.Validate(new UsernameDtoValidatior(), usernameDto);//fluent validation errorlarını burda yakalıyoruz

                var user = await _userDal.Where(x => x.Username == usernameDto.Username).FirstOrDefaultAsync();
                if (user == null)
                {
                    return new ErrorDataResult<ApplicationUser>("Kullanıcı Bulunamadı");
                }
                else
                {
                    return new SuccessDataResult<ApplicationUser>(user);
                }
            }
            catch (ValidationException validationException)
            {
                return new ValidationErrorDataResult<ApplicationUser>(validationException.Errors);
            }
        }

        public Task<IDataResult<ApplicationUser>> SaveUser(UsernameDto usernameDto)
        {
            throw new NotImplementedException();
        }
    }
}
