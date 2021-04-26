using System;
using FirstBankOfSuncoastNew.Models;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using CsvHelper;
using System.Globalization;
using CsvHelper.Configuration;

namespace FirstBankOfSuncoastNew
{
    class Transaction
    {
        public string Account { get; set; }
        public int Amount { get; set; }
        public string Type { get; set; } //withdraw or deposit
        public DateTime TimeStamp { get; set; } = DateTime.Now;

        private List<Transaction> transactions = new List<Transaction>();

        public string Description()
        {
            var transactionDetails = $"Account: {Account} - Type: {Type} - Amount: {Amount} - Time: {TimeStamp}";
            return transactionDetails;
        }
        public void LoadTransactionsFromCSV()
        {
            if (File.Exists("totalTransactions.csv"))
            {
                var fileReader = new StreamReader("totalTransactions.csv");
                var csvReader = new CsvReader(fileReader, CultureInfo.InvariantCulture);
                transactions = csvReader.GetRecords<Transaction>().ToList();
                fileReader.Close();
            }
        }

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
            // load past transactions from a file when it first starts.

            var database = new Transaction();
            database.LoadTransactionsFromCSV();

            Console.WriteLine("");
            WelcomeMessage();

            var totalTransactions = new List<Transaction>();

            var keepGoing = true;

            while (keepGoing)
            {

                Console.WriteLine("Do you want Checking, Savings, Balance, Transaction History, or Quit?");
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
                        var transaction = new Transaction();
                        transaction.Account = "checking";
                        transaction.Type = "deposit";
                        transaction.TimeStamp = DateTime.Now;

                        Console.WriteLine("How much would you like to deposit into checking?");
                        transaction.Amount = int.Parse(Console.ReadLine());

                        if (transaction.Amount <= 0)
                        {
                            Console.WriteLine("Sorry, you cannot deposit that amount.");
                        }

                        else
                        {
                            //Add to list
                            totalTransactions.Add(transaction);
                            Console.WriteLine("");
                            Console.WriteLine($"You've added {transaction.Amount} into your checking account.");
                            Console.WriteLine("");

                            var fileWriter = new StreamWriter("totalTransactions.csv");
                            var csvWriter = new CsvWriter(fileWriter, CultureInfo.InvariantCulture);
                            csvWriter.WriteRecords(totalTransactions);
                            fileWriter.Close();
                        }

                    }
                    //WITHDRAW-
                    if (checkingAnswer == "withdraw")
                    {
                        var transaction = new Transaction();
                        Console.WriteLine("How much would you like to withdraw from checking?");
                        transaction.Amount = int.Parse(Console.ReadLine());
                        transaction.Account = "checking";
                        transaction.Type = "withdraw";
                        transaction.TimeStamp = DateTime.Now;

                        //this should be the statement that makes it so you can't withdraw more than is in your account, but I cannot figure it out.
                        if (transaction.Amount <= 0)
                        {
                            Console.WriteLine("Insufficient funds.");
                        }

                        else
                        {
                            totalTransactions.Add(transaction);

                            Console.WriteLine("");
                            Console.WriteLine($"You've withdrawn {transaction.Amount} from  your checking account.");
                            Console.WriteLine("");

                            var fileWriter = new StreamWriter("totalTransactions.csv");
                            var csvWriter = new CsvWriter(fileWriter, CultureInfo.InvariantCulture);
                            csvWriter.WriteRecords(totalTransactions);
                            fileWriter.Close();
                        }
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

                        var transaction = new Transaction();
                        Console.WriteLine("How much would you like to deposit into savings?");
                        transaction.Amount = int.Parse(Console.ReadLine());
                        transaction.Account = "savings";
                        transaction.Type = "deposit";

                        if (transaction.Amount <= 0)
                        {
                            Console.WriteLine("Sorry, you cannot deposit that amount.");
                        }

                        else
                        {
                            totalTransactions.Add(transaction);
                            Console.WriteLine("");
                            Console.WriteLine($"You've deposited {transaction.Amount} into your savings account.");
                            Console.WriteLine("");

                            var fileWriter = new StreamWriter("totalTransactions.csv");
                            var csvWriter = new CsvWriter(fileWriter, CultureInfo.InvariantCulture);
                            csvWriter.WriteRecords(totalTransactions);
                            fileWriter.Close();
                        }

                    }

                    //WITHDRAW--
                    if (savingsAnswer == "withdraw")
                    {
                        var transaction = new Transaction();
                        Console.WriteLine("How much would you like to withdraw from savings?");
                        transaction.Amount = int.Parse(Console.ReadLine());
                        transaction.Account = "savings";
                        transaction.Type = "withdraw";

                        //this should be the statement that doesn't allow for you to withdraw more than is in your account, but I cannot figure it out.
                        if (transaction.Amount <= 0)
                        {
                            Console.WriteLine("Sorry, you cannot withdraw that amount.");
                        }

                        else
                        {
                            totalTransactions.Add(transaction);

                            Console.WriteLine("");
                            Console.WriteLine($"You've withdrawn {transaction.Amount} from your savings account.");
                            Console.WriteLine("");

                            var fileWriter = new StreamWriter("totalTransactions.csv");
                            var csvWriter = new CsvWriter(fileWriter, CultureInfo.InvariantCulture);
                            csvWriter.WriteRecords(totalTransactions);
                            fileWriter.Close();
                        }
                    }
                }

                // BALANCE -
                if (choice == "balance")
                {
                    Console.WriteLine("");
                    Console.WriteLine("The balance of your accounts:");
                    Console.WriteLine("");
                    // a. show total balance of checking

                    var allTransactionsChecking = totalTransactions.Where(transaction => transaction.Account == "checking");
                    var allDepositsChecking = allTransactionsChecking.Where(deposit => deposit.Type == "deposit").Sum(deposit => deposit.Amount);
                    var allWithdrawalsChecking = allTransactionsChecking.Where(withdraw => withdraw.Type == "withdraw").Sum(withdraw => withdraw.Amount);

                    var totalCheckingsBalance = allDepositsChecking - allWithdrawalsChecking;
                    Console.WriteLine($"Checking Balance: {totalCheckingsBalance}");

                    // // b. show total balance of savings

                    var allTransactionsSavings = totalTransactions.Where(transaction => transaction.Account == "savings");
                    var allDepositsSavings = allTransactionsSavings.Where(deposit => deposit.Type == "deposit").Sum(deposit => deposit.Amount);
                    var allWithdrawalsSavings = allTransactionsSavings.Where(withdraw => withdraw.Type == "withdraw").Sum(withdraw => withdraw.Amount);

                    var totalSavingsBalance = allDepositsSavings - allWithdrawalsSavings;
                    Console.WriteLine($"Savings Balance: {totalSavingsBalance}");
                    Console.WriteLine("");

                }
                else if (choice == "transaction history")
                {
                    foreach (var transaction in totalTransactions)
                    {
                        Console.WriteLine(transaction.Description());
                        Console.WriteLine("");

                    }
                }
            }

        }
    }
}
