﻿using SmartSchool.Aplicacao.Semestres.Interface;
using SmartSchool.Comum.Mapeador;
using SmartSchool.Comum.Repositorio;
using SmartSchool.Comum.TratamentoErros;
using SmartSchool.Dominio.Semestres;
using SmartSchool.Dominio.Semestres.Especificacao;
using SmartSchool.Dto.Semestres;
using System;
using System.Collections.Generic;

namespace SmartSchool.Aplicacao.Semestres.Servico
{
	public class SemestreServico : ISemestreServico
	{
		private readonly IRepositorio<Semestre> _semestreRepositorio;
		public SemestreServico(IRepositorio<Semestre> semestreRepositorio)
		{
			this._semestreRepositorio = semestreRepositorio;
		}

		public IEnumerable<SemestreDto> Obter()
		{
			var semestres = this._semestreRepositorio.Obter();

			return semestres.MapearParaDto<SemestreDto>();
		}

		public void CriarSemestre(SemestreDto semestreDto)
		{
			var semestre = Semestre.Criar(semestreDto);

			this._semestreRepositorio.Adicionar(semestre);
		}

		public void AlterarSemestre(Guid idSemestre, AlterarSemestreDto semestreDto)
		{
			var semestre = this.ObterSemestreDominio(idSemestre);

			semestre.AlterarDataInicio(semestreDto.DataInicio);
			semestre.AlterarDataFim(semestreDto.DataFim);

			this._semestreRepositorio.Atualizar(semestre, true);
		}

		public SemestreDto ObterPorId(Guid idSemestre) => this.ObterSemestreDominio(idSemestre).MapearParaDto<SemestreDto>();

		public void Remover(Guid id)
		{
			var semestre = this.ObterSemestreDominio(id);

			this._semestreRepositorio.Remover(semestre);
		}

		private Semestre ObterSemestreDominio(Guid idSemestre)
		{
			if (idSemestre.Equals(Guid.Empty))
				throw new ArgumentNullException(null, "Id nulo do Semestre (não foi informado).");


			var semestre = this._semestreRepositorio.Obter(new BuscaDeSemestrePorIdEspecificacao(idSemestre));

			if (semestre == null)
				throw new RecursoInexistenteException($"Semestre com ID '{idSemestre}' não existe.");

			return semestre;
		}
	}
}
