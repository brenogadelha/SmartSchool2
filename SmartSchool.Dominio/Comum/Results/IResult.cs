using System;
using System.Collections.Generic;

namespace SmartSchool.Dominio.Comum.Results
{
    public interface IResult
    {
        ResultStatus Status { get; }

        IEnumerable<string> Errors { get; }

        List<ValidationError> ValidationErrors { get; }

        Type? ValueType { get; }

        object? GetValue();
    }
}