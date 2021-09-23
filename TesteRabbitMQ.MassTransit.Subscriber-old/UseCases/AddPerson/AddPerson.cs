using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace TesteRabbitMQ.MassTransit.Subscriber.UseCases.AddPerson
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
            }
        }

        public class Handler
            : IRequestHandler<Model.Input, Model.Output>
        {
            #region Methods

            public async Task<Model.Output> Handle(Model.Input request, CancellationToken cancellationToken)
            {


                return await Task.FromResult(new Model.Output {  });
            }

            #endregion
        }
    }
}
