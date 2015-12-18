using System.Xml.Serialization;

namespace QuestionnaireApp.Models
{
	[XmlType(AnonymousType = true)]
	public class QuestionnaireSectionQuestionOptionCondition
	{
		/// <remarks />
		public string ExpectedValue { get; set; }

		/// <remarks />
		[XmlIgnore]
		public bool ExpectedValueSpecified { get; set; }

		/// <remarks />
		public string Operator { get; set; }

		/// <remarks />
		public int TargetSectionId { get; set; }
	}
}