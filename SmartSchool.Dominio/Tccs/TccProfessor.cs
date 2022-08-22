using SmartSchool.Comum.Dominio;
using SmartSchool.Dominio.Professores;
using SmartSchool.Dominio.Semestres;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SmartSchool.Dominio.Tccs
{
    public class TccProfessor : IEntidade
    {
        private TccProfessor() { }

        public Guid ProfessorID { get; private set; }
        public Professor Professor { get; private set; }

        public Guid TccID { get; private set; }
        public Tcc Tcc { get; private set; }

		[JsonIgnore]
		public List<TccAlunoProfessor> Alunos { get; private set; } = new List<TccAlunoProfessor>();

		public static TccProfessor Criar(Guid professorId, Guid tccId) => new TccProfessor()
        {
            ProfessorID = professorId,
            TccID = tccId
        };
    }
}
