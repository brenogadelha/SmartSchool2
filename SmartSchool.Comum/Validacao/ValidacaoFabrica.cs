using FluentValidation;
using SmartSchool.Comum.TratamentoErros;
using System.Linq;

namespace SmartSchool.Comum.Validacao
{
	public static class ValidacaoFabrica
	{
		public static void Validar<T>(T objeto, AbstractValidator<T> validador) {

			var validacaoResult = validador.Validate(objeto);

			if (!validacaoResult.IsValid)
				throw ErroNegocioException.Criar(validacaoResult.Errors.Select(x => x.ErrorMessage));
		}
	}
}
