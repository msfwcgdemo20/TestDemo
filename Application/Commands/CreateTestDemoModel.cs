using TestDemo.Domain;
using MFoundation.Core.Messaging.Commands;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TestDemo.Application.Commands
{
    public class CreateTestDemoModel : ICommand
    {
        public string Name { get; private set; }

        public CreateTestDemoModel()
        {
        }

        [JsonConstructor]
        public CreateTestDemoModel(string name)
        {
            Name = name;
        }
    }
}
