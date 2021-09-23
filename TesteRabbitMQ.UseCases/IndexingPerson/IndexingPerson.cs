using MediatR;
using Microsoft.Extensions.Options;
using ES = Nest;
using Serilog;
using System;
using System.Threading;
using System.Threading.Tasks;
using TesteRabbitMQ.Crosscutting.AppSettings;
using TesteRabbitMQ.DataTypes.Entities;

namespace TesteRabbitMQ.UseCases.IndexingPerson
{
    public class IndexingPerson
    {
        public class Model
        {
            public class Input
                : IRequest<Output>
            {
                public Person Person { get; set; }
            }

            public class Output
            {
            }
        }

        public class Handler
            : IRequestHandler<Model.Input, Model.Output>
        {
            #region Variables

            private readonly ILogger _logger;
            private readonly AppSettings _appSettings;

            private readonly IMediator _mediator;

            #endregion

            #region Constructors

            public Handler(ILogger logger, IOptions<AppSettings> appSettingsOptions, IMediator mediator)
            {
                _logger = logger;

                _appSettings = appSettingsOptions.Value;

                _mediator = mediator;
            }

            #endregion

            #region Methods

            public async Task<Model.Output> Handle(Model.Input request, CancellationToken cancellationToken)
            {
                var settings = new ES.ConnectionSettings(new Uri($"{_appSettings.ElasticSearchNodeUrl}:{_appSettings.ElasticSearchNodePort}"));
                var client = new ES.ElasticClient(settings);

                var responseES = client.Index(request.Person, idx => idx.Index(_appSettings.ElasticSearchPersonIndexName));

                _logger.Information($"Resultado indexação da 'Person' de Id {request.Person.Id} => {responseES.Result}");

                var searchPersonResult = await _mediator.Send(new SearchIndexedPerson.SearchIndexedPerson.Model.Input { PersonId = request.Person.Id });

                return await Task.FromResult(new Model.Output());
            }

            #endregion
        }
    }
}
