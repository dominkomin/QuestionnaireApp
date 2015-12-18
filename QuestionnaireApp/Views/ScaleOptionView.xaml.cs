using System.Windows.Controls;
using System.Windows.Input;
using QuestionnaireApp.ViewModels;

namespace QuestionnaireApp.Views
{
	/// <summary>
	/// Interaction logic for ScaleOptionView.xaml
	/// </summary>
	public partial class ScaleOptionView
	{
		public ScaleOptionView()
		{
			InitializeComponent();
		}

		private void Slider_OnPreviewMouseUp(object sender, MouseButtonEventArgs e)
		{
			var slider = (Slider)sender;
			var optionViewModel = (OptionViewModel) DataContext;
			optionViewModel.SingleOptionCommand.Execute(slider.Value);
		}
	}
}
