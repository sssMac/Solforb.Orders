using Microsoft.EntityFrameworkCore;
using Solforb.Orders.Application.Persistence.Contracts;
using Solforb.Orders.Domain;
using Solforb.Orders.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Solforb.Orders.Persistence.Repositories
{
	public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseDomainEntity
	{
		private OrdersDbContext _context;
		private DbSet<TEntity> dbSet;

		public GenericRepository(OrdersDbContext context)
		{
			this._context = context;
			dbSet = context.Set<TEntity>();
		}

		public virtual async Task<IEnumerable<TEntity>> Get(
			Expression<Func<TEntity, bool>> filter = null,
			Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
			string includeProperties = "")
		{
			IQueryable<TEntity> query = dbSet;

			if (filter != null)
			{
				query = query.Where(filter);
			}
			foreach (var includeProperty in includeProperties.Split
				(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
			{
				query = query.Include(includeProperty);
			}


			if (orderBy != null)
			{
				return await orderBy(query).ToListAsync();
			}
			else
			{
				return await query.ToListAsync();
			}
		}

		public virtual async Task<TEntity> GetByID(
			int id,
			string includeProperties = "")
		{
			IQueryable<TEntity> query = dbSet;

			foreach (var includeProperty in includeProperties.Split
			(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
			{
				query = query.Include(includeProperty);
			}

			return await query.Where(e => e.Id == id).FirstOrDefaultAsync();
		}

		public virtual async Task Delete(object id)
		{
			TEntity entityToDelete = await dbSet.FindAsync(id);
			await Delete(entityToDelete);
		}

		public virtual async Task Delete(TEntity entityToDelete)
		{
			if (_context.Entry(entityToDelete).State == EntityState.Detached)
			{
				dbSet.Attach(entityToDelete);
			}
			dbSet.Remove(entityToDelete);
		}

		public virtual async Task Update(TEntity entityToUpdate)
		{
			dbSet.Attach(entityToUpdate);
			_context.Entry(entityToUpdate).State = EntityState.Modified;
		}

		public virtual async Task<TEntity> Insert(TEntity entity)
		{
			await dbSet.AddAsync(entity);
			return entity;
		}

		public virtual async Task<bool> Exists(int id)
		{
			var entity = await GetByID(id);
			return entity != null;
		}

		public virtual async Task InsertRange(List<TEntity> entity)
		{
			await dbSet.AddRangeAsync(entity);
		}

		public virtual async Task UpdateRange(List<TEntity> entityToUpdate)
		{
			dbSet.AttachRange(entityToUpdate);
			_context.Entry(entityToUpdate).State = EntityState.Modified;
		}
	}
}

