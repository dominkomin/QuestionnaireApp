using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using QuestionnaireApp.ViewModels;

namespace QuestionnaireApp.Views
{
	/// <summary>
	/// Interaction logic for CheckboxOptionView.xaml
	/// </summary>
	public partial class CheckboxOptionView : UserControl
	{
		public CheckboxOptionView()
		{
			InitializeComponent();
		}

		private void ToggleButton_OnChecked(object sender, RoutedEventArgs e)
		{
			var slider = (CheckBox)sender;
			var optionViewModel = (OptionViewModel) DataContext;
			if (slider.IsChecked != null && slider.IsChecked.Value)
			{
				optionViewModel.SingleOptionCommand.Execute("+" + slider.Content);
			}
			else
			{
				optionViewModel.SingleOptionCommand.Execute("-" + slider.Content);
			}

			
		}
	}
}
