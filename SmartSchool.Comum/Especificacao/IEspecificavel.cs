using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SmartSchool.Comum.Especificao
{
    public interface IEspecificavel<T>
    {
        List<string> ObjetosInclusaoStrings { get; set; }
        List<Expression<Func<T, object>>> ObjetosInclusaoTipo { get; set; }

        Expression<Func<T, bool>> ExpressaoEspecificacao { get; }
        bool EhSatisfeitaPor(T obj);
        IList<string> SubInclusoes { get; }
    }
}
