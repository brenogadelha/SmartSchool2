using FluentAssertions;
using SmartSchool.Aplicacao.Semestres.Interface;
using SmartSchool.Aplicacao.Semestres.Servico;
using SmartSchool.Dados.Comum;
using SmartSchool.Dados.Contextos;
using SmartSchool.Dados.Modulos.Semestres;
using SmartSchool.Dto.Semestres;
using System;
using System.Linq;
using Xunit;

namespace SmartSchool.Testes.Integracao.Aplicacao
{
	public class SemestreServicoTestes : TesteIntegracao
	{
		private readonly IUnidadeDeTrabalho _contextos;
		private readonly ISemestreServico _semestreServico;

		public SemestreServicoTestes()
		{
			this._contextos = ContextoFactory.Criar();

			var semestreRepositorio = new SemestreRepositorio(this._contextos);

			this._semestreServico = new SemestreServico(semestreRepositorio);
		}

		[Fact(DisplayName = "Inclui Semestre, obtém de volta (Por ID), Altera, exclui e verifica exclusão")]
		public void DeveCriarSemestreObterExcluirVerificar()
		{
			var semestreDto = new SemestreDto() { DataInicio = DateTime.Now, DataFim = DateTime.Now.AddMonths(4) };

			this._semestreServico.CriarSemestre(semestreDto);

			var semestreObtidoPorDataInicio = this._contextos.SmartContexto.Semestres.FirstOrDefault();

			semestreObtidoPorDataInicio.Should().NotBeNull();
			semestreObtidoPorDataInicio.ID.Should().NotBe(Guid.Empty);

			var semestreObtidoPorId = this._semestreServico.ObterPorId(semestreObtidoPorDataInicio.ID);

			semestreObtidoPorId.Should().NotBeNull();
			semestreObtidoPorId.DataInicio.ToString().Should().Contain(semestreDto.DataInicio.ToString());
			semestreObtidoPorId.DataFim.ToString().Should().Contain(semestreDto.DataFim.ToString());

			// instancia alteração	
			var novaDataInicio = DateTime.Now.AddDays(10);
			var novaDataFim = DateTime.Now.AddMonths(5);

			var semestreDtoAlteracao = new AlterarObterSemestreDto() { DataInicio = novaDataInicio, DataFim = novaDataFim };

			this._semestreServico.AlterarSemestre(semestreObtidoPorId.ID, semestreDtoAlteracao);

			//obtém o Semestre alterado do banco de dados
			var semestreDtoAlteradoVindoDoBanco = this._semestreServico.ObterPorId(semestreObtidoPorDataInicio.ID);

			semestreDtoAlteradoVindoDoBanco.ID.Should().Be(semestreObtidoPorId.ID);
			semestreDtoAlteradoVindoDoBanco.DataInicio.ToString().Should().Contain(novaDataInicio.ToString());
			semestreDtoAlteradoVindoDoBanco.DataFim.ToString().Should().Contain(novaDataFim.ToString());

			//Deleta Semestre
			this._semestreServico.Remover(semestreDtoAlteradoVindoDoBanco.ID);

			//obtém novamente e verifica exclusão
			var semestreObtidoPorDataInicioAposExclusao = this._contextos.SmartContexto.Semestres.FirstOrDefault();

			semestreObtidoPorDataInicioAposExclusao.Should().BeNull();
		}

		[Fact(DisplayName = "Obtém a lista de Semestres com sucesso")]
		public void DeveListarTodosSemestres()
		{
			var semestreDto = new SemestreDto() { DataInicio = DateTime.Now, DataFim = DateTime.Now.AddMonths(4) };
			var semestreDto2 = new SemestreDto() { DataInicio = DateTime.Now.AddDays(10), DataFim = DateTime.Now.AddMonths(2) };
			var semestreDto3 = new SemestreDto() { DataInicio = DateTime.Now.AddDays(15), DataFim = DateTime.Now.AddMonths(3) };
			var semestreDto4 = new SemestreDto() { DataInicio = DateTime.Now.AddDays(20), DataFim = DateTime.Now.AddMonths(5) };

			this._semestreServico.CriarSemestre(semestreDto);
			this._semestreServico.CriarSemestre(semestreDto2);
			this._semestreServico.CriarSemestre(semestreDto3);
			this._semestreServico.CriarSemestre(semestreDto4);

			//Obtemos todos os ativos
			var semestresObtidos = this._semestreServico.Obter().ToList();

			semestresObtidos.Should().NotBeNull();
			semestresObtidos.Count.Should().Be(4);
			semestresObtidos.Where(x => x.DataInicio == semestreDto.DataInicio).Count().Should().Be(1);
			semestresObtidos.Where(x => x.DataInicio == semestreDto2.DataInicio).Count().Should().Be(1);
			semestresObtidos.Where(x => x.DataInicio == semestreDto3.DataInicio).Count().Should().Be(1);
			semestresObtidos.Where(x => x.DataInicio == semestreDto4.DataInicio).Count().Should().Be(1);
		}
	}
}
