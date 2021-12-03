using FluentValidation;
using SmartSchool.Dto.Disciplinas;
using System;

namespace SmartSchool.Dominio.Disciplinas.Validacao
{
	public class DisciplinaValidacao : AbstractValidator<DisciplinaDto>
	{		
		public DisciplinaValidacao()
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
