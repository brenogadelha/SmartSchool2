using SmartSchool.Dto.Curso;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.Aplicacao.Cursos.Interface
{
	public interface ICursoServico
	{
		IEnumerable<ObterCursoDto> Obter();
		void CriarCurso(CursoDto cursoDto);
		void AlterarCurso(Guid idCurso, AlterarCursoDto cursoDto, bool? atualizarDisciplinas = null);
		ObterCursoDto ObterPorId(Guid idCurso);
		void Remover(Guid id);
	}
}
