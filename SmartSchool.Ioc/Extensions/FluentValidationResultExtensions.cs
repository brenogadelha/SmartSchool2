using FluentValidation;
using FluentValidation.Results;
using SmartSchool.Dominio.Comum.Results;
using System;
using System.Collections.Generic;

namespace SmartSchool.Ioc.Extensions
{
    public static class FluentValidationResultExtensions
    {
        public static List<ValidationError> AsErrors(this ValidationResult valResult)
        {
            List<ValidationError> list = new List<ValidationError>();
            foreach (ValidationFailure error in valResult.Errors)
            {
                list.Add(new ValidationError
                {
                    Severity = FromSeverity(error.Severity),
                    ErrorMessage = error.ErrorMessage,
                    Identifier = error.PropertyName
                });
            }

            return list;
        }

        public static ValidationSeverity FromSeverity(Severity severity)
        {
            return severity switch
            {
                Severity.Error => ValidationSeverity.Error,
                Severity.Warning => ValidationSeverity.Warning,
                Severity.Info => ValidationSeverity.Info,
                _ => throw new ArgumentOutOfRangeException("severity", "Unexpected Severity"),
            };
        }
    }
}
