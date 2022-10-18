using SmartSchool.Dto.Professores;
using System;
using System.Runtime.Serialization;

namespace SmartSchool.Dto.Dtos.Professores
{
    public class AlterarProfessorDto : ProfessorDto
    {
        [IgnoreDataMember]
        public Guid ID { get; set; }
    }
}
