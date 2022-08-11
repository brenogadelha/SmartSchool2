﻿using SmartSchool.Dominio.Alunos;
using System;
using System.Threading.Tasks;

namespace SmartSchool.Dominio.Alunos.Servicos
{
	public interface IAlunoServicoDominio
	{
		Task<Aluno> ObterPorIdAsync(Guid idAtividade);
		Task<Aluno> ObterPorMatriculaAsync(int matricula);
		Task<bool> VerificarExisteAlunoComMesmoCpfCnpj(string cpfCnpj, Guid? idAtual);
		Task<bool> VerificarExisteAlunoComMesmoEmail(string email, Guid? idAtual);
	}
}