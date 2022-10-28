using MortgageCalculatorMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MortgageCalculatorMVC.Helpers
{
    public class LoanHelper
    {
        // Get loan data model
        public Loan GetPayments(Loan loan)
        {
            // Calculate monthly payment, assign results to loan.Payment model
            loan.Payment = CalcPayment(loan.Amount, loan.Rate, loan.Term);

            // Caluclate payment schedule
            // Instantiate loan detail variables to track
            var balance = loan.Amount;
            var totalInterest = 0.0m;
            var monthlyInterest = 0.0m;
            var monthlyPrincipal = 0.0m;
            var monthlyRate = CalcMonthlyIntRate(loan.Rate); // Convert APR to monthly int rate

            // Caclcuate and update loan details for each month until the end of the term
            for (int month = 1; month <= loan.Term; month++)
            {
                // Calculate monthly interest payment
                monthlyInterest = CalcMonthlyInterest(balance, monthlyRate);
                // Calculate total interest paid so far
                totalInterest += monthlyInterest;
                // Calculate monthly principal payment
                monthlyPrincipal = loan.Payment - monthlyInterest;
                // Update remaining balance minus principal payment
                balance -= monthlyPrincipal;

                // Instantiate a new LoanPayment object
                LoanPayment loanPayment = new();
                // Input current payment details
                loanPayment.Month = month;
                loanPayment.Payment = loan.Payment;
                loanPayment.MonthlyInterest = monthlyInterest;
                loanPayment.MonthlyPrincipal = monthlyPrincipal;
                loanPayment.TotalInterest = totalInterest;
                loanPayment.Balance = balance;

                // Push loanPayment into the Payments list in the Loan model
                loan.Payments.Add(loanPayment);
            }
            // Update loan.TotalInterest and TotalCost
            loan.TotalInterest = totalInterest;
            loan.TotalCost = loan.Amount + totalInterest;

            // Return loan model 
            return loan;
        }

        // Create private payment calculator
        private decimal CalcPayment(decimal amount, decimal rate, int term)
        {
            // Track total payment
            decimal payment = 0.0m;

            // Covert annual int rate input to a monthly int rate
            decimal monthlyIntRate = CalcMonthlyIntRate(rate);

            // Convert rate and amount parameters to double value type to perform math operations properly
            var rateD = Convert.ToDouble(monthlyIntRate);
            var amountD = Convert.ToDouble(amount);

            // Calculate monthly payment
            var paymentD = (amountD * rateD) / (1 - Math.Pow(1 + rateD, -term));

            // Return total payment converted to decimal
            return Convert.ToDecimal(paymentD);
        }

        // Calculate monthly interest rate from an annual rate input, e.g. 2.5% APR = .002083333
        private decimal CalcMonthlyIntRate (decimal rate)
        {
            return (rate / 1200);
        }

        // Calculate monthly interest payment
        private decimal CalcMonthlyInterest(decimal balance, decimal monthlyRate)
        {
            return (balance * monthlyRate);
        }
    }
}
