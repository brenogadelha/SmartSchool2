using FluentValidation;

namespace SmartSchool.Aplicacao.Cursos.Alterar.Validacao
{
	public class AlterarCursoValidacao : AbstractValidator<AlterarCursoCommand>
	{
		public AlterarCursoValidacao()
		{
			this.RuleFor(x => x.Nome).NotEmpty()
				.WithMessage("Nome do Curso deve ser informado.");

			this.RuleFor(p => p.Nome)
				.MaximumLength(80).WithMessage("Nome do Curso não pode passar de 80 caracteres.");

			this.RuleFor(x => x.DisciplinasId).NotEmpty()
				.WithMessage("Deve ser informado ao menos uma Disciplina.");
		}
	}
}
