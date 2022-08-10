//using FluentAssertions;
//using SmartSchool.API.Controllers;
//using SmartSchool.Aplicacao.Alunos.Interface;
//using SmartSchool.Aplicacao.Alunos.Servico;
//using SmartSchool.Comum.Dominio.Enums;
//using SmartSchool.Dados.Comum;
//using SmartSchool.Dados.Contextos;
//using SmartSchool.Dados.Modulos.Alunos;
//using SmartSchool.Dados.Modulos.Cursos;
//using SmartSchool.Dados.Modulos.Semestres;
//using SmartSchool.Dados.Modulos.Usuarios;
//using SmartSchool.Dominio.Cursos;
//using SmartSchool.Dominio.Disciplinas;
//using SmartSchool.Dominio.Semestres;
//using SmartSchool.Dto.Alunos;
//using SmartSchool.Dto.Alunos.Obter;
//using SmartSchool.Dto.Curso;
//using SmartSchool.Dto.Disciplinas;
//using SmartSchool.Dto.Semestres;
//using SmartSchool.Testes.Compartilhado.Builders;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using Xunit;

//namespace SmartSchool.Testes.API.Controllers
//{
//	public class AlunoControllerTestes : TesteApi
//	{
//		private readonly IUnidadeDeTrabalho _contextos;
//		private readonly IAlunoServico _alunoServico;
//		private readonly AlunoDtoBuilder _alunoDtoBuilder;
//		private readonly AlunoController _alunoController;

//		private readonly DisciplinaDto _disciplinaDto1;
//		private readonly DisciplinaDto _disciplinaDto2;
//		private readonly DisciplinaDto _disciplinaDto3;

//		private readonly Disciplina _disciplina1;
//		private readonly Disciplina _disciplina2;
//		private readonly Disciplina _disciplina3;

//		private readonly SemestreDto _semestreDto;
//		private readonly Semestre _semestre;

//		public AlunoControllerTestes()
//		{
//			this._contextos = ContextoFactory.Criar();

//			var alunoRepositorio = new AlunoRepositorio(this._contextos);
//			var cursoRepositorio = new CursoRepositorio(this._contextos);
//			var disciplinaRepositorio = new DisciplinaRepositorio(this._contextos);
//			var semestreRepositorio = new SemestreRepositorio(this._contextos);

//			this._alunoServico = new AlunoServico(alunoRepositorio, disciplinaRepositorio, semestreRepositorio, cursoRepositorio);
//			this._alunoController = new AlunoController(this._alunoServico);
						
//			// Criação de Disciplinas
//			this._disciplinaDto1 = new DisciplinaDto() { Nome = "Cálculo I", Periodo = 1 };
//			this._disciplinaDto2 = new DisciplinaDto() { Nome = "Cálculo II", Periodo = 2 };
//			this._disciplinaDto3 = new DisciplinaDto() { Nome = "Cálculo III", Periodo = 3 };

//			this._disciplina1 = Disciplina.Criar(_disciplinaDto1);
//			this._disciplina2 = Disciplina.Criar(_disciplinaDto2);
//			this._disciplina3 = Disciplina.Criar(_disciplinaDto3);

//			var disciplinasIds = new List<Guid>() { this._disciplina1.ID, this._disciplina2.ID };

//			// Criação de Curso
//			var cursoDto = new CursoDto() { Nome = "Engenharia da Computação", DisciplinasId = disciplinasIds };
//			var curso = Curso.Criar(cursoDto);

//			// Criação de Semestre
//			this._semestreDto = new SemestreDto() { DataInicio = DateTime.Now.AddDays(-20), DataFim = DateTime.Now.AddYears(4) };
//			this._semestre = Semestre.Criar(_semestreDto);

//			this._contextos.SmartContexto.Cursos.Add(curso);
//			this._contextos.SmartContexto.Disciplinas.Add(_disciplina1);
//			this._contextos.SmartContexto.Disciplinas.Add(_disciplina2);
//			this._contextos.SmartContexto.Disciplinas.Add(_disciplina3);
//			this._contextos.SmartContexto.Semestres.Add(_semestre);
//			this._contextos.SmartContexto.SaveChanges();

//			var alunoDisciplinaDto = new AlunoDisciplinaDto()
//			{
//				DisciplinaId = _disciplina1.ID,
//				Periodo = 1,
//				SemestreId = _semestre.ID,
//				StatusDisciplina = StatusDisciplinaEnum.Cursando
//			};

//			List<AlunoDisciplinaDto> alunosDisciplinas = new List<AlunoDisciplinaDto>();
//			alunosDisciplinas.Add(alunoDisciplinaDto);

//			this._alunoDtoBuilder = AlunoDtoBuilder.Novo
//				.ComCidade("Rio de Janeiro")
//				.ComCpfCnpj("48340829033")
//				.ComCursoId(curso.ID)
//				.ComAlunosDisciplinas(alunosDisciplinas)
//				.ComEndereco("Rua molina 423, Rio Comprido")
//				.ComCelular("99999999")
//				.ComDataNascimento(DateTime.Now.AddDays(-5000))
//				.ComDataInicio(DateTime.Now.AddDays(-20))
//				.ComDataFim(DateTime.Now.AddYears(4))
//				.ComEmail("estevao.pulante@unicarioca.com.br")
//				.ComNome("Estevão")
//				.ComSobrenome("Pulante")
//				.ComTelefone("2131593159")
//				.ComId(Guid.NewGuid());
//		}

//		[Fact(DisplayName = "Inclui Aluno, obtém de volta (Por ID e Matrícula), Altera, exclui e verifica exclusão")]
//		public void DeveCriarAlunoObterExcluirVerificar()
//		{
//			var alunoDto = this._alunoDtoBuilder.Instanciar();
//			this._alunoController.CriarAluno(alunoDto);

//			var alunoObtidoPorNome = this._contextos.SmartContexto.Alunos.SingleOrDefault(x => x.Nome == alunoDto.Nome);

//			alunoObtidoPorNome.Should().NotBeNull();
//			alunoObtidoPorNome.ID.Should().NotBe(Guid.Empty);

//			var alunoObtidoPorId = this._alunoController.ObterPorId(alunoObtidoPorNome.ID).Value as ObterAlunoDto;

//			alunoObtidoPorId.Should().NotBeNull();
//			alunoObtidoPorId.ID.Should().NotBe(Guid.Empty);
//			alunoObtidoPorId.Nome.Should().Be(alunoDto.Nome);
//			alunoObtidoPorId.Ativo.Should().Be(true);
//			alunoObtidoPorId.Sobrenome.Should().Be(alunoDto.Sobrenome);
//			alunoObtidoPorId.Celular.Should().Be(alunoDto.Celular);
//			alunoObtidoPorId.Endereco.Should().Be(alunoDto.Endereco);
//			alunoObtidoPorId.Cidade.Should().Be(alunoDto.Cidade);
//			alunoObtidoPorId.Cpf.Should().Be(alunoDto.Cpf);
//			alunoObtidoPorId.DataNascimento.ToString().Should().Contain(alunoDto.DataNascimento.ToString("dd/MM/yyyy"));
//			alunoObtidoPorId.DataInicio.ToString().Should().Contain(alunoDto.DataInicio.ToString("dd/MM/yyyy"));
//			alunoObtidoPorId.DataFim.ToString().Should().Contain(alunoDto.DataFim.ToString("dd/MM/yyyy"));
//			alunoObtidoPorId.Email.Should().Be(alunoDto.Email);
//			alunoObtidoPorId.Telefone.Should().Be(alunoDto.Telefone);
//			alunoObtidoPorId.Curso.Should().Be("Engenharia da Computação");

//			// Obtem por Matrícula
//			var alunoObtidoPorMatricula = this._alunoController.ObterPorMatricula(alunoObtidoPorNome.Matricula).Value as ObterAlunoDto;

//			alunoObtidoPorMatricula.Should().NotBeNull();
//			alunoObtidoPorMatricula.ID.Should().NotBe(Guid.Empty);
//			alunoObtidoPorMatricula.Nome.Should().Be(alunoDto.Nome);

//			// instancia alteração com novas disciplinas
//			var dataNascimentoNova = DateTime.Now.AddYears(-50);
//			var dataInicioNova = DateTime.Now.AddDays(-50);
//			var dataFimNova = DateTime.Now.AddYears(5);

//			var alunoDisciplinaDto = new AlunoDisciplinaDto()
//			{
//				DisciplinaId = this._disciplina1.ID,
//				Periodo = 1,
//				SemestreId = this._semestre.ID,
//				StatusDisciplina = StatusDisciplinaEnum.Cursando
//			};

//			var alunoDisciplinaDto1 = new AlunoDisciplinaDto()
//			{
//				DisciplinaId = this._disciplina2.ID,
//				Periodo = 2,
//				SemestreId = this._semestre.ID,
//				StatusDisciplina = StatusDisciplinaEnum.Cursando
//			};

//			var alunoDisciplinaDto2 = new AlunoDisciplinaDto()
//			{
//				DisciplinaId = this._disciplina3.ID,
//				Periodo = 2,
//				SemestreId = this._semestre.ID,
//				StatusDisciplina = StatusDisciplinaEnum.Cursando
//			};

//			List<AlunoDisciplinaDto> alunosDisciplinas = new List<AlunoDisciplinaDto>();
//			alunosDisciplinas.Add(alunoDisciplinaDto);
//			alunosDisciplinas.Add(alunoDisciplinaDto1);
//			alunosDisciplinas.Add(alunoDisciplinaDto2);

//			var alunoDtoAlteracao = AlunoDtoBuilder.Novo
//				.ComCelular("21912345999")
//				.ComAtivo(true)
//				.ComEndereco("Rua joazeiro norte 459, Rio Comprido")
//				.ComAlunosDisciplinas(alunosDisciplinas)
//				.ComCidade("São Paulo")
//				.ComCpfCnpj("85444471043")
//				.ComDataNascimento(dataNascimentoNova)
//				.ComDataInicio(dataInicioNova)
//				.ComDataFim(dataFimNova)
//				.ComEmail("estevao.russo@unicarioca.com.br")
//				.ComNome("Estevann")
//				.ComSobrenome("Russo")
//				.ComTelefone("2131592121")
//				.ComId(alunoObtidoPorId.ID).InstanciarAlteracao();

//			this._alunoController.AlterarAluno(alunoObtidoPorId.ID, alunoDtoAlteracao);

//			//obtém o Aluno alterado do banco de dados
//			var alunoDtoAlteradoVindoDoBanco = this._alunoController.ObterPorId(alunoObtidoPorNome.ID).Value as ObterAlunoDto;

//			alunoDtoAlteradoVindoDoBanco.ID.Should().Be(alunoObtidoPorId.ID);
//			alunoDtoAlteradoVindoDoBanco.Nome.Should().Be("Estevann");
//			alunoDtoAlteradoVindoDoBanco.Ativo.Should().Be(true);
//			alunoDtoAlteradoVindoDoBanco.Sobrenome.Should().Be("Russo");
//			alunoDtoAlteradoVindoDoBanco.Celular.Should().Be("21912345999");
//			alunoDtoAlteradoVindoDoBanco.Endereco.Should().Be("Rua joazeiro norte 459, Rio Comprido");
//			alunoDtoAlteradoVindoDoBanco.Cidade.Should().Be("São Paulo");
//			alunoDtoAlteradoVindoDoBanco.Cpf.Should().Be("85444471043");
//			alunoDtoAlteradoVindoDoBanco.DataNascimento.ToString().Should().Contain(dataNascimentoNova.ToString("dd/MM/yyyy"));
//			alunoDtoAlteradoVindoDoBanco.Email.Should().Be("estevao.russo@unicarioca.com.br");
//			alunoDtoAlteradoVindoDoBanco.Telefone.Should().Be("2131592121");
//			alunoDtoAlteradoVindoDoBanco.DataInicio.ToString().Should().Contain(dataInicioNova.ToString("dd/MM/yyyy"));
//			alunoDtoAlteradoVindoDoBanco.DataFim.ToString().Should().Contain(dataFimNova.ToString("dd/MM/yyyy"));

//			// Obtem Todo o Historico do Aluno
//			var historicoAluno = this._alunoController.ObterHistoricoPorIdAluno(alunoDtoAlteradoVindoDoBanco.ID).Value as IEnumerable<ObterHistoricoAlunoDto>;

//			historicoAluno.Count().Should().Be(3);
//			historicoAluno.Where(ha => ha.NomeDisciplina == this._disciplina1.Nome).Count().Should().Be(1);
//			historicoAluno.Where(ha => ha.NomeDisciplina == this._disciplina2.Nome).Count().Should().Be(1);
//			historicoAluno.Where(ha => ha.NomeDisciplina == this._disciplina3.Nome).Count().Should().Be(1);

//			// Obtem Historico por Periodo (2)
//			var historicoAlunoPorPeriodo = this._alunoController.ObterHistoricoPorIdAluno(alunoDtoAlteradoVindoDoBanco.ID, 2).Value as IEnumerable<ObterHistoricoAlunoDto>;

//			historicoAlunoPorPeriodo.Count().Should().Be(2);
//			historicoAlunoPorPeriodo.Where(ha => ha.NomeDisciplina == this._disciplina2.Nome).Count().Should().Be(1);
//			historicoAlunoPorPeriodo.Where(ha => ha.NomeDisciplina == this._disciplina3.Nome).Count().Should().Be(1);

//			//Deleta Aluno
//			this._alunoController.RemoverAluno(alunoDtoAlteradoVindoDoBanco.ID);

//			//obtém novamente e verifica exclusão
//			var alunoObtidoPorNomeAposExclusao = this._contextos.SmartContexto.Alunos.SingleOrDefault(x => x.Nome == alunoDtoAlteradoVindoDoBanco.Nome);

//			alunoObtidoPorNomeAposExclusao.Ativo.Should().Be(false);
//		}

//		[Fact(DisplayName = "Obtém a lista de Alunos com sucesso")]
//		public void DeveListarTodosAlunos()
//		{
//			var aluno0Dto = this._alunoDtoBuilder.Instanciar();

//			this._alunoController.CriarAluno(aluno0Dto);

//			var aluno1Dto = AlunoDtoBuilder.Novo
//				.ComCelular("21912399999")
//				.ComCursoId(this._alunoDtoBuilder.Instanciar().CursoId)
//				.ComAlunosDisciplinas(this._alunoDtoBuilder.Instanciar().AlunosDisciplinas)
//				.ComEndereco("Rua molina 423, Rio Comprido")
//				.ComCidade("São Paulo")
//				.ComCpfCnpj("05228025081")
//				.ComDataNascimento(DateTime.Now.AddYears(-40))
//				.ComDataInicio(DateTime.Now.AddDays(-50))
//				.ComDataFim(DateTime.Now.AddYears(4))
//				.ComEmail("estevao.russo@unicarioca.com.br")
//				.ComNome("Estevann")
//				.ComSobrenome("Russo")
//				.ComTelefone("2131592121")
//				.ComId(Guid.NewGuid()).Instanciar();

//			this._alunoController.CriarAluno(aluno1Dto);

//			var aluno2Dto = AlunoDtoBuilder.Novo
//				.ComCelular("21912388899")
//				.ComCursoId(this._alunoDtoBuilder.Instanciar().CursoId)
//				.ComAlunosDisciplinas(this._alunoDtoBuilder.Instanciar().AlunosDisciplinas)
//				.ComEndereco("Rua molina 423, Rio Comprido")
//				.ComCidade("Espirito Santo")
//				.ComCpfCnpj("51886437076")
//				.ComDataNascimento(DateTime.Now.AddYears(-50))
//				.ComDataInicio(DateTime.Now.AddDays(20))
//				.ComDataFim(DateTime.Now.AddYears(4))
//				.ComEmail("estevao.russao@unicarioca.com.br")
//				.ComNome("Estevann")
//				.ComSobrenome("Russao")
//				.ComTelefone("2131592222")
//				.ComId(Guid.NewGuid()).Instanciar();

//			this._alunoController.CriarAluno(aluno2Dto);

//			var aluno3Dto = AlunoDtoBuilder.Novo
//				.ComCelular("21912388877")
//				.ComEndereco("Rua molina 423, Rio Comprido")
//				.ComCidade("Minas Gerais")
//				.ComCursoId(this._alunoDtoBuilder.Instanciar().CursoId)
//				.ComAlunosDisciplinas(this._alunoDtoBuilder.Instanciar().AlunosDisciplinas)
//				.ComCpfCnpj("84012584057")
//				.ComDataNascimento(DateTime.Now.AddYears(-60))
//				.ComDataInicio(DateTime.Now.AddDays(40))
//				.ComDataFim(DateTime.Now.AddYears(4))
//				.ComEmail("estevao.mineiro@unicarioca.com.br")
//				.ComNome("Jordan")
//				.ComSobrenome("Mineiro")
//				.ComTelefone("2131593353")
//				.ComId(Guid.NewGuid()).Instanciar();

//			this._alunoController.CriarAluno(aluno3Dto);

//			var alunoObtidoPorNome = this._contextos.SmartContexto.Alunos.SingleOrDefault(x => x.Nome == aluno3Dto.Nome);

//			//Deleta um Aluno - Deve ficar inativo
//			this._alunoController.RemoverAluno(alunoObtidoPorNome.ID);

//			//Obtemos todos os ativos
//			var alunosObtidos = this._alunoController.ObterTodos().Value as List<ObterAlunoDto>;

//			alunosObtidos.Should().NotBeNull();
//			alunosObtidos.Count.Should().Be(3);
//			alunosObtidos.Where(x => x.Nome == "Jordan").Count().Should().Be(0);
//		}

//		[Fact(DisplayName = "Obtém a lista de Alunos por parte do Nome com sucesso")]
//		public void DeveBuscarAlunoPorNomeLoginParcial()
//		{
//			var aluno0Dto = this._alunoDtoBuilder.Instanciar();

//			this._alunoController.CriarAluno(aluno0Dto);

//			var aluno1Dto = AlunoDtoBuilder.Novo
//				.ComCelular("21912399999")
//				.ComEndereco("Rua molina 423, Rio Comprido")
//				.ComCidade("São Paulo")
//				.ComCursoId(this._alunoDtoBuilder.Instanciar().CursoId)
//				.ComAlunosDisciplinas(this._alunoDtoBuilder.Instanciar().AlunosDisciplinas)
//				.ComCpfCnpj("05228025081")
//				.ComDataNascimento(DateTime.Now.AddYears(-25))
//				.ComDataInicio(DateTime.Now.AddDays(20))
//				.ComDataFim(DateTime.Now.AddYears(4))
//				.ComEmail("sunamita.pulante@unicarioca.com.br")
//				.ComNome("Sunamita")
//				.ComSobrenome("Pulante")
//				.ComTelefone("2131592121")
//				.ComId(Guid.NewGuid()).Instanciar();

//			this._alunoController.CriarAluno(aluno1Dto);

//			var aluno2Dto = AlunoDtoBuilder.Novo
//				.ComCelular("21912388899")
//				.ComEndereco("Rua molina 423, Rio Comprido")
//				.ComCidade("Espirito Santo")
//				.ComCursoId(this._alunoDtoBuilder.Instanciar().CursoId)
//				.ComAlunosDisciplinas(this._alunoDtoBuilder.Instanciar().AlunosDisciplinas)
//				.ComCpfCnpj("51886437076")
//				.ComDataNascimento(DateTime.Now.AddYears(-30))
//				.ComDataInicio(DateTime.Now.AddDays(-50))
//				.ComDataFim(DateTime.Now.AddYears(4))
//				.ComEmail("estevao.russao@unicarioca.com.br")
//				.ComNome("Estevann")
//				.ComSobrenome("Russao")
//				.ComTelefone("2131592222")
//				.ComId(Guid.NewGuid()).Instanciar();

//			this._alunoController.CriarAluno(aluno2Dto);

//			var aluno3Dto = AlunoDtoBuilder.Novo
//				.ComCelular("21912388877")
//				.ComCursoId(this._alunoDtoBuilder.Instanciar().CursoId)
//				.ComAlunosDisciplinas(this._alunoDtoBuilder.Instanciar().AlunosDisciplinas)
//				.ComEndereco("Rua molina 423, Rio Comprido")
//				.ComCidade("Minas Gerais")
//				.ComCpfCnpj("84012584057")
//				.ComDataNascimento(DateTime.Now.AddYears(-40))
//				.ComDataInicio(DateTime.Now.AddDays(-10))
//				.ComDataFim(DateTime.Now.AddYears(2))
//				.ComEmail("jordania.mineiro@unicarioca.com.br")
//				.ComNome("Jordania")
//				.ComSobrenome("Mineiro")
//				.ComTelefone("2131593353")
//				.ComId(Guid.NewGuid()).Instanciar();

//			this._alunoController.CriarAluno(aluno3Dto);

//			var aluno4Dto = AlunoDtoBuilder.Novo
//				.ComCelular("21912399997")
//				.ComCursoId(this._alunoDtoBuilder.Instanciar().CursoId)
//				.ComAlunosDisciplinas(this._alunoDtoBuilder.Instanciar().AlunosDisciplinas)
//				.ComEndereco("Rua molina 423, Rio Comprido")
//				.ComCidade("Minas Gerais")
//				.ComCpfCnpj("78755724019")
//				.ComDataNascimento(DateTime.Now.AddYears(-50))
//				.ComDataInicio(DateTime.Now.AddDays(-20))
//				.ComDataFim(DateTime.Now.AddYears(4))
//				.ComEmail("estevao.mineiro@unicarioca.com.br")
//				.ComNome("Jordan")
//				.ComSobrenome("Cartman")
//				.ComTelefone("2131593353")
//				.ComId(Guid.NewGuid()).Instanciar();

//			this._alunoController.CriarAluno(aluno4Dto);

//			var alunoObtidoPorNome = this._contextos.SmartContexto.Alunos.SingleOrDefault(x => x.Nome == aluno4Dto.Nome);

//			//Deleta um aluno que não deve retornar em nenhuma busca
//			this._alunoController.RemoverAluno(alunoObtidoPorNome.ID);

//			//Busca por nome parcial
//			var alunosObtidosPorNomeParcial = this._alunoController.ObterPorNomeSobrenomeParcial("este").Value as List<ObterAlunoDto>;

//			alunosObtidosPorNomeParcial.Should().NotBeNull();
//			alunosObtidosPorNomeParcial.Count.Should().Be(2);
//			alunosObtidosPorNomeParcial.Where(x => x.Nome == "Estevão").Count().Should().Be(1);
//			alunosObtidosPorNomeParcial.Where(x => x.Nome == "Estevann").Count().Should().Be(1);

//			//Busca por Sobrenome parcial
//			var alunosObtidosPorSobrenomeParcial = this._alunoController.ObterPorNomeSobrenomeParcial("Pula").Value as List<ObterAlunoDto>;

//			alunosObtidosPorSobrenomeParcial.Should().NotBeNull();
//			alunosObtidosPorSobrenomeParcial.Count.Should().Be(2);
//			alunosObtidosPorSobrenomeParcial.Where(x => x.Nome == "Estevão").Count().Should().Be(1);
//			alunosObtidosPorSobrenomeParcial.Where(x => x.Nome == "Sunamita").Count().Should().Be(1);

//			//Busca por Nome e Sobrenome Parcial
//			var alunosObtidosPorNomeParcial2 = this._alunoController.ObterPorNomeSobrenomeParcial("JORdania Minei").Value as List<ObterAlunoDto>;

//			alunosObtidosPorNomeParcial2.Should().NotBeNull();
//			alunosObtidosPorNomeParcial2.Count.Should().Be(1);
//			alunosObtidosPorNomeParcial2.Where(x => x.Nome == "Jordania").Count().Should().Be(1);
//		}
//	}
//}
