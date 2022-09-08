using FluentAssertions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SmartSchool.Aplicacao.Semestres.Adicionar;
using SmartSchool.Comum.Repositorio;
using SmartSchool.Dados.Comum;
using SmartSchool.Dados.Contextos;
using SmartSchool.Dados.Modulos.Semestres;
using SmartSchool.Dominio.Comum.Results;
using SmartSchool.Dominio.Semestres;
using System;
using Xunit;

namespace SmartSchool.Testes.API.Controllers.Semestres
{
	public class AdicionarSemestreTeste : TesteApi
	{
		private readonly IUnidadeDeTrabalho _contextos;
		private readonly IMediator _mediator;

		public AdicionarSemestreTeste()
		{
			this._contextos = ContextoFactory.Criar();
			var semestreRepositorio = new SemestreRepositorio(this._contextos);

			var serviceProvider = GetServiceProviderComMediatR((typeof(IRepositorio<Semestre>), semestreRepositorio));

			this._mediator = serviceProvider.GetRequiredService<IMediator>();
		}

		[Fact(DisplayName = "Adiciona Semestre")]
		public async void DeveCriarSemestre()
		{
			var semestreDto = new AdicionarSemestreCommand() { DataInicio = DateTime.Now, DataFim = DateTime.Now.AddMonths(4) };

			var retorno = await this._mediator.Send(semestreDto);

			var result = retorno.Should().BeOfType<Result<Guid>>().Subject;

			result.Value.Should().NotBeEmpty();
		}
	}
}
