using Microsoft.VisualStudio.TestTools.UnitTesting;
using POS_system;
using System;
using System.Collections.Generic;
using System.Text;

namespace POS_system.Tests
{
    [TestClass()]
    public class DenominationBillsAndCoinsTests
    {
        [TestMethod()]
        public void DenominationBillsAndCoins_denominations_minus5_ThrowsException_Test()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(
                () => new DenominationBillsAndCoins(-5));
        }
    }
}