using MFoundation.Core.Aggregates;
using System;

namespace TestDemo.Domain
{
    public class TestDemoModel : AggregateRoot
    {
        public string Name { get; private set; }
        public TestDemoModel()
        {
            Id = Guid.NewGuid();
        }

        public TestDemoModel(string name) : this()
        {
            Name = name;
        }
    }
}
