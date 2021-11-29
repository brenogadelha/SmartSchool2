using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace SmartSchool.Dto.Professores
{
    public class ProfessorDto
    {
        public int Matricula { get; set; }
        public string Nome { get; set; }
        public List<Guid> Disciplinas { get; set; }
        [IgnoreDataMember]
        public Guid ID { get; set; }
    }
}
