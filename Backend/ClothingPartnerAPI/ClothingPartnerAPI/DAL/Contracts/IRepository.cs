using System.Linq.Expressions;

namespace ClothingPartnerAPI.DAL.Contracts
{
    //Base repository with CRUDs
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity Get(int id);
        IEnumerable<TEntity> GetAll(params Expression<Func<TEntity, object>>[] properties);
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(int id);

        int CommitChanges();
        Task<int> CommitChangesAsync();
    }
}
