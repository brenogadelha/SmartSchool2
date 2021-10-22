using SmartSchool.Comum.Dominio;
using SmartSchool.Comum.Dominio.Enums;
using SmartSchool.Dominio.Alunos;
using SmartSchool.Dominio.Disciplinas;
using System;

namespace SmartSchool.Dominio.Semestres
{
	public class SemestreAlunoDisciplina : IEntidade
	{
		private SemestreAlunoDisciplina() { }

		public int Periodo { get; set; }
		public Guid SemestreID { get; private set; }
		public Semestre Semestre { get; private set; }
		public Guid DisciplinaID { get; private set; }
		public Guid AlunoID { get; private set; }
		public AlunoDisciplina AlunoDisciplina { get; private set; }
		public StatusDisciplinaEnum StatusDisciplina { get; private set; }

		public static SemestreAlunoDisciplina Criar(int periodo, Semestre semestre, AlunoDisciplina alunoDisciplina, StatusDisciplinaEnum statusDisciplina)
		{
			var semestreDisciplina = new SemestreAlunoDisciplina { AlunoDisciplina = alunoDisciplina, Semestre = semestre };
			semestreDisciplina.SemestreID = semestre.ID;
			semestreDisciplina.DisciplinaID = alunoDisciplina.DisciplinaID;
			semestreDisciplina.AlunoID = alunoDisciplina.AlunoID;
			semestreDisciplina.StatusDisciplina = statusDisciplina;
			semestreDisciplina.Periodo = periodo;

			return semestreDisciplina;
		}

		public static SemestreAlunoDisciplina Criar(int periodo, Guid idSemestre, Guid idDisciplina, Guid idAluno, StatusDisciplinaEnum statusDisciplina) => new SemestreAlunoDisciplina() { SemestreID = idSemestre, DisciplinaID = idDisciplina, AlunoID = idAluno, Periodo = periodo, StatusDisciplina = statusDisciplina };
	}

}
