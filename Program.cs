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
            if (File.Exists("transactions.csv"))
            {
                var fileReader = new StreamReader("transactions.csv");
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

            var database = new Transaction();
            // load past transactions from a file when it first starts.

            database.LoadTransactionsFromCSV();

            Console.WriteLine("");
            WelcomeMessage();

            var transactions = new List<Transaction>();

            var keepGoing = true;

            while (keepGoing)
            {
                //menu options

                Console.WriteLine("Do you want Checking, Savings, Balance, History, or Quit?");
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

                    //checking DEPOSIT-

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
                            //add to list

                            transactions.Add(transaction);
                            Console.WriteLine("");
                            Console.WriteLine($"You've added {transaction.Amount} into your checking account.");
                            Console.WriteLine("");

                            var fileWriter = new StreamWriter("transactions.csv");
                            var csvWriter = new CsvWriter(fileWriter, CultureInfo.InvariantCulture);
                            csvWriter.WriteRecords(transactions);
                            fileWriter.Close();
                        }

                    }
                    //checking WITHDRAW-
                    if (checkingAnswer == "withdraw")
                    {
                        Console.WriteLine("How much would you like to withdraw from checking?");

                        var withdrawChoice = int.Parse(Console.ReadLine());

                        //filter out checking acct
                        var allTransactionsChecking = transactions.Where(transaction => transaction.Account == "checking");

                        //filter out deposits & withdraws
                        var allDepositsChecking = allTransactionsChecking.Where(transaction => transaction.Type == "deposit").Sum(deposit => deposit.Amount);
                        var allWithdrawalsChecking = allTransactionsChecking.Where(transaction => transaction.Type == "withdraw").Sum(withdraw => withdraw.Amount);

                        var totalCheckingsBalance = allDepositsChecking - allWithdrawalsChecking;

                        //doesn't allow account to go negative
                        if (withdrawChoice > totalCheckingsBalance)
                        {
                            Console.WriteLine("Insufficient funds.");
                        }

                        else
                        {
                            var transaction = new Transaction();
                            {
                                transaction.Amount = withdrawChoice;
                                transaction.Account = "checking";
                                transaction.Type = "withdraw";
                                transaction.TimeStamp = DateTime.Now;
                                transactions.Add(transaction);
                            }

                            Console.WriteLine("");
                            Console.WriteLine($"You've withdrawn {transaction.Amount} from  your checking account.");
                            Console.WriteLine("");

                            var fileWriter = new StreamWriter("transactions.csv");
                            var csvWriter = new CsvWriter(fileWriter, CultureInfo.InvariantCulture);
                            csvWriter.WriteRecords(transactions);
                            fileWriter.Close();
                        }
                    }
                }
                //SAVINGS - 
                else if (choice == "savings")
                {
                    Console.WriteLine("Savings account: Would you like to deposit or withdraw?");
                    var savingsAnswer = Console.ReadLine();
                    //savingsDEPOSIT-
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
                            transactions.Add(transaction);
                            Console.WriteLine("");
                            Console.WriteLine($"You've deposited {transaction.Amount} into your savings account.");
                            Console.WriteLine("");

                            var fileWriter = new StreamWriter("transactions.csv");
                            var csvWriter = new CsvWriter(fileWriter, CultureInfo.InvariantCulture);
                            csvWriter.WriteRecords(transactions);
                            fileWriter.Close();
                        }

                    }

                    //savingsWITHDRAW--
                    if (savingsAnswer == "withdraw")
                    {
                        Console.WriteLine("How much would you like to withdraw from savings?");

                        var withdrawSavingsChoice = int.Parse(Console.ReadLine());

                        //filter out checking acct
                        var allTransactionsSavings = transactions.Where(transaction => transaction.Account == "savings");

                        //filter out deposits & withdraws
                        var allDepositsChecking = allTransactionsSavings.Where(transaction => transaction.Type == "deposit").Sum(deposit => deposit.Amount);
                        var allWithdrawalsChecking = allTransactionsSavings.Where(transaction => transaction.Type == "withdraw").Sum(withdraw => withdraw.Amount);

                        var totalSavingsBalance = allDepositsChecking - allWithdrawalsChecking;

                        //doesn't allow account to go negative
                        if (withdrawSavingsChoice > totalSavingsBalance)
                        {
                            Console.WriteLine("Insufficient funds.");
                        }

                        else
                        {
                            var transaction = new Transaction();
                            transaction.Amount = withdrawSavingsChoice;
                            transaction.Account = "savings";
                            transaction.Type = "withdraw";
                            transaction.TimeStamp = DateTime.Now;
                            transactions.Add(transaction);

                            Console.WriteLine("");
                            Console.WriteLine($"You've withdrawn {transaction.Amount} from your savings account.");
                            Console.WriteLine("");

                            var fileWriter = new StreamWriter("transactions.csv");
                            var csvWriter = new CsvWriter(fileWriter, CultureInfo.InvariantCulture);
                            csvWriter.WriteRecords(transactions);
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

                    var allTransactionsChecking = transactions.Where(transaction => transaction.Account == "checking");
                    var allDepositsChecking = allTransactionsChecking.Where(transaction => transaction.Type == "deposit").Sum(deposit => deposit.Amount);
                    var allWithdrawalsChecking = allTransactionsChecking.Where(transaction => transaction.Type == "withdraw").Sum(withdraw => withdraw.Amount);

                    var totalCheckingsBalance = allDepositsChecking - allWithdrawalsChecking;
                    Console.WriteLine($"Checking Balance: {totalCheckingsBalance}");

                    // // b. show total balance of savings

                    var allTransactionsSavings = transactions.Where(transaction => transaction.Account == "savings");
                    var allDepositsSavings = allTransactionsSavings.Where(transaction => transaction.Type == "deposit").Sum(deposit => deposit.Amount);
                    var allWithdrawalsSavings = allTransactionsSavings.Where(transaction => transaction.Type == "withdraw").Sum(withdraw => withdraw.Amount);

                    var totalSavingsBalance = allDepositsSavings - allWithdrawalsSavings;
                    Console.WriteLine($"Savings Balance: {totalSavingsBalance}");
                    Console.WriteLine("");

                }
                else if (choice == "history")
                {
                    foreach (var transaction in transactions)
                    {
                        Console.WriteLine(transaction.Description());
                        Console.WriteLine("");

                    }
                }
            }

        }
    }
}
