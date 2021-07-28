using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace TestDemo.Infrastructure.Repositories
{
    public interface IReadTestDemoModelRepository
    {
        IQueryable<Domain.TestDemoModel> GetAllTestDemoModels();
        Domain.TestDemoModel GetTestDemoModelById(Guid id);
    }
}