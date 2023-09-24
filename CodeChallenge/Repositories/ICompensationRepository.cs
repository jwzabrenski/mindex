using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeChallenge.Models;

namespace CodeChallenge.Repositories
{
    public interface ICompensationRepository
    {
        Compensation Add(Compensation compensation);
        Compensation GetByEmployeeId(string employeeId);
        Task SaveAsync();
    }
}