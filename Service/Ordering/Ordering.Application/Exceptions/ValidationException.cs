using FluentValidation.Results;

namespace Ordering.Application.Exceptions
{
    public class ValidationException : ApplicationException
    {
        public Dictionary<string, string[]> Errors { get; }
        public ValidationException() : base("One or more validation error occured")
        {
            Errors = new Dictionary<string, string[]>();
        }
        public ValidationException(IEnumerable<ValidationFailure> validationFailures) : this()
        {
            Errors = validationFailures
                .GroupBy(o => o.PropertyName, o => o.ErrorMessage)
                .ToDictionary(o => o.Key, o => o.ToArray());
        }


    }
}
