using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MortgageCalculatorMVC.Models
{
    public class LoanPayment
    {
        // Monthly payment properties to track:
        // Month number being paid, e.g. Month 6 payment of 60 months
        public int Month { get; set; }

        // Monthly payment total
        // Apply display format
        [DisplayFormat(DataFormatString = "{0:C0}", ApplyFormatInEditMode = true)]
        public decimal Payment { get; set; }

        // Monthly principal only payment
        // Apply display format
        [DisplayFormat(DataFormatString = "{0:C0}")]
        public decimal MonthlyPrincipal { get; set; }

        // Monthly interest only payment
        public decimal MonthlyInterest { get; set; }

        // Total interest paid so far
        public decimal TotalInterest { get; set; }

        // Updated balance remaining
        public decimal Balance { get; set; }
    }
}
