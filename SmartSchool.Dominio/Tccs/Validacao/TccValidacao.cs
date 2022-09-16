using FluentValidation;

namespace SmartSchool.Dominio.Tccs.Validacao
{
	public class TccValidacao : AbstractValidator<Tcc>
	{
		public TccValidacao()
		{
			this.RuleFor(x => x.Tema).NotEmpty()
				.WithMessage("Tema do TCC deve ser informado.");

			this.RuleFor(p => p.Tema)
				.MaximumLength(160).WithMessage("Tema do TCC não pode passar de 160 caracteres.");

			this.RuleFor(p => p.Descricao)
				.MaximumLength(3008).WithMessage("Descrição do TCC não pode passar de 3008 caracteres.");
		}
	}
}
