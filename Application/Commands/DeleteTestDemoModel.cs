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
    public class DeleteTestDemoModel : ICommand<bool>
    {
        public Guid Id { get; private set; }

        public DeleteTestDemoModel(Guid id)
        {
            Id = id;
        }
    }

    internal class DeleteTestDemoModelHandler : ICommandHandler<DeleteTestDemoModel, bool>
    {
        private readonly ILogger<DeleteTestDemoModelHandler> _logger;
        private readonly ITestDemoModelRepository _testDemoRepository;
        public DeleteTestDemoModelHandler(ILogger<DeleteTestDemoModelHandler> logger, ITestDemoModelRepository Repository)
        {
            _logger = logger;
            _testDemoRepository = Repository;
        }

        public async Task<bool> Handle(DeleteTestDemoModel request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Handling Create TestDemoModel request. TestDemoModel Id: {request.Id}");

            bool result = _testDemoRepository.Delete(request.Id);
            if (result)
            {
                await _testDemoRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            }
            return result;
        }
    }
}
