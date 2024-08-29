
using InsuranceApp;

internal class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Insurance Number :");
        string iNo = Console.ReadLine();
        Console.WriteLine("Insurance Name :");
        string iName = Console.ReadLine();
        Console.WriteLine("Amount Covered :");
        double amount = Convert.ToDouble(Console.ReadLine());

        Console.WriteLine("Select \n 1.Life Insurance \n 2.Motor Insurance");
        int choice = Convert.ToInt32(Console.ReadLine());

        if(choice == 1)
        {
            LifeInsurance li = new LifeInsurance();
            li.InsuranceNo = iNo;
            li.InsuranceName = iName;
            li.AmountCovered = amount;
            AddPolicy(li, choice);
        }

        else if(choice == 2)
        {
            MotorInsurance mi = new MotorInsurance();
            mi.InsuranceNo = iNo;
            mi.InsuranceName = iName;
            mi.AmountCovered = amount;
            AddPolicy(mi, choice);
        }
        else
        {
            Console.WriteLine("Invalid Choice");
        }
    }

    public static void AddPolicy(Insurance ins, int opt)
    {
        if (opt == 1)
        {
            LifeInsurance lif = (LifeInsurance)ins;

            Console.WriteLine("Policy Term : ");
            lif.PolicyTerm = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Benefit Percent: ");
            lif.BenefitPercent = float.Parse(Console.ReadLine());
            Console.WriteLine("Calculated Premium: " + lif.CalculatePremium());
        }
        else if (opt == 2)
        {
            MotorInsurance mti = (MotorInsurance)ins;
            
            Console.WriteLine("Depreciation Percent : ");
            mti.DepPercent = float.Parse(Console.ReadLine());
            Console.WriteLine("Calculated Premium: " + mti.CalculatePremium());
        }
    }
    }


