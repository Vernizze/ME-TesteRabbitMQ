using MassTransit;
using MediatR;
using Serilog;
using System.Threading.Tasks;
using static TesteRabbitMQ.UseCases.AddPerson.AddPerson.Model;

namespace TesteRabbitMQ.MassTransit.Subscriber.Consumers
{
    internal interface IAddPersonConsumer
    {
    }

    internal class AddPersonConsumer
        : IConsumer<Input>, IAddPersonConsumer
    {
        #region Variables

        private ILogger _logger;
        private IMediator _mediator;

        #endregion

        #region Constructors

        public AddPersonConsumer(ILogger logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        #endregion

        #region Methods

        public async Task Consume(ConsumeContext<Input> context)
            => await _mediator.Send(new Input { Name = context.Message.Name, Age = context.Message.Age });

        #endregion
    }
}
