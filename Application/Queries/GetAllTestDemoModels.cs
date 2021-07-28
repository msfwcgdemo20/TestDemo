using MFoundation.Core.Messaging.Queries;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestDemo.Infrastructure.Repositories;

namespace TestDemo.Application.Queries
{
    public class GetAllTestDemoModels : IQuery<IEnumerable<Domain.TestDemoModel>>
    {
    }

    internal class GetAllTestDemoModelsHandler : IQueryHandler<GetAllTestDemoModels, IEnumerable<Domain.TestDemoModel>>
    {
        private readonly ILogger<GetAllTestDemoModelsHandler> _logger;
        private readonly IReadTestDemoModelRepository _readTestDemoModelRepository;

        public GetAllTestDemoModelsHandler(ILogger<GetAllTestDemoModelsHandler> logger,
            IReadTestDemoModelRepository readTestDemoModelRepository)
        {
            _logger = logger;
            _readTestDemoModelRepository = readTestDemoModelRepository;
        }

        public Task<IEnumerable<Domain.TestDemoModel>> Handle(GetAllTestDemoModels request, CancellationToken cancellationToken)
        {
            var result = _readTestDemoModelRepository.GetAllTestDemoModels().AsEnumerable();
            return Task.FromResult(result);
        }
    }
}
