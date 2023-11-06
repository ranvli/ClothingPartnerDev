using ClothingPartnerAPI.DAL.Contracts;
using ClothingPartnerAPI.Services.Contracts;
using System.Linq.Expressions;

namespace ClothingPartnerAPI.Services
{
    public abstract class Service<TEntityModel> : IService<TEntityModel> where TEntityModel : class
    {
        private readonly IRepository<TEntityModel> Repository;

        public Service(IRepository<TEntityModel> repository)
        {
            Repository = repository;
        }

        public bool Add(TEntityModel entity)
        {
            Repository.Add(entity);
            int result = Repository.CommitChanges();
            return result > 0;
        }

        public bool Delete(int id)
        {
            Repository.Delete(id);
            int result = Repository.CommitChanges();
            return result > 0;
        }

        public TEntityModel Get(int id)
        {
            var model = Repository.Get(id);

            if (model == null)
                throw new Exception("ErrNotFound");

            return model;
        }

        public IEnumerable<TEntityModel> GetAll(params Expression<Func<TEntityModel, object>>[] properties)
        {
            IEnumerable<TEntityModel> models;
            models = Repository.GetAll(properties);
            return models;
        }

        public void Update(TEntityModel entity)
        {
            Repository.Update(entity);
            Repository.CommitChanges();
        }
    }
}
