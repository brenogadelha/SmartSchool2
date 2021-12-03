using FluentAssertions;
using SmartSchool.Comum.TratamentoErros;
using SmartSchool.Dominio.Cursos;
using SmartSchool.Dto.Curso;
using System;
using System.Collections.Generic;
using Xunit;

namespace SmartSchool.Testes.Unidade.Dominio
{
	public class CursoTestes : TesteUnidade
	{
		public CursoTestes(){}

		[Fact(DisplayName = "Criação e Alteração de Curso com Sucesso")]
		public void DeveCriarNovoUsuario()
		{
			var disciplinas = new List<Guid>();
			disciplinas.Add(Guid.NewGuid());

			var cursoDto = new CursoDto() { Nome = "Engenharia da Computação", DisciplinasId = disciplinas };

			var curso = Curso.Criar(cursoDto);

			curso.Should().NotBeNull();
			curso.ID.Should().NotBe(Guid.Empty);
			curso.Nome.Should().Be(cursoDto.Nome);
			curso.Disciplinas.Should().NotBeEmpty();

			// Alteração

			curso.AlterarNome("Ciência da Computação");

			curso.Nome.Should().Be("Ciência da Computação");
		}

		public static IEnumerable<object[]> DadosPraTestesException =>
		new List<object[]>
		{
			new object[] { "", new List<Guid>() { Guid.NewGuid() }, "Nome do Curso deve ser informado." },
			new object[] { null, new List<Guid>() { Guid.NewGuid() }, "Nome do Curso deve ser informado." },
			new object[] { "nome do curso com mais de 80 caracteres para validação de banco de dados nome do curso com mais de 80 caracteres para validação de banco de dados", new List<Guid>() { Guid.NewGuid() }, "Nome do Curso não pode passar de 80 caracteres." },
			new object[] { "Engenharia da Computação", null, "Deve ser informado ao menos uma Disciplina." },

		};

		[Theory(DisplayName = "Obtém Exception ao Criar Curso com valores Errados, Nulos ou Vazios")]
		[MemberData(nameof(DadosPraTestesException))]
		public void ObterExceptionAoCriarCursoComValoresNulosOuVazios(string nome, List<Guid> disciplinas, string erro)
		{
			var cursoDto = new CursoDto() { Nome = nome, DisciplinasId = disciplinas };

			var exception = Assert.Throws<ErroNegocioException>(() => Curso.Criar(cursoDto));
			Assert.Equal(erro, exception.Message);
		}
	}
}
