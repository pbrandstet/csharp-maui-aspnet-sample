using CustomApp.Models.Pages;
using Microsoft.Extensions.Logging;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CustomApp.Pages;

public partial class CalculatorPage : ContentPage
{
	private string _equation = String.Empty;
	public CalculatorPage()
	{
		InitializeComponent();

        BindingContext = CalculatorPageModel.Instance;
    }

    private void OnButtonClicked(object sender, EventArgs e)
	{
		var button = (Button)sender;
		var text = button.Text;

		switch(text)
		{
			case "AC":
				CalculatorPageModel.Instance.ClearInput();
				break;
			case "=":
				var result = CalculatorPageModel.Instance.SolveEquation();
				CalculatorPageModel.Instance.UpdateEquation(result);
				break;
            default:
				CalculatorPageModel.Instance.AddCharacterToEquation(text);
				break;
		}

        //NotifyPropertyChanged();
        Console.WriteLine(this._equation);
	}
}