using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SmartSchool.Dados.Comum;

namespace SmartSchool.Dados.Contextos
{
    public class Contextos : IUnidadeDeTrabalho
    {
        public SmartContexto SmartContexto { get; }

        public Contextos(SmartContexto smartContexto) => SmartContexto = smartContexto;

    }
}
