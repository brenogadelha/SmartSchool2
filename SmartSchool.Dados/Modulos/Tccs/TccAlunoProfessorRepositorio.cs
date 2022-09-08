using Microsoft.EntityFrameworkCore;
using SmartSchool.Comum.Especificao;
using SmartSchool.Comum.Repositorio;
using SmartSchool.Dados.Comum;
using SmartSchool.Dominio.Tccs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartSchool.Dados.Modulos.Tccs
{
	public class TccAlunoProfessorRepositorio : IRepositorio<TccAlunoProfessor>
	{
		private readonly IUnidadeDeTrabalho _contexto;
		public TccAlunoProfessorRepositorio(IUnidadeDeTrabalho contextos) => this._contexto = contextos;

		public virtual async Task Adicionar(TccAlunoProfessor entidade, bool finalizarTransacao = true)
		{
			await this._contexto.SmartContexto.TccAlunosProfessores.AddAsync(entidade);

			if (finalizarTransacao)
				await this._contexto.SmartContexto.SaveChangesAsync();
		}

		public virtual async Task Atualizar(TccAlunoProfessor entidade, bool finalizarTransacao = true)
		{
			this._contexto.SmartContexto.TccAlunosProfessores.Update(entidade);

			if (finalizarTransacao)
				await this._contexto.SmartContexto.SaveChangesAsync();
		}

		public virtual async Task<IEnumerable<TccAlunoProfessor>> Obter() => await this._contexto.SmartContexto.TccAlunosProfessores.ToListAsync();

		public virtual async Task<TccAlunoProfessor> ObterAsync(IEspecificavel<TccAlunoProfessor> especificacao)
		{
			return await this._contexto.SmartContexto.GetDbSetWithQueryable(especificacao).FirstOrDefaultAsync();

			//var query = GetDbSetWithQueryable(especificacao);

			//return await (usarTracking ? query.FirstOrDefaultAsync(especificacao.ExpressaoEspecificacao) : query.AsNoTracking().FirstOrDefaultAsync(especificacao.ExpressaoEspecificacao));
		}

		public virtual async Task<IEnumerable<TccAlunoProfessor>> Procurar(IEspecificavel<TccAlunoProfessor> especificacao) =>
			await this._contexto.SmartContexto.GetDbSetWithQueryable(especificacao).ToListAsync();

		public virtual async Task RemoverAsync(TccAlunoProfessor entidade, bool finalizarTransacao = true)
		{
			await Task.FromResult(this._contexto.SmartContexto.TccAlunosProfessores.Remove(entidade));

			if (finalizarTransacao)
				this._contexto.SmartContexto.SaveChanges();
		}
	}
}
