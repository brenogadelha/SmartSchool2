﻿using MediatR;
using SmartSchool.Comum.Mapeador;
using SmartSchool.Comum.Repositorio;
using SmartSchool.Dominio.Comum.Results;
using SmartSchool.Dominio.Disciplinas;
using SmartSchool.Dominio.Disciplinas.Especificacao;
using SmartSchool.Dto.Disciplinas.Obter;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SmartSchool.Aplicacao.Disciplinas.Listar
{
    public class ListarDisciplinasQueryHandler : IRequestHandler<ListarDisciplinasQuery, IResult>
    {
        private readonly IRepositorio<Disciplina> _disciplinaRepositorio;

        public ListarDisciplinasQueryHandler(IRepositorio<Disciplina> disciplinaRepositorio)
        {
            _disciplinaRepositorio = disciplinaRepositorio;
        }

        public async Task<IResult> Handle(ListarDisciplinasQuery request, CancellationToken cancellationToken)
        {
            var cursos = await this._disciplinaRepositorio.Procurar(new BuscaDeDisciplinaPorAtivoEspecificacao().IncluiInformacoesDeProfessor());

            return Result<IEnumerable<ObterDisciplinaDto>>.Success(cursos.MapearParaDto<ObterDisciplinaDto>());
        }
    }
}
