using ClothingPartnerAPI.DAL.Context;
using ClothingPartnerAPI.DAL.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ClothingPartnerAPI.DAL
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected dynamic DbContext;
        protected readonly DbSet<TEntity> DbSet;

        public Repository(ClothingPartnerContext context)
        {
            DbContext = context;
            DbSet = DbContext.Set<TEntity>();
        }

        public TEntity Get(int id)
        {
            return DbSet.Find(id);
        }

        public void Add(TEntity entity)
        {
            DbSet.Add(entity);
        }

        public void Update(TEntity entity)
        {
            DbSet.Update(entity);
        }

        public virtual IEnumerable<TEntity> GetAll(params Expression<Func<TEntity, object>>[] properties)
        {
            if (properties == null)
                throw new ArgumentNullException(nameof(properties));

            var query = DbSet as IQueryable<TEntity>; // _dbSet = dbContext.Set<TEntity>()

            query = properties
                       .Aggregate(query, (current, property) => current.Include(property));

            return query.AsNoTracking().ToList(); //readonly
        }

        public void Delete(int id)
        {
            TEntity existing = DbContext.Find<TEntity>(id);
            if (existing != null) DbSet.Remove(existing);
        }

        public int CommitChanges()
        {
            return DbContext.SaveChanges();
        }

        public Task<int> CommitChangesAsync()
        {
            return DbContext.SaveChangesAsync();
        }
    }
}
