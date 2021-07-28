using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using TestDemo.Application.Commands;
using TestDemo.Application.Queries;
using MFoundation.Core.Messaging.Commands;
using MFoundation.Core.Messaging.Queries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace TestDemo.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class TestDemosController : ControllerBase
    {
        private readonly ILogger<TestDemosController> _logger;
        private readonly ICommandBus _commandBus;
        private readonly IQueryBus _queryBus;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="commandBus"></param>
        /// <param name="queryBus"></param>
        public TestDemosController(ILogger<TestDemosController> logger,
            ICommandBus commandBus,
            IQueryBus queryBus)
        {
            _logger = logger;
            _commandBus = commandBus;
            _queryBus = queryBus;
        }

        /// <summary>
        /// Gets Service Name
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("/ServiceHealth")]
        public IActionResult ServiceHealth()
        {
            _logger.LogInformation("Get Service Name.");
             // return Ok("This TestDemos Project Service API.");
            return Ok("Service is Running...");
}


/// <summary>
/// Gets all TestDemoModels defined in the application.
/// </summary>
/// <returns></returns>
[HttpGet]
        public async Task<ActionResult<IEnumerable<Domain.TestDemoModel>>> GetTestDemos()
        {
            IEnumerable<Domain.TestDemoModel> result = await _queryBus.Send<GetAllTestDemoModels, IEnumerable<Domain.TestDemoModel>>(new GetAllTestDemoModels());
            if (result == null || result.Count() < 1)
            {
                return NotFound();
            }
            return Ok(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<ActionResult<Domain.TestDemoModel>> GetTestDemoById([FromRoute]Guid id)
        {
            Domain.TestDemoModel result = await _queryBus.Send<GetTestDemoModelById, Domain.TestDemoModel>(new GetTestDemoModelById(id));
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        /// <summary>
        /// This method creates new Custom Attribute.
        /// </summary>
        /// <param name="createTestDemoModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddTestDemo([FromBody]CreateTestDemoModel createTestDemoModel)
        {
            _logger.LogInformation("Create TestDemo request received.");
            await _commandBus.Send(createTestDemoModel);
            return Ok();
        }

        /// <summary>
        /// Delete Cusom Attribute
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<ActionResult<bool>> DeleteTestDemoById([FromRoute]Guid id)
        {
            await _commandBus.Send<DeleteTestDemoModel, bool>(new DeleteTestDemoModel(id));
            return Ok();
        }
    }
}
