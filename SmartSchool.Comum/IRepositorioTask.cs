using SmartSchool.Comum.Dominio;
using SmartSchool.Comum.Especificao;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartSchool.Comum.Repositorio
{
	public interface IRepositorioTask<TEntity>
		where TEntity : class
	{
		Task<IEnumerable<TEntity>> Obter();

		Task<TEntity> ObterAsync(IEspecificavel<TEntity> especificacao);

		Task<IEnumerable<TEntity>> Procurar(IEspecificavel<TEntity> especificacao);

		Task Adicionar(TEntity entidade, bool finalizarTransacao = true);

		Task Atualizar(TEntity entidade, bool finalizarTransacao = true);

		Task RemoverAsync(TEntity entidade, bool finalizarTransacao = true);
	}
}
