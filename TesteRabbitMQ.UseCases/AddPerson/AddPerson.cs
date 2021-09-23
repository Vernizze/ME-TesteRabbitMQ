using MediatR;
using Serilog;
using System;
using System.Threading;
using System.Threading.Tasks;
using TesteRabbitMQ.UseCases.Data.Entities;
using TesteRabbitMQ.UseCases.Data.Repositories;
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
                public ILogger Logger { get; set; }
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
            #region Methods

            public async Task<Model.Output> Handle(Model.Input request, CancellationToken cancellationToken)
            {
                var person = new Person
                {
                    Id = Guid.NewGuid(),
                    Age = request.Age,
                    Name = request.Name
                };

                PersonRepository.Instance.Add(person);

                var persons = PersonRepository.Instance.GetAll();

                if (persons != null) 
                {
                    request.Logger.Information($"Persons in Repository => {persons.Count}");

                    persons.ForEach(p => request.Logger.Information($"Person Id: {p.Id} - Person Name: {p.Name} = Person Age: {p.Age}"));
                }                

                return await Task.FromResult(new Model.Output { Person = person });
            }

            #endregion
        }
    }
}
