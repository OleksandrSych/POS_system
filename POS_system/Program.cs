using System;
using System.Collections.Generic;
using System.Linq;

namespace POS_system
{
    class Program
    {
        static readonly DenominationBillsAndCoins billsAndCoins;
        static Program()
        {
            billsAndCoins = new DenominationBillsAndCoins();
            billsAndCoins.AddNewDenomination(0.01, 0.05, 0.10, 0.25, 0.50, 1.00, 2.00, 5.00, 10.00, 20.00, 50.00, 100.00);
        }
        static void Main(string[] args)
        {
            while (true)
            {
                try
                {
                    Console.Write("Price: ");
                    ValidatePrice(Console.ReadLine(), out double price);
                    Console.Write("Bills And Coins: ");
                    ValidateUserBillsAndCoins(Console.ReadLine(), out List<CurrencyDenomination> userBillsAndCoins);

                    POS_terminal pOS_Terminal = new POS_terminal(billsAndCoins)
                    {
                        SetPrice = price,
                        SetBillsAndCoins = userBillsAndCoins
                    };
                    if (pOS_Terminal.IsThereEnoughMoney())
                    {
                        OutputToConsoleAassumption(pOS_Terminal.GetAssumption());
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
                Console.ReadLine();
                Console.Clear();
            }
        }

        static void ValidatePrice(string inputPrice, out double price)
        {
            if (!double.TryParse(inputPrice, out price))
            {
                throw new ArgumentException("Incorret Price");
            }
            if (price < 0)
            {
                throw new ArgumentOutOfRangeException("The price cannot be less than 0");
            }
        }

        static void ValidateUserBillsAndCoins(string inputPrice, out List<CurrencyDenomination> userBillsAndCoins)
        {
            var billsAndCoins = new List<double>();
            foreach (var bill in inputPrice.Split(","))
            {
                if (double.TryParse(bill, out double userBill))
                {
                    if (userBill > 0)
                    {
                        billsAndCoins.Add(userBill);
                    }
                    else
                    {
                        throw new ArgumentOutOfRangeException("The bill cannot be less than 0");
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
        private static void OutputToConsoleAassumption(List<CurrencyDenomination> assumptions)
        {
            foreach (var assumption in assumptions)
            {
                Console.WriteLine($"{assumption.Denomination} - {assumption.Count}");
            }
        }
    }
}
