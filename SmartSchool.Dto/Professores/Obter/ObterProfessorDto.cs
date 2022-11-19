using System;
using System.Collections.Generic;

namespace SmartSchool.Dto.Dtos.Professores
{
    public class ObterProfessorDto
    {
        public Guid ID { get; set; }
        public int Matricula { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
		public List<string> Disciplinas { get; set; }
	}
}
