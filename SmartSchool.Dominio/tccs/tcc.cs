using SmartSchool.Comum.Dominio;
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
		public string Objetivo { get; set; }
		public string Problematica { get; set; }

		[JsonIgnore]
		public List<TccProfessor> Professores { get; set; } = new List<TccProfessor>();

		[NotMapped]
		public List<Guid> ProfessoresIds
		{
			get => this.Professores.Select(u => u.ProfessorID).ToList();
		}

		public static Tcc Criar(TccDto dto)
		{
			//ValidacaoFabrica.Validar(dto, new SemestreValidacao());

			var tcc = new Tcc()
			{
				ID = Guid.NewGuid(),
				Descricao = dto.Descricao,
				Objetivo = dto.Objetivo,
				Problematica = dto.Problematica				
			};

			tcc.AtualizarProfessores(dto.Professores);

			return tcc;
		}

		public void AtualizarProfessores(List<Guid> novosProfessores)
		{
			// Verifica se foram incluídos novos Professores. Caso não, são removidos os atuais.
			if (novosProfessores == null || !novosProfessores.Any())
			{
				this.Professores.Clear();
				return;
			}

			// Excluir do Tcc os Professores que não estão presentes na nova lista
			if (this.ProfessoresIds != null && this.ProfessoresIds.Any())
				for (int i = this.ProfessoresIds.Count - 1; i > -1; i--)
				{
					if (!novosProfessores.Any(idNovo => idNovo == this.ProfessoresIds[i]))
					{
						this.Professores.Remove(this.Professores.FirstOrDefault(p => p.ProfessorID == this.ProfessoresIds[i]));
					}
				}

			List<TccProfessor> listaTemp = new List<TccProfessor>();

			// Adicionar ao Tcc os Professores da lista que são diferentes das atuais
			foreach (Guid id in novosProfessores)
				if (!this.ProfessoresIds.Any(l => l == id))
				{
					listaTemp.Add(TccProfessor.Criar(this.ID, id));
				}

			this.Professores.AddRange(listaTemp);
		}
		// Fazer métodos GET para tcc's com status "SOLICITADO".
		// Se negado, informar o motivo para o aluno.
	}
}


