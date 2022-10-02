using FluentValidation;

namespace SmartSchool.Aplicacao.Semestres.Alterar.Validacao
{
	public class AlterarSemestreValidacao : AbstractValidator<AlterarSemestreCommand>
	{
		public AlterarSemestreValidacao()
		{
			this.RuleFor(p => p.DataInicio).LessThan(p => p.DataFim)
				.WithMessage("Data de início do Semestre deve ser anterior à Data de Fim prevista.");
		}
	}
}
