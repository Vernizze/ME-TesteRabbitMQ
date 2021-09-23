using System;
using System.Collections.Generic;

namespace TesteRabbitMQ.DataTypes.Interfaces.Databases
{
    public interface IBaseDB<TEntity>
    {
        public Dictionary<Guid, TEntity> GetEntities { get; }
    }
}
