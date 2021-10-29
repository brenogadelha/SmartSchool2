using System;

namespace SmartSchool.Dto.Alunos.Obter
{
    public class ObterAlunoDto
    {
		public Guid ID { get; set; }
		public int Matricula { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Telefone { get; set; }
        public bool Ativo { get; set; }
    }
}
