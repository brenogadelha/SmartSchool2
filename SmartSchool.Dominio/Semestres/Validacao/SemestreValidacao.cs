using FluentValidation;
using SmartSchool.Dto.Semestres;

namespace SmartSchool.Dominio.Semestres.Validacao
{
	public class SemestreValidacao : AbstractValidator<Semestre>
	{
		public SemestreValidacao()
		{
			this.RuleFor(p => p.DataInicio).LessThan(p => p.DataFim)
				.WithMessage("Data de início do Semestre deve ser anterior à Data de Fim prevista.");
		}
	}
}
