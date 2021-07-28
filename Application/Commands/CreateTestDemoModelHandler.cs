using TestDemo.Infrastructure.Repositories;
using MediatR;
using MFoundation.Core.Messaging.Commands;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TestDemo.Application.Commands
{
    public class CreateTestDemoModelHandler : ICommandHandler<CreateTestDemoModel>
    {
        private readonly ILogger<CreateTestDemoModelHandler> _logger;
        private readonly ITestDemoModelRepository _testDemoRepository;
        public CreateTestDemoModelHandler(ILogger<CreateTestDemoModelHandler> logger, ITestDemoModelRepository TestDemoModelRepository)
        {
            _logger = logger;
            _testDemoRepository = TestDemoModelRepository;
        }

        public async Task<Unit> Handle(CreateTestDemoModel request, CancellationToken cancellationToken)
        {
            // throw new NotImplementedException();
            _logger.LogInformation($"Handling Create Custom TestDemoModel request. TestDemoModel Name: {request.Name}");

            Domain.TestDemoModel TestDemoModel = new Domain.TestDemoModel(request.Name);

            _testDemoRepository.Create(TestDemoModel);
            await _testDemoRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            return Unit.Value;
        }

        private bool DoValidate(CreateTestDemoModel createTestDemoModel)
        {
            return true;
        }
    }
}
