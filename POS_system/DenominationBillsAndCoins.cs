using System;
using System.Collections.Generic;

namespace POS_system
{
    /// <summary>
    /// Country's  denomination of bills and coins
    /// </summary>
    public class DenominationBillsAndCoins
    {
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
                    this.GetDenominations.Add(bill);
                }
                else
                {
                    throw new ArgumentOutOfRangeException($"Incorrect denomination. Denomination '{bill}' must be more than 0.");
                }
            }
        }

        public List<double> GetDenominations { get; } = new List<double>();
    }
}
