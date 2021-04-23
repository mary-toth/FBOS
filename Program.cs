using System;
using FirstBankOfSuncoastNew.Models;
using System.Linq;
using System.Collections.Generic;

namespace FirstBankOfSuncoast
{
    class Transactions
    {
        public string Account { get; set; }
        public int Amount { get; set; }
        public string Type { get; set; } //withdraw or deposit
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

            //LOAD past transactions-

            //Show list of transactions to user-

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
                    //DEPOSIT-
                    if (checkingAnswer == "deposit")
                    {
                        var newDeposit = new Transactions();
                        Console.WriteLine("How much would you like to deposit into checking?");
                        newDeposit.Amount = int.Parse(Console.ReadLine());

                        if (newDeposit.Amount < 0)
                        {
                            Console.WriteLine("You cannot enter a negative number.");
                        }
                        newDeposit.Account = "checking";
                        newDeposit.Type = "deposit";

                        if (newDeposit.Amount > 0)
                        {
                            transactions.Add(newDeposit);
                            Console.WriteLine($"You've added {newDeposit.Amount} into your checking account.");
                        }

                    }
                    //WITHDRAW-
                    if (checkingAnswer == "withdraw")
                    {
                        var newWithdraw = new Transactions();
                        Console.WriteLine("How much would you like to withdraw from checking?");
                        newWithdraw.Amount = int.Parse(Console.ReadLine());

                        if (newWithdraw.Amount < 0)
                        {
                            Console.WriteLine("You cannot enter a negative number.");
                        }
                        newWithdraw.Account = "checking";
                        newWithdraw.Type = "withdraw";

                        if (newWithdraw.Amount > 0)
                            transactions.Add(newWithdraw);

                        Console.WriteLine($"You've withdrawn {newWithdraw.Amount} from  your checking account.");
                    }
                }
                //SAVINGS - 
                else if (choice == "savings")
                {
                    Console.WriteLine("Savings account: Would you like to deposit or withdraw?");
                    var savingsAnswer = Console.ReadLine();
                    //DEPOSIT-
                    if (savingsAnswer == "deposit")
                    {
                        var newSavingsDeposit = new Transactions();
                        Console.WriteLine("How much would you like to deposit into savings?");
                        newSavingsDeposit.Amount = int.Parse(Console.ReadLine());

                        if (newSavingsDeposit.Amount < 0)
                        {
                            Console.WriteLine("You cannot enter a negative number.");
                        }
                        newSavingsDeposit.Account = "savings";
                        newSavingsDeposit.Type = "deposit";

                        if (newSavingsDeposit.Amount > 0)
                        {
                            transactions.Add(newSavingsDeposit);
                            Console.WriteLine($"You've added {newSavingsDeposit.Amount} into your savings account.");
                        }
                    }

                    //WITHDRAW--
                    if (savingsAnswer == "withdraw")
                    {
                        var newSavingsWithdraw = new Transactions();
                        Console.WriteLine("How much would you like to withdraw from savings?");
                        newSavingsWithdraw.Amount = int.Parse(Console.ReadLine());

                        if (newSavingsWithdraw.Amount < 0)
                        {
                            Console.WriteLine("You cannot enter a negative number.");
                        }
                        newSavingsWithdraw.Account = "savings";
                        newSavingsWithdraw.Type = "withdraw";

                        if (newSavingsWithdraw.Amount > 0)
                            transactions.Add(newSavingsWithdraw);

                        Console.WriteLine($"You've withdrawn {newSavingsWithdraw.Amount} from your savings account.");
                    }
                }

                // BALANCE -
                else if (choice == "balance")
                {
                    Console.WriteLine("The balance of your accounts:");
                    // a. show total balance of checking

                    var allTransactionsChecking = transactions.Where(transaction => transaction.Account == "checking");
                    var allDepositsChecking = allTransactionsChecking.Where(deposit => deposit.Type == "deposit").Sum(deposit => deposit.Amount);
                    var allWithdrawalsChecking = allTransactionsChecking.Where(withdraw => withdraw.Type == "withdraw").Sum(withdraw => withdraw.Amount);

                    Console.WriteLine($"Checking Account: {allDepositsChecking - allWithdrawalsChecking} ");

                    // b. show total balance of savings

                    var allTransactionsSavings = transactions.Where(transaction => transaction.Account == "savings");
                    var allDepositsSavings = allTransactionsSavings.Where(deposit => deposit.Type == "deposit").Sum(deposit => deposit.Amount);
                    var allWithdrawalsSavings = allTransactionsSavings.Where(withdraw => withdraw.Type == "withdraw").Sum(withdraw => withdraw.Amount);

                    Console.WriteLine($"Savings Account: {allDepositsSavings - allWithdrawalsSavings} ");
                }


            }

        }
    }
}
