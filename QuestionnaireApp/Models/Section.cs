using System.Xml.Serialization;

namespace QuestionnaireApp.Models
{
	public class Section
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