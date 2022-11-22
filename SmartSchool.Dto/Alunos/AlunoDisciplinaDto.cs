using SmartSchool.Comum.Dominio.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.Dto.Alunos
{
	public class AlunoDisciplinaDto
	{
		public int Periodo { get; set; }
		public Guid DisciplinaId { get; set; }
		public Guid SemestreId { get; set; }
		public StatusDisciplina StatusDisciplina { get; set; }
	}
}
