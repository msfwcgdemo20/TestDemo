using MFoundation.Core.Messaging.Queries;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestDemo.Infrastructure.Repositories;

namespace TestDemo.Application.Queries
{
    public class GetTestDemoModelById : IQuery<Domain.TestDemoModel>
    {
        public Guid TestDemoModelId { get; private set; }

        public GetTestDemoModelById(Guid id)
        {
            TestDemoModelId = id;
        }
    }

    internal class GetTestDemoModelByIdHandler : IQueryHandler<GetTestDemoModelById, Domain.TestDemoModel>
    {
        private readonly ILogger<GetTestDemoModelByIdHandler> _logger;
        private readonly IReadTestDemoModelRepository _readTestDemoModelRepository;

        public GetTestDemoModelByIdHandler(ILogger<GetTestDemoModelByIdHandler> logger,
            IReadTestDemoModelRepository readTestDemoModelRepository)
        {
            _logger = logger;
            _readTestDemoModelRepository = readTestDemoModelRepository;
        }

        public Task<Domain.TestDemoModel> Handle(GetTestDemoModelById request, CancellationToken cancellationToken)
        {
            var result = _readTestDemoModelRepository.GetTestDemoModelById(request.TestDemoModelId);
            return Task.FromResult(result);
        }
    }
}
