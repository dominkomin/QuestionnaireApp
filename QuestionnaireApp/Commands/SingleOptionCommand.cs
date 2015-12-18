using System;
using System.Windows.Input;
using QuestionnaireApp.ViewModels;

namespace QuestionnaireApp.Commands
{
	internal class SingleOptionCommand : ICommand
	{
		private readonly OptionViewModel _viewModel;

		/// <summary>
		/// Initializes a new instance of the UpdateCustomerCommand class.
		/// </summary>
		public SingleOptionCommand(OptionViewModel viewModel)
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
			return true;
		}

		public void Execute(object parameter)
		{
			if (_viewModel.Question.QuestionType.Type != QuestionTypeOption.Checkbox)
			{
				_viewModel.Question.Answer = parameter.ToString();
			}
			else
			{
				if (parameter.ToString()[0] == '+')
				{
					_viewModel.Question.Answer += parameter.ToString().Substring(1) + ";";
				}
				else
				{
					_viewModel.Question.Answer = _viewModel.Question.Answer.ToString()
						.Replace(parameter.ToString().Substring(1) + ";", "");
				}
			}
		}
	}
}