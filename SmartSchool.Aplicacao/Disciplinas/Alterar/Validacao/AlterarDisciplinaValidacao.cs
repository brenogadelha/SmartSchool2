using FluentValidation;

namespace SmartSchool.Aplicacao.Disciplinas.Alterar.Validacao
{
	public class AlterarDisciplinaValidacao : AbstractValidator<AlterarDisciplinaCommand>
	{		
		public AlterarDisciplinaValidacao()
		{

			this.RuleFor(x => x.Nome).NotEmpty()
				.WithMessage("Nome da Disciplina deve ser informado.");

			this.RuleFor(p => p.Nome)
				.MaximumLength(80).WithMessage("Nome da Disciplina não pode passar de 80 caracteres.");

			this.RuleFor(p => p.Periodo)
				.NotEmpty().WithMessage("Período referente a Disciplina deve ser informado.");
		}
	}
}
