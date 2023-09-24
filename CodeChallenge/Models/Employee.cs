using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CodeChallenge.Models
{
    public class Employee
    {        
        public String EmployeeId { get; set; }        
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Position { get; set; }
        public String Department { get; set; }
        [ForeignKey("Compensation")]
        public string CompensationId {get; set; }        
        public Compensation Compensation { get; set; }
        public List<Employee> DirectReports { get; set; }
    }
}
