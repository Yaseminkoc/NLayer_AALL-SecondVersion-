using FluentValidation.Results;

namespace NLayer.Core.Results.Concrete
{
    public class ValidationErrorDataResult<T> : DataResult<T>
    {
        private IEnumerable<ValidationFailure> _failures;

        public ValidationErrorDataResult(IEnumerable<ValidationFailure> failures) : base(default, false)
        {
            _failures = failures;
        }
        public IEnumerable<ValidationFailure> GetValidationFailures()
        {
            return _failures;
        }
    }
}
