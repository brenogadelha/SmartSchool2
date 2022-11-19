﻿using SmartSchool.Comum.Repositorio;
using SmartSchool.Comum.TratamentoErros;
using SmartSchool.Dominio.Alunos.Especificacao;
using System;
using System.Threading.Tasks;

namespace SmartSchool.Dominio.Alunos.Servicos
{
	public class AlunoServicoDominio : IAlunoServicoDominio
	{
		private readonly IRepositorio<Aluno> _alunoRepositorio;

		public AlunoServicoDominio(IRepositorio<Aluno> alunoRepositorio)
		{
			this._alunoRepositorio = alunoRepositorio;
		}

		public async Task<Aluno> ObterPorIdAsync(Guid idAluno)
		{
			if (idAluno.Equals(Guid.Empty))
				throw new ArgumentNullException(null, "Id nulo do Aluno (não foi informado).");

			var aluno = await this._alunoRepositorio.ObterAsync(new BuscaDeAlunoPorIdEspecificacao(idAluno).IncluiInformacoesDeHistorico().IncluiInformacoesDeDisciplina().IncluiInformacoesDeCurso());

			if (aluno == null)
				throw new RecursoInexistenteException($"Aluno com ID '{idAluno}' não existe.");

			return aluno;
		}

		public async Task<Aluno> ObterPorMatriculaAsync(int matricula)
		{
			if (matricula < 0)
				throw new ArgumentNullException(null, "Matricula do Aluno (não foi informado).");

			var aluno = await this._alunoRepositorio.ObterAsync(new BuscaDeAlunoPorMatriculaEspecificacao(matricula).IncluiInformacoesDeHistorico().IncluiInformacoesDeDisciplina().IncluiInformacoesDeCurso());

			if (aluno == null)
				throw new RecursoInexistenteException($"Aluno com matrícula '{matricula}' não existe.");

			return aluno;
		}

		public async Task<bool> VerificarExisteAlunoComMesmoCpfCnpj(string cpfCnpj, Guid? idAtual)
		{
			var alunoComMesmoCpfCnpj = await this._alunoRepositorio.ObterAsync(new BuscaDeAlunoPorCpfCnpjEspecificacao(cpfCnpj));
			if (alunoComMesmoCpfCnpj != null && (!idAtual.HasValue || idAtual.HasValue && alunoComMesmoCpfCnpj.ID != idAtual))
				return true;

			return false;
		}

		public async Task<bool> VerificarExisteAlunoComMesmoEmail(string email, Guid? idAtual)
		{
			var alunoComMesmoEmail = await this._alunoRepositorio.ObterAsync(new BuscaDeAlunoPorEmailEspecificacao(email));
			if (alunoComMesmoEmail != null && (!idAtual.HasValue || idAtual.HasValue && alunoComMesmoEmail.ID != idAtual))
				return true;

			return false;
		}

		public async Task<bool> VerificarExisteAlunoComMesmaMatricula(int matricula, Guid? idAtual)
		{
			var alunoComMesmaMatricula = await this._alunoRepositorio.ObterAsync(new BuscaDeAlunoPorMatriculaEspecificacao(matricula));
			if (alunoComMesmaMatricula != null && (!idAtual.HasValue || idAtual.HasValue && alunoComMesmaMatricula.ID != idAtual))
				return true;

			return false;
		}
	}
}
