using FluentAssertions;
using SmartSchool.Comum.TratamentoErros;
using SmartSchool.Dominio.Professores;
using SmartSchool.Dto.Professores;
using System;
using System.Collections.Generic;
using Xunit;

namespace SmartSchool.Testes.Unidade.Dominio
{
	public class ProfessorTestes : TesteUnidade
	{
		public ProfessorTestes() { }

		[Fact(DisplayName = "Criação e Alteração de Professor com Sucesso")]
		public void DeveCriarNovoUsuario()
		{
			var disciplinas = new List<Guid>();
			disciplinas.Add(Guid.NewGuid());

			var professorDto = new ProfessorDto() { Matricula = 2017100150, Nome = "Paulo Roberto", Disciplinas = disciplinas };

			var professor = Professor.Criar(professorDto);

			professor.Should().NotBeNull();
			professor.ID.Should().NotBe(Guid.Empty);
			professor.Nome.Should().Be(professorDto.Nome);
			professor.Matricula.Should().Be(professorDto.Matricula);

			// Alteração

			professor.AlterarNome("Estevan");
			professor.AlterarMatricula(2017100120);

			professor.Nome.Should().Be("Estevan");
			professor.Matricula.Should().Be(2017100120);
		}

		public static IEnumerable<object[]> DadosPraTestesException =>
		new List<object[]>
		{
			new object[] { "", 2017100150, new List<Guid>() { Guid.NewGuid() }, "Nome do Professor deve ser informado." },
			new object[] { null, 2017100150, new List<Guid>() { Guid.NewGuid() }, "Nome do Professor deve ser informado." },
			new object[] { "nome do Professor com mais de 160 caracteres para validação de banco de dados nome do Professor com mais de 160 caracteres para validação de banco de dados nome do Professor com mais de 160 caracteres para validação de banco de dados", 2017100150, new List<Guid>() { Guid.NewGuid() }, "Nome do Professor não pode passar de 160 caracteres." },
			new object[] { "João Paulo", 2017100150, null, "Deve ser informado ao menos uma Disciplina." },
			new object[] { "João Paulo", null, new List<Guid>() { Guid.NewGuid() }, "Matrícula de Professor deve ser informada." }

		};

		[Theory(DisplayName = "Obtém Exception ao Criar Professor com valores Errados, Nulos ou Vazios")]
		[MemberData(nameof(DadosPraTestesException))]
		public void ObterExceptionAoCriarProfessorComValoresNulosOuVazios(string nome, int matricula, List<Guid> disciplinas, string erro)
		{
			var professorDto = new ProfessorDto() { Nome = nome, Matricula = matricula, Disciplinas = disciplinas };

			var exception = Assert.Throws<ErroNegocioException>(() => Professor.Criar(professorDto));
			Assert.Equal(erro, exception.Message);
		}
	}
}
