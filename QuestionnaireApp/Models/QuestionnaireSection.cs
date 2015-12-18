using System.Xml.Serialization;

namespace QuestionnaireApp.Models
{
	/// <remarks />
	[XmlType(AnonymousType = true)]
	public class QuestionnaireSection
	{
		/// <remarks />
		public string Text { get; set; }

		/// <remarks />
		public string Description { get; set; }

		/// <remarks />
		[XmlArrayItem("Question", IsNullable = false)]
		public QuestionnaireSectionQuestion[] Questions { get; set; }

		/// <remarks />
		[XmlAttribute]
		public byte id { get; set; }
	}
}