using FluentAssertions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
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
using SmartSchool.Dto.Curso;
using SmartSchool.Dto.Disciplinas;
using SmartSchool.Dto.Semestres;
using SmartSchool.Testes.Compartilhado.Builders;
using System;
using System.Collections.Generic;
using Xunit;

namespace SmartSchool.Testes.API.Controllers.Alunos
{
	public class CriarAlunoTestes : TesteApi
	{
		private readonly IUnidadeDeTrabalho _contextos;
		private readonly IMediator _mediator;
		private readonly AlunoDtoBuilder _alunoDtoBuilder;

		private readonly DisciplinaDto _disciplinaDto1;
		private readonly DisciplinaDto _disciplinaDto2;
		private readonly DisciplinaDto _disciplinaDto3;

		private readonly Disciplina _disciplina1;
		private readonly Disciplina _disciplina2;
		private readonly Disciplina _disciplina3;

		private readonly SemestreDto _semestreDto;
		private readonly Semestre _semestre;

		private readonly Curso _curso;

		public CriarAlunoTestes()
		{
			this._contextos = ContextoFactory.Criar();

			var alunoRepositorioTask = new AlunoRepositorio(this._contextos);
			var cursoRepositorioTask = new CursoRepositorio(this._contextos);
			var disciplinaRepositorioTask = new DisciplinaRepositorio(this._contextos);
			var semestreRepositorioTask = new SemestreRepositorio(this._contextos);

			var cursoDominio = new CursoServicoDominio(cursoRepositorioTask);
			var alunoDominio = new AlunoServicoDominio(alunoRepositorioTask);
			var disciplinaDominio = new DisciplinaServicoDominio(disciplinaRepositorioTask);
			var semestreDominio = new SemestreServicoDominio(semestreRepositorioTask);

			var serviceProvider = GetServiceProviderComMediatR((typeof(IRepositorio<Aluno>), alunoRepositorioTask), (typeof(IDisciplinaServicoDominio), disciplinaDominio),
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
			var curso = Curso.Criar(cursoDto);

			var cursoDto2 = new CursoDto() { Nome = "Engenharia da Computação2", DisciplinasId = disciplinasIds };
			_curso = Curso.Criar(cursoDto2);

			// Criação de Semestre
			this._semestreDto = new SemestreDto() { DataInicio = DateTime.Now.AddDays(-20), DataFim = DateTime.Now.AddYears(4) };
			this._semestre = Semestre.Criar(_semestreDto);

			this._contextos.SmartContexto.Cursos.Add(curso);
			this._contextos.SmartContexto.Cursos.Add(_curso);
			this._contextos.SmartContexto.Disciplinas.Add(_disciplina1);
			this._contextos.SmartContexto.Disciplinas.Add(_disciplina2);
			this._contextos.SmartContexto.Disciplinas.Add(_disciplina3);
			this._contextos.SmartContexto.Semestres.Add(_semestre);
			this._contextos.SmartContexto.SaveChanges();

			var alunoDisciplinaDto = new AlunoDisciplinaDto()
			{
				DisciplinaId = _disciplina1.ID,
				Periodo = 1,
				SemestreId = _semestre.ID,
				StatusDisciplina = StatusDisciplina.Cursando
			};

			List<AlunoDisciplinaDto> alunosDisciplinas = new List<AlunoDisciplinaDto>();
			alunosDisciplinas.Add(alunoDisciplinaDto);

			this._alunoDtoBuilder = AlunoDtoBuilder.Novo
				.ComCidade("Rio de Janeiro")
				.ComCpfCnpj("48340829033")
				.ComCursoId(curso.ID)
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
		}

		[Fact(DisplayName = "Cria Aluno")]
		public async void DeveCriarAlunoObterExcluirVerificar()
		{
			var request = this._alunoDtoBuilder.InstanciarCommand();

			var retorno = await this._mediator.Send(request);
			var result = retorno.Should().BeOfType<Result<Guid>>().Subject;

			result.Value.Should().NotBeEmpty();
		}
	}
}
