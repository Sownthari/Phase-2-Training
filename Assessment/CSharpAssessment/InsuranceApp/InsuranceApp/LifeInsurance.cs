using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceApp
{
    public class LifeInsurance : Insurance
    {
        private int policyTerm;
        private float benefitPercent;

        public int PolicyTerm
        {
            get
            {
                return policyTerm;
            }
            set
            {
                policyTerm = value;
            }
        }

        public float BenefitPercent
        {
            get
            {
                return benefitPercent;
            }
            set
            {
                benefitPercent = value;
            }
        }

        public double CalculatePremium()
        {
            return (AmountCovered - ((AmountCovered * BenefitPercent) / 100)) / PolicyTerm;
            //return AmountCovered -((AmountCovered * BenefitPercent) / 100) /PolicyTerm;
        }
    }
}
