using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestDemo.Infrastructure.EntityConfigurations
{
    internal class TestDemoModelEntityTypeConfiguration : IEntityTypeConfiguration<Domain.TestDemoModel>
    {
        public void Configure(EntityTypeBuilder<Domain.TestDemoModel> builder)
        {
            builder.ToTable("TestDemoModel", TestDemoContext.DEFAULT_SCHEMA);

            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).IsRequired();
            builder.Property(a => a.Name).IsRequired().HasMaxLength(50);


            builder.Ignore(a => a.UncommittedEvents);
        }
    }
}
