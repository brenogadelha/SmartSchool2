using Microsoft.EntityFrameworkCore;
using SmartSchool.Comum.Especificao;
using SmartSchool.Comum.Repositorio;
using SmartSchool.Dados.Comum;
using SmartSchool.Dominio.Cursos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartSchool.Dados.Modulos.Cursos
{
	public class CursoRepositorio : IRepositorio<Curso>
	{
		private readonly IUnidadeDeTrabalho _contexto;
		public CursoRepositorio(IUnidadeDeTrabalho contextos) => this._contexto = contextos;

		public virtual async Task Adicionar(Curso entidade, bool finalizarTransacao = true)
		{
			await this._contexto.SmartContexto.Cursos.AddAsync(entidade);

			if (finalizarTransacao)
				await this._contexto.SmartContexto.SaveChangesAsync();
		}

		public virtual async Task Atualizar(Curso entidade, bool finalizarTransacao = true)
		{
			this._contexto.SmartContexto.Cursos.Update(entidade);

			if (finalizarTransacao)
				await this._contexto.SmartContexto.SaveChangesAsync();
		}

		public virtual async Task<IEnumerable<Curso>> Obter() => await this._contexto.SmartContexto.Cursos.ToListAsync();

		public virtual async Task<Curso> ObterAsync(IEspecificavel<Curso> especificacao)
		{
			return await this._contexto.SmartContexto.GetDbSetWithQueryable(especificacao).FirstOrDefaultAsync();

			//var query = GetDbSetWithQueryable(especificacao);

			//return await (usarTracking ? query.FirstOrDefaultAsync(especificacao.ExpressaoEspecificacao) : query.AsNoTracking().FirstOrDefaultAsync(especificacao.ExpressaoEspecificacao));
		}

		public virtual async Task<IEnumerable<Curso>> Procurar(IEspecificavel<Curso> especificacao) =>
			await this._contexto.SmartContexto.GetDbSetWithQueryable(especificacao).ToListAsync();

		public virtual async Task RemoverAsync(Curso entidade, bool finalizarTransacao = true)
		{
			await Task.FromResult(this._contexto.SmartContexto.Cursos.Remove(entidade));

			if (finalizarTransacao)
				this._contexto.SmartContexto.SaveChanges();
		}
	}
}
