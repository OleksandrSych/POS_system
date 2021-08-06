using Microsoft.VisualStudio.TestTools.UnitTesting;
using POS_system;
using System;
using System.Collections.Generic;
using System.Text;

namespace POS_system.Tests
{
    [TestClass()]
    public class POS_terminalTests
    {
        [TestMethod()]
        public void IsThereEnoughMoney_GetExceptionTest()
        {
            DenominationBillsAndCoins billsAndCoins = new DenominationBillsAndCoins();
            billsAndCoins.AddNewDenomination(0.01);
            POS_terminal pOS_Terminal = new POS_terminal(billsAndCoins);
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => pOS_Terminal.SetPrice = -0.001);            
        }

        [TestMethod()]
        public void IsThereEnoughMoney_PassTest()
        {
            DenominationBillsAndCoins billsAndCoins = new DenominationBillsAndCoins();
            billsAndCoins.AddNewDenomination(0.01);
            POS_terminal pOS_Terminal = new POS_terminal(billsAndCoins);
            pOS_Terminal.SetPrice = 0.1;
            Assert.IsNotNull(pOS_Terminal); 
        }

        [TestMethod()]
        public void IsThereEnoughMoney_True_Price10Money11Test()
        {
            DenominationBillsAndCoins billsAndCoins = new DenominationBillsAndCoins();
            POS_terminal pOS_Terminal = new POS_terminal(billsAndCoins);
            pOS_Terminal.SetPrice = 10;
            pOS_Terminal.SetBillsAndCoins = new List<CurrencyDenomination>
                { new CurrencyDenomination {Denomination = 11, Count =1 } };
            Assert.IsTrue(pOS_Terminal.IsThereEnoughMoney());
        }

        [TestMethod()]
        public void IsThereEnoughMoney_True_Price10Money10Test()
        {
            DenominationBillsAndCoins billsAndCoins = new DenominationBillsAndCoins();
            POS_terminal pOS_Terminal = new POS_terminal(billsAndCoins);
            pOS_Terminal.SetPrice = 10;
            pOS_Terminal.SetBillsAndCoins = new List<CurrencyDenomination>
                { new CurrencyDenomination {Denomination = 10, Count =1 } };
            Assert.IsTrue(pOS_Terminal.IsThereEnoughMoney());
        }

        [TestMethod()]
        public void IsThereEnoughMoney_False_Price10Money9Test()
        {
            DenominationBillsAndCoins billsAndCoins = new DenominationBillsAndCoins();
            POS_terminal pOS_Terminal = new POS_terminal(billsAndCoins);
            pOS_Terminal.SetPrice = 10;
            pOS_Terminal.SetBillsAndCoins = new List<CurrencyDenomination>
                { new CurrencyDenomination {Denomination = 9, Count =1 } };
            Assert.IsFalse(pOS_Terminal.IsThereEnoughMoney());
        }

        [TestMethod()]
        public void GetAssumption_AssumptionBillsCount0Test()
        {
            DenominationBillsAndCoins billsAndCoins = new DenominationBillsAndCoins();
            billsAndCoins.AddNewDenomination(0.01);
            POS_terminal pOS_Terminal = new POS_terminal(billsAndCoins)
            {
                SetPrice = 10,
                SetBillsAndCoins = new List<CurrencyDenomination>
                { new CurrencyDenomination {Denomination = 10, Count =1 } }
            };
            Assert.AreEqual(pOS_Terminal.GetAssumption().Count, 0);
        }

        [TestMethod()]
        public void GetAssumption_AssumptionCount1Test()
        {
            DenominationBillsAndCoins billsAndCoins = new DenominationBillsAndCoins();
            billsAndCoins.AddNewDenomination(1);
            POS_terminal pOS_Terminal = new POS_terminal(billsAndCoins)
            {
                SetPrice = 1,
                SetBillsAndCoins = new List<CurrencyDenomination>
                { new CurrencyDenomination {Denomination = 11, Count =1 } }
            };
            Assert.AreEqual(pOS_Terminal.GetAssumption().Count, 1);
        }

        [TestMethod()]
        public void GetAssumption_AssumptionBillsCount10Test()
        {
            DenominationBillsAndCoins billsAndCoins = new DenominationBillsAndCoins();
            billsAndCoins.AddNewDenomination(1);
            POS_terminal pOS_Terminal = new POS_terminal(billsAndCoins)
            {
                SetPrice = 1,
                SetBillsAndCoins = new List<CurrencyDenomination>
                { new CurrencyDenomination {Denomination = 11, Count =1 } }
            };
            Assert.AreEqual(pOS_Terminal.GetAssumption()[0].Count, 10);
        }

        [TestMethod()]
        public void GetAssumption_GetExceptionTest()
        {
            DenominationBillsAndCoins billsAndCoins = new DenominationBillsAndCoins();
            billsAndCoins.AddNewDenomination(10);
            POS_terminal pOS_Terminal = new POS_terminal(billsAndCoins)
            {
                SetPrice = 1,
                SetBillsAndCoins = new List<CurrencyDenomination>
                { new CurrencyDenomination {Denomination = 5, Count =1 } }
            };
            Assert.ThrowsException<ArgumentException>(() => pOS_Terminal.GetAssumption());

        }
    }
}