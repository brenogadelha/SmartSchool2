using FluentAssertions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SmartSchool.Aplicacao.Tccs.Adicionar;
using SmartSchool.Comum.Repositorio;
using SmartSchool.Dados.Comum;
using SmartSchool.Dados.Contextos;
using SmartSchool.Dados.Modulos.Professores;
using SmartSchool.Dados.Modulos.Tccs;
using SmartSchool.Dominio.Comum.Results;
using SmartSchool.Dominio.Disciplinas;
using SmartSchool.Dominio.Professores;
using SmartSchool.Dominio.Professores.Servicos;
using SmartSchool.Dominio.Tccs;
using SmartSchool.Dominio.Tccs.Servicos;
using System;
using System.Collections.Generic;
using Xunit;

namespace SmartSchool.Testes.API.Controllers.Tccs
{
	public class CriarTccTestes : TesteApi
	{
		private readonly IUnidadeDeTrabalho _contextos;
		private readonly IMediator _mediator;

		private readonly Professor _professor1;
		private readonly Professor _professor2;
		private readonly Disciplina _disciplina;

		public CriarTccTestes()
		{
			this._contextos = ContextoFactory.Criar();

			var tccRepositorio = new TccRepositorio(this._contextos);
			var professorRepositorio = new ProfessorRepositorio(this._contextos);

			var tccDominio = new TccServicoDominio(tccRepositorio);
			var professorDominio = new ProfessorServicoDominio(professorRepositorio);

			var serviceProvider = GetServiceProviderComMediatR((typeof(IRepositorio<Tcc>), tccRepositorio),
				(typeof(ITccServicoDominio), tccDominio), (typeof(IProfessorServicoDominio), professorDominio));

			this._mediator = serviceProvider.GetRequiredService<IMediator>();

			// Criação de Professores
			this._disciplina = Disciplina.Criar("Cálculo I", 1);

			this._professor1 = Professor.Criar("José", 1, "josepaulo@unicarioca.com.br", new List<Guid> { this._disciplina.ID });
			this._professor2 = Professor.Criar("Paulo Augusto", 1, "pauloaugusto@unicarioca.com.br", new List<Guid> { this._disciplina.ID });

			this._contextos.SmartContexto.Disciplinas.Add(_disciplina);
			this._contextos.SmartContexto.Professores.Add(_professor1);
			this._contextos.SmartContexto.Professores.Add(_professor2);
			this._contextos.SmartContexto.SaveChanges();
		}

		[Fact(DisplayName = "Inclui Tcc com sucesso")]
		public async void DeveCriarTcc()
		{
			var professores = new List<Guid>() { _professor1.ID, _professor2.ID };

			var tccCommand = new AdicionarTccCommand() { Tema = "Visualização de Dados e Automação", Descricao = "descrição tema", Professores = professores };

			var retorno = await this._mediator.Send(tccCommand);

			var result = retorno.Should().BeOfType<Result<Guid>>().Subject;

			result.Value.Should().NotBeEmpty();
		}
	}
}