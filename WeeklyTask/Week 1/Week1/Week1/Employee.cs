using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week1
{
    public abstract class Employee
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public float Bonus {  get; set; }
        public float NetSalary { get; set; }
        public abstract float CalculateSalary();
        public abstract float CalculateBonus();
        public abstract void DisplayDetails();

    }
}
