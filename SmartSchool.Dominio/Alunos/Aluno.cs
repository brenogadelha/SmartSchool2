using Newtonsoft.Json;
using SmartSchool.Comum.Dominio;
using SmartSchool.Comum.Validacao;
using SmartSchool.Dominio.Alunos.Validacao;
using SmartSchool.Dominio.Cursos;
using SmartSchool.Dominio.Disciplinas;
using SmartSchool.Dominio.Semestres;
using SmartSchool.Dto.Alunos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace SmartSchool.Dominio.Alunos
{
	public class Aluno : IEntidadeAgregavel<Guid>
	{
		public Guid ID { get; private set; }
		public int Matricula { get; private set; }
		public string Nome { get; private set; }
		public string Sobrenome { get; private set; }
		public string Cpf { get; private set; }
		public string Cidade { get; private set; }
		public string Email { get; private set; }
		public string Celular { get; private set; }
		public string Telefone { get; private set; }
		public string Endereco { get; private set; }
		public DateTime DataNascimento { get; private set; }
		public DateTime DataInicio { get; private set; }
		public DateTime DataFim { get; private set; }
		public bool Ativo { get; private set; }

		public Curso Curso { get; set; }
		public Guid CursoId { get; private set; }

		[JsonIgnore]
		public List<AlunoDisciplina> AlunosDisciplinas { get; private set; } = new List<AlunoDisciplina>();

		[NotMapped]
		public List<Disciplina> Disciplinas
		{
			get => this.AlunosDisciplinas.Select(u => u.Disciplina).ToList();
		}

		//[JsonIgnore]
		public List<SemestreAlunoDisciplina> SemestresDisciplinas { get; private set; } = new List<SemestreAlunoDisciplina>();

		//[JsonIgnore]
		//public List<AlunoSemestre> Semestres { get; private set; } = new List<AlunoSemestre>();

		public Aluno() { }
		public static Aluno Criar(AlunoDto alunoDto)
		{
			ValidacaoFabrica.Validar(alunoDto, new AlunoValidacao());

			var aluno = new Aluno()
			{
				ID = Guid.NewGuid(),
				Nome = alunoDto.Nome,
				Sobrenome = alunoDto.Sobrenome,
				Telefone = alunoDto.Telefone,
				DataInicio = alunoDto.DataInicio,
				DataFim = alunoDto.DataFim,
				Ativo = true,
				DataNascimento = alunoDto.DataNascimento,
				Matricula = alunoDto.Matricula,
				CursoId = alunoDto.CursoId,
				Celular = alunoDto.Celular,
				Cidade = alunoDto.Cidade,
				Cpf = alunoDto.Cpf,
				Email = alunoDto.Email,
				Endereco = alunoDto.Endereco
			};

			if(alunoDto.AlunosDisciplinas != null)
			aluno.AtualizarDisciplinas(alunoDto.AlunosDisciplinas.Select(ad => ad.DisciplinaId).ToList());

			return aluno;
		}

		public void AlterarNome(string nome) => this.Nome = nome;
		public void AlterarSobrenome(string sobrenome) => this.Sobrenome = sobrenome;
		public void AlterarCpf(string cpf) => this.Cpf = cpf;
		public void AlterarEmail(string email) => this.Email = email;
		public void AlterarCelular(string celular) => this.Celular = celular;
		public void AlterarEndereco(string endereco) => this.Endereco = endereco;
		public void AlterarCidade(string cidade) => this.Cidade = cidade;
		public void AlterarTelefone(string telefone) => this.Telefone = telefone;
		public void AlterarAtivo(bool ativo) => this.Ativo = ativo;
		public void AlterarDataNascimento(DateTime dataNascimento) => this.DataNascimento = dataNascimento;
		public void AlterarDataInicio(DateTime dataInicio) => this.DataInicio = dataInicio;
		public void AlterarDataFim(DateTime dataFim) => this.DataFim = dataFim;

		public void AtualizarDisciplinas(List<Guid> novasDisciplinas)
		{
			// Verifica se foram incluídas novas Disciplinas. Caso não, são removidas as atuais.
			if (novasDisciplinas == null || !novasDisciplinas.Any())
			{
				this.AlunosDisciplinas.Clear();
				return;
			}

			// Excluir do Curso as Disciplinas que não estão presentes na nova lista
			if (this.Disciplinas != null && this.Disciplinas.Any())
				for (int i = this.Disciplinas.Count - 1; i > -1; i--)
				{
					if (!novasDisciplinas.Any(idNovo => idNovo == this.Disciplinas[i].ID))
					{
						this.AlunosDisciplinas.Remove(this.AlunosDisciplinas.FirstOrDefault(p => p.DisciplinaID == this.Disciplinas[i].ID));
					}
				}

			List<AlunoDisciplina> listaTemp = new List<AlunoDisciplina>();

			// Adicionar ao Aluno as Disciplinas da lista que são diferentes das atuais
			foreach (Guid id in novasDisciplinas)
				if (!this.Disciplinas.Any(l => l.ID == id))
				{
					listaTemp.Add(AlunoDisciplina.Criar(this.ID, id));
				}

			this.AlunosDisciplinas.AddRange(listaTemp);
		}
	}
}


