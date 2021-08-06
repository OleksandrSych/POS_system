using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace POS_system
{
    /// <summary>
    /// POS system can to calculate the correct change and display the optimum (i.e. minimum) number 
    /// of bills and coins to return to the customer based on the price 
    /// and the bills or coins deposited by the customer 
    /// </summary>
    public class POS_terminal
    {
        readonly DenominationBillsAndCoins billsAndCoins;
        double price;
        double userMoney;
        public POS_terminal(DenominationBillsAndCoins billsAndCoins)
        {
            this.billsAndCoins = billsAndCoins;
        }
        /// <summary>
        /// The optimum (i.e. minimum) number of bills and coins to return to the customer
        /// </summary>
        public List<CurrencyDenomination> GetAssumption()
        {
            List<CurrencyDenomination> assumption = new List<CurrencyDenomination>();
            var assumptionMoney = Math.Round(userMoney - price, 5);
            var denominationBillsAndCoins = billsAndCoins.GetDenominations
                .Where(x => x <= assumptionMoney && x > 0)
                .OrderByDescending(x => x);
            foreach (var bill in denominationBillsAndCoins)
            {
                if (assumptionMoney == 0)
                    break;
                if (assumptionMoney >= bill)
                {
                    int counBillst = (int)Math.Floor(assumptionMoney / bill);
                    assumption.Add(new CurrencyDenomination { Denomination = bill, Count = counBillst });
                    assumptionMoney = Math.Round(assumptionMoney - counBillst * bill, 5);
                }
            }
            if (assumptionMoney == 0)
            {
                return assumption;
            }
            else
            {
                throw new ArgumentException("There is no way to give change with current bills and coins!");
            }
        }
        public double SetPrice
        {
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("The price cannot be less than 0");
                }
                price = value;
            }
        }

        public List<CurrencyDenomination> SetBillsAndCoins
        {
            set
            {
                if (value.Where(x => x.Denomination < 0 || x.Count <= 0).Count() > 0)
                {
                    throw new ArgumentOutOfRangeException("The bill cannot be less than 0");
                }
                userMoney = value.Sum(x => x.Denomination * x.Count);
            }
        }
        public bool IsThereEnoughMoney()
        {
            return userMoney >= price;
        }
    }
}
