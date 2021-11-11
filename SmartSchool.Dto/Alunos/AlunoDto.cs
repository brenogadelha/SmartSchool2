using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.Dto.Alunos
{
	public class AlunoDto
	{
		public int Matricula { get; set; }
		public string Nome { get; set; }
		public string Sobrenome { get; set; }
		public string Telefone { get; set; }
		public string Endereco { get; set; }
		public string Cpf { get; set; }
		public string Cidade { get; set; }
		public string Email { get; set; }
		public string Celular { get; set; }
		public DateTime DataNascimento { get; set; }
		public DateTime DataInicio { get; set; }
		public DateTime DataFim { get; set; }
		public bool Ativo { get; set; }
		public List<AlunoDisciplinaDto> AlunosDisciplinas { get; set; } = new List<AlunoDisciplinaDto>();
		public Guid CursoId { get; set; }
	}
}
