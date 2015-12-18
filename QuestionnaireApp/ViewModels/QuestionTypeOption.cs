
using System;

namespace QuestionnaireApp.ViewModels
{
	public enum QuestionTypeOption
	{
		Text,
		Checkbox,
		Radio,
		Dropdown,
		Scale,
		Date,
		Number,
		Undefined
	}

	public class QuestionType
	{
		readonly QuestionTypeOption _type = QuestionTypeOption.Undefined;
		
		
		public QuestionType(string questionType)
		{
			Enum.TryParse(questionType, out _type);
		}

		public QuestionTypeOption Type
		{
			get { return _type; }
		}
	}
}
