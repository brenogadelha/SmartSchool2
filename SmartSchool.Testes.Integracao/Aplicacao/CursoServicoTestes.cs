using FluentAssertions;
using SmartSchool.Aplicacao.Cursos.Interface;
using SmartSchool.Aplicacao.Cursos.Servico;
using SmartSchool.Dados.Comum;
using SmartSchool.Dados.Contextos;
using SmartSchool.Dados.Modulos.Cursos;
using SmartSchool.Dados.Modulos.Usuarios;
using SmartSchool.Dominio.Disciplinas;
using SmartSchool.Dto.Curso;
using SmartSchool.Dto.Disciplinas;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace SmartSchool.Testes.Integracao.Aplicacao
{
	public class CursoServicoTestes : TesteIntegracao
	{
		private readonly IUnidadeDeTrabalho _contextos;
		private readonly ICursoServico _cursoServico;

		private readonly DisciplinaDto _disciplinaDto1;
		private readonly DisciplinaDto _disciplinaDto2;
		private readonly DisciplinaDto _disciplinaDto3;

		private readonly Disciplina _disciplina1;
		private readonly Disciplina _disciplina2;
		private readonly Disciplina _disciplina3;

		public CursoServicoTestes()
		{
			this._contextos = ContextoFactory.Criar();

			var cursoRepositorio = new CursoRepositorio(this._contextos);
			var disciplinaRepositorio = new DisciplinaRepositorio(this._contextos);

			this._cursoServico = new CursoServico(cursoRepositorio, disciplinaRepositorio);

			// Criação de Disciplinas
			this._disciplinaDto1 = new DisciplinaDto() { Nome = "Linguagens Formais e Automatos", Periodo = 1 };
			this._disciplinaDto2 = new DisciplinaDto() { Nome = "Teoria em Grafos", Periodo = 2 };
			this._disciplinaDto3 = new DisciplinaDto() { Nome = "Projeto Integrador", Periodo = 3 };

			this._disciplina1 = Disciplina.Criar(_disciplinaDto1);
			this._disciplina2 = Disciplina.Criar(_disciplinaDto2);
			this._disciplina3 = Disciplina.Criar(_disciplinaDto3);

			this._contextos.SmartContexto.Disciplinas.Add(_disciplina1);
			this._contextos.SmartContexto.Disciplinas.Add(_disciplina2);
			this._contextos.SmartContexto.Disciplinas.Add(_disciplina3);
			this._contextos.SmartContexto.SaveChanges();
		}

		[Fact(DisplayName = "Inclui Curso, obtém de volta (Por ID), Altera, exclui e verifica exclusão")]
		public void DeveCriarCursoObterExcluirVerificar()
		{
			var disciplinas = new List<Guid>() { _disciplina1.ID, _disciplina2.ID, _disciplina3.ID };

			var cursoDto = new CursoDto() { Nome = "Engenharia da Computação", DisciplinasId = disciplinas };

			this._cursoServico.CriarCurso(cursoDto);

			var cursoObtidoPorNome = this._contextos.SmartContexto.Cursos.SingleOrDefault(x => x.Nome == cursoDto.Nome);

			cursoObtidoPorNome.Should().NotBeNull();
			cursoObtidoPorNome.ID.Should().NotBe(Guid.Empty);

			var cursoObtidoPorId = this._cursoServico.ObterPorId(cursoObtidoPorNome.ID);

			cursoObtidoPorId.Should().NotBeNull();
			cursoObtidoPorId.ID.Should().NotBe(Guid.Empty);
			cursoObtidoPorId.Nome.Should().Be(cursoDto.Nome);
			cursoObtidoPorId.Disciplinas.Where(x => x == _disciplina1.Nome).Count().Should().Be(1);
			cursoObtidoPorId.Disciplinas.Where(x => x == _disciplina2.Nome).Count().Should().Be(1);
			cursoObtidoPorId.Disciplinas.Where(x => x == _disciplina3.Nome).Count().Should().Be(1);
			cursoObtidoPorId.Disciplinas.Count().Should().Be(3);

			// instancia alteração	
			var disciplinas2 = new List<Guid>() { _disciplina2.ID, _disciplina3.ID };
			var cursoDtoAlteracao = new AlterarCursoDto() { Nome = "Ciência da Computação", DisciplinasId = disciplinas2 };

			this._cursoServico.AlterarCurso(cursoObtidoPorId.ID, cursoDtoAlteracao);

			//obtém o Curso alterado do banco de dados
			var cursoDtoAlteradoVindoDoBanco = this._cursoServico.ObterPorId(cursoObtidoPorNome.ID);

			cursoDtoAlteradoVindoDoBanco.ID.Should().Be(cursoObtidoPorId.ID);
			cursoDtoAlteradoVindoDoBanco.Nome.Should().Be("Ciência da Computação");
			cursoDtoAlteradoVindoDoBanco.Disciplinas.Count().Should().Be(2);
			cursoDtoAlteradoVindoDoBanco.Disciplinas.Where(x => x == _disciplina2.Nome).Count().Should().Be(1);
			cursoDtoAlteradoVindoDoBanco.Disciplinas.Where(x => x == _disciplina3.Nome).Count().Should().Be(1);

			//Deleta Curso
			this._cursoServico.Remover(cursoDtoAlteradoVindoDoBanco.ID);

			//obtém novamente e verifica exclusão
			var cursoObtidoPorNomeAposExclusao = this._contextos.SmartContexto.Cursos.SingleOrDefault(x => x.Nome == cursoDtoAlteradoVindoDoBanco.Nome);

			cursoObtidoPorNomeAposExclusao.Should().BeNull();
		}

		[Fact(DisplayName = "Obtém a lista de Cursos com sucesso")]
		public void DeveListarTodosCursos()
		{
			var disciplinas = new List<Guid>() { _disciplina1.ID, _disciplina2.ID, _disciplina3.ID };
			var disciplinas2 = new List<Guid>() { _disciplina2.ID, _disciplina3.ID };
			var disciplinas3 = new List<Guid>() { _disciplina1.ID, _disciplina2.ID };
			var disciplinas4 = new List<Guid>() { _disciplina1.ID, _disciplina3.ID };

			var cursoDto = new CursoDto() { Nome = "Engenharia da Computação", DisciplinasId = disciplinas };
			var cursoDto2 = new CursoDto() { Nome = "Ciência da Computação", DisciplinasId = disciplinas2 };
			var cursoDto3 = new CursoDto() { Nome = "Análise de Sistemas", DisciplinasId = disciplinas3 };
			var cursoDto4 = new CursoDto() { Nome = "Redes", DisciplinasId = disciplinas4 };

			this._cursoServico.CriarCurso(cursoDto);
			this._cursoServico.CriarCurso(cursoDto2);
			this._cursoServico.CriarCurso(cursoDto3);
			this._cursoServico.CriarCurso(cursoDto4);

			//Obtemos todos os ativos
			var cursosObtidos = this._cursoServico.Obter().ToList();

			cursosObtidos.Should().NotBeNull();
			cursosObtidos.Count.Should().Be(4);
			cursosObtidos.Where(x => x.Nome == "Engenharia da Computação").Count().Should().Be(1);
			cursosObtidos.Where(x => x.Nome == "Ciência da Computação").Count().Should().Be(1);
			cursosObtidos.Where(x => x.Nome == "Análise de Sistemas").Count().Should().Be(1);
			cursosObtidos.Where(x => x.Nome == "Redes").Count().Should().Be(1);
		}
	}
}
