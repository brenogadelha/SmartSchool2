﻿using MediatR;
using SmartSchool.Dominio.Comum.Results;
using System;

namespace SmartSchool.Aplicacao.Cursos.ObterPorId
{
    public class ObterCursoQuery : IRequest<IResult>
    {
        public Guid Id { get; set; }
    }
}
