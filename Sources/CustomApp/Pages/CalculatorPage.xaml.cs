using CustomApp.Models.Pages;
using Microsoft.Extensions.Logging;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CustomApp.Pages;

public partial class CalculatorPage : ContentPage, INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

	private string _equation = String.Empty;
	public CalculatorPage()
	{
		InitializeComponent();

        BindingContext = CalculatorPageModel.Instance;
    }


    private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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
				CalculatorPageModel.Instance.SolveEquation();
				break;
            default:
				CalculatorPageModel.Instance.AddCharacterToEquation(text);
				break;
		}

        //NotifyPropertyChanged();
        Console.WriteLine(this._equation);
	}
}