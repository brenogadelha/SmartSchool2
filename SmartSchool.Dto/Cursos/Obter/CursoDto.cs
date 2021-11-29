using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.Dto.Curso
{
	public class ObterCursoDto
	{
		public Guid ID { get; set; }
		public List<string> Disciplinas { get; set; }
		public string Nome { get; set; }
	}
}
