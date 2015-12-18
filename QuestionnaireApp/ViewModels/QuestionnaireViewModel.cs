using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Input;
using PostSharp.Patterns.Model;
using QuestionnaireApp.Commands;
using QuestionnaireApp.Models;
using QuestionnaireApp.Views;

namespace QuestionnaireApp.ViewModels
{
	[NotifyPropertyChanged]
	internal class QuestionnaireViewModel
	{
		public SectionViewModel CurrentSectionViewModel { get; private set; }
		public SectionView CurrentSectionView { get; private set; }

		public string Text { get; private set; }
		public string Description { get; private set; }
		public Questionnaire QuestionnaireModel { get; private set; }
		private readonly Dictionary<SectionViewModel, SectionView> _sections = new Dictionary<SectionViewModel, SectionView>();
		public int CurrentSectionId { get; private set; }
		private readonly Stack<int> _previousSectionId = new Stack<int>();
		private readonly List<int> _sectionNumbers;
		private readonly List<int> _skippedSections = new List<int>();

		public ICommand NextSectionCommand { get; private set; }

		public ICommand PreviousSectionCommand { get; private set; }
		public ICommand FinnishCommand { get; private set; }
		private readonly string _appDirPath;

		public QuestionnaireViewModel(Questionnaire questionnaire, string appDirPath)
		{
			_appDirPath = appDirPath;
			var i = 1;
			_sectionNumbers = new List<int>();
			while (i >= 1 & i <= questionnaire.Sections.Length)
			{
				_sectionNumbers.Add(i);
				i++;
			}

			FinnishCommand = new FinnishCommand(this);
			NextSectionCommand = new NextSectionCommand(this);
			PreviousSectionCommand = new PreviousSectionCommand(this);

			QuestionnaireModel = questionnaire;
			Description = questionnaire.Description;
			Text = questionnaire.Text;
			CurrentSectionId = 1;
			LoadSection(CurrentSectionId);
		}

		public void SaveAnswers()
		{
			var file = new StreamWriter(_appDirPath + "questionnaireOutput.txt", false);
			foreach (var section in _sections.Where(section => _skippedSections.All(ss => ss != section.Key.Id)))
			{
				file.WriteLine(section.Key.Text);
				foreach (var question in section.Key.QuestionsViewModelView.Keys)
				{
					file.WriteLine(question.Text);
					if (question.Answer != null)
					{
						file.WriteLine(question.Answer);
					}
					else
					{
						file.WriteLine("empty answer");
					}
					file.WriteLine("");
				}
			}
			file.Flush();
			file.Close();
		}

		public int GetOpenedSectionCount()
		{
			return _sections.Count;
		}

		public void MoveSection(bool toNext)
		{
			if (toNext)
			{
				var targetSection = 0;
				foreach (var question in CurrentSectionViewModel.QuestionsViewModelView.Keys.Where(question => question.Answer != null))
				{
					targetSection = question.TargetSectionId;
				}

				_previousSectionId.Push(CurrentSectionId);
				foreach (var nextSection in (from question in CurrentSectionViewModel.QuestionsViewModelView.Keys
					from option in question.OptionsViewModelView.Keys
					where option.Condition != null
					select option).Select(option => option.Condition.GiveNextSectionNumber()).Where(nextSection => nextSection != 0))
				{
					targetSection = nextSection;
				}

				if (targetSection > (CurrentSectionId + 1))
				{
					_skippedSections.AddRange(_sectionNumbers.Where(sn => sn > CurrentSectionId && sn < targetSection));
					CurrentSectionId = targetSection;
					LoadSection(targetSection);
				}
				else
				{
					LoadSection(++CurrentSectionId);
				}
			}
			else
			{
				CurrentSectionId = _previousSectionId.Pop();
				_skippedSections.RemoveAll(ss => ss > CurrentSectionId);
				LoadSection(CurrentSectionId);
			}
		}

		public int GetSectionsCount()
		{
			return QuestionnaireModel.Sections.Length;
		}

		private void LoadSection(int sectionId)
		{
			KeyValuePair<SectionViewModel, SectionView> sectionViewViewModel =
				_sections.SingleOrDefault(s => s.Key.Id == sectionId);
			if (Equals(null, sectionViewViewModel.Key))
			{
				CurrentSectionViewModel = new SectionViewModel(QuestionnaireModel.Sections.Single(s => s.id == sectionId));
				CurrentSectionView = new SectionView
				{
					DataContext = CurrentSectionViewModel
				};
				_sections.Add(CurrentSectionViewModel, CurrentSectionView);
			}
			else
			{
				CurrentSectionViewModel = sectionViewViewModel.Key;
				CurrentSectionView = sectionViewViewModel.Value;
			}
		}
	}
}