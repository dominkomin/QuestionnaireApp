using System.Collections.Generic;
using System.Windows.Input;
using PostSharp.Patterns.Model;
using QuestionnaireApp.Commands;
using QuestionnaireApp.Models;

namespace QuestionnaireApp.ViewModels
{
	[NotifyPropertyChanged]
	internal class OptionViewModel
	{
		public ICommand SingleOptionCommand { get; private set; }

		public QuestionViewModel Question { get; set; }

		public int Id { get; private set; }
		public string Content { get; private set; }
		public string Description { get; private set; }
		public string RadioGroupName { get; set; }
		public int MinValue { get; set; }
		public int MaxValue { get; set; }
		public string MinLabel { get; set; }
		public string MaxLabel { get; set; }
		public List<string> DropList { get; set; }

		public Condition Condition { get; private set; }

		public OptionViewModel(QuestionnaireSectionQuestionOption option, QuestionViewModel questionViewModel)
		{
			DropList = new List<string>();
			Question = questionViewModel;
			SingleOptionCommand = new SingleOptionCommand(this);
			Id = option.id;
			Content = option.Content;
			Description = option.Description;

			if (option.Condition != null)
			{
				if (option.Condition.Operator == null)
				{
					Condition = new Condition("EQ", option.Content, option.Condition.TargetSectionId);
				}
				else
				{
					Condition = new Condition(option.Condition.Operator, option.Condition.ExpectedValue,
						option.Condition.TargetSectionId);
				}
			}
		}
	}
}