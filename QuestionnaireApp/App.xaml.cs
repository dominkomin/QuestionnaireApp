using System;
using System.Windows;
using System.Xml;
using System.Xml.Serialization;
using QuestionnaireApp.Models;
using QuestionnaireApp.ViewModels;

namespace QuestionnaireApp
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App
	{
		protected override void OnStartup(StartupEventArgs e)
		{
			var serializer = new XmlSerializer(typeof (Questionnaire));
			Questionnaire questionnaire;
			var appDirPath = AppDomain.CurrentDomain.BaseDirectory;
			try
			{
				using (var reader = XmlReader.Create(appDirPath + "q1.xml"))
				{
					questionnaire = (Questionnaire) serializer.Deserialize(reader);
				}
			}
			catch (Exception exception)
			{
				MessageBox.Show(exception.Message + ". Copy questionnaire XML file to " + appDirPath);
				Shutdown();
				return;
			}


			var main = new Views.MainWindow
			{
				DataContext = new QuestionnaireViewModel(questionnaire, appDirPath)
			};
			main.Show();
			base.OnStartup(e);
		}
	}
}