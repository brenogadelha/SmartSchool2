using SmartSchool.Dto.Semestres;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.Aplicacao.Semestres.Interface
{
	public interface ISemestreServico
	{
		IEnumerable<AlterarObterSemestreDto> Obter();
		void CriarSemestre(SemestreDto semestreDto);
		void AlterarSemestre(Guid idSemestre, AlterarObterSemestreDto semestreDto);
		AlterarObterSemestreDto ObterPorId(Guid idSemestre);
		void Remover(Guid id);
	}
}
