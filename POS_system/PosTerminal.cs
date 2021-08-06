using System;
using System.Collections.Generic;
using System.Linq;

namespace POS_system
{
    /// <summary>
    /// POS system can to calculate the correct change and display the optimum (i.e. minimum) number 
    /// of bills and coins to return to the customer based on the price 
    /// and the bills or coins deposited by the customer 
    /// </summary>
    public class PosTerminal
    {
        readonly DenominationBillsAndCoins _billsAndCoins;
        double _price;
        double _userMoney;
        public PosTerminal(DenominationBillsAndCoins billsAndCoins)
        {
            this._billsAndCoins = billsAndCoins;
        }
        /// <summary>
        /// The optimum (i.e. minimum) number of bills and coins to return to the customer
        /// </summary>
        public List<CurrencyDenomination> GetAssumption()
        {
            var assumption = new List<CurrencyDenomination>();
            var assumptionMoney = Math.Round(_userMoney - _price, 5);
            var money = assumptionMoney;
            var denominationBillsAndCoins = _billsAndCoins.GetDenominations
                .Where(x => x <= money && x > 0)
                .OrderByDescending(x => x);
            foreach (var bill in denominationBillsAndCoins)
            {
                if (assumptionMoney == 0)
                    break;
                if (!(assumptionMoney >= bill)) continue;
                var countBills = (int)Math.Floor(assumptionMoney / bill);
                assumption.Add(new CurrencyDenomination { Denomination = bill, Count = countBills });
                assumptionMoney = Math.Round(assumptionMoney - countBills * bill, 5);
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
                    throw new ArgumentOutOfRangeException($"The price '{value}' cannot be less than 0");
                }
                _price = value;
            }
        }

        public List<CurrencyDenomination> SetBillsAndCoins
        {
            set
            {
                if (value.Count(x => x.Denomination < 0 || x.Count <= 0) > 0)
                {
                    throw new ArgumentOutOfRangeException($"The bill cannot be less than 0");
                }
                _userMoney = value.Sum(x => x.Denomination * x.Count);
            }
        }
        public bool IsThereEnoughMoney()
        {
            return _userMoney >= _price;
        }
    }
}
