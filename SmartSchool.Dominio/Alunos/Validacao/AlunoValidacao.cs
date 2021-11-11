using FluentValidation;
using SmartSchool.Comum.Validacao;
using SmartSchool.Dto.Alunos;
using System;

namespace SmartSchool.Dominio.Alunos.Validacao
{
	public class AlunoValidacao : AbstractValidator<AlunoDto>
	{		
		public AlunoValidacao()
		{
			this.RuleFor(x => x.Cpf).Must(x => Validar.CpfCnpj(x))
				.WithMessage("CPF de Aluno é inválido.");

			this.RuleFor(x => x.Email).NotEmpty()
				.WithMessage("Email de Aluno deve ser informado.");

			this.RuleFor(x => x.Email).Must(x => Validar.Email(x)).Unless(x => string.IsNullOrEmpty(x.Email))
				.WithMessage("Email de Aluno é inválido.");

			this.RuleFor(x => x.Nome).NotEmpty()
				.WithMessage("Nome de Aluno deve ser informado.");

			this.RuleFor(x => x.Sobrenome).NotEmpty()
				.WithMessage("Sobrenome de Aluno deve ser informado.");			

			this.RuleFor(x => x.DataNascimento).LessThan(DateTime.Now)
				.WithMessage("Data de Nascimento deve ser anterior a hoje.");

			this.RuleFor(x => x.DataNascimento).NotEmpty()
				.WithMessage("Data de Nascimento deve ser informada.");
		}
	}
}
