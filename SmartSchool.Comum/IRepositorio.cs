using SmartSchool.Comum.Dominio;
using SmartSchool.Comum.Especificao;
using System.Collections.Generic;

namespace SmartSchool.Comum.Repositorio
{
	public interface IRepositorio<TEntity>
        where TEntity : IEntidade
    {
        IEnumerable<TEntity> Obter();

        TEntity Obter(IEspecificavel<TEntity> especificacao);

        IEnumerable<TEntity> Procurar(IEspecificavel<TEntity> especificacao);

        void Adicionar(TEntity entidade, bool finalizarTransacao = true);

        void Atualizar(TEntity entidade, bool finalizarTransacao = true);

        void Remover(TEntity entidade, bool finalizarTransacao = true);

    }
}
