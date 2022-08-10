using FluentValidation;
using FluentValidation.Results;

namespace SmartSchool.Comum.Entidades
{
    public abstract class GenericValidator<T, TValidator>
        where TValidator : AbstractValidator<T>, new()
        where T : GenericValidator<T, TValidator>
    {
        public bool IsValid()
        {
            return Validate().IsValid;
        }

        public ValidationResult Validate()
        {
            var validator = new TValidator();
            return validator.Validate((T)this);
        }
    }
}
