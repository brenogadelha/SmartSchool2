using SmartSchool.Comum.Dominio;
using SmartSchool.Comum.Validacao;
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

		[JsonIgnore]
		public List<SemestreAlunoDisciplina> AlunosDisciplinas { get; private set; } = new List<SemestreAlunoDisciplina>();

		public static Semestre Criar(SemestreDto dto)
		{
			ValidacaoFabrica.Validar(dto, new SemestreValidacao());

			var semestre = new Semestre()
			{
				ID = Guid.NewGuid(),
				DataInicio = dto.DataInicio,
				DataFim = dto.DataFim
			};

			return semestre;
		}

		public void AlterarDataInicio(DateTime dataInicio) => this.DataInicio = dataInicio;
		public void AlterarDataFim(DateTime dataFim) => this.DataFim = dataFim;
	}
}
