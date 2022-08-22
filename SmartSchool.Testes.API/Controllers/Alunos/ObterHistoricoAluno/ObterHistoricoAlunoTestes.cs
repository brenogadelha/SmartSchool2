using FluentAssertions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SmartSchool.Aplicacao.Alunos.ObterHistoricoAluno;
using SmartSchool.Comum.Dominio.Enums;
using SmartSchool.Comum.Repositorio;
using SmartSchool.Dados.Comum;
using SmartSchool.Dados.Contextos;
using SmartSchool.Dados.Modulos.Alunos;
using SmartSchool.Dados.Modulos.Cursos;
using SmartSchool.Dados.Modulos.Disciplinas;
using SmartSchool.Dados.Modulos.Semestres;
using SmartSchool.Dominio.Alunos;
using SmartSchool.Dominio.Alunos.Servicos;
using SmartSchool.Dominio.Comum.Results;
using SmartSchool.Dominio.Cursos;
using SmartSchool.Dominio.Cursos.Servicos;
using SmartSchool.Dominio.Disciplinas;
using SmartSchool.Dominio.Disciplinas.Servicos;
using SmartSchool.Dominio.Semestres;
using SmartSchool.Dominio.Semestres.Servicos;
using SmartSchool.Dto.Alunos;
using SmartSchool.Dto.Alunos.Obter;
using SmartSchool.Dto.Curso;
using SmartSchool.Dto.Disciplinas;
using SmartSchool.Dto.Semestres;
using SmartSchool.Testes.Compartilhado.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace SmartSchool.Testes.API.Controllers.Alunos.ObterHistoricoAluno
{
	public class ObterHistoricoAlunoTestes : TesteApi
	{
		private readonly IUnidadeDeTrabalho _contextos;
		private readonly IMediator _mediator;
		private readonly AlunoDtoBuilder _alunoDtoBuilder;

		private readonly Aluno _aluno;
		//private readonly AlunoBuilder _alunoBuilder;

		private readonly DisciplinaDto _disciplinaDto1;
		private readonly DisciplinaDto _disciplinaDto2;
		private readonly DisciplinaDto _disciplinaDto3;

		private readonly Disciplina _disciplina1;
		private readonly Disciplina _disciplina2;
		private readonly Disciplina _disciplina3;

		private readonly SemestreDto _semestreDto;
		private readonly Semestre _semestre;

		private readonly Curso _curso;

		public ObterHistoricoAlunoTestes()
		{
			//this._alunoBuilder = new AlunoBuilder();
			this._contextos = ContextoFactory.Criar();

			var alunoRepositorioTask = new AlunoRepositorioTask(this._contextos);
			var cursoRepositorioTask = new CursoRepositorioTask(this._contextos);
			var disciplinaRepositorioTask = new DisciplinaRepositorioTask(this._contextos);
			var semestreRepositorioTask = new SemestreRepositorioTask(this._contextos);

			var cursoDominio = new CursoServicoDominio(cursoRepositorioTask);
			var alunoDominio = new AlunoServicoDominio(alunoRepositorioTask);
			var disciplinaDominio = new DisciplinaServicoDominio(disciplinaRepositorioTask);
			var semestreDominio = new SemestreServicoDominio(semestreRepositorioTask);

			var serviceProvider = GetServiceProviderComMediatR((typeof(IRepositorioTask<Aluno>), alunoRepositorioTask), (typeof(IDisciplinaServicoDominio), disciplinaDominio),
			(typeof(ISemestreServicoDominio), semestreDominio), (typeof(ICursoServicoDominio), cursoDominio), (typeof(IAlunoServicoDominio), alunoDominio));

			this._mediator = serviceProvider.GetRequiredService<IMediator>();

			// Criação de Disciplinas
			this._disciplinaDto1 = new DisciplinaDto() { Nome = "Cálculo I", Periodo = 1 };
			this._disciplinaDto2 = new DisciplinaDto() { Nome = "Cálculo II", Periodo = 2 };
			this._disciplinaDto3 = new DisciplinaDto() { Nome = "Cálculo III", Periodo = 3 };

			this._disciplina1 = Disciplina.Criar(_disciplinaDto1);
			this._disciplina2 = Disciplina.Criar(_disciplinaDto2);
			this._disciplina3 = Disciplina.Criar(_disciplinaDto3);

			var disciplinasIds = new List<Guid>() { this._disciplina1.ID, this._disciplina2.ID };

			// Criação de Curso
			var cursoDto = new CursoDto() { Nome = "Engenharia da Computação", DisciplinasId = disciplinasIds };
			this._curso = Curso.Criar(cursoDto);

			// Criação de Semestre
			this._semestreDto = new SemestreDto() { DataInicio = DateTime.Now.AddDays(-20), DataFim = DateTime.Now.AddYears(4) };
			this._semestre = Semestre.Criar(_semestreDto);

			this._contextos.SmartContexto.Cursos.Add(this._curso);
			this._contextos.SmartContexto.Disciplinas.Add(_disciplina1);
			this._contextos.SmartContexto.Disciplinas.Add(_disciplina2);
			this._contextos.SmartContexto.Disciplinas.Add(_disciplina3);
			this._contextos.SmartContexto.Semestres.Add(_semestre);

			var alunoDisciplinaDto = new AlunoDisciplinaDto()
			{
				DisciplinaId = _disciplina1.ID,
				Periodo = 1,
				SemestreId = _semestre.ID,
				StatusDisciplina = StatusDisciplina.Cursando
			};

			var alunoDisciplinaDto1 = new AlunoDisciplinaDto()
			{
				DisciplinaId = this._disciplina2.ID,
				Periodo = 2,
				SemestreId = this._semestre.ID,
				StatusDisciplina = StatusDisciplina.Cursando
			};

			var alunoDisciplinaDto2 = new AlunoDisciplinaDto()
			{
				DisciplinaId = this._disciplina3.ID,
				Periodo = 2,
				SemestreId = this._semestre.ID,
				StatusDisciplina = StatusDisciplina.Cursando
			};

			List<AlunoDisciplinaDto> alunosDisciplinas = new List<AlunoDisciplinaDto>();
			alunosDisciplinas.Add(alunoDisciplinaDto);
			alunosDisciplinas.Add(alunoDisciplinaDto1);
			alunosDisciplinas.Add(alunoDisciplinaDto2);

			this._alunoDtoBuilder = AlunoDtoBuilder.Novo
				.ComCidade("Rio de Janeiro")
				.ComCpfCnpj("48340829033")
				.ComCursoId(this._curso.ID)
				.ComAlunosDisciplinas(alunosDisciplinas)
				.ComEndereco("Rua molina 423, Rio Comprido")
				.ComCelular("99999999")
				.ComDataNascimento(DateTime.Now.AddDays(-5000))
				.ComDataInicio(DateTime.Now.AddDays(-20))
				.ComDataFim(DateTime.Now.AddYears(4))
				.ComEmail("estevao.pulante@unicarioca.com.br")
				.ComNome("Estevão")
				.ComSobrenome("Pulante")
				.ComTelefone("2131593159")
				.ComId(Guid.NewGuid());

			this._aluno = Aluno.Criar(this._alunoDtoBuilder.Instanciar());

			this._aluno.SemestresDisciplinas.Add(SemestreAlunoDisciplina.Criar(alunoDisciplinaDto.Periodo, alunoDisciplinaDto.SemestreId,
					alunoDisciplinaDto.DisciplinaId, this._aluno.ID, alunoDisciplinaDto.StatusDisciplina));

			this._aluno.SemestresDisciplinas.Add(SemestreAlunoDisciplina.Criar(alunoDisciplinaDto1.Periodo, alunoDisciplinaDto1.SemestreId,
					alunoDisciplinaDto1.DisciplinaId, this._aluno.ID, alunoDisciplinaDto1.StatusDisciplina));

			this._aluno.SemestresDisciplinas.Add(SemestreAlunoDisciplina.Criar(alunoDisciplinaDto2.Periodo, alunoDisciplinaDto2.SemestreId,
					alunoDisciplinaDto2.DisciplinaId, this._aluno.ID, alunoDisciplinaDto2.StatusDisciplina));

			this._contextos.SmartContexto.Alunos.Add(this._aluno);
			this._contextos.SmartContexto.SaveChanges();
		}

		[Fact(DisplayName = "Obtém Histórico de Aluno")]
		public async void DeveObterHistoricoAluno()
		{
			//var aluno = this._alunoBuilder.ObterAluno();

			var requestAlunoHistorico = await this._mediator.Send(new ObterHistoricoAlunoCommand { Id = this._aluno.ID });
			var historicoAluno = requestAlunoHistorico.Should().BeOfType<Result<IEnumerable<ObterHistoricoAlunoDto>>>().Subject;

			historicoAluno.Value.Should().NotBeNull();
			historicoAluno.Value.Count().Should().Be(3);
			historicoAluno.Value.Where(ha => ha.NomeDisciplina == "Cálculo I").Count().Should().Be(1);
			historicoAluno.Value.Where(ha => ha.Periodo == 2).Count().Should().Be(2);
			historicoAluno.Value.Where(ha => ha.StatusDisciplinaDescricao == "Cursando").Count().Should().Be(3);

			var requestAlunoHistoricoPorPeriodo = await this._mediator.Send(new ObterHistoricoAlunoCommand { Id = this._aluno.ID, Periodo = 2 });
			var historicoAlunoPorPeriodo = requestAlunoHistoricoPorPeriodo.Should().BeOfType<Result<IEnumerable<ObterHistoricoAlunoDto>>>().Subject;

			historicoAlunoPorPeriodo.Value.Count().Should().Be(2);
			historicoAlunoPorPeriodo.Value.Where(ha => ha.NomeDisciplina == "Cálculo II").Count().Should().Be(1);
			historicoAlunoPorPeriodo.Value.Where(ha => ha.NomeDisciplina == "Cálculo III").Count().Should().Be(1);
		}
	}
}
