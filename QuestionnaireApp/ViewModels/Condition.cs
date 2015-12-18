using System;

namespace QuestionnaireApp.ViewModels
{
	enum ConditionTypeOption
	{
		Eq,
		Neq,
		Gt,
		Gte,
		Lt,
		Lte,
		Undefined
	}

	class Condition
	{
		private readonly ConditionTypeOption _conditionType = ConditionTypeOption.Undefined;
		public string ExpectedValue { get; private set; }
		public string ActualValue { get; set; }
		public int TargetSection { get; private set; }
		
		
		public Condition(string conditionType, string expectedValue, int targetSection)
		{
			ExpectedValue = expectedValue;
			Enum.TryParse(conditionType, out _conditionType);
			TargetSection = targetSection;
		}

		public int GiveNextSectionNumber()
		{
			if (ExpectedValue == null && ActualValue == null)
			{
				return 0;
			}

			switch (_conditionType)
			{
				case ConditionTypeOption.Eq:
					if (ExpectedValue != null && (ActualValue == ExpectedValue || (ActualValue != null && ActualValue.Contains(ExpectedValue))))
						return TargetSection;
					break;
				case ConditionTypeOption.Neq:
					if (ActualValue != ExpectedValue)
						return TargetSection;
					break;
				case ConditionTypeOption.Gt:
					if (int.Parse(ActualValue) > int.Parse(ExpectedValue))
						return TargetSection;
					break;
				case ConditionTypeOption.Gte:
					if (int.Parse(ActualValue) >= int.Parse(ExpectedValue))
						return TargetSection;
					break;
				case ConditionTypeOption.Lt:
					if (int.Parse(ActualValue) < int.Parse(ExpectedValue))
						return TargetSection;
					break;
				case ConditionTypeOption.Lte:
					if (int.Parse(ActualValue) <= int.Parse(ExpectedValue))
						return TargetSection;
					break;
				default:
					return 0;
			}
			return 0;
		}
	}
}
