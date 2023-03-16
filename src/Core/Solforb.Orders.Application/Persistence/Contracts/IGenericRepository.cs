using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Solforb.Orders.Application.Persistence.Contracts
{
	public interface IGenericRepository<TEntity> where TEntity : class
	{
		Task<IEnumerable<TEntity>> Get(
			Expression<Func<TEntity, bool>> filter = null,
			Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
			string includeProperties = "");

		Task<TEntity> GetByID(
			object id,
			string includeProperties = "");
		Task<TEntity> Insert(TEntity entity);
		Task<bool> Exists(object id);
		Task Insert(List<TEntity> entity);
		Task Delete(object id);
		Task Delete(TEntity entityToDelete);
		Task Update(TEntity entityToUpdate);
		Task Update(List<TEntity> entityToUpdate);
	}
}
