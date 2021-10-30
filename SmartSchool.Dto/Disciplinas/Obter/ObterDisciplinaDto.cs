using System;
using System.Collections.Generic;

namespace SmartSchool.Dto.Disciplinas.Obter
{
    public class ObterDisciplinaDto : DisciplinaDto
    {
        public Guid ID { get; set; }
		public List<string> Professores { get; set; }
	}
}
