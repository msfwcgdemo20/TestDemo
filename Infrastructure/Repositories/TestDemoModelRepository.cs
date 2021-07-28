using MFoundation.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TestDemo.Domain;
using System;
using System.Linq;

namespace TestDemo.Infrastructure.Repositories
{
    public class TestDemoModelRepository : ITestDemoModelRepository, IReadTestDemoModelRepository
    {
        private readonly ILogger<TestDemoModelRepository> _logger;
        private readonly TestDemoContext _context;

        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }

        public TestDemoModelRepository(ILogger<TestDemoModelRepository> logger, TestDemoContext context)
        {
            context.Database.EnsureCreated();
            _logger = logger;
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void Create(Domain.TestDemoModel TestDemoModel)
        {
            _logger.LogInformation($"Saving Create Custom TestDemo request. TestDemoId:{ TestDemoModel.Id }, TestDemo Name:{ TestDemoModel.Name }");

            _context.TestDemoModels.Add(TestDemoModel);

        }

        public void Update(Domain.TestDemoModel TestDemoModel)
        {
            _context.Entry(TestDemoModel).State = EntityState.Modified;
        }

        public bool Delete(Guid id)
        {
            Domain.TestDemoModel TestDemoModel = GetTestDemoModelById(id);
            if (TestDemoModel == null)
            {
                return false;

            }
            _context.Entry(TestDemoModel).State = EntityState.Deleted;
            return true;
        }

        public IQueryable<Domain.TestDemoModel> GetAllTestDemoModels()
        {
            return _context.TestDemoModels.AsNoTracking().AsQueryable();
        }

        public Domain.TestDemoModel GetTestDemoModelById(Guid id)
        {
            return _context.TestDemoModels
                .AsNoTracking()
                .Where(a => a.Id == id)
                .FirstOrDefault();

        }
    }
}
