using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeChallenge.Models;

namespace CodeChallenge.Services
{
    public interface IReportingStructureService
    {
        ReportingStructure GetByEmployeeId(string id);
    }
}