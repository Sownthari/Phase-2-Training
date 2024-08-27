// See https://aka.ms/new-console-template for more information
using ADOApp;
using System.Data.SqlClient;



SqlQuery sql = new SqlQuery();


string c;

do
{
    Console.WriteLine("Enter the choice \n1.Insert \n2.Display \n3.Update \n4.Delete");
    int choice = Convert.ToInt32(Console.ReadLine());

    switch (choice)
    {
        case 1:
            sql.Insert();
            break;
        case 2:
            sql.Retrieve();
            break;
        case 3:
            sql.Update();
            break;
        case 4:
            sql.Delete();
            break;
    }

    Console.WriteLine("Do you want to continue Yes or No");
    c = Console.ReadLine();
}
while (c.ToLower().Equals("yes"));
