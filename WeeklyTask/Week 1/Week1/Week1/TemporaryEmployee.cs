using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week1
{
    public class TemporaryEmployee : Employee
    {
        public int DailyWages { get; set; }
        public int NoOfDays { get; set; }

        public override float CalculateSalary()
        {
            this.NetSalary = DailyWages * NoOfDays;
            return this.NetSalary;
        }
        public override float CalculateBonus()
        {
            if (DailyWages < 1000)
            {
                this.Bonus = (float)(this.NetSalary * 0.15);
            }

            else if (DailyWages >= 1000 && DailyWages < 1500)
            {
                this.Bonus = (float)(this.NetSalary * 0.12);
            }

            else if (DailyWages >= 1500 && DailyWages < 1750)
            {
                this.Bonus = (float)(this.NetSalary * 0.11);
            }
            else
            {
                this.Bonus = (float)(this.NetSalary * 0.08);
            }

            return this.Bonus;
        }

        public override void DisplayDetails()
        {
            Console.WriteLine("The details are:");
            Console.WriteLine($"Employee Name: {this.Name}");
            Console.WriteLine($"Employee Id: {this.Id}");
            Console.WriteLine($"Daily Wages: {DailyWages}");
            Console.WriteLine($"No Of days worked: {NoOfDays}");
            Console.WriteLine($"Net Salary: {CalculateSalary()}");
            Console.WriteLine($"Bonus: {CalculateBonus()}");

        }
    }
}
