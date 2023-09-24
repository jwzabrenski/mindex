using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CodeChallenge.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CodeChallenge.Models;

namespace CodeChallenge.Controllers
{
    [ApiController]
    [Route("api/compensation")]
    public class CompensationController : ControllerBase
    {
        private readonly ILogger<CompensationController> _logger;
        private readonly ICompensationService _compensationService;

        public CompensationController(ILogger<CompensationController> logger, ICompensationService compensationService)
        {
            _logger = logger;
            _compensationService = compensationService;
        }
        
        [HttpPost]
        public IActionResult CreateCompensation([FromBody] Compensation compensation)
        {
            _logger.LogDebug($"Received compensation create request for {compensation.EmployeeId}");

            _compensationService.Create(compensation);

            return CreatedAtRoute("getCompensationByEmployeeId", new { id = compensation.EmployeeId }, compensation);
        }

        [HttpGet("{id}", Name = "getCompensationByEmployeeId")]
        public IActionResult GetCompensationByEmployeeId(String id)
        {
            _logger.LogDebug($"Received employee get request for '{id}'");

            var compensation = _compensationService.GetByEmployeeId(id);

            if (compensation == null)
                return NotFound();

            return Ok(compensation);
        }
    }
}