using MediatR;
using Serilog;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TesteRabbitMQ.DataTypes.Entities;
using TesteRabbitMQ.UseCases.Data.Databases;
using TesteRabbitMQ.UseCases.Data.Repositories.Base;

namespace TesteRabbitMQ.UseCases.AddPerson
{
    public class AddPerson
    {
        public class Model 
        {
            public class Input
                : IRequest<Output>
            {
                public string Name { get; set; }
                public int Age { get; set; }
            }

            public class Output
            {
                public Person Person { get; set; }
            }
        }

        public class Handler
            : IRequestHandler<Model.Input, Model.Output>
        {
            #region Variables

            private readonly IMediator _mediator;
            private readonly ILogger _logger;

            #endregion

            #region Constructors

            public Handler(ILogger logger, IMediator mediator) 
            {
                _logger = logger;
                _mediator = mediator;
            }

            #endregion

            #region Methods

            public async Task<Model.Output> Handle(Model.Input request, CancellationToken cancellationToken)
            {
                var person = new Person
                {
                    Id = Guid.NewGuid(),
                    Age = request.Age,
                    Name = request.Name
                };

                AddPersonRepository.Instance.Add(person);

                var getPersons = await _mediator.Send(new GetAllPersons.GetAllPersons.Model.Input());

                var persons = getPersons?.Persons;

                if (persons != null) 
                {
                    _logger.Information($"Persons in Repository => {persons.Count}");

                    persons.ForEach(p => _logger.Information($"Person Id: {p.Id} - Person Name: {p.Name} = Person Age: {p.Age}"));
                }                

                return await Task.FromResult(new Model.Output { Person = person });
            }

            #endregion
        }

        public class AddPersonRepository
           : BaseRepository<Person>
        {
            #region Variables

            private static AddPersonRepository _instance;

            #endregion

            #region Constructors

            private AddPersonRepository()
                => _entities = PersonDB.Instance.GetEntities;

            #endregion

            #region Attributes

            public static AddPersonRepository Instance
            {
                get
                {
                    _instance = _instance ?? new AddPersonRepository();


                    return _instance;
                }
            }

            #endregion

            #region Methods

            public override Person GetOne(Guid id)
                => throw new NotImplementedException("Método 'GetOne' não implementado nesse Repositório!");

            public override List<Person> GetAll()
                => throw new NotImplementedException("Método 'GetAll' não implementado nesse Repositório!");

            public override void Update(Person entity)
                => throw new NotImplementedException("Método 'Update' não implementado nesse Repositório!");

            public override void Delete(Guid id)
                => throw new NotImplementedException("Método 'Delete' não implementado nesse Repositório!");

            #endregion
        }
    }
}
