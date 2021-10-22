using SmartSchool.Dto.Semestres;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.Aplicacao.Semestres.Interface
{
	public interface ISemestreServico
	{
		IEnumerable<SemestreDto> Obter();
		void CriarSemestre(SemestreDto semestreDto);
		void AlterarSemestre(Guid idSemestre, AlterarSemestreDto semestreDto);
		SemestreDto ObterPorId(Guid idSemestre);
		void Remover(Guid id);
	}
}
