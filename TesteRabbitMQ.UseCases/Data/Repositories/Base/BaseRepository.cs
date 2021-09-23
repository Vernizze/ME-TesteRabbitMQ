using System;
using System.Collections.Generic;
using System.Linq;
using TesteRabbitMQ.DataTypes.Entities.Base;

namespace TesteRabbitMQ.UseCases.Data.Repositories.Base
{
    public abstract class BaseRepository<TEntity>
        where TEntity : BaseEntity
    {
        #region Variables

        protected Dictionary<Guid, TEntity> _entities = new Dictionary<Guid, TEntity>();

        #endregion

        #region Methods

        public virtual void Add(TEntity entity)
            => _entities.Add(entity.Id, entity);

        public virtual TEntity GetOne(Guid id)
        {
            _entities.TryGetValue(id, out TEntity result);

            return result;
        }

        public virtual List<TEntity> GetAll()
            => _entities.Values.ToList();

        public virtual void Update(TEntity entity)
        {
            if (_entities.TryGetValue(entity.Id, out TEntity result))
                _entities[entity.Id] = result.UpdateEntity(result) as TEntity;
        }

        public virtual void Delete(Guid id)
        {
            if (_entities.TryGetValue(id, out TEntity result))
                _entities.Remove(id);
        }

        #endregion
    }
}
