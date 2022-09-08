using Microsoft.EntityFrameworkCore;
using SmartSchool.Comum.Especificao;
using SmartSchool.Comum.Repositorio;
using SmartSchool.Dados.Comum;
using SmartSchool.Dominio.Semestres;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartSchool.Dados.Modulos.Semestres
{
	public class SemestreRepositorio : IRepositorio<Semestre>
	{
		private readonly IUnidadeDeTrabalho _contexto;
		public SemestreRepositorio(IUnidadeDeTrabalho contextos) => this._contexto = contextos;

		public virtual async Task Adicionar(Semestre entidade, bool finalizarTransacao = true)
		{
			await this._contexto.SmartContexto.Semestres.AddAsync(entidade);

			if (finalizarTransacao)
				await this._contexto.SmartContexto.SaveChangesAsync();
		}

		public virtual async Task Atualizar(Semestre entidade, bool finalizarTransacao = true)
		{
			this._contexto.SmartContexto.Semestres.Update(entidade);

			if (finalizarTransacao)
				await this._contexto.SmartContexto.SaveChangesAsync();
		}

		public virtual async Task<IEnumerable<Semestre>> Obter() => await this._contexto.SmartContexto.Semestres.ToListAsync();

		public virtual async Task<Semestre> ObterAsync(IEspecificavel<Semestre> especificacao)
		{
			return await this._contexto.SmartContexto.GetDbSetWithQueryable(especificacao).FirstOrDefaultAsync();

			//var query = GetDbSetWithQueryable(especificacao);

			//return await (usarTracking ? query.FirstOrDefaultAsync(especificacao.ExpressaoEspecificacao) : query.AsNoTracking().FirstOrDefaultAsync(especificacao.ExpressaoEspecificacao));
		}

		public virtual async Task<IEnumerable<Semestre>> Procurar(IEspecificavel<Semestre> especificacao) =>
			await this._contexto.SmartContexto.GetDbSetWithQueryable(especificacao).ToListAsync();

		public virtual async Task RemoverAsync(Semestre entidade, bool finalizarTransacao = true)
		{
			await Task.FromResult(this._contexto.SmartContexto.Semestres.Remove(entidade));

			if (finalizarTransacao)
				this._contexto.SmartContexto.SaveChanges();
		}
	}
}
