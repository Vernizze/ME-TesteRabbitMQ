using MassTransit;
using MediatR;
using System.Threading.Tasks;
using static TesteRabbitMQ.MassTransit.Subscriber.UseCases.AddPerson.AddPerson.Model;

namespace TesteRabbitMQ.MassTransit.Subscriber.Consumers
{
    internal interface IAddPersonConsumer
    {
    }

    internal class AddPersonConsumer
        : IConsumer<Input>, IAddPersonConsumer
    {
        #region Variables

        private IMediator _mediator;

        #endregion

        #region Variables

        public AddPersonConsumer(IMediator mediator) 
            => _mediator = mediator;

        #endregion

        #region Methods

        public Task Consume(ConsumeContext<Input> context)
        {
            _mediator.Send(context.Message);

            return Task.CompletedTask;
        }

        #endregion
    }
}
