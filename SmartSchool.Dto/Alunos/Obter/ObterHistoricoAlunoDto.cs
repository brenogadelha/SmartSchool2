using System;

namespace SmartSchool.Dto.Alunos.Obter
{
	public class ObterHistoricoAlunoDto
	{
		public Guid SemestreID { get; set; }
		public string NomeDisciplina { get; set; }
		public int Periodo { get; set; }
		public string StatusDisciplinaDescricao { get; set; }
	}
}
