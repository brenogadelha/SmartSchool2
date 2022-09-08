using Microsoft.EntityFrameworkCore;
using SmartSchool.Comum.Especificao;
using SmartSchool.Comum.Repositorio;
using SmartSchool.Dados.Comum;
using SmartSchool.Dominio.Disciplinas;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartSchool.Dados.Modulos.Disciplinas
{
	public class DisciplinaRepositorio : IRepositorio<Disciplina>
	{
		private readonly IUnidadeDeTrabalho _contexto;
		public DisciplinaRepositorio(IUnidadeDeTrabalho contextos) => this._contexto = contextos;

		public virtual async Task Adicionar(Disciplina entidade, bool finalizarTransacao = true)
		{
			await this._contexto.SmartContexto.Disciplinas.AddAsync(entidade);

			if (finalizarTransacao)
				await this._contexto.SmartContexto.SaveChangesAsync();
		}

		public virtual async Task Atualizar(Disciplina entidade, bool finalizarTransacao = true)
		{
			this._contexto.SmartContexto.Disciplinas.Update(entidade);

			if (finalizarTransacao)
				await this._contexto.SmartContexto.SaveChangesAsync();
		}

		public virtual async Task<IEnumerable<Disciplina>> Obter() => await this._contexto.SmartContexto.Disciplinas.ToListAsync();

		public virtual async Task<Disciplina> ObterAsync(IEspecificavel<Disciplina> especificacao)
		{
			return await this._contexto.SmartContexto.GetDbSetWithQueryable(especificacao).FirstOrDefaultAsync();

			//var query = GetDbSetWithQueryable(especificacao);

			//return await (usarTracking ? query.FirstOrDefaultAsync(especificacao.ExpressaoEspecificacao) : query.AsNoTracking().FirstOrDefaultAsync(especificacao.ExpressaoEspecificacao));
		}

		public virtual async Task<IEnumerable<Disciplina>> Procurar(IEspecificavel<Disciplina> especificacao) =>
			await this._contexto.SmartContexto.GetDbSetWithQueryable(especificacao).ToListAsync();

		public virtual async Task RemoverAsync(Disciplina entidade, bool finalizarTransacao = true)
		{
			await Task.FromResult(this._contexto.SmartContexto.Disciplinas.Remove(entidade));

			if (finalizarTransacao)
				this._contexto.SmartContexto.SaveChanges();
		}
	}
}
