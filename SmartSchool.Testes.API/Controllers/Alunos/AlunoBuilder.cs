using SmartSchool.Comum.Dominio.Enums;
using SmartSchool.Dados.Comum;
using SmartSchool.Dados.Contextos;
using SmartSchool.Dominio.Alunos;
using SmartSchool.Dominio.Cursos;
using SmartSchool.Dominio.Disciplinas;
using SmartSchool.Dominio.Semestres;
using SmartSchool.Dto.Alunos;
using SmartSchool.Dto.Curso;
using SmartSchool.Dto.Disciplinas;
using SmartSchool.Dto.Semestres;
using SmartSchool.Testes.Compartilhado.Builders;
using SmartSchool.Testes.Integracao;
using System;
using System.Collections.Generic;

namespace SmartSchool.Testes.API.Controllers.Alunos
{
	public class AlunoBuilder : BaseMediatorServiceProvider
	{
		private readonly IUnidadeDeTrabalho _contextos;
		private readonly AlunoDtoBuilder _alunoDtoBuilder;

		private readonly Aluno _aluno;

		private readonly DisciplinaDto _disciplinaDto1;
		private readonly DisciplinaDto _disciplinaDto2;
		private readonly DisciplinaDto _disciplinaDto3;

		private readonly Disciplina _disciplina1;
		private readonly Disciplina _disciplina2;
		private readonly Disciplina _disciplina3;

		private readonly SemestreDto _semestreDto;
		private readonly Semestre _semestre;

		private readonly Curso _curso;

		public AlunoBuilder()
		{
			this._contextos = ContextoFactory.Criar();

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
			this._contextos.SmartContexto.SaveChangesAsync();
		}

		public Aluno ObterAluno() => this._aluno;
	}
}
