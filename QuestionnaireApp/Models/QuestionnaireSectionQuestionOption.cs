using System.Xml.Serialization;

namespace QuestionnaireApp.Models
{
	/// <remarks />
	[XmlType(AnonymousType = true)]
	public class QuestionnaireSectionQuestionOption
	{
		/// <remarks />
		public string Content { get; set; }

		/// <remarks />
		public string Description { get; set; }

		/// <remarks />
		public QuestionnaireSectionQuestionOptionCondition Condition { get; set; }

		/// <remarks />
		[XmlAttribute]
		public byte id { get; set; }
	}
}