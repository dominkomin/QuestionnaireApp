using System.Collections.Generic;
using System.Collections.ObjectModel;
using QuestionnaireApp.Models;
using QuestionnaireApp.Views;
using PostSharp.Patterns.Model;

namespace QuestionnaireApp.ViewModels
{
	[NotifyPropertyChanged]
	class SectionViewModel
	{
		public string Text { get; set; }
		public string Description { get; set; }
		public ObservableCollection<QuestionView> Questions { get; set; }
		public int Id { get; set; }

		public Dictionary<QuestionViewModel, QuestionView> QuestionsViewModelView
		{
			get { return _questionsViewModelView; }
		}

		private readonly Dictionary<QuestionViewModel, QuestionView> _questionsViewModelView;


		public SectionViewModel(QuestionnaireSection section)
		{
			_questionsViewModelView = new Dictionary<QuestionViewModel, QuestionView>();
			Questions = new ObservableCollection<QuestionView>();
			Text = section.Text;
			Id = section.id;
			Description = section.Description;

			foreach (var question in section.Questions)
			{
				var questionViewModel = new QuestionViewModel(question);
				var questionView = new QuestionView
				{
					DataContext = questionViewModel
				};
				Questions.Add(questionView);
				QuestionsViewModelView.Add(questionViewModel, questionView);
			}
		}
	}
}
