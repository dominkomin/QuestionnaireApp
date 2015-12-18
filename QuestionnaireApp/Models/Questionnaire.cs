using System.Xml.Serialization;

namespace QuestionnaireApp.Models
{
		/// <remarks />
		[XmlType(AnonymousType = true)]
		[XmlRoot(Namespace = "", IsNullable = false)]
		public class Questionnaire
		{
			/// <remarks />
			public string Text { get; set; }

			/// <remarks />
			public string Description { get; set; }

			/// <remarks />
			[XmlArrayItem("Section", IsNullable = false)]
			public QuestionnaireSection[] Sections { get; set; }
		}
}