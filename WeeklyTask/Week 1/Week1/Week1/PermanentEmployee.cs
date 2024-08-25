using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week1
{
    public class PermanentEmployee : Employee
    {
        public int Pf {  get; set; }
        public float BasicSalary { get; set; }


        public override float CalculateSalary()
        {
            this.NetSalary = BasicSalary - Pf;
            return this.NetSalary;
        }
        public override float CalculateBonus()
        {
            if (Pf < 1000)
            {
                this.Bonus = (float)(BasicSalary * 0.10);
            }

            else if (Pf >= 1000 && Pf < 1500)
            {
                this.Bonus = (float)(BasicSalary * 0.115);
            }

            else if (Pf >= 1500 && Pf < 1800)
            {
                this.Bonus = (float)(BasicSalary * 0.12);
            }
            else 
            {
                this.Bonus = (float)(BasicSalary * 0.15);
            }

            return this.Bonus;
        }

        public override void DisplayDetails()
        {
            Console.WriteLine("The details are:");
            Console.WriteLine($"Employee Name: {this.Name}");
            Console.WriteLine($"Employee Id: {this.Id}");
            Console.WriteLine($"Basic Salary: {BasicSalary}");
            Console.WriteLine($"PF: {Pf}");
            Console.WriteLine($"Bonus: {CalculateBonus()}");
            Console.WriteLine($"Net Salary: {CalculateSalary()}");

        }


    }
}
