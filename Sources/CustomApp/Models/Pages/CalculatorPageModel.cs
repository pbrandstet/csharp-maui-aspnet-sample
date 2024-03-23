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

        public void UpdateEquation(string equation)
        {
            Equation = equation;
        }

        public void ClearInput()
        {
            Equation = string.Empty;
        }

        private LogicalExpression SolveLogicalExpression(LogicalExpression exp)
        {
            if(exp.GetType() == typeof(BinaryExpression))
            {
                var expression = (BinaryExpression)exp;

                if(expression.LeftExpression.GetType() == typeof(BinaryExpression) && expression.RightExpression.GetType() == typeof(BinaryExpression))
                {
                    var left = SolveLogicalExpression(expression.LeftExpression);
                    var right = SolveLogicalExpression(expression.RightExpression);
                    return SolveLogicalExpression(new BinaryExpression(expression.Type, left, right));
                }

                if(expression.LeftExpression.GetType() == typeof (BinaryExpression) && expression.RightExpression.GetType() == typeof(ValueExpression))
                {
                    var left = SolveLogicalExpression(expression.LeftExpression);
                    return new BinaryExpression(expression.Type, left, expression.RightExpression);
                }

                if(expression.LeftExpression.GetType() == typeof(ValueExpression) && expression.RightExpression.GetType() == typeof (BinaryExpression))
                {
                    var right = SolveLogicalExpression(expression.RightExpression);
                    return new BinaryExpression(expression.Type, expression.LeftExpression, right);
                }

                if (expression.LeftExpression.GetType() == typeof (ValueExpression) && expression.RightExpression.GetType() == typeof (ValueExpression))
                {
                    try
                    {
                        var left = (ValueExpression)expression.LeftExpression;
                        var leftValue = double.Parse(left.Value.ToString());

                        var right = (ValueExpression)expression.RightExpression;
                        var rightValue = double.Parse(right.Value.ToString());

                        if (expression is LogicalExpression)
                        {
                            switch (expression.Type)
                            {
                                case BinaryExpressionType.Plus:
                                    return new ValueExpression(leftValue + rightValue);
                                case BinaryExpressionType.Minus:
                                    return new ValueExpression(leftValue - rightValue);
                                case BinaryExpressionType.Times:
                                    return new ValueExpression(leftValue * rightValue);
                                case BinaryExpressionType.Div:
                                    return new ValueExpression(leftValue / rightValue);
                            }
                        }
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine(e);
                    }
                    
                }   
            }
            return null;
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

            if (result == null) throw new Exception("Failed to solve expression");

            if(result is ValueExpression)
            {
                var res = (ValueExpression)result;
                return double.Parse(res.Value.ToString());
            }
            else
            {
                throw new Exception("Returned result wasn't of type ValueExpression");
            }


            
        }

        public string SolveEquation()
        {
            try
            {
                Expression e = new Expression(Equation);
                e.Evaluate();

                Debug.WriteLine(e.ToString());

                var result = SolveExpression(e);

                return result.ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return "SYNTAX ERROR";
            }
            


        }
    }
}
