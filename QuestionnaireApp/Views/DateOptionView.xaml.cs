using System.Windows.Controls;
using QuestionnaireApp.ViewModels;

namespace QuestionnaireApp.Views
{
	/// <summary>
	/// Interaction logic for DateOptionView.xaml
	/// </summary>
	public partial class DateOptionView
	{
		public DateOptionView()
		{
			InitializeComponent();
		}

		private void DatePicker_OnSelectedDateChanged(object sender, SelectionChangedEventArgs e)
		{
			var datePicker = (DatePicker)sender;
			var optionViewModel = (OptionViewModel) DataContext;
			optionViewModel.SingleOptionCommand.Execute(datePicker.SelectedDate);
		}
	}
}
