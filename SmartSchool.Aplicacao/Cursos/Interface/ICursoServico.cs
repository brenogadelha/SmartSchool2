using SmartSchool.Dto.Curso;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.Aplicacao.Cursos.Interface
{
	public interface ICursoServico
	{
		IEnumerable<CursoDto> Obter();
		void CriarCurso(CursoDto cursoDto);
		void AlterarCurso(Guid idCurso, AlterarCursoDto cursoDto);
		CursoDto ObterPorId(Guid idCurso);
		void Remover(Guid id);
	}
}
