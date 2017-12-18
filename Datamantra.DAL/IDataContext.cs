using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datamantra.DAL
{
    public interface IDataContext
    {
        /// <summary>
		/// Applies the pending changes to the database.
		/// </summary>
		/// <returns></returns>
		int SaveChanges();

        /// <summary>
        /// Applies the pending changes to the database asynchronously.
        /// </summary>
        /// <returns></returns>
        Task<int> SaveChangesAsync();

        IDbSet<TEntity> Set<TEntity>() where TEntity : class;

        DbContextTransaction BeginTransaction();

        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
    }
}
