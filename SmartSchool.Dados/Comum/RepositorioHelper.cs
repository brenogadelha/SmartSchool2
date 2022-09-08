using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SmartSchool.Comum.Especificao;

namespace SmartSchool.Dados.Comum
{
	public static class RepositorioHelper
	{
		public static IEnumerable<T> ObterPorEspecificacao<T>(this DbContext context, IEspecificavel<T> especificacao) where T : class
		{
			var queryableResultWithIncludes = especificacao
													   .ObjetosInclusaoTipo
													   .Aggregate(context.Set<T>().AsQueryable(),
														(current, include) => current.Include(include));

			var queryableResultWithIncludesStrings = especificacao.ObjetosInclusaoStrings
															.Aggregate(queryableResultWithIncludes,
															(current, include) => current.Include(include));


			return queryableResultWithIncludesStrings
						.Where<T>(especificacao.ExpressaoEspecificacao)
						.AsEnumerable();
		}

		public static IQueryable<T> GetDbSetWithQueryable<T>(this DbContext context, IEspecificavel<T> especificacao) where T : class
		{
			IQueryable<T> query = context.Set<T>();

			foreach (var inclusao in especificacao.ObjetosInclusaoTipo)
			{
				query = query.Include(inclusao);
			}

			foreach (var subInclusao in especificacao.ObjetosInclusaoStrings)
			{
				query = query.Include(subInclusao);
			}

			return query.Where<T>(especificacao.ExpressaoEspecificacao);
		}
	}
}