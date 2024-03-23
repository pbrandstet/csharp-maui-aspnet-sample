using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using NCalc;
using NCalc.Domain;

namespace CustomApp.Models.Pages
{
    class CalculatorPageModel : AbstractModel
    {
        private string _equation;
        private double _result;

        public static readonly CalculatorPageModel Instance = new CalculatorPageModel();

        private CalculatorPageModel() { }

        public string Equation
        {
            set => SetProperty(ref _equation, value);
            get => _equation;
        }

        public void AddCharacterToEquation(string character)
        {
            Equation += character;
        }

        public void ClearInput()
        {
            Equation = string.Empty;
        }

        private LogicalExpression SolveLogicalExpression(LogicalExpression exp)
        {
            if(exp.GetType() == typeof(BinaryExpression))
            {
                return SolveBinaryExpression(exp as BinaryExpression);
            }
            return new ValueExpression(1);
        }

        private BinaryExpression SolveBinaryExpression(BinaryExpression exp)
        {

        }

        //private ValueExpression SolveValueExpression(LogicalExpression left, LogicalExpression right)
        //{

        //}

        private double SolveExpression(Expression exp)
        {
            if (exp == null) throw new ArgumentNullException();

            var parsedExp = exp.ParsedExpression;
            if (parsedExp == null) throw new Exception("Failed to parse expression");

            var result = SolveLogicalExpression(parsedExp);


            return 0;
        }

        public string SolveEquation()
        {
            try
            {
                Expression e = new Expression(Equation);
                e.Evaluate();

                Debug.WriteLine(e.ToString());

                var result = SolveExpression(e);

                Console.WriteLine("solved equation");
                return "Hallo";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return "SYNTAX ERROR";
            }
            


        }
    }
}
