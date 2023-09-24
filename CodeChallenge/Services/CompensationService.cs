using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeChallenge.Services;
using CodeChallenge.Models;
using Microsoft.Extensions.Logging;
using CodeChallenge.Repositories;

namespace CodeChallenge.Services
{
    public class CompensationService : ICompensationService
    {
        private readonly IEmployeeService _employeeService;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ICompensationRepository _compensationRepository;
        private readonly ILogger<CompensationService> _logger;

        public CompensationService(ILogger<CompensationService> logger, IEmployeeRepository employeeRepository, ICompensationRepository compensationRepository)
        {
            _employeeRepository = employeeRepository;
            _compensationRepository = compensationRepository;
            _logger = logger;
        }

        public Compensation Create(Compensation compensation)
        {
            if(compensation != null)
            {
                _compensationRepository.Add(compensation);
                _compensationRepository.SaveAsync().Wait();
            }

            return compensation;
        }

        public Compensation GetByEmployeeId(string employeeId)
        {
            if(!String.IsNullOrEmpty(employeeId))
            {
                return _compensationRepository.GetByEmployeeId(employeeId);
            }

            return null;
        }
    }
}