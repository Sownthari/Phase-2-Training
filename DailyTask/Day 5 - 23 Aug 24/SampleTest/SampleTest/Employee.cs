using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleTest
{
    public class Employee
    {
        string Name;
        int Age;
        public Employee(string nme, int age)
        {
            Name = nme;
            Age = age;
        }
        public string name
        {
            get
            {
                return Name;
            }
            set
            {
                Name = value;
            }
        }
        public int age
        {
            get
            {
                return Age;
            }
            set { Age = value; }
        }

    }
}
