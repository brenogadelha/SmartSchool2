//using SmartSchool.Dominio.Alunos;
//using SmartSchool.Dominio.Semestres;
//using System;
//using System.Collections.Generic;
//using System.Text.Json.Serialization;

//namespace SmartSchool.Dominio.Alunos
//{
//    public class AlunoSemestre
//    {
//        private AlunoSemestre() { }

//        public Guid AlunoID { get; private set; }
//        public Aluno Aluno { get; private set; }

//        public Guid SemestreID { get; private set; }
//        public Semestre Semestre { get; private set; }

//        public static AlunoSemestre Criar(Guid alunoId, Guid semestreId) => new AlunoSemestre()
//        {
//            AlunoID = alunoId,
//            SemestreID = semestreId
//        };
//    }
//}
