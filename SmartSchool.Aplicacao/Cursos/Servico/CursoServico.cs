using SmartSchool.Aplicacao.Cursos.Interface;
using SmartSchool.Comum.Mapeador;
using SmartSchool.Comum.Repositorio;
using SmartSchool.Comum.TratamentoErros;
using SmartSchool.Dominio.Cursos;
using SmartSchool.Dominio.Cursos.Especificacao;
using SmartSchool.Dominio.Disciplinas;
using SmartSchool.Dominio.Professores.Especificacao;
using SmartSchool.Dto.Curso;
using System;
using System.Collections.Generic;

namespace SmartSchool.Aplicacao.Cursos.Servico
{
	public class CursoServico : ICursoServico
	{
		private readonly IRepositorio<Curso> _cursoRepositorio;
		private readonly IRepositorio<Disciplina> _disciplinaRepositorio;
		public CursoServico(IRepositorio<Curso> cursoRepositorio, IRepositorio<Disciplina> disciplinaRepositorio)
		{
			this._cursoRepositorio = cursoRepositorio;
			this._disciplinaRepositorio = disciplinaRepositorio;
		}

		public IEnumerable<CursoDto> Obter()
		{
			var cursos = this._cursoRepositorio.Obter();

			return cursos.MapearParaDto<CursoDto>();
		}

		public void CriarCurso(CursoDto cursoDto)
		{
			foreach (var disciplinaId in cursoDto.DisciplinasId)
				this.ObterDisciplinaDominio(disciplinaId);

			var curso = Curso.Criar(cursoDto);

			this._cursoRepositorio.Adicionar(curso);
		}

		public void AlterarCurso(Guid idCurso, AlterarCursoDto cursoDto)
		{
			var curso = this.ObterCursoDominio(idCurso);

			foreach (var disciplinaId in cursoDto.DisciplinasId)
				this.ObterDisciplinaDominio(disciplinaId);

			curso.AlterarNome(cursoDto.Nome);
			curso.AtualizarDisciplinas(cursoDto.DisciplinasId);

			this._cursoRepositorio.Atualizar(curso, true);
		}

		public CursoDto ObterPorId(Guid idCurso) => this.ObterCursoDominio(idCurso).MapearParaDto<CursoDto>();

		public void Remover(Guid id)
		{
			var cursoExistente = this.ObterCursoDominio(id);

			this._cursoRepositorio.Remover(cursoExistente, true);
		}

		private Curso ObterCursoDominio(Guid idCurso)
		{
			if (idCurso.Equals(Guid.Empty))
				throw new ArgumentNullException(null, "Id nulo do Curso (não foi informado).");


			var curso = this._cursoRepositorio.Obter(new BuscaDeCursoPorIdEspecificacao(idCurso));

			if (curso == null)
				throw new RecursoInexistenteException($"Curso com ID '{idCurso}' não existe.");

			return curso;
		}

		private Disciplina ObterDisciplinaDominio(Guid idDisciplina)
		{
			if (idDisciplina.Equals(Guid.Empty))
				throw new ArgumentNullException(null, "Id nulo da Disciplina (não foi informado).");


			var disciplina = this._disciplinaRepositorio.Obter(new BuscaDeDisciplinaPorIdEspecificacao(idDisciplina));

			if (disciplina == null)
				throw new RecursoInexistenteException($"Disciplina com ID '{idDisciplina}' não existe.");

			return disciplina;
		}
	}
}
