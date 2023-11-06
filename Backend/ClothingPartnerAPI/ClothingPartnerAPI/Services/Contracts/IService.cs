using System.Linq.Expressions;

namespace ClothingPartnerAPI.Services.Contracts
{
    public interface IService<TEntityModel> where TEntityModel : class
    {
        TEntityModel Get(int id);
        IEnumerable<TEntityModel> GetAll(params Expression<Func<TEntityModel, object>>[] properties);
        bool Add(TEntityModel entity);
        void Update(TEntityModel entity);
        bool Delete(int id);
    }
}
