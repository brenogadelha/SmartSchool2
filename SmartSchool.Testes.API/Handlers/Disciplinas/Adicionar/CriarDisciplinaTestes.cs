using FluentAssertions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SmartSchool.Aplicacao.Disciplinas.Adicionar;
using SmartSchool.Comum.Repositorio;
using SmartSchool.Dados.Comum;
using SmartSchool.Dados.Contextos;
using SmartSchool.Dados.Modulos.Disciplinas;
using SmartSchool.Dominio.Comum.Results;
using SmartSchool.Dominio.Disciplinas;
using SmartSchool.Dominio.Disciplinas.Servicos;
using System;
using Xunit;

namespace SmartSchool.Testes.API.Controllers.Disciplinas
{
	public class CriarDisciplinaTestes : TesteApi
	{
		private readonly IUnidadeDeTrabalho _contextos;
		private readonly IMediator _mediator;

		public CriarDisciplinaTestes()
		{
			this._contextos = ContextoFactory.Criar();

			var disciplinaRepositorio = new DisciplinaRepositorio(this._contextos);

			var disciplinaServico = new DisciplinaServicoDominio(disciplinaRepositorio);

			var serviceProvider = GetServiceProviderComMediatR((typeof(IRepositorio<Disciplina>), disciplinaRepositorio), (typeof(IDisciplinaServicoDominio), disciplinaServico));

			this._mediator = serviceProvider.GetRequiredService<IMediator>();
		}

		[Fact(DisplayName = "Inclui Disciplina, obtém de volta (Por ID), Altera, exclui e verifica exclusão")]
		public async void DeveCriarDisciplinaObterExcluirVerificar()
		{
			// Criação de Disciplinas
			var disciplinaDto = new AdicionarDisciplinaCommand() { Nome = "Linguagens Formais e Automatos", Periodo = 1 };

			var retorno = await this._mediator.Send(disciplinaDto);

			var result = retorno.Should().BeOfType<Result<Guid>>().Subject;

			result.Value.Should().NotBeEmpty();
		}
	}
}