using SmartSchool.Dto.Dtos.Professores;
using System;
using System.Collections.Generic;

namespace SmartSchool.Dto.Tccs
{
	public class ObterTccDto : TccDto
	{
		public Guid Id { get; set; }
		public List<ObterProfessorLightDto> professores { get; set; }
	}
}
