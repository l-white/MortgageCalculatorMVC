using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MortgageCalculatorMVC.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using MortgageCalculatorMVC.Helpers;

namespace MortgageCalculatorMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        // GET App HTTP filter
        [HttpGet]
        public IActionResult App()
        {
            // Create an instance of the loan model, set defaults to what you whant, pass to this App view
            Loan loan = new();
            loan.Payment = 0.0m;
            loan.TotalInterest = 0.0m;
            loan.TotalCost = 0.0m;
            loan.Rate = 3.5m;
            loan.Amount = 15000m;
            loan.Term = 60;

            return View(loan);
        }

        // POST App HTTP filter, i.e. take in and process user-submitted data and return the results
        [HttpPost]
        [AutoValidateAntiforgeryToken] // Form validation security
        public IActionResult App(Loan loan) // Input user-defined loan criteria model as App() parameter
        {
            // Calculate monthly loan payment details
            // Create an instance of LoanHelper
            var loanHelper = new LoanHelper();

            // Call GetPayments method, pass in loan parameter, assign results to a new Loan model
            Loan newLoan = loanHelper.GetPayments(loan);
            
            // Return the new loan model
            return View(newLoan);
        }       

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
