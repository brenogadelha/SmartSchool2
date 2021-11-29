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

			this.RuleFor(p => p.Nome)
				.MaximumLength(32).WithMessage("Nome do Aluno não pode passar de 32 caracteres.");

			this.RuleFor(p => p.Sobrenome)
				.MaximumLength(128).WithMessage("Sobrenome do Aluno não pode passar de 128 caracteres.");

			this.RuleFor(x => x.Sobrenome).NotEmpty()
				.WithMessage("Sobrenome de Aluno deve ser informado.");			

			this.RuleFor(x => x.DataNascimento).LessThan(DateTime.Now)
				.WithMessage("Data de Nascimento deve ser anterior a hoje.");

			this.RuleFor(x => x.DataNascimento).NotEmpty()
				.WithMessage("Data de Nascimento deve ser informada.");

			this.RuleFor(p => p.DataInicio).LessThan(p => p.DataFim)
				.WithMessage("Data de início do Aluno deve ser anterior à Data de Fim do curso prevista.");
		}
	}
}
