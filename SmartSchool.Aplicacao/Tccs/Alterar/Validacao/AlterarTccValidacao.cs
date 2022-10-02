using FluentValidation;
using SmartSchool.Aplicacao.Tccs.Alterar;

namespace SmartSchool.Dominio.Tccs.Validacao
{
	public class AlterarTccValidacao : AbstractValidator<AlterarTccCommand>
	{
		public AlterarTccValidacao()
		{
			this.RuleFor(x => x.Tema).NotEmpty()
				.WithMessage("Tema do TCC deve ser informado.");

			this.RuleFor(x => x.Professores).NotEmpty()
				.WithMessage("Deve ser informado pelo menos um professor para orientar sobre o Tema.");

			this.RuleFor(p => p.Tema)
				.MaximumLength(160).WithMessage("Tema do TCC não pode passar de 160 caracteres.");

			this.RuleFor(p => p.Descricao)
				.MaximumLength(3008).WithMessage("Descrição do TCC não pode passar de 3008 caracteres.");
		}
	}
}
