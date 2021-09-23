using System;
using System.Collections.Generic;
using TesteRabbitMQ.DataTypes.Entities;
using TesteRabbitMQ.DataTypes.Interfaces.Databases;

namespace TesteRabbitMQ.UseCases.Data.Databases
{
    public class PersonDB
        : IBaseDB<Person>
    {
        #region Variables

        private static PersonDB _instance;

        protected Dictionary<Guid, Person> _entities = new Dictionary<Guid, Person>();        

        #endregion

        #region Constructors

        private PersonDB() { }

        #endregion

        #region Attributes

        public static PersonDB Instance
        {
            get
            {
                _instance = _instance ?? new PersonDB();


                return _instance;
            }
        }

        public Dictionary<Guid, Person> GetEntities => _entities;

        #endregion
    }
}
