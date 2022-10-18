using SmartSchool.Dto.Dtos.Professores;
using System;
using System.Collections.Generic;

namespace SmartSchool.Dto.Tccs
{
	public class ObterTccDto
	{
		public Guid Id { get; set; }
		public string Tema { get; set; }
		public string Descricao { get; set; }
		public List<ObterProfessorLightDto> Professores { get; set; }
	}
}
