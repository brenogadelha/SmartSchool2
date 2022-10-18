using FluentAssertions;
using SmartSchool.Comum.TratamentoErros;
using SmartSchool.Dominio.Disciplinas;
using SmartSchool.Dto.Disciplinas;
using System;
using System.Collections.Generic;
using Xunit;

namespace SmartSchool.Testes.Unidade.Dominio
{
	public class DisciplinaTestes : TesteUnidade
	{
		public DisciplinaTestes() { }

		[Fact(DisplayName = "Criação e Alteração de Disciplina com Sucesso")]
		public void DeveCriarNovoUsuario()
		{
			var disciplinaDto = new DisciplinaDto() { Nome = "Cálculo I", Periodo = 1 };

			var disciplina = Disciplina.Criar(disciplinaDto);

			disciplina.Should().NotBeNull();
			disciplina.ID.Should().NotBe(Guid.Empty);
			disciplina.Nome.Should().Be(disciplinaDto.Nome);
			disciplina.Periodo.Should().Be((PeriodoDisciplinaEnum)disciplinaDto.Periodo);

			// Alteração

			disciplina.AlterarNome("Cálculo II");
			disciplina.AlterarPeriodo(2);

			disciplina.Nome.Should().Be("Cálculo II");
			disciplina.Periodo.Should().Be((PeriodoDisciplinaEnum)2);
		}

		public static IEnumerable<object[]> DadosPraTestesException =>
		new List<object[]>
		{
			new object[] { "", 1, "Nome da Disciplina deve ser informado." },
			new object[] { null, 1, "Nome da Disciplina deve ser informado." },
			new object[] { "nome da Disciplina com mais de 80 caracteres para validação de banco de dados nome da Disciplina com mais de 80 caracteres para validação de banco de dados", 1, "Nome da Disciplina não pode passar de 80 caracteres." },
			new object[] { "Engenharia Economica", null, "Período referente a Disciplina deve ser informado." },

		};

		[Theory(DisplayName = "Obtém Exception ao Criar Disciplina com valores Errados, Nulos ou Vazios")]
		[MemberData(nameof(DadosPraTestesException))]
		public void ObterExceptionAoCriarDisciplinaComValoresNulosOuVazios(string nome, int periodo, string erro)
		{
			var disciplinaDto = new DisciplinaDto() { Nome = nome, Periodo = periodo };

			var exception = Assert.Throws<ErroNegocioException>(() => Disciplina.Criar(disciplinaDto));
			Assert.Equal(erro, exception.Message);
		}
	}
}
