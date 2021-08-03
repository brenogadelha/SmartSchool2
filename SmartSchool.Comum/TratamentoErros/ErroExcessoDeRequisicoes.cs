using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentValidation.Results;

namespace SmartSchool.Comum.TratamentoErros
{
    public class ErroExcessoDeRequisicoes : Exception
    {
        public ErroExcessoDeRequisicoes(string message) : base(message)
        {
        }

        public static ErroExcessoDeRequisicoes Criar(IEnumerable<string> messages)
        {
            StringBuilder builder = new StringBuilder();

            foreach (var item in messages)
                builder.Append($"{item}, ");

            var mensagem = builder.ToString();

            mensagem = mensagem.TrimEnd(' ');
            mensagem = mensagem.TrimEnd(',');

            return new ErroExcessoDeRequisicoes(mensagem);
        }

        public static ErroExcessoDeRequisicoes Criar(ValidationResult result)
            => Criar(result.Errors.Select(x => x.ErrorMessage));
        
    }
}
