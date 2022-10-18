//using FluentAssertions;
//using SmartSchool.Aplicacao.Disciplinas.Interface;
//using SmartSchool.Aplicacao.Disciplinas.Servico;
//using SmartSchool.Dados.Comum;
//using SmartSchool.Dados.Contextos;
//using SmartSchool.Dados.Modulos.Usuarios;
//using SmartSchool.Dto.Disciplinas;
//using SmartSchool.Dto.Disciplinas.Alterar;
//using System;
//using System.Linq;
//using Xunit;

//namespace SmartSchool.Testes.Integracao.Aplicacao
//{
//	public class DisciplinaServicoTestes : TesteIntegracao
//	{
//		private readonly IUnidadeDeTrabalho _contextos;
//		private readonly IDisciplinaServico _disciplinaServico;

//		public DisciplinaServicoTestes()
//		{
//			this._contextos = ContextoFactory.Criar();

//			var disciplinaRepositorio = new DisciplinaRepositorio(this._contextos);

//			this._disciplinaServico = new DisciplinaServico(disciplinaRepositorio);
//		}

//		[Fact(DisplayName = "Inclui Disciplina, obtém de volta (Por ID), Altera, exclui e verifica exclusão")]
//		public void DeveCriarDisciplinaObterExcluirVerificar()
//		{
//			// Criação de Disciplinas
//			var disciplinaDto = new DisciplinaDto() { Nome = "Linguagens Formais e Automatos", Periodo = 1 };

//			this._disciplinaServico.CriarDisciplina(disciplinaDto);

//			var disciplinaObtidaPorNome = this._contextos.SmartContexto.Disciplinas.SingleOrDefault(x => x.Nome == disciplinaDto.Nome);

//			disciplinaObtidaPorNome.Should().NotBeNull();
//			disciplinaObtidaPorNome.ID.Should().NotBe(Guid.Empty);

//			var disciplinaObtidaPorId = this._disciplinaServico.ObterPorId(disciplinaObtidaPorNome.ID);

//			disciplinaObtidaPorId.Should().NotBeNull();
//			disciplinaObtidaPorId.ID.Should().NotBe(Guid.Empty);
//			disciplinaObtidaPorId.Nome.Should().Be(disciplinaDto.Nome);
//			disciplinaObtidaPorId.Periodo.Should().Be(disciplinaDto.Periodo);

//			// instancia alteração	

//			var disciplinaDtoAlteracao = new AlterarDisciplinaDto() { Nome = "Teoria em Grafos", Periodo = 2 };

//			this._disciplinaServico.AlterarDisciplina(disciplinaObtidaPorId.ID, disciplinaDtoAlteracao);

//			//obtém a Disciplina alterado do banco de dados
//			var disciplinaDtoAlteradoVindoDoBanco = this._disciplinaServico.ObterPorId(disciplinaObtidaPorNome.ID);

//			disciplinaDtoAlteradoVindoDoBanco.ID.Should().Be(disciplinaObtidaPorId.ID);
//			disciplinaDtoAlteradoVindoDoBanco.Nome.Should().Be("Teoria em Grafos");
//			disciplinaDtoAlteradoVindoDoBanco.Periodo.Should().Be(2);

//			//Deleta Disciplina
//			this._disciplinaServico.Remover(disciplinaDtoAlteradoVindoDoBanco.ID);

//			//obtém novamente e verifica exclusão
//			var disciplinaObtidaPorNomeAposExclusao = this._contextos.SmartContexto.Disciplinas.SingleOrDefault(x => x.Nome == disciplinaDtoAlteradoVindoDoBanco.Nome);

//			disciplinaObtidaPorNomeAposExclusao.Should().BeNull();
//		}

//		[Fact(DisplayName = "Obtém a lista de Disciplinas com sucesso")]
//		public void DeveListarTodasDisciplinas()
//		{
//			// Criação de Disciplinas
//			var disciplinaDto1 = new DisciplinaDto() { Nome = "Cálculo I", Periodo = 1 };
//			var disciplinaDto2 = new DisciplinaDto() { Nome = "Cálculo II", Periodo = 2 };
//			var disciplinaDto3 = new DisciplinaDto() { Nome = "Cálculo III", Periodo = 3 };

//			this._disciplinaServico.CriarDisciplina(disciplinaDto1);
//			this._disciplinaServico.CriarDisciplina(disciplinaDto2);
//			this._disciplinaServico.CriarDisciplina(disciplinaDto3);

//			//Obtemos todos os ativos
//			var disciplinasObtidas = this._disciplinaServico.Obter().ToList();

//			disciplinasObtidas.Should().NotBeNull();
//			disciplinasObtidas.Count.Should().Be(3);
//			disciplinasObtidas.Where(x => x.Nome == "Cálculo I").Count().Should().Be(1);
//			disciplinasObtidas.Where(x => x.Nome == "Cálculo II").Count().Should().Be(1);
//			disciplinasObtidas.Where(x => x.Nome == "Cálculo III").Count().Should().Be(1);
//		}
//	}
//}
