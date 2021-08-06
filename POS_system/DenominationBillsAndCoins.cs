using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace POS_system
{
    /// <summary>
    /// Country's  denomination of bills and coins
    /// </summary>
    public class DenominationBillsAndCoins
    {
        List<double> denominations = new List<double>();
        public DenominationBillsAndCoins(params double[] denominations)
        {
            AddDenominations(denominations);
        }

        public void AddNewDenomination(params double[] denomination)
        {
            AddDenominations(denomination);
        }
        private void AddDenominations(params double[] denominations)
        {
            foreach (var bill in denominations)
            {
                if (bill > 0)
                {
                    this.denominations.Add(bill);
                }
                else
                {
                    throw new Exception("Incorrect denomination. Denomination must be more than 0.");
                }
            }
        }

        public List<double> GetDenominations
        {
            get
            {
                return denominations;
            }
        }
    }
}
