
using System;

namespace POS_system
{
    public struct CurrencyDenomination
    {
        public double Denomination { get; set; }
        public int Count { get; set; }
        public override string ToString()
        {
            return $"{Denomination} - {Count}";
        }
    }
}
