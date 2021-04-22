using System;
using FirstBankOfSuncoastNew.Models;
using System.Linq;
using System.Collections.Generic;

namespace FirstBankOfSuncoast
{
    class Transactions
    {
        public int Checking { get; set; }
        public int Savings { get; set; }
        public int TotalAmount { get; set; }
        // public string Deposit {get; set;}
        // public string Withdraw {get; set;}

    }
    class Program
    {
        static void WelcomeMessage()
        {
            Console.WriteLine(new string('*', 34));
            Console.WriteLine("Welcome to First Bank of Suncoast!");
            Console.WriteLine(new string('*', 34));
            Console.WriteLine("");
        }

        static void Main(string[] args)
        {
            WelcomeMessage();

            var transactions = new List<Transactions>();

            var keepGoing = true;

            while (keepGoing)
            {
                Console.WriteLine("Do you want Checking, Savings, Balance, or Quit?");
                var choice = Console.ReadLine().ToLower();
                //QUIT - stops program
                if (choice == "quit")
                {
                    keepGoing = false;
                }
                //CHECKING - 
                else if (choice == "checking")
                {
                    Console.WriteLine("Checking account: Would you like to deposit or withdraw?");
                    var checkingAnswer = Console.ReadLine();
                    //ADD-
                    if (checkingAnswer == "deposit")
                    {
                        var newNumber = new Transactions();
                        Console.WriteLine("How much would you like to deposit into checking?");
                        newNumber.Checking = int.Parse(Console.ReadLine());

                        transactions.Add(newNumber);

                        Console.WriteLine($"You've added {newNumber.Checking} into your checking account.");

                    }
                    //WITHDRAW-
                    if (checkingAnswer == "withdraw")
                    {
                        Console.WriteLine("Enter number to withdraw:");
                        var removeNumber = int.Parse(Console.ReadLine());

                        Transactions removedNumber = transactions.FirstOrDefault(checking => checking.Checking == removeNumber);

                        transactions.Remove(removedNumber);

                        Console.WriteLine($"You have withdrawn {removeNumber} from your account");

                    }
                }
                //SAVINGS - 
                else if (choice == "savings")
                {
                    Console.WriteLine("Savings account: would you like to deposit or withdraw? ");
                    var savingsAnswer = Console.ReadLine();
                    if (savingsAnswer == "deposit")
                    {
                        var savingsDeposit = new Transactions();
                        Console.WriteLine("Enter the amount you would like to deposit into your savings account.");
                        Console.ReadLine();

                        transactions.Add(savingsDeposit);
                    }
                    if (savingsAnswer == "withdraw")
                    {
                        var savingsWithdraw = new Transactions();
                        Console.WriteLine("Enter the amount you would like to withdraw from your savings account.");
                        Console.ReadLine();

                        transactions.Remove(savingsWithdraw);
                    }

                }
                // BALANCE -
                else if (choice == "balance")
                {
                    Console.WriteLine("The balance of your checking account is:");
                    // a. show total balance of checking


                    var totalBalance = transactions.Aggregate(0, (currentTotal, transaction) => currentTotal + transaction.Checking);

                    Console.WriteLine($"Checking Account: {totalBalance}");

                    // var checkingBalance = transactions.Sum(balance => balance.Checking);

                    // Console.WriteLine($"Checking Account: {checkingBalance}");

                    // b. show total balance of savings
                }
            }

        }

    }
}
