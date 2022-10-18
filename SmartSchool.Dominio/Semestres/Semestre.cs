using SmartSchool.Comum.Dominio;
using SmartSchool.Comum.Validacao;
using SmartSchool.Dominio.Comum.Results;
using SmartSchool.Dominio.Semestres.Validacao;
using SmartSchool.Dto.Semestres;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SmartSchool.Dominio.Semestres
{
	public class Semestre : IEntidade
	{
		public Guid ID { get; private set; }
		public DateTime DataInicio { get; private set; }
		public DateTime DataFim { get; private set; }
		public bool Ativo { get; set; }

		[JsonIgnore]
		public List<SemestreAlunoDisciplina> AlunosDisciplinas { get; private set; } = new List<SemestreAlunoDisciplina>();

		public static Semestre Criar(SemestreDto dto) => Criar(dto.DataInicio, dto.DataFim);

		public static Result<Semestre> Criar(DateTime dataInicio, DateTime dataFim)
		{
			var semestre = new Semestre()
			{
				ID = Guid.NewGuid(),
				DataInicio = dataInicio,
				DataFim = dataFim,
				Ativo = true
			};

			ValidacaoFabrica.Validar(semestre, new SemestreValidacao());

			return Result<Semestre>.Success(semestre);
		}

		public void AlterarDataInicio(DateTime dataInicio) => this.DataInicio = dataInicio;
		public void AlterarDataFim(DateTime dataFim) => this.DataFim = dataFim;
		public void AlterarAtivo(bool ativo) => this.Ativo = ativo;
	}
}
