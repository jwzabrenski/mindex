using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeChallenge.Models;
using Microsoft.Extensions.Logging;
using CodeChallenge.Repositories;

namespace CodeChallenge.Services
{
    public class ReportingStructureService : IReportingStructureService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ILogger<EmployeeService> _logger;

        public ReportingStructureService(ILogger<EmployeeService> logger, IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
            _logger = logger;
        }

        public ReportingStructure GetByEmployeeId(string id)
        {
            if(!String.IsNullOrEmpty(id))
            {
                var employee = _employeeRepository.GetById(id);
                if (employee != null) return new ReportingStructure(employee);
            }

            return null;
        }
    }
}