using MediatR;
using SmartSchool.Dominio.Comum.Results;
using System;

namespace SmartSchool.Aplicacao.Semestres.Adicionar
{
	public class AdicionarSemestreCommand : IRequest<IResult>
    {
		public DateTime DataInicio { get; set; }
		public DateTime DataFim { get; set; }
	}
}
