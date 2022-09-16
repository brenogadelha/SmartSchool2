using SmartSchool.Comum.Dominio;
using SmartSchool.Comum.Validacao;
using SmartSchool.Dominio.Comum.Results;
using SmartSchool.Dominio.Professores;
using SmartSchool.Dominio.Tccs.Validacao;
using SmartSchool.Dto.Tccs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;

namespace SmartSchool.Dominio.Tccs
{
	public class Tcc : IEntidadeAgregavel<Guid>
	{
		public Guid ID { get; private set; }
		public string Tema { get; set; }
		public string Descricao { get; set; }
		public bool Ativo { get; set; }

		[JsonIgnore]
		public List<TccProfessor> TccProfessores { get; set; } = new List<TccProfessor>();

		[NotMapped]
		public List<Guid> ProfessoresIds
		{
			get => this.TccProfessores.Select(u => u.ProfessorID).ToList();
		}

		[NotMapped]
		public List<Professor> Professores
		{
			get => this.TccProfessores.Select(u => u.Professor).ToList();
		}

		public static Tcc Criar(TccDto dto)
		{
			//ValidacaoFabrica.Validar(dto, new SemestreValidacao());

			var tcc = new Tcc()
			{
				ID = Guid.NewGuid(),
				Tema = dto.Tema,
				Descricao = dto.Descricao,
				Ativo = true
			};

			tcc.AtualizarProfessores(dto.Professores);

			ValidacaoFabrica.Validar(tcc, new TccValidacao());

			return tcc;
		}

		public static Result<Tcc> Criar(string tema, string descricao, List<Guid> professores)
		{

			var tcc = new Tcc()
			{
				ID = Guid.NewGuid(),
				Tema = tema,
				Descricao = descricao,
				Ativo = true
			};

			tcc.AtualizarProfessores(professores);

			ValidacaoFabrica.Validar(tcc, new TccValidacao());

			return Result<Tcc>.Success(tcc);
		}

		public void AlterarAtivo(bool ativo) => this.Ativo = ativo;
		public void AlterarTema(string tema) => this.Tema = tema;
		public void AlterarDescricao(string descricao) => this.Descricao = descricao;

		public void AtualizarProfessores(List<Guid> novosProfessores)
		{
			// Verifica se foram incluídos novos Professores. Caso não, são removidos os atuais.
			if (novosProfessores == null || !novosProfessores.Any())
			{
				this.TccProfessores.Clear();
				return;
			}

			// Excluir do Tcc os Professores que não estão presentes na nova lista
			if (this.ProfessoresIds != null && this.ProfessoresIds.Any())
				for (int i = this.ProfessoresIds.Count - 1; i > -1; i--)
				{
					if (!novosProfessores.Any(idNovo => idNovo == this.ProfessoresIds[i]))
					{
						this.TccProfessores.Remove(this.TccProfessores.FirstOrDefault(p => p.ProfessorID == this.ProfessoresIds[i]));
					}
				}

			List<TccProfessor> listaTemp = new List<TccProfessor>();

			// Adicionar ao Tcc os Professores da lista que são diferentes das atuais
			foreach (Guid id in novosProfessores)
				if (!this.ProfessoresIds.Any(l => l == id))
				{
					listaTemp.Add(TccProfessor.Criar(id, this.ID));
				}

			this.TccProfessores.AddRange(listaTemp);
		}
	}
}


