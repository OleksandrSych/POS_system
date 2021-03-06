using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Resources;

namespace POS_system
{
    public class Program
    {
        private static readonly DenominationBillsAndCoins BillsAndCoins;
        static Program()
        {
            BillsAndCoins = new DenominationBillsAndCoins();
            BillsAndCoins.AddNewDenomination(
                Properties.Resources.DenominationRes
                     .Split(", ")
                     .Select(double.Parse)
                     .ToArray()
                );
        }
        static void Main(string[] args)
        {
            while (true)
            {
                try
                {
                    Console.Write("Price: ");
                    ValidatePrice(Console.ReadLine(), out var price);
                    Console.Write("Bills And Coins: ");
                    ValidateUserBillsAndCoins(Console.ReadLine(), out var userBillsAndCoins);

                    var posTerminal = new PosTerminal(BillsAndCoins)
                    {
                        SetPrice = price,
                        SetBillsAndCoins = userBillsAndCoins
                    };
                    if (posTerminal.IsThereEnoughMoney())
                    {
                        OutputToConsole(posTerminal.GetAssumption());
                    }
                    else
                    {
                        Console.WriteLine("Not enough money contributed");
                    }
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                }
                Console.WriteLine("Please, press any key to start again!");
                Console.ReadKey();
                Console.Clear();
            }
        }

        public static void ValidatePrice(string inputPrice, out double price)
        {
            if (!double.TryParse(inputPrice, out price))
            {
                throw new ArgumentOutOfRangeException($"The price '{price}' is not number");
            }
            if (price < 0)
            {
                throw new ArgumentOutOfRangeException($"The price '{price}' cannot be less than 0");
            }
        }

        public static void ValidateUserBillsAndCoins(string inputPrice, out List<CurrencyDenomination> userBillsAndCoins)
        {
            var billsAndCoins = new List<double>();
            foreach (var bill in inputPrice.Split(","))
            {
                if (double.TryParse(bill, out var userBill))
                {
                    if (userBill > 0)
                    {
                        billsAndCoins.Add(userBill);
                    }
                    else
                    {
                        throw new ArgumentOutOfRangeException($"The bill '{userBill}' cannot be less than 0");
                    }
                }
                else
                {
                    throw new ArgumentException("Incorrect bill");
                }
            }
            userBillsAndCoins = billsAndCoins.GroupBy(x => x)
                    .Select(x => new CurrencyDenomination { Denomination = x.Key, Count = x.Count() })
                    .ToList();
        }
        public static void OutputToConsole(IEnumerable<CurrencyDenomination> assumptions)
        {
            Console.WriteLine("\nNumber of bills and coins to return to the customer:");
            foreach (var assumption in assumptions)
            {
                Console.WriteLine(assumption);
            }
        }
    }
}
