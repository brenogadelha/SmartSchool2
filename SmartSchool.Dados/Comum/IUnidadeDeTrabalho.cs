using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SmartSchool.Dados.Contextos;

namespace SmartSchool.Dados.Comum
{
    public interface IUnidadeDeTrabalho
    {
        SmartContexto SmartContexto { get; }        
    }
}
