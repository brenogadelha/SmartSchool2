﻿using SmartSchool.Comum.Repositorio;
using SmartSchool.Comum.TratamentoErros;
using SmartSchool.Dominio.Cursos.Especificacao;
using System;
using System.Threading.Tasks;

namespace SmartSchool.Dominio.Cursos.Servicos
{
	public class CursoServicoDominio : ICursoServicoDominio
	{
		private readonly IRepositorio<Curso> _cursoRepositorio;

		public CursoServicoDominio(IRepositorio<Curso> alunoRepositorio)
		{
			this._cursoRepositorio = alunoRepositorio;
		}

		public async Task<Curso> ObterAsync(Guid idCurso)
		{
			if (idCurso.Equals(Guid.Empty))
				throw new ArgumentNullException(null, "Id nulo do Curso (não foi informado).");

			var curso = await this._cursoRepositorio.ObterAsync(new BuscaDeCursoPorIdEspecificacao(idCurso).IncluiInformacoesDeDisciplina());

			if (curso == null)
				throw new RecursoInexistenteException($"Curso com ID '{idCurso}' não existe.");

			return curso;
		}

		public async Task<bool> VerificarExisteCursoComMesmoNome(string nome, Guid? idAtual)
		{
			var cursoComMesmoNome = await this._cursoRepositorio.ObterAsync(new BuscaDeCursoPorNomeEspecificacao(nome));
			if (cursoComMesmoNome != null && (!idAtual.HasValue || idAtual.HasValue && cursoComMesmoNome.ID != idAtual))
				return true;

			return false;
		}
	}
}
