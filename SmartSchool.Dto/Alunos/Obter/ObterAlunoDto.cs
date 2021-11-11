using System;

namespace SmartSchool.Dto.Alunos.Obter
{
    public class ObterAlunoDto
    {
		public Guid ID { get; set; }
		public int Matricula { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Endereco { get; set; }
        public DateTime DataNascimento { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public string Telefone { get; set; }
        public bool Ativo { get; set; }
        public string Cpf { get; set; }
        public string Cidade { get; set; }
        public string Email { get; set; }
        public string Celular { get; set; }
        public string Curso { get; set; }
    }
}
