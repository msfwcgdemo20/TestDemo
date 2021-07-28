using TestDemo.Domain;
using MFoundation.Core.Repositories;
using System;
using System.Collections.Generic;

namespace TestDemo.Infrastructure.Repositories
{
    public interface ITestDemoModelRepository
    {
        IUnitOfWork UnitOfWork { get; }
        void Create(Domain.TestDemoModel TestDemoModel);
        void Update(Domain.TestDemoModel TestDemoModel);
        bool Delete(Guid id);
    }
}
