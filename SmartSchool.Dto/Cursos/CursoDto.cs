using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.Dto.Curso
{
	public class CursoDto
	{
		public string Nome { get; set; }
		public List<Guid> DisciplinasId { get; set; }
	}
}
