using Microsoft.EntityFrameworkCore;
using SmartSchool.Comum.Especificao;
using SmartSchool.Comum.Repositorio;
using SmartSchool.Dados.Comum;
using SmartSchool.Dominio.Tccs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartSchool.Dados.Modulos.Tccs
{
	public class TccRepositorio : IRepositorio<Tcc>
	{
		private readonly IUnidadeDeTrabalho _contexto;
		public TccRepositorio(IUnidadeDeTrabalho contextos) => this._contexto = contextos;

		public virtual async Task Adicionar(Tcc entidade, bool finalizarTransacao = true)
		{
			await this._contexto.SmartContexto.Tccs.AddAsync(entidade);

			if (finalizarTransacao)
				await this._contexto.SmartContexto.SaveChangesAsync();
		}

		public virtual async Task Atualizar(Tcc entidade, bool finalizarTransacao = true)
		{
			this._contexto.SmartContexto.Tccs.Update(entidade);

			if (finalizarTransacao)
				await this._contexto.SmartContexto.SaveChangesAsync();
		}

		public virtual async Task<IEnumerable<Tcc>> Obter() => await this._contexto.SmartContexto.Tccs.ToListAsync();

		public virtual async Task<Tcc> ObterAsync(IEspecificavel<Tcc> especificacao)
		{
			return await this._contexto.SmartContexto.GetDbSetWithQueryable(especificacao).FirstOrDefaultAsync();

			//var query = GetDbSetWithQueryable(especificacao);

			//return await (usarTracking ? query.FirstOrDefaultAsync(especificacao.ExpressaoEspecificacao) : query.AsNoTracking().FirstOrDefaultAsync(especificacao.ExpressaoEspecificacao));
		}

		public virtual async Task<IEnumerable<Tcc>> Procurar(IEspecificavel<Tcc> especificacao) =>
			await this._contexto.SmartContexto.GetDbSetWithQueryable(especificacao).ToListAsync();

		public virtual async Task RemoverAsync(Tcc entidade, bool finalizarTransacao = true)
		{
			await Task.FromResult(this._contexto.SmartContexto.Tccs.Remove(entidade));

			if (finalizarTransacao)
				this._contexto.SmartContexto.SaveChanges();
		}
	}
}
