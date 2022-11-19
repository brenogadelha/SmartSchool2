using System;

namespace SmartSchool.Dto.Dtos.Professores
{
    public class ObterProfessorLightDto
    {
        public Guid ID { get; set; }
        public int Matricula { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
	}
}
