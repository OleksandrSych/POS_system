using Microsoft.VisualStudio.TestTools.UnitTesting;
using POS_system;
using System;
using System.Collections.Generic;
using System.Text;

namespace POS_system.Tests
{
    [TestClass()]
    public class ProgramTests
    {
        [TestMethod()]
        public void ValidatePrice_Input_Test_Throws_Exception()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => Program.ValidatePrice("test", out _));
        }
        [TestMethod()]
        public void ValidatePrice_Input_Null_Throws_Exception()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => Program.ValidatePrice(null, out _));
        }
        [TestMethod()]
        public void ValidatePrice_Input_minus5_Throws_Exception()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => Program.ValidatePrice("-5", out _));
        }
        [TestMethod()]
        public void ValidatePrice_Input_5_return_5()
        {
            Program.ValidatePrice("5", out var price);
            Assert.AreEqual(5.0, price);
        }
        [TestMethod()]
        public void ValidatePrice_Input_0_5_return_0_5()
        {
            Program.ValidatePrice("0.5", out var price);
            Assert.AreEqual(0.5, price);
        }

        [TestMethod()]
        public void ValidateUserBillsAndCoins_Input_0_5_return_0_5_and_1()
        {
            Program.ValidateUserBillsAndCoins("0.5", out var price);
            Assert.AreEqual(1, price.Count);
            Assert.AreEqual(1, price[0].Count);
            Assert.AreEqual(0.5, price[0].Denomination);
        }

        [TestMethod()]
        public void ValidateUserBillsAndCoins_Input_minus5_Throws_Exception()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(
                () => Program.ValidateUserBillsAndCoins("-5", out var price));
        }

        [TestMethod()]
        public void ValidateUserBillsAndCoins_Input_Text_Throws_Exception()
        {
            Assert.ThrowsException<ArgumentException>(
                () => Program.ValidateUserBillsAndCoins("Text", out var price));
        }

        [TestMethod()]
        public void ValidateUserBillsAndCoins_Input_Null_Throws_Exception()
        {
            Assert.ThrowsException<NullReferenceException>(
                () => Program.ValidateUserBillsAndCoins(null, out var price));
        }

        [TestMethod()]
        public void OutputToConsoleTest()
        {
            Program.OutputToConsole(new CurrencyDenomination[]{new CurrencyDenomination(){Denomination = 1, Count = 1}});
            Assert.IsTrue(true);
        }
    }
}