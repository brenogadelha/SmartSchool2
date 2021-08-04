using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace SmartSchool.Dto.Dtos.Professores
{
    public class AlterarProfessorDto
    {
        [IgnoreDataMember]
        public Guid ID { get; set; }
        public int Matricula { get; set; }
        public string Nome { get; set; }
    }
}
