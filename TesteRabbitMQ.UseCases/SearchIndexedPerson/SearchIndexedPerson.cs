using MediatR;
using Microsoft.Extensions.Options;
using ES = Nest;
using Serilog;
using System;
using System.Threading;
using System.Threading.Tasks;
using TesteRabbitMQ.Crosscutting.AppSettings;
using TesteRabbitMQ.DataTypes.Entities;
using Newtonsoft.Json;

namespace TesteRabbitMQ.UseCases.SearchIndexedPerson
{
    /*
     Comandos Docker para criar Server ElasticSearch:

    docker pull docker.elastic.co/elasticsearch/elasticsearch:7.15.0

    docker run -p 9200:9200 -p 9300:9300 -e "discovery.type=single-node" docker.elastic.co/elasticsearch/elasticsearch:7.15.0
     */

    public class SearchIndexedPerson
    {
        public class Model
        {
            public class Input
                : IRequest<Output>
            {
                public Guid PersonId { get; set; }
            }

            public class Output
            {
                public string IndexedPersonJson { get; set; }
            }
        }

        public class Handler
            : IRequestHandler<Model.Input, Model.Output>
        {
            #region Variables

            private readonly ILogger _logger;
            private readonly AppSettings _appSettings;

            #endregion

            #region Constructors

            public Handler(ILogger logger, IOptions<AppSettings> appSettingsOptions)
            {
                _logger = logger;

                _appSettings = appSettingsOptions.Value;
            }

            #endregion

            #region Methods

            public async Task<Model.Output> Handle(Model.Input request, CancellationToken cancellationToken)
            {
                var settings = new ES.ConnectionSettings(new Uri($"{_appSettings.ElasticSearchNodeUrl}:{_appSettings.ElasticSearchNodePort}"));
                var client = new ES.ElasticClient(settings);

                var indexedPerson = client.Get<Person>(request.PersonId, idx => idx.Index(_appSettings.ElasticSearchPersonIndexName));

                var result = JsonConvert.SerializeObject(indexedPerson.Source);

                _logger.Information($"'Person' indexada => {result}");

                return await Task.FromResult(new Model.Output { IndexedPersonJson = result });
            }

            #endregion
        }
    }
}
