using System.Windows.Controls;
using QuestionnaireApp.ViewModels;

namespace QuestionnaireApp.Views
{
	/// <summary>
	/// Interaction logic for DropDownView.xaml
	/// </summary>
	public partial class DropDownView
	{
		public DropDownView()
		{
			InitializeComponent();
		}

		private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			var comboBox = (ComboBox)sender;
			var optionViewModel = (OptionViewModel) DataContext;
			optionViewModel.SingleOptionCommand.Execute(comboBox.SelectedValue);
		}
	}
}
