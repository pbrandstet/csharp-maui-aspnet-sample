using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomApp.Models.Pages
{
    class CalculatorPageModel : AbstractModel
    {
        private string _equation;

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

        public void SolveEquation()
        {
            
        }
    }
}
