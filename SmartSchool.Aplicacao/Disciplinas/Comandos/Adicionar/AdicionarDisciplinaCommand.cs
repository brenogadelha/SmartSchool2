using MediatR;
using SmartSchool.Dominio.Comum.Results;

namespace SmartSchool.Aplicacao.Disciplinas.Adicionar
{
	public class AdicionarDisciplinaCommand : IRequest<IResult>
    {
		public string Nome { get; set; }
		public int Periodo { get; set; }
	}
}
