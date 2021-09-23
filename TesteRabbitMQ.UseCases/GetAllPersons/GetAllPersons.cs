using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TesteRabbitMQ.DataTypes.Entities;
using TesteRabbitMQ.UseCases.Data.Databases;
using TesteRabbitMQ.UseCases.Data.Repositories.Base;

namespace TesteRabbitMQ.UseCases.GetAllPersons
{
    public class GetAllPersons
    {
        public class Model
        {
            public class Input
                : IRequest<Output>
            {
            }

            public class Output
            {
                public List<Person> Persons { get; set; }
            }
        }

        public class Handler
            : IRequestHandler<Model.Input, Model.Output>
        {
            #region Methods

            public async Task<Model.Output> Handle(Model.Input request, CancellationToken cancellationToken)
                => await Task.FromResult(new Model.Output { Persons = GetAllPersonRepository.Instance.GetAll() });

            #endregion
        }

        public class GetAllPersonRepository
           : BaseRepository<Person>
        {
            #region Variables

            private static GetAllPersonRepository _instance;

            #endregion

            #region Constructors

            private GetAllPersonRepository()
                => _entities = PersonDB.Instance.GetEntities;

            #endregion

            #region Attributes

            public static GetAllPersonRepository Instance
            {
                get
                {
                    _instance = _instance ?? new GetAllPersonRepository();


                    return _instance;
                }
            }

            #endregion

            #region Methods

            public override void Add(Person entity)
                => throw new NotImplementedException("Método 'Add' não implementado nesse Repositório!");

            public override Person GetOne(Guid id)
                => throw new NotImplementedException("Método 'GetOne' não implementado nesse Repositório!");

            public override void Update(Person entity)
                => throw new NotImplementedException("Método 'Update' não implementado nesse Repositório!");

            public override void Delete(Guid id)
                => throw new NotImplementedException("Método 'Delete' não implementado nesse Repositório!");

            #endregion
        }
    }
}
