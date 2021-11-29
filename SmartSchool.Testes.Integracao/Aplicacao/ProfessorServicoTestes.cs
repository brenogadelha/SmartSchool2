using FluentAssertions;
using SmartSchool.Aplicacao.Professores.Interface;
using SmartSchool.Aplicacao.Professores.Servico;
using SmartSchool.Dados.Comum;
using SmartSchool.Dados.Contextos;
using SmartSchool.Dados.Modulos.Usuarios;
using SmartSchool.Dominio.Disciplinas;
using SmartSchool.Dto.Disciplinas;
using SmartSchool.Dto.Dtos.Professores;
using SmartSchool.Dto.Professores;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace SmartSchool.Testes.Integracao.Aplicacao
{
	public class ProfessorServicoTestes : TesteIntegracao
	{
		private readonly IUnidadeDeTrabalho _contextos;
		private readonly IProfessorServico _professorServico;

		public ProfessorServicoTestes()
		{
			this._contextos = ContextoFactory.Criar();

			var professorRepositorio = new ProfessorRepositorio(this._contextos);
			var disciplinaRepositorio = new DisciplinaRepositorio(this._contextos);

			this._professorServico = new ProfessorServico(professorRepositorio, disciplinaRepositorio);
		}

		[Fact(DisplayName = "Inclui Professor, obtém de volta (Por ID), Altera, exclui e verifica exclusão")]
		public void DeveCriarProfessorObterExcluirVerificar()
		{
			// Criação de Professores
			var disciplinaDto1 = new DisciplinaDto() { Nome = "Linguagens Formais e Automatos", Periodo = 1 };
			var disciplinaDto2 = new DisciplinaDto() { Nome = "Teoria em Grafos", Periodo = 2 };
			var disciplinaDto3 = new DisciplinaDto() { Nome = "Projeto Integrador", Periodo = 3 };

			var disciplina1 = Disciplina.Criar(disciplinaDto1);
			var disciplina2 = Disciplina.Criar(disciplinaDto2);
			var disciplina3 = Disciplina.Criar(disciplinaDto3);

			this._contextos.SmartContexto.Disciplinas.Add(disciplina1);
			this._contextos.SmartContexto.Disciplinas.Add(disciplina2);
			this._contextos.SmartContexto.Disciplinas.Add(disciplina3);
			this._contextos.SmartContexto.SaveChanges();

			var professorDto = new ProfessorDto() { Matricula = 2017100150, Nome = "Paulo Roberto", Disciplinas = new List<Guid>() { disciplina1.ID, disciplina2.ID, disciplina3.ID } };

			this._professorServico.CriarProfessor(professorDto);

			var professorObtidoPorNome = this._contextos.SmartContexto.Professores.SingleOrDefault(x => x.Nome == professorDto.Nome);

			professorObtidoPorNome.Should().NotBeNull();
			professorObtidoPorNome.ID.Should().NotBe(Guid.Empty);

			var professorObtidoPorId = this._professorServico.ObterPorId(professorObtidoPorNome.ID);

			professorObtidoPorId.Should().NotBeNull();
			professorObtidoPorId.ID.Should().NotBe(Guid.Empty);
			professorObtidoPorId.Nome.Should().Be(professorDto.Nome);
			professorObtidoPorId.Matricula.Should().Be(professorDto.Matricula);

			// instancia alteração	

			var professorDtoAlteracao = new AlterarProfessorDto() { Matricula = 2018100150, Nome = "João Lucas", Disciplinas = new List<Guid>() { disciplina1.ID, disciplina2.ID } };

			this._professorServico.AlterarProfessor(professorObtidoPorId.ID, professorDtoAlteracao);

			//obtém o Professor alterado do banco de dados
			var professorDtoAlteradoVindoDoBanco = this._professorServico.ObterPorId(professorObtidoPorNome.ID);

			professorDtoAlteradoVindoDoBanco.ID.Should().Be(professorObtidoPorId.ID);
			professorDtoAlteradoVindoDoBanco.Nome.Should().Be("João Lucas");
			professorDtoAlteradoVindoDoBanco.Matricula.Should().Be(2018100150);

			//Deleta Professor
			this._professorServico.Remover(professorDtoAlteradoVindoDoBanco.ID);

			//obtém novamente e verifica exclusão
			var professorObtidoPorNomeAposExclusao = this._contextos.SmartContexto.Professores.SingleOrDefault(x => x.Nome == professorDtoAlteradoVindoDoBanco.Nome);

			professorObtidoPorNomeAposExclusao.Should().BeNull();
		}

		[Fact(DisplayName = "Obtém a lista de Professores com sucesso")]
		public void DeveListarTodosProfessores()
		{
			// Criação de Disciplinas
			var disciplinaDto1 = new DisciplinaDto() { Nome = "Cálculo I", Periodo = 1 };
			var disciplinaDto2 = new DisciplinaDto() { Nome = "Cálculo II", Periodo = 2 };
			var disciplinaDto3 = new DisciplinaDto() { Nome = "Cálculo III", Periodo = 3 };

			var disciplina1 = Disciplina.Criar(disciplinaDto1);
			var disciplina2 = Disciplina.Criar(disciplinaDto2);
			var disciplina3 = Disciplina.Criar(disciplinaDto3);

			this._contextos.SmartContexto.Disciplinas.Add(disciplina1);
			this._contextos.SmartContexto.Disciplinas.Add(disciplina2);
			this._contextos.SmartContexto.Disciplinas.Add(disciplina3);
			this._contextos.SmartContexto.SaveChanges();

			var professorDto = new ProfessorDto() { Matricula = 2017100150, Nome = "Estevão jose", Disciplinas = new List<Guid>() { disciplina1.ID, disciplina2.ID, disciplina3.ID } };
			var professorDto2 = new ProfessorDto() { Matricula = 2018100150, Nome = "Luis Roberto", Disciplinas = new List<Guid>() { disciplina1.ID, disciplina2.ID } };
			var professorDto3 = new ProfessorDto() { Matricula = 2019100150, Nome = "Angelo Cardoso", Disciplinas = new List<Guid>() { disciplina2.ID, disciplina3.ID } };

			this._professorServico.CriarProfessor(professorDto);
			this._professorServico.CriarProfessor(professorDto2);
			this._professorServico.CriarProfessor(professorDto3);

			//Obtemos todos os ativos
			var professoresObtidos = this._professorServico.Obter().ToList();

			professoresObtidos.Should().NotBeNull();
			professoresObtidos.Count.Should().Be(3);
			professoresObtidos.Where(x => x.Nome == "Estevão jose").Count().Should().Be(1);
			professoresObtidos.Where(x => x.Nome == "Luis Roberto").Count().Should().Be(1);
			professoresObtidos.Where(x => x.Nome == "Angelo Cardoso").Count().Should().Be(1);
		}
	}		
}
