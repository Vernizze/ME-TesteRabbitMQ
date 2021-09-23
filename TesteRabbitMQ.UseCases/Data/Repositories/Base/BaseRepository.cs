using System;
using System.Collections.Generic;
using System.Linq;
using TesteRabbitMQ.UseCases.Data.Entities.Base;

namespace TesteRabbitMQ.UseCases.Data.Repositories.Base
{
    public abstract class BaseRepository<TEntity>
        where TEntity : BaseEntity
    {
        #region Variables

        protected Dictionary<Guid, TEntity> _entities = new Dictionary<Guid, TEntity>();

        #endregion

        #region Methods

        public void Add(TEntity entity)
            => _entities.Add(entity.Id, entity);

        public TEntity GetOne(Guid id)
        {
            _entities.TryGetValue(id, out TEntity result);

            return result;
        }

        public List<TEntity> GetAll()
            => _entities.Values.ToList();

        public void Update(TEntity entity)
        {
            if (_entities.TryGetValue(entity.Id, out TEntity result))
                _entities[entity.Id] = result.UpdateEntity(result) as TEntity;
        }

        public void Delete(Guid id)
        {
            if (_entities.TryGetValue(id, out TEntity result))
                _entities.Remove(id);
        }

        #endregion
    }
}
