using System.Windows;
using QuestionnaireApp.ViewModels;
using Xceed.Wpf.Toolkit;

namespace QuestionnaireApp.Views
{
	/// <summary>
	/// Interaction logic for NumberOptionView.xaml
	/// </summary>
	public partial class NumberOptionView
	{
		public NumberOptionView()
		{
			InitializeComponent();
		}

		private void UpDownBase_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
		{
			var integerUpDown = (IntegerUpDown)sender;
			var optionViewModel = (OptionViewModel) DataContext;
			optionViewModel.SingleOptionCommand.Execute(integerUpDown.Value);
		}
	}
}
