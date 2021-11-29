using SmartSchool.Aplicacao.Professores.Interface;
using SmartSchool.Comum.Mapeador;
using SmartSchool.Comum.Repositorio;
using SmartSchool.Comum.TratamentoErros;
using SmartSchool.Comum.Validacao;
using SmartSchool.Dominio.Disciplinas;
using SmartSchool.Dominio.Professores;
using SmartSchool.Dominio.Professores.Especificacao;
using SmartSchool.Dominio.Professores.Validacao;
using SmartSchool.Dto.Dtos.Professores;
using SmartSchool.Dto.Professores;
using System;
using System.Collections.Generic;

namespace SmartSchool.Aplicacao.Professores.Servico
{
	public class ProfessorServico : IProfessorServico
	{
		private readonly IRepositorio<Professor> _professorRepositorio;
		private readonly IRepositorio<Disciplina> _disciplinaRepositorio;

		public ProfessorServico(IRepositorio<Professor> professorRepositorio, IRepositorio<Disciplina> disciplinaRepositorio)
		{
			this._professorRepositorio = professorRepositorio;
			this._disciplinaRepositorio = disciplinaRepositorio;
		}

		public IEnumerable<ObterProfessorDto> Obter()
		{
			var professor = this._professorRepositorio.Procurar(new BuscaDeProfessorEspecificacao().IncluiInformacoesDeDisciplina());

			return professor.MapearParaDto<ObterProfessorDto>();
		}

		public void CriarProfessor(ProfessorDto professorDto)
		{
			if (professorDto.Disciplinas != null)
			{
				foreach (var disciplina in professorDto.Disciplinas)
					this.ObterDisciplinaDominio(disciplina);
			}

			var professor = Professor.Criar(professorDto);

			this._professorRepositorio.Adicionar(professor);
		}

		public void AlterarProfessor(Guid idProfessor, AlterarProfessorDto professorDto)
		{
			ValidacaoFabrica.Validar(professorDto, new AlterarProfessorValidacao());

			var professor = this.ObterProfessorDominio(idProfessor);

			professor.AlterarNome(professorDto.Nome);
			professor.AlterarMatricula(professorDto.Matricula);

			if (professorDto.Disciplinas != null)
			{
				foreach (var disciplina in professorDto.Disciplinas)
					this.ObterDisciplinaDominio(disciplina);
				professor.AtualizarDisciplinas(professorDto.Disciplinas);
			}

			this._professorRepositorio.Atualizar(professor, true);
		}

		public ObterProfessorDto ObterPorId(Guid idProfessor) => this.ObterProfessorDominio(idProfessor).MapearParaDto<ObterProfessorDto>();

		public void Remover(Guid id)
		{
			var professor = this.ObterProfessorDominio(id);

			this._professorRepositorio.Remover(professor, true);
		}

		private Professor ObterProfessorDominio(Guid idProfessor)
		{
			if (idProfessor.Equals(Guid.Empty))
				throw new ArgumentNullException(null, "Id nulo do Professor (não foi informado).");


			var professor = this._professorRepositorio.Obter(new BuscaDeProfessorPorIdEspecificacao(idProfessor).IncluiInformacoesDeDisciplina());

			if (professor == null)
				throw new RecursoInexistenteException($"Professor com ID '{idProfessor}' não existe.");

			return professor;
		}

		private Disciplina ObterDisciplinaDominio(Guid idDisciplina)
		{
			if (idDisciplina.Equals(Guid.Empty))
				throw new ArgumentNullException(null, "Id nulo da Disciplina (não foi informado).");


			var disciplina = this._disciplinaRepositorio.Obter(new BuscaDeDisciplinaPorIdEspecificacao(idDisciplina));

			if (disciplina == null)
				throw new RecursoInexistenteException($"Disciplina com ID '{idDisciplina}' não existe.");

			return disciplina;
		}
	}
}
