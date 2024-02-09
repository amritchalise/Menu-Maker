using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MenuMaker_Calculator.Models;
using Microsoft.AspNetCore.SignalR.Protocol;
using Microsoft.AspNetCore.Http;

namespace MenuMaker_Calculator.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly CalculatorModel _calculator;
    



    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
        _calculator = new CalculatorModel();
        

    }

    

        public ActionResult Index(string currentExpression, string calresult)
        {
        string previousResult = HttpContext.Session.GetString("PreviousResult");
        if (previousResult == null)
        {
            previousResult = "0"; // Default value
        }
        _calculator.CurrentExpression = currentExpression ?? _calculator.CurrentExpression;
        _calculator.Calresult = calresult ?? _calculator.Calresult;
        return View(_calculator);
        }

    [HttpPost]
    public ActionResult calculate(string input, string currentExpression, string action)
    {
        _calculator.CurrentExpression = currentExpression;
        if (action == "Clear" )
        {
            _calculator.Clear();
            
        }
        else if (action == "Calculate")
        {
            _calculator.Calculate();
            HttpContext.Session.SetString("PreviousResult", _calculator.Calresult);
        }
        else if (action == "Ans")
        {
            string previousResult = HttpContext.Session.GetString("PreviousResult");
            if (previousResult != null)
            {
                _calculator.AddInput(previousResult);
            }
        }
        else if (action == "Backspace")
        {
            // Remove last character from current expression
            if (_calculator.CurrentExpression.Length > 0)
            {
                _calculator.CurrentExpression = _calculator.CurrentExpression.Substring(0, _calculator.CurrentExpression.Length - 1);
            }
        }
        else
        {
            _calculator.AddInput(input);
        }
        return RedirectToAction("Index", new { currentExpression = _calculator.CurrentExpression, calresult = _calculator.Calresult } );
    }


    

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

