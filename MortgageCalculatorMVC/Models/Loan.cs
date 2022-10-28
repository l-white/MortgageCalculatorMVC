using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MortgageCalculatorMVC.Models
{
    public class Loan
    {
        // Important properties of the loan to track:
        // Loan amount
        public decimal Amount { get; set; }

        // Annual interest rate
        public decimal Rate { get; set; }

        // Term in months, i.e. length of loan repayments
        public int Term { get; set; }

        // Total monthly payment
        public decimal Payment { get; set; }

        // Total interest paid
        public decimal TotalInterest { get; set; }

        // Total cost of loan
        public decimal TotalCost { get; set; }

        // Create and instantiate a list of all loan payments for this loan as stored in the model LoanPayment
        public List<LoanPayment> Payments { get; set; } = new List<LoanPayment>();


    }
}
