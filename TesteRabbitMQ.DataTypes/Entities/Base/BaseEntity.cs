using System;

namespace TesteRabbitMQ.DataTypes.Entities.Base
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }
        public abstract BaseEntity UpdateEntity(BaseEntity newEntity);
    }
}
