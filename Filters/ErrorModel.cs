using System;

namespace TestDemo.Filters
{
    internal class ErrorModel
    {
        public Guid CorrelationId { get; set; }
        public string ErrorCode { get; set; }
        public string Message { get; set; }
        public string Detail { get; set; }
        public string HelpUrl { get; set; }
    }
}