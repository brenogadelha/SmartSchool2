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

namespace SmartSchool.Testes.API.Controllers.Alunos.AlterarAluno
{
	public class AlterarAlunoTestes : TesteApi
	{
		private readonly IUnidadeDeTrabalho _contextos;
		private readonly IMediator _mediator;

		private readonly AlunoBuilder _alunoBuilder;

		private readonly DisciplinaDto _disciplinaDto1;
		private readonly DisciplinaDto _disciplinaDto2;
		private readonly DisciplinaDto _disciplinaDto3;

		private readonly Disciplina _disciplina1;
		private readonly Disciplina _disciplina2;
		private readonly Disciplina _disciplina3;

		private readonly SemestreDto _semestreDto;
		private readonly Semestre _semestre;

		private readonly Curso _curso;

		public AlterarAlunoTestes()
		{
			this._alunoBuilder = new AlunoBuilder(); 
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
			this._disciplinaDto1 = new DisciplinaDto() { Nome = "Física I", Periodo = 1 };
			this._disciplinaDto2 = new DisciplinaDto() { Nome = "Física II", Periodo = 2 };
			this._disciplinaDto3 = new DisciplinaDto() { Nome = "Física III", Periodo = 3 };

			this._disciplina1 = Disciplina.Criar(_disciplinaDto1);
			this._disciplina2 = Disciplina.Criar(_disciplinaDto2);
			this._disciplina3 = Disciplina.Criar(_disciplinaDto3);

			var disciplinasIds = new List<Guid>() { this._disciplina1.ID, this._disciplina2.ID };

			// Criação de Curso
			var cursoDto = new CursoDto() { Nome = "Ciência da Computação", DisciplinasId = disciplinasIds };
			this._curso = Curso.Criar(cursoDto);

			// Criação de Semestre
			this._semestreDto = new SemestreDto() { DataInicio = DateTime.Now.AddDays(-20), DataFim = DateTime.Now.AddYears(4) };
			this._semestre = Semestre.Criar(_semestreDto);

			this._contextos.SmartContexto.Cursos.Add(_curso);
			this._contextos.SmartContexto.Disciplinas.Add(_disciplina1);
			this._contextos.SmartContexto.Disciplinas.Add(_disciplina2);
			this._contextos.SmartContexto.Disciplinas.Add(_disciplina3);
			this._contextos.SmartContexto.Semestres.Add(_semestre);
			this._contextos.SmartContexto.SaveChangesAsync();			
		}

		[Fact(DisplayName = "Alterar Aluno com Sucesso")]
		public async void DeveAlterarAluno()
		{
			// instancia alteração com novas disciplinas
			var dataNascimentoNova = DateTime.Now.AddYears(-50);
			var dataInicioNova = DateTime.Now.AddDays(-50);
			var dataFimNova = DateTime.Now.AddYears(5);

			var alunoDisciplinaDto = new AlunoDisciplinaDto()
			{
				DisciplinaId = this._disciplina1.ID,
				Periodo = 1,
				SemestreId = this._semestre.ID,
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

			var alunoDtoAlteracao = AlunoDtoBuilder.Novo
				.ComCelular("21912345999")
				.ComCursoId(_curso.ID)
				.ComAtivo(true)
				.ComEndereco("Rua joazeiro norte 459, Rio Comprido")
				.ComAlunosDisciplinas(alunosDisciplinas)
				.ComCidade("São Paulo")
				.ComCpfCnpj("85444471043")
				.ComDataNascimento(dataNascimentoNova)
				.ComDataInicio(dataInicioNova)
				.ComDataFim(dataFimNova)
				.ComEmail("estevao.russo@unicarioca.com.br")
				.ComNome("Estevann")
				.ComSobrenome("Russo")
				.ComTelefone("2131592121")
				.ComId(this._alunoBuilder.ObterAluno().ID).InstanciarCommandAlteracao();

			var retornoAlteracao = await this._mediator.Send(alunoDtoAlteracao);

			retornoAlteracao.Status.Should().Be(Result.Success().Status);
		}		
	}
}
