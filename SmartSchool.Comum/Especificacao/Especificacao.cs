using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SmartSchool.Comum.Especificao
{
	public abstract class Especificacao<T> : IEspecificavel<T>
    {
        private Func<T, bool> _especificacaoCompilada;

        public abstract Expression<Func<T, bool>> ExpressaoEspecificacao { get; }

        private Func<T, bool> EspecificaoCompilada => _especificacaoCompilada ?? (_especificacaoCompilada = ExpressaoEspecificacao.Compile());

        public List<string> ObjetosInclusaoStrings { get; set; } = new List<string>();

        public List<Expression<Func<T, object>>> ObjetosInclusaoTipo { get; set; } = new List<Expression<Func<T, object>>>();

        public IList<string> SubInclusoes { get; } = new List<string>();

        public bool EhSatisfeitaPor(T obj) => EspecificaoCompilada(obj);
    }
}
