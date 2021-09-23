using TesteRabbitMQ.DataTypes.Entities.Base;

namespace TesteRabbitMQ.DataTypes.Entities
{
    public class Person
        : BaseEntity
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public override BaseEntity UpdateEntity(BaseEntity newEntity)
        {
            var newPerson = newEntity as Person;

            this.Name = newPerson.Name;
            this.Age = newPerson.Age;

            return this;
        }
    }
}
