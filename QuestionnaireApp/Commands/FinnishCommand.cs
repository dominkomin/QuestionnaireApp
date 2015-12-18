using System;
using System.Windows.Input;
using QuestionnaireApp.ViewModels;

namespace QuestionnaireApp.Commands
{
	internal class FinnishCommand : ICommand
	{
		private readonly QuestionnaireViewModel _viewModel;

		/// <summary>
		/// Initializes a new instance of the UpdateCustomerCommand class.
		/// </summary>
		public FinnishCommand(QuestionnaireViewModel viewModel)
		{
			_viewModel = viewModel;
		}

		public event EventHandler CanExecuteChanged
		{
			add { CommandManager.RequerySuggested += value; }
			remove { CommandManager.RequerySuggested -= value; }
		}

		public bool CanExecute(object parameter)
		{
			return _viewModel.GetOpenedSectionCount() > 0;
		}

		public void Execute(object parameter)
		{
			_viewModel.SaveAnswers();
		}
	}
}