using SmartSchool.Dominio.Cursos;
using System;

namespace SmartSchool.Dominio.Disciplinas
{
	public class CursoDisciplina
	{
		private CursoDisciplina() { }

		public Guid CursoID { get; private set; }
		public Curso Curso { get; private set; }

		public Guid DisciplinaID { get; private set; }
		public Disciplina Disciplina { get; private set; }

		public static CursoDisciplina Criar(Guid cursoId, Guid disciplinaId) => new CursoDisciplina()
		{
			CursoID = cursoId,
			DisciplinaID = disciplinaId
		};
	}
}
