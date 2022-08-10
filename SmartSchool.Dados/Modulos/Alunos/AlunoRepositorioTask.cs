using Microsoft.EntityFrameworkCore;
using SmartSchool.Comum.Especificao;
using SmartSchool.Comum.Repositorio;
using SmartSchool.Dados.Comum;
using SmartSchool.Dominio.Alunos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartSchool.Dados.Modulos.Alunos
{
	public class AlunoRepositorioTask : IRepositorioTask<Aluno>
	{
		private readonly IUnidadeDeTrabalho _contexto;
		public AlunoRepositorioTask(IUnidadeDeTrabalho contextos) => this._contexto = contextos;

		public virtual async Task Adicionar(Aluno entidade, bool finalizarTransacao = true)
		{
			await this._contexto.SmartContexto.Alunos.AddAsync(entidade);

			if (finalizarTransacao)
				await this._contexto.SmartContexto.SaveChangesAsync();
		}

		public virtual async Task Atualizar(Aluno entidade, bool finalizarTransacao = true)
		{
			this._contexto.SmartContexto.Alunos.Update(entidade);

			if (finalizarTransacao)
				await this._contexto.SmartContexto.SaveChangesAsync();
		}

		public virtual async Task<IEnumerable<Aluno>> Obter() => await this._contexto.SmartContexto.Alunos.ToListAsync();

		public virtual async Task<Aluno> ObterAsync(IEspecificavel<Aluno> especificacao)
		{
			return await this._contexto.SmartContexto.GetDbSetWithQueryable(especificacao).FirstOrDefaultAsync();

			//var query = GetDbSetWithQueryable(especificacao);

			//return await (usarTracking ? query.FirstOrDefaultAsync(especificacao.ExpressaoEspecificacao) : query.AsNoTracking().FirstOrDefaultAsync(especificacao.ExpressaoEspecificacao));
		}

		public virtual async Task<IEnumerable<Aluno>> Procurar(IEspecificavel<Aluno> especificacao) =>
			await this._contexto.SmartContexto.GetDbSetWithQueryable(especificacao).ToListAsync();

		public virtual async Task RemoverAsync(Aluno entidade, bool finalizarTransacao = true)
		{
			await Task.FromResult(this._contexto.SmartContexto.Alunos.Remove(entidade));

			if (finalizarTransacao)
				this._contexto.SmartContexto.SaveChanges();
		}
	}
}
