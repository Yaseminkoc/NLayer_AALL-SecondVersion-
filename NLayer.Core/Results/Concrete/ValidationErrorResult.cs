using FluentValidation.Results;

namespace NLayer.Core.Results.Concrete
{
    public class ValidationErrorResult : Result
    {
        private IEnumerable<ValidationFailure> _failures;

        public ValidationErrorResult(IEnumerable<ValidationFailure> failures) : base(false)
        {
            _failures = failures;
        }

        public IEnumerable<ValidationFailure> GetValidationFailures()
        {
            return _failures;
        }
    }
}
