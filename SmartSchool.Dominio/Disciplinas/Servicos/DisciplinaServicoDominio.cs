using SmartSchool.Comum.Repositorio;
using SmartSchool.Comum.TratamentoErros;
using SmartSchool.Dominio.Disciplinas.Especificacao;
using System;
using System.Threading.Tasks;

namespace SmartSchool.Dominio.Disciplinas.Servicos
{
	public class DisciplinaServicoDominio : IDisciplinaServicoDominio
	{
		private readonly IRepositorio<Disciplina> _disciplinaRepositorio;

		public DisciplinaServicoDominio(IRepositorio<Disciplina> disciplinaRepositorio)
		{
			this._disciplinaRepositorio = disciplinaRepositorio;
		}

		public async Task<Disciplina> ObterAsync(Guid idDisciplina)
		{
			if (idDisciplina.Equals(Guid.Empty))
				throw new ArgumentNullException(null, "Id nulo da Disciplina (não foi informado).");

			var disciplina = await this._disciplinaRepositorio.ObterAsync(new BuscaDeDisciplinaPorIdEspecificacao(idDisciplina).IncluiInformacoesDeProfessor());

			if (disciplina == null)
				throw new RecursoInexistenteException($"Disciplina com ID '{idDisciplina}' não existe.");

			return disciplina;
		}

		public async Task<bool> VerificarExisteDisciplinaComMesmoNome(string nome, Guid? idAtual)
		{
			var cursoComMesmoNome = await this._disciplinaRepositorio.ObterAsync(new BuscaDeDisciplinaPorNomeEspecificacao(nome));
			if (cursoComMesmoNome != null && (!idAtual.HasValue || idAtual.HasValue && cursoComMesmoNome.ID != idAtual))
				return true;

			return false;
		}
	}
}
