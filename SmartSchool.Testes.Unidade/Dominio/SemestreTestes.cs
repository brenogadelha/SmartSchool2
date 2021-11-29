using FluentAssertions;
using SmartSchool.Comum.TratamentoErros;
using SmartSchool.Dominio.Semestres;
using SmartSchool.Dto.Semestres;
using System;
using System.Collections.Generic;
using Xunit;

namespace SmartSchool.Testes.Unidade.Dominio
{
	public class SemestreTestes : TesteUnidade
	{

		public SemestreTestes() { }

		[Fact(DisplayName = "Criação e Alteração de Disciplina com Sucesso")]
		public void DeveCriarNovoSemestre()
		{
			var semestreDto = new SemestreDto() { DataInicio = DateTime.Now, DataFim = DateTime.Now.AddMonths(4) };

			var semestre = Semestre.Criar(semestreDto);

			semestre.Should().NotBeNull();
			semestre.ID.Should().NotBe(Guid.Empty);
			semestre.DataInicio.ToString().Should().Contain(semestreDto.DataInicio.ToString());
			semestre.DataFim.ToString().Should().Contain(semestreDto.DataFim.ToString());

			// Alteração

			var novaDataInicio = DateTime.Now.AddDays(10);
			var novaDataFim = DateTime.Now.AddMonths(5);

			semestre.AlterarDataInicio(novaDataInicio);
			semestre.AlterarDataFim(novaDataFim);

			semestre.DataInicio.ToString().Should().Contain(novaDataInicio.ToString());
			semestre.DataFim.ToString().Should().Contain(novaDataFim.ToString());
		}

		public static IEnumerable<object[]> DadosPraTestesException =>
		new List<object[]>
		{
			new object[] { DateTime.Now, DateTime.Now.AddDays(-5), "Data de início do Semestre deve ser anterior à Data de Fim prevista." }
		};

		[Theory(DisplayName = "Obtém Exception ao Criar Semestre com valores Errados, Nulos ou Vazios")]
		[MemberData(nameof(DadosPraTestesException))]
		public void ObterExceptionAoCriarSemestreComValoresNulosOuVazios(DateTime dataInicio, DateTime dataFim, string erro)
		{
			var semestreDto = new SemestreDto() { DataInicio = dataInicio, DataFim = dataFim };

			var exception = Assert.Throws<ErroNegocioException>(() => Semestre.Criar(semestreDto));
			Assert.Equal(erro, exception.Message);
		}
	}
}
