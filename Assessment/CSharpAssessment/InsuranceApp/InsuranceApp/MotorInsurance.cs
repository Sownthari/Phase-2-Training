using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceApp 
{
    public class MotorInsurance : Insurance
    {
        private double idv;
        private float depPercent;

        public double Idv
        {
            get
            {
                return idv;
            }
            set
            {
                idv = value;
            }
        }

        public float DepPercent
        {
            get
            {
                return depPercent;
            }
            set
            {
                depPercent = value;
            }
        }

        public double CalculatePremium()
        {
            Idv = AmountCovered -((AmountCovered * DepPercent) /100);

            return Idv * 0.03;
        }
    }
}
