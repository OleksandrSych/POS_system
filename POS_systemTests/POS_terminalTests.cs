using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using POS_system;

namespace POS_systemTests
{
    [TestClass()]
    public class PosTerminalTests
    {
        [TestMethod()]
        public void IsThereEnoughMoney_GetExceptionTest()
        {
            var billsAndCoins = new DenominationBillsAndCoins();
            billsAndCoins.AddNewDenomination(0.01);
            var posTerminal = new PosTerminal(billsAndCoins);
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => posTerminal.SetPrice = -0.001);
        }

        [TestMethod()]
        public void IsThereEnoughMoney_PassTest()
        {
            var billsAndCoins = new DenominationBillsAndCoins();
            billsAndCoins.AddNewDenomination(0.01);
            var posTerminal = new PosTerminal(billsAndCoins) { SetPrice = 0.1 };
            Assert.IsNotNull(posTerminal);
        }

        [TestMethod()]
        public void IsThereEnoughMoney_True_Price10Money11Test()
        {
            var billsAndCoins = new DenominationBillsAndCoins();
            var posTerminal = new PosTerminal(billsAndCoins)
            {
                SetPrice = 10,
                SetBillsAndCoins = new List<CurrencyDenomination>
                {
                    new CurrencyDenomination {Denomination = 11, Count = 1}
                }
            };
            Assert.IsTrue(posTerminal.IsThereEnoughMoney());
        }

        [TestMethod()]
        public void IsThereEnoughMoney_True_Price10Money10Test()
        {
            var billsAndCoins = new DenominationBillsAndCoins();
            var posTerminal = new PosTerminal(billsAndCoins)
            {
                SetPrice = 10,
                SetBillsAndCoins = new List<CurrencyDenomination>
                {
                    new CurrencyDenomination {Denomination = 10, Count = 1}
                }
            };
            Assert.IsTrue(posTerminal.IsThereEnoughMoney());
        }

        [TestMethod()]
        public void IsThereEnoughMoney_False_Price10Money9Test()
        {
            var billsAndCoins = new DenominationBillsAndCoins();
            var posTerminal = new PosTerminal(billsAndCoins)
            {
                SetPrice = 10,
                SetBillsAndCoins = new List<CurrencyDenomination>
                {
                    new CurrencyDenomination {Denomination = 9, Count = 1}
                }
            };
            Assert.IsFalse(posTerminal.IsThereEnoughMoney());
        }

        [TestMethod()]
        public void GetAssumption_AssumptionBillsCount0Test()
        {
            var billsAndCoins = new DenominationBillsAndCoins();
            billsAndCoins.AddNewDenomination(0.01);
            var posTerminal = new PosTerminal(billsAndCoins)
            {
                SetPrice = 10,
                SetBillsAndCoins = new List<CurrencyDenomination>
                { new CurrencyDenomination {Denomination = 10, Count =1 } }
            };
            Assert.AreEqual(posTerminal.GetAssumption().Count, 0);
        }

        [TestMethod()]
        public void GetAssumption_AssumptionCount1Test()
        {
            var billsAndCoins = new DenominationBillsAndCoins();
            billsAndCoins.AddNewDenomination(1);
            var posTerminal = new PosTerminal(billsAndCoins)
            {
                SetPrice = 1,
                SetBillsAndCoins = new List<CurrencyDenomination>
                { new CurrencyDenomination {Denomination = 11, Count =1 } }
            };
            Assert.AreEqual(posTerminal.GetAssumption().Count, 1);
        }

        [TestMethod()]
        public void GetAssumption_AssumptionBillsCount10Test()
        {
            var billsAndCoins = new DenominationBillsAndCoins();
            billsAndCoins.AddNewDenomination(1);
            var posTerminal = new PosTerminal(billsAndCoins)
            {
                SetPrice = 1,
                SetBillsAndCoins = new List<CurrencyDenomination>
                { new CurrencyDenomination {Denomination = 11, Count =1 } }
            };
            Assert.AreEqual(posTerminal.GetAssumption()[0].Count, 10);
        }

        [TestMethod()]
        public void GetAssumption_GetExceptionTest()
        {
            var billsAndCoins = new DenominationBillsAndCoins();
            billsAndCoins.AddNewDenomination(10);
            var posTerminal = new PosTerminal(billsAndCoins)
            {
                SetPrice = 1,
                SetBillsAndCoins = new List<CurrencyDenomination>
                { new CurrencyDenomination {Denomination = 5, Count =1 } }
            };
            Assert.ThrowsException<ArgumentException>(() => posTerminal.GetAssumption());

        }
    }
}