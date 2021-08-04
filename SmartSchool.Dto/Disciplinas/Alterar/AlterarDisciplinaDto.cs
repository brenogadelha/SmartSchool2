using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace SmartSchool.Dto.Disciplinas.Alterar
{
   public class AlterarDisciplinaDto : DisciplinaDto
    {
        [IgnoreDataMember]
        public Guid ID { get; set; }
    }
}
