using FluentValidation;

namespace SmartSchool.Aplicacao.Professores.Alterar.Validacao
{
	public class AlterarProfessorValidacao : AbstractValidator<AlterarProfessorCommand>
	{
		public AlterarProfessorValidacao()
		{
			this.RuleFor(x => x.Nome).NotEmpty()
				.WithMessage("Nome do Professor deve ser informado.");

			this.RuleFor(x => x.Matricula).NotEmpty()
				.WithMessage("Matrícula de Professor deve ser informada.");

			this.RuleFor(p => p.Nome)
				.MaximumLength(160).WithMessage("Nome do Professor não pode passar de 160 caracteres.");

			this.RuleFor(x => x.Disciplinas).NotEmpty()
				.WithMessage("Deve ser informado ao menos uma Disciplina.");
		}
	}
}
