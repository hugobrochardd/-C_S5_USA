using System;
using System.Collections.Generic;

// Base class for transactions
class Transaction
{
    protected double Amount; // The amount of the transaction
    protected double Revenue; // The calculated revenue or payment

    // Getter methods to allow access to protected variables
    public double GetAmount() => Amount;
    public double GetRevenue() => Revenue;

    // Virtual method to display transaction details
    public virtual void Display()
    {
        Console.WriteLine($"Amount: {Amount:C}, Revenue: {Revenue:C}");
    }
}

// Loan class, inheriting from Transaction
class Loan : Transaction
{
    protected int Period; // Loan period in years
    protected double Interest; // Annual interest rate

    // Constructor to initialize loan details
    public Loan(double amount, int period, double interest)
    {
        Amount = amount;
        Period = period;
        Interest = interest;
        Revenue = CalculateLoanRevenue(amount, period, interest); // Calculate annual payment
    }

    // Private method to calculate annual loan revenue (negative payment)
    private double CalculateLoanRevenue(double amount, int period, double interest)
    {
        // Formula to calculate monthly payment
        double monthlyPayment = amount * interest / 12 / (1 - Math.Pow(1 + interest / 12, -period * 12));
        return -monthlyPayment * 12; // Convert monthly to annual payment
    }

    // Override the display method to include loan-specific details
    public override void Display()
    {
        Console.WriteLine($"Loan - Amount: {Amount:C}, Period: {Period} years, Interest Rate: {Interest:P}, Annual Payment: {Revenue:C}");
    }
}

// Saving class, inheriting from Transaction
class Saving : Transaction
{
    protected double InterestRate; // Fixed annual interest rate

    // Constructor to initialize saving details
    public Saving(double amount)
    {
        Amount = amount;
        InterestRate = 0.05; // Default interest rate is 5%
        Revenue = amount * InterestRate; // Calculate annual interest income
    }

    // Override the display method to include saving-specific details
    public override void Display()
    {
        Console.WriteLine($"Saving - Amount: {Amount:C}, Interest Rate: {InterestRate:P}, Interest Income: {Revenue:C}");
    }
}

// Main program
class Program
{
    static void Main(string[] args)
    {
        // Dictionaries to store loan and saving transactions
        Dictionary<string, Loan> loans = new Dictionary<string, Loan>();
        Dictionary<string, Saving> savings = new Dictionary<string, Saving>();

        // Welcome message for the user
        Console.WriteLine("Welcome to the Personal Banking System!");
        Console.WriteLine("This system allows you to manage loans and savings.");
        Console.WriteLine("Follow the menu options to perform various operations.\n");

        while (true) // Infinite loop for the menu
        {
            // Display the menu options
            Console.WriteLine("=====================================");
            Console.WriteLine("Menu:");
            Console.WriteLine("1. Add one transaction");
            Console.WriteLine("2. Show all transactions");
            Console.WriteLine("3. Show all loans");
            Console.WriteLine("4. Show all savings");
            Console.WriteLine("5. Show account balance");
            Console.WriteLine("6. Show net revenue next year");
            Console.WriteLine("7. Exit");
            Console.WriteLine("=====================================");
            Console.Write("Please enter your choice (1-7): ");

            string choice = Console.ReadLine(); // Read user input for menu selection

            try
            {
                if (choice == "1")
                {
                    // Option to add a new transaction
                    Console.Write("Enter transaction type (loan/saving): ");
                    string type = Console.ReadLine().ToLower();

                    if (type == "loan")
                    {
                        // Adding a new loan transaction
                        Console.Write("Enter loan amount: ");
                        double amount = double.Parse(Console.ReadLine());
                        Console.Write("Enter loan period (years): ");
                        int period = int.Parse(Console.ReadLine());
                        Console.Write("Enter annual interest rate (e.g., 0.1 for 10%): ");
                        double interest = double.Parse(Console.ReadLine());

                        Loan loan = new Loan(amount, period, interest); // Create loan object
                        loans.Add($"loan#{loans.Count + 1}", loan); // Add to the dictionary

                        Console.WriteLine("Loan added successfully!"); // Confirmation message
                    }
                    else if (type == "saving")
                    {
                        // Adding a new saving transaction
                        Console.Write("Enter saving amount: ");
                        double amount = double.Parse(Console.ReadLine());

                        Saving saving = new Saving(amount); // Create saving object
                        savings.Add($"saving#{savings.Count + 1}", saving); // Add to the dictionary

                        Console.WriteLine("Saving added successfully!"); // Confirmation message
                    }
                    else
                    {
                        Console.WriteLine("Invalid transaction type. Please enter 'loan' or 'saving'.");
                    }
                }
                else if (choice == "2")
                {
                    // Display all transactions (both loans and savings)
                    Console.WriteLine("\nAll Transactions:");
                    if (loans.Count == 0 && savings.Count == 0)
                    {
                        Console.WriteLine("No transactions found.");
                    }
                    else
                    {
                        foreach (var loan in loans.Values) loan.Display();
                        foreach (var saving in savings.Values) saving.Display();
                    }
                }
                else if (choice == "3")
                {
                    // Display all loans
                    Console.WriteLine("\nAll Loans:");
                    if (loans.Count == 0)
                    {
                        Console.WriteLine("No loans found.");
                    }
                    else
                    {
                        foreach (var loan in loans.Values) loan.Display();
                    }
                }
                else if (choice == "4")
                {
                    // Display all savings
                    Console.WriteLine("\nAll Savings:");
                    if (savings.Count == 0)
                    {
                        Console.WriteLine("No savings found.");
                    }
                    else
                    {
                        foreach (var saving in savings.Values) saving.Display();
                    }
                }
                else if (choice == "5")
                {
                    // Calculate and display the account balance
                    double totalLoans = 0, totalSavings = 0;
                    foreach (var loan in loans.Values) totalLoans += loan.GetAmount();
                    foreach (var saving in savings.Values) totalSavings += saving.GetAmount();

                    Console.WriteLine($"\nAccount Balance: {totalSavings - totalLoans:C}");
                }
                else if (choice == "6")
                {
                    // Calculate and display net revenue for next year
                    double totalLoanPayments = 0, totalSavingIncome = 0;
                    foreach (var loan in loans.Values) totalLoanPayments += loan.GetRevenue();
                    foreach (var saving in savings.Values) totalSavingIncome += saving.GetRevenue();

                    Console.WriteLine($"\nNet Revenue Next Year: {totalSavingIncome + totalLoanPayments:C}");
                }
                else if (choice == "7")
                {
                    // Exit the program
                    Console.WriteLine("Thank you for using the Personal Banking System. Goodbye!");
                    break;
                }
                else
                {
                    // Handle invalid menu choices
                    Console.WriteLine("Invalid choice. Please select a number between 1 and 7.");
                }
            }
            catch (FormatException)
            {
                // Handle invalid numerical input
                Console.WriteLine("Invalid input. Please enter a valid number.");
            }
        }
    }
}