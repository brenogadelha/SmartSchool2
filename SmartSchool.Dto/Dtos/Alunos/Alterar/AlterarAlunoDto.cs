using System;
using System.Runtime.Serialization;

namespace SmartSchool.Dto.Dtos.Alunos
{
   public class AlterarAlunoDto : AlunoDto
    {
        [IgnoreDataMember]
        public Guid ID { get; set; }
    }
}
