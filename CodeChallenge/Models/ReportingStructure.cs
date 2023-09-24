using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeChallenge.Models
{
    public class ReportingStructure
    {
        public Employee Employee { get; set; }
        public int NumberOfReports => CountTreeNodes(Employee.DirectReports);

        public ReportingStructure(Employee employee)
        {
            Employee = employee;
        }

        public int CountTreeNodes(List<Employee> nodes)
        {            
            if (nodes != null && nodes.Any())
            {
                int count = nodes.Count;

                foreach (Employee node in nodes)
                {
                    if (node.DirectReports != null) count += CountTreeNodes(node.DirectReports);
                }

                return count;
            }

            return 0;        
        }
    }
}