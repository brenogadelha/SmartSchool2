using System;

namespace SmartSchool.Comum.TratamentoErros
{
	public class RecursoInexistenteException : Exception
    {
        public RecursoInexistenteException(string message) : base(message)
        {
        }
    }
}
