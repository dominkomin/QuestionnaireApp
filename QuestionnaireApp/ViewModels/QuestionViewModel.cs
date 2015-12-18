using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using PostSharp.Patterns.Model;
using QuestionnaireApp.Models;
using QuestionnaireApp.Views;

namespace QuestionnaireApp.ViewModels
{
	[NotifyPropertyChanged]
	internal class QuestionViewModel
	{
		public bool IsMandatory { get; private set; }
		public string Text { get; private set; }
		public string Description { get; private set; }
		private object _answer;
		public int TargetSectionId { get; set; }
		public ObservableCollection<object> Options { get; private set; }

		public QuestionType QuestionType { get; private set; }

		public Dictionary<OptionViewModel, object> OptionsViewModelView
		{
			get { return _optionsViewModelView; }
		}

		private readonly Dictionary<OptionViewModel, object> _optionsViewModelView;
		public int Id { get; private set; }

		public object Answer
		{
			get { return _answer; }
			set
			{
				_answer = value;
				foreach (var optionViewModelView in OptionsViewModelView.Where(optionViewModelView => optionViewModelView.Key.Condition != null))
				{
					optionViewModelView.Key.Condition.ActualValue = _answer.ToString();
				}
			}
		}

		public QuestionViewModel(QuestionnaireSectionQuestion question)
		{
			_optionsViewModelView =  new Dictionary<OptionViewModel, object>();
			Id = question.id;
			Options = new ObservableCollection<object>();
			Text = Id + ". " + question.Text;
			IsMandatory = question.IsMandatory;
			Description = question.Description;
			TargetSectionId = question.TargetSectionId;

			QuestionType = new QuestionType(question.Type);

			if (question.Options.Length == 1)
			{
				MakeSingleOption(new OptionViewModel(question.Options.First(), this));
			}
			else
			{
				MakeMultipleOption(question.Options);
			}
		}

		private void MakeMultipleOption(IEnumerable<QuestionnaireSectionQuestionOption> options)
		{
			if (QuestionType.Type == QuestionTypeOption.Scale)
			{
				var optionsList = options as IList<QuestionnaireSectionQuestionOption> ?? options.ToList();
				var optionViewModel = new OptionViewModel(optionsList.First(), this)
				{
					MinValue = int.Parse(optionsList.First().Content),
					MaxValue = int.Parse(optionsList.Last().Content),
					MinLabel = optionsList.First().Description,
					MaxLabel = optionsList.Last().Description
				};
				MakeSingleOption(optionViewModel);
			}
			else if (QuestionType.Type == QuestionTypeOption.Dropdown)
			{
				var optionsList = options as IList<QuestionnaireSectionQuestionOption> ?? options.ToList();
				var optionViewModel = new OptionViewModel(optionsList.First(), this);
				foreach (var option in optionsList)
				{
					optionViewModel.DropList.Add(option.Content);
				}
				MakeSingleOption(optionViewModel);
			}
			else
			{
				foreach (var option in options)
				{
					MakeSingleOption(new OptionViewModel(option, this));
				}
			}
		}

		private RadioOptionView MakeRadio(OptionViewModel optionViewModel)
		{
			optionViewModel.RadioGroupName = Id.ToString(CultureInfo.InvariantCulture);

			var radioOptionView = new RadioOptionView
			{
				DataContext = optionViewModel
			};
			return radioOptionView;
		}

		private CheckboxOptionView MakeCheckbox(OptionViewModel optionViewModel)
		{
			var checkboxOptionView = new CheckboxOptionView
			{
				DataContext = optionViewModel
			};
			return checkboxOptionView;
		}

		private ScaleOptionView MakeScale(OptionViewModel optionViewModel)
		{
			var scaleOptionView = new ScaleOptionView
			{
				DataContext = optionViewModel
			};
			return scaleOptionView;
		}

		private TextOptionView MakeText(OptionViewModel optionViewModel)
		{
			var textOptionView = new TextOptionView
			{
				DataContext = optionViewModel
			};
			return textOptionView;
		}

		private NumberOptionView MakeNumber(OptionViewModel optionViewModel)
		{
			var numberOptionView = new NumberOptionView
			{
				DataContext = optionViewModel
			};
			return numberOptionView;
		}

		private DateOptionView MakeDate(OptionViewModel optionViewModel)
		{
			var dateOptionView = new DateOptionView
			{
				DataContext = optionViewModel
			};
			return dateOptionView;
		}

		private DropDownView MakeDropDown(OptionViewModel optionViewModel)
		{
			var dropDownView = new DropDownView
			{
				DataContext = optionViewModel
			};
			return dropDownView;
		}

		private void MakeSingleOption(OptionViewModel optionViewModel)
		{
			object optionView;

			switch (QuestionType.Type)
			{
				case QuestionTypeOption.Text:
					optionView = MakeText(optionViewModel);
					break;
				case QuestionTypeOption.Date:
					optionView = MakeDate(optionViewModel);
					break;
				case QuestionTypeOption.Scale:
					optionView = MakeScale(optionViewModel);
					break;
				case QuestionTypeOption.Number:
					optionView = MakeNumber(optionViewModel);
					break;
				case QuestionTypeOption.Radio:
					optionView = MakeRadio(optionViewModel);
					break;
				case QuestionTypeOption.Checkbox:
					optionView = MakeCheckbox(optionViewModel);
					break;
				case QuestionTypeOption.Dropdown:
					optionView = MakeDropDown(optionViewModel);
					break;
				default:
					throw new Exception("Bad question type");
			}
			OptionsViewModelView.Add(optionViewModel, optionView);
			Options.Add(optionView);
		}
	}
}