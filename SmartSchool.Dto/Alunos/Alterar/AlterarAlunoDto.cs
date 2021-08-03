using System;
using System.Runtime.Serialization;

namespace SmartSchool.Dto.Alunos
{
   public class AlterarAlunoDto : AlunoDto
    {
        [IgnoreDataMember]
        public Guid ID { get; set; }
    }
}
