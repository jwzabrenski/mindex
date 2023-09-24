using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using CodeChallenge.Models;

namespace CodeChallenge.Models
{
    public class Compensation
    {        
        public string CompensationId {get; set; }
        
        [ForeignKey("Employee")]
        public string EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public DateTime EffectiveDate { get; set; }
        public decimal Salary { get; set; }       
    }
}