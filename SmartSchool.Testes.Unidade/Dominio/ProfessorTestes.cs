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

			var professorDto = new ProfessorDto() { Matricula = 2017100150, Nome = "Paulo Roberto", Email = "pauloroberto@unicarioca.com.br", Disciplinas = disciplinas };

			var professor = Professor.Criar(professorDto);

			professor.Should().NotBeNull();
			professor.ID.Should().NotBe(Guid.Empty);
			professor.Nome.Should().Be(professorDto.Nome);
			professor.Email.Should().Be(professorDto.Email);
			professor.Matricula.Should().Be(professorDto.Matricula);

			// Alteração

			professor.AlterarNome("Estevan");
			professor.AlterarEmail("pauloroberto@professor.unicarioca.com.br");
			professor.AlterarMatricula(2017100120);

			professor.Nome.Should().Be("Estevan");
			professor.Matricula.Should().Be(2017100120);
			professor.Email.Should().Be("pauloroberto@professor.unicarioca.com.br");
		}

		public static IEnumerable<object[]> DadosPraTestesException =>
		new List<object[]>
		{
			new object[] { "", 2017100150, "pauloroberto@unicarioca.com.br", new List<Guid>() { Guid.NewGuid() }, "Nome do Professor deve ser informado." },
			new object[] { null, 2017100150, "pauloroberto@unicarioca.com.br", new List<Guid>() { Guid.NewGuid() }, "Nome do Professor deve ser informado." },
			new object[] { "nome do Professor com mais de 160 caracteres para validação de banco de dados nome do Professor com mais de 160 caracteres para validação de banco de dados nome do Professor com mais de 160 caracteres para validação de banco de dados", 2017100150, "pauloroberto@unicarioca.com.br", new List<Guid>() { Guid.NewGuid() }, "Nome do Professor não pode passar de 160 caracteres." },
			new object[] { "João Paulo", 2017100150, "pauloroberto@unicarioca.com.br", null, "Deve ser informado ao menos uma Disciplina." },
			new object[] { "João Paulo", null, "pauloroberto@unicarioca.com.br", new List<Guid>() { Guid.NewGuid() }, "Matrícula de Professor deve ser informada." },
			new object[] { "João Paulo", 2017100150, string.Empty, new List<Guid>() { Guid.NewGuid() }, "Email do Professor deve ser informado." }

		};

		[Theory(DisplayName = "Obtém Exception ao Criar Professor com valores Errados, Nulos ou Vazios")]
		[MemberData(nameof(DadosPraTestesException))]
		public void ObterExceptionAoCriarProfessorComValoresNulosOuVazios(string nome, int matricula, string email, List<Guid> disciplinas, string erro)
		{
			var professorDto = new ProfessorDto() { Nome = nome, Matricula = matricula, Email = email, Disciplinas = disciplinas };

			var exception = Assert.Throws<ErroNegocioException>(() => Professor.Criar(professorDto));
			Assert.Equal(erro, exception.Message);
		}
	}
}
