using System.Windows.Controls;
using QuestionnaireApp.ViewModels;

namespace QuestionnaireApp.Views
{
	/// <summary>
	/// Interaction logic for TextOption.xaml
	/// </summary>
	public partial class TextOptionView
	{
		public TextOptionView()
		{
			InitializeComponent();
		}

		private void TextBoxBase_OnTextChanged(object sender, TextChangedEventArgs e)
		{
			var textBox = (TextBox)sender;
			var optionViewModel = (OptionViewModel) DataContext;
			optionViewModel.SingleOptionCommand.Execute(textBox.Text);
		}
	}
}
