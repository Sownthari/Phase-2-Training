// See https://aka.ms/new-console-template for more information
using Week1;

Console.WriteLine("Enter details");
Console.WriteLine("Enter the type of Employee: ");
string? type = Console.ReadLine();
if(type.Equals("permanent", StringComparison.CurrentCultureIgnoreCase))
{
    PermanentEmployee pe = new PermanentEmployee();
    Console.WriteLine("Enter Employee Id: ");
    pe.Id = Convert.ToInt32(Console.ReadLine());
    Console.WriteLine("Enter Employee Name: ");
    pe.Name = Console.ReadLine();
    Console.WriteLine("Enter Basic Salary: ");
    pe.BasicSalary = Convert.ToInt32(Console.ReadLine());
    Console.WriteLine("Enter PF: ");
    pe.Pf = Convert.ToInt32(Console.ReadLine());
    pe.DisplayDetails();
}
else if (type.Equals("temporary", StringComparison.CurrentCultureIgnoreCase))
{
    TemporaryEmployee te = new TemporaryEmployee();
    Console.WriteLine("Enter Employee Id: ");
    te.Id = Convert.ToInt32(Console.ReadLine());
    Console.WriteLine("Enter Employee Name: ");
    te.Name = Console.ReadLine();
    Console.WriteLine("Enter Daily Wages: ");
    te.DailyWages = Convert.ToInt32(Console.ReadLine());
    Console.WriteLine("Enter No of days worked: ");
    te.NoOfDays = Convert.ToInt32(Console.ReadLine());
    te.DisplayDetails();

}
else
{
    Console.WriteLine("Invalid employee type :(");
}
