using FluentAssertions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SmartSchool.Aplicacao.Tccs.Desvincular;
using SmartSchool.Comum.Repositorio;
using SmartSchool.Dados.Comum;
using SmartSchool.Dados.Contextos;
using SmartSchool.Dados.Modulos.Tccs;
using SmartSchool.Dominio.Alunos;
using SmartSchool.Dominio.Comum.Results;
using SmartSchool.Dominio.Tccs;
using SmartSchool.Testes.API.Controllers.Alunos;
using System.Linq;
using Xunit;

namespace SmartSchool.Testes.API.Controllers.Tccs
{
	public class DesvincularAlunoTccTestes : TesteApi
	{
		private readonly IUnidadeDeTrabalho _contextos;
		private readonly IMediator _mediator;

		private readonly TccBuilder _tccBuilder;
		private readonly AlunoBuilder _alunoBuilder;

		private readonly Aluno _aluno;

		public DesvincularAlunoTccTestes()
		{
			this._contextos = ContextoFactory.Criar();
			this._tccBuilder = new TccBuilder();
			this._alunoBuilder = new AlunoBuilder();

			var tccAlunoProfessorRepositorio = new TccAlunoProfessorRepositorio(this._contextos);

			var serviceProvider = GetServiceProviderComMediatR((typeof(IRepositorio<TccAlunoProfessor>), tccAlunoProfessorRepositorio));

			this._mediator = serviceProvider.GetRequiredService<IMediator>();

			var tcc = this._tccBuilder.ObterTcc();
			this._aluno = this._alunoBuilder.ObterAluno();

			var tccAlunoProfessor = TccAlunoProfessor.Criar(tcc.ID, tcc.ProfessoresIds.FirstOrDefault(), this._aluno.ID, "solicitação");

			this._contextos.SmartContexto.TccAlunosProfessores.Add(tccAlunoProfessor);
			this._contextos.SmartContexto.SaveChanges();
		}

		[Fact(DisplayName = "Desvincula Tcc do aluno com sucesso")]
		public async void DeveRemoverTcc()
		{
			var requestDesvincularTcc = await this._mediator.Send(new DesvincularTccCommand { ID = this._aluno.ID });

			requestDesvincularTcc.Status.Should().Be(Result.Success().Status);
		}
	}
}