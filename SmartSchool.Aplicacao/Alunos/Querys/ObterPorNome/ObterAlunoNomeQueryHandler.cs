﻿using MediatR;
using SmartSchool.Comum.Mapeador;
using SmartSchool.Comum.Repositorio;
using SmartSchool.Comum.TratamentoErros;
using SmartSchool.Dominio.Alunos;
using SmartSchool.Dominio.Alunos.Especificacao;
using SmartSchool.Dominio.Comum.Results;
using SmartSchool.Dto.Alunos.Obter;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SmartSchool.Aplicacao.Alunos.ObterPorNome
{
	public class ObterAlunoNomeQueryHandler : IRequestHandler<ObterAlunoNomeQuery, IResult>
	{
		private readonly IRepositorio<Aluno> _alunoRepositorio;

		public ObterAlunoNomeQueryHandler(IRepositorio<Aluno> alunoRepositorio)
		{
			_alunoRepositorio = alunoRepositorio;
		}

		public async Task<IResult> Handle(ObterAlunoNomeQuery request, CancellationToken cancellationToken)
		{
			var alunos = await this._alunoRepositorio.Procurar(new BuscaDeAlunoPorNomeParcialEspecificacao(request.Busca));

			return Result<IEnumerable<ObterAlunoDto>>.Success(alunos.MapearParaDto<ObterAlunoDto>());
		}
	}
}
