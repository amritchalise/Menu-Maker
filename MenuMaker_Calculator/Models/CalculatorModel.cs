using System;
namespace MenuMaker_Calculator.Models
{
    public class CalculatorModel
    {
        public string CurrentExpression { get; set; }
        public string Calresult { get; set; }
        

        public CalculatorModel()
        {
            CurrentExpression = "0";
            Calresult = "0";
          
        }

        public void AddInput(string input)
        {
            if (input == "%")
            {
                if (!CurrentExpression.EndsWith("%"))
                {
                    // Convert the expression to a percentage
                    if (double.TryParse(CurrentExpression, out double number))
                    {
                        CurrentExpression = (number / 100).ToString();
                    }
                }
            }
            else
            {

                if (input == "." && CurrentExpression.Contains("."))
                    return;

                if (CurrentExpression == "0" && input != ".")
                    CurrentExpression = input;
                else
                    CurrentExpression += input;

            }
        }
            


        public void Clear()
        {
            CurrentExpression = "0";
            
        }

        public void Calculate()
        {
            try
            {
                
                var dataTable = new System.Data.DataTable();
                var result = dataTable.Compute(CurrentExpression, "");
                Calresult = result.ToString();
                 
            }
            catch (Exception ex)
            {
                CurrentExpression = "Error";
                Console.WriteLine($"Error occurred during calculation: {ex.Message}");
            }
            
        }
    }
}


