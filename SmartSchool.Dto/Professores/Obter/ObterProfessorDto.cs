using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.Dto.Dtos.Professores
{
    public class ObterProfessorDto
    {
        public Guid ID { get; set; }
        public int Matricula { get; set; }
        public string Nome { get; set; }
    }
}
