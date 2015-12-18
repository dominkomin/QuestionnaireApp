using System.Xml.Serialization;

namespace QuestionnaireApp.Models
{
	/// <remarks />
	[XmlType(AnonymousType = true)]
	public class QuestionnaireSectionQuestion
	{
		/// <remarks />
		public string Text { get; set; }

		/// <remarks />
		public string Description { get; set; }

		/// <remarks />
		public bool IsMandatory { get; set; }

		/// <remarks />
		public string Type { get; set; }

		/// <remarks />
		public byte TargetSectionId { get; set; }

		/// <remarks />
		[XmlIgnore]
		public bool TargetSectionIdSpecified { get; set; }

		/// <remarks />
		[XmlArrayItem("Option", IsNullable = false)]
		public QuestionnaireSectionQuestionOption[] Options { get; set; }

		/// <remarks />
		[XmlAttribute]
		public byte id { get; set; }
	}
}