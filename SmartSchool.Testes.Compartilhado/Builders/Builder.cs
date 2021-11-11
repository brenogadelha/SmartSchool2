using Microsoft.EntityFrameworkCore;

namespace SmartSchool.Testes.Compartilhado.Builders
{
	public abstract class Builder<T> where T : class
	{
		public abstract T Instanciar();
		protected virtual void CriarDependencias(DbContext context) { }

		public T Criar(DbContext context)
		{
			CriarDependencias(context);

			var entity = Instanciar();
			context.Set<T>().Add(entity);

			context.SaveChanges();

			return entity;
		}
	}
}
