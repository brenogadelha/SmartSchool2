using SmartSchool.Aplicacao.Cursos.Interface;
using SmartSchool.Comum.Mapeador;
using SmartSchool.Comum.Repositorio;
using SmartSchool.Comum.TratamentoErros;
using SmartSchool.Comum.Validacao;
using SmartSchool.Dominio.Cursos;
using SmartSchool.Dominio.Cursos.Especificacao;
using SmartSchool.Dominio.Cursos.Validacao;
using SmartSchool.Dominio.Disciplinas;
using SmartSchool.Dominio.Disciplinas.Especificacao;
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

		public IEnumerable<ObterCursoDto> Obter()
		{
			var disciplina = this._cursoRepositorio.Procurar(new BuscaDeCursoEspecificacao().IncluiInformacoesDeDisciplina());

			return disciplina.MapearParaDto<ObterCursoDto>();
		}

		public void CriarCurso(CursoDto cursoDto)
		{
			this.VerificarExisteCursoComMesmoNome(cursoDto.Nome, null);

			foreach (var disciplinaId in cursoDto.DisciplinasId)
				this.ObterDisciplinaDominio(disciplinaId);

			var curso = Curso.Criar(cursoDto);

			this._cursoRepositorio.Adicionar(curso);
		}

		public void AlterarCurso(Guid idCurso, AlterarCursoDto cursoDto, bool? atualizarDisciplinas = null)
		{
			this.VerificarExisteCursoComMesmoNome(cursoDto.Nome, cursoDto.ID);

			var curso = this.ObterCursoDominio(idCurso);

			if (atualizarDisciplinas.HasValue)
			{
				curso.AtualizarDisciplinas(cursoDto.DisciplinasId);
				this._cursoRepositorio.Atualizar(curso, true);

				return;
			}

			ValidacaoFabrica.Validar(cursoDto, new CursoValidacao());					

			foreach (var disciplinaId in cursoDto.DisciplinasId)
				this.ObterDisciplinaDominio(disciplinaId);

			curso.AlterarNome(cursoDto.Nome);
			curso.AtualizarDisciplinas(cursoDto.DisciplinasId);

			this._cursoRepositorio.Atualizar(curso, true);
		}

		public ObterCursoDto ObterPorId(Guid idCurso) => this.ObterCursoDominio(idCurso).MapearParaDto<ObterCursoDto>();

		public void Remover(Guid id)
		{
			var cursoExistente = this.ObterCursoDominio(id);

			this._cursoRepositorio.Remover(cursoExistente, true);
		}

		private Curso ObterCursoDominio(Guid idCurso)
		{
			if (idCurso.Equals(Guid.Empty))
				throw new ArgumentNullException(null, "Id nulo do Curso (não foi informado).");


			var curso = this._cursoRepositorio.Obter(new BuscaDeCursoPorIdEspecificacao(idCurso).IncluiInformacoesDeDisciplina());

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

		private void VerificarExisteCursoComMesmoNome(string nome, Guid? idAtual)
		{
			var alunoComMesmoNome = this._cursoRepositorio.Obter(new BuscaDeCursoPorNomeEspecificacao(nome));
			if (alunoComMesmoNome != null && (!idAtual.HasValue || idAtual.HasValue && alunoComMesmoNome.ID != idAtual))
				throw new ErroNegocioException($"Já existe um Curso com o mesmo nome '{nome}'.");
		}
	}
}
