using SmartSchool.Comum.Dominio;
using SmartSchool.Dominio.Disciplinas;
using SmartSchool.Dominio.Semestres;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SmartSchool.Dominio.Alunos
{
	public class AlunoDisciplina : IEntidade
	{
		private AlunoDisciplina() { }

		public Guid AlunoID { get; private set; }
		public Aluno Aluno { get; private set; }

		public Guid DisciplinaID { get; private set; }
		public Disciplina Disciplina { get; private set; }

		[JsonIgnore]
		public List<SemestreAlunoDisciplina> Semestres { get; private set; } = new List<SemestreAlunoDisciplina>();

		public static AlunoDisciplina Criar(Guid alunoId, Guid disciplinaId) => new AlunoDisciplina()
		{
			AlunoID = alunoId,
			DisciplinaID = disciplinaId
		};
	}
}
