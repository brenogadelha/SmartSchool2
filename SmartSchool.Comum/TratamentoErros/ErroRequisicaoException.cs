using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentValidation.Results;

namespace SmartSchool.Comum.TratamentoErros
{
	public class ErroRequisicaoException : Exception
	{
		public ErroRequisicaoException(string message) : base(message)
		{
		}

		public static ErroRequisicaoException Criar(IEnumerable<string> messages)
		{
			StringBuilder builder = new StringBuilder();

			foreach (var item in messages)
				builder.Append($"{item}, ");

			var mensagem = builder.ToString();

			mensagem = mensagem.TrimEnd(' ');
			mensagem = mensagem.TrimEnd(',');

			return new ErroRequisicaoException(mensagem);
		}

		public static ErroRequisicaoException Criar(ValidationResult result)
			=> Criar(result.Errors.Select(x => x.ErrorMessage));

	}
}
