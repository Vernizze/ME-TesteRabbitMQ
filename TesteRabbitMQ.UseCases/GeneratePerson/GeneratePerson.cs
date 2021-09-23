using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Add = TesteRabbitMQ.UseCases.AddPerson.AddPerson.Model;

namespace TesteRabbitMQ.UseCases.GeneratePerson
{
    public class GeneratePerson
    {
        public class Model
        {
            public class Input
                : IRequest<Output>
            {
            }

            public class Output
            {
                public Add.Input Input { get; set; }
            }
        }

        public class Handler
            : IRequestHandler<Model.Input, Model.Output>
        {
            #region Methods

            public async Task<Model.Output> Handle(Model.Input request, CancellationToken cancellationToken)
            {
                int rnd = new Random().Next();

                var result = new Add.Input 
                {
                    Name = $"Person#{rnd}",
                    Age = rnd
                };

                return await Task.FromResult(new Model.Output { Input = result });
            }

            #endregion
        }
    }
}
